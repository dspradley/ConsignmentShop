using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsignmentShopLibrary
{
    
    public class Vendor
    {
        //With auto properties you can avoid having to type out a private variable then have get and set methods within a class. It allows for them to be visible outside
        //Of our class and use them in interfaces which normally no member variable can do. 
        //prob tab tab is our shortcut. 
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public double Commision { get; set; }
        //Amount of money due to vendor based on sales.
        public decimal PaymentDue { get; set; }

        //Going to create a default commision rate. ctor tab tab is the shortcut for constructor in c#.
        public Vendor()
        {
            Commision = .5;
        }

        public string Display
        {
            get
            {
                //Place holders in the curly braces for our inputting values. 
                return string.Format("{0} {1} - ${2}", FirstName, LastName, PaymentDue);
            }
        }

    }
}
