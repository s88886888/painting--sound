using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintingAndSound.DAL.Services
{
  public  interface IServiceBase<T> where T : class,new()
    {
       abstract T Create();
        Task RemoveAsync(int Id);
        IQueryable<T> GetAll();
        T Update(T model);
    }
}
