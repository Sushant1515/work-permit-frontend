using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkFlowEntry.Models;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;

namespace WorkFlowEntry.Services
{
  
    public interface IDataApiService
    {
        Task<List<Data_API>> DataApi();
        Task<List<Data_API>> GetAsset();
        Task<List<Data_API>> Equipment();
        Task<List<Data_API>> GetLocation();
        //Task<List<CurrentPrice>> CurrentPriceApi();
    }
    
}
