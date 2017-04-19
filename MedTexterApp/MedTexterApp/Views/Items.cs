using System;
using System.Collections.Generic;
using System.Text;

namespace MedTexterApp.Views
{
    // Class that will bind the items into MasterDetail Page ListView
    public class Items
    {
        public Items(string name)
        {
            this.Name = name;
        }
        public string Name { private set; get; }
    }
}
