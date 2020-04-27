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
    public class CategoryController
    {
        public Category FindByPKID(int id)
        {
            using (var context = new Context())
            {
                return context.Categories.Find(id);
            }
        }
        public List<Category> List()
        {
            using (var context = new Context())
            {
                return context.Categories.ToList();
            }
        }
    }
}
