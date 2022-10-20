using DataServices;
using Models;
using System;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public class TaskManager
    { // singleton
        private static TaskManager _taskManager;
        private static object lockObject = new object();
        TaskService taskService = new TaskService();

        public TaskManager()
        {

        }

        public static TaskManager CreateAsTaskManager()
        {
            lock (lockObject)
            {
                return _taskManager ?? (_taskManager = new TaskManager());
            }
        }

        public List<ToDoTask> GetTaskList(int? id = null)
        {
            return taskService.GetList(id).Objects.ToList();
        }

        public string TaskInsert(ToDoTask task)
        {
            return taskService.Insert(task).ErrorText;
        }

        public ToDoTask GetTaskById(int id)
        {
            return taskService.GetById(id).Object;
        }

        public string UpdateTask(ToDoTask toDoTask)
        {
            return taskService.Update(toDoTask).ErrorText;
        }
        public string DeleteTask(int id)
        {
            return taskService.Delete(id).ErrorText;
        }
    }
}
