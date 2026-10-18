using System.Collections.Generic;
using System.Linq;
using Dapper;
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
            using var connection = new SqlConnection(_connectionString);
            return connection.Query<Group>(
                "SELECT GroupId, GroupName FROM Groups"
            ).ToList();
        }

        public Group? GetById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            return connection.QueryFirstOrDefault<Group>(
                "SELECT GroupId, GroupName FROM Groups WHERE GroupId = @Id",
                new { Id = id }
            );
        }
    }
}
