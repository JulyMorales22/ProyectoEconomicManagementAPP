using EconomicManagementAPP.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using EconomicManagementAPP.Interface;

namespace EconomicManagementAPP.Services
{
    public class RepositorieOperationTypes : IRepositorieOperationTypes
    {
        private readonly string connectionString;

        public RepositorieOperationTypes(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
       

        public async Task<IEnumerable<OperationTypes>> GetOperation()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<OperationTypes>(@"SELECT Id,Description
                                                            FROM OperationTypes
                                                            ORDER BY Id");
        }

      

        public async Task<string> GetOperationTypeByCategoryId(int categoryId, int userId)
        {
            using var connection = new SqlConnection(connectionString);
            var description = await connection.QuerySingleAsync<string>(@"SELECT o.Description
                                                                FROM Categories as c
                                                                JOIN OperationTypes as o
                                                                ON c.OperationTypeId=o.Id
                                                                WHERE c.Id=@categoryId  AND c.UserId = @UserId", new { categoryId, userId });
            return description;
        }
    }
}
