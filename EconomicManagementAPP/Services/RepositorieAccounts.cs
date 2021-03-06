using EconomicManagementAPP.Models;
using Microsoft.Data.SqlClient;
using Dapper;
using EconomicManagementAPP.Interface;

namespace EconomicManagementAPP.Services
{
    public class RepositorieAccounts : IRepositorieAccounts
    {
        private readonly string connectionString;

        public RepositorieAccounts(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task Create(Accounts accounts)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>($@"INSERT INTO Accounts 
                                                (Name, AccountTypeId, Balance, Description, DbStatus) 
                                                VALUES (@Name, @AccountTypeId, @Balance, @Description, @DbStatus ); SELECT SCOPE_IDENTITY();", accounts);
            accounts.Id = id;
        }

        public async Task<bool> ExistingAccount(string Name, int AccountTypeId)
        {
            using var connection = new SqlConnection(connectionString);
            var exist = await connection.QueryFirstOrDefaultAsync<int>(
                                    @"SELECT 1
                                    FROM Accounts
                                    WHERE Name = @Name AND AccountTypeId = @AccountTypeId;",
                                    new { Name, AccountTypeId });
            return exist == 1;
        }

        // Obtenemos las cuentas del usuario
        public async Task<IEnumerable<Accounts>> GetAccounts(int UserId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Accounts>(@"SELECT a.Id AS 'Id', a.Name AS 'Name', [at].Id AS 'AccountTypeId', a.Balance AS 'Balance', a.[Description] AS 'Description'
                                                        FROM  Accounts AS a 
                                                        JOIN AccountTypes AS [at] 
                                                        ON a.AccountTypeId=at.Id
                                                        JOIN Users AS u
                                                        ON u.Id=[at].UserId
                                                        WHERE [at].UserId=@UserId AND [a].DbStatus=1  AND [at].DbStatus=1 AND u.DbStatus=1", new { UserId });
        }
        public async Task Modify(Accounts accounts)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE Accounts
                                            SET Name = @Name, Description = @Description, Balance=@Balance
                                            WHERE Id = @Id", accounts);
        }

        public async Task<Accounts> GetAccountById(int id)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Accounts>(@"
                                                                SELECT Id, Name, AccountTypeId ,Balance, Description, dbStatus
                                                                FROM Accounts
                                                                WHERE Id = @Id  AND dbStatus=1",
                                                                new { id });
        }

        public async Task DeleteModify(int id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync("UPDATE Accounts SET DbStatus=0  WHERE Id = @Id", new { id });
        }

        public async Task Delete(int id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync("DELETE  FROM Accounts WHERE Id = @Id", new { id });
        }
        public async Task<int> GetNumberTransaction(int id)
        {
          
            using var connection = new SqlConnection(connectionString);
            int numberTransaction = await connection.QuerySingleAsync<int>("SELECT COUNT (AccountId) AS CantidadTransaciones FROM Transactions WHERE AccountId = @Id;", new { id });
      

            return numberTransaction;
        }
        public async Task<bool> ExistingAccountTransaction(int Id)
        {
            using var connection = new SqlConnection(connectionString);
            var exist = await connection.QueryFirstOrDefaultAsync<int>(
                                @"SELECT 1
                                    FROM Transactions
                                    WHERE AccountId = @Id GROUP BY  AccountId;",
                                new { Id });
            return exist == 1;

        }


    }
}
