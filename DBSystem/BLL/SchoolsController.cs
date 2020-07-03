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
    public class SchoolsController
    {
        public Schools FindByPKID(string id)
        {
            using (var context = new ContextStarTED())
            {
                return context.Schools.Find(id);
            }
        }
        public List<Schools> List()
        {
            using (var context = new ContextStarTED())
            {
                return context.Schools.ToList();
            }
        }
    }
}
