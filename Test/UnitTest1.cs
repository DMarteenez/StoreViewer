using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoreViewer;
using StoreDataCreator;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Test
{
    [TestClass]
    public class UnitTest1
    {
        DataRepository datRep;
        string _path = "Doc.xml";
        [TestMethod]
        public void TestMethod1()
        {
            datRep = new DataRepository();
            datRep.SetFilePath(_path);

            string path = datRep.GetFilePath();

            Assert.AreEqual(path, "Doc.xml");
        }
        [TestMethod]
        public void TestMethod2()
        {
            datRep = new DataRepository();
            datRep.SetFilePath(_path);
            List<Product> prod = new List<Product>();

            prod.Add(new Product(1, 1, "Pencil", new DateTime(2020, 4, 28), 100, 5, 9.99));
            prod.Add(new Product(1, 2, "Milk", new DateTime(2019, 4, 14), 5, 10, 5));
            prod.Add(new Product(2, 3, "Gold bar", new DateTime(1578, 6, 2), 99999, 10, 54915.73));
            prod.Add(new Product(2, 4, "Watch", new DateTime(2012, 8, 17), 3, 50, 385.35));

            XDocument doc = new XDocument
            (
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("Products",
                    from p
                    in prod
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
            doc.Save(_path);

            datRep.Load();

            List<Product> prod2 = datRep._prods;
            if(prod.Count != prod2.Count)
            {
                Assert.Fail();
            }
            for(int i = 0; i < prod.Count; i++)
            {
                if(prod[i].ID != prod2[i].ID ||
                    prod[i].storeID != prod2[i].storeID ||
                    prod[i].shelfLife != prod2[i].shelfLife ||
                    prod[i].quantity != prod2[i].quantity ||
                    prod[i].price != prod2[i].price ||
                    prod[i].name != prod2[i].name ||
                    prod[i].arrivalDate != prod2[i].arrivalDate)
                {
                    Assert.Fail();
                }
            }
        }
        [TestMethod]
        public void TestMethod3()
        {
            datRep = new DataRepository();
            datRep.SetFilePath(_path);
            List<Product> prod = new List<Product>();

            prod.Add(new Product(1, 1, "Pencil", new DateTime(2020, 4, 28), 100, 5, 9.99));
            prod.Add(new Product(1, 1, "Milk", new DateTime(2019, 4, 14), 5, 10, 5));
            prod.Add(new Product(2, 3, "Gold bar", new DateTime(1578, 6, 2), 99999, 10, 54915.73));
            prod.Add(new Product(2, 4, "Watch", new DateTime(2012, 8, 17), 3, 50, 385.35));

            XDocument doc = new XDocument
            (
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("Products",
                    from p
                    in prod
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
            doc.Save(_path);

            datRep.Load();

            Assert.AreEqual(datRep._errorOccurred, true);
        }

        [TestMethod]
        public void TestMethod4()
        {
            datRep = new DataRepository();
            datRep.SetFilePath(_path);
            List<Product> prod = new List<Product>();

            prod.Add(new Product(1, 1, "", new DateTime(2020, 4, 28), 100, 5, 9.99));
            prod.Add(new Product(1, 2, "Milk", new DateTime(2019, 4, 14), 5, 10, 5));
            prod.Add(new Product(2, 3, "Gold bar", new DateTime(1578, 6, 2), 99999, 10, 54915.73));
            prod.Add(new Product(2, 4, "Watch", new DateTime(2012, 8, 17), 3, 50, 385.35));

            XDocument doc = new XDocument
            (
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("Products",
                    from p
                    in prod
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
            doc.Save(_path);

            datRep.Load();

            Assert.AreEqual(datRep._errorOccurred, true);
        }
        [TestMethod]
        public void TestMethod5()
        {
            datRep = new DataRepository();
            datRep.SetFilePath(_path);
            List<Product> prod = new List<Product>();

            prod.Add(new Product(1, -1, "Pencil", new DateTime(2020, 4, 28), 100, 5, 9.99));
            prod.Add(new Product(1, 2, "Milk", new DateTime(2019, 4, 14), 5, 10, 5));
            prod.Add(new Product(2, -3, "Gold bar", new DateTime(1578, 6, 2), 99999, 10, 54915.73));
            prod.Add(new Product(2, 4, "Watch", new DateTime(2012, 8, 17), 3, 50, 385.35));

            XDocument doc = new XDocument
            (
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("Products",
                    from p
                    in prod
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
            doc.Save(_path);

            datRep.Load();

            Assert.AreEqual(datRep._errorOccurred, true);
        }
        [TestMethod]
        public void TestMethod6()
        {
            datRep = new DataRepository();
            datRep.SetFilePath(_path);
            List<Product> prod = new List<Product>();

            prod.Add(new Product(1, 1, "Pencil", new DateTime(2020, 4, 28), 100, 5, 9.99));
            prod.Add(new Product(1, 2, "Milk", new DateTime(2019, 4, 14), 5, 10, 5));
            prod.Add(new Product(2, 3, "Gold bar", new DateTime(1578, 6, 2), 99999, 10, 54915.73));
            prod.Add(new Product(2, 4, "Watch", new DateTime(2012, 8, 17), 3, 50, 385.35));

            XDocument doc = new XDocument
            (
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("Products",
                    from p
                    in prod
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
            doc.Save(_path);
            datRep.Load();
            datRep.Add(new Product(17, 8, "Apple", new DateTime(2012, 8, 17), 3, 50, 385.35));
            datRep.Save();
            prod.Add(new Product(17, 8, "Apple", new DateTime(2012, 8, 17), 3, 50, 385.35));
            List<Product> prod2 = datRep._prods;
            if (prod.Count != prod2.Count)
            {
                Assert.Fail();
            }
            for (int i = 0; i < prod.Count; i++)
            {
                if (prod[i].ID != prod2[i].ID ||
                    prod[i].storeID != prod2[i].storeID ||
                    prod[i].shelfLife != prod2[i].shelfLife ||
                    prod[i].quantity != prod2[i].quantity ||
                    prod[i].price != prod2[i].price ||
                    prod[i].name != prod2[i].name ||
                    prod[i].arrivalDate != prod2[i].arrivalDate)
                {
                    Assert.Fail();
                }
            }
        }
        [TestMethod]
        public void TestMethod7()
        {
            datRep = new DataRepository();
            datRep.SetFilePath(_path);
            List<Product> prod = new List<Product>();

            prod.Add(new Product(1, 1, "Pencil", new DateTime(2020, 4, 28), 100, 5, 9.99));
            prod.Add(new Product(1, 2, "Milk", new DateTime(2019, 4, 14), 5, 10, 5));
            prod.Add(new Product(2, 3, "Gold bar", new DateTime(1578, 6, 2), 99999, 10, 54915.73));
            prod.Add(new Product(2, 4, "Watch", new DateTime(2012, 8, 17), 3, 50, 385.35));

            XDocument doc = new XDocument
            (
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("Products",
                    from p
                    in prod
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
            doc.Save(_path);
            datRep.Load();
            datRep.Add(new Product(17, 4, "Apple", new DateTime(2012, 8, 17), 3, 50, 385.35));
            datRep.Save();
            Assert.AreEqual(datRep._errorOccurred, true);
        }
        [TestMethod]
        public void TestMethod8()
        {
            datRep = new DataRepository();
            datRep.SetFilePath(_path);
            List<Product> prod = new List<Product>();

            prod.Add(new Product(1, 1, "Pencil", new DateTime(2020, 4, 28), 100, 5, 9.99));
            prod.Add(new Product(1, 2, "Milk", new DateTime(2019, 4, 14), 5, 10, 5));
            prod.Add(new Product(2, 3, "Gold bar", new DateTime(1578, 6, 2), 99999, 10, 54915.73));
            prod.Add(new Product(2, 4, "Watch", new DateTime(2012, 8, 17), 3, 50, 385.35));

            XDocument doc = new XDocument
            (
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("Products",
                    from p
                    in prod
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
            doc.Save(_path);
            datRep.Load();
            datRep.Delete(1);
            datRep.Save();
            prod = new List<Product>();

            prod.Add(new Product(1, 2, "Milk", new DateTime(2019, 4, 14), 5, 10, 5));
            prod.Add(new Product(2, 3, "Gold bar", new DateTime(1578, 6, 2), 99999, 10, 54915.73));
            prod.Add(new Product(2, 4, "Watch", new DateTime(2012, 8, 17), 3, 50, 385.35));
            List<Product> prod2 = datRep._prods;
            if (prod.Count != prod2.Count)
            {
                Assert.Fail();
            }
            for (int i = 0; i < prod.Count; i++)
            {
                if (prod[i].ID != prod2[i].ID ||
                    prod[i].storeID != prod2[i].storeID ||
                    prod[i].shelfLife != prod2[i].shelfLife ||
                    prod[i].quantity != prod2[i].quantity ||
                    prod[i].price != prod2[i].price ||
                    prod[i].name != prod2[i].name ||
                    prod[i].arrivalDate != prod2[i].arrivalDate)
                {
                    Assert.Fail();
                }
            }
        }
        [TestMethod]
        public void TestMethod9()
        {
            datRep = new DataRepository();
            datRep.SetFilePath(_path);
            List<Product> prod = new List<Product>();

            prod.Add(new Product(1, 1, "Pencil", new DateTime(2020, 4, 28), 100, 5, 9.99));
            prod.Add(new Product(1, 2, "Milk", new DateTime(2019, 4, 14), 5, 10, 5));
            prod.Add(new Product(2, 3, "Gold bar", new DateTime(1578, 6, 2), 99999, 10, 54915.73));
            prod.Add(new Product(2, 4, "Watch", new DateTime(2012, 8, 17), 3, 50, 385.35));

            XDocument doc = new XDocument
            (
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("Products",
                    from p
                    in prod
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
            doc.Save(_path);
            datRep.Load();
            datRep.Delete(12);
            datRep.Save();
            Assert.AreEqual(datRep._errorOccurred, true);
        }
        [TestMethod]
        public void TestMethod10()
        {
            datRep = new DataRepository();
            datRep.SetFilePath(_path);
            List<Product> prod = new List<Product>();

            prod.Add(new Product(1, 1, "Pencil", new DateTime(2020, 4, 28), 100, 5, 9.99));
            prod.Add(new Product(1, 2, "Milk", new DateTime(2019, 4, 14), 5, 10, 5));
            prod.Add(new Product(2, 3, "Gold bar", new DateTime(1578, 6, 2), 99999, 10, 54915.73));
            prod.Add(new Product(2, 4, "Watch", new DateTime(2012, 8, 17), 3, 50, 385.35));

            XDocument doc = new XDocument
            (
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("Products",
                    from p
                    in prod
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
            doc.Save(_path);
            datRep.Load();
            datRep.Save();
            string[] res = datRep.GetData(1,2,"2019-04-14", 0);
            string[] result = new string[8];
            result[0] += 1 + "\n";
            result[1] += 2 + "\n";
            result[2] += "Milk" + "\n";
            result[3] += "2019-04-14" + "\n";
            result[4] += 5 + "\n";
            result[6] += 10 + "\n";
            result[7] += 5 + "\n";
            List<Product> prod2 = datRep._prods;
            for (int i = 0; i < 8; i++)
            {
                if (i != 5)
                {
                    if (result[i] != res[i])
                    {
                        Assert.Fail();
                    }
                }
            }
        }
        [TestMethod]
        public void TestMethod11()
        {
            datRep = new DataRepository();
            datRep.SetFilePath(_path);
            List<Product> prod = new List<Product>();

            prod.Add(new Product(1, 1, "Pencil", new DateTime(2020, 4, 28), 100, 5, 9.99));
            prod.Add(new Product(1, 2, "Milk", new DateTime(2019, 4, 14), 5, 10, 5));
            prod.Add(new Product(2, 3, "Gold bar", new DateTime(1578, 6, 2), 99999, 10, 54915.73));
            prod.Add(new Product(2, 4, "Watch", new DateTime(2012, 8, 17), 3, 50, 385.35));

            XDocument doc = new XDocument
            (
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("Products",
                    from p
                    in prod
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
            doc.Save(_path);
            datRep.Load();
            datRep.Save();
            string[] res = datRep.GetData(34, 2, "2019-04-14", 0);
            string[] result = new string[8];
            result[0] =  null;
            result[1] = null;
            result[2] = null;
            result[3] = null;
            result[4] = null;
            result[6] = null;
            result[7] = null;
            List<Product> prod2 = datRep._prods;
            for (int i = 0; i < 8; i++)
            {
                if (i != 5)
                {
                    if (result[i] != res[i])
                    {
                        Assert.Fail();
                    }
                }
            }
        }
    }
}
