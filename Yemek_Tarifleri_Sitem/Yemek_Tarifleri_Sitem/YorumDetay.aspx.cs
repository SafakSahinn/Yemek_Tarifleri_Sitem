using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Yemek_Tarifleri_Sitem
{
    public partial class YorumDetay : System.Web.UI.Page
    {
        sqlsinif bgl = new sqlsinif();
        string id;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Request.QueryString["Yorumid"]);

            if (Page.IsPostBack == false)
            {
                SqlCommand komut = new SqlCommand("SELECT YorumAdSoyad,YorumMail,Yorumicerik,YemekAd FROM Tbl_yorumlar INNER JOIN Tbl_Yemekler ON Tbl_yorumlar.yemekid=Tbl_yemekler.yemekid WHERE yorumid=@P1", bgl.baglanti());
                komut.Parameters.AddWithValue("@P1", (id > 0 ? id : 0));
                SqlDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    TxtAd.Text = dr[0].ToString();
                    TxtMail.Text = dr[1].ToString();
                    TxtIcerik.Text = dr[2].ToString();
                    TxtYemek.Text = dr[3].ToString();

                }
                bgl.baglanti().Close();
            }
        }

        protected void BtnOnayla_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Request.QueryString["Yorumid"]);
            SqlCommand komut = new SqlCommand("UPDATE Tbl_yorumlar SET yorumicerik=@P1,yorumonay=@P2 WHERE yorumid=@P3",bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtIcerik.Text);
            komut.Parameters.AddWithValue("@P2", true);
            komut.Parameters.AddWithValue("@P3", (id > 0 ? id : 0));
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
        }
    }
}