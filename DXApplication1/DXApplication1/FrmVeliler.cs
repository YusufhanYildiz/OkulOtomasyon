using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DXApplication1
{
    public partial class FrmVeliler : Form
    {
        public FrmVeliler()
        {
            InitializeComponent();
        }

        DbOkulEntities db  = new DbOkulEntities();

        void Listele()
        {
            //var liste = from item in db.TBL_VELILER
            //            select new { item.VELIID, item.VELIANNE, item.VELIBABA, item.VELITEL1, item.VELITEL2, item.VELIMAIL };
            //gridControl1.DataSource = liste.ToList();
            using(DbOkulEntities db = new DbOkulEntities())
            {
                var liste = from item in db.TBL_VELILER
                            select new { item.VELIID, item.VELIANNE, item.VELIBABA, item.VELITEL1, item.VELITEL2, item.VELIMAIL };
                gridControl1.DataSource = liste.ToList();
            }
        }

        void Temizle()
        {
            txtid.Text = "";
            txtAnneAd.Text = "";
            txtBabaAd.Text = "";
            mskTel1.Text = "";
            msktelefon2.Text = "";
            txtmail.Text = "";
        }

        private void gridView1_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {

        }

        private void FrmVeliler_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void btnkaydet_Click_1(object sender, EventArgs e)
        {
            TBL_VELILER veli = new TBL_VELILER();
            veli.VELIANNE = txtAnneAd.Text;
            veli.VELIBABA = txtBabaAd.Text;
            veli.VELITEL1 = mskTel1.Text;
            veli.VELITEL2 = msktelefon2.Text;
            veli.VELIMAIL = txtmail.Text;
            db.TBL_VELILER.Add(veli);
            db.SaveChanges();
            Listele();
        }

        private void gridView1_FocusedRowObjectChanged_1(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            txtid.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle,"VELIID").ToString();
            txtAnneAd.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "VELIANNE").ToString();
            txtBabaAd.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "VELIBABA").ToString();
            mskTel1.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "VELITEL1").ToString();
            msktelefon2.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "VELITEL2").ToString();
            txtmail.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "VELIMAIL").ToString();
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "VELIID").ToString());
            //var item = db.TBL_VELILER.Find(id);
            //item.VELIANNE = txtAnneAd.Text;
            //item.VELIBABA = txtBabaAd.Text;
            //item.VELITEL1 = mskTel1.Text;
            //item.VELITEL2 = msktelefon2.Text;
            //item.VELIMAIL = txtmail.Text;
            //db.SaveChanges();
            //Listele();
            //Temizle();
            using(DbOkulEntities db = new DbOkulEntities())
            {
                var item = db.TBL_VELILER.FirstOrDefault(x => x.VELIID == id);
                item.VELIANNE = txtAnneAd.Text;
                item.VELIBABA = txtBabaAd.Text;
                item.VELITEL1 = mskTel1.Text;
                item.VELITEL2 = msktelefon2.Text;
                item.VELIMAIL = txtmail.Text;
                db.SaveChanges();
                Listele();
                Temizle();
            }
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "VELIID").ToString());
            //var item = db.TBL_VELILER.Find(id);
            //db.TBL_VELILER.Remove(item);
            //db.SaveChanges();
            //Listele();
            //Temizle();
            using (DbOkulEntities db = new DbOkulEntities())
            {
                var item = db.TBL_VELILER.First(x => x.VELIID == id);
                db.TBL_VELILER.Remove(item);
                db.SaveChanges();
                Listele();
                Temizle();
            }
        }

        private void btntemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }
    }
}
