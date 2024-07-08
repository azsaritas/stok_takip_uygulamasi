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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    //KULLANICI ARAMA FORMU
    public partial class Form11 : Form
    {
        sqlbaglantisi baglanti = new sqlbaglantisi();

        //SqlConnection baglanti = new SqlConnection(@"Data Source=AZIZPC\SQLEXPRESS;Initial Catalog=stok_takip;Integrated Security=True");

        public Form11()
        {
            InitializeComponent();
        }
        public DataTable veriYeni; //diğer formlarda kullanmak için oluşturulan datatable 
        private void button1_Click(object sender, EventArgs e)
        {
            //tüm değerlere göre ürün sorgulaması yapar
            SqlCommand sqlAramaKomutu = new SqlCommand("SELECT * FROM Urunler WHERE UrunID=@uid" +
                " and UrunTipi=@utip and Marka=@umarka and Model=@umodel and Fiyat=@ufiyat and Satici=@usatici", baglanti.SQLBaglanti());
            if ((textBox13.Text.All(char.IsDigit)) && (textBox9.Text.All(char.IsDigit)))
            {
                //baglanti.Open();

                sqlAramaKomutu.Parameters.AddWithValue("@uid", textBox9.Text);
                sqlAramaKomutu.Parameters.AddWithValue("@utip", textBox10.Text);
                sqlAramaKomutu.Parameters.AddWithValue("@umarka", textBox11.Text);
                sqlAramaKomutu.Parameters.AddWithValue("@umodel", textBox12.Text);
                sqlAramaKomutu.Parameters.AddWithValue("@ufiyat", textBox13.Text);
                sqlAramaKomutu.Parameters.AddWithValue("@usatici", textBox13.Text);

                SqlDataAdapter veriOkuyucu = new SqlDataAdapter(sqlAramaKomutu);
                DataTable veriTablosu = new DataTable();
                veriOkuyucu.Fill(veriTablosu);
                veriYeni = veriTablosu;

                sqlAramaKomutu.ExecuteNonQuery();
                //baglanti.Close();
                this.Close();
            }
            else
            {
                MessageBox.Show("Geçerli Değerler Girin!");
            }
        }

        private void Form10_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //ürün id e göre sorgulama yapar
            SqlCommand sqlAramaKomutu = new SqlCommand("SELECT * FROM Urunler WHERE UrunID=@uid", baglanti.SQLBaglanti());
            if (!(string.IsNullOrEmpty(textBox9.Text))&&(textBox9.Text.All(char.IsDigit)))
            {
                //baglanti.Open();

                sqlAramaKomutu.Parameters.AddWithValue("@uid", textBox9.Text);
                SqlDataAdapter veriOkuyucu = new SqlDataAdapter(sqlAramaKomutu);
                DataTable veriTablosu = new DataTable();
                veriOkuyucu.Fill(veriTablosu);
                veriYeni = veriTablosu;

                sqlAramaKomutu.ExecuteNonQuery();
                //baglanti.Close();
                this.Close();
            }
            else
            {
                MessageBox.Show("Geçerli Bir Ürün ID Girin!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            //ürün tipine göre sorgulama yapar
            SqlCommand sqlAramaKomutu = new SqlCommand("SELECT * FROM Urunler WHERE UrunTipi=@utip", baglanti.SQLBaglanti());

            if ((!(string.IsNullOrEmpty(textBox10.Text))))
            {
                //baglanti.Open();

                sqlAramaKomutu.Parameters.AddWithValue("@utip", textBox10.Text);
                
                SqlDataAdapter veriOkuyucu = new SqlDataAdapter(sqlAramaKomutu);
                DataTable veriTablosu = new DataTable();
                veriOkuyucu.Fill(veriTablosu);
                veriYeni = veriTablosu;

                sqlAramaKomutu.ExecuteNonQuery();
                //baglanti.Close();
                this.Close();
            }
            else {
                MessageBox.Show("Ürün Tipi Girin!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            //markaya göre sorgulama yapar
            SqlCommand sqlAramaKomutu = new SqlCommand("SELECT * FROM Urunler WHERE Marka=@umarka", baglanti.SQLBaglanti());
            if (!(string.IsNullOrEmpty(textBox11.Text))) 
            {
                //baglanti.Open();

                sqlAramaKomutu.Parameters.AddWithValue("@umarka", textBox11.Text);

                SqlDataAdapter veriOkuyucu = new SqlDataAdapter(sqlAramaKomutu);
                DataTable veriTablosu = new DataTable();
                veriOkuyucu.Fill(veriTablosu);
                veriYeni = veriTablosu;

                sqlAramaKomutu.ExecuteNonQuery();
                //baglanti.Close();
                this.Close();
            }
            else
            {
                MessageBox.Show("Marka Girin!");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            //modele göre sorgulama yapar
            SqlCommand sqlAramaKomutu = new SqlCommand("SELECT * FROM Urunler WHERE Model=@umodel", baglanti.SQLBaglanti());
            if (!(string.IsNullOrEmpty(textBox12.Text)))
            {
            //baglanti.Open();

            sqlAramaKomutu.Parameters.AddWithValue("@umodel", textBox12.Text);

            SqlDataAdapter veriOkuyucu = new SqlDataAdapter(sqlAramaKomutu);
            DataTable veriTablosu = new DataTable();
            veriOkuyucu.Fill(veriTablosu);
            veriYeni = veriTablosu;

            sqlAramaKomutu.ExecuteNonQuery();
            //baglanti.Close();
            this.Close();
            }
            else
            {
                MessageBox.Show("Ürün Modeli Girin!");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

            //fiyata göre sorgualam yapar
            SqlCommand sqlAramaKomutu = new SqlCommand("SELECT * FROM Urunler WHERE Fiyat=@ufiyat", baglanti.SQLBaglanti());
            if (!(string.IsNullOrEmpty(textBox13.Text))&&(textBox13.Text.All(char.IsDigit)))
            {
                //baglanti.Open();

                sqlAramaKomutu.Parameters.AddWithValue("@ufiyat", Convert.ToInt32(textBox13.Text));

                SqlDataAdapter veriOkuyucu = new SqlDataAdapter(sqlAramaKomutu);
                DataTable veriTablosu = new DataTable();
                veriOkuyucu.Fill(veriTablosu);
                veriYeni = veriTablosu;

                sqlAramaKomutu.ExecuteNonQuery();
                //baglanti.Close();
                this.Close();
            }
            else
            {
                MessageBox.Show("Geçerli Bir Ürün Fiyatı Girin!");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            
            //satıcıya göre sorgulama yapar
            SqlCommand sqlAramaKomutu = new SqlCommand("SELECT * FROM Urunler WHERE Satici=@usatici", baglanti.SQLBaglanti());
            if (!(string.IsNullOrEmpty(textBox14.Text)))
            {
                //baglanti.Open();

                sqlAramaKomutu.Parameters.AddWithValue("@usatici", textBox14.Text);

                SqlDataAdapter veriOkuyucu = new SqlDataAdapter(sqlAramaKomutu);
                DataTable veriTablosu = new DataTable();
                veriOkuyucu.Fill(veriTablosu);
                veriYeni = veriTablosu;

                sqlAramaKomutu.ExecuteNonQuery();
                //baglanti.Close();
                this.Close();
            }
            else
            {
                MessageBox.Show("Satıcı Girin!");
            }
        }

        private void Form11_FormClosed(object sender, FormClosedEventArgs e)
        {
            baglanti.SQLBaglanti().Close();
        }
    }
}
