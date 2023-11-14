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

namespace demo_ex
{
    public partial class sign_up : Form
    {

        db_connect_class db = new db_connect_class();

        public sign_up()
        {
            InitializeComponent();
            this.Text = "Регистрация";
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void login_up_Load(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
            textBox1.MaxLength = 50;
            textBox2.MaxLength = 50;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var login_user = textBox1.Text;
            var pass_user = textBox2.Text;

            string queryString = $"insert into register(login_user, pass_user) values('{login_user}', '{pass_user}')";


            SqlCommand command = new SqlCommand(queryString, db.getConnection());

            db.openConnection();

            if(command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Аккаунт создан");
                sign_in sign_In = new sign_in();
                this.Hide();
                sign_In.Show();
            }
            else
            {
                MessageBox.Show("Аккаунт не создан");
            }
            db.closeConnection();
        }

        private Boolean check_User()
        {
            var login_user = textBox1.Text;
            var pass_user = textBox2.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string query_string = $"select id_user, login_user, pass_user from register where login_user = '{login_user}' and pass_user = '{pass_user}'";

            SqlCommand command = new SqlCommand(query_string, db.getConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if(table.Rows.Count > 0)
            {
                MessageBox.Show("Пользователь уже существует");
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
