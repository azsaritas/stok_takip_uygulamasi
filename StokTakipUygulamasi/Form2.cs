using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    //KULLANICI GİRİŞİ FORMU

    public partial class Form2 : Form
    {
        sqlbaglantisi baglanti=new sqlbaglantisi();
        // SqlConnection baglanti3 = new SqlConnection(connectionString);

        //SqlConnection baglanti3 = new SqlConnection("Server = .;Database=stok_takip;Integrated Security=True");
        //SqlConnection baglanti3 = new SqlConnection(@"Data Source=.;Initial Catalog=stok_takip;Integrated Security=True");
        //SQLConnection baglanti3 = new SQLConnection();
        public Form2()
        {
     
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //bu kısımda kullanıcı ad ve şifre sorgusu yapılır 
            //baglanti.SQLBaglanti();
            SqlCommand sqlkomutGiris = new SqlCommand("SELECT * FROM Kullanicilar WHERE (Ad=@kkad OR Eposta=@kkeposta) AND Sifre=@kksifre", baglanti.SQLBaglanti());
            sqlkomutGiris.Parameters.AddWithValue("@kkad", textBox1.Text);
            sqlkomutGiris.Parameters.AddWithValue("@kkeposta", textBox1.Text);
            sqlkomutGiris.Parameters.AddWithValue("@kksifre", textBox2.Text);

            SqlDataReader sqlVeriOku = sqlkomutGiris.ExecuteReader();

            if (sqlVeriOku.Read())  //değer true ise giriş yapılır
            {
                MessageBox.Show("Giriş Başarılı");
                Form5 stokBilgiEkrani = new Form5(); //kullanıcı paneli olan form5 açılır
                stokBilgiEkrani.KullaniciAdi = textBox1.Text; //kullanıcı adı form5e iletilir
                stokBilgiEkrani.KullaniciSifre = textBox2.Text; //kullanıcı şifresi form5e iletilir
                this.Close();  //bu form kapatılır
                stokBilgiEkrani.Show();  //form5 açılır
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre yanlış"); //hatalı durum
            }

            baglanti.SQLBaglanti().Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form6 kullaniciKaydi = new Form6(); //kayıt olmak için kayıt formu olan form6 açılır
            kullaniciKaydi.ShowDialog();
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) //şifre girilen textbox checkboxun duruma göre sansürlenir
            {
                textBox2.PasswordChar = '\0'; //şifreyi göster durumu
            }
            else
            {
                textBox2.PasswordChar = '*'; //şifreyi gizle durumu
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
        
            textBox2.PasswordChar = '*'; //şifre gizli haldes
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
        
        }
    }
}
