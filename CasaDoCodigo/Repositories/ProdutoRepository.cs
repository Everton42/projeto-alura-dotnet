using CasaDoCodigo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        private readonly ICategoriaRepository categoriaRepositorio;

        public ProdutoRepository(ApplicationContext contexto, ICategoriaRepository categoriaRepositorio) : base(contexto)
        {
            this.categoriaRepositorio = categoriaRepositorio;
        }


        public IList<Produto> GetProdutos()
        {
            return dbSet.ToList();
        }

        public async Task SaveProdutos(List<Livro> livros)
        {
            try
            {
                foreach (var livro in livros)
                {
                    if (!dbSet.Where(p => p.Codigo == livro.Codigo).Any())
                    {
                        var categoria = await categoriaRepositorio.SaveCategoria(livro.Categoria);
                        dbSet.Add(new Produto(livro.Codigo, livro.Nome, livro.Preco, categoria));
                    }
                }
                await contexto.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    public class Livro
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public string Subcategoria { get; set; }
        public decimal Preco { get; set; }
    }
}
