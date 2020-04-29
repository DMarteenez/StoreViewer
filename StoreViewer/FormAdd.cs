using StoreDataCreator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace StoreViewer
{
    public partial class FormAdd : Form
    {
        string dataFilePath;

        public FormAdd(string _dataFilePath)
        {
            InitializeComponent();

            dataFilePath = _dataFilePath;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool replaced = true;
            while (replaced)
            {
                replaced = false;
                if (textBox3.Text.Contains("  "))
                {
                    textBox3.Text = textBox3.Text.Replace("  ", " ");
                    replaced = true;
                }  
            }

            if(!textBox3.Text.Any(x => char.IsLetter(x)) ||
               numericUpDown1.Value == 0 ||
               numericUpDown2.Value == 0 ||
               numericUpDown3.Value == 0 ||
               numericUpDown4.Value == 0 ||
               numericUpDown5.Value == 0)
            {
                MessageBox.Show("Error: Empty or zero fields are not allowed.\nProduct name must contain letters.");
            }
            else
            {
                var foundProduct = from p in XDocument.Load(dataFilePath).Descendants("Product")
                                   where (int)p.Attribute("ID") == (int)numericUpDown2.Value
                                   select p;
                
                if (foundProduct.Count() > 0)
                {
                    MessageBox.Show("Error: Record with ID = " + (int)numericUpDown2.Value + " already exists.");
                }
                else
                {
                    XDocument doc = XDocument.Load(dataFilePath);

                    var newProduct = new XElement("Product",
                                        new XAttribute("ID", (int)numericUpDown2.Value),
                                        new XElement("StoreID", (int)numericUpDown1.Value),
                                        new XElement("Name", textBox3.Text),
                                        new XElement("Date", dateTimePicker1.Value.ToString("yyyy-MM-dd")),
                                        new XElement("ShelfLife", (int)numericUpDown3.Value),
                                        new XElement("Quantity", (int)numericUpDown4.Value),
                                        new XElement("Price", (double)numericUpDown5.Value)
                                    );

                    doc.Element("Products").Add(newProduct);
                    doc.Save(dataFilePath);

                    MessageBox.Show("Record added sucessfully.");
                } 
            }
            //label2.Text = dataFilePath;
        }
    }
}
