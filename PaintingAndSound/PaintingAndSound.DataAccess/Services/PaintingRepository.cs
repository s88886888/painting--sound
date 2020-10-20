using PaintingAndSound.Entities;
using PaintingAndSound.ORM;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaintingAndSound.DataAccess.Services
{
    public class PaintingRepository : EntityRepository<Painting>
    {
        private readonly HSDbContext _db;
        public PaintingRepository(HSDbContext db):base(db)
        {
            _db = db;
        }
    }
}
