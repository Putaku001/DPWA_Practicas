using practica11.Data;
using practica11.Models;

namespace practica11.Repositories
{
    public class RoleRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public RoleRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public List<RoleModel> GetAllRoles()
        {
            return _applicationDbContext.RoleModel.ToList();
        }

        public RoleModel? GetRoleById(int id)
        {
            return _applicationDbContext.RoleModel.FirstOrDefault(r => r.Id == id);
        }

        public void AddRole(RoleModel role)
        {
            _applicationDbContext.RoleModel.Add(role);
            _applicationDbContext.SaveChanges();
        }

        public void UpdateRole(RoleModel role)
        {
            _applicationDbContext.RoleModel.Update(role);
            _applicationDbContext.SaveChanges();
        }

        public void DeleteRole(int id)
        {
            var role = GetRoleById(id);
            if (role != null)
            {
                _applicationDbContext.RoleModel.Remove(role);
                _applicationDbContext.SaveChanges();
            }
        }
    }
}
