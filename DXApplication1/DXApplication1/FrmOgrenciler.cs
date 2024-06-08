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
using System.IO;

namespace DXApplication1
{
    public partial class FrmOgrenciler : Form
    {
        public FrmOgrenciler()
        {
            InitializeComponent();
        }
        sqlbaglanti bgl = new sqlbaglanti();
        void listele()
        {
            //5.SINIF
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from TBL_OGRENCILER where OGRSINIF='5.SINIF'", bgl.baglanti());
            da1.Fill(dt1);
            grdctrl5.DataSource = dt1;

            //6.SINIF
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from TBL_OGRENCILER where OGRSINIF='6.SINIF'", bgl.baglanti());
            da2.Fill(dt2);
            grdctrl6.DataSource = dt2;

            //7.SINIF
            DataTable dt3 = new DataTable();
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from TBL_OGRENCILER where OGRSINIF='7.SINIF'", bgl.baglanti());
            da3.Fill(dt3);
            grdctrl7.DataSource = dt3;

            //8.SINIF
            DataTable dt4 = new DataTable();
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from TBL_OGRENCILER where OGRSINIF='8.SINIF'", bgl.baglanti());
            da4.Fill(dt4);
            grdctrl8.DataSource = dt4;
        }
        void sehirekle()
        {
            SqlCommand komut = new SqlCommand("Select * from TBL_ILLER",bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbil.Properties.Items.Add(dr[1]);
            }
            bgl.baglanti().Close();
        }

        private void gridView1_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {

        }

        private void FrmOgrenciler_Load(object sender, EventArgs e)
        {
            listele();
            sehirekle();
        }

        private void cmbil_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbilce.Properties.Items.Clear();
            cmbilce.Text = "";

            SqlCommand komut = new SqlCommand("Select * from TBL_ILCELER where sehir=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",cmbil.SelectedIndex + 1);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbilce.Properties.Items.Add(dr[1]);
            }
            bgl.baglanti().Close();
        }

        public string cinsiyet;

        private void btnkaydet_Click_1(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TBL_OGRENCILER (OGRAD,OGRSOYAD,OGRNO,OGRSINIF,OGRDOGTAR,OGRCINSIYET,OGRTC,OGRIL,OGRILCE,OGRADRES) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10)",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",txtad.Text);
            komut.Parameters.AddWithValue("@p2", txtsoyad.Text);
            komut.Parameters.AddWithValue("@p3", mskogrno.Text);
            komut.Parameters.AddWithValue("@p4", cmbsinif.Text);
            komut.Parameters.AddWithValue("@p5", dateEdit1.Text);
            if (rdbtnerkek.Checked == true)
            {
                komut.Parameters.AddWithValue("p6", cinsiyet = "E");
            }
            else
            {
                komut.Parameters.AddWithValue("p6", cinsiyet = "K");
            }
            komut.Parameters.AddWithValue("@p7", msktc.Text);
            komut.Parameters.AddWithValue("@p8", cmbil.Text);
            komut.Parameters.AddWithValue("@p9", cmbilce.Text);
            komut.Parameters.AddWithValue("@p10", rtxtadres.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Öğrenci Eklendi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            listele();
        }

        private void gridView1_FocusedRowObjectChanged_1(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtid.Text = dr["OGRID"].ToString();
                txtad.Text = dr["OGRAD"].ToString();
                txtsoyad.Text = dr["OGRSOYAD"].ToString();
                msktc.Text = dr["OGRTC"].ToString();
                mskogrno.Text = dr["OGRNO"].ToString();
                cmbsinif.Text = dr["OGRSINIF"].ToString();
                if (dr["OGRCINSIYET"].ToString()=="E")
                {
                    rdbtnerkek.Checked = true;
                }
                else
                {
                    rdbtnkadin.Checked = true;
                }
                cmbil.Text = dr["OGRIL"].ToString();
                cmbilce.Text = dr["OGRILCE"].ToString();
                dateEdit1.Text = dr["OGRDOGTAR"].ToString();
                rtxtadres.Text = dr["OGRADRES"].ToString();
            }
        }

        private void gridView2_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            DataRow dr = gridView2.GetDataRow(gridView2.FocusedRowHandle);
            if (dr != null)
            {
                txtid.Text = dr["OGRID"].ToString();
                txtad.Text = dr["OGRAD"].ToString();
                txtsoyad.Text = dr["OGRSOYAD"].ToString();
                msktc.Text = dr["OGRTC"].ToString();
                mskogrno.Text = dr["OGRNO"].ToString();
                cmbsinif.Text = dr["OGRSINIF"].ToString();
                if (dr["OGRCINSIYET"].ToString() == "E")
                {
                    rdbtnerkek.Checked = true;
                }
                else
                {
                    rdbtnkadin.Checked = true;
                }
                cmbil.Text = dr["OGRIL"].ToString();
                cmbilce.Text = dr["OGRILCE"].ToString();
                dateEdit1.Text = dr["OGRDOGTAR"].ToString();
                rtxtadres.Text = dr["OGRADRES"].ToString();
            }
        }

        private void gridView3_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            DataRow dr = gridView3.GetDataRow(gridView3.FocusedRowHandle);
            if (dr != null)
            {
                txtid.Text = dr["OGRID"].ToString();
                txtad.Text = dr["OGRAD"].ToString();
                txtsoyad.Text = dr["OGRSOYAD"].ToString();
                msktc.Text = dr["OGRTC"].ToString();
                mskogrno.Text = dr["OGRNO"].ToString();
                cmbsinif.Text = dr["OGRSINIF"].ToString();
                if (dr["OGRCINSIYET"].ToString() == "E")
                {
                    rdbtnerkek.Checked = true;
                }
                else
                {
                    rdbtnkadin.Checked = true;
                }
                cmbil.Text = dr["OGRIL"].ToString();
                cmbilce.Text = dr["OGRILCE"].ToString();
                dateEdit1.Text = dr["OGRDOGTAR"].ToString();
                rtxtadres.Text = dr["OGRADRES"].ToString();
            }
        }

        private void gridView4_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            DataRow dr = gridView4.GetDataRow(gridView4.FocusedRowHandle);
            if (dr != null)
            {
                txtid.Text = dr["OGRID"].ToString();
                txtad.Text = dr["OGRAD"].ToString();
                txtsoyad.Text = dr["OGRSOYAD"].ToString();
                msktc.Text = dr["OGRTC"].ToString();
                mskogrno.Text = dr["OGRNO"].ToString();
                cmbsinif.Text = dr["OGRSINIF"].ToString();
                if (dr["OGRCINSIYET"].ToString() == "E")
                {
                    rdbtnerkek.Checked = true;
                }
                else
                {
                    rdbtnkadin.Checked = true;
                }
                cmbil.Text = dr["OGRIL"].ToString();
                cmbilce.Text = dr["OGRILCE"].ToString();
                dateEdit1.Text = dr["OGRDOGTAR"].ToString();
                rtxtadres.Text = dr["OGRADRES"].ToString();
            }
        }
    }
}
