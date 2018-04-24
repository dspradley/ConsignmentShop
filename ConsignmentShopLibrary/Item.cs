using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Daniel Spradley
namespace ConsignmentShopLibrary
{
    public class Item
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public bool Sold { get; set; }

        public bool PaymentDistributed { get; set; }
       
        public Vendor Owner { get; set; }

        public decimal Price { get; set; }

        //No private value used as storage. This will just read other properties and puts them together.
        public string Display
        {
            get
            {
                //Place holders in the curly braces for our inputting values. 
                return string.Format("{0} - ${1}", Title, Price);
            }
        }

    }
}
