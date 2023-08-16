using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Yemek_Tarifleri_Sitem
{
    public partial class TarifOnerDetay : System.Web.UI.Page
    {
        sqlsinif bgl = new sqlsinif();
        string id;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Request.QueryString["Tarifid"]);

            if (Page.IsPostBack == false)
            {
                SqlCommand komut = new SqlCommand("SELECT * FROM Tbl_Tarifler where Tarifid=@P1", bgl.baglanti());
                komut.Parameters.AddWithValue("@P1", id);
                SqlDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    TextBox1.Text = dr[1].ToString();
                    TextBox2.Text = dr[2].ToString();
                    TextBox3.Text = dr[3].ToString();
                    TextBox4.Text = dr[5].ToString();
                    TextBox5.Text = dr[6].ToString();
                }
                bgl.baglanti().Close();

                //kategori listesi
                SqlCommand komut2 = new SqlCommand("SELECT * FROM Tbl_Kategoriler", bgl.baglanti());
                SqlDataReader dr2 = komut2.ExecuteReader();
                DropDownList1.DataTextField = "KategoriAd";
                DropDownList1.DataValueField = "Kategoriid";
                DropDownList1.DataSource = dr2;
                DropDownList1.DataBind();
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //durum güncelleme
            int id = Convert.ToInt32(Request.QueryString["Tarifid"]);
            SqlCommand komut = new SqlCommand("UPDATE Tbl_Tarifler SET Tarifdurum=1 WHERE tarifid=@P1", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", id);
            komut.ExecuteNonQuery();

            //yemeği ana sayfaya ekleme
            SqlCommand komut2 = new SqlCommand("INSERt INTO Tbl_Yemekler (YemekAd,YemekMalzeme,YemekTarif,Kategoriid) VALUES (@P1,@P2,@P3,@P4)", bgl.baglanti());
            komut2.Parameters.AddWithValue("@P1", TextBox1.Text);
            komut2.Parameters.AddWithValue("@P2", TextBox2.Text);
            komut2.Parameters.AddWithValue("@P3", TextBox3.Text);
            komut2.Parameters.AddWithValue("@P4", DropDownList1.SelectedValue);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
        }
    }
}