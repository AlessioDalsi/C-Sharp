using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITS.ToDoList.Data.Models
{
	public class ToDoItem
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public DateTime CreationDate { get; set; }
		public string UserId { get; set; }
		public bool Completed { get; set; }
		public DateTime? CompletedDate { get; set; }
		public string CompletedUserId { get; set; }
		public int? CategoryId { get; set; }

	}
}
