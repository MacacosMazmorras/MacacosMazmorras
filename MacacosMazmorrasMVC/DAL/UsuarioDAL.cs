﻿using System;
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
    }
}