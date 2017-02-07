using ITS.ToDoList.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITS.ToDoList.Web.Models.MyListViewModels
{
	/// <summary>
	/// ViewModel per la pagina con l'elenco dei TO DO Items.
	/// Conterrà i filtri, elenco categorie, elenco toDoItems
	/// </summary>
    public class IndexViewModel
    {
		public IEnumerable<ToDoItemViewModel> Items { get; set; }
		//public IEnumerable<SelectListItem> Categories { get; set; }

		public FiltersModel Filters { get; set; }
	}
}
