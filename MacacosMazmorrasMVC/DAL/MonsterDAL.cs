using MacacosMazmorrasMVC.Models;
using System.Data.SqlClient;

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
                                MonsterName = reader["MonsterName"].ToString(),
                                MonsterSize = reader["MonsterSize"].ToString(),
                                MonsterType = reader["MonsterType"].ToString(),
                                MonsterAC = Convert.ToInt32(reader["MonsterAC"]),
                                MonsterHP = Convert.ToInt32(reader["MonsterHP"]),
                                MonsterSpeed = Convert.ToInt32(reader["MonsterSpeed"]),
                                MonsterStr = Convert.ToInt32(reader["MonsterStr"]),
                                MonsterDex = Convert.ToInt32(reader["MonsterDex"]),
                                MonsterCon = Convert.ToInt32(reader["MonsterCon"]),
                                MonsterInt = Convert.ToInt32(reader["MonsterInt"]),
                                MonsterWis = Convert.ToInt32(reader["MonsterWis"]),
                                MonsterCha = Convert.ToInt32(reader["MonsterCha"]),
                                MonsterCR = Convert.ToDecimal(reader["MonsterCR"]),
                                MonsterXP = Convert.ToInt32(reader["MonsterXP"]),
                                MonsterImgUrl = (reader["MonsterImgUrl"] != DBNull.Value) ? reader["MonsterImgUrl"].ToString() : (string?)null
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
                string query = $"SELECT * FROM Monster" +
                    $"INNER JOIN SesionMonster" +
                    $"ON MonsterId=FKMonsterId" +
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
                                MonsterName = reader["MonsterName"].ToString(),
                                MonsterSize = reader["MonsterSize"].ToString(),
                                MonsterType = reader["MonsterType"].ToString(),
                                MonsterAC = Convert.ToInt32(reader["MonsterAC"]),
                                MonsterHP = Convert.ToInt32(reader["MonsterHP"]),
                                MonsterSpeed = Convert.ToInt32(reader["MonsterSpeed"]),
                                MonsterStr = Convert.ToInt32(reader["MonsterStr"]),
                                MonsterDex = Convert.ToInt32(reader["MonsterDex"]),
                                MonsterCon = Convert.ToInt32(reader["MonsterCon"]),
                                MonsterInt = Convert.ToInt32(reader["MonsterInt"]),
                                MonsterWis = Convert.ToInt32(reader["MonsterWis"]),
                                MonsterCha = Convert.ToInt32(reader["MonsterCha"]),
                                MonsterCR = Convert.ToDecimal(reader["MonsterCR"]),
                                MonsterXP = Convert.ToInt32(reader["MonsterXP"]),
                                MonsterImgUrl = (reader["MonsterImgUrl"] != DBNull.Value) ? reader["MonsterImgUrl"].ToString() : (string?)null
                            };
                            monsters.Add(monster);
                        }
                    }
                }
            }
            return monsters;
        }
    }
}
