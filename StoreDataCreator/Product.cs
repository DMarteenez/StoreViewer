using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDataCreator
{
    public class Product
    {
        public int storeID;
        public int ID;
        public string name;
        public DateTime arrivalDate;
        public int shelfLife;
        public int quantity;
        public double price;

        public Product()
        {
        }

            public  Product(int _storeID, int _ID, string _name, DateTime _arrivalDate, int _shelfLife, int _quantity, double _price)
        {
            storeID = _storeID;
            ID = _ID;
            name = _name;
            arrivalDate = _arrivalDate;
            shelfLife = _shelfLife;
            quantity = _quantity;
            price = _price;
        }
    }
}
