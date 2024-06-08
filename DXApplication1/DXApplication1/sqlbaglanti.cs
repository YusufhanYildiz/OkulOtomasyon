using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXApplication1
{
    public class sqlbaglanti
    {
        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection(@"Data Source=DESKTOP-IB03MGP;Initial Catalog=dbo.okuloto;Integrated Security=True;Encrypt=False");
            baglan.Open();
            return baglan;
        }
    }
}
