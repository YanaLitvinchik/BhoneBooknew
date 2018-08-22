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
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string query = "EXEC getCategories";
            conn.Open();
            SqlCommand cmd = new SqlCommand(query,conn);
            SqlDataReader reader = cmd.ExecuteReader();
            string name = "";
            while (reader.Read())
            {
                name = reader["Name"].ToString();
                listBox1.Items.Add(name);
            }
            conn.Close();
        }
    }
}
