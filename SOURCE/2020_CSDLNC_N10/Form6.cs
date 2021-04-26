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
    public partial class Form6 : Form
    {
        public string category;
        public Form6()
        {
            InitializeComponent();
        }

        public string id_f6;
        SqlConnection con;

        private void Form6_Load(object sender, EventArgs e)
        {
            label5.Text = category;
            label3.Text = id_f6;
            string conString = ConfigurationManager.ConnectionStrings["SHOPEE"].ConnectionString.ToString();
            con = new SqlConnection(conString);
            con.Open();

            string sqlselect = "EXEC USP_FIND_PRODUCT_BY_CATEGORY N'" + category + "'";
            SqlCommand cmd = new SqlCommand(sqlselect, con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sqlselect = "SELECT * FROM DBO.PRODUCT WHERE PRODUCT_ID = '" + textBox1.Text + "'";
            SqlCommand cmd = new SqlCommand(sqlselect, con);
            SqlDataReader data = cmd.ExecuteReader();
            if (data.Read() == true)
            {
                Form3 form3 = new Form3();
                form3.id_prd = textBox1.Text;
                form3.id_f3 = id_f6;
                form3.ShowDialog();
            }
            else
            {
                MessageBox.Show("Không tìm thấy sản phẩm");
                this.Close();
            }
        }
    }
}
