using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BhoneBook
{
    public partial class Form1 : Form
    {
        string conn_string;
        SqlConnection conn;
        public Form1()
        {
            InitializeComponent();
            conn_string = "Data Source=K09-03L;Initial Catalog=BookPhone;Integrated Security=False;User ID=sa;Password=student;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            conn = new SqlConnection(conn_string);
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
            int k = listBox1.SelectedIndex + 1;
            string query = $"EXEC getContact {k}";
            conn.Open();

            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            List<Contact> contacts = new List<Contact>();
            while(reader.Read())
            {
                Contact c = new Contact()
                {
                    Person = reader["Person"].ToString(),
                    Phone = reader["Phone"].ToString(),
                    Email = reader["Email"].ToString(),
                    Social = reader["Social"].ToString()
                };
                contacts.Add(c);
            }
            dataGridView1.DataSource = contacts;
            conn.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UbdateData();
        }

        private void UbdateData()
        {
            string query = "EXEC getCategories";
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            string name = "";
            listBox1.Items.Clear();
            while (reader.Read())
            {
                name = reader["Name"].ToString();
                listBox1.Items.Add(name);
            }
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string query = $"EXEC addCategory N'{name}'";
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Category add successfuly");
        }

        private void tabPage4_Leave(object sender, EventArgs e)
        {
            UbdateData();
        }
    }
}
