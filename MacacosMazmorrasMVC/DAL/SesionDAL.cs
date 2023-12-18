using MacacosMazmorrasMVC.Models;
using System.Data.SqlClient;

namespace MacacosMazmorrasMVC.DAL
{
    public class SesionDAL
    {
        private readonly string connectionString; //Cadena de conexión con la base de datos

        public SesionDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
        //
        //Obtains all the Sesions from a User (Recieves userId and returns a SESION LIST)
        //
        public List<Sesion> ObtainAllUserSesions(int userId)
        {
            List<Sesion> sesions = new List<Sesion>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = $"SELECT * FROM Sesion " +
                               $"INNER JOIN Campaign ON FKCampaignId = CampaignId " +
                               $"WHERE FKUsuarioId = {userId}";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Sesion sesion = new Sesion()
                            {
                                SesionId = Convert.ToInt32(reader["SesionId"]),
                                SesionName = reader["SesionName"].ToString(),
                                SesionDesc = reader["SesionDesc"].ToString(),
                                FKCampaignId = Convert.ToInt32(reader["FKCampaignId"]),
                                SesionDate = (reader["SesionDate"] != DBNull.Value) ? Convert.ToDateTime(reader["SesionDate"]) : (DateTime?)null
                            };
                            sesions.Add(sesion);
                        }
                    }
                }
            }
            return sesions;
        }
        //
        //Obtains all the Sesions from a Campaign (Recieves CAMPAIGNID and returns a SESION LIST)
        //
        public List<Sesion> ObtainAllCampaignSesions(int campaignId)
        {
            List<Sesion> sesions = new List<Sesion>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = $"SELECT * FROM Sesion WHERE FKCampaignId = @CampaignId;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CampaignId", campaignId);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Sesion sesion = new Sesion()
                            {
                                SesionId = Convert.ToInt32(reader["SesionId"]),
                                SesionName = reader["SesionName"].ToString(),
                                SesionDesc = reader["SesionDesc"].ToString(),
                                FKCampaignId = Convert.ToInt32(reader["FKCampaignId"]),
                                SesionDate = (reader["SesionDate"] != DBNull.Value) ? Convert.ToDateTime(reader["SesionDate"]) : (DateTime?)null
                            };
                            sesions.Add(sesion);
                        }
                    }
                }
            }
            return sesions;
        }
        //
        //Obtains a sesion from a Campaign (recieves SESIONID and returns SESION object)
        //--for the modify sesion form
        public Sesion ObtainSession(int sesionId)
        {
            Sesion sesion = new Sesion();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = $"SELECT * FROM Sesion WHERE SesionId = @SesionId;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SesionId", sesionId);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sesion.SesionId = Convert.ToInt32(reader["SesionId"]);
                            sesion.SesionName = reader["SesionName"].ToString();
                            sesion.SesionDesc = reader["SesionDesc"].ToString();
                            sesion.FKCampaignId = Convert.ToInt32(reader["FKCampaignId"]);
                            sesion.SesionDate = (reader["SesionDate"] != DBNull.Value) ? Convert.ToDateTime(reader["SesionDate"]) : (DateTime?)null;
                        }
                    }
                }
            }
            return sesion;
        }
        //
        //Inserts into the DB a new sesion (receives object)
        //
        public void InsertSesion(Sesion sesion)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Sesion (SesionName, SesionDesc, FKCampaignId, SesionDate) " +
                               "VALUES (@SesionName, @SesionDesc, @FKCampaignId, @SesionDate)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SesionName", sesion.SesionName);
                    command.Parameters.AddWithValue("@SesionDesc", sesion.SesionDesc);
                    command.Parameters.AddWithValue("@SesionDate", (object)sesion.SesionDate ?? DBNull.Value);
                    command.Parameters.AddWithValue("@FKCampaignId", sesion.FKCampaignId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        //
        //Update an existing sesion (receives object)
        //
        public void UpdateSesion(Sesion sesion)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Sesion " +
                               "SET SesionName = @SesionName, SesionDesc = @SesionDesc, SesionDate = @SesionDate " +
                               "WHERE SesionId = @SesionId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SesionId", sesion.SesionId);
                    command.Parameters.AddWithValue("@SesionName", sesion.SesionName);
                    command.Parameters.AddWithValue("@SesionDesc", sesion.SesionDesc);
                    command.Parameters.AddWithValue("@SesionDate", (object)sesion.SesionDate ?? DBNull.Value);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        //
        //Deletes a sesion from DB (recieves ID)
        //
        public void DeleteSesion(int sesionId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand command = connection.CreateCommand())
                        {
                            command.Transaction = transaction;

                            // Delete from SesionMonster
                            command.CommandText = "DELETE FROM SesionMonster WHERE FKSesionId = @SesionId";
                            command.Parameters.AddWithValue("@SesionId", sesionId);
                            command.ExecuteNonQuery();

                            // Delete from Sesion
                            command.CommandText = "DELETE FROM Sesion WHERE SesionId = @SesionId";
                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw; // Re-throw the exception after rolling back the transaction
                    }
                }
            }
        }

    }
}
