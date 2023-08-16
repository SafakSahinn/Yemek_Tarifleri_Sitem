using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Yemek_Tarifleri_Sitem
{
    public partial class iletisim : System.Web.UI.Page
    {
        sqlsinif bgl = new sqlsinif();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("INSERT INTO Tbl_Mesajlar (MesajGonderen,MesajBaslik,MesajMail,Mesajicerik) VALUES (@P1,@P2,@P3,@P4)", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtGonderen.Text);
            komut.Parameters.AddWithValue("@P2", TxtBaslik.Text);
            komut.Parameters.AddWithValue("@P3", TxtMail.Text);
            komut.Parameters.AddWithValue("@P4", TxtMesaj.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
        }
    }
}