using Microsoft.EntityFrameworkCore;
using PaintingAndSound.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintingAndSound.DAL.Services
{
    public class ServiceBase<T> : ServiceBaseIDAL<T> where T : BasicsBase, new()
    {
        private readonly HSDbContext db;

        //public ServiceBase()
        //{

        //}
        public ServiceBase(HSDbContext _Db)
        {
            db = _Db ?? throw new ArgumentNullException(nameof(_Db));
        }
        public T Create(T model)
        {
            db.Set<T>().Add(model);
            return model;
        }

        public IQueryable<T> GetAll()
        {
            return db.Set<T>().Where(m => !m.IsDelete).AsNoTracking();
        }

        public async Task<T> GetAllById(int Id)
        {
            return await GetAll().FirstOrDefaultAsync(a => a.Id == Id);
        }

        public Task<IEnumerable<T>> GetAllsByIds(IEnumerable<int> Ids)
        {
            throw new NotImplementedException();

        }

        public async Task<bool> RemoveAsync(T model, bool IsDelete = false)
        {
            if (model.IsDelete == true)
            {
                db.Entry(model).State = EntityState.Deleted;
                await SaceAsync();
                IsDelete = true;
            }
            else
            {
                return IsDelete;
            }
            return true;
        }

        public async Task<bool> SaceAsync()
        {
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<T> TExistsAsync(T model)
        {
            db.Entry(model).State = EntityState.Modified;
            await SaceAsync();
            return model;
        }
    }
}
