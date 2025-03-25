using System;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Entities
{
    public partial class GravitySurveyOnDeleteNoAction : DbContext
    {
        public GravitySurveyOnDeleteNoAction()
        {
        }

        public GravitySurveyOnDeleteNoAction(DbContextOptions<GravitySurveyOnDeleteNoAction> options)
            : base(options)
        {
        }
        public virtual DbSet<AuditLog> AuditLogs { get; set; }
        public virtual DbSet<AccessLevelEntity> AccessLevels { get; set; }
        public virtual DbSet<AreaCoordinateEntity> AreaCoordinates { get; set; }
        public virtual DbSet<CustomerEntity> Customers { get; set; }
        public virtual DbSet<CustomerTypeEntity> CustomerTypes { get; set; }
        public virtual DbSet<EmployeeEntity> Employees { get; set; }
        public virtual DbSet<EquipmentEntity> Equipment { get; set; }
        public virtual DbSet<PicketEntity> Pickets { get; set; }
        public virtual DbSet<PicketCoordinateEntity> PicketCoordinates { get; set; }
        public virtual DbSet<PointEntity> Points { get; set; }
        public virtual DbSet<PositionEntity> Positions { get; set; }
        public virtual DbSet<ProfileEntity> Profiles { get; set; }
        public virtual DbSet<ProfileCoordinateEntity> ProfileCoordinates { get; set; }
        public virtual DbSet<ProjectEntity> Projects { get; set; }
        public virtual DbSet<ReportEntity> Reports { get; set; }
        public virtual DbSet<SquareEntity> Squares { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=GravitySurveyOnDeleteNoAction;Trusted_Connection=True;TrustServerCertificate=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // AccessLevel
            modelBuilder.Entity<AccessLevelEntity>(entity =>
            {
                entity.HasKey(e => e.IdAccessLevel)
                      .HasName("PK_AccessLevel");

                entity.ToTable("AccessLevel");

                entity.Property(e => e.IdAccessLevel).HasColumnName("ID_AccessLevel");

                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(e => e.Description)
                      .IsRequired()
                      .HasMaxLength(255);
            });

            // Position
            modelBuilder.Entity<PositionEntity>(entity =>
            {
                entity.HasKey(e => e.IdPosition)
                      .HasName("PK_Position");

                entity.ToTable("Position");

                entity.Property(e => e.IdPosition).HasColumnName("ID_Position");

                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(e => e.Salary)
                      .IsRequired();

                // Внешний ключ ID_AccessLevel, ON DELETE SET NULL (свойство должно быть nullable)
                entity.Property(e => e.IdAccessLevel).HasColumnName("ID_AccessLevel");
                entity.HasOne(d => d.IdAccessLevelNavigation)
                      .WithMany(p => p.Positions)
                      .HasForeignKey(d => d.IdAccessLevel)
                      .OnDelete(DeleteBehavior.SetNull)
                      .HasConstraintName("FK_Position_AccessLevel");
            });

            // Employee
            modelBuilder.Entity<EmployeeEntity>(entity =>
            {
                entity.HasKey(e => e.IdEmployee)
                      .HasName("PK_Employee");

                entity.ToTable("Employee");

                entity.HasIndex(e => e.Passport)
                      .IsUnique()
                      .HasDatabaseName("UQ_Employee_Passport");

                entity.Property(e => e.IdEmployee).HasColumnName("ID_Employee");

                entity.Property(e => e.Passport)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.Login)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(e => e.Password)
                      .IsRequired()
                      .HasMaxLength(255);

                // Внешний ключ ID_Position (nullable) с ON DELETE SET NULL
                entity.Property(e => e.IdPosition).HasColumnName("ID_Position");
                entity.HasOne(d => d.IdPositionNavigation)
                      .WithMany(p => p.Employees)
                      .HasForeignKey(d => d.IdPosition)
                      .OnDelete(DeleteBehavior.SetNull)
                      .HasConstraintName("FK_Employee_Position");
            });

            // CustomerType
            modelBuilder.Entity<CustomerTypeEntity>(entity =>
            {
                entity.HasKey(e => e.IdCustomerType)
                      .HasName("PK_CustomerType");

                entity.ToTable("CustomerType");

                entity.Property(e => e.IdCustomerType).HasColumnName("ID_CustomerType");

                entity.Property(e => e.Description)
                      .IsRequired()
                      .HasMaxLength(255);
            });

            // Customer
            modelBuilder.Entity<CustomerEntity>(entity =>
            {
                entity.HasKey(e => e.IdCustomer)
                      .HasName("PK_Customer");

                entity.ToTable("Customer");

                entity.Property(e => e.IdCustomer).HasColumnName("ID_Customer");

                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.Login)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(e => e.Password)
                      .IsRequired()
                      .HasMaxLength(255);

                // Внешний ключ ID_Type (nullable) с ON DELETE SET NULL
                entity.Property(e => e.IdType).HasColumnName("ID_Type");
                entity.HasOne(d => d.IdTypeNavigation)
                      .WithMany(p => p.Customers)
                      .HasForeignKey(d => d.IdType)
                      .OnDelete(DeleteBehavior.SetNull)
                      .HasConstraintName("FK_Customer_CustomerType");
            });

            // Equipment
            modelBuilder.Entity<EquipmentEntity>(entity =>
            {
                entity.HasKey(e => e.IdEquipment)
                      .HasName("PK_Equipment");

                entity.HasIndex(e => e.SerialNumber)
                      .IsUnique()
                      .HasDatabaseName("UQ_Equipment_SerialNumber");

                entity.Property(e => e.IdEquipment).HasColumnName("ID_Equipment");

                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(e => e.SerialNumber)
                      .IsRequired()
                      .HasMaxLength(50);
            });

            // Project
            modelBuilder.Entity<ProjectEntity>(entity =>
            {
                entity.HasKey(e => e.IdProject)
                      .HasName("PK_Project");

                entity.ToTable("Project");

                entity.Property(e => e.IdProject).HasColumnName("ID_Project");

                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(50);

                // Внешний ключ ID_Customer (nullable) с ON DELETE SET NULL
                entity.Property(e => e.IdCustomer).HasColumnName("ID_Customer");
                entity.HasOne(d => d.IdCustomerNavigation)
                      .WithMany(p => p.Projects)
                      .HasForeignKey(d => d.IdCustomer)
                      .OnDelete(DeleteBehavior.SetNull)
                      .HasConstraintName("FK_Project_Customer");

                // Внешний ключ ID_Employee (nullable) с ON DELETE SET NULL
                entity.Property(e => e.IdEmployee).HasColumnName("ID_Employee");
                entity.HasOne(d => d.IdEmployeeNavigation)
                      .WithMany(p => p.Projects)
                      .HasForeignKey(d => d.IdEmployee)
                      .OnDelete(DeleteBehavior.SetNull)
                      .HasConstraintName("FK_Project_Employee");
            });

            // Square
            modelBuilder.Entity<SquareEntity>(entity =>
            {
                entity.HasKey(e => e.IdSquare)
                      .HasName("PK_Square");

                entity.ToTable("Square");

                entity.Property(e => e.IdSquare).HasColumnName("ID_Square");

                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(e => e.Alitude)
                      .IsRequired();

                // Внешний ключ ID_Project (nullable) с ON DELETE SET NULL
                entity.Property(e => e.IdProject).HasColumnName("ID_Project");
                entity.HasOne(d => d.IdProjectNavigation)
                      .WithMany(p => p.Squares)
                      .HasForeignKey(d => d.IdProject)
                      .OnDelete(DeleteBehavior.SetNull)
                      .HasConstraintName("FK_Square_Project");
            });

            // Profile
            modelBuilder.Entity<ProfileEntity>(entity =>
            {
                entity.HasKey(e => e.IdProfile)
                      .HasName("PK_Profile");

                entity.ToTable("Profile");

                entity.Property(e => e.IdProfile).HasColumnName("ID_profile");

                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(50);

                // Внешний ключ ID_Square (nullable) с ON DELETE SET NULL
                entity.Property(e => e.IdSquare).HasColumnName("ID_Square");
                entity.HasOne(d => d.IdSquareNavigation)
                      .WithMany(p => p.Profiles)
                      .HasForeignKey(d => d.IdSquare)
                      .OnDelete(DeleteBehavior.SetNull)
                      .HasConstraintName("FK_Profile_Square");
            });

            // Picket
            modelBuilder.Entity<PicketEntity>(entity =>
            {
                entity.HasKey(e => e.IdPicket)
                      .HasName("PK_Picket");

                // Настройка триггера, если требуется (EF Core не создаёт триггеры, но можно указать имя)
                entity.ToTable("Picket", tb => tb.HasTrigger("TR_Picket_Delete"));

                entity.Property(e => e.IdPicket).HasColumnName("ID_Picket");

                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(50);

                // Внешний ключ ID_profile (NO ACTION -> Restrict)
                entity.Property(e => e.IdProfile).HasColumnName("ID_profile");
                entity.HasOne(d => d.IdProfileNavigation)
                      .WithMany(p => p.Pickets)
                      .HasForeignKey(d => d.IdProfile)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("FK_Picket_Profile");
            });

            // PicketCoordinate
            modelBuilder.Entity<PicketCoordinateEntity>(entity =>
            {
                entity.HasKey(e => e.IdRecord)
                      .HasName("PK_PicketCoordinate");

                entity.Property(e => e.IdRecord).HasColumnName("ID_Record");

                // Внешний ключ ID_Picket (nullable) с ON DELETE SET NULL
                entity.Property(e => e.IdPicket).HasColumnName("ID_Picket");
                entity.HasOne(d => d.IdPicketNavigation)
                      .WithMany(p => p.PicketCoordinates)
                      .HasForeignKey(d => d.IdPicket)
                      .OnDelete(DeleteBehavior.SetNull)
                      .HasConstraintName("FK_PicketCoordinate_Picket");

                entity.Property(e => e.X)
                      .IsRequired();

                entity.Property(e => e.Y)
                      .IsRequired();
            });

            // Point
            modelBuilder.Entity<PointEntity>(entity =>
            {
                entity.HasKey(e => e.IdPoint)
                      .HasName("PK_Point");

                entity.ToTable("Point");

                entity.Property(e => e.IdPoint).HasColumnName("ID_Point");

                entity.Property(e => e.X)
                      .IsRequired();

                entity.Property(e => e.Y)
                      .IsRequired();

                entity.Property(e => e.Gravity)
                      .IsRequired();

                entity.Property(e => e.GravityAnomaly)
                      .IsRequired();

                entity.Property(e => e.Amendments)
                      .IsRequired();

                entity.Property(e => e.Datetime)
                      .IsRequired()
                      .HasColumnType("datetime");

                // Внешний ключ ID_Operator (nullable) с ON DELETE SET NULL
                entity.Property(e => e.IdOperator).HasColumnName("ID_Operator");
                entity.HasOne(d => d.IdOperatorNavigation)
                      .WithMany(p => p.Points)
                      .HasForeignKey(d => d.IdOperator)
                      .OnDelete(DeleteBehavior.SetNull)
                      .HasConstraintName("FK_Point_Employee");

                // Внешний ключ ID_Equipment (nullable) с ON DELETE SET NULL
                entity.Property(e => e.IdEquipment).HasColumnName("ID_Equipment");
                entity.HasOne(d => d.IdEquipmentNavigation)
                      .WithMany(p => p.Points)
                      .HasForeignKey(d => d.IdEquipment)
                      .OnDelete(DeleteBehavior.SetNull)
                      .HasConstraintName("FK_Point_Equipment");

                // Внешний ключ ID_Picket (NO ACTION -> Restrict)
                entity.Property(e => e.IdPicket).HasColumnName("ID_Picket");
                entity.HasOne(d => d.IdPicketNavigation)
                      .WithMany(p => p.Points)
                      .HasForeignKey(d => d.IdPicket)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("FK_Point_Picket");
            });

            // AreaCoordinates
            modelBuilder.Entity<AreaCoordinateEntity>(entity =>
            {
                entity.HasKey(e => e.IdRecord)
                      .HasName("PK_AreaCoordinate");

                entity.Property(e => e.IdRecord).HasColumnName("ID_Record");

                // Внешний ключ ID_Square (nullable) с ON DELETE SET NULL
                entity.Property(e => e.IdSquare).HasColumnName("ID_Square");
                entity.HasOne(d => d.IdSquareNavigation)
                      .WithMany(p => p.AreaCoordinates)
                      .HasForeignKey(d => d.IdSquare)
                      .OnDelete(DeleteBehavior.SetNull)
                      .HasConstraintName("FK_AreaCoordinate_Square");

                entity.Property(e => e.X)
                      .IsRequired();

                entity.Property(e => e.Y)
                      .IsRequired();
            });

            // ProfileCoordinate
            modelBuilder.Entity<ProfileCoordinateEntity>(entity =>
            {
                entity.HasKey(e => e.IdRecord)
                      .HasName("PK_ProfileCoordinate");

                entity.Property(e => e.IdRecord).HasColumnName("ID_Record");

                // Внешний ключ ID_profile (nullable) с ON DELETE SET NULL
                entity.Property(e => e.IdProfile).HasColumnName("ID_profile");
                entity.HasOne(d => d.IdProfileNavigation)
                      .WithMany(p => p.ProfileCoordinates)
                      .HasForeignKey(d => d.IdProfile)
                      .OnDelete(DeleteBehavior.SetNull)
                      .HasConstraintName("FK_ProfileCoordinate_Profile");

                entity.Property(e => e.X)
                      .IsRequired();

                entity.Property(e => e.Y)
                      .IsRequired();
            });

            // Report
            modelBuilder.Entity<ReportEntity>(entity =>
            {
                entity.HasKey(e => e.IdReport)
                      .HasName("PK_Report");

                entity.ToTable("Report");

                entity.Property(e => e.IdReport).HasColumnName("ID_Report");

                // Внешний ключ ID_Employee (nullable) с ON DELETE SET NULL
                entity.Property(e => e.IdEmployee).HasColumnName("ID_Employee");
                entity.HasOne(d => d.IdEmployeeNavigation)
                      .WithMany(p => p.Reports)
                      .HasForeignKey(d => d.IdEmployee)
                      .OnDelete(DeleteBehavior.SetNull)
                      .HasConstraintName("FK_Report_Employee");

                // Внешний ключ ID_Project (NO ACTION -> Restrict)
                entity.Property(e => e.IdProject).HasColumnName("ID_Project");
                entity.HasOne(d => d.IdProjectNavigation)
                      .WithMany(p => p.Reports)
                      .HasForeignKey(d => d.IdProject)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("FK_Report_Project");

                entity.Property(e => e.ReportFile)
                      .IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
