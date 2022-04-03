using EconomicManagementAPP.Models;

namespace EconomicManagementAPP.Interface
{
    public interface IRepositorieAccounts
    {
        Task Create(Accounts accounts);
        Task<bool> ExistingAccount(string Name, int AccountTypeId);
        Task<IEnumerable<Accounts>> GetAccounts(int userId = 1);
        Task Modify(Accounts accounts);
        Task<Accounts> GetAccountById(int id);
        Task Delete(int id);
        Task DeleteModify(int id); //solo cambiar el estado en la base de datos
        Task<int> GetNumberTransaction(int id); //obtener el numero de transacciones
        Task<bool> ExistingAccountTransaction(int Id); //verificar si la cuenta tiene transacciones


    }
}
