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

namespace DXApplication1
{
    public partial class FrmGiris : Form
    {
        public FrmGiris()
        {
            InitializeComponent();
        }
        DbOkulEntities db = new DbOkulEntities();
        sqlbaglanti bgl = new sqlbaglanti();

        private void btnyonetici_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select OGRTTC,OGRTSIFRE from TBL_AYARLAR inner join TBL_OGRETMENLER on TBL_AYARLAR.AYARLARID=TBL_OGRETMENLER.OGRTID where OGRTTC=@p1 and OGRTSIFRE=@p2",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",msktc.Text);
            komut.Parameters.AddWithValue("@p2",txtsifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                FrmAnaModul frm1 = new FrmAnaModul();
                frm1.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı Kullanıcı Adı veya Şifre");
                msktc.Text = "";
                txtsifre.Text = "";
            }
            bgl.baglanti().Close();
        }

        private void btnogrt_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select OGRTTC,OGRTSIFRE from TBL_AYARLAR inner join TBL_OGRETMENLER on TBL_AYARLAR.AYARLARID=TBL_OGRETMENLER.OGRTID where OGRTTC=@p1 and OGRTSIFRE=@p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", msktc.Text);
            komut.Parameters.AddWithValue("@p2", txtsifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                FrmOgretmenAnaModul frm2 = new FrmOgretmenAnaModul();
                frm2.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı Kullanıcı Adı veya Şifre");
                msktc.Text = "";
                txtsifre.Text = "";
            }
            bgl.baglanti().Close();
        }

        private void btnogrenci_Click(object sender, EventArgs e)
        {
        }
    }
}
