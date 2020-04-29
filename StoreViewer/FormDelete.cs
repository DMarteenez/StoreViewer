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
    public partial class FormDelete : Form
    {
        int deleteID;
        string dataFilePath;
        bool completed;

        public FormDelete(int _productID, string _dataFilePath)
        {
            InitializeComponent();

            deleteID = _productID;
            dataFilePath = _dataFilePath;
            completed = false;

            label1.Text = "Delete product with ID = " + deleteID + "?";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (completed)
            {
                this.Close();
            }
            else
            {
                var foundProduct = from p in XDocument.Load(dataFilePath).Descendants("Product")
                             where (int)p.Attribute("ID") == deleteID
                             select p;

                if(foundProduct.Count() < 1)
                {
                    label1.Text = "Error: Record not found.";
                }
                else
                {
                    XDocument doc = XDocument.Load(dataFilePath);
                    doc.Descendants("Products").Elements("Product").Where(x => (int)x.Attribute("ID") == deleteID).Remove();
                    doc.Save(dataFilePath);
                    
                    label1.Text = "Record deleted.";
                }

                button1.Location = new Point(83, 74);
                button2.Enabled = false;
                button2.Visible = false;
                completed = true;
            }        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
