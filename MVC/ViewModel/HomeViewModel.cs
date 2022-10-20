using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.ViewModel
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            TaskList = new List<ToDoTask>() { };
            CategoryList = new List<Category>() { };
            Error = "";
        }
        public List<ToDoTask> TaskList{ get; set; }
        public List<Category> CategoryList { get; set; }
        public ToDoTask Task { get; set; }
        public Category Category { get; set; }
        public string Error { get; set; }
    }
}