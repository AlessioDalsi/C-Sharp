using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITS.ToDoList.Data.Models
{
    public class ToDoItemDetails : ToDoItem
    {
		public string User { get; set; }

		public string CompletedUser { get; set; }

		public string CategoryName { get; set; }

		public string CategoryDescription { get; set; }


	}
}
