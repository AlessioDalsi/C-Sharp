using Dapper;
using ITS.ToDoList.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Memory;

namespace ITS.ToDoList.Data
{
    public class SqlToDoItemRepository : IToDoItemRepository
    {
		string _connectionString;

		//public SqlToDoItemRepository(string connectionString)
		//{
		//	this._connectionString = connectionString;
		//}

		public SqlToDoItemRepository(IConfiguration configuration, IMemoryCache cache)
		{
			this._connectionString = configuration.GetConnectionString("DefaultConnection");
		}

		/// <summary>
		/// Inserimento di un elemento della TO DO List
		/// </summary>
		/// <param name="item">Elemento da inserire</param>
		public void Insert(ToDoItem item)
		{
			using (var connection = new SqlConnection(this._connectionString))
			{
				connection.Open();

				connection.Query(@"
INSERT INTO [dbo].[ToDoItems] ([Name], [Content], [UserId], [CategoryId])
VALUES (@Title ,@Content ,@UserId ,@CategoryId)", item);
			}
		}

		/// <summary>
		/// Ritorna tutti gli elementi della lista
		/// </summary>
		/// <param name="userId">Identificativo dell'utente</param>
		public IEnumerable<ToDoItemDetails> GetAll(string userId)
		{
			using (var connection = new SqlConnection(this._connectionString))
			{
				connection.Open();
				return connection.Query<ToDoItemDetails>(@"
SELECT t.Id ,
       t.[Name] as [Title] ,
       t.Content ,
       t.CreationDate ,
       t.UserId ,
	   u1.UserName AS [User],
       t.CategoryId ,
	   c.[Name] AS [CategoryName],
	   c.[Description] AS [CategoryDescription],
       t.Completed ,
       t.CompletedDate ,
       t.CompletedUserId,
	   u2.UserName AS [CompletedUser]
	    FROM dbo.ToDoItems t
INNER JOIN dbo.AspNetUsers u1 ON u1.Id = t.UserId
LEFT JOIN dbo.AspNetUsers u2 ON u2.Id = t.CompletedUserId
LEFT JOIN dbo.Categories c ON c.Id = t.CategoryId
WHERE t.UserId = @UserId", new { UserId = userId });
			}
		}

		/// <summary>
		/// Ritorna tutti gli elementi della lista
		/// </summary>
		/// <param name="userId">Identificativo dell'utente</param>
		/// <param name="filterText">Testo da utilizzare come filtro</param>
		public IEnumerable<ToDoItemDetails> GetAll(string userId, string filterText)
		{
			if (!string.IsNullOrEmpty(filterText))
				filterText = $"%{filterText}%";

			using (var connection = new SqlConnection(this._connectionString))
			{
				connection.Open();
				return connection.Query<ToDoItemDetails>(@"
SELECT t.Id ,
       t.[Name] as [Title] ,
       t.Content ,
       t.CreationDate ,
       t.UserId ,
	   u1.UserName AS [User],
       t.CategoryId ,
	   c.[Name] AS [CategoryName],
	   c.[Description] AS [CategoryDescription],
       t.Completed ,
       t.CompletedDate ,
       t.CompletedUserId,
	   u2.UserName AS [CompletedUser]
	    FROM dbo.ToDoItems t
INNER JOIN dbo.AspNetUsers u1 ON u1.Id = t.UserId
LEFT JOIN dbo.AspNetUsers u2 ON u2.Id = t.CompletedUserId
LEFT JOIN dbo.Categories c ON c.Id = t.CategoryId
WHERE t.UserId = @UserId and (@text is NULL OR @text = '' OR t.[Name] like @text OR t.Content like @text)", 
				new {
					UserId = userId,
					text = filterText });
			}
		}

		/// <summary>
		/// Ritorno un elemento della to do list
		/// </summary>
		/// <param name="userId">Identificativo dell'utente</param>
		/// <param name="id">Identificativo dell'elemento da recuperare</param>
		public ToDoItemDetails Get(string userId, int id)
		{
			using (var connection = new SqlConnection(this._connectionString))
			{
				connection.Open();
				return connection.Query<ToDoItemDetails>(@"
SELECT t.Id ,
       t.[Name] as [Title] ,
       t.Content ,
       t.CreationDate ,
       t.UserId ,
	   u1.UserName AS [User],
       t.CategoryId ,
	   c.[Name] AS [CategoryName],
	   c.[Description] AS [CategoryDescription],
       t.Completed ,
       t.CompletedDate ,
       t.CompletedUserId,
	   u2.UserName AS [CompletedUser]
	    FROM dbo.ToDoItems t
INNER JOIN dbo.AspNetUsers u1 ON u1.Id = t.UserId
LEFT JOIN dbo.AspNetUsers u2 ON u2.Id = t.CompletedUserId
LEFT JOIN dbo.Categories c ON c.Id = t.CategoryId
WHERE 
	t.UserId = @UserId AND t.Id = @Id", new { UserId = userId, Id = id }).FirstOrDefault();
			}
		}

		/// <summary>
		/// Marca un'attività come completata
		/// </summary>
		/// <param name="id">Identificativo to do item</param>
		/// <param name="userId">Identificativo utente</param>
		public void SetCompleted(int id, string userId)
		{
			using (var connection = new SqlConnection(this._connectionString))
			{
				connection.Open();

				connection.Query(@"
UPDATE dbo.ToDoItems
SET Completed = 1,
	CompletedDate = GETDATE(),
	CompletedUserId = @UserId
WHERE Id = @Id",
					new
					{
						Id = id,
						UserId = userId,
					});

			}
		}
	}
}
