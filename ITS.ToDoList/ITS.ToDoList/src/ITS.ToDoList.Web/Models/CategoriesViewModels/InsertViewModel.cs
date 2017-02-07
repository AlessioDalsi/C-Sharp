using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITS.ToDoList.Web.Models.CategoriesViewModels
{
    public class InsertViewModel
    {
		[Required]
		[StringLength(25)]
		public string Name { get; set; }

		[DataType(DataType.MultilineText)]
		[StringLength(250)]
		public string Description { get; set; }
	}
}
