using Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices
{
    public class CategoryService : StateConnection, IService<Category>
    {
        public EntityResult<Category> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public EntityResult<Category> GetById(int id)
        {
            EntityResult<Category> result = new EntityResult<Category>()
            {
                ErrorText = "success"
            };

            try
            {
                NpgsqlCommand command = new NpgsqlCommand("select * from public.category where id=@id;", OpenConnection());
                command.Parameters.AddWithValue("@id", id);
                NpgsqlDataReader reader = command.ExecuteReader();
                Category entity = new Category();
                while (reader.Read())
                {
                    entity.Id = reader.GetInt32(0);
                    entity.CreatedDate = reader.GetDateTime(1);
                    entity.ModifiedDate = reader.GetDateTime(2);
                    entity.Name = reader.GetString(3);
                    entity.IsDeleted = reader.GetBoolean(4);
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

        public EntityResult<Category> GetList(int? id =null)
        {
            EntityResult<Category> result = new EntityResult<Category>()
            {
                ErrorText = "success"
            };

            try
            {
                List<Category> categoryList = new List<Category>();
                NpgsqlCommand command = new NpgsqlCommand("select * from public.category where not isdeleted order by id asc", OpenConnection());
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Category task = new Category()
                    {
                        Id = reader.GetInt32(0),
                        CreatedDate = reader.GetDateTime(1),
                        ModifiedDate = reader.GetDateTime(2),
                        Name = reader.GetString(3),
                        IsDeleted = reader.GetBoolean(4)
                    };

                    categoryList.Add(task);
                }

                result.Objects = categoryList;
                CloseConnection();
            }
            catch (NpgsqlException ex)
            {
                result.ErrorText = ex.Message;
            }

            return result;
        }

        public EntityResult<Category> Insert(Category entities)
        {
            EntityResult<Category> result = new EntityResult<Category>()
            {
                ErrorText = "success"
            };

            try
            {
                NpgsqlCommand command = new NpgsqlCommand("INSERT INTO public.category" +
                    "(createddate, modifieddate, nameof, isdeleted)" +
                    " VALUES(CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, @nameof, FALSE); ", OpenConnection());
                command.Parameters.AddWithValue("@nameof", entities.Name);

                command.ExecuteNonQuery();
                CloseConnection();
            }
            catch (NpgsqlException ex)
            {
                result.ErrorText = ex.Message;
            }

            return result;
        }

        public EntityResult<Category> Update(Category category)
        {
            EntityResult<Category> result = new EntityResult<Category>()
            {
                ErrorText = "success"
            };

            try
            {
                NpgsqlCommand command = new NpgsqlCommand("UPDATE public.category SET modifieddate=CURRENT_TIMESTAMP, nameof=@name, isdeleted=@isdeleted WHERE id=@id;", OpenConnection());
                command.Parameters.AddWithValue("@name", category.Name);
                command.Parameters.AddWithValue("@isdeleted", category.IsDeleted);
                command.Parameters.AddWithValue("@id", category.Id);

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
