using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Linq;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace MedTexterApp
{
    public class DocumentDBService : IDocumentDBService
    {
        public List<LoginItem> Items { get; private set; }
        public string UserId { get; private set; }

        DocumentClient client;
        Uri collectionLink;
        Uri collectionLink1;

        public DocumentDBService()
        {
            // Link to the DocumentDB with the database name and collection name 
            collectionLink = UriFactory.CreateDocumentCollectionUri(Constants.DatabaseName, Constants.CollectionName);
            collectionLink1 = UriFactory.CreateDocumentCollectionUri(Constants.DatabaseName, "Login");
        }

        // Method that will allow user to login to the facebook
        public async Task<bool> LoginAsync(Xamarin.Forms.Page page)
        {
            string resourceToken = null;
            var tcs = new TaskCompletionSource<bool>();
            LoginItem item;

#if __IOS__
            var controller = UIKit.UIApplication.SharedApplication.KeyWindow.RootViewController;
#endif
            try
            {
                // Opening the facebook webauthenticator
                var auth = new Xamarin.Auth.WebRedirectAuthenticator(
                    new Uri(Constants.ResourceTokenBrokerUrl + "/.auth/login/facebook"),
                    new Uri(Constants.ResourceTokenBrokerUrl + "/.auth/login/done"));

                auth.Completed += async (sender, e) =>
                {
                    if (e.IsAuthenticated && e.Account.Properties.ContainsKey("token"))
                    {
#if __IOS__
						controller.DismissViewController(true, null);
#endif
                        var easyAuthResponseJson = JsonConvert.DeserializeObject<JObject>(e.Account.Properties["token"]);
                        var easyAuthToken = easyAuthResponseJson.GetValue("authenticationToken").ToString();

                        // Call the ResourceBroker to get the DocumentDB resource token
                        using (var httpClient = new HttpClient())
                        {

                            httpClient.DefaultRequestHeaders.Add("x-zumo-auth", easyAuthToken);
                            var response = await httpClient.GetAsync(Constants.ResourceTokenBrokerUrl + "/api/resourcetoken/");
                            var jsonString = await response.Content.ReadAsStringAsync();
                            var tokenJson = JsonConvert.DeserializeObject<JObject>(jsonString);
                            resourceToken = tokenJson.GetValue("token").ToString();
                            UserId = tokenJson.GetValue("userid").ToString();

                            if (!string.IsNullOrWhiteSpace(resourceToken))
                            {
                                item = new LoginItem();
                                item.Email = item.UserId;
                                item.FacebookId = "test";
                                item.FirstName = "test";
                                item.LastName = "test";
                                client = new DocumentClient(new Uri(Constants.EndpointUri), resourceToken);
                                await SaveLoginItemAsync(item);
                                tcs.SetResult(true);
                            }
                            else
                            {
                                tcs.SetResult(false);
                            }
                        }
                    }
                };

#if __IOS__
				controller.PresentViewController(auth.GetUI(), true, null);
#elif __ANDROID__
                Xamarin.Forms.Forms.Context.StartActivity(auth.GetUI(Xamarin.Forms.Forms.Context));
#endif
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: ", ex.Message);
            }

            await tcs.Task;
            return tcs.Task.Result;
        }        

        // Save the information into the DocumentDB collection
        public async Task SaveLoginItemAsync(LoginItem item, bool isNewItem = true)
        {
            try
            {
                if (isNewItem)
                {
                    item.UserId = UserId;
                    string primaryKey = "DABq3WN4topQNBhrNRnI1CE2AFrSuZCcNb4WZKBvzeX0bFXqYG05RrGN4PdfOHCq5M0AZOS8f0f2XgS7DJLwog==";
                    client = new DocumentClient(new Uri(Constants.EndpointUri), primaryKey);
                    await client.CreateDocumentAsync(collectionLink1, item);
                }
                else
                {
                    await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(Constants.DatabaseName, Constants.CollectionName, item.UserId), item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: ", ex.Message);
            }
        }       
    }
}
