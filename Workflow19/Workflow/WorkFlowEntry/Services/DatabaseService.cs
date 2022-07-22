using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WorkFlowEntry.Repositories;

namespace WorkFlowEntry.Services
{
    public interface IDatabasesetting
    {
        DataTable FillDatatable(string cmdstr, DataTable xdt);
       
    }
    public class DatabaseService : IDatabasesetting
    {
        private readonly IDatabaseRepository _dbRepository;
        public DatabaseService(IDatabaseRepository dbRepository)
        {
            _dbRepository = dbRepository;
        }
        public DataTable FillDatatable(string cmdstr, DataTable xdt)
        {
            return _dbRepository.FillDatatable(cmdstr, xdt);
        }
       
    }
}
