using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using MacacosMazmorrasMVC.Models;

namespace MacacosMazmorrasMVC.DAL
{
    public class UsuarioDAL
    {
        private readonly string connectionString; //Cadena de conexión con la base de datos

        public UsuarioDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Usuario GetUser(Usuario userToGet)
        {
            Usuario newUser = new Usuario();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = $"SELECT * FROM Usuario " +
                                   $"WHERE UsuarioMail = @UsuarioMail;";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UsuarioMail", userToGet.UsuarioMail);

                        connection.Open();

                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                newUser.UsuarioId = Convert.ToInt32(reader["UsuarioId"]);
                                newUser.UsuarioName = reader["UsuarioName"].ToString();
                                newUser.UsuarioMail = reader["UsuarioMail"].ToString();
                                newUser.UsuarioPassword = reader["UsuarioPassword"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return newUser;
        }
        //
        //Recovers data from user by id
        //
        public Usuario GetUserById(int userId)
        {
            Usuario newUser = new Usuario();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = $"SELECT * FROM Usuario " +
                                   $"WHERE UsuarioId = @UsuarioId;";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UsuarioId", userId);

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                newUser.UsuarioId = Convert.ToInt32(reader["UsuarioId"]);
                                newUser.UsuarioName = reader["UsuarioName"].ToString();
                                newUser.UsuarioMail = reader["UsuarioMail"].ToString();
                                newUser.UsuarioPassword = reader["UsuarioPassword"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return newUser;
        }

        //Inserts into BD new user (Recieves object)
        public void InsertUsuario(Usuario usuario)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Usuario (UsuarioName, UsuarioMail, UsuarioPassword) " +
                                   "VALUES (@UsuarioName, @UsuarioMail, @UsuarioPassword)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UsuarioName", usuario.UsuarioName);
                        command.Parameters.AddWithValue("@UsuarioMail", usuario.UsuarioMail);
                        command.Parameters.AddWithValue("@UsuarioPassword", usuario.UsuarioPassword);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        //
        //Checks if there is an already existing user in DB
        //
        public bool CheckUser(Usuario user)
        {
            bool userExists = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT COUNT(*) FROM Usuario " +
                                   "WHERE UsuarioMail = @UsuarioMail AND UsuarioPassword = @UsuarioPassword";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UsuarioMail", user.UsuarioMail);
                        command.Parameters.AddWithValue("@UsuarioPassword", user.UsuarioPassword);

                        connection.Open();

                        int count = (int)command.ExecuteScalar();

                        userExists = count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return userExists;
        }
        //
        //Deletes a USUARIO from DB (recieves ID)
        //
        public void DeleteUser(int userId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();  // Open the connection before starting the transaction

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand command = connection.CreateCommand())
                        {
                            command.Transaction = transaction;

                            // Delete from SesionMonster
                            command.CommandText = "DELETE FROM SesionMonster WHERE FKSesionId IN (SELECT SesionId FROM Sesion WHERE FKCampaignId IN (SELECT CampaignId FROM Campaign WHERE FKUsuarioId = @UsuarioId))";
                            command.Parameters.AddWithValue("@UsuarioId", userId);
                            command.ExecuteNonQuery();

                            // Delete from Sesion
                            command.CommandText = "DELETE FROM Sesion WHERE FKCampaignId IN (SELECT CampaignId FROM Campaign WHERE FKUsuarioId = @UsuarioId)";
                            command.ExecuteNonQuery();

                            // Delete from Campaign
                            command.CommandText = "DELETE FROM Campaign WHERE FKUsuarioId = @UsuarioId";
                            command.ExecuteNonQuery();

                            // Delete from Usuario
                            command.CommandText = "DELETE FROM Usuario WHERE UsuarioId = @UsuarioId";
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
