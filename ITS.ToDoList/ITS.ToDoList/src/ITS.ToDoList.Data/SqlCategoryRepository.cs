using ITS.ToDoList.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Memory;

namespace ITS.ToDoList.Data
{
	public class SqlCategoryRepository : ICategoryRepository
	{
		string _connectionString;

		//public SqlCategoryRepository(string connectionString)
		//{
		//	this._connectionString = connectionString;
		//}

		public SqlCategoryRepository(IConfiguration configuration, IMemoryCache cache)
		{
			//var connectionStringName = configuration.GetSection("ToDoList")["ConnectionStringName"];

			this._connectionString = configuration.GetConnectionString("DefaultConnection");
		}

		/// <summary>
		/// Inserimento di una nuova categoria
		/// </summary>
		/// <param name="category">Categoria da inserire</param>
		public void Insert(Category category)
		{
			using (var connection = new SqlConnection(this._connectionString))
			{
				connection.Open();

				connection.Query(@"
INSERT INTO Categories (Name, Description)
VALUES (@Name, @Description)", category);
			}
		}

		/// <summary>
		/// Elenco di tutte le categorie
		/// </summary>
		public IEnumerable<Category> GetList()
		{
			using (var connection = new SqlConnection(this._connectionString))
			{
				connection.Open();

				return connection.Query<Category>(@"
SELECT Id, Name, Description 
FROM Categories");
			}
		}
	}
}
