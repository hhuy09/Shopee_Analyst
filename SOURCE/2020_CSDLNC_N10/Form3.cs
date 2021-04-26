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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        public string id_f3;
        public string id_prd;
        public string id_shop;
        public string sname;
        SqlConnection con;

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            label10.Text = id_f3;
            label11.Text = id_prd;
            string conString = ConfigurationManager.ConnectionStrings["SHOPEE"].ConnectionString.ToString();
            con = new SqlConnection(conString);
            con.Open();

            string sqlselect = "EXEC USP_PRODUCT_DETAIL '" + id_prd + "'";
            SqlCommand cmd = new SqlCommand(sqlselect, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read() == true)
            {
                string name = dr["PRODUCT_NAME"].ToString();
                label1.Text = name;
                string star = dr["AVERAGE_RATING"].ToString();
                label2.Text = star;
                string pre = dr["PREODER"].ToString();
                label3.Text = pre;
            }

            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView5.DataSource = dt;

            SqlCommand cmd4 = new SqlCommand(sqlselect, con);
            SqlDataReader dr4 = cmd4.ExecuteReader();
            DataTable dt4 = new DataTable();
            dt4.Load(dr4);
            dataGridView4.DataSource = dt4;

            string sqlPRICE = "SELECT PRICE FROM DBO.PRODUCT, DBO.CLASSIFICATION WHERE PRODUCT_ID = '" + id_prd + "' AND PRODUCT = PRODUCT_ID";
            SqlCommand cmd1 = new SqlCommand(sqlPRICE, con);
            SqlDataReader dr1 = cmd1.ExecuteReader();
            DataTable dt1 = new DataTable();
            dt1.Load(dr1);
            dataGridView6.DataSource = dt1;

            string sqlGROUP = "SELECT GROUP_ID, GROUP_NAME, PRICE, INVENTORY_NUMBER FROM DBO.PRODUCT, DBO.CLASSIFICATION WHERE PRODUCT_ID = '" + id_prd + "' AND PRODUCT = PRODUCT_ID";
            SqlCommand cmd2 = new SqlCommand(sqlGROUP, con);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            DataTable dt2 = new DataTable();
            dt2.Load(dr2);
            dataGridView1.DataSource = dt2;

            string sqlRARE = "EXEC USP_RATING_REPLY '" + id_prd + "'";
            SqlCommand cmd3 = new SqlCommand(sqlRARE, con);
            SqlDataReader dr3 = cmd3.ExecuteReader();
            DataTable dt3 = new DataTable();
            dt3.Load(dr3);
            dataGridView2.DataSource = dt3;

            string sqlADDRESS = "SELECT ADD_INFO_ID, ADDRESS A_FULLNAME, A_PHONENUMBER FROM DBO.ADDRESS_INFO WHERE A_ACCOUNT = '" + id_f3 + "'";
            SqlCommand cmd5 = new SqlCommand(sqlADDRESS, con);
            SqlDataReader dr5 = cmd5.ExecuteReader();
            DataTable dt5 = new DataTable();
            dt5.Load(dr5);
            dataGridView3.DataSource = dt5;

            string sqlshopname = "SELECT * FROM DBO.PRODUCT, DBO.SHOP WHERE SHOP = SHOP_ID AND PRODUCT_ID = '" + id_prd + "'";
            SqlCommand cmd6 = new SqlCommand(sqlshopname, con);
            SqlDataReader dr6 = cmd6.ExecuteReader();
            if (dr6.Read() == true)
            {
                sname = dr6["SHOP_NAME"].ToString();
                label8.Text = sname;

                id_shop = dr6["SHOP_ID"].ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            Form7 form7 = new Form7();
            form7.id_f7 = id_f3;
            form7.id_gr = textBox2.Text;
            form7.re_address = textBox1.Text;
            form7.num = comboBox1.Text;
            form7.id_shop = id_shop;
            form7.sname = sname;
            form7.id_prd = id_prd;
            form7.name = label1.Text;

            string sqlselect = "SELECT * FROM DBO.CLASSIFICATION WHERE GROUP_ID = '" + textBox2.Text + "'";
            SqlCommand cmd = new SqlCommand(sqlselect, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read() == true)
            {
                string price  = dr["PRICE"].ToString();
                form7.price = price;
            }

            form7.ShowDialog();


        }

    }
}
