using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DXApplication1
{
    public partial class FrmAnaModul : DevExpress.XtraEditors.XtraForm
    {
        public FrmAnaModul()
        {
            InitializeComponent();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        FrmOgretmenler frm1;
        FrmOgrenciler frm2;
        FrmVeliler frm3;
        private void btnogretmen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frm1 == null || frm1.IsDisposed)
            {
            frm1 = new FrmOgretmenler();
            frm1.MdiParent = this;
            frm1.Show();
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frm2 == null || frm2.IsDisposed)
            {
                frm2 = new FrmOgrenciler();
                frm2.MdiParent = this;
                frm2.Show();
            }
        }

        private void btnVeliler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frm3 == null || frm3.IsDisposed)
            {
                frm3 = new FrmVeliler();
                frm3.MdiParent = this;
                frm3.Show();
            }
        }
    }
}
