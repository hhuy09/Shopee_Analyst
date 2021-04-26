using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2020_CSDLNC_N10
{
    public partial class Form2 : Form
    {

        public Form2()
        {
            InitializeComponent();
        }

        public string id_f2;

        private void Form2_Load(object sender, EventArgs e)
        {
            label1.Text = id_f2;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //this.Hide();
            Form6 form6 = new Form6();
            form6.category = "Thời Trang Nam";
            form6.id_f6 = label1.Text;
            form6.ShowDialog();
            //this.Close();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            //this.Hide();
            Form4 form4 = new Form4();
            form4.id_f4 = id_f2;
            form4.ShowDialog();
            //this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.ShowDialog();
            //this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //this.Hide();
            Form5 form5 = new Form5();
            form5.temp = textBox1.Text;
            form5.id_f5 = id_f2;
            form5.ShowDialog();
            //this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form6 form6 = new Form6();
            form6.category = "Điện Thoại & Phụ Kiện";
            form6.id_f6 = label1.Text;
            form6.ShowDialog();
            //this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //this.Hide();
            Form6 form6 = new Form6();
            form6.category = "Thiết Bị Điện Tử";
            form6.id_f6 = label1.Text;
            form6.ShowDialog();
            //this.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //this.Hide();
            Form6 form6 = new Form6();
            form6.category = "Máy tính & Laptop";
            form6.id_f6 = label1.Text;
            form6.ShowDialog();
            //this.Close();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //this.Hide();
            Form6 form6 = new Form6();
            form6.category = "Máy ảnh - Máy quay phim";
            form6.id_f6 = label1.Text;
            form6.ShowDialog();
            //this.Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //this.Hide();
            Form6 form6 = new Form6();
            form6.category = "Đồng Hồ";
            form6.id_f6 = label1.Text;
            form6.ShowDialog();
            //this.Close();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            //this.Hide();
            Form6 form6 = new Form6();
            form6.category = "Giày Dép Nam";
            form6.id_f6 = label1.Text;
            form6.ShowDialog();
            //this.Close();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            //this.Hide();
            Form6 form6 = new Form6();
            form6.category = "Chăm sóc thú cưng";
            form6.id_f6 = label1.Text;
            form6.ShowDialog();
            //this.Close();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            //this.Hide();
            Form6 form6 = new Form6();
            form6.category = "Giặt giũ & Chăm sóc nhà cửa";
            form6.id_f6 = label1.Text;
            form6.ShowDialog();
            //this.Close();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            //this.Hide();
            Form6 form6 = new Form6();
            form6.category = "Thời Trang Trẻ Em";
            form6.id_f6 = label1.Text;
            form6.ShowDialog();
            //this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //this.Hide();
            Form6 form6 = new Form6();
            form6.category = "Thời Trang Nữ";
            form6.id_f6 = label1.Text;
            form6.ShowDialog();
            //this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //this.Hide();
            Form6 form6 = new Form6();
            form6.category = "Mẹ & Bé";
            form6.id_f6 = label1.Text;
            form6.ShowDialog();
            //this.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form6 form6 = new Form6();
            form6.category = "Nhà Cửa & Đời Sống";
            form6.id_f6 = label1.Text;
            form6.ShowDialog();
            //this.Close();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            //this.Hide();
            Form6 form6 = new Form6();
            form6.category = "Sức Khỏe & Sắc Đẹp";
            form6.id_f6 = label1.Text;
            form6.ShowDialog();
            //this.Close();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            //this.Hide();
            Form6 form6 = new Form6();
            form6.category = "Giày Dép Nữ";
            form6.id_f6 = label1.Text;
            form6.ShowDialog();
            //this.Close();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            //this.Hide();
            Form6 form6 = new Form6();
            form6.category = "Túi Ví";
            form6.id_f6 = label1.Text;
            form6.ShowDialog();
            //this.Close();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            //this.Hide();
            Form6 form6 = new Form6();
            form6.category = "Phụ Kiện Thời Trang";
            form6.id_f6 = label1.Text;
            form6.ShowDialog();
            //this.Close();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            //this.Hide();
            Form6 form6 = new Form6();
            form6.category = "Đồ Chơi";
            form6.id_f6 = label1.Text;
            form6.ShowDialog();
            //this.Close();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            //this.Hide();
            Form6 form6 = new Form6();
            form6.category = "Thiết Bị Điện Gia Dụng";
            form6.id_f6 = label1.Text;
            form6.ShowDialog();
            //this.Close();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            //this.Hide();
            Form6 form6 = new Form6();
            form6.category = "Sản Phẩm Khác";
            form6.id_f6 = label1.Text;
            form6.ShowDialog();
            //this.Close();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.ShowDialog();
            //this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form8 form8 = new Form8();
            form8.id_f8 = id_f2;
            form8.ShowDialog();
        }
    }
}
