using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITS.ToDoList.Web.Services
{
    public class TempFavoritesService
    {
		private readonly IHttpContextAccessor _httpContextAccessor;
		public TempFavoritesService(IHttpContextAccessor httpContextAccessor)
		{
			this._httpContextAccessor = httpContextAccessor;
		}


		/// <summary>
		/// Ritorno gli elementi preferiti che ho in sessione
		/// </summary>
		public List<int> GetFavorites()
		{
			var favoritesObj = this._httpContextAccessor.HttpContext.Session.GetString("favorites");
			List<int> list;
			if (string.IsNullOrEmpty(favoritesObj))
				list = new List<int>();
			else
				list = JsonConvert.DeserializeObject<List<int>>(favoritesObj);

			return list;
		}

		/// <summary>
		/// Aggiungo un elemento tra i preferiti
		/// </summary>
		/// <param name="id">Identificavo del todo item</param>
		public void AddToFavorites(int id)
		{
			List<int> list = GetFavorites();

			if (!list.Contains(id))
				list.Add(id);

			this._httpContextAccessor.HttpContext.Session.SetString("favorites", JsonConvert.SerializeObject(list));
		}

		/// <summary>
		/// Riumuovo un elemento dai preferiti
		/// </summary>
		/// <param name="id">Identificavo del todo item</param>
		public void RemoveToFavorites(int id)
		{
			List<int> list = GetFavorites();

			if (list.Contains(id))
				list.Remove(id);

			this._httpContextAccessor.HttpContext.Session.SetString("favorites", JsonConvert.SerializeObject(list));
		}
	}
}
