using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseDVLD.Repositories
{
    public interface IGeneralRepository<Entity>
    {
        int Add(Entity entity);
        bool Update(Entity entity);
        bool Delete(int entityID);
        DataTable GetAll();
 

    }
}
