using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Yemek_Tarifleri_Sitem
{
    public partial class YemekDuzenle : System.Web.UI.Page
    {
        sqlsinif bgl = new sqlsinif();
        string id;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Request.QueryString["Yemekid"]);
            if (Page.IsPostBack == false)
            {
                SqlCommand cmd = new SqlCommand(" Select * From Tbl_Yemekler where Yemekid=@p1", bgl.baglanti());
                cmd.Parameters.AddWithValue("@p1", (id > 0 ? id : 0));
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    TextBox1.Text = dr[1].ToString();
                    TextBox2.Text = dr[2].ToString();
                    TextBox3.Text = dr[3].ToString();
                }
                bgl.baglanti().Close();

                if (Page.IsPostBack == false)
                {
                    //Kategori Listesi
                    SqlCommand komut2 = new SqlCommand("SELECT * FROM Tbl_Kategoriler", bgl.baglanti());
                    SqlDataReader dr2 = komut2.ExecuteReader();
                    DropDownList1.DataTextField = "KategoriAd";
                    DropDownList1.DataValueField = "Kategoriid";
                    DropDownList1.DataSource = dr2;
                    DropDownList1.DataBind();
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            FileUpload1.SaveAs(Server.MapPath("/resimler/"+FileUpload1.FileName));

            int id = Convert.ToInt32(Request.QueryString["Yemekid"]);
            SqlCommand komut = new SqlCommand("UPDATE Tbl_Yemekler SET YemekAd=@P1,Yemekmalzeme=@P2,YemekTarif=@P3,kategoriid=@P4,yemekresim=@P6 WHERE yemekid=@P5", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", TextBox1.Text);
            komut.Parameters.AddWithValue("@P2", TextBox2.Text);
            komut.Parameters.AddWithValue("@P3", TextBox3.Text);
            komut.Parameters.AddWithValue("@P4", DropDownList1.SelectedValue);
            komut.Parameters.AddWithValue("@P6", "~/resimler/"+ FileUpload1.FileName);
            komut.Parameters.AddWithValue("@P5", (id > 0 ? id : 0));
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {   //tüm yemeklerin durumunu false yapma
            SqlCommand komut = new SqlCommand("UPDATE Tbl_Yemekler SET durum=0", bgl.baglanti());
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();

            //günün yemeği için id ye göre durumu tru yapma
            int id = Convert.ToInt32(Request.QueryString["Yemekid"]);
            SqlCommand komut2 = new SqlCommand("UPDATE Tbl_Yemekler SET durum=1 WHERE yemekid=@P1", bgl.baglanti());
            komut2.Parameters.AddWithValue("@P1", id);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
        }
    }
}