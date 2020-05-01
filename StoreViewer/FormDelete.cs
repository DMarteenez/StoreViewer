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
        DataRepository dataRep;
        Form1 parrent;

        public FormDelete(Form _parrent, int _productID, DataRepository _dataRep)
        {
            InitializeComponent();

            deleteID = _productID;
            dataRep = _dataRep;
            parrent = _parrent as Form1;

            label1.Text = "Delete product with ID = " + deleteID + "?";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataRep.Delete(deleteID);

            if (dataRep._errorOccurred)
            {
                MessageBox.Show(dataRep._errorMessage);
                dataRep._errorOccurred = false;
            }
            else
            {
                MessageBox.Show("Record deleted."); 
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormDelete_FormClosed(object sender, FormClosedEventArgs e)
        {
            parrent.button1_Click(sender, e);
        }
    }
}
