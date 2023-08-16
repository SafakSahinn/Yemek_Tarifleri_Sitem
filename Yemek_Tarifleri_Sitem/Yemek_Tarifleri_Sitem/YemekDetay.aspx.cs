using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Yemek_Tarifleri_Sitem
{
    public partial class YemekDetay : System.Web.UI.Page
    {
        sqlsinif bgl = new sqlsinif();
        string yemekid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Request.QueryString["Yemekid"]);

            SqlCommand komut = new SqlCommand("SELECT YemekAd FROM Tbl_Yemekler WHERE yemekid=@P1", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", id);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                Label8.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();

            //Yorumları listeleme
            SqlCommand komut2 = new SqlCommand("SELECT * FROM Tbl_Yorumlar WHERE yemekid=@P2", bgl.baglanti());
            komut2.Parameters.AddWithValue("@P2", yemekid);
            SqlDataReader dr2 = komut2.ExecuteReader();
            DataList2.DataSource = dr2;
            DataList2.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("INSERT INTO Tbl_Yorumlar (YorumAdSoyad,YorumMail,Yorumicerik,yemekid) VALUES (@P1,@P2,@P3,@P4)", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1",TextBox1.Text);
            komut.Parameters.AddWithValue("@P2",TextBox2.Text);
            komut.Parameters.AddWithValue("@P3",TextBox3.Text);
            komut.Parameters.AddWithValue("@P4",yemekid);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
        }
    }
}