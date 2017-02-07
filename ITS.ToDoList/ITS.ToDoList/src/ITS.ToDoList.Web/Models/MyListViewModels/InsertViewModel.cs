using ITS.ToDoList.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITS.ToDoList.Web.Models.MyListViewModels
{
    public class InsertViewModel
    {
		[Display(Name = "Titolo")]
		[Required]
		[StringLength(50)]
		public string Title { get; set; }

		[Display(Name = "Testo")]
		public string Content { get; set; }

		[Display(Name = "Categoria")]
		public int? CategoryId { get; set; }

		public IEnumerable<SelectListItem> Categories { get; set; }
	}
}
