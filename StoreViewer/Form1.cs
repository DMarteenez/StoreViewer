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

        DataRepository dataRep;
        
        public Form1()
        {
            InitializeComponent();

            dataRep = new DataRepository();

            textBox1.Text = "-chose file-";   
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            textBox1.Text = openFileDialog1.FileName;
            button4_Click(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!dataRep._ready)
                return;

            FormAdd formAdd = new FormAdd(this, dataRep);
            formAdd.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataRep.SetFilePath(textBox1.Text);
            if (dataRep._errorOccurred)
            {
                MessageBox.Show(dataRep._errorMessage);
                dataRep._errorOccurred = false;
            }
            else
            {
                label23.Text = textBox1.Text;
                dataRep.Load();
                loadComboBoxes();
                button1_Click(sender, e);
            }  
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!dataRep._ready)
                return;

            FormDelete formDelete = new FormDelete(this, Convert.ToInt32(numericUpDown1.Value), dataRep);
            formDelete.Show();      
        }

        private void loadComboBoxes()
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();


            string distinctCheck = "";
            comboBox1.Items.Add("-any-");
            comboBox1.Text = "-any-";
            foreach (var pel in dataRep._prods)
            {
                if (!distinctCheck.Contains(pel.storeID.ToString()))
                {
                    comboBox1.Items.Add(pel.storeID);
                    distinctCheck += pel.storeID;
                }
            }

            distinctCheck = "";
            comboBox2.Items.Add("-any-");
            comboBox2.Text = "-any-";
            foreach (var pel in dataRep._prods)
            {
                if (!distinctCheck.Contains(pel.ID.ToString()))
                {
                    comboBox2.Items.Add(pel.ID);
                    distinctCheck += pel.ID;
                }
            }

            distinctCheck = "";
            comboBox3.Items.Add("-any-");
            comboBox3.Text = "-any-";
            foreach (var pel in dataRep._prods)
            {
                if (!distinctCheck.Contains(pel.arrivalDate.ToString()))
                {
                    comboBox3.Items.Add(pel.arrivalDate);
                    distinctCheck += pel.arrivalDate;
                }
            }

            distinctCheck = "";
            comboBox4.Items.Add("-any-");
            comboBox4.Text = "-any-";
            comboBox4.Items.Add("expired");
            comboBox4.Items.Add("unepired");
        }

        public void button1_Click(object sender, EventArgs e)
        {
            if (!dataRep._ready)
                return;

            string[] res = dataRep.GetData(
                comboBox1.Text == "-any-" ? 0 : Convert.ToInt32(comboBox1.Text),
                comboBox2.Text == "-any-" ? 0 : Convert.ToInt32(comboBox2.Text),
                comboBox3.Text == "-any-" ? "" : comboBox3.Text,
                comboBox4.Text == "-any-" ? 0 : comboBox4.Text == "expired" ? 1 : 0);

            label17.Text = res[1];
            label18.Text = res[0];
            label16.Text = res[2];
            label15.Text = res[3];
            label14.Text = res[4];
            label13.Text = res[6];
            label12.Text = res[7];
            label21.Text = res[5];
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            dataRep.Save();
        }
    }
}
