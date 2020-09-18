using PaintingAndSound.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintingAndSound.DAL.Services
{
    public interface ServiceBaseIDAL<T> where T : class, new()
    {
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();
        /// <summary>
        /// 根据Id获取
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<T> GetAllById(int Id);
        /// <summary>
        /// 根据id列表获取列表数据
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllsByIds(IEnumerable<int> Ids);
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        T Create(T model);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        Task<bool> RemoveAsync(T model,bool IsDelete);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<T> TExistsAsync(T model);
        /// <summary>
        /// 获取状态，保存数据库
        /// </summary>
        /// <returns></returns>
        Task<bool> SaceAsync();
    }
}
