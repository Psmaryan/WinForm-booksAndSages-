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
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        BookContext db;
        Book b;
        Sage s;
        public Form3()
        {
            InitializeComponent();

            db = new BookContext();
            db.Sages.Load();
            dataGridView1.DataSource = db.Sages.Local.ToBindingList();

            dataGridView1.AllowUserToAddRows = false;

            pictureBox1.Size = new Size(150, 160);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.BorderStyle = BorderStyle.Fixed3D;

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'bookAndSageDataSet.Sages' table. You can move, or remove it, as needed.
            this.sagesTableAdapter.Fill(this.bookAndSageDataSet.Sages);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count < 1)
                return;

            int index = dataGridView1.SelectedRows[0].Index;
            int id = 0;
            bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
            if (converted == false)
                return;

            Sage sage = db.Sages.Find(id);

            List<Book> bb = sage.Books.ToList();

            foreach(Book b1 in bb)
            if (b1.Sages.Count == 1)
            {
                int id1 = 0;

                id1 = b1.Id;
                Book book = db.Books.Find(id1);
                db.Books.Remove(book);
                db.Entry(book).State = EntityState.Deleted;
                db.SaveChanges();
                MessageBox.Show("The book " + b1.Name + "is removed");
            }

            db.Sages.Remove(sage);
            db.Entry(sage).State = EntityState.Deleted;
            db.SaveChanges();

            MessageBox.Show("Sage was removed");
        }

        private void button1_Click(object sender, EventArgs e)
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

            if (f4.button2.Text == "Ok")
            {
                s = new Sage();

                s.Name = f4.textBox1.Text;
                s.Age = (int)f4.numericUpDown1.Value;
                s.City = f4.textBox3.Text;

                if (f4.pictureBox1.Image != null)
                {
                    ImageConverter converter = new ImageConverter();
                    s.Image = (byte[])converter.ConvertTo(f4.pictureBox1.Image, typeof(byte[]));

                    /* MemoryStream ms = new MemoryStream();
                     Image img = Image.FromFile(f4.pictureBox1.Image.ToString());
                     img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

                     s.Image = ms.ToArray();*/

                   // s.Image = File.ReadAllBytes(f4.pictureBox1.Image.ToString());
                }

                db.Sages.Add(s);
                db.SaveChanges();

                MessageBox.Show("New sage was added");

            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.SelectedRows[0].Index;
            int id = 0;
            bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
            if (converted == false)
                return;

            Sage sage = db.Sages.Find(id);

            Form4 f4 = new Form4();
            f4.button2.Text = "Update";

            f4.textBox1.Text = sage.Name;
            f4.numericUpDown1.Value = sage.Age;
            f4.textBox3.Text = sage.City;

            if (sage.Image != null)
            {

                MemoryStream mStream = new MemoryStream(sage.Image);
                f4.pictureBox1.Image = Image.FromStream(mStream);

            }

            do
            { 
                DialogResult result = f4.ShowDialog(this);

                 if (result == DialogResult.Cancel)
                     return;
            }
        while (f4.label6.Text == "Enter all fields from *");


            sage.Name = f4.textBox1.Text;
            sage.Age = (int)f4.numericUpDown1.Value;
            sage.City = f4.textBox3.Text;

            if (f4.pictureBox1.Image != null)
            {
                ImageConverter converter = new ImageConverter();
                sage.Image = (byte[])converter.ConvertTo(f4.pictureBox1.Image, typeof(byte[]));
            }
            else
            {
                sage.Image = null;
            }

            db.SaveChanges();
            dataGridView1.Refresh();
            MessageBox.Show("Sage was updated");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.SelectedRows[0].Index;
            int id = 0;
            bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
            if (converted == false)
                return;


            Sage sage = db.Sages.Find(id);

            //db.Books.Load();

            List<Book> book = sage.Books.ToList();

            listBox1.Items.Clear();

            foreach (Book bk in book)
            {
                listBox1.Items.Add(bk.Name);
                //  listBox1.DisplayMember = "Name";
            }

            if (book.Count == 0)
            {
        
                listBox1.Items.Add("No books");
            }

            pictureBox1.Image = null;


            if (sage.Image != null)
            {
                label2.Text = "";

                MemoryStream mStream = new MemoryStream(sage.Image);
                pictureBox1.Image = Image.FromStream(mStream);

            }
            else
            {

                label2.Text = "";
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
