using StoreDataCreator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace StoreViewer
{
    public partial class Form1 : Form
    {

        bool brokenData;

        public static string errorLineWrongData = "Error: Wrong input file data format.";
        public static string errorLineFileNotFound = "Error: Input file not found.";

        public Form1()
        {
            InitializeComponent();

            brokenData = false;

            textBox1.Text = "-chose file-";

            if (File.Exists(@"C:\Users\Znatok\Desktop\container.xml"))
            {
                textBox1.Text = @"C:\Users\Znatok\Desktop\container.xml";
                label23.Text = textBox1.Text;
            }
            button4_Click(this, new EventArgs());
        }

        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (brokenData)
                    throw new Exception();

                var prodSt = comboBox1.Text == "-any-" ?
                         from p in XDocument.Load(label23.Text).Descendants("Product")
                         select p :
                         from p in XDocument.Load(label23.Text).Descendants("Product")
                         where (string)p.Element("StoreID") == comboBox1.Text
                         select p;

                var prodStId = comboBox2.Text == "-any-" ?
                               from p in prodSt
                               select p :
                               from p in prodSt
                               where (string)p.Attribute("ID") == comboBox2.Text
                               select p;

                var prodStIdDa = comboBox3.Text == "-any-" ?
                                 from p in prodStId
                                 select p :
                                 from p in prodStId
                                 where (string)p.Element("Date") == comboBox3.Text
                                 select p;

                var prodStIdDaSl = comboBox4.Text == "-any-" ?
                                   from p in prodStIdDa
                                   select p :
                                   comboBox4.Text == "expired" ?
                                   from p in prodStIdDa
                                   where ((DateTime)p.Element("Date")).AddDays((int)p.Element("ShelfLife")) > DateTime.Today
                                   select p :
                                   from p in prodStIdDa
                                   where ((DateTime)p.Element("Date")).AddDays((int)p.Element("ShelfLife")) <= DateTime.Today
                                   select p;

                StringBuilder res = new StringBuilder();

                foreach (var pel in prodStIdDaSl)
                {
                    res.AppendLine((string)pel.Attribute("ID"));
                }
                label17.Text = res.ToString();

                res.Clear();
                foreach (var pel in prodStIdDaSl)
                {
                    res.AppendLine((string)pel.Element("StoreID"));
                }
                label18.Text = res.ToString();

                res.Clear();
                foreach (var pel in prodStIdDaSl)
                {
                    res.AppendLine((string)pel.Element("Name"));
                }
                label16.Text = res.ToString();

                res.Clear();
                foreach (var pel in prodStIdDaSl)
                {
                    res.AppendLine((string)pel.Element("Date"));
                }
                label15.Text = res.ToString();

                res.Clear();
                foreach (var pel in prodStIdDaSl)
                {
                    res.AppendLine((string)pel.Element("ShelfLife"));
                }
                label14.Text = res.ToString();

                res.Clear();
                foreach (var pel in prodStIdDaSl)
                {
                    res.AppendLine((string)pel.Element("Quantity"));
                }
                label13.Text = res.ToString();

                res.Clear();
                foreach (var pel in prodStIdDaSl)
                {
                    res.AppendLine((string)pel.Element("Price"));
                }
                label12.Text = res.ToString();

                res.Clear();
                foreach (var pel in prodStIdDaSl)
                {
                    if (((DateTime)pel.Element("Date")).AddDays((int)pel.Element("ShelfLife")) > DateTime.Today)
                    {
                        res.AppendLine("Expired");
                    }
                    else
                    {
                        res.AppendLine("");
                    }
                }
                label21.Text = res.ToString();
            }
            catch
            {
                MessageBox.Show(errorLineWrongData);
                brokenData = true;
            }
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            textBox1.Text = openFileDialog1.FileName;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (brokenData)
            {
                MessageBox.Show(errorLineWrongData);
            }
            else
            {
                FormAdd formAdd = new FormAdd(label23.Text);
                formAdd.Show();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();

            try
            {
                brokenData = false;
                if (File.Exists(textBox1.Text))
                {
                    label23.Text = textBox1.Text;

                    var doc = from p in XDocument.Load(label23.Text).Descendants("Product")
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

                    foreach (var i in doc)
                    {
                        intTest = i.ID;
                        intTest = i.storeID;
                        intTest = i.shelfLife;
                        intTest = i.quantity;
                        dblTest = i.price;
                        strTest = i.name;
                        dateTest = i.arrivalDate;
                    }

                    loadComboBoxes();
                }
                else
                {
                    MessageBox.Show(errorLineFileNotFound);
                }
            }
            catch
            {
                MessageBox.Show(errorLineWrongData);
                brokenData = true;
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (brokenData)
            {
                MessageBox.Show(errorLineWrongData);
            }
            else
            {
                FormDelete formDelete = new FormDelete(Convert.ToInt32(numericUpDown1.Value), label23.Text);
                formDelete.Show();
            }        
        }

        private void loadComboBoxes()
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();

            var prod2 = from p in XDocument.Load(label23.Text).Descendants("Product")
                        select p;

            string distinctCheck = "";
            comboBox1.Items.Add("-any-");
            comboBox1.Text = "-any-";
            foreach (var pel in prod2)
            {
                if (!distinctCheck.Contains((string)pel.Element("StoreID")))
                {
                    comboBox1.Items.Add((string)pel.Element("StoreID"));
                    distinctCheck += (string)pel.Element("StoreID");
                }
            }

            distinctCheck = "";
            comboBox2.Items.Add("-any-");
            comboBox2.Text = "-any-";
            foreach (var pel in prod2)
            {
                if (!distinctCheck.Contains((string)pel.Attribute("ID")))
                {
                    comboBox2.Items.Add((string)pel.Attribute("ID"));
                    distinctCheck += (string)pel.Attribute("ID");
                }
            }

            distinctCheck = "";
            comboBox3.Items.Add("-any-");
            comboBox3.Text = "-any-";
            foreach (var pel in prod2)
            {
                if (!distinctCheck.Contains((string)pel.Element("Date")))
                {
                    comboBox3.Items.Add((string)pel.Element("Date"));
                    distinctCheck += (string)pel.Element("Date");
                }
            }

            distinctCheck = "";
            comboBox4.Items.Add("-any-");
            comboBox4.Text = "-any-";
            comboBox4.Items.Add("expired");
            comboBox4.Items.Add("unepired");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
