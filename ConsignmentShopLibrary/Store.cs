using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Daniel Spradley
namespace ConsignmentShopLibrary
{   
    //This is our backend class. 
    public class Store
    {
        public string Name { get; set; }
        //Public list of type vendor in a list form. Not just single instances of that type
        public List<Vendor> Vendors { get; set; }

        public List<Item> Items { get; set; }

        public Store()
        {
            //Instantition before use. 
            Vendors = new List<Vendor>();
            Items = new List<Item>();
        }
    }
}
