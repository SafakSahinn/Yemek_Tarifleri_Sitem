using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Yemek_Tarifleri_Sitem
{
    public partial class Yemekler : System.Web.UI.Page
    {
        sqlsinif bgl = new sqlsinif();

        string islem = "";
        string id = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Panel2.Visible = false;
            Panel4.Visible = false;

            if (Page.IsPostBack == false)
            {
                id = Request.QueryString["Yemekid"];
                islem = Request.QueryString["islem"];

                //Kategori Listesi
                SqlCommand komut2 = new SqlCommand("SELECT * FROM Tbl_Kategoriler", bgl.baglanti());
                SqlDataReader dr2 = komut2.ExecuteReader();
                DropDownList1.DataTextField = "KategoriAd";
                DropDownList1.DataValueField = "Kategoriid";
                DropDownList1.DataSource = dr2;
                DropDownList1.DataBind();
            }

            //Yemek Listesi
            SqlCommand komut = new SqlCommand("SELECT * FROM Tbl_Yemekler",bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            DataList1.DataSource = dr;
            DataList1.DataBind();

            if (islem == "sil")
            {
                int id = Convert.ToInt32(Request.QueryString["Yemekid"]);
                SqlCommand komut2 = new SqlCommand("DELETE FROM Tbl_Yemekler WHERE Yemekid=@P1",bgl.baglanti());
                komut2.Parameters.AddWithValue("@P1", (id > 0 ? id : 0));
                komut2.ExecuteNonQuery();
                bgl.baglanti().Close();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Panel2.Visible = true;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Panel2.Visible = false;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Panel4.Visible = true;
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Panel4.Visible = false;

        }

        protected void BtnEkle_Click(object sender, EventArgs e)
        {
            //yemek ekleme
            SqlCommand komut = new SqlCommand("INSERT INTO Tbl_Yemekler (YemekAd,YemekMalzeme,YemekTarif,Kategoriid) VALUES (@P1,@P2,@P3,@P4)", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1",TextBox1.Text);
            komut.Parameters.AddWithValue("@P2",TextBox2.Text);
            komut.Parameters.AddWithValue("@P3",TextBox3.Text);
            komut.Parameters.AddWithValue("@P4",DropDownList1.SelectedValue);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();

            //kategori sayısını artırma
            SqlCommand komut2 = new SqlCommand("UPDATE Tbl_Kategoriler SET KategoriAdet=KategoriAdet+1 WHERE kategoriid=@P1",bgl.baglanti());
            komut2.Parameters.AddWithValue("@P1",DropDownList1.SelectedValue);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
        }
    }
}