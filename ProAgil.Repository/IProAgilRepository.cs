using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public interface IProAgilRepository
    {
         void Add<T> (T entity) where T : class;
         void Update<T> (T entity) where T : class;
         void Delete<T> (T entity) where T : class;
         Task<bool> SaveChangesAsync();
         Task<Evento[]> GetAllEventosAsync(bool includePalestrante);
         Task<Evento[]> GetAllEventosAsyncByTema(string tema, bool includePalestrante);
         Task<Evento> GetEventoAsyncById(int eventoId, bool includePalestrante);
         Task<Palestrante> GetPalestranteAsyncById(int palestranteId, bool includeEventos);
        Task<Palestrante[]> GetAllPalestrantesAsyncByNome(string nome, bool includeEventos);
    }
}