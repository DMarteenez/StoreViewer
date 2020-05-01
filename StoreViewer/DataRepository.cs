using StoreDataCreator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StoreViewer
{
    public class DataRepository
    {
        public List<Product> _prods;
        private string _filePath;

        /// <summary>
        /// Set to false after handling an arror
        /// </summary>
        public bool _errorOccurred;
        public string _errorMessage;
        public bool _ready;

        public DataRepository() : this("") { }
        public DataRepository(string filePath)
        {
            _prods = new List<Product>();
            _filePath = filePath;

            _errorOccurred = false;
            _ready = false;
        }

        public string GetFilePath() 
        {
            if (_errorOccurred)
                return null;

            return _filePath; 
        }

        public void SetFilePath(string path)
        {
            if (_errorOccurred)
                return;

            if (File.Exists(path)) 
            {
                _filePath = path;
            }
            else
            {
                _errorOccurred = true;
                _errorMessage = "Error: File not found.";
                return;
            }    
        }

        public void Load()
        {
            if (_errorOccurred)
                return;

            try
            {
                var prods = from p in XDocument.Load(_filePath).Descendants("Product")
                            select new Product
                            {
                                ID = (int)p.Attribute("ID"),
                                storeID = (int)p.Element("StoreID"),
                                name = (string)p.Element("Name"),
                                arrivalDate = (DateTime)p.Element("Date"),
                                shelfLife = (int)p.Element("ShelfLife"),
                                quantity = (int)p.Element("Quantity"),
                                price = (double)p.Element("Price")
                            };

                int intTest;
                double dblTest;
                string strTest;
                DateTime dateTest;

                foreach (var i in prods)
                {
                    intTest = i.ID;
                    intTest = i.storeID;
                    intTest = i.shelfLife;
                    intTest = i.quantity;
                    dblTest = i.price;
                    strTest = i.name;
                    dateTest = i.arrivalDate;
                }

                _prods.Clear();
                foreach (var i in prods)
                {
                    i.expired = (DateTime)i.arrivalDate.AddDays(i.shelfLife) > DateTime.Today;
                    _prods.Add(i);
                }

                _ready = true;
            }
            catch
            {
                _errorOccurred = true;
                _errorMessage = "Error: Wrong input data format.";
            }
            
        }

        public void Save()
        {
            if (_errorOccurred)
                return;

            if(File.Exists(_filePath))
            {
                XDocument doc = new XDocument
            (
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("Products",
                    from p
                    in _prods
                    select new XElement("Product",
                        new XAttribute("ID", p.ID),
                        new XElement("StoreID", p.storeID),
                        new XElement("Name", p.name),
                        new XElement("Date", p.arrivalDate.ToString("yyyy-MM-dd")),
                        new XElement("ShelfLife", p.shelfLife),
                        new XElement("Quantity", p.quantity),
                        new XElement("Price", p.price)
                    )
                )
            );
                doc.Save(_filePath);
            }
            else
            {
                _errorOccurred = true;
                _errorMessage = "Error: File not found.";
                return;
            }
            
        }

        public void Add(Product newPr)
        {
            if (_errorOccurred)
                return;

            foreach (var i in _prods)
            {
                if(newPr.ID == i.ID)
                {
                    _errorOccurred = true;
                    _errorMessage = "Error: Product with ID = " + newPr.ID + " already exists.";
                    return;
                }
            }

            _prods.Add(newPr);
        }

        public void Delete(int delPrID)
        {
            if (_errorOccurred)
                return;

            _errorOccurred = true;
            _errorMessage = "Error: Product with ID = " + delPrID + " not found.";

            foreach (var i in _prods)
            {
                if (delPrID == i.ID)
                {
                    _prods.Remove(i);
                    _errorOccurred = false;
                    _errorMessage = "";
                    return;
                }
            }
        }

        /// <summary>
        /// No filter: 0 for IDs, empty string for date. Expired filter: -1 - false, 0 - no filter, 1 - true
        /// </summary>
        /// <param name="stIdFilter"></param>
        /// <param name="idFilter"></param>
        /// <param name="nameFilter"></param>
        /// <param name="expiredFilter"></param>
        /// <returns></returns>
        public string[] GetData(int stIdFilter, int idFilter, string dateFilter, int expiredFilter)
        {
            if (_errorOccurred)
                return null;

            List<Product> tmpProds1 = new List<Product>();
            List<Product> tmpProds2 = new List<Product>();
            string[] result = new string[8];

            foreach(var i in _prods)
            {
                if(stIdFilter == 0 || i.storeID == stIdFilter)
                {
                    tmpProds1.Add(i);
                }
            }

            tmpProds2.AddRange(tmpProds1);
            tmpProds1.Clear();
            foreach (var i in tmpProds2)
            {
                if (idFilter == 0 || i.ID == idFilter)
                {
                    tmpProds1.Add(i);
                }
            }

            tmpProds2.Clear();
            tmpProds2.AddRange(tmpProds1);
            tmpProds1.Clear();
            try
            {
                foreach (var i in tmpProds2)
                {
                    if (dateFilter.Equals("") || i.arrivalDate == Convert.ToDateTime(dateFilter))
                    {
                        tmpProds1.Add(i);
                    }
                }
            }
            catch
            {
                _errorOccurred = true;
                _errorMessage = "Error: Wrong date filter format.";
                return null;
            }
            

            tmpProds2.Clear();
            tmpProds2.AddRange(tmpProds1);
            tmpProds1.Clear();
            foreach (var i in tmpProds2)
            {
                if (expiredFilter == 0)
                {
                    tmpProds1.Add(i);
                }
                else if (expiredFilter == 1)
                {
                    if (i.expired)
                        tmpProds1.Add(i);
                }
                else // expiredFilter == -1
                {
                    if (!i.expired)
                        tmpProds1.Add(i);
                }
            }

            foreach (var i in tmpProds1)
            {
                result[0] += i.storeID + "\n";
                result[1] += i.ID + "\n";
                result[2] += i.name + "\n";
                result[3] += i.arrivalDate.ToString("yyyy-MM-dd") + "\n";
                result[4] += i.shelfLife + "\n";
                result[5] += (i.expired ? "Expired" : "") + "\n";
                result[6] += i.quantity + "\n";
                result[7] += i.price + "\n";
            }

            return result;
        }
    }
}
