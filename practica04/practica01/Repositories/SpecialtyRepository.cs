using practica01.Data;
using practica01.Models;

namespace practica01.Repositories
{
    public class SpecialtyRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public SpecialtyRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public List<SpecialtyModel> GetAll()
        {
            return _applicationDbContext.specialtyModel.ToList();
        }

        public SpecialtyModel? GetById(int id)
        {
            return _applicationDbContext.specialtyModel.FirstOrDefault(p => p.Id == id);
        }

        public void Add(SpecialtyModel specialtyModel)
        {
            _applicationDbContext.specialtyModel.Add(specialtyModel);
            _applicationDbContext.SaveChanges();
        }

        public void Update(SpecialtyModel specialtyModel)
        {
            _applicationDbContext.specialtyModel.Update(specialtyModel);
            _applicationDbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var specialty = _applicationDbContext.specialtyModel.Find(id);

            if (specialty != null)
            {
                _applicationDbContext.specialtyModel.Remove(specialty);
                _applicationDbContext.SaveChanges();
            }
        }
    }
}
