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
using System.Text.RegularExpressions;

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
           /* textBox2.PasswordChar = '*';*/
            textBox1.MaxLength = 50;
            textBox2.MaxLength = 50;

            button1.Text = "Регистрация";
            linkLabel1.Text = "Авторизация";

            label1.Text = "Логин";
            label2.Text = "Пароль";

            label3.Text = "Неправильный логин"; label3.ForeColor = Color.Red; label3.Visible = false;
            label4.Text = "Неправильный пароль"; label4.ForeColor = Color.Red; label4.Visible = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var login_user = textBox1.Text;
            var pass_user = textBox2.Text;

            bool valid_login = false;
            bool valid_pass = false;

            Regex regex = new Regex(@"[A-Z]");

            if (login_user.Contains("@") && (login_user.Contains("gmail.com") || login_user.Contains("mail.ru") || login_user.Contains("yandex.ru")))
            {
                label3.Visible = false;
                label1.Visible = true;

                valid_login = true;
            }
            else
            {
                label1.Visible = false;
                label3.Visible = true;
            }
            if(pass_user.Length > 5 && pass_user.Length < 20 && regex.IsMatch(pass_user))
            {
                label4.Visible = false;
                label2.Visible = true;

                valid_pass = true;
            }
            else
            {
                label2.Visible = false;
                label4.Visible = true;
            }

            if(valid_login && valid_pass)
            {
                string queryString = $"insert into register(login_user, pass_user) values('{login_user}', '{pass_user}')";

                SqlCommand command = new SqlCommand(queryString, db.getConnection());

                db.openConnection();

                if (command.ExecuteNonQuery() == 1)
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            sign_in sgi = new sign_in();
            sgi.Show();
        }
    }
}
