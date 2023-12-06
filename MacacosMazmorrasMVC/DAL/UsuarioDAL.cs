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

        public void InsertUsuario(Usuario usuario)
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


    }
}
