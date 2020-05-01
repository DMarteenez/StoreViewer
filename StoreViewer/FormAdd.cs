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
        DataRepository dataRep;
        Form1 parrent;

        public FormAdd(Form _parrent, DataRepository _dataRep)
        {
            InitializeComponent();

            dataRep = _dataRep;
            parrent = _parrent as Form1;
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
                dataRep.Add(new Product(
                    (int)numericUpDown1.Value,
                    (int)numericUpDown2.Value,
                    textBox3.Text,
                    dateTimePicker1.Value,
                    (int)numericUpDown3.Value,
                    (int)numericUpDown4.Value,
                    (double)numericUpDown5.Value));
                if (dataRep._errorOccurred)
                {
                    MessageBox.Show(dataRep._errorMessage);
                    dataRep._errorOccurred = false;
                }
                else
                {
                    MessageBox.Show("Record added sucessfully."); 
                }
                
            }
        }

        private void FormAdd_FormClosed(object sender, FormClosedEventArgs e)
        {
            parrent.button1_Click(sender, e);
        }
    }
}
