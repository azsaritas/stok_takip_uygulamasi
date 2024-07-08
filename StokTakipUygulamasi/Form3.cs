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
    //ADMİN GİRİŞİ FORMU

    public partial class Form3 : Form
    {
        sqlbaglantisi baglanti = new sqlbaglantisi();

        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //sql bağlantıları yapılır
            //SqlConnection baglanti5 = new SqlConnection("Server = .;Database=stok_takip;Integrated Security=True");

            //baglanti5.Open(); //bağlantı açılır ad ve şifre sorugusu yapılır
            SqlCommand sqlkomutGiris = new SqlCommand("SELECT * FROM Adminler WHERE AdminKullaniciAdi=@aad AND AdminSifre=@aasifre", baglanti.SQLBaglanti());
            sqlkomutGiris.Parameters.AddWithValue("@aad", textBox1.Text);
            sqlkomutGiris.Parameters.AddWithValue("@aasifre", textBox2.Text);

            SqlDataReader sqlVeriOku = sqlkomutGiris.ExecuteReader();

            if (sqlVeriOku.Read()) //değer true ise admin paneli olan form9 açılır
            {
                MessageBox.Show("Giriş Başarılı");
                this.Close();   //bu form kapatılır
                Form9 stokBilgiEkrani = new Form9(); //admin paneli açılır
                stokBilgiEkrani.Show();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre yanlış"); //hatalı durum
            }

            baglanti.SQLBaglanti().Close(); //bağlantı kapatılır
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)   //şifrenin gösterilip gösterilmeme durumu ayarlanır
            {
                textBox2.PasswordChar = '\0';
            }
            else
            {
                textBox2.PasswordChar = '*';
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*'; //form açılışında şifre gizli durumda olur
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
    }
}
