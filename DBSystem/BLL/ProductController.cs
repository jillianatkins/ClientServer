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
    public class ProductController
    {
        public Product FindByPKID(int id)
        {
            using (var context = new Context())
            {
                return context.Products.Find(id);
            }
        }
        public List<Product> List()
        {
            using (var context = new Context())
            {
                return context.Products.ToList();
            }
        }
        public List<Product> FindByID(int id)
        {
            using (var context = new Context())
            {
                IEnumerable<Product> results =
                    context.Database.SqlQuery<Product>("Products_GetByCategories @ID"
                        , new SqlParameter("ID", id));
                return results.ToList();
            }
        }
        public List<Product> FindByPartialName(string partialname)
        {
            using (var context = new Context())
            {
                IEnumerable<Product> results =
                    context.Database.SqlQuery<Product>("Products_GetByPartialProductName @PartialName",
                         new SqlParameter("PartialName", partialname));
                return results.ToList();
            }
        }
        public int Add(Product item)
        {
            using (var context = new Context())
            {
                context.Products.Add(item);
                context.SaveChanges();
                return item.ProductID;

            }
        }
        public int Update(Product item)
        {
            using (var context = new Context())
            {
                context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                return context.SaveChanges();
            }
        }
        public int Delete(int productid)
        {
            using (var context = new Context())
            {
                var existing = context.Products.Find(productid);
                if (existing == null)
                {
                    throw new Exception("Record has been removed from database");
                }
                context.Products.Remove(existing);
                return context.SaveChanges();
            }
        }
    }
}
