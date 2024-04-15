using JsLocalizeText.DAL;
using JsLocalizeText.Models;

namespace JsLocalizeText.Services
{
    public interface ICultureService
    {
        IQueryable<Culture> GetAllAsIQueryable();
    }
    public class CultureService : ICultureService
    {
        IRepository<Culture> _repository;

        public CultureService(IRepository<Culture> repository)
        {
            _repository = repository;
        }
        public IQueryable<Culture> GetAllAsIQueryable()
        =>_repository.GetAsIQueryable();
    }
}
