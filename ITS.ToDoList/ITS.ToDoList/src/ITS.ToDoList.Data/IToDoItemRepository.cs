using ITS.ToDoList.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITS.ToDoList.Data
{
    public interface IToDoItemRepository
    {
		/// <summary>
		/// Inserimento di un elemento della TO DO List
		/// </summary>
		/// <param name="item">Elemento da inserire</param>
		void Insert(ToDoItem item);

		/// <summary>
		/// Ritorna tutti gli elementi della lista
		/// </summary>
		/// <param name="userId">Identificativo dell'utente</param>
		IEnumerable<ToDoItemDetails> GetAll(string userId);

		/// <summary>
		/// Ritorna tutti gli elementi della lista
		/// </summary>
		/// <param name="userId">Identificativo dell'utente</param>
		/// <param name="filterText">Testo da utilizzare come filtro</param>
		IEnumerable<ToDoItemDetails> GetAll(string userId, string filterText);

		/// <summary>
		/// Ritorno un elemento della to do list
		/// </summary>
		/// <param name="userId">Identificativo dell'utente</param>
		/// <param name="id">Identificativo dell'elemento da recuperare</param>
		ToDoItemDetails Get(string userId, int id);

		/// <summary>
		/// Marca un'attività come completata
		/// </summary>
		/// <param name="id">Identificativo to do item</param>
		/// <param name="userId">Identificativo utente</param>
		void SetCompleted(int id, string userId);
	}
}
