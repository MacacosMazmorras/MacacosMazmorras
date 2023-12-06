using MacacosMazmorrasMVC.Models;
using System.Data.SqlClient;

namespace MacacosMazmorrasMVC.DAL
{
    public class SesionMonsterDAL
    {
        private readonly string connectionString; //Cadena de conexión con la base de datos

        public SesionMonsterDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
        //
        //Inserts into the DB a new SesionMonster (receives object)
        //
        public void InsertSesion(SesionMonster relation)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO SesionMonster (FKSesionId, FKMonsterId)" +
                               "VALUES (@FKSesionId, @FKMonsterId)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FKSesionId", relation.FKSesionId);
                    command.Parameters.AddWithValue("@FKMonsterId", relation.FKMonsterId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        //
        //Deletes the relation from DB (receives object)
        //
        public void DeleteSesion(SesionMonster relation)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM SesionMonster WHERE FKSesionId = @FKSesionId AND FKMonsterId = @FKMonsterId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FKSesionId", relation.FKSesionId);
                    command.Parameters.AddWithValue("@FKMonsterId", relation.FKMonsterId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
