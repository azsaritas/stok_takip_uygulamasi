using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    //KULLANICI PANELİ FORMU

    public partial class Form5 : Form
    {
        sqlbaglantisi baglanti = new sqlbaglantisi();

        //SqlConnection baglanti = new SqlConnection(@"Data Source=AZIZPC\SQLEXPRESS;Initial Catalog=stok_takip;Integrated Security=True");
        //SqlConnection baglanti = new SqlConnection("Server = .;Database=stok_takip;Integrated Security=True");
        //SQLConnection baglanti = new SQLConnection();

        public string KullaniciAdi; //form2den çekilen kullanıcı adı
        public string KullaniciSifre; //form2dem çekilen kullanici sifresi
        public bool butonDurum=false;  //sipariş gösterilen butonun durumu
        public bool butonviewDurum=false; //view çalıştırılan butonun durumu
        public Form5()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //combobox değeri değiştiği zaman devreye girer
            //baglanti.Open();
            if (comboBox1.SelectedIndex==0) //comboboxta tümü seçildi ise tüm ürünleri gösterir
            {
                //urunleri gösteren sql sorgusu
                SqlCommand sqlKomutu = new SqlCommand("SELECT * FROM Urunler", baglanti.SQLBaglanti());
                SqlDataAdapter veriOkuyucu = new SqlDataAdapter(sqlKomutu);
                DataTable veriTablosu = new DataTable();
                veriOkuyucu.Fill(veriTablosu);
                dataGridView1.DataSource = veriTablosu; //gelen veriler datagridviewe gönderilir
                sqlKomutu.ExecuteNonQuery();

            }
            else //comboboxta belirli bir kategori değeri seçildi ise o kategorideki ürünleri gösterir
            {
                
                SqlCommand sqlKomutu = new SqlCommand("SELECT * FROM Urunler Where UrunTipi=@urunTipi", baglanti.SQLBaglanti());
                sqlKomutu.Parameters.AddWithValue("@urunTipi", comboBox1.SelectedItem.ToString()); //comboboxtan ürün tipi değeri alınır
                SqlDataAdapter veriOkuyucu = new SqlDataAdapter(sqlKomutu);
                DataTable veriTablosu = new DataTable();
                veriOkuyucu.Fill(veriTablosu);
                dataGridView1.DataSource = veriTablosu; 
                sqlKomutu.ExecuteNonQuery();
            }
                        
            //baglanti.Close();

        }

        private void Form5_Load(object sender, EventArgs e)
        {
            
            button3.Enabled = false;

            label15.Text = KullaniciAdi + " Olarak Giriş Yaptınız."; //giriş yapan kullanıcının adı gözükür
            label13.ForeColor = Color.Red; //label rengi kırmızı olur
            label13.Text = "Kullanıcı Girişi Yaptığınız İçin\nÜrün Düzenleme Yapamazsınız!";
            //baglanti.Open();
            SqlCommand sqlKomutu = new SqlCommand("SELECT * FROM Urunler", baglanti.SQLBaglanti()); //başlangışta tüm ürünleri gösterir
            SqlDataAdapter veriOkuyucu = new SqlDataAdapter(sqlKomutu);
            DataTable veriTablosu = new DataTable();
            veriOkuyucu.Fill(veriTablosu);
            dataGridView1.DataSource = veriTablosu;

            sqlKomutu.ExecuteNonQuery();

            //baglanti.Close();
            Form1 form1 = (Form1)Application.OpenForms["Form1"]; //form1 gizlenir
            if (form1 != null)
            {
                form1.Hide();        
            }

            comboBox1.Items.Clear(); //combobox itemleri temizlenir
            comboBox1.Items.Add("Tümü"); //comboboxa tümü itemi item0 olarak eklenir
            comboBox1.SelectedIndex = 0; //item 0 seçilir yani tümü 

            //baglanti.Open();
            SqlCommand sqlKomutux = new SqlCommand("up_kategoriListesi", baglanti.SQLBaglanti()); //kategori listesini getiren sp çalıştırılır
            sqlKomutux.CommandType = CommandType.StoredProcedure;

            SqlDataReader okuyucu = sqlKomutux.ExecuteReader();

            while (okuyucu.Read())
            {
                comboBox1.Items.Add(okuyucu["UrunTipi"].ToString()); //kategori listesi comboboxa eklenir
            }

            //baglanti.Close();
        }

        private void Form5_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 form1a = (Form1)Application.OpenForms["Form1"]; //form5 kapatıldığında form1 açılır
            form1a.Show();
            baglanti.SQLBaglanti().Close();
        }

        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {
           
        }

        private void button20_Click(object sender, EventArgs e)
        { 
            Form11 form11 = new Form11(); //ürün ara butonuna basılınca form11 açılır
            form11.ShowDialog();   
            dataGridView1.DataSource = form11.veriYeni; //form11 den veriler tekrardan bu form5 e gelip datagrid viewe yazılır
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            label13.ForeColor = Color.Red;
            label13.Text = "Kullanıcı Girişi Yaptığınız İçin\nÜrün Düzenleme İşlemleri Yapamazsınız!";
            if (e.RowIndex == -1) //datagridviewde tıklanan yer index dışı ise return döner
            {
                return;
            }
            else
            {
                if (butonviewDurum == false) //siparişlerimi göster butonu aktif değilse çalışır
                {
                    if (butonDurum == false) 
                    {
                        //datagridview değerleri sutun sutun textboxlara yazdırılır
                        textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                        textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                        textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                        textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                        textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                        textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                        textBox7.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                        textBox8.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                        textBox9.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                        textBox10.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    }
                    else
                    {
                        label5.Text = "SiparisID: "; //label5 siparis id olarak değiştirilir çünkü siparişler görüntüleniyor
                        label6.Text = "KullanıcıID: ";// aynı şekilde label6 kullanıcı id olarak değiştirliyor

                        textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                        textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();          
                        textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                        textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                        textBox9.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                        textBox10.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                        textBox11.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                    }
                }
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //temizle butonuna tıklandığında textbox değerleri sıfırlanır
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox10.Text = "";
            label13.Text = "Temizleme Başarılı!";
            label13.ForeColor = Color.Green;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

            //yenile butonu
            button3.Enabled = false; //diğer butonlarn durumlarını tekrardan ayarlar
            button1.Enabled = true;
            butonviewDurum = false;
            label5.Text = "KategoriID: "; //labeller eski haline döndürülür çünkü artık tüm ürünler görüntüleniyor
            label6.Text = "Ürün Tipi: ";
            int seciliIndex = comboBox1.SelectedIndex; //comboboxta seçili indexe göre yenileme yapılır


            comboBox1.Items.Clear(); 
            comboBox1.Items.Add("Tümü");

            //baglanti.Open();

            SqlCommand sqlKomutuc = new SqlCommand("up_kategoriListesi", baglanti.SQLBaglanti()); //kategori listesi tekrardan comboboxa yüklenit
            sqlKomutuc.CommandType = CommandType.StoredProcedure;

            SqlDataReader okuyucu = sqlKomutuc.ExecuteReader();

            while (okuyucu.Read())
            {
                comboBox1.Items.Add(okuyucu["UrunTipi"].ToString());

            }

            //baglanti.Close();
            comboBox1.SelectedIndex = seciliIndex; 
            //baglanti.Open();

            if (comboBox1.SelectedIndex == 0) //comboboxta seçili index 0 yani tümü ise tüm ürünler gösterilir
            {
                    SqlCommand sqlKomutux = new SqlCommand("SELECT * FROM Urunler", baglanti.SQLBaglanti());
                    SqlDataAdapter veriOkuyucux = new SqlDataAdapter(sqlKomutux);
                    DataTable veriTablosux = new DataTable();
                    veriOkuyucux.Fill(veriTablosux);
                    dataGridView1.DataSource = veriTablosux;

                    sqlKomutux.ExecuteNonQuery();  
            }

            if (comboBox1.SelectedIndex > 0) //comboboxta seçili bir kategori varsa o kategori ürünlerini gösterir
            {
                    SqlCommand sqlKomutua = new SqlCommand("SELECT * FROM Urunler where UrunTipi=@urunTipi", baglanti.SQLBaglanti());
                    sqlKomutua.Parameters.AddWithValue("@urunTipi", comboBox1.SelectedItem.ToString());
                    SqlDataAdapter veriOkuyucua = new SqlDataAdapter(sqlKomutua);
                    DataTable veriTablosua = new DataTable();
                    veriOkuyucua.Fill(veriTablosua);
                    dataGridView1.DataSource = veriTablosua;

                    sqlKomutua.ExecuteNonQuery();
            }
            //baglanti.Close();
            label13.Text = "Yenileme Başarılı!";
            label13.ForeColor = Color.White;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ürün al butonu
            if (textBox11.Text.All(char.IsNumber) && !(string.IsNullOrEmpty(textBox11.Text))) //ürün adeti köntrölü yapılır
            {
                //baglanti.Open();

                //kullanıcılar tablosundan ad ve sifre ile sorgu yapılıp kullanıcıID alınır
                SqlCommand sqlKomutuKullaniciID = new SqlCommand("Select KullaniciID From Kullanicilar Where Ad=@sialici AND Sifre=@kullanicisifre", baglanti.SQLBaglanti());

                sqlKomutuKullaniciID.Parameters.AddWithValue("@sialici", KullaniciAdi.ToString());
                sqlKomutuKullaniciID.Parameters.AddWithValue("@kullanicisifre", KullaniciSifre.ToString());

                //kullanıcılar tablosundan ad ve şifre ile sorgu yapılıp adres bilgisi alınır
                SqlCommand sqlKomutuKullaniciAdres = new SqlCommand("Select Adres From Kullanicilar Where Ad=@sialicix2 AND Sifre=@kullanicisifrex2", baglanti.SQLBaglanti());
                sqlKomutuKullaniciAdres.Parameters.AddWithValue("@sialicix2", KullaniciAdi.ToString());
                sqlKomutuKullaniciAdres.Parameters.AddWithValue("@kullanicisifrex2", KullaniciSifre.ToString());

                //alınan adet tutarı stoktaki değerden fazla ise işleme girmez
                if (Convert.ToInt32(textBox5.Text) >= Convert.ToInt32(textBox11.Text))
                {
                    //urun alındı stored procedure çalıştırılarak alınan ürün siparişler kısmına eklenir
                    SqlCommand sqlKomutuUrunAlindi = new SqlCommand("up_UrunAlindi", baglanti.SQLBaglanti());
                    sqlKomutuUrunAlindi.CommandType = CommandType.StoredProcedure;
                    sqlKomutuUrunAlindi.Parameters.AddWithValue("@UrunID", Convert.ToInt32(textBox1.Text));
                    sqlKomutuUrunAlindi.Parameters.AddWithValue("@AlinanMiktar", Convert.ToInt32(textBox11.Text));

                    sqlKomutuUrunAlindi.ExecuteNonQuery();

                    //siparişler kısmına alınan ürünün bilgileri girilir
                    SqlCommand sqlEklemeKomutu = new SqlCommand("INSERT INTO Siparisler(UrunID,KullaniciID,Alici,Adres,Satici,Urun,Fiyat,Adet,Tarih) " +
                        "VALUES (@siurunid,@sikullaniciid,@sialicix,@siadres,@sisatici,@siurun,@sifiyat,@siadet,@sitarih)", baglanti.SQLBaglanti());
                    var xkullaniciAdres = sqlKomutuKullaniciAdres.ExecuteScalar();

                    var xkullaniciID = sqlKomutuKullaniciID.ExecuteScalar();

                    DateTime suan = DateTime.Now;

                    //datagridview den alına textbox değerleri değişkenlere atanır
                    sqlEklemeKomutu.Parameters.AddWithValue("@siadres", xkullaniciAdres.ToString());
                    sqlEklemeKomutu.Parameters.AddWithValue("@siurunid", textBox1.Text);
                    sqlEklemeKomutu.Parameters.AddWithValue("@sialicix", KullaniciAdi);
                    sqlEklemeKomutu.Parameters.AddWithValue("@sikullaniciid", Convert.ToInt32(xkullaniciID));
                    sqlEklemeKomutu.Parameters.AddWithValue("@siurun", textBox3.Text + textBox4.Text);
                    sqlEklemeKomutu.Parameters.AddWithValue("@siadet", Convert.ToInt32(textBox11.Text));
                    sqlEklemeKomutu.Parameters.AddWithValue("@sifiyat", Convert.ToInt32(textBox6.Text));
                    sqlEklemeKomutu.Parameters.AddWithValue("@sitarih", suan.ToShortDateString());
                    sqlEklemeKomutu.Parameters.AddWithValue("@sisatici", textBox9.Text);

                    sqlEklemeKomutu.ExecuteNonQuery();
                    label13.Text = "Ürün Alımı Başarılı!  \nSiparişler Kısmını Kontrol Ediniz!";
                    label13.ForeColor = Color.Green;
                    button4.BackColor = Color.GreenYellow;

                }
                else //girilem adet stoktaki değerden fazla ise hata mesejı gösterilir
                {
                    MessageBox.Show("Stok Yetersiz!");
                }
                
                //baglanti.Close();
            }
            else {
                textBox11.Text = "Geçersiz Değer"; //adet yerine sayı dışı değer girilirse bu hata yazdırılır
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //siparişeri görüntüle butonu
            button3.Enabled = true;
            button1.Enabled = false;
            butonviewDurum = false;
            label5.Text = "SiparisID: "; //label değerleri sipariş tablosuna göre düzenlenir
            label6.Text = "KullanıcıID: ";
            butonDurum = true;
            //baglanti.Open();
            
            //kullanıcı ad ve şireye göre kullanıcılar tablosundan kullanıcı id alınır
            SqlCommand sqlKomutuKullaniciID = new SqlCommand("Select * From Kullanicilar Where Ad=@sialici AND Sifre=@kullanicisifre", baglanti.SQLBaglanti());

            sqlKomutuKullaniciID.Parameters.AddWithValue("@sialici", KullaniciAdi.ToString());
            sqlKomutuKullaniciID.Parameters.AddWithValue("@kullanicisifre", KullaniciSifre.ToString());
            var xkullaniciID = sqlKomutuKullaniciID.ExecuteScalar();

            //alınan kullanııc ide göre siparişler kısmından giriş yapan kullanıcının verdiği siparişler gözükür
            SqlCommand sqlKomutux = new SqlCommand("SELECT * FROM Siparisler Where KullaniciID=@kid", baglanti.SQLBaglanti());
            sqlKomutux.Parameters.AddWithValue("@kid", xkullaniciID);
            SqlDataAdapter veriOkuyucux = new SqlDataAdapter(sqlKomutux);
            DataTable veriTablosux = new DataTable();
            veriOkuyucux.Fill(veriTablosux);
            dataGridView1.DataSource = veriTablosux;

            sqlKomutux.ExecuteNonQuery();

            //baglanti.Close();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //siparişi iptal et butonu
            //baglanti.Open();
 
            //kullanıcı id ve sipariş ide göre sipraişi iptal eden sql sorgusu
            SqlCommand sqlKomutux = new SqlCommand("Delete FROM Siparisler Where KullaniciID=@kid and SiparisID=@sid", baglanti.SQLBaglanti());
            sqlKomutux.Parameters.AddWithValue("@kid", textBox10.Text);
            sqlKomutux.Parameters.AddWithValue("@sid", textBox2.Text);

            //ürün alındı stored procedure çalışır fakat alınan miktar değeri -1 ile çarpılarak miktar iptal edlilip stoka eklenir
            SqlCommand sqlKomutuUrunAlindi = new SqlCommand("up_UrunAlindi", baglanti.SQLBaglanti());
            sqlKomutuUrunAlindi.CommandType = CommandType.StoredProcedure;
            sqlKomutuUrunAlindi.Parameters.AddWithValue("@UrunID", Convert.ToInt32(textBox1.Text));
            sqlKomutuUrunAlindi.Parameters.AddWithValue("@AlinanMiktar", -1*(Convert.ToInt32(textBox11.Text))); //tekrak eklemek için -1 ile çarpıldı

            sqlKomutuUrunAlindi.ExecuteNonQuery();

            sqlKomutux.ExecuteNonQuery();

            label13.Text = "Sipariş İptal Edildi! Yenile Butonuna Basınız.";

            //baglanti.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            //ürünleri görüntüe butonu
            button3.Enabled = false;
            button1.Enabled = true;
            butonviewDurum = false;
            butonDurum = false;
            label5.Text = "KategoriID: "; //label değerleri tekrardan ürünlere göre düzenlennir
            label6.Text = "Ürün Tipi: ";
            int seciliIndex = comboBox1.SelectedIndex;


            comboBox1.Items.Clear();
            comboBox1.Items.Add("Tümü");

            //baglanti.Open();

            SqlCommand sqlKomutuc = new SqlCommand("up_kategoriListesi", baglanti.SQLBaglanti());
            sqlKomutuc.CommandType = CommandType.StoredProcedure;

            SqlDataReader okuyucu = sqlKomutuc.ExecuteReader();

            while (okuyucu.Read())
            {
                comboBox1.Items.Add(okuyucu["UrunTipi"].ToString()); //kategori listesinden kategoriler comboboxa eklenir

            }

            //baglanti.Close();

            comboBox1.SelectedIndex = seciliIndex;

            //baglanti.Open();

            if (comboBox1.SelectedIndex == 0) //combobox değeri 0 yani tümü ise tüm ürünler gösterilir
            {
                SqlCommand sqlKomutux = new SqlCommand("SELECT * FROM Urunler", baglanti.SQLBaglanti());
                SqlDataAdapter veriOkuyucux = new SqlDataAdapter(sqlKomutux);
                DataTable veriTablosux = new DataTable();
                veriOkuyucux.Fill(veriTablosux);
                dataGridView1.DataSource = veriTablosux;

                sqlKomutux.ExecuteNonQuery();
            }

            if (comboBox1.SelectedIndex > 0) //comboboxta bir değer seçili ise o kategoriye göre ürünler gösterilir
            {
                SqlCommand sqlKomutua = new SqlCommand("SELECT * FROM Urunler where UrunTipi=@urunTipi", baglanti.SQLBaglanti());
                sqlKomutua.Parameters.AddWithValue("@urunTipi", comboBox1.SelectedItem.ToString());
                SqlDataAdapter veriOkuyucua = new SqlDataAdapter(sqlKomutua);
                DataTable veriTablosua = new DataTable();
                veriOkuyucua.Fill(veriTablosua);
                dataGridView1.DataSource = veriTablosua;

                sqlKomutua.ExecuteNonQuery();
            }
            //baglanti.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //BU KISIMDA VİEW KULLANILARAK EN COK SATAN ÜRÜNLER GÖSTERİLİYOR    
            butonviewDurum = true;
            //baglanti.Open();
            SqlCommand sqlKomutu = new SqlCommand("SELECT * FROM EnCokSatanUrunler", baglanti.SQLBaglanti()); //encoksatanurunler viewi çalıştırılır
            SqlDataAdapter veriOkuyucu = new SqlDataAdapter(sqlKomutu);
            DataTable veriTablosu = new DataTable();
            veriOkuyucu.Fill(veriTablosu);
            dataGridView1.DataSource = veriTablosu; //en çok satan ürünler datagridview e yazdırılır

            sqlKomutu.ExecuteNonQuery();
            //baglanti.Close();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 form1a = (Form1)Application.OpenForms["Form1"]; //ana menü butonu ile form kapatılıp form1 açılır
            form1a.Show();
            baglanti.SQLBaglanti().Close();
        }
    }
}
