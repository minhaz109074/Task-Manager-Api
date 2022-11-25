using Microsoft.EntityFrameworkCore;
using Task_Manager_Api.Models;

namespace Task_Manager_Api.Repositories
{
    public class MainDbContext: DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options): base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }

        public DbSet<TaskItem> TaskList { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Log>   Logs { get; set; }
        public DbSet<ExceptionLog> ExceptionLogs { get; set; }
        protected  override void OnModelCreating(ModelBuilder modelBuilder)
        {
           base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TaskItem>().HasOne<Person>().WithMany(p => p.TaskAssign)
                .HasForeignKey(t => t.TaskAssignedTo).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<TaskItem>().HasOne<Person>()
                .WithMany(p => p.TaskRuqest).HasForeignKey(t => t.TaskRequestedBy).OnDelete(DeleteBehavior.Cascade);

           modelBuilder.Entity<TaskItem>().Property(t => t.Id).UseIdentityByDefaultColumn();
           modelBuilder.Entity<Log>().Property(l => l.Id).UseIdentityByDefaultColumn();
           modelBuilder.Entity<ExceptionLog>().Property(e => e.Id).UseIdentityByDefaultColumn();
        }
        public override  Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).UpdatedAt = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedAt = DateTime.Now;
                }
            }
            
            return base.SaveChangesAsync();
        }
    }
}
