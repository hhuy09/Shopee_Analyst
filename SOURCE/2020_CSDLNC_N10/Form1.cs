using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace _2020_CSDLNC_N10
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection con;

        private void button1_Click(object sender, EventArgs e)
        {

            string user_name = textBox1.Text;
            string pass_word = textBox2.Text;
            //string sqlselect = "EXEC USP_LOGIN '" + user_name + "', '" + pass_word + "'";
            string sqlselect = "SELECT * FROM DBO.ACCOUNT WHERE USERNAME = '" + user_name + "'AND PASSWORD = '" + pass_word + "'";
            SqlCommand cmd = new SqlCommand(sqlselect, con);
            SqlDataReader data = cmd.ExecuteReader();
            if (data.Read() == true)
            {
                MessageBox.Show("Đăng nhập thành công");
                string id = data["ACCOUNT_ID"].ToString();
                Form2 form2 = new Form2();
                form2.id_f2 = id;
                form2.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Đăng nhập thất bại!");
                Form1 form1 = new Form1();
                form1.Show();
                this.Hide();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string conString = ConfigurationManager.ConnectionStrings["SHOPEE"].ConnectionString.ToString();
            con = new SqlConnection(conString);
            con.Open();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            con.Close();
            Application.Exit();
        }
    }
}
