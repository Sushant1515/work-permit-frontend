using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WorkFlowEntry.Repositories
{
   public interface IDatabaseRepository
    {
        DataTable FillDatatable(string cmdstr, DataTable xdt);
       

    }
}
