using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        
        BookContext db;

        public List<Sage> s1;
        public List<Book> b1;
        public List<Sage> sg22; 

        Book b;
        Sage s;

        public Form2()
        {
            //Form1 f1 = new Form1();

            InitializeComponent();
            b = new Book();
            db = new BookContext();

            s1 = new List<Sage>();
            sg22 = new List<Sage>();

            List<Sage> sages = db.Sages.ToList();

            foreach(Sage sg in sages)
                comboBox1.Items.Add(sg);

            comboBox1.ValueMember = "Id";
            comboBox1.DisplayMember = "Name";


        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Close();
        }

       

        private void button2_Click(object sender, EventArgs e)
        {

            if (listBox1.Items.Count == 0 || textBox4.Text == "")
                label6.Text = "Enter all fields from *";
            else
                label6.Text = "";

            // Enter all fields from '*'
            /* if (button2.Text == "Ok")
            {
                b.Name = textBox4.Text;
                b.YearOfPublication = (int)numericUpDown1.Value;
                b.Description = richTextBox1.Text;

                db.Books.Add(b);
                db.SaveChanges();

                b.Sages.Clear();

                textBox4.Text = "";
                numericUpDown1.Value = 1000;
                richTextBox1.Text = "";
                listBox1.Items.Clear();

                Form1 f1 = new Form1();
                f1.dataGridView1.Refresh();

                db.Books.Load();
                f1.dataGridView1.DataSource = db.Books.Local.ToBindingList();

                MessageBox.Show("New book added");

            }
            else if (button2.Text == "Update")
            {

            }*/
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int id = 0;

            Sage sg = (Sage)comboBox1.SelectedItem;
                id = sg.Id;
                Sage s2 = db.Sages.Find(id);
                s1.Add(s2);
            
            
            listBox1.Items.Add(comboBox1.SelectedItem);

            listBox1.ValueMember = "Id";
            listBox1.DisplayMember = "Name";


            // listBox1.Items.Add(comboBox1.SelectedItem);

          //  b.Sages.Add(ss);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            f4.button2.Text = "Ok";

            // f2.ShowDialog(this);
            do
            { 
               DialogResult result = f4.ShowDialog(this);

               if (result == DialogResult.Cancel)
                  return;
            }
            while (f4.label6.Text == "Enter all fields from *");


            //if (f4.button2.Text == "Ok")
            //{
                s = new Sage();

                s.Name = f4.textBox1.Text;
                s.Age = (int)f4.numericUpDown1.Value;
                s.City = f4.textBox3.Text;
            if (f4.pictureBox1.Image != null)
            {
                ImageConverter converter = new ImageConverter();
                s.Image = (byte[])converter.ConvertTo(f4.pictureBox1.Image, typeof(byte[]));

            }


            db.Sages.Add(s);
                db.SaveChanges();

                int id = 0;

                Sage sg = s;
                id = sg.Id;
                Sage s2 = db.Sages.Find(id);
                s1.Add(s2);


                listBox1.Items.Add(s);

                listBox1.ValueMember = "Id";
                listBox1.DisplayMember = "Name";


                MessageBox.Show("New sage added");

            //}
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int id = 0;


            if (listBox1.SelectedItem != null)
            {
                Sage sg = (Sage)listBox1.SelectedItem;
                id = sg.Id;
                Sage s2 = db.Sages.Find(id);
                s1.Remove(s2);

                if (button2.Text == "Update")
                    sg22.Add(s2);

                listBox1.Items.Remove(listBox1.SelectedItem);
            }
        }
    }
}
