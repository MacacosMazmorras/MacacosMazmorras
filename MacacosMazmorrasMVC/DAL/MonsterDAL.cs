using MacacosMazmorrasMVC.Models;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace MacacosMazmorrasMVC.DAL
{
    public class MonsterDAL
    {
        private readonly string connectionString; //Cadena de conexión con la base de datos

        public MonsterDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
        //
        //Obtains all the Mosters from DB
        //--for the glossary and lists
        public List<Monster> ObtainAllMonsters()

        {
            List<Monster> monsters = new List<Monster>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = $"SELECT * FROM Monster;";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Monster monster = new Monster()
                                {
                                    MonsterId = Convert.ToInt32(reader["MonsterId"]),
                                    Name = reader["MonsterName"].ToString(),
                                    MonsterType = reader["MonsterType"].ToString(),
                                    Ac = reader["MonsterAC"].ToString(),
                                    Hp = ExtractNumber(reader["MonsterHP"].ToString()),
                                    MonsterSpeed = reader["MonsterSpeed"].ToString(),
                                    Str = Convert.ToInt32(reader["MonsterStr"]),
                                    Dex = Convert.ToInt32(reader["MonsterDex"]),
                                    Con = Convert.ToInt32(reader["MonsterCon"]),
                                    Inte = Convert.ToInt32(reader["MonsterInt"]),
                                    Wis = Convert.ToInt32(reader["MonsterWis"]),
                                    Cha = Convert.ToInt32(reader["MonsterCha"]),
                                    MonsterCR = reader["MonsterCR"].ToString(),
                                    MonsterAction = (reader["MonsterActions"] != DBNull.Value) ? reader["MonsterActions"].ToString() : (string?)null,
                                    ImgUrl = (reader["MonsterImgUrl"] != DBNull.Value) ? reader["MonsterImgUrl"].ToString() : (string?)null
                                };
                                monsters.Add(monster);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Registra la excepción para poder depurar
                Console.WriteLine($"Error in ObtainAllMonsters: {ex.Message}");
                throw; // Vuelve a lanzar la excepción para que se propague hacia arriba
            }
            return monsters;
        }
        //
        //Obtains the Monsters for page (in this case 10 for page (receives page and cuantity for page)
        //
        public List<Monster> ObtainAllMonstersPaged(int page, int monstersQuanity)
        {
            List<Monster> monsters = new List<Monster>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                int startRow = (page - 1) * monstersQuanity + 1;
                int endRow = startRow + monstersQuanity - 1;

                string query = $"SELECT * FROM " +
                    $"(SELECT *, ROW_NUMBER() OVER (ORDER BY MonsterId) AS RowNum FROM Monster) " +
                    $"AS Sub WHERE RowNum " +
                    $"BETWEEN {startRow} AND {endRow}";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Monster monster = new Monster()
                            {
                                MonsterId = Convert.ToInt32(reader["MonsterId"]),
                                Name = reader["MonsterName"].ToString(),
                                MonsterType = reader["MonsterType"].ToString(),
                                Ac = reader["MonsterAC"].ToString(),
                                Hp = ExtractNumber(reader["MonsterHP"].ToString()),
                                MonsterSpeed = reader["MonsterSpeed"].ToString(),
                                Str = Convert.ToInt32(reader["MonsterStr"]),
                                Dex = Convert.ToInt32(reader["MonsterDex"]),
                                Con = Convert.ToInt32(reader["MonsterCon"]),
                                Inte = Convert.ToInt32(reader["MonsterInt"]),
                                Wis = Convert.ToInt32(reader["MonsterWis"]),
                                Cha = Convert.ToInt32(reader["MonsterCha"]),
                                MonsterCR = reader["MonsterCR"].ToString(),
                                MonsterAction = (reader["MonsterActions"] != DBNull.Value) ? reader["MonsterActions"].ToString() : (string?)null,
                                ImgUrl = (reader["MonsterImgUrl"] != DBNull.Value) ? reader["MonsterImgUrl"].ToString() : (string?)null
                            };
                            monsters.Add(monster);
                        }
                    }
                }
            }
            return monsters;
        }
        //
        //Obtains the Monsters from a Sesion (receives SESIONID)
        //
        public List<Monster> ObtainSesionMonsters(int sesionId)
        {
            List<Monster> monsters = new List<Monster>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = $"SELECT * FROM Monster " +
                    $"INNER JOIN SesionMonster " +
                    $"ON MonsterId=FKMonsterId " +
                    $"WHERE FKSesionId = {sesionId};";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Monster monster = new Monster()
                            {
                                MonsterId = Convert.ToInt32(reader["MonsterId"]),
                                Name = reader["MonsterName"].ToString(),
                                MonsterType = reader["MonsterType"].ToString(),
                                Ac = reader["MonsterAC"].ToString(),
                                Hp = ExtractNumber(reader["MonsterHP"].ToString()),
                                MonsterSpeed = reader["MonsterSpeed"].ToString(),
                                Str = Convert.ToInt32(reader["MonsterStr"]),
                                Dex = Convert.ToInt32(reader["MonsterDex"]),
                                Con = Convert.ToInt32(reader["MonsterCon"]),
                                Inte = Convert.ToInt32(reader["MonsterInt"]),
                                Wis = Convert.ToInt32(reader["MonsterWis"]),
                                Cha = Convert.ToInt32(reader["MonsterCha"]),
                                MonsterCR = reader["MonsterCR"].ToString(),
                                MonsterAction = (reader["MonsterActions"] != DBNull.Value) ? reader["MonsterActions"].ToString() : (string?)null,
                                ImgUrl = (reader["MonsterImgUrl"] != DBNull.Value) ? reader["MonsterImgUrl"].ToString() : (string?)null
                            };
                            monsters.Add(monster);
                        }
                    }
                }
            }
            return monsters;
        }

        //
        //Obtains the quantity os Monster for pagination
        //
        public int ObtainTotalMonsterCount()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Monster";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    return (int)command.ExecuteScalar();
                }
            }
        }

        public int ExtractNumber(string input)
        {
            // Define una expresión regular para encontrar el número
            string pattern = @"\d+"; // \d+ coincide con uno o más dígitos
                                     // Busca coincidencias en la cadena de entrada
            Match match = Regex.Match(input, pattern);

            // Verifica si se encontró una coincidencia
            if (match.Success)
            {
                // Intenta convertir la coincidencia a un número entero
                if (int.TryParse(match.Value, out int result))
                {
                    return result;
                }
            }
            // Si no se pudo extraer un número, puedes manejarlo como desees (lanzar una excepción, devolver un valor predeterminado, etc.)
            throw new InvalidOperationException("No se pudo extraer un número válido de la cadena.");
        }
    }
}
