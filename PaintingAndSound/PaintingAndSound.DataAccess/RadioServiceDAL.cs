using PaintingAndSound.DAL.Services;
using PaintingAndSound.Entities;
using PaintingAndSound.ORM;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaintingAndSound.DataAccess
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
