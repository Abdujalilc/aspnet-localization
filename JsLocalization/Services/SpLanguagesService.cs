using JsLocalization.DAL;
using JsLocalization.Models;

namespace JsLocalization.Services
{
    public interface ISpLanguagesService
    {
        SpLanguage GetByID(int id);
        bool Create(SpLanguage role);
        bool Update(SpLanguage role);
        bool Delete(int id);
        IQueryable<SpLanguage> GetAllAsIQueryable();
    }
    public class SpLanguagesService : ISpLanguagesService
    {
        IRepository<SpLanguage> _repository;

        public SpLanguagesService(IRepository<SpLanguage> repository)
        {
            _repository = repository;
        }

        public SpLanguage GetByID(int id)
        {
            var rep = _repository.Get(id);
            return rep;
        }

        public bool Create(SpLanguage model)
        {
            try
            {
                _repository.Insert(model);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(SpLanguage model)
        {
            try
            {
                _repository.Update(model);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var fd = _repository.Get(id);
                _repository.Delete(fd);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public IQueryable<SpLanguage> GetAllAsIQueryable()
        =>_repository.GetAsIQueryable();
    }
}
