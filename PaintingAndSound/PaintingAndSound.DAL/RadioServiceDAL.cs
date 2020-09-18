using PaintingAndSound.DAL.Services;
using PaintingAndSound.DB;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaintingAndSound.DAL
{
  public  class RadioServiceDAL<T>: ServiceBase<T> where T : Radio, new()
    {
        private readonly HSDbContext db;

        public RadioServiceDAL(HSDbContext _Db):base(_Db)
        {
            db = _Db ?? throw new ArgumentNullException(nameof(_Db));
        }
    }
}
