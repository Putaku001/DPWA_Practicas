using practica01.Data;
using practica01.Models;

namespace practica01.Repositories
{
    public class AuthRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public AuthRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public bool RegisterUser(UserModel userModel)
        {
            try
            {
                _applicationDbContext.UserModel.Add(userModel);
                _applicationDbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public UserModel? ValidateUser(string email, string password)
        {
            return _applicationDbContext.UserModel.FirstOrDefault(u => u.Email == email && u.Password == password);
        }
    }
}
