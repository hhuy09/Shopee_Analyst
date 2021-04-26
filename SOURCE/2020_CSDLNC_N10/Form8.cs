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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }

        SqlConnection con;
        public string id_f8;


        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            string conString = ConfigurationManager.ConnectionStrings["SHOPEE"].ConnectionString.ToString();
            con = new SqlConnection(conString);
            con.Open();

            label3.Text = id_f8;

            string sqlCode = "SELECT ORDER_ID, SHIPPING_CODE, SENDING_INFO, RECEIVING_INFO, ORDER_DATE, TOTAL_MONEY, DELIVERY_DATE FROM DBO.ADDRESS_INFO, dbo.ORDER_SHIPPING_INFO WHERE ADD_INFO_ID = RECEIVING_INFO AND A_ACCOUNT = '" + id_f8 + "'";
            SqlCommand cmd3 = new SqlCommand(sqlCode, con);
            SqlDataReader dr3 = cmd3.ExecuteReader();
            DataTable dt3 = new DataTable();
            dt3.Load(dr3);
            dataGridView1.DataSource = dt3;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string sqlCode = "SELECT STATUS_NAME, S_MODIFIED_DATE FROM DBO.STATUS_INFO, DBO.STATUS WHERE STATUS_ID = STATUS AND S_ORDER = '" + textBox1.Text + "'";
            SqlCommand cmd3 = new SqlCommand(sqlCode, con);
            SqlDataReader dr3 = cmd3.ExecuteReader();
            DataTable dt3 = new DataTable();
            dt3.Load(dr3);
            dataGridView2.DataSource = dt3;
        }
    }
}
