using JsLocalization.DAL;
using JsLocalization.Models;

namespace JsLocalization.Services
{
    public interface ICultureService
    {
        Culture GetByID(int id);
        bool Create(Culture role);
        bool Update(Culture role);
        bool Delete(int id);
        IQueryable<Culture> GetAllAsIQueryable();
    }
    public class CultureService : ICultureService
    {
        IRepository<Culture> _repository;

        public CultureService(IRepository<Culture> repository)
        {
            _repository = repository;
        }

        public Culture GetByID(int id)
        {
            var rep = _repository.Get(id);
            return rep;
        }

        public bool Create(Culture model)
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

        public bool Update(Culture model)
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

        public IQueryable<Culture> GetAllAsIQueryable()
        =>_repository.GetAsIQueryable();
    }
}
