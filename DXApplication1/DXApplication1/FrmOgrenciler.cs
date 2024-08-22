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
            SqlDataAdapter da1 = new SqlDataAdapter("Execute OgrenciVeli5", bgl.baglanti());
            da1.Fill(dt1);
            grdctrl5.DataSource = dt1;

            //6.SINIF
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Execute OgrenciVeli6", bgl.baglanti());
            da2.Fill(dt2);
            grdctrl6.DataSource = dt2;

            //7.SINIF
            DataTable dt3 = new DataTable();
            SqlDataAdapter da3 = new SqlDataAdapter("Execute OgrenciVeli7", bgl.baglanti());
            da3.Fill(dt3);
            grdctrl7.DataSource = dt3;

            //8.SINIF
            DataTable dt4 = new DataTable();
            SqlDataAdapter da4 = new SqlDataAdapter("Execute OgrenciVeli8", bgl.baglanti());
            da4.Fill(dt4);
            grdctrl8.DataSource = dt4;
        }

        void velilistesi()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select VELIID,(VELIANNE + ' | ' +VELIBABA) as 'VELIANNEBABA' from TBL_VELILER",bgl.baglanti());
            da.Fill(dt);
            lookUpEdit1.Properties.ValueMember = "VELIID";
            lookUpEdit1.Properties.DisplayMember = "VELIANNEBABA";
            lookUpEdit1.Properties.DataSource = dt;

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
        void temizle()
        {
            txtid.Text = "";
            txtad.Text = "";
            txtsoyad.Text = "";
            mskogrno.Text = "";
            msktc.Text = "";
            rdbtnerkek.Checked = false;
            rdbtnkadin.Checked = false;
            cmbsinif.Text = "";
            cmbil.Text = "";
            cmbilce.Text = "";
            dateEdit1.Text = "";
            rtxtadres.Text = "";
            pcredit.Text = "";
        }

        private void gridView1_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {

        }

        private void FrmOgrenciler_Load(object sender, EventArgs e)
        {
            listele();
            sehirekle();
            temizle();
            velilistesi();
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
            SqlCommand komut = new SqlCommand("insert into TBL_OGRENCILER (OGRAD,OGRSOYAD,OGRNO,OGRSINIF,OGRDOGTAR,OGRCINSIYET,OGRTC,OGRIL,OGRILCE,OGRADRES,OGRFOTO,OGRVELIID) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12)",bgl.baglanti());
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
            komut.Parameters.AddWithValue("@p11", Path.GetFileName(yeniyol));
            komut.Parameters.AddWithValue("@p12", lookUpEdit1.EditValue);
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
                yeniyol = "C:\\Users\\Yusufhan\\Documents\\GitHub\\OkulOtomasyon\\DXApplication1\\DXApplication1" + "\\resimler\\" + dr["OGRFOTO"].ToString();
                pcredit.Image = Image.FromFile(yeniyol);
                lookUpEdit1.Text = dr["VELIANNEBABA"].ToString();
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
                yeniyol = "C:\\Users\\Yusufhan\\Documents\\GitHub\\OkulOtomasyon\\DXApplication1\\DXApplication1" + "\\resimler\\" + dr["OGRFOTO"].ToString();
                pcredit.Image = Image.FromFile(yeniyol);
                lookUpEdit1.Text = dr["VELIANNEBABA"].ToString();
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
                yeniyol = "C:\\Users\\Yusufhan\\Documents\\GitHub\\OkulOtomasyon\\DXApplication1\\DXApplication1" + "\\resimler\\" + dr["OGRFOTO"].ToString();
                pcredit.Image = Image.FromFile(yeniyol);
                lookUpEdit1.Text = dr["VELIANNEBABA"].ToString();
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
                yeniyol = "C:\\Users\\Yusufhan\\Documents\\GitHub\\OkulOtomasyon\\DXApplication1\\DXApplication1" + "\\resimler\\" + dr["OGRFOTO"].ToString();
                pcredit.Image = Image.FromFile(yeniyol);
                lookUpEdit1.Text = dr["VELIANNEBABA"].ToString();

            }
        }

        public string yeniyol;

        private void btnresim_Click(object sender, EventArgs e)
        {
            OpenFileDialog dosya = new OpenFileDialog();
            dosya.Filter = "Resim Dosyası |*.jpg;*.png;*.nef | Tüm Dosyalar |*.*";
            dosya.ShowDialog();
            string dosyayolu = dosya.FileName;
            yeniyol = "C:\\Users\\Yusufhan\\Documents\\GitHub\\OkulOtomasyon\\DXApplication1\\DXApplication1" + "\\resimler\\" + Guid.NewGuid().ToString() + ".jpg";
            File.Copy(dosyayolu, yeniyol);
            pcredit.Image=Image.FromFile(yeniyol);

        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Update TBL_OGRENCILER set OGRAD=@p1,OGRSOYAD=@p2,OGRTC=@p3,OGRNO=@p4,OGRSINIF=@p5,OGRDOGTAR=@p6,OGRCINSIYET=@p7,OGRIL=@p8,OGRILCE=@p9,OGRADRES=@p10,OGRFOTO=@p11,OGRVELIID=@p13 where OGRID=@p12",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtad.Text);
            komut.Parameters.AddWithValue("@p2", txtsoyad.Text);
            komut.Parameters.AddWithValue("@p3", msktc.Text);
            komut.Parameters.AddWithValue("@p4", mskogrno.Text);
            komut.Parameters.AddWithValue("@p5", cmbsinif.Text);
            komut.Parameters.AddWithValue("@p6", dateEdit1.Text);
            if (rdbtnerkek.Checked == true)
            {
                komut.Parameters.AddWithValue("p7", cinsiyet = "E");
            }
            else
            {
                komut.Parameters.AddWithValue("p7", cinsiyet = "K");
            }
            komut.Parameters.AddWithValue("@p8", cmbil.Text);
            komut.Parameters.AddWithValue("@p9", cmbilce.Text);
            komut.Parameters.AddWithValue("@p10", rtxtadres.Text);
            komut.Parameters.AddWithValue("@p11", Path.GetFileName(yeniyol));
            komut.Parameters.AddWithValue("@p12",txtid.Text);
            komut.Parameters.AddWithValue("@p13", lookUpEdit1.EditValue);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Öğrenci Bilgileri Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Delete from TBL_OGRENCILER where OGRID=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Öğrenci Bilgisi Silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
        }

        private void btntemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            FrmNufusCuzdani frm = new FrmNufusCuzdani();

            if (dr!= null)
            {
                frm.ad = dr["OGRAD"].ToString();
                frm.soyad = dr["OGRSOYAD"].ToString();
                frm.tc = dr["OGRTC"].ToString();
                frm.cinsiyet = dr["OGRCINSIYET"].ToString();
                frm.dogtarihi = dr["OGRDOGTAR"].ToString();
                frm.uzanti = "C:\\Users\\Yusufhan\\Documents\\GitHub\\OkulOtomasyon\\DXApplication1\\DXApplication1" + "\\resimler\\" + dr["OGRFOTO"].ToString();
            }
            frm.Show();
        }
    }
}
