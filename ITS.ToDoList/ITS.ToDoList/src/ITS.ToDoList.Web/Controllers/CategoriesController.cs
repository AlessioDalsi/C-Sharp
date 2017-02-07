using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ITS.ToDoList.Web.Models.CategoriesViewModels;
using ITS.ToDoList.Data;
using ITS.ToDoList.Data.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;

namespace ITS.ToDoList.Web.Controllers
{
	[Authorize]
    public class CategoriesController : Controller
    {
		private readonly ICategoryRepository _categoryRepository;
		private readonly ILogger _logger;
		private readonly IMemoryCache _cache;

		public CategoriesController(
					ICategoryRepository categoryRepository, 
					ILoggerFactory loggerFactory,
					IMemoryCache memoryCache)
		{
			this._categoryRepository = categoryRepository;
			this._logger = loggerFactory.CreateLogger<CategoriesController>();
			this._cache = memoryCache;
		}

		[HttpGet]
        public IActionResult Insert()
        {
			var model = new InsertViewModel();
			return View(model);
        }

		[HttpPost]
		public IActionResult Insert(InsertViewModel model)
		{
			if(ModelState.IsValid)
			{
				try
				{
					this._categoryRepository.Insert(new Category()
					{
						Name = model.Name,
						Description = model.Description
					});

					// rimuovo i valori non aggiornati dalla cache,
					// così al prossimo utilizzo ritrovo la categoria appena inserita
					this._cache.Remove("categories");

					return RedirectToAction("Index", "MyList");
				}
				catch (Exception ex)
				{
					this._logger.LogError(ex.Message, ex);
					ModelState.AddModelError("", "Errore non previsto in fase di inserimento.");
				}
			}

			return View(model);
		}
	}
}