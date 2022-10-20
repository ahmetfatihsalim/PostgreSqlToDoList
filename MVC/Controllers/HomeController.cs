using Manager;
using MVC.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private TaskManager taskManager = TaskManager.CreateAsTaskManager();
        private CategoryManager categoryManager = CategoryManager.CreateAsCategoryManager();
        //GET
        public ActionResult Index(int? id = null)
        {
            HomeViewModel homeViewModel = new HomeViewModel();
            homeViewModel.TaskList = taskManager.GetTaskList(id);
            homeViewModel.CategoryList = categoryManager.GetCategoryList();

            return View(homeViewModel);
        }

        [HttpPost]
        public ActionResult Index(HomeViewModel model)
        {
            return RedirectToAction("Index", "Home");
        }

        //GET
        public  ActionResult GetAllTasks()
        {
            HomeViewModel homeViewModel = new HomeViewModel();
            homeViewModel.TaskList = taskManager.GetTaskList();
            homeViewModel.CategoryList = categoryManager.GetCategoryList();
            return RedirectToAction("Index");
        }

        // GET
        public ActionResult CreateNewTask()
        {
            ViewBag.CategoryList = new SelectList(categoryManager.GetCategoryList(), "id","name");
            return View();
        }

        [HttpPost]
        public ActionResult CreateNewTask(HomeViewModel model)
        {
            string result = taskManager.TaskInsert(model.Task);
            ViewBag.CategoryList = new SelectList(categoryManager.GetCategoryList(), "id", "name");
            ViewBag.Error = result;
            return View();
        }

        //GET
        public ActionResult EditTask(int id)
        {
            HomeViewModel homeViewModel = new HomeViewModel();
            ViewBag.CategoryList = new SelectList(categoryManager.GetCategoryList(), "id", "name");
            homeViewModel.Task = taskManager.GetTaskById(id);

            return View(homeViewModel);
        }

        [HttpPost]
        public ActionResult EditTask(HomeViewModel model)
        {
            string result = taskManager.UpdateTask(model.Task);
            ViewBag.CategoryList = new SelectList(categoryManager.GetCategoryList(), "id", "name");
            ViewBag.Error = result;
            return View();
        }

        //GET
        public ActionResult DeleteTask(int id)
        {
            string result = taskManager.DeleteTask(id);
            ViewBag.Error = result;
            return RedirectToAction("Index");
        }

        // GET
        public ActionResult CreateNewCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateNewCategory(HomeViewModel model)
        {
            string result = categoryManager.CategoryInsert(model.Category);
            ViewBag.Error = result;
            return View();
        }

        //GET
        public ActionResult EditCategory(int id)
        {
            HomeViewModel homeViewModel = new HomeViewModel();
            homeViewModel.Category = categoryManager.GetCategoryById(id);
            homeViewModel.TaskList = taskManager.GetTaskList(id);

            return View(homeViewModel);
        }

        [HttpPost]
        public ActionResult EditCategory(HomeViewModel model)
        {
            string result = categoryManager.UpdateCategory(model.Category);
            ViewBag.Error = result;
            return View();
        }
    }
}