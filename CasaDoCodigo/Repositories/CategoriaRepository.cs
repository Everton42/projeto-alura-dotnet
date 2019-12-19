using CasaDoCodigo.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(ApplicationContext contexto) : base(contexto)
        {
        }

        public async Task<Categoria> SaveCategoria(string nome)
        {
            Categoria categoria = null;
            categoria = dbSet.Where(c => c.Nome == nome).SingleOrDefault();
            if (categoria == null)
            {
                categoria = new Categoria(nome);
                dbSet.Add(categoria);
                await contexto.SaveChangesAsync();
                return categoria;
            }
            return categoria;
        }
    }
}
