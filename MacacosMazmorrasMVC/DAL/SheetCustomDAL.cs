using MacacosMazmorrasMVC.Models;
using Microsoft.Data.SqlClient;

namespace MacacosMazmorrasMVC.DAL
{
    public class SheetCustomDAL
    {
        private readonly string connectionString; //Cadena de conexión con la base de datos

        public SheetCustomDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
        //
        //Obtains SheetCustom list from a USER (receives USER ID)
        //
        public List<SheetCustom> ObtainUserSheets(int userId)
        {
            List<SheetCustom> sheets = new List<SheetCustom>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = $"SELECT * FROM SheetCustom" +
                    $"WHERE FKUsuarioId = {userId};";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SheetCustom sheet = new SheetCustom()
                            {
                                SheetCustomId = Convert.ToInt32(reader["SheetCustomId"]),
                                FKUsuarioId = Convert.ToInt32(reader["FKUsuarioId"]),
                                SheetCustomName = reader["SheetCustomName"].ToString(),
                                SheetCustomBackground = (reader["SheetCustomBackground"] != DBNull.Value) ? reader["SheetCustomBackground"].ToString() : (string?)null,
                                SheetCustomImageUrl = (reader["SheetCustomImageUrl"] != DBNull.Value) ? reader["SheetCustomImageUrl"].ToString() : (string?)null,
                                SheetCustomStr = Convert.ToInt32(reader["SheetCustomStr"]),
                                SheetCustomDex = Convert.ToInt32(reader["SheetCustomDex"]),
                                SheetCustomCon = Convert.ToInt32(reader["SheetCustomCon"]),
                                SheetCustomInt = Convert.ToInt32(reader["SheetCustomInt"]),
                                SheetCustomWis = Convert.ToInt32(reader["SheetCustomWis"]),
                                SheetCustomCha = Convert.ToInt32(reader["SheetCustomCha"]),
                                SheetCustomCA = Convert.ToInt32(reader["SheetCustomCA"]),
                                SheetCustomPV = Convert.ToInt32(reader["SheetCustomPV"]),
                                FKTypeSheetId = Convert.ToInt32(reader["FKTypeSheetId"]),
                                SheetCustomRace = (reader["SheetCustomImageUrl"] != DBNull.Value) ? reader["SheetCustomRace"].ToString() : (string?)null,
                                SheetCustomCR = (reader["SheetCustomCR"] != DBNull.Value) ? Convert.ToInt32(reader["SheetCustomCR"]) : (int?)null,
                                SheetCustomLevel = (reader["SheetCustomLevel"] != DBNull.Value) ? Convert.ToInt32(reader["SheetCustomLevel"]) : (int?)null
                            };
                            sheets.Add(sheet);
                        }
                    }
                }
            }
            return sheets;
        }
        //
        //Inserts into the DB a new Sheet (receives object)
        //
        public void InsertSheet(SheetCustom sheet)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO SheetCustom (FKUsuarioId, SheetCustomName, SheetCustomBackground, SheetCustomImageUrl, SheetCustomStr, SheetCustomDex, SheetCustomCon, SheetCustomInt, SheetCustomWis, SheetCustomCha, SheetCustomCA, SheetCustomPV, FKTypeSheetId, SheetCustomRace, SheetCustomCR, SheetCustomLevel) " +
                               "VALUES (@FKUsuarioId, @SheetCustomName, @SheetCustomBackground, @SheetCustomImageUrl, @SheetCustomStr, @SheetCustomDex, @SheetCustomCon, @SheetCustomInt, @SheetCustomWis, @SheetCustomCha, @SheetCustomCA, @SheetCustomPV, @FKTypeSheetId, @SheetCustomRace, @SheetCustomCR, @SheetCustomLevel)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FKUsuarioId", sheet.FKUsuarioId);
                    command.Parameters.AddWithValue("@SheetCustomName", sheet.SheetCustomName);
                    command.Parameters.AddWithValue("@SheetCustomBackground", (object)sheet.SheetCustomBackground ?? DBNull.Value);
                    command.Parameters.AddWithValue("@SheetCustomImageUrl", (object)sheet.SheetCustomImageUrl ?? DBNull.Value);
                    command.Parameters.AddWithValue("@SheetCustomStr", sheet.SheetCustomStr);
                    command.Parameters.AddWithValue("@SheetCustomDex", sheet.SheetCustomDex);
                    command.Parameters.AddWithValue("@SheetCustomCon", sheet.SheetCustomCon);
                    command.Parameters.AddWithValue("@SheetCustomInt", sheet.SheetCustomInt);
                    command.Parameters.AddWithValue("@SheetCustomWis", sheet.SheetCustomWis);
                    command.Parameters.AddWithValue("@SheetCustomCha", sheet.SheetCustomCha);
                    command.Parameters.AddWithValue("@SheetCustomCA", sheet.SheetCustomCA);
                    command.Parameters.AddWithValue("@SheetCustomPV", sheet.SheetCustomPV);
                    command.Parameters.AddWithValue("@FKTypeSheetId", sheet.FKTypeSheetId);
                    command.Parameters.AddWithValue("@SheetCustomRace", (object)sheet.SheetCustomRace ?? DBNull.Value);
                    command.Parameters.AddWithValue("@SheetCustomCR", (object)sheet.SheetCustomCR ?? DBNull.Value);
                    command.Parameters.AddWithValue("@SheetCustomLevel", (object)sheet.SheetCustomLevel ?? DBNull.Value);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        //
        //Update an existing sheet (receives object)
        //
        public void UpdateSheet(SheetCustom sheet)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE SheetCustom " +
                               "SET SheetCustomName = @SheetCustomName, SheetCustomBackground = @SheetCustomBackground, SheetCustomImageUrl = @SheetCustomImageUrl, SheetCustomStr = @SheetCustomStr, SheetCustomDex = @SheetCustomDex, SheetCustomCon = @SheetCustomCon, SheetCustomInt = @SheetCustomInt, SheetCustomWis = @SheetCustomWis, SheetCustomCha = @SheetCustomCha, SheetCustomCA = @SheetCustomCA, SheetCustomPV = @SheetCustomPV, FKTypeSheetId = @FKTypeSheetId, SheetCustomRace = @SheetCustomRace, SheetCustomCR = @SheetCustomCR, SheetCustomLevel = @SheetCustomLevel" +
                               "WHERE SheetCustomId = @SheetCustomId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SheetCustomId", sheet.SheetCustomId);
                    command.Parameters.AddWithValue("@SheetCustomName", sheet.SheetCustomName);
                    command.Parameters.AddWithValue("@SheetCustomBackground", (object)sheet.SheetCustomBackground ?? DBNull.Value);
                    command.Parameters.AddWithValue("@SheetCustomImageUrl", (object)sheet.SheetCustomImageUrl ?? DBNull.Value);
                    command.Parameters.AddWithValue("@SheetCustomStr", sheet.SheetCustomStr);
                    command.Parameters.AddWithValue("@SheetCustomDex", sheet.SheetCustomDex);
                    command.Parameters.AddWithValue("@SheetCustomCon", sheet.SheetCustomCon);
                    command.Parameters.AddWithValue("@SheetCustomInt", sheet.SheetCustomInt);
                    command.Parameters.AddWithValue("@SheetCustomWis", sheet.SheetCustomWis);
                    command.Parameters.AddWithValue("@SheetCustomCha", sheet.SheetCustomCha);
                    command.Parameters.AddWithValue("@SheetCustomCA", sheet.SheetCustomCA);
                    command.Parameters.AddWithValue("@SheetCustomPV", sheet.SheetCustomPV);
                    command.Parameters.AddWithValue("@FKTypeSheetId", sheet.FKTypeSheetId);
                    command.Parameters.AddWithValue("@SheetCustomRace", (object)sheet.SheetCustomRace ?? DBNull.Value);
                    command.Parameters.AddWithValue("@SheetCustomCR", (object)sheet.SheetCustomCR ?? DBNull.Value);
                    command.Parameters.AddWithValue("@SheetCustomLevel", (object)sheet.SheetCustomLevel ?? DBNull.Value);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        //
        //Deletes a sheet from DB (recieves ID)
        //
        public void DeleteSheet(int sheetId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM SheetCustom WHERE SheetCustomId = @SheetCustomId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SheetCustomId", sheetId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
