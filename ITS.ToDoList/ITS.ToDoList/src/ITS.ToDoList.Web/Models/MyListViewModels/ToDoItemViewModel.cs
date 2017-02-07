using ITS.ToDoList.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITS.ToDoList.Web.Models.MyListViewModels
{
    public class ToDoItemViewModel
    {
		public ToDoItemViewModel() { }
		public ToDoItemViewModel(ToDoItemDetails item)
		{
			if (item == null)
				return;

			this.CategoryDescription = item.CategoryDescription;
			this.CategoryId = item.CategoryId;
			this.CategoryName = item.CategoryName;
			this.Completed = item.Completed;
			this.CompletedDate = item.CompletedDate;
			this.CompletedUser = item.CompletedUser;
			this.CompletedUserId = item.CompletedUserId;
			this.Content = item.Content;
			this.CreationDate = item.CreationDate;
			this.Id = item.Id;
			this.Title = item.Title;
			this.User = item.User;
			this.UserId = item.UserId;
		}

		public int Id { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public DateTime CreationDate { get; set; }
		public string UserId { get; set; }
		[DataType("BoolIcon")]
		public bool Completed { get; set; }
		public DateTime? CompletedDate { get; set; }
		public string CompletedUserId { get; set; }
		public int? CategoryId { get; set; }

		public string User { get; set; }
		public string CompletedUser { get; set; }
		public string CategoryName { get; set; }
		public string CategoryDescription { get; set; }
	}
}
