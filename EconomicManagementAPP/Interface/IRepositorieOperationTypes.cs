using EconomicManagementAPP.Models;

namespace EconomicManagementAPP.Interface
{
    public interface IRepositorieOperationTypes
    {
        Task<IEnumerable<OperationTypes>> GetOperation();
        Task<string> GetOperationTypeByCategoryId(int categoryId, int userId);
    }
}
