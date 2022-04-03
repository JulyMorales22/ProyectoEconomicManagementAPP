using EconomicManagementAPP.Models;

namespace EconomicManagementAPP.Interface
{
    public interface IRepositorieTransactions
    {
        Task Create(Transactions transactions);
        Task<IEnumerable<Transactions>> GetTransactions(int AccountId);
        Task<IEnumerable<Transactions>> GetAllTransactions(int userId);
    }

    
}