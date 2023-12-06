using MacacosMazmorrasMVC.Models;
using System.Data.SqlClient;

namespace MacacosMazmorrasMVC.DAL
{
    public class SpellDAL
    {
        private readonly string connectionString; //Cadena de conexión con la base de datos

        public SpellDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
        //
        //Obtain all the spells from DB
        //
        public List<Spell> ObtainAllSpells()
        {
            List<Spell> spells = new List<Spell>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = $"SELECT * FROM Spell;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Spell spell = new Spell()
                            {
                                SpellId = Convert.ToInt32(reader["SpellId"]),
                                SpellName = reader["SpellName"].ToString(),
                                SpellDescription = reader["SpellDescription"].ToString(),
                                SpellRange = (reader["SpellRange"] != DBNull.Value) ? reader["SpellRange"].ToString() : (string?)null,
                                SpellDuration = reader["SpellDuration"].ToString(),
                                SpellMaterial = reader["SpellMaterial"].ToString(),
                                SpellConcentration = Convert.ToBoolean(reader["SpellConcentration"]),
                                SpellLevel = Convert.ToInt32(reader["SpellLevel"])
                            };
                            spells.Add(spell);
                        }
                    }
                }
            }
            return spells;
        }
        //
        //Obtains the SPELLS from a character in SHEETCUSTOM (receives sheetID)
        //
        public List<Spell> ObtainSheetSpells(int sheetCustomId)
        {
            List<Spell> spells = new List<Spell>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = $"SELECT * FROM Spell" +
                    $"INNER JOIN SheetCustom" +
                    $"ON SpellId = FKSpellId" +
                    $"WHERE FKSheetCustomId = {sheetCustomId};";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Spell spell = new Spell()
                            {
                                SpellId = Convert.ToInt32(reader["SpellId"]),
                                SpellName = reader["SpellName"].ToString(),
                                SpellDescription = reader["SpellDescription"].ToString(),
                                SpellRange = (reader["SpellRange"] != DBNull.Value) ? reader["SpellRange"].ToString() : (string?)null,
                                SpellDuration = reader["SpellDuration"].ToString(),
                                SpellMaterial = reader["SpellMaterial"].ToString(),
                                SpellConcentration = Convert.ToBoolean(reader["SpellConcentration"]),
                                SpellLevel = Convert.ToInt32(reader["SpellLevel"])
                            };
                            spells.Add(spell);
                        }
                    }
                }
            }
            return spells;
        }
    }
}
