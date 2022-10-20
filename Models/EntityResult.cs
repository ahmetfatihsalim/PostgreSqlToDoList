using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class EntityResult<T> where T : class // GenericResult
    {
        public string ErrorText { get; set; }
        public T Object { get; set; }
        public IList<T> Objects { get; set; }
    }
}
