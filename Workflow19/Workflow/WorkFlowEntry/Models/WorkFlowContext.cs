using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkFlowEntry.Models
{
    public class WorkFlowContext : DbContext
    {
        public WorkFlowContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB; Initial Catalog=Workflow;;Integrated Security=true");
            }
        }

       

        public WorkFlowContext(DbContextOptions<WorkFlowContext> options)
            : base(options)
        {

        }
        public DbSet<WorkFlow_Master> WorkFlow_Master { get; set; }
        public DbSet<WorkFlow_Instance_Details> WorkFlow_Instance_Details { get; set; }
        public DbSet<Process_Request> Process_Request { get; set; }
        public DbSet<WorkFlow_Instance> WorkFlow_Instance { get; set; }
        public DbSet<WorkFlow_Process_Mapping> WorkFlow_Process_Mapping { get; set; }
        public DbSet<WorkFlow_Step_Details> WorkFlow_Step_Details { get; set; }
        public DbSet<User_Master> User_Master { get; set; }
        public DbSet<WorkPermitRequest> WorkPermitRequest { get; set; }
        public DbSet<tbl_incidentmgmt> tbl_incidentmgmt { get; set; }
        public DbSet<workpermitconfig> workpermitconfig { get; set; }
        public DbSet<tbl_image> tbl_image { get; set; }

        public DbSet<areainfo> areainfo { get; set; }
        public DbSet<assetinfo> assetinfo { get; set; }
        public DbSet<typeinfo> typeinfo { get; set; }
        public DbSet<subtypeinfo> subtypeinfo { get; set; }
        public DbSet<applicantinfo> applicantinfo { get; set; }
        public DbSet<workorderinfo> workorderinfo { get; set; }
    }
}
