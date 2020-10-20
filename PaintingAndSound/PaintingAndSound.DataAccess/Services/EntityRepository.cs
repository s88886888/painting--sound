using Microsoft.EntityFrameworkCore;
using PaintingAndSound.Entities;
using PaintingAndSound.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PaintingAndSound.DataAccess.Services
{
    public class EntityRepository<T> : IEntityRepository<T> where T : BasicsBase, new()
    {
        readonly HSDbContext _HSDbContext;

        public EntityRepository(HSDbContext context) => _HSDbContext = context;

        public virtual void Save()
        {
            try
            {
                _HSDbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                // 获取错误信息集合
                var errorMessages = ex.Message;
                var itemErrorMessage = string.Join("; ", errorMessages);
                var exceptionMessage = string.Concat(ex.Message, " Error: ", itemErrorMessage);
                throw new DbUpdateException(exceptionMessage, ex);
            }
        }

        public virtual IQueryable<T> GetAll() => _HSDbContext.Set<T>().Where(a => a.IsDelete == false);




        public virtual IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _HSDbContext.Set<T>();
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
            return query;
        }

        public virtual T GetSingle(int id) => GetAll().FirstOrDefault(x => x.Id == id);

        public virtual T GetSingle(int id, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> dbSet = _HSDbContext.Set<T>();
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    dbSet = dbSet.Include(includeProperty);
                }
            }

            var result = dbSet.FirstOrDefault(x => x.Id == id);
            return result;
        }

        public virtual T GetSingleBy(Expression<Func<T, bool>> predicate) => _HSDbContext.Set<T>().Where(predicate).FirstOrDefault(predicate);

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate) => _HSDbContext.Set<T>().Where(predicate);



        public virtual bool Add(T entity)
        {
            try
            {
                _HSDbContext.Set<T>().Add(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual bool AddAndSave(T entity)
        {
            try
            {
                Add(entity);
                Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual bool Edit(T entity)
        {
            try
            {
                _HSDbContext.Set<T>().Update(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual bool EditAndSave(T entity)
        {
            try
            {
                Edit(entity);
                Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual bool EditAndSaveBy(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> newValueExpression)
        {
            try
            {
                var dbSet = _HSDbContext.Set<T>();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual bool AddOrEdit(T entity)
        {
            try
            {
                var p = GetAll().FirstOrDefault(x => x.Id == entity.Id);
                if (p == null)
                {
                    Add(entity);
                }
                else
                {
                    Edit(entity);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual bool AddOrEditAndSave(T entity)
        {
            try
            {
                AddOrEdit(entity);
                Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual bool Delete(T entity)
        {
            try
            {
                _HSDbContext.Set<T>().Remove(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual bool DeleteAndSave(T entity)
        {
            try
            {
                Delete(entity);
                Save();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public virtual bool DeleteAndSaveBy(Expression<Func<T, bool>> predicate)
        {
            try
            {
                var dbSet = _HSDbContext.Set<T>();
                var toBeDeleteItems = dbSet.Where(predicate);//.Delete();
                foreach (var item in toBeDeleteItems)
                {
                    dbSet.Remove(item);
                }
                return true;
            }
            catch
            {
                return false;
            }

        }


        public bool HasInstance(Expression<Func<T, bool>> predicate)
        {
            var dbSet = _HSDbContext.Set<T>();
            return dbSet.Any(predicate);
        }

        public virtual T1 GetSingleOther<T1>(int id) where T1 : class, new()
        {
            var dbSet = _HSDbContext.Set<T1>();
            return dbSet.Find(id);
        }

        /// <summary>
        /// 判断是否有相应==是否存在数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool PaintingExists(int id)
        {
            return _HSDbContext.Paintings.Any(t => t.Id == id);
        }
        public bool RadiosExists(int id)
        {
            return _HSDbContext.Radios.Any(t => t.Id == id);
        }
        public bool WorksExists(int id)
        {
            return _HSDbContext.Works.Any(t => t.Id == id);
        }
        public bool WorksCommentsExists(int id)
        {
            return _HSDbContext.WorksComments.Any(t => t.Id == id);
        }

        #region 异步方法的具体实现
        public virtual async Task<bool> SaveAsyn()
        {
            await _HSDbContext.SaveChangesAsync();
            return true;
        }

        public virtual async Task<IQueryable<T>> GetAllAsyn()
        {
            var dbSet = _HSDbContext.Set<T>();
            var result = await dbSet.ToListAsync();
            return result.AsQueryable<T>();
        }

        public virtual async Task<IQueryable<T>> GetAllIncludingAsyn(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _HSDbContext.Set<T>(); //.Include(includeProperties);
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
            var result = await query.ToListAsync();
            return result.AsQueryable();
        }

        public virtual async Task<IQueryable<T>> GetAllAsyn(Expression<Func<T, bool>> predicate)
        {
            var result = await _HSDbContext.Set<T>().Where(predicate).ToListAsync();
            return result.AsQueryable();

        }

        public virtual async Task<IQueryable<T>> GetAllIncludingAsyn(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _HSDbContext.Set<T>().Where(predicate);
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
            var result = await query.ToListAsync();
            return result.AsQueryable();
        }

        public virtual async Task<T> GetSingleAsyn(int id)
        {
            var dbSet = _HSDbContext.Set<T>();
            var result = await dbSet.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public virtual async Task<T> GetSingleAsyn(int id, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> dbSet = _HSDbContext.Set<T>();
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    dbSet = dbSet.Include(includeProperty);
                }
            }

            var result = await dbSet.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public virtual async Task<T> GetSingleAsyn(Expression<Func<T, bool>> predicate)
        {
            var dbSet = _HSDbContext.Set<T>();
            var result = await dbSet.FirstOrDefaultAsync(predicate);
            return result;
        }


        public virtual async Task<IQueryable<T>> FindByAsyn(Expression<Func<T, bool>> predicate)
        {
            var result = await _HSDbContext.Set<T>().Where(predicate).ToListAsync();
            return result.AsQueryable();
        }

        public virtual async Task<bool> HasInstanceAsyn(int id) => await _HSDbContext.Set<T>().AnyAsync(x => x.Id == id);

        public virtual async Task<bool> HasInstanceAsyn(Expression<Func<T, bool>> predicate) => await _HSDbContext.Set<T>().AnyAsync(predicate);

        public virtual async Task<bool> AddOrEditAndSaveAsyn(T entity)
        {
            var dbSet = _HSDbContext.Set<T>();
            var hasInstance = await dbSet.AnyAsync(x => x.Id == entity.Id);
            if (hasInstance)
                dbSet.Update(entity);
            else
                await dbSet.AddAsync(entity);
            try
            {
                await _HSDbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public bool HasInstance(int id)
        {
            var dbSet = _HSDbContext.Set<T>();
            return dbSet.Any(x => x.Id == id);
        }
        #endregion


        public bool PaintingExistsByUserId(int id)
        {
            return _HSDbContext.Paintings.Any(t => t.UserId == id);
        }
        public bool RadiosExistsByUserId(int id)
        {
            return _HSDbContext.Radios.Any(t => t.UserId == id);
        }

        public List<Painting> GetPaintingAllByUserId(int userid)
        {
            return _HSDbContext.Paintings.Where(t => t.UserId == userid).ToList();
        }
        public List<Radio> GetRadiosAllByUserId(int userid)
        {
            return _HSDbContext.Radios.Where(t => t.UserId == userid).ToList();
        }

        public PaintionPhotos FindbyPaintingId(int id)
        {
            return _HSDbContext.PaintionPhotos.FirstOrDefault(t => t.PaintingId == id);
        }

    }
}
