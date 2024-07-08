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
    //Kullanıcı Arama Formu
    public partial class Form10 : Form
    {

        sqlbaglantisi baglanti = new sqlbaglantisi();

        //SqlConnection baglanti = new SqlConnection(@"Data Source=AZIZPC\SQLEXPRESS;Initial Catalog=stok_takip;Integrated Security=True");

        public Form10()
        {
            InitializeComponent();
        }
        public DataTable veriYeni; //diğer formlara aktarmak için oluşturulan datatable

       

        private void button1_Click(object sender, EventArgs e)
        {

            //verilen parametrelere göre kullanıcılar tablosundan kullanıcı sorgusu yapılır
            //tüm değerler göre sorgular
            SqlCommand sqlAramaKomutu = new SqlCommand("SELECT * FROM Kullanicilar WHERE KullaniciID=@kid" +
                " and Ad=@kad and Soyad=@ksoyad and Eposta=@keposta and Telefon=@ktelefon", baglanti.SQLBaglanti());
            //baglanti.Open();

            sqlAramaKomutu.Parameters.AddWithValue("@kid", textBox9.Text);
            sqlAramaKomutu.Parameters.AddWithValue("@kad", textBox10.Text);
            sqlAramaKomutu.Parameters.AddWithValue("@ksoyad", textBox11.Text);
            sqlAramaKomutu.Parameters.AddWithValue("@keposta", textBox12.Text);
            sqlAramaKomutu.Parameters.AddWithValue("@ktelefon", textBox13.Text);


            SqlDataAdapter veriOkuyucu = new SqlDataAdapter(sqlAramaKomutu);
            DataTable veriTablosu = new DataTable();
            veriOkuyucu.Fill(veriTablosu);
            veriYeni = veriTablosu;

            sqlAramaKomutu.ExecuteNonQuery();
            //baglanti.Close();
            this.Close();
        }

        private void Form10_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //sadece kullanıcı id e göre sorgulama yapar
            SqlCommand sqlAramaKomutu = new SqlCommand("SELECT * FROM Kullanicilar WHERE KullaniciID=@kid", baglanti.SQLBaglanti());
            if (!(string.IsNullOrEmpty(textBox9.Text)) && (textBox9.Text.All(char.IsDigit)))
            {
               // baglanti.Open();

                sqlAramaKomutu.Parameters.AddWithValue("@kid", textBox9.Text);
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
                MessageBox.Show("Geçerli Bir Kullanıcı ID Girin!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            //ad ve soyada göre sorgulama yapar
            SqlCommand sqlAramaKomutu = new SqlCommand("SELECT * FROM Kullanicilar WHERE Ad=@kad and Soyad=@ksoyad", baglanti.SQLBaglanti());
            
            if ((!(string.IsNullOrEmpty(textBox10.Text))) && (!(string.IsNullOrEmpty(textBox11.Text))))
            {
                //baglanti.Open();

                sqlAramaKomutu.Parameters.AddWithValue("@kad", textBox10.Text);
                sqlAramaKomutu.Parameters.AddWithValue("@ksoyad", textBox11.Text);
                SqlDataAdapter veriOkuyucu = new SqlDataAdapter(sqlAramaKomutu);
                DataTable veriTablosu = new DataTable();
                veriOkuyucu.Fill(veriTablosu);
                veriYeni = veriTablosu;

                sqlAramaKomutu.ExecuteNonQuery();
                //baglanti.Close();
                this.Close();
            }
            else {
                MessageBox.Show("Ad ve Soyad Girin!");

            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //epostaya göre sorgulama yapar
            SqlCommand sqlAramaKomutu = new SqlCommand("SELECT * FROM Kullanicilar WHERE Eposta=@keposta", baglanti.SQLBaglanti());
            if (!(string.IsNullOrEmpty(textBox12.Text))) 
            {
                //baglanti.Open();

                sqlAramaKomutu.Parameters.AddWithValue("@keposta", textBox12.Text);

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
                MessageBox.Show("E-posta Adresi Girin!");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //telefon numarasına göre sorgulama yapar
            SqlCommand sqlAramaKomutu = new SqlCommand("SELECT * FROM Kullanicilar WHERE Telefon=@ktelefon", baglanti.SQLBaglanti());
            if (!(string.IsNullOrEmpty(textBox13.Text)))
            {
            //baglanti.Open();

            sqlAramaKomutu.Parameters.AddWithValue("@ktelefon", textBox13.Text);

            SqlDataAdapter veriOkuyucu = new SqlDataAdapter(sqlAramaKomutu);
            DataTable veriTablosu = new DataTable();
            veriOkuyucu.Fill(veriTablosu);
            veriYeni = veriTablosu;

            sqlAramaKomutu.ExecuteNonQuery();
           // baglanti.Close();
            this.Close();
            }
            else
            {
                MessageBox.Show("Telefon Numarası Girin!");
            }
        }

        private void Form10_FormClosed(object sender, FormClosedEventArgs e)
        {
            baglanti.SQLBaglanti().Close();
        }
    }
}
