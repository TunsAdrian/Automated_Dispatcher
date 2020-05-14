using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AutomatedDispatcher.Data
{
    public partial class webappContext : DbContext
    {
        public webappContext()
        {
        }

        public webappContext(DbContextOptions<webappContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<EmployeeSkill> EmployeeSkill { get; set; }
        public virtual DbSet<Skill> Skill { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Task> Task { get; set; }
        public virtual DbSet<TaskSkill> TaskSkill { get; set; }

        public DbSet<Skill> Skills { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // TODO: keep in mind that maybe this will be needed in the future
                //optionsBuilder.UseSqlServer("Server=idostuff.xyz,1433;Database=webapp;user id=webappAdmin;password=ChangeM3Now!234;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employee", "task_assignment");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CurrentWorkload).HasColumnName("Current_Workload");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("First_Name")
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("Last_Name")
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.WorkingHours).HasColumnName("Working_Hours");
            });

            modelBuilder.Entity<EmployeeSkill>(entity =>
            {
                entity.HasKey(e => new { e.EmployeeId, e.SkillId })
                    .HasName("PK_employee_skill_EmployeeID");

                entity.ToTable("employee_skill", "task_assignment");

                entity.HasIndex(e => e.SkillId)
                    .HasName("SkillID_idx");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.SkillId).HasColumnName("SkillID");

                entity.Property(e => e.SkillLevel).HasColumnName("Skill_Level");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeSkill)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("employee_skill$EmployeeID");

                entity.HasOne(d => d.Skill)
                    .WithMany(p => p.EmployeeSkill)
                    .HasForeignKey(d => d.SkillId)
                    .HasConstraintName("employee_skill$SkillID");
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.ToTable("skill", "task_assignment");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.SkillName)
                    .IsRequired()
                    .HasColumnName("Skill_Name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("status", "task_assignment");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Status1)
                    .HasColumnName("Status")
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.ToTable("task", "task_assignment");

                entity.HasIndex(e => e.EmployeeId)
                    .HasName("EmployeeID_idx");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.EndDate)
                    .HasColumnName("End_Date")
                    .HasColumnType("date");

                entity.Property(e => e.ExpectedTime).HasColumnName("Expected_Time");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StartDate)
                    .HasColumnName("Start_Date")
                    .HasColumnType("date");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Task)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("tasks$EmployeeID_Task");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Task)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK__tasks__StatusID__72C60C4A");
            });

            modelBuilder.Entity<TaskSkill>(entity =>
            {
                entity.HasKey(e => new { e.TaskId, e.SkillId })
                    .HasName("PK_task_skill_TaskID");

                entity.ToTable("task_skill", "task_assignment");

                entity.HasIndex(e => e.SkillId)
                    .HasName("SkillID_Task_idx");

                entity.Property(e => e.TaskId).HasColumnName("TaskID");

                entity.Property(e => e.SkillId).HasColumnName("SkillID");

                entity.Property(e => e.MinRequiredLevel).HasColumnName("Min_Required_Level");

                entity.HasOne(d => d.Skill)
                    .WithMany(p => p.TaskSkill)
                    .HasForeignKey(d => d.SkillId)
                    .HasConstraintName("task_skill$SkillID_Task");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.TaskSkill)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("task_skill$TaskID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
