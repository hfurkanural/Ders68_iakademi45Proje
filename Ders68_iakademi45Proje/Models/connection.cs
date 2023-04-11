using Microsoft.Data.SqlClient;

namespace Ders68_iakademi45Proje.Models
{
    public class connection
    {
        public static SqlConnection ServerConnect
        {
            get
            {
                SqlConnection sqlConnection = new SqlConnection("Server=FURKAN;Database=iakademi45Core_proje;Trusted_Connection=True;TrustServerCertificate=True;");
                return sqlConnection;
            }
        }
    }
}
