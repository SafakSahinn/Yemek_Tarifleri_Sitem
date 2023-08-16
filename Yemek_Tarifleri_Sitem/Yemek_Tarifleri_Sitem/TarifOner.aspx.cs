using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Yemek_Tarifleri_Sitem
{
    public partial class TarifOner : System.Web.UI.Page
    {
        sqlsinif bgl = new sqlsinif();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnTarifOner_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("INSERT INTO Tbl_Tarifler (TarifAd,TarifMalzeme,TarifYapilis,TarifResim,TarifSahip,TarifSahipMail) VALUES (@P1,@P2,@P3,@P4,@P5,@P6)",bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtTarifAd.Text);
            komut.Parameters.AddWithValue("@P2", TxtMalzemeler.Text);
            komut.Parameters.AddWithValue("@P3", TxtYapilis.Text);
            komut.Parameters.AddWithValue("@P4", FileUpload1.FileName);
            komut.Parameters.AddWithValue("@P5", TxtTarifiOneren.Text);
            komut.Parameters.AddWithValue("@P6", TxtMail.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            Response.Write("Tarifiniz kaydedildi");
        }
    }
}