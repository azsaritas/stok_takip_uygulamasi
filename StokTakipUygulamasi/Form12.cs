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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace WindowsFormsApp1
{
    //İSTATİSTİKLER FORMU

    public partial class Form12 : Form
    {
        sqlbaglantisi baglanti = new sqlbaglantisi();


        //SqlConnection baglanti = new SqlConnection(@"Data Source=AZIZPC\SQLEXPRESS;Initial Catalog=stok_takip;Integrated Security=True");


        public Form12()
        {
            
            InitializeComponent();
            button1.BackColor = Color.GreenYellow;
            button2.BackColor = Color.White;
            button3.BackColor = Color.White;
            panel1.Visible = true; //panel 1 ürün istatistiklerini aktif eder
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;

            //yazılan stored procedurelar ile istatistikler çekilir

            //baglanti.Open();
            SqlCommand sqlKomutu1 = new SqlCommand("up_toplamUrunSayisi", baglanti.SQLBaglanti());
            SqlCommand sqlKomutu2 = new SqlCommand("up_toplamUrunCesidi", baglanti.SQLBaglanti());
            SqlCommand sqlKomutu3 = new SqlCommand("up_toplamStokSayisi", baglanti.SQLBaglanti());
            SqlCommand sqlKomutu4 = new SqlCommand("up_toplamUrunDegeri", baglanti.SQLBaglanti());
            SqlCommand sqlKomutu5 = new SqlCommand("up_enYeniUrunn", baglanti.SQLBaglanti());
            SqlCommand sqlKomutu6 = new SqlCommand("up_enEskiUrunn", baglanti.SQLBaglanti());
            SqlCommand sqlKomutu7 = new SqlCommand("up_enCokStok", baglanti.SQLBaglanti());
            SqlCommand sqlKomutu8 = new SqlCommand("up_enAzStok", baglanti.SQLBaglanti());
            SqlCommand sqlKomutu9 = new SqlCommand("up_enCokStokTipi", baglanti.SQLBaglanti());
            SqlCommand sqlKomutu10 = new SqlCommand("up_enAzStokTipi", baglanti.SQLBaglanti());
            SqlCommand sqlKomutu11 = new SqlCommand("up_enPahali", baglanti.SQLBaglanti());
            SqlCommand sqlKomutu12 = new SqlCommand("up_enUcuz", baglanti.SQLBaglanti());

            //komutların tipi storedprocedure olarak ayarlanır
            sqlKomutu1.CommandType = CommandType.StoredProcedure;
            sqlKomutu2.CommandType = CommandType.StoredProcedure;
            sqlKomutu3.CommandType = CommandType.StoredProcedure;
            sqlKomutu4.CommandType = CommandType.StoredProcedure;
            sqlKomutu5.CommandType = CommandType.StoredProcedure;
            sqlKomutu6.CommandType = CommandType.StoredProcedure;
            sqlKomutu7.CommandType = CommandType.StoredProcedure;
            sqlKomutu8.CommandType = CommandType.StoredProcedure;
            sqlKomutu9.CommandType = CommandType.StoredProcedure;
            sqlKomutu10.CommandType = CommandType.StoredProcedure;
            sqlKomutu11.CommandType = CommandType.StoredProcedure;
            sqlKomutu12.CommandType = CommandType.StoredProcedure;

            //okunan tekli değerler değişkenlere atanır
            var urunSayisi = sqlKomutu1.ExecuteScalar();
            var urunCesidi = sqlKomutu2.ExecuteScalar();
            var urunStokSayisi = sqlKomutu3.ExecuteScalar();
            var urunDegeri = sqlKomutu4.ExecuteScalar();
            var enYeniUrunn = sqlKomutu5.ExecuteScalar();
            var enEskiUrunn = sqlKomutu6.ExecuteScalar();
            var enCokStok = sqlKomutu7.ExecuteScalar();
            var enAzStok = sqlKomutu8.ExecuteScalar();
            var enCokStokTipi = sqlKomutu9.ExecuteScalar();
            var enAzStokTipi = sqlKomutu10.ExecuteScalar();
            var enPahali = sqlKomutu11.ExecuteScalar();
            var enUcuz = sqlKomutu12.ExecuteScalar();

            sqlKomutu1.ExecuteNonQuery();

            //değişkenler lablellere atanır
            label17.Text = urunSayisi.ToString();
            label18.Text = urunCesidi.ToString();
            label19.Text = urunStokSayisi.ToString();
            label20.Text = urunDegeri.ToString();
            label21.Text = enYeniUrunn.ToString();
            label22.Text = enEskiUrunn.ToString();
            label23.Text = enCokStok.ToString();
            label24.Text = enAzStok.ToString();
            label25.Text = enCokStokTipi.ToString();
            label26.Text = enAzStokTipi.ToString();
            label27.Text = enPahali.ToString();
            label28.Text = enUcuz.ToString();

            //baglanti.Close();
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //butonların rengi ve panel durumları ayarlanır

            button1.BackColor = Color.GreenYellow;
            button2.BackColor = Color.White;
            button3.BackColor = Color.White;
            button4.BackColor = Color.White;

            panel1.Visible = true;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //ÜSTEKİ AYNI İŞLEMLER KULLANICI İSTATİSTİKLERİ İÇİN YAPILIR

            button1.BackColor = Color.White;
            button2.BackColor = Color.GreenYellow;
            button3.BackColor = Color.White;
            button4.BackColor = Color.White;

            panel2.Visible=true;
            panel3.Visible=false;
            panel4.Visible = false;


            //baglanti.Open();
            SqlCommand sqlKomutu1 = new SqlCommand("up_toplamKullanici", baglanti.SQLBaglanti());
            SqlCommand sqlKomutu2 = new SqlCommand("up_erkekKullanici", baglanti.SQLBaglanti());
            SqlCommand sqlKomutu3 = new SqlCommand("up_kizKullanici", baglanti.SQLBaglanti());
            SqlCommand sqlKomutu4 = new SqlCommand("up_enGencKullanici", baglanti.SQLBaglanti());
            SqlCommand sqlKomutu5 = new SqlCommand("up_enYasliKullanici", baglanti.SQLBaglanti());
            SqlCommand sqlKomutu6 = new SqlCommand("up_enEskiKayitliKullanici", baglanti.SQLBaglanti());
            SqlCommand sqlKomutu7 = new SqlCommand("up_enYeniKayitliKullanici", baglanti.SQLBaglanti());
            SqlCommand sqlKomutu8 = new SqlCommand("up_18yasAltiKullanici", baglanti.SQLBaglanti());
            SqlCommand sqlKomutu9 = new SqlCommand("up_18yasUstuKullanici", baglanti.SQLBaglanti());
        

            sqlKomutu1.CommandType = CommandType.StoredProcedure;
            sqlKomutu2.CommandType = CommandType.StoredProcedure;
            sqlKomutu3.CommandType = CommandType.StoredProcedure;
            sqlKomutu4.CommandType = CommandType.StoredProcedure;
            sqlKomutu5.CommandType = CommandType.StoredProcedure;
            sqlKomutu6.CommandType = CommandType.StoredProcedure;
            sqlKomutu7.CommandType = CommandType.StoredProcedure;
            sqlKomutu8.CommandType = CommandType.StoredProcedure;
            sqlKomutu9.CommandType = CommandType.StoredProcedure;
         

            var toplamKullanici = sqlKomutu1.ExecuteScalar();
            var erkekKullanici = sqlKomutu2.ExecuteScalar();
            var kizKullanici = sqlKomutu3.ExecuteScalar();
            var enGencKullanici = sqlKomutu4.ExecuteScalar();
            var enYasliKullanici = sqlKomutu5.ExecuteScalar();
            var enEskiKullanici = sqlKomutu6.ExecuteScalar();
            var enYeniKullanici = sqlKomutu7.ExecuteScalar();
            var on8yasAlti = sqlKomutu8.ExecuteScalar();
            var on8yasUstu = sqlKomutu9.ExecuteScalar();
            

            sqlKomutu1.ExecuteNonQuery();
            label38.Text = toplamKullanici.ToString();
            label37.Text = erkekKullanici.ToString();
            label36.Text = kizKullanici.ToString();
            label35.Text = enGencKullanici.ToString();
            label34.Text = enYasliKullanici.ToString();
            label33.Text = enEskiKullanici.ToString();
            label32.Text = enYeniKullanici.ToString();
            label31.Text = on8yasAlti.ToString();
            label30.Text = on8yasUstu.ToString();
            

           // baglanti.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //ÜSTEKİ AYNI İŞLEMLER SATICI İSTATİSTİKLERİ İÇİN YAPILIR

            button1.BackColor = Color.White;
            button2.BackColor = Color.White;
            button3.BackColor = Color.GreenYellow;
            button4.BackColor = Color.White;

            panel2.Visible=false;
            panel3.Visible=true;
            panel4.Visible = false;


            //baglanti.Open();
            SqlCommand sqlKomutu1 = new SqlCommand("up_toplamSatici", baglanti.SQLBaglanti());
            SqlCommand sqlKomutu2 = new SqlCommand("up_enCokUrunluSatici", baglanti.SQLBaglanti());
            SqlCommand sqlKomutu3 = new SqlCommand("up_enAzUrunluSatici", baglanti.SQLBaglanti());
            SqlCommand sqlKomutu4 = new SqlCommand("up_enPahaliUrunSatici", baglanti.SQLBaglanti());
            SqlCommand sqlKomutu5 = new SqlCommand("up_enUcuzUrunSatici", baglanti.SQLBaglanti());
            SqlCommand sqlKomutu6 = new SqlCommand("up_enCokStokluSatici", baglanti.SQLBaglanti());
            SqlCommand sqlKomutu7 = new SqlCommand("up_enAzStokluSatici", baglanti.SQLBaglanti());
            SqlCommand sqlKomutu8 = new SqlCommand("up_urunCesidiEnCokSatici", baglanti.SQLBaglanti());
            SqlCommand sqlKomutu9 = new SqlCommand("up_urunCesidiEnAzSatici", baglanti.SQLBaglanti());


            sqlKomutu1.CommandType = CommandType.StoredProcedure;
            sqlKomutu2.CommandType = CommandType.StoredProcedure;
            sqlKomutu3.CommandType = CommandType.StoredProcedure;
            sqlKomutu4.CommandType = CommandType.StoredProcedure;
            sqlKomutu5.CommandType = CommandType.StoredProcedure;
            sqlKomutu6.CommandType = CommandType.StoredProcedure;
            sqlKomutu7.CommandType = CommandType.StoredProcedure;
            sqlKomutu8.CommandType = CommandType.StoredProcedure;
            sqlKomutu9.CommandType = CommandType.StoredProcedure;


            var toplamSatici = sqlKomutu1.ExecuteScalar();
            var enCokUrunluSatici = sqlKomutu2.ExecuteScalar();
            var enAzUrunluSatici = sqlKomutu3.ExecuteScalar();
            var enPahaliUrunluSatici = sqlKomutu4.ExecuteScalar();
            var enUcuzUrunluSatici = sqlKomutu5.ExecuteScalar();
            var enCokStokluSatici = sqlKomutu6.ExecuteScalar();
            var enAzStokluSatici = sqlKomutu7.ExecuteScalar();
            var urunCesidiEnCokSatici = sqlKomutu8.ExecuteScalar();
            var urunCesidiEnAzSatici = sqlKomutu9.ExecuteScalar();


            sqlKomutu1.ExecuteNonQuery();
            label63.Text = toplamSatici.ToString();
            label62.Text = enCokUrunluSatici.ToString();
            label61.Text = enAzUrunluSatici.ToString();
            label60.Text = enPahaliUrunluSatici.ToString();
            label59.Text = enUcuzUrunluSatici.ToString();
            label58.Text = enCokStokluSatici.ToString();
            label57.Text = enAzStokluSatici.ToString();
            label56.Text = urunCesidiEnCokSatici.ToString();
            label55.Text = urunCesidiEnAzSatici.ToString();


            //baglanti.Close();

        }

        private void label54_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //BU KISIMDA FONKSİYONLAR KULLANILARAK SİPARİŞ İSTATİSTİKLERİ GÖSTERİLİR

            button1.BackColor = Color.White;
            button2.BackColor = Color.White;
            button3.BackColor = Color.White;
            button4.BackColor = Color.GreenYellow;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = true;

            //baglanti.Open();
          
            //kullanılan fonksiyonlar çağrılır
            SqlCommand sqlKomutu1 = new SqlCommand("SELECT dbo.SiparisSayisi()", baglanti.SQLBaglanti());
            SqlCommand sqlKomutu2 = new SqlCommand("SELECT dbo.SiparistenToplamKazanc()", baglanti.SQLBaglanti());
            SqlCommand sqlKomutu3 = new SqlCommand("SELECT dbo.SiparisiOlanSaticiSayisi()", baglanti.SQLBaglanti());

            //fonksiyonlar değişkneler atanır
            var siparisSayisi = sqlKomutu1.ExecuteScalar();
            var siparisKazanc = sqlKomutu2.ExecuteScalar();
            var siparisiOlanSaticilar = sqlKomutu3.ExecuteScalar();

           

            sqlKomutu1.ExecuteNonQuery();

            //değişkenler labellara atanır
            label53.Text = siparisSayisi.ToString();
            label52.Text = siparisKazanc.ToString()+" TL";
            label54.Text = siparisiOlanSaticilar.ToString();

           // baglanti.Close();

        }

        private void Form12_FormClosed(object sender, FormClosedEventArgs e)
        {
            baglanti.SQLBaglanti().Close();
        }
    }
}
