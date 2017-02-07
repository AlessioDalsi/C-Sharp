using ITS.ToDoList.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITS.ToDoList.Web.Models.MyListViewModels
{
    public class UpdateViewModel
	{
		public UpdateViewModel() { }
		public UpdateViewModel(ToDoItemDetails item)
		{
			if (item == null)
				return;

			this.Id = item.Id;
			this.CategoryId = item.CategoryId;
			this.Completed = item.Completed;
			this.CompletedDate = item.CompletedDate;
			this.CompletedUser = item.CompletedUser;
			this.Content = item.Content;
			this.CreationDate = item.CreationDate;
			this.CreationUser = item.User;
			this.Title = item.Title;
		}


		[Required]
		public int Id { get; set; }
		[Display(Name = "Titolo")]
		[Required]
		[StringLength(50)]
		public string Title { get; set; }
		[Display(Name = "Testo")]
		public string Content { get; set; }
		[Display(Name = "Categoria")]
		public int? CategoryId { get; set; }
		public IEnumerable<SelectListItem> Categories { get; set; }

		[Display(Name = "Completata")]
		public bool Completed { get; set; }
		[Display(Name = "Data di completamento")]
		public DateTime? CompletedDate { get; set; }
		[Display(Name = "Utente che ha completato")]
		public string CompletedUser { get; set; }

		//[DataType(DataType.Date)]
		//[DataType("MyDateTime")]
		[Display(Name = "Data di creazione")]
		public DateTime CreationDate { get; set; }
		[Display(Name = "Utente che ha creato")]
		public string CreationUser { get; set; }

	}
}
