using System;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models
{
    public partial class zedContext : DbContext
    {
        private static string _connectionString;
        private static readonly string LATEST_MYSQL_VERSION = "8.0.23-mysql";
        public zedContext(string connectionString)
        {
            zedContext._connectionString = connectionString;
        }

        public zedContext(DbContextOptions<zedContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Reminder> Reminders { get; set; }
        public virtual DbSet<ReminderType> ReminderTypes { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<TaskDependency> TaskDependencies { get; set; }
        public virtual DbSet<TaskPriority> TaskPriorities { get; set; }
        public virtual DbSet<TaskStatus> TaskStatuses { get; set; }
        public virtual DbSet<TaskType> TaskTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, 
                //you should move it out of source code. You can avoid scaffolding the connection 
                //string by using the Name= syntax to read it from configuration - 
                //see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on 
                //storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql(zedContext._connectionString, ServerVersion.FromString(LATEST_MYSQL_VERSION),
                    builder =>
                    {
                        builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                    });
            }
            else
            {
                Debug.WriteLine("the else");
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reminder>(entity =>
            {
                entity.ToTable("reminders");

                entity.HasIndex(e => e.ReminderTypeId, "reminders_type_id");

                entity.HasIndex(e => new { e.TaskId, e.ReminderTypeId }, "uk_reminders")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnType("varchar(150)")
                    .HasColumnName("description")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.FirstReminderLeadTimeInMinute)
                    .HasColumnName("first_reminder_lead_time_in_minute")
                    .HasDefaultValueSql("'15'");

                entity.Property(e => e.FrequencyInMinute)
                    .HasColumnName("frequency_in_minute")
                    .HasDefaultValueSql("'5'");

                entity.Property(e => e.Name)
                    .HasColumnType("varchar(50)")
                    .HasColumnName("name")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.NumberOfRetries)
                    .HasColumnName("number_of_retries")
                    .HasDefaultValueSql("'3'");

                entity.Property(e => e.ReminderTypeId)
                    .HasColumnName("reminder_type_id")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.TaskId).HasColumnName("task_id");

                entity.HasOne(d => d.ReminderType)
                    .WithMany(p => p.Reminders)
                    .HasForeignKey(d => d.ReminderTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("reminders_type_id");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.Reminders)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("reminders_tasks_task_id");
            });

            modelBuilder.Entity<ReminderType>(entity =>
            {
                entity.ToTable("reminder_types");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("varchar(200)")
                    .HasColumnName("description")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("name")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.ToTable("tasks");

                entity.HasIndex(e => e.TaskPriorityId, "tasks_task_priority_types");

                entity.HasIndex(e => e.TaskTypeId, "tasks_task_types");

                entity.HasIndex(e => new { e.Name, e.StartTime, e.DurationInMinutes }, "uk_tasks")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("varchar(200)")
                    .HasColumnName("description")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.DurationInMinutes)
                    .HasColumnName("duration_in_minutes")
                    .HasDefaultValueSql("'30'");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("name")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.NeedReminder).HasColumnName("need_reminder");

                entity.Property(e => e.StartTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("start_time");

                entity.Property(e => e.TaskPriorityId)
                    .HasColumnName("task_priority_id")
                    .HasDefaultValueSql("'2'");

                entity.Property(e => e.TaskTypeId).HasColumnName("task_type_id");

                entity.HasOne(d => d.TaskPriority)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.TaskPriorityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tasks_task_priority_types");

                entity.HasOne(d => d.TaskType)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.TaskTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tasks_task_types");
            });

            modelBuilder.Entity<TaskDependency>(entity =>
            {
                entity.ToTable("task_dependency");

                entity.HasIndex(e => e.TaskId, "task_dependency_tasks_task_id");

                entity.HasIndex(e => new { e.DependingTaskId, e.TaskId }, "uk_task_dependency")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DependingTaskId).HasColumnName("depending_task_id");

                entity.Property(e => e.TaskId).HasColumnName("task_id");

                entity.HasOne(d => d.DependingTask)
                    .WithMany(p => p.TaskDependencyDependingTasks)
                    .HasForeignKey(d => d.DependingTaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("task_dependency_tasks_depending_task_id");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.TaskDependencyTasks)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("task_dependency_tasks_task_id");
            });

            modelBuilder.Entity<TaskPriority>(entity =>
            {
                entity.ToTable("task_priority");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("varchar(200)")
                    .HasColumnName("description")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("name")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<TaskStatus>(entity =>
            {
                entity.ToTable("task_status");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("varchar(200)")
                    .HasColumnName("description")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("name")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<TaskType>(entity =>
            {
                entity.ToTable("task_types");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("varchar(200)")
                    .HasColumnName("description")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("name")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
