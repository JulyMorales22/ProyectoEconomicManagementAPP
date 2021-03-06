using EconomicManagementAPP.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using EconomicManagementAPP.Interface;

namespace EconomicManagementAPP.Services
{
    public class RepositorieCategories : IRepositorieCategories
    {
        private readonly string connectionString;
        public RepositorieCategories(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task Create(Categories categories)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>($@"INSERT INTO Categories 
                                                (Name, OperationTypeId, UserId, DbStatus) 
                                                VALUES (@Name, @OperationTypeId, @UserId,  @DbStatus ); SELECT SCOPE_IDENTITY();", categories);
            categories.Id = id;
        }
        public async Task<bool> ExistingCategories(string name, int operationTypeId, int userId)
        {
            using var connection = new SqlConnection(connectionString);
            var exist = await connection.QueryFirstOrDefaultAsync<int>(
                                    @"SELECT 1
                                    FROM Categories
                                    WHERE Name = @Name AND OperationTypeId = @OperationTypeId AND UserId = @UserId;",
                                    new { name, operationTypeId, userId });
            return exist == 1;
        }

        public async Task<IEnumerable<Categories>> GetCategories(int UserId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Categories>(@"SELECT c.Id AS 'Id', c.Name AS 'Name', c.UserId AS 'UserId',c.OperationTypeId, ot.Description AS OperationTypeDescription
															FROM  Categories AS c 
															JOIN OperationTypes AS [ot] 
															ON c.OperationTypeId=ot.Id
				
                                                    WHERE [c].UserId=@UserId AND [c].DbStatus=1", new { UserId });


        }

       

        public async Task Modify(Categories categories)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE Categories
                                            SET Name = @Name
                                            WHERE Id = @Id", categories);
        }

        public async Task Delete(int id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync("DELETE  FROM Categories WHERE Id = @Id", new { id });
        }
        public async Task DeleteModify(int id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync("UPDATE Categories SET DbStatus=0  WHERE Id = @Id", new { id });
        }

        public async Task<Categories> GetCategorieByIds(int categoryId, int userId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Categories>(@"
                                                                SELECT Id, Name, OperationTypeId ,UserId
                                                                FROM Categories
                                                                WHERE Id = @categoryId  AND UserId = @UserId",
                                                                new { categoryId, userId });
        }
        public async Task<bool> ExistingCategoriesTransaction(int Id)
        {
            using var connection = new SqlConnection(connectionString);
            var exist = await connection.QueryFirstOrDefaultAsync<int>(
                                @"SELECT 1
                                    FROM Transactions
                                    WHERE CategoryId = @Id GROUP BY  CategoryId;",
                                new { Id });
            return exist == 1;

        }

    }
}
