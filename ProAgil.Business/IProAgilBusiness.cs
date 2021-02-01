using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Business
{
    public interface IProAgilBusiness
    {
        void Add<T> (T entity) where T : class;
         void Update<T> (T entity) where T : class;
         void Delete<T> (T entity) where T : class;
         Task<bool> SaveChangesAsync();
         Task<Evento[]> GetAllEventosAsync(bool includePalestrante = false);
         Task<Evento[]> GetAllEventosAsyncByTema(string tema, bool includePalestrante = false);
         Task<Evento> GetEventoAsyncById(int eventoId, bool includePalestrante = false);
         Task<Palestrante> GetPalestranteAsyncById(int palestranteId, bool includeEventos = false);
        Task<Palestrante[]> GetAllPalestrantesAsyncByNome(string nome, bool includeEventos = false);
    }
}