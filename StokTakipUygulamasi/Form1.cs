//Aziz Sarıtaş
//210757061
//Bilgisayar Mühendisliği


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
   //ANA MENÜ FORMU
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 kullaniciGirisi = new Form2();  //kullanıcı girişi için form2 açılır
            kullaniciGirisi.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            Form3 adminGirisi = new Form3(); //admin girişi için form3 açılır
            adminGirisi.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4 saticiGirisi = new Form4(); //satıcı girişi için form4 açılır
            saticiGirisi.Show();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit(); //uygulama tamamen kapatılır
        }
    }
}
