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
using DevExpress.XtraLayout.Resizing;
using static DevExpress.XtraEditors.Mask.MaskSettings;

namespace DXApplication1
{
    public partial class FrmAyarlar : Form
    {
        public FrmAyarlar()
        {
            InitializeComponent();
        }

        sqlbaglanti bgl = new sqlbaglanti();
        DbOkulEntities db = new DbOkulEntities();


        //ADO.NET Öğretmen Şifre Bilgileri
        void listele()
        {
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("Execute AyarlarOgretmenler",bgl.baglanti());
            da1.Fill(dt1);
            gridControl1.DataSource = dt1;

        }

        //Entity İle Öğrenci Listele
        void ogrlistele()
        {
            gridControl2.DataSource = db.AyarlarOgrenciler();
        }

        void temizle()
        {
            txtogrtid.Text = "";
            txtogrid.Text = "";
            txtbrans.Text = "";
            txtsinif.Text = "";
            txtogrtsifre.Text = "";
            txtogrsifre.Text = "";
            mskogrtc.Text = "";
            mskogrttc.Text = "";
            pictureEdit1.Text = "";
            pictureEdit2.Text = "";
            lookUpEdit1.Text = "";
            lookUpEdit2.Text = "";
            lookUpEdit1.Properties.NullText = "Öğretmen Seçiniz";
            lookUpEdit2.Properties.NullText = "Öğretmen Seçiniz";
        }
        //ADO.NET lookupedit veri ekleme
        void ogretmenlistesi()
        {
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Select OGRTID,(OGRTAD+' '+OGRTSOYAD) as 'OGRTADSOYAD',OGRTBRANS from TBL_OGRETMENLER",bgl.baglanti());
            da2.Fill(dt2);
            lookUpEdit1.Properties.ValueMember = "OGRTID";
            lookUpEdit1.Properties.DisplayMember = "OGRTADSOYAD";
            lookUpEdit1.Properties.NullText = "Öğretmen Seçiniz";
            lookUpEdit1.Properties.DataSource = dt2;
        }

        //Entity Lookupedit veri ekleme
        void ogrencilistesi()
        {
            var deger = from item in db.TBL_OGRENCILER
                        select new
                        {
                            OGRID = item.OGRID,
                            OGRADSOYAD = item.OGRAD + " " + item.OGRSOYAD,
                            OGRSINIF = item.OGRSINIF,
                        };
            lookUpEdit2.Properties.ValueMember = "OGRID";
            lookUpEdit2.Properties.DisplayMember = "OGRADSOYAD";
            lookUpEdit2.Properties.NullText = "Öğrenci Seçiniz";
            lookUpEdit2.Properties.DataSource = deger.ToList();

        }


