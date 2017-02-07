using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ITS.ToDoList.Web.Models;
using Microsoft.AspNetCore.Identity;
using ITS.ToDoList.Web.Models.MyListViewModels;
using ITS.ToDoList.Data;
using Microsoft.Extensions.Logging;
using ITS.ToDoList.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ITS.ToDoList.Web.Services;

namespace ITS.ToDoList.Web.Controllers
{
	[Authorize]
    public class MyListController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IToDoItemRepository _toDoItemRepository;
		private readonly ICategoryRepository _categoryRepository;
		private readonly ILogger _logger;
		private readonly IMemoryCache _cache;
		private readonly TempFavoritesService _favoritesService;

		public MyListController(
			UserManager<ApplicationUser> userManager, 
			IToDoItemRepository toDoItemRepository,
			ICategoryRepository categoryRepository,
			ILoggerFactory loggerFactory,
			IMemoryCache memoryCache,
			TempFavoritesService favoritesService)
		{
			this._userManager = userManager;
			this._toDoItemRepository = toDoItemRepository;
			this._categoryRepository = categoryRepository;
			this._logger = loggerFactory.CreateLogger<MyListController>();
			this._cache = memoryCache;
			this._favoritesService = favoritesService;
		}

		[HttpGet]
        public async Task<IActionResult> Index()
        {
			var user = await GetCurrentUserAsync();
			var items = this._toDoItemRepository.GetAll(user.Id);
			var model = new IndexViewModel();
			model.Items = items.Select(a => new ToDoItemViewModel(a));
			model.Filters = new FiltersModel();
			return View(model);
        }

		[HttpPost]
		public async Task<IActionResult> Index(FiltersModel filters)
		{
			var user = await GetCurrentUserAsync();
			var items = this._toDoItemRepository.GetAll(user.Id, filters.FilterText);
			var model = new IndexViewModel();
			model.Items = items.Select(a => new ToDoItemViewModel(a));
			model.Filters = filters;
			return View(model);
		}


		[HttpPost]
		public bool SetCompleted(int id)
		{
			try
			{
				this._toDoItemRepository.SetCompleted(id, _userManager.GetUserId(User));
				return true;
			}
			catch (Exception ex)
			{
				this._logger.LogError(ex.Message, ex);
				return false;
			}
		}


		[HttpGet]
		public IActionResult Insert()
		{
			var model = new InsertViewModel();
			model.Categories = GetCategories();
			return View(model);
		}

		[HttpPost]
		public IActionResult Insert(InsertViewModel model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					this._toDoItemRepository.Insert(new ToDoItem()
					{
						Title = model.Title,
						Content = model.Content,
						UserId = _userManager.GetUserId(User),
						CategoryId = model.CategoryId
					});
					return RedirectToAction("Index", "MyList");
				}
				catch (Exception ex)
				{
					this._logger.LogError(ex.Message, ex);
					ModelState.AddModelError("", "Errore non previsto in fase di inserimento.");
				}
			}

			model.Categories = GetCategories();

			return View(model);
		}

		[HttpGet]
		public IActionResult Update(int id)
		{
			var item = _toDoItemRepository.Get(_userManager.GetUserId(User), id);
			if (item == null)
				return NotFound();

			var model = new UpdateViewModel(item);
			model.Categories = GetCategories();

			return View(model);
		}

		/// <summary>
		/// Recupero delle categorie
		/// </summary>
		private IEnumerable<SelectListItem> GetCategories()
		{
			// recupero l'elemento dalla cache
			var result = _cache.Get<IEnumerable<SelectListItem>>("categories");
			if (result == null)
			{
				// se non lo trovo in cache vado su database
				result = this._categoryRepository
							.GetList()
							.Select(c => new SelectListItem()
							{
								Value = c.Id.ToString(),
								Text = c.Name
							});
				// metto l'oggetto in cache
				_cache.Set("categories", result);
			}
			// ritorno l'oggetto
			return result;
		}

		

		private Task<ApplicationUser> GetCurrentUserAsync()
		{
			return _userManager.GetUserAsync(HttpContext.User);
		}
	}
}