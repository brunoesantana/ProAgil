using System.Threading.Tasks;
using ProAgil.Domain;
using ProAgil.Repository;

namespace ProAgil.Business
{
    public class ProAgilBusiness : IProAgilBusiness
    {
        private readonly IProAgilRepository _repository;

        public ProAgilBusiness(IProAgilRepository repository)
        {
            _repository = repository;
        }

        public void Add<T>(T entity) where T : class
        {
            _repository.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _repository.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _repository.Delete(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _repository.SaveChangesAsync();
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrante = false)
        {
            return await _repository.GetAllEventosAsync(includePalestrante);
        }

        public async Task<Evento[]> GetAllEventosAsyncByTema(string tema, bool includePalestrante = false)
        {
            return await _repository.GetAllEventosAsyncByTema(tema, includePalestrante);
        }
     
        public async Task<Evento> GetEventoAsyncById(int eventoId, bool includePalestrante = false)
        {
            return await _repository.GetEventoAsyncById(eventoId, includePalestrante);
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsyncByNome(string nome, bool includeEventos = false)
        {
            return await _repository.GetAllPalestrantesAsyncByNome(nome, includeEventos);
        }

        public async Task<Palestrante> GetPalestranteAsyncById(int palestranteId, bool includeEventos = false)
        {
            return await _repository.GetPalestranteAsyncById(palestranteId, includeEventos);
        }
    }
}