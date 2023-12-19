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
                string query = "SELECT * FROM Spell";
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
        //Obtains the SPELLS for page (in this case 10 for page (receives page and cuantity for page)
        //
        public List<Spell> ObtainAllSpellsPaged(int page, int spellsQuanity)
        {
            List<Spell> spells = new List<Spell>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                int startRow = (page - 1) * spellsQuanity + 1;
                int endRow = startRow + spellsQuanity - 1;

                string query = $"SELECT * FROM ( " +
                               $"SELECT *, ROW_NUMBER() OVER (ORDER BY SpellId) AS RowNum FROM Spell) AS Sub " +
                               $"WHERE RowNum BETWEEN {startRow} AND {endRow}";

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
                    $"WHERE FKSheetCustomId = @SheetCustomId;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SheetCustomId", sheetCustomId);

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
        //Obtains the quantity os spells for pagination
        //
        public int ObtainTotalSpellCount()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Spell";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    return (int)command.ExecuteScalar();
                }
            }
        }

        //
        //Obtains the Spells for page (in this case 10 for page (receives page and cuantity for page)
        //ObtainSpellsSearchResult
        //
        public List<Spell> ObtainSpellsSearchResultPaged(string searchTerm, int page, int spellsQuantity)
        {
            List<Spell> spells = new List<Spell>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                int startRow = (page - 1) * spellsQuantity + 1;
                int endRow = startRow + spellsQuantity - 1;


                string query = $"SELECT * FROM (" +
                               $"SELECT *, ROW_NUMBER() OVER (ORDER BY SpellId) AS RowNum FROM Spell WHERE SpellName LIKE @searchTerm) AS Sub " +
                               $"WHERE RowNum BETWEEN {startRow} AND {endRow}";


                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@searchTerm", $"{searchTerm}%"); //buscamos cualquier coincidencia entera o parcial sobre searchTerm
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
        //Obtains the quantity search of Spells for pagination
        //
        public int ObtainTotalSpellsCountSearched(string searchTerm)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Spell WHERE SpellName LIKE @searchTerm";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@searchTerm", $"{searchTerm}%"); //buscamos cualquier coincidencia entera o parcial sobre searchTerm
                    connection.Open();
                    return (int)command.ExecuteScalar();
                }
            }
        }
    }
}
