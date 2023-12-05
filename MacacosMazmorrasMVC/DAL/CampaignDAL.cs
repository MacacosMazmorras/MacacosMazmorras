﻿using MacacosMazmorrasMVC.Models;
using Microsoft.Data.SqlClient;

namespace MacacosMazmorrasMVC.DAL
{
    public class CampaignDAL
    {
        private readonly string connectionString; //Cadena de conexión con la base de datos

        public CampaignDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
        //
        //Obtains all the campaigns from a User (Recieves USERID and returns a CAMPAIGN LIST)
        //
        public List<Campaign> ObtainAllUserCampaigns(int usuarioId)
        {
            List<Campaign> campaigns = new List<Campaign>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = $"SELECT * FROM Campaign WHERE FKUsuarioId = {usuarioId};";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Campaign campaign = new Campaign()
                            {
                                CampaignId = Convert.ToInt32(reader["CampaignId"]),
                                CampaignName = reader["CampaignName"].ToString(),
                                CampaignDesc = reader["CampaignDesc"].ToString(),
                                FKUsuarioId = Convert.ToInt32(reader["FKUsuarioId"]),
                                CampaignMap = (reader["CampaignMap"] != DBNull.Value) ? reader["CampaignMap"].ToString() : (string?)null
                            };
                            campaigns.Add(campaign);
                        }
                    }
                }
            }
            return campaigns;
        }
        //
        //Obtains a campaign from a User (recieves CAMPAIGNID and returns CAMPAIGN object)
        //--for the modify campaign form
        public Campaign ObtainUserCampaign(int campaignId)
        {
            Campaign campaign = new Campaign();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = $"SELECT * FROM Campaign WHERE CampaignId = {campaignId};";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            campaign.CampaignId = Convert.ToInt32(reader["CampaignId"]);
                            campaign.CampaignName = reader["CampaignName"].ToString();
                            campaign.CampaignDesc = reader["CampaignDesc"].ToString();
                            campaign.FKUsuarioId = Convert.ToInt32(reader["FKUsuarioId"]);
                            campaign.CampaignMap = (reader["CampaignMap"] != DBNull.Value) ? reader["CampaignMap"].ToString() : (string?)null;
                        }
                    }
                }
            }
            return campaign;
        }
        //
        //Inserts into the DB a new campaign (receives object)
        //
        public void InsertCampaign(Campaign campaign)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Campaign (CampaignName, CampaignDesc, CampaignMap, FKUsuarioId) " +
                               "VALUES (@CampaignName, @CampaignDesc, @CampaignMap, @FKUsuarioId)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CampaignName", campaign.CampaignName);
                    command.Parameters.AddWithValue("@CampaignDesc", campaign.CampaignDesc);
                    command.Parameters.AddWithValue("@CampaignMap", (object)campaign.CampaignMap ?? DBNull.Value);
                    command.Parameters.AddWithValue("@FKUsuarioId", campaign.FKUsuarioId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        //
        //Update an existing campaign (receives object)
        //
        public void UpdateCampaign(Campaign campaign)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Campaign " +
                               "SET CampaignName = @CampaignName, CampaignDesc = @CampaignDesc, CampaignMap = @CampaignMap" +
                               "WHERE CampaignId = @CampaignId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CampaignId", campaign.CampaignId);
                    command.Parameters.AddWithValue("@CampaignName", campaign.CampaignName);
                    command.Parameters.AddWithValue("@CampaignDesc", campaign.CampaignDesc);;
                    command.Parameters.AddWithValue("@CampaignMap", (object)campaign.CampaignMap ?? DBNull.Value);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        //
        //Deletes a camapign from DB (recieves ID)
        //
        public void DeleteCampaign(int campaignId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Campaign WHERE CampaignId = @CampaignId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CampaignId", campaignId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}