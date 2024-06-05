using System;
using System.Collections.Generic;
using System.Data.SqlClient; //Potrebno ukljuciti!
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bioskop
{
    class Konekcija
    {
        public SqlConnection KreirajKonekciju() 
        { 
            SqlConnectionStringBuilder ccnSb = new SqlConnectionStringBuilder();

            ccnSb.DataSource = @"markop166\SQLEXPRESS"; //naziv servera
            ccnSb.InitialCatalog = "EONIS-Bioskop"; //naziv baze u okviru servera
            ccnSb.IntegratedSecurity = true;   //ukoliko se radi  na lokalnoj masini ovaj prostor se postavlja na TRUE!

            string con= ccnSb.ToString();
            SqlConnection konekcija = new SqlConnection(con);  
            return konekcija;
        }
    }
}
