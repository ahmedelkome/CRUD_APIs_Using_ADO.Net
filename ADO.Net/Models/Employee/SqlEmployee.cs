using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace ADO.Net.Models.Employee
{
    public class SqlEmployee : IEmployee
    {
        public readonly IConfiguration _configure;
        public SqlEmployee(IConfiguration configure)
        {
            _configure = configure;
        }



        public Employee SelectEmployee(int id)
        {
            Employee employee = null;

            using (SqlConnection connection = new SqlConnection(_configure.GetSection("Connection Strings").Value))
            using (SqlCommand command = new SqlCommand("", connection))
            {

                string myQuery = "SELECT * FROM Employees";

                if (id > 0)
                {
                    myQuery = myQuery + " WHERE id = " + id.ToString();
                }
                command.CommandText = myQuery;
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    employee = new Employee
                    {
                        id = !reader.IsDBNull(reader.GetOrdinal("id")) ? reader.GetInt32(reader.GetOrdinal("id")) : 0, // أو قيمة افتراضية مناسبة
                        fName = !reader.IsDBNull(reader.GetOrdinal("fName")) ? reader.GetString(reader.GetOrdinal("fName")) : string.Empty,
                        lName = !reader.IsDBNull(reader.GetOrdinal("lName")) ? reader.GetString(reader.GetOrdinal("lName")) : string.Empty,
                        age = !reader.IsDBNull(reader.GetOrdinal("age")) ? reader.GetInt32(reader.GetOrdinal("age")) : 0,
                        email = !reader.IsDBNull(reader.GetOrdinal("email")) ? reader.GetString(reader.GetOrdinal("email")) : string.Empty,
                        salary = !reader.IsDBNull(reader.GetOrdinal("salary")) ? reader.GetDecimal(reader.GetOrdinal("salary")) : 0,
                        phone = !reader.IsDBNull(reader.GetOrdinal("phone")) ? reader.GetString(reader.GetOrdinal("phone")) : string.Empty,
                        country = !reader.IsDBNull(reader.GetOrdinal("country")) ? reader.GetString(reader.GetOrdinal("country")) : string.Empty,
                        state = !reader.IsDBNull(reader.GetOrdinal("state")) ? reader.GetString(reader.GetOrdinal("state")) : string.Empty,
                        gender = !reader.IsDBNull(reader.GetOrdinal("gender")) ? reader.GetString(reader.GetOrdinal("gender")) : string.Empty
                    };

                }
                connection.Close();
                return employee;
            }
        }

        public Employee SaveEmployee(Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(_configure.GetSection("Connection Strings").Value))
            using (SqlCommand command = new SqlCommand("", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SaveOrUpdateEmployee ";
                connection.Open();
                command.Parameters.AddWithValue("@Id", employee.id);
                command.Parameters.AddWithValue("@FName", employee.fName);
                command.Parameters.AddWithValue("@LName", employee.lName);
                command.Parameters.AddWithValue("@age", employee.age);
                command.Parameters.AddWithValue("@salary", employee.salary);
                command.Parameters.AddWithValue("@country", employee.country);
                command.Parameters.AddWithValue("@phone", employee.phone);
                command.Parameters.AddWithValue("@gender", employee.gender);
                command.Parameters.AddWithValue("@email", employee.email);
                command.Parameters.AddWithValue("@state", employee.state);

                var rows = command.ExecuteNonQuery();

                if (rows > 0)
                {
                    return employee;
                }
                else
                {
                    return null;
                }
            }

        }

        public bool DeleteEmployee(int id)
        {
            using(SqlConnection connection = new SqlConnection(_configure.GetSection("Connection Strings").Value))
                using (SqlCommand command = new SqlCommand("", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "DeleteEmployee";
                connection.Open();
                command.Parameters.AddWithValue("@id", id);
                var row = command.ExecuteNonQuery();

                if (row != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}

