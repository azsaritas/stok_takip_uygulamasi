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

namespace WindowsFormsApp1
{
    //ADMİN PANELİ FORMU

    public partial class Form9 : Form 
    {
        sqlbaglantisi baglanti = new sqlbaglantisi();

        //SqlConnection baglanti = new SqlConnection(@"Data Source=AZIZPC\SQLEXPRESS;Initial Catalog=stok_takip;Integrated Security=True");
        //SqlConnection baglanti = new SqlConnection("Server = .;Database=stok_takip;Integrated Security=True");

        public Form9()
        {

            InitializeComponent();
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            label2.Visible = false;
            comboBox1.Visible = false; 
        }
        //ADMİN PANELİ
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {   
            //baglanti.Open();
            //combobox değerine göre ürünleri listeler
            if (comboBox1.SelectedIndex == 0) //0 yani tümü seçili ise tüm ürünleri gösterir
            {
                SqlCommand sqlKomutu = new SqlCommand("SELECT * FROM Urunler", baglanti.SQLBaglanti());
                SqlDataAdapter veriOkuyucu = new SqlDataAdapter(sqlKomutu);
                DataTable veriTablosu = new DataTable();
                veriOkuyucu.Fill(veriTablosu);
                dataGridView1.DataSource = veriTablosu;
                sqlKomutu.ExecuteNonQuery();
            }
            else  // değilse ürün tipine göre ürünleri gösterir
            {
                SqlCommand sqlKomutu = new SqlCommand("SELECT * FROM Urunler Where UrunTipi=@urunTipi", baglanti.SQLBaglanti());
                sqlKomutu.Parameters.AddWithValue("@urunTipi", comboBox1.SelectedItem.ToString());
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
            button6.PerformClick(); //başlangıçta button6 basılı hale gelir
            button6.BackColor = Color.GreenYellow;

            Form1 form1 = (Form1)Application.OpenForms["Form1"];
            if (form1 != null)
            {
                         
                form1.Hide(); //form1 gizlenir
                
            }

        }

        private void Form5_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 form1a = (Form1)Application.OpenForms["Form1"];
            form1a.Show();       //bu form kapatıldğında form1 açılır


        }

        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1) //datagridviewde geçersiz yere tıklanırsa return eder
            {
                return;
            }
            if (panel1.Visible && panel2.Visible==false && panel3.Visible==false) //panel1 aktif (ürün işlemleri)
                //kullanılan panellerin durumuna göre datagridviewden textboxlara gidecek değerleri ayarlar
            {
                textBox18.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                textBox7.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                textBox8.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                textBox19.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();

            }
            if (panel1.Visible && panel2.Visible && panel3.Visible == false) //panel 2 aktif (kullanıcı işlemleri)
            {
                textBox9.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBox10.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox11.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBox17.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                textBox14.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                textBox12.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                textBox13.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                textBox15.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                textBox16.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
            }
            if (panel1.Visible && panel2.Visible == false && panel3.Visible) //panel 3 aktif (satıcı paneli)
            {
                textBox26.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBox25.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox24.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBox23.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                textBox22.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
               
            }

        }
        public bool bosMuDoluMu(params string[] textler)
        {
            bool durum = true; //girilen textboxların boş mu dolu mu olduğunu kontrol eden fonksiyon
            for (int i = 0; i < textler.Count(); i++)
            {

                if ((string.IsNullOrEmpty(textler[i])))
                {
                   return false;

                }
                else
                {
                    durum = true;

                }
            }
            return durum;

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //SqlCommand sqlEklemeKomutu = new SqlCommand("INSERT INTO Urunler(KategoriID,UrunTipi,Marka,Model,StokMiktari,Fiyat,UretimTarihi,Garanti,Satici) " +
            //    "VALUES (@ukid,@utip,@umarka,@umodel,@ustok,@ufiyat,@uuretim,@ugaranti,@usatici)",baglanti);

            //ürün ekleme stored procedure kullanılarak ürün eklenir
            //BU KISIM DAHA ÖNCEKİ FORMLARDA AÇIKLANDI
            SqlCommand sqlEklemeKomutu = new SqlCommand("up_UrunEkle", baglanti.SQLBaglanti());
            sqlEklemeKomutu.CommandType = CommandType.StoredProcedure;

            bool bosDurumu=(bosMuDoluMu(textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text, textBox8.Text,textBox19.Text,textBox18.Text));

            if (bosDurumu && (textBox2.Text.All(char.IsDigit)) && (textBox5.Text.All(char.IsDigit)) && (textBox6.Text.All(char.IsDigit))
                && (textBox8.Text.All(char.IsDigit)) && (DateTime.TryParse(textBox7.Text, out DateTime result)))
            {
                //baglanti.Open();

                sqlEklemeKomutu.Parameters.AddWithValue("@ukid", textBox2.Text);
                sqlEklemeKomutu.Parameters.AddWithValue("@utip", textBox18.Text);
                sqlEklemeKomutu.Parameters.AddWithValue("@umarka", textBox3.Text);
                sqlEklemeKomutu.Parameters.AddWithValue("@umodel", textBox4.Text);
                sqlEklemeKomutu.Parameters.AddWithValue("@ustok", Convert.ToInt32(textBox5.Text));
                sqlEklemeKomutu.Parameters.AddWithValue("@ufiyat", Convert.ToInt32(textBox6.Text));
                sqlEklemeKomutu.Parameters.AddWithValue("@uuretim", Convert.ToDateTime(textBox7.Text));
                sqlEklemeKomutu.Parameters.AddWithValue("@ugaranti", Convert.ToInt32(textBox8.Text));
                sqlEklemeKomutu.Parameters.AddWithValue("@usatici", textBox19.Text);

                sqlEklemeKomutu.ExecuteNonQuery();
                label13.Text = "Ürün Ekleme Başarılı! Yenile Butonuna Basınız.";
                label13.ForeColor = Color.Green;
                button4.BackColor = Color.GreenYellow;
               // baglanti.Close();
            }
            else
            {
                MessageBox.Show("Ürün Ekleme Başarısız!\nGeçerli Değerler Giriniz.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //yenile butonu --BU KISIM DAHA ÖNCEKİ FORMLARDA AÇIKLANDI
            int seciliIndex = comboBox1.SelectedIndex;


            comboBox1.Items.Clear();
            comboBox1.Items.Add("Tümü");

            //baglanti.Open();

            SqlCommand sqlKomutuc = new SqlCommand("up_kategoriListesi", baglanti.SQLBaglanti());
            sqlKomutuc.CommandType = CommandType.StoredProcedure;

            SqlDataReader okuyucu = sqlKomutuc.ExecuteReader();

            while (okuyucu.Read())
            {
                comboBox1.Items.Add(okuyucu["UrunTipi"].ToString());

            }

            //baglanti.Close();
            comboBox1.SelectedIndex = seciliIndex;
            //baglanti.Open();
           
            if (comboBox1.SelectedIndex==0)
            {
                
                SqlCommand sqlKomutu = new SqlCommand("SELECT * FROM URUNLER ", baglanti.SQLBaglanti());
                SqlDataAdapter veriOkuyucu = new SqlDataAdapter(sqlKomutu);
                DataTable veriTablosu = new DataTable();
                veriOkuyucu.Fill(veriTablosu);
                dataGridView1.DataSource = veriTablosu;

                sqlKomutu.ExecuteNonQuery();
            }
            else
            {
                SqlCommand sqlKomutu = new SqlCommand("SELECT * FROM URUNLER Where UrunTipi=@urunTipi", baglanti.SQLBaglanti());
                sqlKomutu.Parameters.AddWithValue("@urunTipi", comboBox1.SelectedItem.ToString());
                SqlDataAdapter veriOkuyucu = new SqlDataAdapter(sqlKomutu);
                DataTable veriTablosu = new DataTable();
                veriOkuyucu.Fill(veriTablosu);
                dataGridView1.DataSource = veriTablosu;

                sqlKomutu.ExecuteNonQuery();
            }
            //baglanti.Close();
            label13.Text = "Yenileme Başarılı!";
            label13.ForeColor = Color.Green;
            button4.BackColor = Color.White;

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            //SqlCommand sqlEklemeKomutu = new SqlCommand("UPDATE Urunler SET KategoriID=@ukid,UrunTipi=@utip,Marka=@umarka,Model=@umodel,StokMiktari=@ustok," +
            //    "Fiyat=@ufiyat,UretimTarihi=@uuretim,Garanti=@ugaranti,Satici=@usatici WHERE UrunID=@uid", baglanti);

            //stored procedure ile ürün güncelleme 
            //BU KISIM DAHA ÖNCEKİ FORMLARDA AÇIKLANDI
            SqlCommand sqlEklemeKomutu = new SqlCommand("up_UrunGuncelle", baglanti.SQLBaglanti()); //Ürün güncellemek için stored procedure kullanılıd
            sqlEklemeKomutu.CommandType = CommandType.StoredProcedure;

            bool bosDurumu = (bosMuDoluMu(textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text,
                textBox6.Text, textBox7.Text, textBox8.Text,textBox19.Text,textBox18.Text)); //bu fonksiyon textboxa girilen değerlerin boş olup olmadığını kontrol eder
            //Aziz Sarıtaş
            if (bosDurumu && (textBox2.Text.All(char.IsDigit)) && (textBox5.Text.All(char.IsDigit)) && (textBox6.Text.All(char.IsDigit))
                            && (textBox8.Text.All(char.IsDigit)) && (DateTime.TryParse(textBox7.Text, out DateTime result)))
            {      //textbox değerleri kontrol edilir değerler geçerliyese devreeye girer
               // baglanti.Open(); 
                sqlEklemeKomutu.Parameters.AddWithValue("@uid", textBox1.Text); //ürün id değeri
                sqlEklemeKomutu.Parameters.AddWithValue("@ukid", textBox2.Text); //kategori id değeri
                sqlEklemeKomutu.Parameters.AddWithValue("@utip", textBox18.Text); //ürün tipi
                sqlEklemeKomutu.Parameters.AddWithValue("@umarka", textBox3.Text); //marka
                sqlEklemeKomutu.Parameters.AddWithValue("@umodel", textBox4.Text); //model
                sqlEklemeKomutu.Parameters.AddWithValue("@ustok", Convert.ToInt32(textBox5.Text)); //stok miktari
                sqlEklemeKomutu.Parameters.AddWithValue("@ufiyat", Convert.ToInt32(textBox6.Text)); //fiyat
                sqlEklemeKomutu.Parameters.AddWithValue("@uuretim", Convert.ToDateTime(textBox7.Text)); //üretim tarihi
                sqlEklemeKomutu.Parameters.AddWithValue("@ugaranti", Convert.ToInt32(textBox8.Text)); //garanti süresi
                //sqlEklemeKomutu.Parameters.AddWithValue("@usatici", textBox19.Text); //satıcı bilgisi

                sqlEklemeKomutu.ExecuteNonQuery();

                label13.Text = "Güncelleme Başarılı! Yenile Butonuna Basınız.";
                label13.ForeColor = Color.Green;
                button4.BackColor = Color.GreenYellow;
                

               // baglanti.Close();
            }
            else
            {
                MessageBox.Show("Güncelleme Başarısız!\nGeçerli Değerler Giriniz.");
            }
        }
        
        private void button5_Click_1(object sender, EventArgs e)
        {
            //temizleme butonu textbox değerlerini temizler
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox18.Text = "";
            label13.Text = "Temizleme Başarılı!";
            label13.ForeColor = Color.Green;


        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            //ürünleri ürün ide göre siler 
            //BU KISIM DAHA ÖNCEKİ FORMLARDA AÇIKLANDI
            SqlCommand sqlEklemeKomutu = new SqlCommand("DELETE FROM Urunler WHERE UrunID=@uid", baglanti.SQLBaglanti());

            //baglanti.Open();
            sqlEklemeKomutu.Parameters.AddWithValue("@uid", textBox1.Text);

            sqlEklemeKomutu.ExecuteNonQuery();
            label13.Text = "Ürün Silme Başarılı! Yenile Butonuna Basınız.";
            label13.ForeColor = Color.Green;
            button4.BackColor = Color.GreenYellow;

            //baglanti.Close();
            comboBox1.SelectedIndex = 0;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            //Ürün işlemleri butonu
            button6.BackColor = Color.GreenYellow; //tıklanan buton rengi değişir
            button7.BackColor = Color.White;
            button8.BackColor = Color.White;
            button22.BackColor = Color.White; 

            panel1.Visible = true; //panel 1 ürün işlemleri aktif olur diğer paneller gözükmez
            panel2.Visible = false; 
            panel3.Visible = false;
            label2.Visible = true;
            comboBox1.Visible = true; //combobox görünür hale gelir

            comboBox1.Items.Clear();
            comboBox1.Items.Add("Tümü");
            comboBox1.SelectedIndex = 0;

            //baglanti.Open();

            SqlCommand sqlKomutux = new SqlCommand("up_kategoriListesi", baglanti.SQLBaglanti());
            sqlKomutux.CommandType = CommandType.StoredProcedure;

            SqlDataReader okuyucu = sqlKomutux.ExecuteReader();

            while (okuyucu.Read())
            {
                comboBox1.Items.Add(okuyucu["UrunTipi"].ToString()); //comboboxa kategori listesi yüklenir
            }
            //baglanti.Close();

            //baglanti.Open();

            //ürünler gösterilir

            SqlCommand sqlKomutu = new SqlCommand("SELECT * FROM Urunler", baglanti.SQLBaglanti());
            SqlDataAdapter veriOkuyucu = new SqlDataAdapter(sqlKomutu);
            DataTable veriTablosu = new DataTable();
            veriOkuyucu.Fill(veriTablosu);
            dataGridView1.DataSource = veriTablosu;

            sqlKomutu.ExecuteNonQuery();

            //baglanti.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //kullanıcı işlemleri butonu
            button6.BackColor = Color.White;
            button7.BackColor = Color.GreenYellow;
            button8.BackColor = Color.White;
            button22.BackColor = Color.White;

            panel1.Visible=true;  //panel 2 kullanıcı işlemleri aktif hale getirilr
            panel2.Visible = true;
            panel3.Visible = false;
            label2.Visible = false;
            comboBox1.Visible = false;

            //baglanti.Open();

            //kullanıcılar gösterilir

            SqlCommand sqlKomutu2 = new SqlCommand("SELECT * FROM Kullanicilar", baglanti.SQLBaglanti());
            SqlDataAdapter veriOkuyucu2 = new SqlDataAdapter(sqlKomutu2);
            DataTable veriTablosu2 = new DataTable();
            veriOkuyucu2.Fill(veriTablosu2);
            dataGridView1.DataSource = veriTablosu2;

            sqlKomutu2.ExecuteNonQuery();

            //baglanti.Close();
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            //SqlCommand sqlEklemeKomutu = new SqlCommand("INSERT INTO Kullanicilar(Ad,Soyad,Sifre,KayitTarihi,Yas,Cinsiyet,Eposta,Telefon) " +
            //    "VALUES (@kad,@ksoyad,@ksifre,@kkayit,@kyas,@kcinsiyet,@keposta,@ktelefon)", baglanti);

            //stored procedure ile kullanıcı ekleme 

            SqlCommand sqlEklemeKomutu = new SqlCommand("up_KullaniciEkle", baglanti.SQLBaglanti()); //kullanıcı ekle stored procedure çalıştırılır
            sqlEklemeKomutu.CommandType = CommandType.StoredProcedure;

            //textbox değerleriin boş dolu olup olmadığı kontrol edilir

            bool bosDurumu = (bosMuDoluMu(textBox10.Text, textBox11.Text, textBox17.Text, textBox14.Text, textBox12.Text,
                textBox13.Text, textBox15.Text,textBox16.Text));

            if (bosDurumu && (textBox12.Text.All(char.IsDigit)) && (DateTime.TryParse(textBox14.Text, out DateTime result))
                &&(textBox16.Text.All(char.IsDigit)))
            {
                //baglanti.Open();

                //parameterelere göre kullanıcı tablosuna kullanıcı eklenir

                sqlEklemeKomutu.Parameters.AddWithValue("@kad", textBox10.Text.Trim());
                sqlEklemeKomutu.Parameters.AddWithValue("@ksoyad", textBox11.Text.Trim());
                sqlEklemeKomutu.Parameters.AddWithValue("@ksifre", textBox17.Text.Trim());
                sqlEklemeKomutu.Parameters.AddWithValue("@kkayit", Convert.ToDateTime(textBox14.Text));
                sqlEklemeKomutu.Parameters.AddWithValue("@kyas", Convert.ToInt32(textBox12.Text));
                sqlEklemeKomutu.Parameters.AddWithValue("@kcinsiyet", textBox13.Text.Trim());
                sqlEklemeKomutu.Parameters.AddWithValue("@keposta", textBox15.Text.Trim());
                sqlEklemeKomutu.Parameters.AddWithValue("@ktelefon", textBox16.Text.Trim());

                sqlEklemeKomutu.ExecuteNonQuery();
                label24.Text = "Kullanıcı Ekleme Başarılı! Yenile Butonuna Basınız.";

                label24.ForeColor = Color.Green;
                button10.BackColor = Color.GreenYellow; 


                //baglanti.Close();
            }
            else
            {
                MessageBox.Show("Kullanıcı Ekleme Başarısız!\nGeçerli Değerler Giriniz.");
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //SqlCommand sqlEklemeKomutu = new SqlCommand("UPDATE Kullanicilar SET Ad=@kad,Soyad=@ksoyad,Sifre=@ksifre," +
            //    "KayitTarihi=@kkayit,Yas=@kyas,Cinsiyet=@kcinsiyet,Eposta=@keposta,Telefon=@ktelefon WHERE KullaniciID=@kid ",baglanti);

            //stored procedure ile kullanıcı güncelleme 

            SqlCommand sqlEklemeKomutu = new SqlCommand("up_KullaniciGuncelle", baglanti.SQLBaglanti());//kullanıcı güncelle stored procedure
            sqlEklemeKomutu.CommandType = CommandType.StoredProcedure;

            //textbox değerleri kontrol edilir
            bool bosDurumu = (bosMuDoluMu(textBox10.Text, textBox11.Text, textBox17.Text, textBox14.Text, textBox12.Text,
                textBox13.Text, textBox15.Text, textBox16.Text));

            if (bosDurumu && (textBox12.Text.All(char.IsDigit)) && (DateTime.TryParse(textBox14.Text, out DateTime result)) 
                && (textBox16.Text.All(char.IsDigit)))
            {
                //baglanti.Open();

                //parametrelere göre kullanıcı tablosundaki kullanıcılar güncellenir
                sqlEklemeKomutu.Parameters.AddWithValue("@kid", textBox9.Text.Trim());
                sqlEklemeKomutu.Parameters.AddWithValue("@kad", textBox10.Text.Trim());
                sqlEklemeKomutu.Parameters.AddWithValue("@ksoyad", textBox11.Text.Trim());
                sqlEklemeKomutu.Parameters.AddWithValue("@ksifre", textBox17.Text.Trim());
                sqlEklemeKomutu.Parameters.AddWithValue("@kkayit", Convert.ToDateTime(textBox14.Text));
                sqlEklemeKomutu.Parameters.AddWithValue("@kyas", Convert.ToInt32(textBox12.Text));
                sqlEklemeKomutu.Parameters.AddWithValue("@kcinsiyet", textBox13.Text.Trim());
                sqlEklemeKomutu.Parameters.AddWithValue("@keposta", textBox15.Text.Trim());
                sqlEklemeKomutu.Parameters.AddWithValue("@ktelefon", textBox16.Text.Trim());

                sqlEklemeKomutu.ExecuteNonQuery();
                label24.Text = "Güncelleme Başarılı! Yenile Butonuna Basınız.";
                label24.ForeColor = Color.Green;
                button10.BackColor = Color.GreenYellow;

                //baglanti.Close();
            }
            else
            {
                MessageBox.Show("Kullanıcı Güncelleme Başarısız!\nGeçerli Değerler Giriniz.");
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            //kullanıcı silme işlemi
            SqlCommand sqlEklemeKomutu = new SqlCommand("DELETE FROM Kullanicilar WHERE KullaniciID=@kid ", baglanti.SQLBaglanti());

            //baglanti.Open();

            sqlEklemeKomutu.Parameters.AddWithValue("@kid", textBox9.Text); //kullanıcı ide göre kullanıcı silinir

            sqlEklemeKomutu.ExecuteNonQuery();
            label24.Text = "Kullanıcı Silme Başarılı! Yenile Butonuna Basınız.";
            label24.ForeColor = Color.Green;
            button10.BackColor = Color.GreenYellow;


            //baglanti.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //temizle butonu textbox değerlerini temizler
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            textBox13.Text = "";
            textBox14.Text = "";
            textBox15.Text = "";
            textBox16.Text = "";
            textBox17.Text = "";
            label24.Text = "Temizleme Başarılı!";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //yenile butonu kullanıcıları yeniler
            //baglanti.Open();
            
            SqlCommand sqlKomutu = new SqlCommand("SELECT * FROM Kullanicilar", baglanti.SQLBaglanti());
            SqlDataAdapter veriOkuyucu = new SqlDataAdapter(sqlKomutu);
            DataTable veriTablosu = new DataTable();
            veriOkuyucu.Fill(veriTablosu);
            dataGridView1.DataSource = veriTablosu;

            sqlKomutu.ExecuteNonQuery();

            //baglanti.Close();
            button10.BackColor = Color.White;
            label24.Text = "Yenileme Başarılı!";
            

        }

        private void button8_Click(object sender, EventArgs e)
        {
            //satıcı işlemleri butonu 
            button6.BackColor = Color.White;
            button7.BackColor = Color.White;
            button8.BackColor = Color.GreenYellow;
            button22.BackColor= Color.White;

            panel1.Visible = true;
            panel2.Visible = false;
            panel3.Visible = true; //panel 3 satıcı işlemleri aktif hale gelir
            label2.Visible = false;
            comboBox1.Visible = false; //combobox gizlenir

            //baglanti.Open();

            //satıcılar tablosundan satıcı listesi çekilir

            SqlCommand sqlKomutu3 = new SqlCommand("SELECT * FROM Saticilar", baglanti.SQLBaglanti());
            SqlDataAdapter veriOkuyucu3 = new SqlDataAdapter(sqlKomutu3);
            DataTable veriTablosu3 = new DataTable();
            veriOkuyucu3.Fill(veriTablosu3);
            dataGridView1.DataSource = veriTablosu3;

            sqlKomutu3.ExecuteNonQuery();

            //baglanti.Close();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            //SqlCommand sqlEklemeKomutu = new SqlCommand("INSERT INTO Saticilar(SaticiMagazaAdi,SaticiEposta,SaticiTelefon,SaticiSifre) " +
            //    "VALUES (@sad,@seposta,@stelefon,@sifre)", baglanti);

            //stored procedure ile satıcı eklenir
            SqlCommand sqlEklemeKomutu = new SqlCommand("up_SaticiEkle", baglanti.SQLBaglanti()); //satici ekle stored procedure
            sqlEklemeKomutu.CommandType = CommandType.StoredProcedure;

            //textboxx değerleri kontrol edilir
            bool bosDurumu = (bosMuDoluMu(textBox25.Text, textBox24.Text, textBox23.Text, textBox22.Text));

            if (bosDurumu && (textBox23.Text.All(char.IsDigit)))
            {
                //baglanti.Open();

                //parametrelere göre satıcılar tablosuna satıcılar eklenir

                sqlEklemeKomutu.Parameters.AddWithValue("@sad", textBox25.Text);
                sqlEklemeKomutu.Parameters.AddWithValue("@seposta", textBox24.Text);
                sqlEklemeKomutu.Parameters.AddWithValue("@stelefon", textBox23.Text);
                sqlEklemeKomutu.Parameters.AddWithValue("@sifre", textBox22.Text);

                sqlEklemeKomutu.ExecuteNonQuery();
                label25.Text = "Satıcı Ekleme Başarılı! Yenile Butonuna Basınız.";
                label25.ForeColor = Color.Green;
                button15.BackColor = Color.GreenYellow;
                //baglanti.Close();
            }
            else
            {
                MessageBox.Show("Satıcı Ekleme Başarısız!\nGeçerli Değerler Giriniz.");
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            //SqlCommand sqlEklemeKomutu = new SqlCommand("UPDATE Saticilar SET SaticiMagazaAdi=@sad," +
            //    "SaticiEposta=@seposta,SaticiTelefon=@stelefon,SaticiSifre=@sifre WHERE SaticiID=@sid ", baglanti);

            //stored procedure ile satıcı güncelleme 
            SqlCommand sqlEklemeKomutu = new SqlCommand("up_SaticiGuncelle", baglanti.SQLBaglanti());//satıcı güncelle stored procedure
            sqlEklemeKomutu.CommandType = CommandType.StoredProcedure;

            //textboxlara girilen değerlerin kotnrolü yapılır
            bool bosDurumu = (bosMuDoluMu(textBox25.Text, textBox24.Text, textBox23.Text, textBox22.Text));

            if (bosDurumu && (textBox23.Text.All(char.IsDigit)))
            {
                //baglanti.Open();

                //parametrelere göre satıcılar listesindeki satıcıalr güncellenir
                sqlEklemeKomutu.Parameters.AddWithValue("@sid", textBox26.Text);
                sqlEklemeKomutu.Parameters.AddWithValue("@sad", textBox25.Text);
                sqlEklemeKomutu.Parameters.AddWithValue("@seposta", textBox24.Text);
                sqlEklemeKomutu.Parameters.AddWithValue("@stelefon", textBox23.Text);
                sqlEklemeKomutu.Parameters.AddWithValue("@sifre", textBox22.Text);

                sqlEklemeKomutu.ExecuteNonQuery();
                label25.Text = "Güncelleme Başarılı! Yenile Butonuna Basınız.";
                label25.ForeColor = Color.Green;
                button15.BackColor = Color.GreenYellow;


                //baglanti.Close();
            }
            else
            {
                MessageBox.Show("Satıcı Güncelleme Başarısız!\nGeçerli Değerler Giriniz.");
            }
        }
        private void button18_Click(object sender, EventArgs e)
        {
            //satıcı silme işlemi
            SqlCommand sqlEklemeKomutu = new SqlCommand("DELETE FROM Saticilar WHERE SaticiID=@sid ", baglanti.SQLBaglanti());

            //baglanti.Open();

            sqlEklemeKomutu.Parameters.AddWithValue("@sid", textBox26.Text); //satıcı id e göre satıcı silinir
           
            sqlEklemeKomutu.ExecuteNonQuery();
            label25.Text = "Satıcı Silme Başarılı! Yenile Butonuna Basınız.";
            label25.ForeColor = Color.Green;
            button15.BackColor = Color.GreenYellow;

            //baglanti.Close();

        }
        private void button20_Click(object sender, EventArgs e)
        {
            //textboxtaki değerlere göre satıcı araması yapılır
            SqlCommand sqlAramaKomutu = new SqlCommand("SELECT * FROM Saticilar WHERE SaticiID=@sid or SaticiMagazaAdi=@sad or SaticiEposta=@seposta or SaticiTelefon=@stelefon", baglanti.SQLBaglanti());
           // baglanti.Open();

            sqlAramaKomutu.Parameters.AddWithValue("@sid", textBox26.Text);
            sqlAramaKomutu.Parameters.AddWithValue("@sad", textBox25.Text);
            sqlAramaKomutu.Parameters.AddWithValue("@seposta", textBox24.Text);
            sqlAramaKomutu.Parameters.AddWithValue("@stelefon", textBox23.Text);
    
            SqlDataAdapter veriOkuyucu = new SqlDataAdapter(sqlAramaKomutu);
            DataTable veriTablosu = new DataTable();
            veriOkuyucu.Fill(veriTablosu);
            dataGridView1.DataSource = veriTablosu;

            sqlAramaKomutu.ExecuteNonQuery();
           // baglanti.Close();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            //satıcı panelindeki yenileme butonu
            //baglanti.Open();

            //satıcılar tablosundan satıcılar çekilir
            SqlCommand sqlKomutu = new SqlCommand("SELECT * FROM Saticilar", baglanti.SQLBaglanti());
            SqlDataAdapter veriOkuyucu = new SqlDataAdapter(sqlKomutu);
            DataTable veriTablosu = new DataTable();
            veriOkuyucu.Fill(veriTablosu);
            dataGridView1.DataSource = veriTablosu;

            sqlKomutu.ExecuteNonQuery();

            //baglanti.Close();
            button15.BackColor = Color.White;

        }

        private void button14_Click(object sender, EventArgs e)
        {
            //satıcı panelindeki temizle butonu textbox değerlerini temizler
            textBox26.Text = "";
            textBox25.Text = "";
            textBox24.Text = "";
            textBox23.Text = "";
            textBox22.Text = "";
            label25.Text = "Temizleme Başarılı!";
            label25.ForeColor = Color.Green;

        }

        private void button21_Click(object sender, EventArgs e)
        {
            //arama butonu form10 u açar
            Form10 form10=new Form10();
            form10.ShowDialog();
            dataGridView1.DataSource = form10.veriYeni;
            
        }

        private void button19_Click(object sender, EventArgs e)
        {
            //arama butonu form11 i açar
            Form11 form11 = new Form11();
            form11.ShowDialog();
            dataGridView1.DataSource = form11.veriYeni;
        }

        private void button22_Click(object sender, EventArgs e)
        {
            //istatistikler butonu form12 yi açar
            button6.BackColor = Color.White;
            button7.BackColor = Color.White;
            button8.BackColor = Color.White;
            button22.BackColor = Color.GreenYellow;
            Form12 form12=new Form12();
            form12.ShowDialog();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            //olay günlükleri butonu form13 ü açar
            Form13 form13 = new Form13();
            form13.ShowDialog();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            //ana menü butonu bu formu kapatıp form1 i açar
            this.Close();
            Form1 form1a = (Form1)Application.OpenForms["Form1"];
            form1a.Show();
            baglanti.SQLBaglanti().Close();
        }
    }
}
