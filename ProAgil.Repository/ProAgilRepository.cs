using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public class ProAgilRepository : IProAgilRepository
    {
        private readonly DataContext _context;
        public ProAgilRepository(DataContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
 
        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrante = false)
        {
            IQueryable<Evento> query = _context.Eventos.Include(i => i.Lotes).Include(i => i.RedesSociais);

            if(includePalestrante)
                query = query.Include(i => i.PalestrantesEventos).ThenInclude(i => i.Palestrante);

            query = query.OrderByDescending(o => o.DataEvento);

            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventosAsyncByTema(string tema, bool includePalestrante = false)
        {
            IQueryable<Evento> query = _context.Eventos.Include(i => i.Lotes).Include(i => i.RedesSociais);

            if(includePalestrante)
                query = query.Include(i => i.PalestrantesEventos).ThenInclude(i => i.Palestrante);

            query = query.Where(w => w.Tema.ToLower().Contains(tema.ToLower())).OrderByDescending(o => o.DataEvento);

            return await query.ToArrayAsync();
        }

        public async Task<Evento> GetEventoAsyncById(int eventoId, bool includePalestrante)
        {
            IQueryable<Evento> query = _context.Eventos.Include(i => i.Lotes).Include(i => i.RedesSociais);

            if(includePalestrante)
                query = query.Include(i => i.PalestrantesEventos).ThenInclude(i => i.Palestrante);

            query = query.Where(w => w.Id == eventoId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsyncByNome(string nome, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.Include(i => i.RedesSociais);

            if(includeEventos)
                query = query.Include(i => i.PalestrantesEventos).ThenInclude(i => i.Evento);

            query = query.Where(w => w.Nome.ToLower().Contains(nome.ToLower())).OrderBy(o => o.Nome);

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante> GetPalestranteAsyncById(int palestranteId, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.Include(i => i.RedesSociais);

            if(includeEventos)
                query = query.Include(i => i.PalestrantesEventos).ThenInclude(i => i.Evento);

            query = query.Where(w => w.Id == palestranteId);

            return await query.FirstOrDefaultAsync();
        }
    }
}