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
    public partial class Form1 : Form
    {
        BookContext db;
        Book b;
        Sage s;

        public Form1()
        {
            InitializeComponent();
             db = new BookContext();

            //db.Books.Load();
            dataGridView1.DataSource = db.Books.Local.ToBindingList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.button2.Text = "Ok";

            do
            {

                DialogResult result = f2.ShowDialog(this);

                if (result == DialogResult.Cancel)
                    return;
            }
            while (f2.label6.Text == "Enter all fields from *");




                b = new Book();
                b.Name = f2.textBox4.Text;
                b.YearOfPublication = (int)f2.numericUpDown1.Value;
                b.Description = f2.richTextBox1.Text;

          //  List<Sage> sage = new List<Sage>(f2.s1);


                foreach (Sage sga in f2.s1)
                {
                     int id;
                     id = sga.Id;
                     Sage s2 = db.Sages.Find(id);
                     b.Sages.Add(s2);
                }

                db.Books.Add(b);
                db.SaveChanges();

               // b.Sages.Clear();

           // f2.s1.Clear();
                MessageBox.Show("New book was added");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            DialogResult result = f3.ShowDialog(this);

            if (result == DialogResult.Cancel)
            {
                dataGridView1.Refresh();
                return;
            }
            dataGridView1.Refresh();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'bookAndSageDataSet1.Books' table. You can move, or remove it, as needed.
            this.booksTableAdapter1.Fill(this.bookAndSageDataSet1.Books);
            // TODO: This line of code loads data into the 'bOOKandSAGESDataSet2.Books' table. You can move, or remove it, as needed.
            //this.booksTableAdapter.Fill(this.bOOKandSAGESDataSet2.Books);
            // TODO: This line of code loads data into the 'bOOKandSAGESDataSet1.Books' table. You can move, or remove it, as needed.


        }

        private void button3_Click(object sender, EventArgs e)
        {

            int index = dataGridView1.SelectedRows[0].Index;
            int id = 0;
            bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
            if (converted == false)
                return;

            Book book = db.Books.Find(id);

            Form2 f2 = new Form2();
            f2.button2.Text = "Update";

            List<Sage> sage = book.Sages.ToList<Sage>();

            //f2.s1.Clear();
            //f2.sg22.Clear();

            foreach (Sage sg in sage)
            {
                int id2 = 0;
                
                id2 = sg.Id;
                Sage s2 = db.Sages.Find(id2);
                f2.s1.Add(s2);


                f2.listBox1.Items.Add(sg);
                f2.listBox1.ValueMember = "Id";
                f2.listBox1.DisplayMember = "Name";

              //  book.Sages.Remove(s2);
              //  db.Entry(book).State = EntityState.Deleted;

            }

            f2.textBox4.Text = book.Name;
            f2.numericUpDown1.Value = book.YearOfPublication;
            f2.richTextBox1.Text = book.Description;

            do
            {
                DialogResult result = f2.ShowDialog(this);

                if (result == DialogResult.Cancel)
                    return;
            }
            while (f2.label6.Text == "Enter all fields from *");

            book.Name = f2.textBox4.Text;
            book.YearOfPublication = (int)f2.numericUpDown1.Value;
            book.Description = f2.richTextBox1.Text;

            

            List<Sage> sage1 = f2.s1;
            book.Sages.Clear();

            foreach (Sage sa in sage1)
            {
                int id4 = 0;
                id4 = sa.Id;
                Sage s2 = db.Sages.Find(id4);
                book.Sages.Add(s2);
            }

            List<Sage> sage2 = f2.sg22;
            // book.Sages.Clear();

            foreach (Sage sa in sage2)
            {
                int id4 = 0;
                id4 = sa.Id;
                Sage s2 = db.Sages.Find(id4);
                book.Sages.Remove(s2);
                //db.Entry(book.Sages).State = EntityState.Deleted;
            }

            //book.Sages.Clear();
            db.Entry(book).State = EntityState.Modified;

            db.SaveChanges();
            dataGridView1.Refresh();

            f2.sg22.Clear();
            f2.s1.Clear();
            MessageBox.Show("Book was updated");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count < 1)
                return;

            int index = dataGridView1.SelectedRows[0].Index;
            int id = 0;
            bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
            if (converted == false)
                return;

            Book book = db.Books.Find(id);
            

            db.Books.Remove(book);
            
            db.SaveChanges();

            MessageBox.Show("Book was removed");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.SelectedRows[0].Index;
            int id = 0;
            bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
            if (converted == false)
                return;
            

             Book book1 = db.Books.Find(id);

            db.Sages.Load();
            
            List<Sage> sage = book1.Sages. ToList();

            listBox1.Items.Clear();

            foreach (Sage sg in sage)
            {
                listBox1.Items.Add(sg.Name);
              //  listBox1.DisplayMember = "Name";
            }

            dataGridView1.Refresh();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }
    }
}
