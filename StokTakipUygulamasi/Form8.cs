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

    //SATICI PANELİ FORMU
    public partial class Form8 : Form
    {
        sqlbaglantisi baglanti = new sqlbaglantisi();

        //SqlConnection baglanti = new SqlConnection(@"Data Source=AZIZPC\SQLEXPRESS;Initial Catalog=stok_takip;Integrated Security=True");


        public string saticiAdi; //form4ten alınan satıcı bilgisi bu forda kullanılır
        public bool butonDurum=false;
        public Form8()
        {

            InitializeComponent();    

        }
        //SATICI PANELİ
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            //baglanti.Open();
            
            //bu kısımda checkbox değeri diğer satıcıların ürünlerini gösterir
            if (checkBox1.Checked == false) //checkbox false ise sadece giriş yapan satıcının ürünleri gözükür
            {
                SqlCommand sqlKomutu = new SqlCommand("SELECT * FROM URUNLER Where UrunTipi=@urunTipi and Satici=@satici", baglanti.SQLBaglanti());
                sqlKomutu.Parameters.AddWithValue("@urunTipi", comboBox1.SelectedItem.ToString());
                sqlKomutu.Parameters.AddWithValue("@satici", saticiAdi);
                //satıcı ve ürün tipine göre ürünler gösterilir

                SqlDataAdapter veriOkuyucu = new SqlDataAdapter(sqlKomutu);
                DataTable veriTablosu = new DataTable();
                veriOkuyucu.Fill(veriTablosu);
                dataGridView1.DataSource = veriTablosu;

                sqlKomutu.ExecuteNonQuery();

                if (comboBox1.SelectedIndex == 0) // comboboxta ürün tipi seçili ise ürün tipine göre ürünler gösteririr
                {
                    SqlCommand sqlKomutux = new SqlCommand("SELECT * FROM Urunler where Satici=@satici", baglanti.SQLBaglanti());
                    sqlKomutux.Parameters.AddWithValue("@satici", saticiAdi);
                    SqlDataAdapter veriOkuyucux = new SqlDataAdapter(sqlKomutux);
                    DataTable veriTablosux = new DataTable();
                    veriOkuyucux.Fill(veriTablosux);
                    dataGridView1.DataSource = veriTablosux;

                    sqlKomutu.ExecuteNonQuery();
                }
            }
            else //chechkbox true diğer satıcıların ürünleri de gösterilir
            {
                SqlCommand sqlKomutu = new SqlCommand("SELECT * FROM Urunler Where UrunTipi=@urunTipi", baglanti.SQLBaglanti());
                sqlKomutu.Parameters.AddWithValue("@urunTipi", comboBox1.SelectedItem.ToString());

                SqlDataAdapter veriOkuyucu = new SqlDataAdapter(sqlKomutu);
                DataTable veriTablosu = new DataTable();
                veriOkuyucu.Fill(veriTablosu);
                dataGridView1.DataSource = veriTablosu;

                sqlKomutu.ExecuteNonQuery();
                if (comboBox1.SelectedIndex == 0) //comboboxta bir ürün tipi seçili değilse (0) tüm ürünler gösterilir
                {
                    SqlCommand sqlKomutux = new SqlCommand("SELECT * FROM Urunler", baglanti.SQLBaglanti());
                    SqlDataAdapter veriOkuyucux = new SqlDataAdapter(sqlKomutux);
                    DataTable veriTablosux = new DataTable();
                    veriOkuyucux.Fill(veriTablosux);
                    dataGridView1.DataSource = veriTablosux;

                    sqlKomutu.ExecuteNonQuery();
                }
            }

            //baglanti.Close();

        }

        private void Form5_Load(object sender, EventArgs e)
        {
            //başlangıç
            comboBox1.Items.Clear(); //başlanıgçta combobox değerleri sıfırlanır
            comboBox1.Items.Add("Tümü"); //item sıfıra tümü değeri eklenir ve seçilir
            comboBox1.SelectedIndex = 0;
            //baglanti.Open();

            SqlCommand sqlKomutux = new SqlCommand("up_kategoriListesi", baglanti.SQLBaglanti());
            sqlKomutux.CommandType = CommandType.StoredProcedure;

            SqlDataReader okuyucu = sqlKomutux.ExecuteReader();

            while (okuyucu.Read())   //kategori listesi spsinden kategoriler alınıp comboboxa eklenir
            { 
                comboBox1.Items.Add(okuyucu["UrunTipi"].ToString());
            }

           // baglanti.Close();

            textBox9.Text = saticiAdi;
            label15.Text = saticiAdi + " Olarak Giriş Yaptınız."; //satıcı adı gözükür

           // baglanti.Open();


            if (checkBox1.Checked == false) //başlangıçta ürünler satıcıya göre gösterilir
            {
                SqlCommand sqlKomutu = new SqlCommand("SELECT * FROM Urunler Where Satici=@satici", baglanti.SQLBaglanti());
                sqlKomutu.Parameters.AddWithValue("@satici", saticiAdi);
                SqlDataAdapter veriOkuyucu = new SqlDataAdapter(sqlKomutu);
                DataTable veriTablosu = new DataTable();
                veriOkuyucu.Fill(veriTablosu);
                dataGridView1.DataSource = veriTablosu;

                sqlKomutu.ExecuteNonQuery();
            }
            if(checkBox1.Checked==true)
            {
                SqlCommand sqlKomutu = new SqlCommand("SELECT * FROM Urunler", baglanti.SQLBaglanti());

                SqlDataAdapter veriOkuyucu = new SqlDataAdapter(sqlKomutu);
                DataTable veriTablosu = new DataTable();
                veriOkuyucu.Fill(veriTablosu);
                dataGridView1.DataSource = veriTablosu;

                sqlKomutu.ExecuteNonQuery();
            }
            //baglanti.Close();

            Form1 form1 = (Form1)Application.OpenForms["Form1"];
            if (form1 != null)
            {
                         
                form1.Hide(); //form11 gizlenir
                
            }

            //baglanti.Open();

            //giriş yapan satıcının toplam kazancı fonksiyon kullanılarak gösterilir

            SqlCommand sqlKomutuq = new SqlCommand("SELECT dbo.ToplamKazancSatici(@satici)", baglanti.SQLBaglanti());
            sqlKomutuq.Parameters.AddWithValue("@satici", saticiAdi);
    
            var saticiUrunu = sqlKomutuq.ExecuteScalar();

            //label17.Text=saticiUrunu.ToString()+ " TL";
            
            //baglanti.Close();

        }

        private void Form5_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 form1a = (Form1)Application.OpenForms["Form1"];
            form1a.Show();  //bu form kapanınca form1 açılır
            baglanti.SQLBaglanti().Close();


        }

        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            label13.Text = "-";
            if (e.RowIndex == -1) //datagridviewde tıklanan yer geçersiz ise return eder
            {
                return;
            }
            if (butonDurum == false) //verilen siparişleri göster butonu aktif değilse çalışır
            {
                if ((dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString()) != saticiAdi)
                {
                    //satıcı olmadığımız ürünlerde işlem yapamayız bu yüzden textboxlar ve butonlar readonnly hale getirilir
                    label13.Text = "Satıcısı Olmadığınız Üründe İşlem Yapamazsınız!";
                    label13.ForeColor = Color.Red;

                    textBox2.ReadOnly = true;
                    textBox3.ReadOnly = true;
                    textBox4.ReadOnly = true;
                    textBox5.ReadOnly = true;
                    textBox6.ReadOnly = true;
                    textBox7.ReadOnly = true;
                    textBox8.ReadOnly = true;
                    textBox10.ReadOnly = true;
                    button1.Enabled = false;
                    button2.Enabled = false;
                    button2.Text = "Seçili Ürünü Sil";
                    button3.Enabled = false;
                    

                }
                else
                {
                    //satıcısı olduğumuz ürünleri seçersek readonly hale gelmez ve üründe düzenleme yapabiliriz
                    label13.Text = "-";
                    textBox2.ReadOnly = false;
                    textBox3.ReadOnly = false;
                    textBox4.ReadOnly = false;
                    textBox5.ReadOnly = false;
                    textBox6.ReadOnly = false;
                    textBox7.ReadOnly = false;
                    textBox8.ReadOnly = false;
                    textBox10.ReadOnly = false;
                    button1.Enabled = true;
                    button2.Enabled = true;
                    button2.Text = "Seçili Ürünü Sil";

                    button3.Enabled = true;
                    textBox10.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                    textBox7.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                    textBox8.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                }
            }
            else
            {
                //verilen sipraişleri göster butonu aktif ise sipraişler gösterilir
                label13.Text = "-";

                textBox2.ReadOnly = true;
                textBox3.ReadOnly = true;
                textBox4.ReadOnly = true;
                textBox5.ReadOnly = true;
                textBox6.ReadOnly = true;
                textBox7.ReadOnly = true;
                textBox8.ReadOnly = true;
                textBox10.ReadOnly = true;
                button1.Enabled = false;
                button2.Enabled = true;
                button2.Text = "Siparişi İptal Et";
                button3.Enabled = false;
                label5.Text = "SiparisID: ";
                label6.Text = "KullanıcıID: ";

                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                textBox9.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                textBox10.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //SqlCommand sqlEklemeKomutu = new SqlCommand("INSERT INTO Urunler(KategoriID,UrunTipi,Marka,Model,StokMiktari,Fiyat,UretimTarihi,Garanti,Satici) " +
            //    "VALUES (@ukid,@utip,@umarka,@umodel,@ustok,@ufiyat,@uuretim,@ugaranti,@usatici)",baglanti);
            

            //bu kısımda stored procedure kullanılarak ürün eklenir
            SqlCommand sqlEklemeKomutu = new SqlCommand("up_UrunEkle", baglanti.SQLBaglanti()); //ürün ekleme stored procedure
            sqlEklemeKomutu.CommandType = CommandType.StoredProcedure;

            //textbox değerleri özel karakter boşluk vs. değerlere karşı kontrol edilerek ekleme yapılır
            if (!(string.IsNullOrEmpty(textBox9.Text)) && (textBox2.Text.All(char.IsDigit)) && (textBox5.Text.All(char.IsDigit)) 
                && (textBox6.Text.All(char.IsDigit)) 
                && (textBox8.Text.All(char.IsDigit))&&(DateTime.TryParse(textBox7.Text,out DateTime result)))
            {
                //baglanti.Open();
                //storede procedure'larda tanımlanan parametrelere göre ürünler eklenir
                sqlEklemeKomutu.Parameters.AddWithValue("@ukid", textBox2.Text);
                sqlEklemeKomutu.Parameters.AddWithValue("@utip", textBox10.Text);
                sqlEklemeKomutu.Parameters.AddWithValue("@umarka", textBox3.Text);
                sqlEklemeKomutu.Parameters.AddWithValue("@umodel", textBox4.Text);
                sqlEklemeKomutu.Parameters.AddWithValue("@ustok", Convert.ToInt32(textBox5.Text));
                sqlEklemeKomutu.Parameters.AddWithValue("@ufiyat", Convert.ToInt32(textBox6.Text));
                sqlEklemeKomutu.Parameters.AddWithValue("@uuretim", Convert.ToDateTime(textBox7.Text));
                sqlEklemeKomutu.Parameters.AddWithValue("@ugaranti", Convert.ToInt32(textBox8.Text));
                sqlEklemeKomutu.Parameters.AddWithValue("@usatici", textBox9.Text);

                sqlEklemeKomutu.ExecuteNonQuery();
                label13.Text = "Ürün Ekleme Başarılı! Yenile Butonuna Basınız.";

                label13.ForeColor = Color.Green;
                button4.BackColor = Color.GreenYellow;

                
                //baglanti.Close();
            }
            else
            {
                MessageBox.Show("Ürün Ekleme Başarısız!\nGeçerli Değerler Giriniz.");
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //yenileme butonu --DAHA ÖNCEKİ FORMLARDA AÇIKLANDI
            butonDurum = false;
            label5.Text = "KategoriID: ";
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
                comboBox1.Items.Add(okuyucu["UrunTipi"].ToString());
               
            }

            //baglanti.Close();
            comboBox1.SelectedIndex = seciliIndex;

            //baglanti.Open();
            if (comboBox1.SelectedIndex == 0)
            {
                if (checkBox1.Checked == true)
                {
                    SqlCommand sqlKomutux = new SqlCommand("SELECT * FROM Urunler", baglanti.SQLBaglanti());
                    SqlDataAdapter veriOkuyucux = new SqlDataAdapter(sqlKomutux);
                    DataTable veriTablosux = new DataTable();
                    veriOkuyucux.Fill(veriTablosux);
                    dataGridView1.DataSource = veriTablosux;

                    sqlKomutux.ExecuteNonQuery();
                }
                if (checkBox1.Checked == false)
                {

                    SqlCommand sqlKomutu = new SqlCommand("SELECT * FROM Urunler Where Satici=@satici", baglanti.SQLBaglanti());
                    sqlKomutu.Parameters.AddWithValue("@satici", saticiAdi);

                    SqlDataAdapter veriOkuyucu = new SqlDataAdapter(sqlKomutu);
                    DataTable veriTablosu = new DataTable();
                    veriOkuyucu.Fill(veriTablosu);
                    dataGridView1.DataSource = veriTablosu;

                    sqlKomutu.ExecuteNonQuery();

                }
            }
            if(comboBox1.SelectedIndex>0)
            {
               
                if (checkBox1.Checked == true)
                {

                    SqlCommand sqlKomutua = new SqlCommand("SELECT * FROM Urunler where UrunTipi=@urunTipi", baglanti.SQLBaglanti());
                    sqlKomutua.Parameters.AddWithValue("@urunTipi", comboBox1.SelectedItem.ToString());
                    SqlDataAdapter veriOkuyucua = new SqlDataAdapter(sqlKomutua);
                    DataTable veriTablosua = new DataTable();
                    veriOkuyucua.Fill(veriTablosua);
                    dataGridView1.DataSource = veriTablosua;

                    sqlKomutua.ExecuteNonQuery();

                }
                if(checkBox1.Checked==false)
                {
                    SqlCommand sqlKomutu = new SqlCommand("SELECT * FROM Urunler Where UrunTipi=@urunTipi and Satici=@satici", baglanti.SQLBaglanti());
                    sqlKomutu.Parameters.AddWithValue("@urunTipi", comboBox1.SelectedItem.ToString());
                    sqlKomutu.Parameters.AddWithValue("@satici", saticiAdi);

                    SqlDataAdapter veriOkuyucu = new SqlDataAdapter(sqlKomutu);
                    DataTable veriTablosu = new DataTable();
                    veriOkuyucu.Fill(veriTablosu);
                    dataGridView1.DataSource = veriTablosu;

                    sqlKomutu.ExecuteNonQuery();

                }
            }
            //baglanti.Close();
            button4.BackColor = Color.White;
            label13.Text = "Yenileme Başarılı!";
                       
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //SqlCommand sqlEklemeKomutu = new SqlCommand("UPDATE Urunler SET KategoriID=@ukid,UrunTipi=@utip,Marka=@umarka,Model=@umodel,StokMiktari=@ustok," +
            //    "Fiyat=@ufiyat,UretimTarihi=@uuretim,Garanti=@ugaranti WHERE UrunID=@uid", baglanti);

            //Ürün güncelle stored procedure ile ürün güncelleme yapılır 
            SqlCommand sqlEklemeKomutu = new SqlCommand("up_UrunGuncelle", baglanti.SQLBaglanti()); //stored procedure çağrılır
            sqlEklemeKomutu.CommandType = CommandType.StoredProcedure;

            //textbox değerleri kontrol edilir
            if (!(string.IsNullOrEmpty(textBox9.Text)) && (textBox2.Text.All(char.IsDigit))&& (textBox5.Text.All(char.IsDigit)) 
                && (textBox6.Text.All(char.IsDigit))
                && (textBox8.Text.All(char.IsDigit)) && (DateTime.TryParse(textBox7.Text, out DateTime result)))
            {
                //baglanti.Open();
                sqlEklemeKomutu.Parameters.AddWithValue("@uid", textBox1.Text);
                sqlEklemeKomutu.Parameters.AddWithValue("@ukid", textBox2.Text);
                sqlEklemeKomutu.Parameters.AddWithValue("@utip", textBox10.Text);
                sqlEklemeKomutu.Parameters.AddWithValue("@umarka", textBox3.Text);
                sqlEklemeKomutu.Parameters.AddWithValue("@umodel", textBox4.Text);
                sqlEklemeKomutu.Parameters.AddWithValue("@ustok", Convert.ToInt32(textBox5.Text));
                sqlEklemeKomutu.Parameters.AddWithValue("@ufiyat", Convert.ToInt32(textBox6.Text));
                sqlEklemeKomutu.Parameters.AddWithValue("@uuretim", Convert.ToDateTime(textBox7.Text));
                sqlEklemeKomutu.Parameters.AddWithValue("@ugaranti", Convert.ToInt32(textBox8.Text));
                sqlEklemeKomutu.ExecuteNonQuery();

                label13.Text = "Güncelleme Başarılı! Yenile Butonuna Basınız.";
                label13.ForeColor = Color.Green;
                button4.BackColor = Color.GreenYellow;


                //baglanti.Close();
            }
            else
            {
                MessageBox.Show("Güncelleme Başarısız!\nGeçerli Değerler Giriniz.");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //temizle butonu textbox değerlerini temizler
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

        private void button2_Click(object sender, EventArgs e)
        {
            //silme butonu 
            if (butonDurum == false)//eğer siparişleri göster butonu aktif değilse ürünler tablosundan silme işlemi yapar
            {
                SqlCommand sqlEklemeKomutu = new SqlCommand("DELETE FROM Urunler WHERE UrunID=@uid", baglanti.SQLBaglanti());

                //baglanti.Open();
                sqlEklemeKomutu.Parameters.AddWithValue("@uid", textBox1.Text);
      
                sqlEklemeKomutu.ExecuteNonQuery();
                label13.Text = "Ürün Silme Başarılı! Yenile Butonuna Basınız.";
                label13.ForeColor = Color.Green;
                button4.BackColor = Color.GreenYellow;


                //baglanti.Close();
            }
            else //siparişleri göster butonu aktif ise siparişler tablosunadn sipraişi iptal eder
            {
                SqlCommand sqlEklemeKomutu = new SqlCommand("DELETE FROM Siparisler WHERE UrunID=@uid", baglanti.SQLBaglanti());
                //baglanti.Open();

                sqlEklemeKomutu.Parameters.AddWithValue("@uid", textBox1.Text);
              
                sqlEklemeKomutu.ExecuteNonQuery();
                label13.Text = "Sipariş İptali Başarılı! Yenile Butonuna Basınız.";
                label13.ForeColor = Color.Green;
                button4.BackColor = Color.GreenYellow;
                //baglanti.Close();
            }
            comboBox1.SelectedIndex = 0;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //checkbox değeri değiştiği zaman datagridviewi o anki buton durumuna göre yeniler
            label5.Text = "KategoriID: ";
            label6.Text = "Ürün Tipi: ";
            butonDurum = false;
            //baglanti.Open();
           
            if (comboBox1.SelectedIndex> 0)
            {

                if (checkBox1.Checked == false) //checkbox aktif değilse saticiya göre listelenir
                {
                    SqlCommand sqlKomutu = new SqlCommand("SELECT * FROM URUNLER Where UrunTipi=@urunTipi and Satici=@satici", baglanti.SQLBaglanti());
                
                    sqlKomutu.Parameters.AddWithValue("@urunTipi", comboBox1.SelectedItem.ToString());
                    sqlKomutu.Parameters.AddWithValue("@satici", saticiAdi);

                    SqlDataAdapter veriOkuyucu = new SqlDataAdapter(sqlKomutu);
                    DataTable veriTablosu = new DataTable();
                    veriOkuyucu.Fill(veriTablosu);
                    dataGridView1.DataSource = veriTablosu;

                    sqlKomutu.ExecuteNonQuery();
                }
                if (checkBox1.Checked == true)
                {
                    SqlCommand sqlKomutu = new SqlCommand("SELECT * FROM Urunler Where UrunTipi=@urunTipi", baglanti.SQLBaglanti());

                    sqlKomutu.Parameters.AddWithValue("@urunTipi", comboBox1.SelectedItem.ToString());

                    SqlDataAdapter veriOkuyucu = new SqlDataAdapter(sqlKomutu);
                    DataTable veriTablosu = new DataTable();
                    veriOkuyucu.Fill(veriTablosu);
                    dataGridView1.DataSource = veriTablosu;

                    sqlKomutu.ExecuteNonQuery();
                }
            }
            if (comboBox1.SelectedIndex == 0)
            {

                if (checkBox1.Checked == false)
                {
                    SqlCommand sqlKomutu = new SqlCommand("SELECT * FROM Urunler Where Satici=@satici", baglanti.SQLBaglanti());
                    sqlKomutu.Parameters.AddWithValue("@satici", saticiAdi);
                    SqlDataAdapter veriOkuyucu = new SqlDataAdapter(sqlKomutu);
                    DataTable veriTablosu = new DataTable();
                    veriOkuyucu.Fill(veriTablosu);
                    dataGridView1.DataSource = veriTablosu;

                    sqlKomutu.ExecuteNonQuery();
                }
                if (checkBox1.Checked == true)
                {
                   
                    SqlCommand sqlKomutu = new SqlCommand("SELECT * FROM Urunler", baglanti.SQLBaglanti());

                    SqlDataAdapter veriOkuyucu = new SqlDataAdapter(sqlKomutu);
                    DataTable veriTablosu = new DataTable();
                    veriOkuyucu.Fill(veriTablosu);
                    dataGridView1.DataSource = veriTablosu;

                    sqlKomutu.ExecuteNonQuery();


                }
            }


            //baglanti.Close();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            Form11 form11 = new Form11();  //ürün arama butonu kullanıldığında form11 açılır 
            form11.ShowDialog();
            dataGridView1.DataSource = form11.veriYeni; //form11den veriler bu forma çekilir
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //verilen sipraişleri gösterme butonu 
            butonDurum = true; //butonun kullanıldığını gösterir
            //baglanti.Open();
            //satıcıya göre siparişleri listeler
            SqlCommand sqlKomutu = new SqlCommand("SELECT * FROM Siparisler Where Satici=@satici", baglanti.SQLBaglanti());
            sqlKomutu.Parameters.AddWithValue("@satici", saticiAdi); //satıcı adı parametre olarak alınır
            SqlDataAdapter veriOkuyucu = new SqlDataAdapter(sqlKomutu);
            DataTable veriTablosu = new DataTable();
            veriOkuyucu.Fill(veriTablosu);
            dataGridView1.DataSource = veriTablosu;

            sqlKomutu.ExecuteNonQuery();
            //baglanti.Close();

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            this.Close();  //ana menü butonuna basıldıgında bu form kapatılarak form1 açılır
            Form1 form1a = (Form1)Application.OpenForms["Form1"];
            form1a.Show();
            baglanti.SQLBaglanti().Close();



        }
    }
}
