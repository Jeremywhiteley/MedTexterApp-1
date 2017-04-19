using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MedTexterApp.Views
{
    public class masterPage:MasterDetailPage
    {
        List<Items> list = new List<Items>();
        public masterPage()
        {
            // Creating list of items class           
            List<Items> list = new List<Items>
            {
                new Items ("First Item"),
                new Items ("Second Item"),
                new Items ("Third Item"),
                new Items ("Fourth Item"),
            };

            // Populating items into ListView
            ListView listView = new ListView();
            var Template = new DataTemplate(typeof(TextCell));
            Template.SetBinding(TextCell.TextProperty, "Name");
            Template.SetValue(TextCell.TextColorProperty, Color.White);
            listView.ItemTemplate = Template;
            listView.ItemsSource = list;
            listView.RowHeight = 70;
            listView.SeparatorColor = Color.White;
            listView.SeparatorVisibility = SeparatorVisibility.Default;

            // Declaring the Master property of the MasterDetailPage 
            this.Master = new ContentPage
            {
                Title = "MedTexterApp",
                Content = listView,
                BackgroundColor = Color.Transparent
            };

            // Setting the DetailPage to the Detail Property of the MasterDetailPage
            this.Detail = new NavigationPage(new detailpage());
        }
    }
}
