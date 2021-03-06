using Dapper;
using EconomicManagementAPP.Models;
using Microsoft.Data.SqlClient;
namespace EconomicManagementAPP.Interface;

public class RepositorieTransactions : IRepositorieTransactions
{
    private readonly string connectionString;
    public RepositorieTransactions(IConfiguration configuration)
    {
        connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<IEnumerable<Transactions>> GetTransactions(int AccountId)
    {
        using var connection = new SqlConnection(connectionString);
        return await connection.QueryAsync<Transactions>(@"SELECT TOP (10) t.Id, t.UserId, t.TransactionDate, t.Total, [ot].Description as OperationTypeDescription, t.Description, a.Name as AccountName, c.Name as 'CategoryName' 
            FROM Transactions as t 
            JOIN Categories as c ON t.CategoryId=c.Id
            JOIN Accounts as a ON t.AccountId=a.Id 
            JOIN OperationTypes AS [ot] ON  [ot].Id=c.OperationTypeId
            WHERE t.AccountId = @AccountId
            ORDER BY 3 DESC", new { AccountId });
    }

    public async Task Create(Transactions transactions)
    {
        using var connection = new SqlConnection(connectionString);

        var id = await connection.QuerySingleAsync<int>(@"INSERT INTO Transactions 
                                                (UserId, TransactionDate, Total, Description, AccountId, CategoryId) 
                                                VALUES (@UserId, @TransactionDate, @Total, @Description, @AccountId, @CategoryId);
                                                SELECT SCOPE_IDENTITY();", transactions);
        transactions.Id = id;
    }
    public async Task<IEnumerable<Transactions>> GetAllTransactions(int userId)
    {
        using var connection = new SqlConnection(connectionString);
        return await connection.QueryAsync<Transactions>(@"SELECT  [t].Description ,[t].[Total] 
                                                             FROM Transactions AS [t]
                                                             JOIN Users AS u
                                                             ON u.Id=[t].UserId
                                                             WHERE [t].UserId=@UserId  AND u.DbStatus=1", new { userId });
    }
}
