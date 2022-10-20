using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Policy;
using System.Xml.Linq;

namespace DataServices
{
    public class TaskService : StateConnection, IService<ToDoTask>
    {
        public EntityResult<ToDoTask> Delete(int Id)
        {
            EntityResult<ToDoTask> result = new EntityResult<ToDoTask>()
            {
                ErrorText = "success"
            };

            try
            {
                NpgsqlCommand command = new NpgsqlCommand("UPDATE public.task SET isdeleted=TRUE WHERE id=@id;", OpenConnection());
                command.Parameters.AddWithValue("@id", Id);

                command.ExecuteNonQuery();
                CloseConnection();
            }
            catch (NpgsqlException ex)
            {
                result.ErrorText = ex.Message;
            }

            return result;

        }

        public EntityResult<ToDoTask> GetById(int id)
        {
            EntityResult<ToDoTask> result = new EntityResult<ToDoTask>()
            {
                ErrorText = "success"
            };

            try
            {
                NpgsqlCommand command = new NpgsqlCommand("select * from public.task where id=@id;", OpenConnection());
                command.Parameters.AddWithValue("@id", id);
                NpgsqlDataReader reader = command.ExecuteReader();
                ToDoTask entity = new ToDoTask();
                while (reader.Read())
                {
                    entity.Id = reader.GetInt32(0);
                    entity.StartDate = reader.GetDateTime(1);
                    entity.EndDate = reader.GetDateTime(2);
                    entity.Urgency = (EUrgency)reader.GetInt32(3);
                    entity.CategoryId = reader.GetInt32(4);
                    entity.CreatedDate = reader.GetDateTime(5);
                    entity.ModifiedDate = reader.GetDateTime(6);
                    entity.Name = reader.GetString(7);
                    entity.IsDeleted = reader.GetBoolean(8);
                    entity.Done = reader.GetBoolean(9);
                    entity.Content = reader.GetString(10).Length > 20 ? reader.GetString(10).Substring(0, 20) + "..." : reader.GetString(10);
                };
                result.Object = entity;
                CloseConnection();
            }
            catch (NpgsqlException ex)
            {
                result.ErrorText = ex.Message;
            }

            return result;
        }

        public EntityResult<ToDoTask> GetList(int? id = null)
        {
            EntityResult<ToDoTask> result = new EntityResult<ToDoTask>()
            {
                ErrorText = "success"
            };

            try
            {
                List<ToDoTask> taskList = new List<ToDoTask>();
                NpgsqlCommand command = new NpgsqlCommand();
                if (id != null)
                {
                    command = new NpgsqlCommand("SELECT t.*, c.nameof FROM public.task as t RIGHT JOIN public.category as c on t.categoryid = c.id WHERE NOT t.isdeleted AND t.categoryid=@id ORDER BY t.createddate DESC", OpenConnection());
                    command.Parameters.AddWithValue("@id", id);
                }
                else
                {
                    command = new NpgsqlCommand("SELECT t.*, c.nameof FROM public.task as t RIGHT JOIN public.category as c on t.categoryid = c.id WHERE NOT t.isdeleted ORDER BY t.createddate DESC", OpenConnection());
                }
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ToDoTask task = new ToDoTask()
                    {
                        Id = reader.GetInt32(0),
                        StartDate = reader.GetDateTime(1),
                        EndDate = reader.GetDateTime(2),
                        Urgency = (EUrgency)reader.GetInt32(3),
                        CategoryId = reader.GetInt32(4),
                        CreatedDate = reader.GetDateTime(5),
                        ModifiedDate = reader.GetDateTime(6),
                        Name = reader.GetString(7),
                        IsDeleted = reader.GetBoolean(8),
                        Done = reader.GetBoolean(9),
                        Content = reader.GetString(10).Length > 20 ? reader.GetString(10).Substring(0,20) + "..." : reader.GetString(10),
                        CategoryName = reader.GetString(11)
                    };

                    taskList.Add(task);
                }

                result.Objects = taskList;
                CloseConnection();
            }
            catch (NpgsqlException ex)
            {
                result.ErrorText = ex.Message;
            }

            return result;
        }

        public EntityResult<ToDoTask> Insert(ToDoTask entities)
        {
            EntityResult<ToDoTask> result = new EntityResult<ToDoTask>()
            {
                ErrorText = "success"
            };

            try
            {
                NpgsqlCommand command = new NpgsqlCommand("INSERT INTO public.task" + 
                    "(startdate, enddate, urgency, categoryid, createddate, modifieddate, nameof, isdeleted, done, contentof)" +
                    " VALUES(@startdate, @enddate, @urgency, @categoryid, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, @name, FALSE, @done, @content); ", OpenConnection());
                command.Parameters.AddWithValue("@startdate", entities.StartDate);
                command.Parameters.AddWithValue("@enddate", entities.EndDate);
                command.Parameters.AddWithValue("@urgency", (int)entities.Urgency);
                command.Parameters.AddWithValue("@categoryid", entities.CategoryId);
                command.Parameters.AddWithValue("@name", entities.Name);
                command.Parameters.AddWithValue("@done", entities.Done);
                command.Parameters.AddWithValue("@content", entities.Content);

                command.ExecuteNonQuery();
                CloseConnection();
            }
            catch (NpgsqlException ex)
            {
                result.ErrorText = ex.Message;
                // close connection koy
            }

            return result;
        }

        public EntityResult<ToDoTask> Update(ToDoTask toDoTask)
        {
            EntityResult<ToDoTask> result = new EntityResult<ToDoTask>()
            {
                ErrorText = "success"
            };

            try
            {
                NpgsqlCommand command = new NpgsqlCommand("UPDATE public.task SET startdate=@startdate, enddate=@enddate, urgency=@urgency, categoryid=@categoryid, modifieddate=CURRENT_TIMESTAMP, nameof=@name, isdeleted=@isdeleted, done=@done, contentof=@content WHERE id=@id;", OpenConnection());
                command.Parameters.AddWithValue("@startdate", toDoTask.StartDate);
                command.Parameters.AddWithValue("@enddate", toDoTask.EndDate);
                command.Parameters.AddWithValue("@urgency", (int)toDoTask.Urgency);
                command.Parameters.AddWithValue("@categoryid", toDoTask.CategoryId);
                command.Parameters.AddWithValue("@name", toDoTask.Name);
                command.Parameters.AddWithValue("@isdeleted", toDoTask.IsDeleted);
                command.Parameters.AddWithValue("@done", toDoTask.Done);
                command.Parameters.AddWithValue("@content", toDoTask.Content);
                command.Parameters.AddWithValue("@id", toDoTask.Id);

                command.ExecuteNonQuery();
                CloseConnection();
            }
            catch (NpgsqlException ex)
            {
                result.ErrorText = ex.Message;
            }

            return result;

        }
    }
}
