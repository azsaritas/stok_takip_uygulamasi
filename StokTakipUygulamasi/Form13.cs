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
    //OLAY GÜNLÜKLERİ FORMU --TRİGGERLER BURADA KULLANILDI

    public partial class Form13 : Form
    {
        sqlbaglantisi baglanti = new sqlbaglantisi();


        //SqlConnection baglanti = new SqlConnection(@"Data Source=AZIZPC\SQLEXPRESS;Initial Catalog=stok_takip;Integrated Security=True");


        public Form13()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //eklenen ürünler
            //TRIGGER KULLANILARAK OLUŞTURULAN OlaylarUrun TABLOSUNDAN EKLENEN ÜRÜNLER LİSTELENİR

            SqlCommand sqlKomutu = new SqlCommand("SELECT * FROM OlaylarUrun Where Durum LIKE '%Eklendi.'", baglanti.SQLBaglanti());
            //LIKE komutu ile sonu eklendi ile biten ürünler seçilir 

            SqlDataAdapter veriOkuyucu = new SqlDataAdapter(sqlKomutu);
            DataTable veriTablosu = new DataTable();
            veriOkuyucu.Fill(veriTablosu);
            dataGridView1.DataSource = veriTablosu;

            button1.BackColor = Color.GreenYellow; //tıklanan butona göre butonların renkleri değişir
            button2.BackColor = Color.White;
            button3.BackColor = Color.White;
            button4.BackColor = Color.White;
            button5.BackColor = Color.White;
            button6.BackColor = Color.White;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //silinen ürünler

            SqlCommand sqlKomutu = new SqlCommand("SELECT * FROM OlaylarUrun Where Durum LIKE '%Silindi.'", baglanti.SQLBaglanti());

            SqlDataAdapter veriOkuyucu = new SqlDataAdapter(sqlKomutu);
            DataTable veriTablosu = new DataTable();
            veriOkuyucu.Fill(veriTablosu);
            dataGridView1.DataSource = veriTablosu;

            button1.BackColor = Color.White;
            button2.BackColor = Color.GreenYellow;
            button3.BackColor = Color.White;
            button4.BackColor = Color.White;
            button5.BackColor = Color.White;
            button6.BackColor = Color.White;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //eklenen kullanıcılar

            SqlCommand sqlKomutu = new SqlCommand("SELECT * FROM OlaylarKullanici Where Durum LIKE '%Eklendi.'", baglanti.SQLBaglanti());

            SqlDataAdapter veriOkuyucu = new SqlDataAdapter(sqlKomutu);
            DataTable veriTablosu = new DataTable();
            veriOkuyucu.Fill(veriTablosu);
            dataGridView1.DataSource = veriTablosu;

            button1.BackColor = Color.White;
            button2.BackColor = Color.White;
            button3.BackColor = Color.GreenYellow;
            button4.BackColor = Color.White;
            button5.BackColor = Color.White;
            button6.BackColor = Color.White;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //silinen kullanıcılar

            SqlCommand sqlKomutu = new SqlCommand("SELECT * FROM OlaylarKullanici Where Durum LIKE '%Silindi.'", baglanti.SQLBaglanti());

            SqlDataAdapter veriOkuyucu = new SqlDataAdapter(sqlKomutu);
            DataTable veriTablosu = new DataTable();
            veriOkuyucu.Fill(veriTablosu);
            dataGridView1.DataSource = veriTablosu;

            button1.BackColor = Color.White;
            button2.BackColor = Color.White;
            button3.BackColor = Color.White;
            button4.BackColor = Color.GreenYellow;
            button5.BackColor = Color.White;
            button6.BackColor = Color.White;
        }

        private void button5_Click(object sender, EventArgs e)
        {

            //eklenen satıcılar

            SqlCommand sqlKomutu = new SqlCommand("SELECT * FROM OlaylarSatici Where Durum LIKE '%Eklendi.'", baglanti.SQLBaglanti());

            SqlDataAdapter veriOkuyucu = new SqlDataAdapter(sqlKomutu);
            DataTable veriTablosu = new DataTable();
            veriOkuyucu.Fill(veriTablosu);
            dataGridView1.DataSource = veriTablosu;

            button1.BackColor = Color.White;
            button2.BackColor = Color.White;
            button3.BackColor = Color.White;
            button4.BackColor = Color.White;
            button5.BackColor = Color.GreenYellow;
            button6.BackColor = Color.White;
        }

        private void button6_Click(object sender, EventArgs e)
        {

            //silinen satıcılar

            SqlCommand sqlKomutu = new SqlCommand("SELECT * FROM OlaylarSatici Where Durum LIKE '%Silindi.'", baglanti.SQLBaglanti());

            SqlDataAdapter veriOkuyucu = new SqlDataAdapter(sqlKomutu);
            DataTable veriTablosu = new DataTable();
            veriOkuyucu.Fill(veriTablosu);
            dataGridView1.DataSource = veriTablosu;

            button1.BackColor = Color.White;
            button2.BackColor = Color.White;
            button3.BackColor = Color.White;
            button4.BackColor = Color.White;
            button5.BackColor = Color.White;
            button6.BackColor = Color.GreenYellow;
        }

        private void Form13_Load(object sender, EventArgs e)
        {
            //başlangıçta eklenen ürünler gösterilir

            SqlCommand sqlKomutu = new SqlCommand("SELECT * FROM OlaylarUrun Where Durum LIKE '%Eklendi.'", baglanti.SQLBaglanti());

            SqlDataAdapter veriOkuyucu = new SqlDataAdapter(sqlKomutu);
            DataTable veriTablosu = new DataTable();
            veriOkuyucu.Fill(veriTablosu);
            dataGridView1.DataSource = veriTablosu;

            dataGridView1.Columns["ID"].Width = 5;
            dataGridView1.Columns["Durum"].Width = 1000;
            dataGridView1.Refresh();

            button1.BackColor = Color.GreenYellow;
            button2.BackColor = Color.White;
            button3.BackColor = Color.White;
            button4.BackColor = Color.White;
            button5.BackColor = Color.White;
            button6.BackColor = Color.White;
        
        }

        private void Form13_FormClosed(object sender, FormClosedEventArgs e)
        {
            baglanti.SQLBaglanti().Close();
        }
    }
}
