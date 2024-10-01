using FoodHub.Model;

namespace FoodHub.Data.Repository.IRepository
{
    public interface IAccountRepository
    {
        Task<Admin> GetAdminByEmailAsync(string email);
        Task AddAdminAsync(Admin admin);
    }
}
