using WebEvaluation.Models;

using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Migrations;
using System.Linq;

namespace WebEvaluation.DAL
{
    public class EvaluationContext : DbContext
    {

        public DbSet<M_Division> Divisions { get; set; }
        public DbSet<M_Group> Groups { get; set; }
        public DbSet<M_Shop> Shops { get; set; }
        public DbSet<M_Staff> Staffs { get; set; }
        public DbSet<M_System> Systems { get; set; }
        public DbSet<M_User> Users { get; set; }
        public DbSet<M_Message> Messages { get; set; }
        public DbSet<T_EvaByStaff> EvaByStaffs { get; set; }
        public DbSet<T_EvaByLeader> EvaByLeaders { get; set; }
        public DbSet<T_Party> Partys { get; set; }
        public DbSet<T_Report> Reports { get; set; }
        public DbSet<S_Code> Codes { get; set; }
        public DbSet<S_Unit> Units { get; set; }
       
        //    public DbSet<Instructor> Instructors { get; set; }
        
      //  public DbSet<_Course> _Courses { get; set; }

     //   public DbSet<_Department> _Departments { get; set; }

    //    public DbSet<_Enrollment> _Enrollments { get; set; }

   //     public DbSet<_OfficeAssignment> _OfficeAssignments { get; set; }

      
 
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
     //       modelBuilder.Entity<_Department>().MapToStoredProcedures();
        }

        /// <summary>
        /// データ作成
        /// </summary>
        /// <param name="context"></param>
        public  void Seed()
        {

            M_DivisionData.Seed(this);

            M_GroupData.Seed(this);

            M_ShopData.Seed(this);

            M_StaffData.Seed(this);

            M_SystemData.Seed(this);

            M_UserData.Seed(this);

           S_CodeData.Seed(this);

           S_UnitData.Seed(this);

            
           /* 
            context.Divisions.AddOrUpdate(M_Division.GetData());
            context.SaveChanges();
            
            context.Groups.AddOrUpdate(M_Group.GetData());
            context.SaveChanges();
            
            context.Shops.AddOrUpdate(M_Shop.GetData());
            context.SaveChanges();
            
            context.Staffs.AddOrUpdate(M_Staff.GetData());
            context.SaveChanges();
            
            context.Systems.AddOrUpdate(M_System.GetData());
            context.SaveChanges();
            
            context.Users.AddOrUpdate(M_User.GetData());
            context.SaveChanges();
            */
        }
    }
}