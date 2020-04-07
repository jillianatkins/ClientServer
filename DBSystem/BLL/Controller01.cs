using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using DBSystem.DAL;
using DBSystem.ENTITIES;

namespace DBSystem.BLL
{
    public class Controller01 //Category
    {
        public Entity01 FindByPKID(int id)
        {
            using (var context = new Context())
            {
                return context.Entity01s.Find(id);
            }
        }
        public List<Entity01> List()
        {
            using (var context = new Context())
            {
                return context.Entity01s.ToList();
            }
        }
    }
}
