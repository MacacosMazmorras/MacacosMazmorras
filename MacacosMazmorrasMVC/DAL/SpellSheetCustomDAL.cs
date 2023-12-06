using MacacosMazmorrasMVC.Models;
using System.Data.SqlClient;

namespace MacacosMazmorrasMVC.DAL
{
    public class SpellSheetCustomDAL
    {
        private readonly string connectionString; //Cadena de conexión con la base de datos

        public SpellSheetCustomDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
        //
        //Inserts into the DB a new relation SpellSheet (receives object)
        //
        public void InsertSpellSheet(SpellSheetCustom relation)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO SpellSheetCustom (FKSheetCustomId, FKSpellId)" +
                               "VALUES (@FKSheetCustomId, @FKSpellId)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FKSheetCustomId", relation.FKSheetCustomId);
                    command.Parameters.AddWithValue("@FKSpellId", relation.FKSpellId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        //
        //Deletes the relation from DB (receives object)
        //
        public void DeleteSpellSheet(SpellSheetCustom relation)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM SpellSheetCustom WHERE FKSheetCustomId = @FKSheetCustomId AND FKSpellId = @FKSpellId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FKSheetCustomId", relation.FKSheetCustomId);
                    command.Parameters.AddWithValue("@FKSpellId", relation.FKSpellId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
