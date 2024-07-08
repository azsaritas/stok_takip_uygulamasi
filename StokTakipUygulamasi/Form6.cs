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
    //KULLANICI KAYDI FORMU

    public partial class Form6 : Form
    {
        sqlbaglantisi baglanti = new sqlbaglantisi();

        //SqlConnection baglanti = new SqlConnection(@"Data Source=AZIZPC\SQLEXPRESS;Initial Catalog=stok_takip;Integrated Security=True");

        public Form6()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //bu kısımda kullanıcı kaydı yapılır

            SqlCommand sqlkomutEkle = new SqlCommand("INSERT INTO Kullanicilar (Ad,Soyad,Sifre," +
                "Yas,Cinsiyet,Eposta,Telefon,Adres)" +
                "VALUES (@kad,@ksoyad,@ksifre,@kyas,@kcinsiyet,@keposta,@ktelefon,@kadres) ", baglanti.SQLBaglanti());

            if (durum1 && durum2 && durum3 && durum4 && durum5 && durum6 && durum7) //textbox durumları kontrol edilir
            {
                //baglanti2.Open();
                //parametrelere göre ad soyad sifre yas eposta ve telefon değerleri kullanıcılar tablosuna eklenir
                sqlkomutEkle.Parameters.AddWithValue("@kad", textBox1.Text.Trim());
                sqlkomutEkle.Parameters.AddWithValue("@ksoyad", textBox2.Text.Trim());
                sqlkomutEkle.Parameters.AddWithValue("@ksifre", textBox6.Text.Trim());
                sqlkomutEkle.Parameters.AddWithValue("@kyas", Convert.ToInt32(textBox3.Text));
                sqlkomutEkle.Parameters.AddWithValue("@keposta", textBox4.Text.Trim());;
                sqlkomutEkle.Parameters.AddWithValue("@ktelefon", textBox5.Text.Trim());
                sqlkomutEkle.Parameters.AddWithValue("@kadres", textBox7.Text.Trim());
                if (radioButtonE.Checked)
                {
                    sqlkomutEkle.Parameters.AddWithValue("@kcinsiyet", "Erkek");
                }
                else
                {
                    sqlkomutEkle.Parameters.AddWithValue("@kcinsiyet", "Kız");
                }

                sqlkomutEkle.ExecuteNonQuery();

                MessageBox.Show("Kayıt Başarılı. Giriş Yapabilirsiniz.");
                this.Close();
                //baglanti2.Close();
                baglanti.SQLBaglanti().Close();
            }
            else
            {
                MessageBox.Show("Kayıt Başarısız ! Lütfen Geçerli Değerler Girin.");
            }
        }

        //BU KISIMDA TIM TEXTBOXLAR SAYI RAKAM BOŞLUK GİBİ DEĞERLERE GÖRE KONTROL EDİLİR
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
        bool durum1,durum2,durum3,durum4,durum5,durum6,durum7 = true;
         
        private void textBox1_Leave(object sender, EventArgs e)
        {          
            if(!(textBox1.Text.All(char.IsLetter)))
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

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (!(textBox3.Text.All(char.IsDigit)))
            {
                textBox3.Text = "Geçerli bir değer girin";
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
                durum5 = false;
            }
            else
            {
                durum5 = true;
            }
        }

        private void textBox4_MouseClick(object sender, MouseEventArgs e)
        {
            if (!durum5)
            {
                textBox4.Text = "";
            }
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            if ((string.IsNullOrEmpty(textBox6.Text)))
            {
                textBox6.Text = "Geçerli bir değer girin";
                durum6 = false;
            }
            else
            {
                durum6 = true;
            }
        }

        private void textBox7_MouseLeave(object sender, EventArgs e)
        {
            if (!(textBox7.Text.All(char.IsLetter)))
            {
                textBox7.Text = "Geçerli bir değer girin";
                durum7 = false;
            }
            else
            {
                durum7 = true;
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox7_MouseClick(object sender, MouseEventArgs e)
        {
            if (!durum7)
            {
                textBox7.Text = "";
            }
        }

        private void Form6_FormClosed(object sender, FormClosedEventArgs e)
        {
            baglanti.SQLBaglanti().Close();
        }

        private void textBox6_MouseClick(object sender, MouseEventArgs e)
        {
            if (!durum6)
            {
                textBox6.Text = "";
            }
        }

        private void textBox3_MouseClick(object sender, MouseEventArgs e)
        {
            if (!durum2)
            {
                textBox3.Text = "";
            }
        }
        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (!(textBox2.Text.All(char.IsLetter)))
            {
                textBox2.Text = "Geçerli bir değer girin";
                durum3 = false;
            }
            else
            {
                durum3 = true;
            }
        }
        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (!durum3)
            {
                textBox2.Text = "";
            }
        }
       
        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (!(textBox5.Text.All(char.IsDigit)))
            {
                textBox5.Text = "Geçerli bir değer girin";
                durum4 = false;
            }
            else
            {
                durum4 = true;
            }
        }
        private void textBox5_MouseClick(object sender, MouseEventArgs e)
        {
            if (!durum4)
            {
                textBox5.Text = "";
            }
        }

        
    }
}
