using MacacosMazmorrasMVC.Models;
using System.Data.SqlClient;

namespace MacacosMazmorrasMVC.DAL
{
    public class TypeSheetDAL
    {
        private readonly string connectionString; //Cadena de conexión con la base de datos

        public TypeSheetDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
        //
        //Obtains all the Types
        //
        public List<TypeSheet> ObtainAllTypes()
        {
            List<TypeSheet> types = new List<TypeSheet>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = $"SELECT * FROM TypeSheet;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TypeSheet type = new TypeSheet()
                            {
                                TypeSheetId = Convert.ToInt32(reader["TypeSheetId"]),
                                TypeSheetClass = reader["TypeSheetClass"].ToString(),
                            };
                            types.Add(type);
                        }
                    }
                }
            }
            return types;
        }
    }
}
