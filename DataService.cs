using Microsoft.Data.SqlClient;
using MVCANGULAR.Models;

namespace MVCANGULAR
{
    public class DataService
    {

        public async Task<List<Models.Employee>> GetEmployees()

        {

            string connectionString = "Data Source=10.20.12.197;Initial Catalog=ReportingTool;User ID=reportuser;Password=reportpassword;TrustServerCertificate=true;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT * FROM EmployeeDataRecord";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        List<Employee> employees = new List<Employee>();
                        while (await reader.ReadAsync())
                        {
                            Employee employee = new Employee
                            {
                                EmplId = Convert.ToInt32(reader["EmplId"]),
                                Education = reader["Education"].ToString(),
                                JoiningYear = Convert.ToInt32(reader["JoiningYear"]),
                                City = reader["City"].ToString(),
                                PaymentTier = Convert.ToInt32(reader["PaymentTier"]),
                                Age = Convert.ToInt32(reader["Age"]),
                                Gender = reader["Gender"].ToString(),
                                EverBenched = reader["EverBenched"].ToString(),
                                ExperienceInCurrentDomain = reader.IsDBNull(reader.GetOrdinal("ExperienceInCurrentDomain")) ? null : Convert.ToInt32(reader["ExperienceInCurrentDomain"]),
                                LeaveOrNot = reader["LeaveOrNot"].ToString()


                            };

                            employees.Add(employee);
                        }
                        return employees;
                    }
                }
            }
        }
    }
    }

