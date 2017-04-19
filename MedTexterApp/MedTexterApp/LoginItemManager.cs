using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedTexterApp
{
    public class LoginItemManager
    {
        IDocumentDBService documentDBService;

        public LoginItemManager(IDocumentDBService service)
        {
            documentDBService = service;
        }
        
        // Method to Authenticate with the facebook
        public Task<bool> LoginAsync(Xamarin.Forms.Page page)
        {
            return documentDBService.LoginAsync(page);
        }      

        // Method to save the login info into the DocumentDB Collection
        public Task SaveLoginItemAsync(LoginItem item, bool isNewItem = false)
        {
            return documentDBService.SaveLoginItemAsync(item, isNewItem);
        }        
    }
}
