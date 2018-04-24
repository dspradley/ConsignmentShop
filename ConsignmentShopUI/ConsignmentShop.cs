using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConsignmentShopLibrary;

//Daniel Spradley
namespace ConsignmentShopUI
{

    //Backend code for our form.

    public partial class ConsignmentShop : Form
    {
        //Had to add ConsignmentShopLibrary to our ConsignmentShopUI references list to access our library and then add it above with using. 
        private Store store = new Store();
        //This is our empty shopping cart. 
        private List<Item> shoppingCartData = new List<Item>();

        //Encapsulates the data for a form at the form level. 
        BindingSource itemsBinding = new BindingSource();
        //Encapsulates the data for our cart.
        BindingSource cartBinding = new BindingSource();
        //Encapsulates data for our vendor list.
        BindingSource vendorsBinding = new BindingSource();


        public ConsignmentShop()
        {
            //Don't ever put anything before this 
            InitializeComponent();
            //This is our temporary method that will create dummie data. 
            SetupData();
            //This lamba expression at the end states. For everyitem in our list it assigns them to X, if book sold is no true, returns to a list to add. 
            itemsBinding.DataSource = store.Items.Where(x => x.Sold == false).ToList();
            //This will act as an intimediator. itemsListbox is our outside scrolling list of items on GUI
            itemsListBox.DataSource = itemsBinding;

            //This will call a method from Item which will concatinate Title and Price 
            itemsListBox.DisplayMember = "Display";
            itemsListBox.ValueMember = "Display";

            //Data source for cart binding is our list. 
            cartBinding.DataSource = shoppingCartData;
            //Data source for list box is our cartbinding.
            shoppingCartListBox.DataSource = cartBinding;

            //Need to set display/value member again. 
            shoppingCartListBox.DisplayMember = "Display";
            shoppingCartListBox.ValueMember = "Display";

            //Going to list every vendor so no use to filter
            vendorsBinding.DataSource = store.Vendors;
            vendorListBox.DataSource = vendorsBinding;

            //Adding display ensures its added to our GUI
            vendorListBox.DisplayMember = "Display";
            vendorListBox.ValueMember = "Display";

        }

        
        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void itemsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SetupData()
        {
            /**
            Vendor demoVendor = new Vendor();

            demoVendor.FirstName = "Kevin";
            demoVendor.LastName = "Ver";
            demoVendor.Commision = .5;

            store.Vendors.Add(demoVendor);

            //Can forgoe the Vendor designatio since this is just creating a new instance.
            demoVendor = new Vendor();

            demoVendor.FirstName = "Kevin";
            demoVendor.LastName = "Ver";
            demoVendor.Commision = .5;

            store.Vendors.Add(demoVendor);

            demoVendor = new Vendor();

            demoVendor.FirstName = "Kevin";
            demoVendor.LastName = "Ver";
            demoVendor.Commision = .5;

            store.Vendors.Add(demoVendor);
            **/


            //Creates a new instance of type Vendor, then onces its created populates given values. 
            store.Vendors.Add(new Vendor { FirstName = "Bill", LastName="Smith"});
            store.Vendors.Add(new Vendor { FirstName = "Red", LastName = "Jones"});
            store.Vendors.Add(new Vendor { FirstName = "Burt", LastName = "Terry"});
            store.Vendors.Add(new Vendor { FirstName = "Dave", LastName = "Deryl"});
            
            //For Price its a decimal in our item class but we added an M to show compiler its a double. These are our books. 
            store.Items.Add(new Item
            { Title = "Moby Dick",
                Description = "A book about a whale",
                Price = 4.50M,
                Owner = store.Vendors[2]
            });
            store.Items.Add(new Item
            {
                Title = "Lord of the Rings",
                Description = "A book about a rings",
                Price = 3.50M,
                Owner = store.Vendors[0]
            });
            store.Items.Add(new Item
            {
                Title = "Game of Thrones",
                Description = "A book about a thrones",
                Price = 2.50M,
                Owner = store.Vendors[1]
            });
            store.Items.Add(new Item
            {
                Title = "Harry Potter",
                Description = "A book about wizards",
                Price = 7.0M,
                Owner = store.Vendors[1]
            });
            store.Items.Add(new Item
            {
                Title = "50 Shades of Grey",
                Description = "A book about repressed spousal emotions",
                Price = 6.52M,
                Owner = store.Vendors[3]
            });

            store.Name = "Books, bindings, and beyond";
        }

        //Had to change the buttons properites under events to point towards this method. This will push our selected item to the cart. 
        private void purchaseItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("I have been clicked!");


            //Figures out what is selected. The () was to show that the item in the box is an item. Without it the return type is a general object so we must tell it what type it is. 
            Item selectedItem = (Item)itemsListBox.SelectedItem;

            //Copy that item to the shopping cart. 
            shoppingCartData.Add(selectedItem);
            //We must tell our data source to refresh after the button has been pushed. Pass in true to pass in an entire list but a simple addition we must call this to false. 
            cartBinding.ResetBindings(false);
        }

        //This is what occurs when our purchase button is clicked. This will mark each item as sold then clear cart after.
        private void makePurchase_Click(object sender, EventArgs e)
        {
            //For each loop loops through each item in our basket and hits true for sold. 
            foreach (Item item in shoppingCartData)
            {
                //This will crash if there is nothing in our cart. 
                item.Sold = true;
                //This is calulate whats owed to our vendor. Need the () since decimal and double don't combine well. 
                item.Owner.PaymentDue += (decimal)item.Owner.Commision * item.Price;
            }

            //Clears our basket
            shoppingCartData.Clear();

            //Since during the rebinding of this line above only stores the first object (filtered list) we need this to refilter list. Reestablishes our data/list before binding. 
            itemsBinding.DataSource = store.Items.Where(x => x.Sold == false).ToList();

            //Rereads all items in the list and causes them to be refreshed. Very important. 
            cartBinding.ResetBindings(false);
            //Needed this since we added our lamba expression. 
            itemsBinding.ResetBindings(false);
            //Need to update vendors binding, not just cart and items. 
            vendorsBinding.ResetBindings(false);
        }
    }
}
