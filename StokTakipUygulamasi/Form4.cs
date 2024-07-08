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
    //SATICI GİRİŞİ FORMU

    public partial class Form4 : Form
    {
        sqlbaglantisi baglanti = new sqlbaglantisi();

        public Form4()
        {
            InitializeComponent();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Form7 saticiKaydi= new Form7();
            saticiKaydi.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //sql bağlantısı yapılır ve isim-eposta şifre kontrolu yapılır

            //SqlConnection baglanti4 = new SqlConnection(@"Data Source=AZIZPC\SQLEXPRESS;Initial Catalog=stok_takip;Integrated Security=True");

            
            SqlCommand sqlkomutGiris = new SqlCommand("SELECT * FROM Saticilar WHERE (SaticiMagazaAdi=@ssad OR SaticiEposta=@sseposta) AND SaticiSifre=@sssifre", baglanti.SQLBaglanti());
            sqlkomutGiris.Parameters.AddWithValue("@ssad", textBox1.Text);
            sqlkomutGiris.Parameters.AddWithValue("@sseposta", textBox1.Text);
            sqlkomutGiris.Parameters.AddWithValue("@sssifre", textBox2.Text);

            SqlDataReader sqlVeriOku = sqlkomutGiris.ExecuteReader();

            if (sqlVeriOku.Read()) //değer true ise satici paneli olan form8 açılır
            {
                MessageBox.Show("Giriş Başarılı");
                Form8 saticiStokBilgiEkrani = new Form8();
                saticiStokBilgiEkrani.saticiAdi = textBox1.Text;  //satici adı form8e iletilir
                this.Close();
                saticiStokBilgiEkrani.Show(); //form8 açılır
                                
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre yanlış"); //hatalı durum 
            }

            baglanti.SQLBaglanti().Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) //şifrenin gizlenip gizlenmeme durumu
            {
                textBox2.PasswordChar = '\0';
            }
            else
            {
                textBox2.PasswordChar = '*';
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*'; //form açılışında şifer gizli durumdadır
        }

        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
     
        }
    }
}
