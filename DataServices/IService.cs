using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices
{
    public interface IService<T> where T : class
    {
        EntityResult<T> Insert(T entities);
        EntityResult<T> Update(T entities);
        EntityResult<T> Delete(int id);
        EntityResult<T> GetById(int id);
        EntityResult<T> GetList(int? id = null);
    }
}
