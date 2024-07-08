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

namespace WindowsFormsApp1
{
    //SATICI KAYDI FORMU

    public partial class Form7 : Form
    {
        sqlbaglantisi baglanti = new sqlbaglantisi();

        //SqlConnection baglanti2 = new SqlConnection(@"Data Source=AZIZPC\SQLEXPRESS;Initial Catalog=stok_takip;Integrated Security=True");

        public Form7()
        {
            InitializeComponent();
            
        }
        //bu kısımda satıcı kaydı yapılır
        private void button1_Click(object sender, EventArgs e)
        {
            //paramterelere göre satıcılar tablosuna satıcı eklenir
            SqlCommand sqlkomutEkle = new SqlCommand("INSERT INTO Saticilar (SaticiMagazaAdi,SaticiEposta,SaticiTelefon,SaticiSifre)" 
                + "VALUES (@sad,@seposta,@stelefon,@ssifre) ", baglanti.SQLBaglanti());

            if (durum1 && durum2 && durum3 && durum4)//textbox durumları kontrol edilir
            {
                //baglanti2.Open();

                sqlkomutEkle.Parameters.AddWithValue("@sad", textBox1.Text);
                sqlkomutEkle.Parameters.AddWithValue("@seposta", textBox4.Text);
                sqlkomutEkle.Parameters.AddWithValue("@stelefon",textBox5.Text);
                sqlkomutEkle.Parameters.AddWithValue("@ssifre", textBox6.Text);
             
                sqlkomutEkle.ExecuteNonQuery();

                MessageBox.Show("Kayıt Başarılı. Giriş Yapabilirsiniz.");
                this.Close();
                //baglanti2.Close();
                baglanti.SQLBaglanti().Close();
            }
            else
            {
                MessageBox.Show("Kayıt Başarısız ! Lütfen Geçerli Değerler Girin."); //hata mesajı
            }
            
        }

        // BU KISIMDA TEXTBOX DEĞERLERİ SAYI KELİME ÖZEL KARAKTER VB. DEĞERLERE KARŞI KONTROL EDİLİR
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
        bool durum1,durum2,durum3,durum4 = true;
         
        private void textBox1_Leave(object sender, EventArgs e)
        {
            if ((string.IsNullOrEmpty(textBox1.Text)))
            {
                textBox1.Text = "Geçerli bir değer girin";
                durum1 = false;
            }
            else
            {
                durum1 = true;
            }
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (!durum1)
            {
                textBox1.Text = "";
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if ((!(textBox5.Text.All(char.IsDigit))))
            {
                textBox5.Text = "Geçerli bir değer girin";
                durum2 = false;
            }
            else 
            {
                durum2 = true;  
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if ((string.IsNullOrEmpty(textBox4.Text)))
            {
                textBox4.Text = "Geçerli bir değer girin";
                durum3 = false;
            }
            else
            {
                durum3 = true;
            }
        }

        private void textBox4_MouseClick(object sender, MouseEventArgs e)
        {
            if (!durum3)
            {
                textBox4.Text = "";
            }
        }

        private void Form7_FormClosed(object sender, FormClosedEventArgs e)
        {
            baglanti.SQLBaglanti().Close();
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            if ((string.IsNullOrEmpty(textBox6.Text)))
            {
                textBox6.Text = "Geçerli bir değer girin";
                durum4 = false;
            }
            else
            {
                durum4 = true;
            }
        }

        private void textBox6_MouseClick(object sender, MouseEventArgs e)
        {
            if (!durum4)
            {
                textBox6.Text = "";
            }
        }

        private void textBox5_MouseClick(object sender, MouseEventArgs e)
        {
            if (!durum2)
            {
                textBox5.Text = "";
            }
        }
        
      
        
    }
}
