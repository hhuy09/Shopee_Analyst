using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2020_CSDLNC_N10
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        SqlConnection con;
        public string re_address;
        public string se_address;
        public string id_gr;
        public string num;
        public string id_f7;
        public string id_shop;
        public string sname;
        public string id_prd;
        public string name;
        public string price;

        private void Form7_Load(object sender, EventArgs e)
        {
            string conString = ConfigurationManager.ConnectionStrings["SHOPEE"].ConnectionString.ToString();
            con = new SqlConnection(conString);
            con.Open();

            label8.Text = sname;
            label10.Text = id_f7;
            label15.Text = id_prd;
            label16.Text = name;
            label17.Text = id_gr;
            label18.Text = num;
            label19.Text = price;

            float spri = float.Parse(price);
            int snum = Int32.Parse(num);
            float ttmoney = spri * snum;
            float pay = ttmoney + 20000;

            label1.Text = ttmoney.ToString();
            label3.Text = pay.ToString();


            string sqlCode = "SELECT * FROM DBO.PROCODE WHERE P_CUSTOMER = '" + id_f7 + "'";
            SqlCommand cmd3 = new SqlCommand(sqlCode, con);
            SqlDataReader dr3 = cmd3.ExecuteReader();
            DataTable dt3 = new DataTable();
            dt3.Load(dr3);
            dataGridView1.DataSource = dt3;

            string sqlreadd = "SELECT * FROM DBO.ADDRESS_INFO WHERE ADD_INFO_ID = '" + re_address + "'";
            SqlCommand cmd1 = new SqlCommand(sqlreadd, con);
            SqlDataReader dr1 = cmd1.ExecuteReader();
            if (dr1.Read() == true)
            {
                string re = dr1["ADDRESS"].ToString();
                label7.Text = re;
            }
            dr1.Dispose();

            string sqlidse = "SELECT TOP(1) * FROM DBO.ADDRESS_INFO WHERE A_ACCOUNT = '" + id_shop + "'";
            SqlCommand cmd = new SqlCommand(sqlidse, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read() == true)
            {
                se_address = dr["ADD_INFO_ID"].ToString();
            }
            dr.Dispose();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int ttmoney = Int32.Parse(label1.Text);
            int pay = Int32.Parse(label3.Text);
            string code = textBox1.Text;
            if(code == "")
            {
                string sqlupdate = "EXEC INSERT_ORDER '" + se_address + "', '" + re_address + "', NULL, 20000, 744, " + ttmoney + ", N'" + comboBox2.Text + "', " + pay + ", N'" + comboBox1.Text + "', '" + id_gr + "', " + num;
                SqlCommand cmd = new SqlCommand(sqlupdate, con);
                cmd.ExecuteNonQuery();
            }
            else
            {
                string sqlupdate = "EXEC INSERT_ORDER '" + se_address + "', '" + re_address + "', " + code + ", 20000, 744, " + ttmoney + ", N'" + comboBox2.Text + "', " + pay + ", N'" + comboBox1.Text + "', '" + id_gr + "', " + num;
                SqlCommand cmd = new SqlCommand(sqlupdate, con);
                cmd.ExecuteNonQuery();
            }
            

            MessageBox.Show("Đặt hàng thành công");
            this.Close();
        }
    }
}
