using ITS.ToDoList.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITS.ToDoList.Data
{
    public interface ICategoryRepository
    {
		/// <summary>
		/// Inserimento di una nuova categoria
		/// </summary>
		/// <param name="category">Categoria da inserire</param>
		void Insert(Category category);

		/// <summary>
		/// Elenco di tutte le categorie
		/// </summary>
		IEnumerable<Category> GetList();
    }
}
