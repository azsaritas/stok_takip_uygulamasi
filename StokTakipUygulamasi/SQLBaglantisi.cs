using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    //SqlConnection baglanti = new SqlConnection(@"Data Source=AZIZPC\SQLEXPRESS;Initial Catalog=stok_takip;Integrated Security=True");


    //BU KISIMDA KULLANILACAK BİLGİSYARIN SQL BAĞLANTI YOLU VERİLMELİDİR

    class sqlbaglantisi
    {
        public SqlConnection SQLBaglanti()
        {
            //BAĞLANTI YOLUNUN YAZILACAĞI YER
            SqlConnection baglanti = new SqlConnection(@"Data Source=AZIZPC\SQLEXPRESS;Initial Catalog=stok_takip;Integrated Security=True");
            baglanti.Open();
            return baglanti;
        }

    }

 

}
