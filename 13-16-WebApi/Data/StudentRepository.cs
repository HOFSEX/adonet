using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Data.SqlClient;
using DBConnect.Models;

namespace DBConnect.Data
{
    public class StudentRepository
    {
        private readonly string _connectionString;

        public StudentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Student> GetAll()
        {
            using var connection = new SqlConnection(_connectionString);
            return connection.Query<Student>(
                "SELECT StudentId, FirstName, LastName, Age, GroupId FROM Students"
            ).ToList();
        }

        public Student? GetById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            return connection.QueryFirstOrDefault<Student>(
                "SELECT StudentId, FirstName, LastName, Age, GroupId FROM Students WHERE StudentId = @Id",
                new { Id = id }
            );
        }

        public int CreateStudent(Student student)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = @"
                INSERT INTO Students (FirstName, LastName, Age, GroupId)
                VALUES (@FirstName, @LastName, @Age, @GroupId);
                SELECT CAST(SCOPE_IDENTITY() AS INT);";

            return connection.QuerySingle<int>(sql, student);
        }
    }
}
