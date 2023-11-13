using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace demo_ex
{
    public partial class sign_in : Form
    {

        db_connect_class db = new db_connect_class();

        public sign_in()
        {
            InitializeComponent();
            this.Text = "Авторизация";
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
            textBox1.MaxLength = 50;
            textBox2.MaxLength = 50;

            button1.Text = "Авторизоваться";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var login_user = textBox1.Text;
            var pass_user = textBox2.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string query_string = $"select id_user, login_user, pass_user from register where login_user = '{login_user}' and pass_user = '{pass_user}'";

            SqlCommand command = new SqlCommand(query_string, db.getConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if(table.Rows.Count == 1)
            {
                MessageBox.Show("Вы вошли");
                this.Hide();
                user usr = new user();
                usr.Show();
            }
            else
            {
                MessageBox.Show("Вы не вошли");
            }
        
        
        }
    }
}
