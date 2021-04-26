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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        public string id_f4;
        SqlConnection con;

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            label3.Text = id_f4;
            string conString = ConfigurationManager.ConnectionStrings["SHOPEE"].ConnectionString.ToString();
            con = new SqlConnection(conString);
            con.Open();

            string sqlselect = "SELECT * FROM DBO.PRODUCT WHERE SHOP = '" + id_f4 + "'";
            SqlCommand cmd = new SqlCommand(sqlselect, con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;

            string sqlselect1 = "SELECT ORDER_ID, SHIPPING_CODE, SENDING_INFO, RECEIVING_INFO, ORDER_DATE, TOTAL_MONEY, DELIVERY_DATE FROM DBO.ADDRESS_INFO, dbo.ORDER_SHIPPING_INFO WHERE ADD_INFO_ID = SENDING_INFO AND A_ACCOUNT = '" + id_f4 + "'";
            SqlCommand cmd1 = new SqlCommand(sqlselect1, con);
            SqlDataReader dr1 = cmd1.ExecuteReader();
            DataTable dt1 = new DataTable();
            dt1.Load(dr1);
            dataGridView2.DataSource = dt1;

            string sqlselect2 = "SELECT DISTINCT ORDER_ID, SHIPPING_CODE, SENDING_INFO, RECEIVING_INFO, ORDER_DATE, TOTAL_MONEY, DELIVERY_DATE FROM DBO.ADDRESS_INFO, dbo.ORDER_SHIPPING_INFO, DBO.STATUS_INFO WHERE ADD_INFO_ID = SENDING_INFO AND ORDER_ID = S_ORDER AND A_ACCOUNT = '" + id_f4 + "' AND NOT EXISTS(SELECT * FROM STATUS_INFO WHERE ORDER_ID = S_ORDER AND STATUS = 'S02')";
            SqlCommand cmd2 = new SqlCommand(sqlselect2, con);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            DataTable dt2 = new DataTable();
            dt2.Load(dr2);
            dataGridView3.DataSource = dt2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sqlupdate = "EXEC INSERT_STT @id, @stt";
            SqlCommand cmd = new SqlCommand(sqlupdate, con);
            cmd.Parameters.Add(new SqlParameter("@id", textBox3.Text));
            cmd.Parameters.Add(new SqlParameter("@stt", comboBox1.Text));
            cmd.ExecuteNonQuery();

            string sqlselect2 = "SELECT DISTINCT ORDER_ID, SHIPPING_CODE, SENDING_INFO, RECEIVING_INFO, ORDER_DATE, TOTAL_MONEY, DELIVERY_DATE FROM DBO.ADDRESS_INFO, dbo.ORDER_SHIPPING_INFO, DBO.STATUS_INFO WHERE ADD_INFO_ID = SENDING_INFO AND ORDER_ID = S_ORDER AND A_ACCOUNT = '" + id_f4 + "' AND NOT EXISTS(SELECT * FROM STATUS_INFO WHERE ORDER_ID = S_ORDER AND STATUS = 'S02')";
            SqlCommand cmd2 = new SqlCommand(sqlselect2, con);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            DataTable dt2 = new DataTable();
            dt2.Load(dr2);
            dataGridView3.DataSource = dt2;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int year = Int32.Parse(textBox4.Text);
            string M1 = "EXEC UT_THONGKEDOANHTHU_THANG '" + id_f4 + "', " + year + " , 1";
            SqlCommand cmd = new SqlCommand(M1, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read() == true)
            {
                label33.Text = dr["TONGDOANHTHU"].ToString();
            }
            dr.Dispose();

            string M2 = "EXEC UT_THONGKEDOANHTHU_THANG '" + id_f4 + "', " + year + " , 2";
            SqlCommand cmd2 = new SqlCommand(M2, con);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            if (dr2.Read() == true)
            {
                label34.Text = dr2["TONGDOANHTHU"].ToString();
            }
            dr2.Dispose();

            string M3 = "EXEC UT_THONGKEDOANHTHU_THANG '" + id_f4 + "', " + year + " , 3";
            SqlCommand cmd3 = new SqlCommand(M3, con);
            SqlDataReader dr3 = cmd3.ExecuteReader();
            if (dr3.Read() == true)
            {
                label35.Text = dr3["TONGDOANHTHU"].ToString();
            }
            dr3.Dispose();

            string M4 = "EXEC UT_THONGKEDOANHTHU_THANG '" + id_f4 + "', " + year + " , 4";
            SqlCommand cmd4 = new SqlCommand(M4, con);
            SqlDataReader dr4 = cmd4.ExecuteReader();
            if (dr4.Read() == true)
            {
                label36.Text = dr4["TONGDOANHTHU"].ToString();
            }
            dr4.Dispose();

            string M5 = "EXEC UT_THONGKEDOANHTHU_THANG '" + id_f4 + "', " + year + " , 5";
            SqlCommand cmd5 = new SqlCommand(M5, con);
            SqlDataReader dr5 = cmd5.ExecuteReader();
            if (dr5.Read() == true)
            {
                label37.Text = dr5["TONGDOANHTHU"].ToString();
            }
            dr5.Dispose();

            string M6 = "EXEC UT_THONGKEDOANHTHU_THANG '" + id_f4 + "', " + year + " , 6";
            SqlCommand cmd6 = new SqlCommand(M6, con);
            SqlDataReader dr6 = cmd6.ExecuteReader();
            if (dr6.Read() == true)
            {
                label38.Text = dr6["TONGDOANHTHU"].ToString();
            }
            dr6.Dispose();

            string M7 = "EXEC UT_THONGKEDOANHTHU_THANG '" + id_f4 + "', " + year + " , 7";
            SqlCommand cmd7 = new SqlCommand(M7, con);
            SqlDataReader dr7 = cmd7.ExecuteReader();
            if (dr7.Read() == true)
            {
                label39.Text = dr7["TONGDOANHTHU"].ToString();
            }
            dr7.Dispose();

            string M8 = "EXEC UT_THONGKEDOANHTHU_THANG '" + id_f4 + "', " + year + " , 8";
            SqlCommand cmd8 = new SqlCommand(M8, con);
            SqlDataReader dr8 = cmd8.ExecuteReader();
            if (dr8.Read() == true)
            {
                label40.Text = dr8["TONGDOANHTHU"].ToString();
            }
            dr8.Dispose();

            string M9 = "EXEC UT_THONGKEDOANHTHU_THANG '" + id_f4 + "', " + year + " , 9";
            SqlCommand cmd9 = new SqlCommand(M9, con);
            SqlDataReader dr9 = cmd9.ExecuteReader();
            if (dr9.Read() == true)
            {
                label41.Text = dr9["TONGDOANHTHU"].ToString();
            }
            dr9.Dispose();

            string M10 = "EXEC UT_THONGKEDOANHTHU_THANG '" + id_f4 + "', " + year + " , 10";
            SqlCommand cmd10 = new SqlCommand(M10, con);
            SqlDataReader dr10 = cmd10.ExecuteReader();
            if (dr10.Read() == true)
            {
                label42.Text = dr10["TONGDOANHTHU"].ToString();
            }
            dr10.Dispose();

            string M11 = "EXEC UT_THONGKEDOANHTHU_THANG '" + id_f4 + "', " + year + " , 11";
            SqlCommand cmd11 = new SqlCommand(M11, con);
            SqlDataReader dr11 = cmd11.ExecuteReader();
            if (dr11.Read() == true)
            {
                label43.Text = dr11["TONGDOANHTHU"].ToString();
            }
            dr11.Dispose();

            string M12 = "EXEC UT_THONGKEDOANHTHU_THANG '" + id_f4 + "', " + year + " , 12";
            SqlCommand cmd12 = new SqlCommand(M12, con);
            SqlDataReader dr12 = cmd12.ExecuteReader();
            if (dr12.Read() == true)
            {
                label44.Text = dr12["TONGDOANHTHU"].ToString();
            }
            dr12.Dispose();
        }


    }
}
