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
    public class ProgramsController
    {
        public Programs FindByPKID(int id)
        {
            using (var context = new ContextStarTED())
            {
                return context.Programs.Find(id);
            }
        }
        public List<Programs> List()
        {
            using (var context = new ContextStarTED())
            {
                return context.Programs.ToList();
            }
        }
        public List<Programs> FindByID(string code)
        {
            using (var context = new ContextStarTED())
            {
                IEnumerable<Programs> results =
                    context.Database.SqlQuery<Programs>("Programs_FindBySchool @schoolcode"
                        , new SqlParameter("schoolcode", code));
                return results.ToList();
            }
        }

        public int Update(Programs program)
        {
            using (var context = new ContextStarTED())
            {
                context.Entry(program).State = System.Data.Entity.EntityState.Modified;
                return context.SaveChanges();
            }
        }
        public int Delete(int programID)
        {
            using (var context = new ContextStarTED())
            {
                var existing = context.Programs.Find(programID);
                if (existing == null)
                {
                    throw new Exception("Record has been removed from database");
                }
                context.Programs.Remove(existing);
                return context.SaveChanges();
            }
        }


    }
}
