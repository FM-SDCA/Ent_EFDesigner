using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ent_EFDesigner
{
    public class EF6_func
    {
        public void Add(extable etb)
        {
            using(var ent = new ex1Entities1())
            {
                ent.extable.Add(etb);
                ent.SaveChanges();
            }
        }

        public Database GetDatabase()
        {
            using (var ent = new ex1Entities1())
            {
                return ent.Database;
            }
        }

        
    }

   
}