        //ADO.NET Verileri GridControle Taşıma
        private void FrmAyarlar_Load(object sender, EventArgs e)
        {
            listele();
            ogretmenlistesi();
            temizle();
            ogrlistele();
            ogrencilistesi();
        }
        public string yeniyol;
        private void gridView1_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtogrtid.Text = dr["AYARLARID"].ToString();
                lookUpEdit1.Text = dr["OGRTADSOYAD"].ToString();
                txtbrans.Text = dr["OGRTBRANS"].ToString();
                mskogrttc.Text = dr["OGRTTC"].ToString();
                txtogrtsifre.Text = dr["OGRTSIFRE"].ToString();
                yeniyol = "C:\\Users\\Yusufhan\\Documents\\GitHub\\OkulOtomasyon\\DXApplication1\\DXApplication1" + "\\resimler\\" + dr["OGRTFOTO"].ToString();
                pictureEdit1.Image = Image.FromFile(yeniyol);
            }
        }
        //Ado.net lookupedit veri değiştirme
        private void lookUpEdit1_Properties_EditValueChanged(object sender, EventArgs e)
        {
            txtogrtsifre.Text = "";
            SqlCommand komut = new SqlCommand("Select * from TBL_OGRETMENLER where OGRTID=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", lookUpEdit1.ItemIndex + 1);
            SqlDataReader dr3 = komut.ExecuteReader();
            while (dr3.Read())
            {
                txtogrtid.Text = dr3["OGRTID"].ToString();
                txtbrans.Text = dr3["OGRTBRANS"].ToString();
                mskogrttc.Text = dr3["OGRTTC"].ToString();
                yeniyol = "C:\\Users\\Yusufhan\\Documents\\GitHub\\OkulOtomasyon\\DXApplication1\\DXApplication1" + "\\resimler\\" + dr3["OGRTFOTO"].ToString();
                pictureEdit1.Image = Image.FromFile(yeniyol);
            }
            bgl.baglanti().Close();
        }


        //Ado.net verileri kaydetme
        private void btnogrtkaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut2 = new SqlCommand("insert into TBL_AYARLAR (AYARLARID,OGRTSIFRE) values (@p1,@p2)",bgl.baglanti());
            komut2.Parameters.AddWithValue("@p1", txtogrtid.Text);
            komut2.Parameters.AddWithValue("@p2", txtogrtsifre.Text);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Şifre oluşturuldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }
        //Ado.net verileri güncelle
        private void btnogrtguncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut3 = new SqlCommand("Update TBL_AYARLAR set OGRTSIFRE=@p1 where AYARLARID=@p2",bgl.baglanti());
            komut3.Parameters.AddWithValue("@p1",txtogrtsifre.Text);
            komut3.Parameters.AddWithValue("@p2", txtogrtid.Text);
            komut3.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Şifre Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void btnogrttemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void gridView2_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            txtogrid.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle,"AYARLAROGRID").ToString();
            lookUpEdit2.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "OGRADSOYAD").ToString();
            txtsinif.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "OGRSINIF").ToString();
            mskogrtc.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "OGRTC").ToString();
            txtogrsifre.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "OGRSIFRE").ToString();
            string uzanti = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "OGRFOTO").ToString();
            yeniyol = "C:\\Users\\Yusufhan\\Documents\\GitHub\\OkulOtomasyon\\DXApplication1\\DXApplication1" + "\\resimler\\" + uzanti;
            pictureEdit2.Image = Image.FromFile(yeniyol);
            

        }
        //Entity Lookupedit veri getirme
        private void lookUpEdit2_Properties_EditValueChanged(object sender, EventArgs e)
        {
            txtogrsifre.Text = "";
            using(DbOkulEntities db = new DbOkulEntities())
            {
                TBL_OGRENCILER sorgu = db.TBL_OGRENCILER.Find(lookUpEdit2.ItemIndex + 1);
                txtogrid.Text = sorgu.OGRID.ToString();
                txtsinif.Text = sorgu.OGRSINIF;
                mskogrtc.Text = sorgu.OGRTC;
                yeniyol = "C:\\Users\\Yusufhan\\Documents\\GitHub\\OkulOtomasyon\\DXApplication1\\DXApplication1" + "\\resimler\\" + sorgu.OGRFOTO;
                pictureEdit2.Image=Image.FromFile(yeniyol);

            }
        }

        private void btnogrkaydet_Click(object sender, EventArgs e)
        {
            TBL_OGRAYARLAR komut = new TBL_OGRAYARLAR();
            komut.AYARLAROGRID = Convert.ToInt32(txtogrid.Text);
            komut.OGRSIFRE = txtogrsifre.Text;
            db.TBL_OGRAYARLAR.Add(komut);
            db.SaveChanges();
            MessageBox.Show("Şifre oluşturuldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ogrlistele();
            temizle();
        }

        private void btnogrguncelle_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "AYARLAROGRID"));
            var item = db.TBL_OGRAYARLAR.FirstOrDefault(x => x.AYARLAROGRID == id);
            item.OGRSIFRE = txtogrsifre.Text;
            db.SaveChanges();
            MessageBox.Show("Şifre Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ogrlistele();
            temizle();
        }

        private void btnogrtemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}
