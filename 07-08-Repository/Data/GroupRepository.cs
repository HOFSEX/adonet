using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using DBConnect.Models;

namespace DBConnect.Data
{
    public class GroupRepository
    {
        private readonly string _connectionString;

        public GroupRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Group> GetAll()
        {
            var result = new List<Group>();

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var command = new SqlCommand(
                "SELECT GroupId, GroupName FROM Groups", connection);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new Group
                {
                    GroupId = reader.GetInt32(0),
                    GroupName = reader.GetString(1)
                });
            }

            return result;
        }
    }
}
