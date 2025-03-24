using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Entities;

public partial class GravitySurveyOnDeleteNoAction : DbContext
{
    public GravitySurveyOnDeleteNoAction()
    {
    }

    public GravitySurveyOnDeleteNoAction(DbContextOptions<GravitySurveyOnDeleteNoAction> options)
        : base(options)
    {
    }

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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=GravitySurveyOnDeleteNoAction;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccessLevelEntity>(entity =>
        {
            entity.HasKey(e => e.IdAccessLevel).HasName("PK__AccessLe__4415BDCE090C6130");

            entity.ToTable("AccessLevel");

            entity.Property(e => e.IdAccessLevel).HasColumnName("ID_AccessLevel");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<AreaCoordinateEntity>(entity =>
        {
            entity.HasKey(e => e.IdRecord).HasName("PK__AreaCoor__1070D2CEC8CB031A");

            entity.Property(e => e.IdRecord).HasColumnName("ID_Record");
            entity.Property(e => e.IdSquare).HasColumnName("ID_Square");

            entity.HasOne(d => d.IdSquareNavigation).WithMany(p => p.AreaCoordinates)
                .HasForeignKey(d => d.IdSquare)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__AreaCoord__ID_Sq__571DF1D5");
        });

        modelBuilder.Entity<CustomerEntity>(entity =>
        {
            entity.HasKey(e => e.IdCustomer).HasName("PK__Customer__2D8FDE5FE0AFE17C");

            entity.ToTable("Customer");

            entity.Property(e => e.IdCustomer).HasColumnName("ID_Customer");
            entity.Property(e => e.IdType).HasColumnName("ID_Type");

            entity.HasOne(d => d.IdTypeNavigation).WithMany(p => p.Customers)
                .HasForeignKey(d => d.IdType)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Customer__ID_Typ__412EB0B6");
        });

        modelBuilder.Entity<CustomerTypeEntity>(entity =>
        {
            entity.HasKey(e => e.IdCustomerType).HasName("PK__Customer__FF17D67D56C98043");

            entity.ToTable("CustomerType");

            entity.Property(e => e.IdCustomerType).HasColumnName("ID_CustomerType");
            entity.Property(e => e.Description).HasMaxLength(255);
        });

        modelBuilder.Entity<EmployeeEntity>(entity =>
        {
            entity.HasKey(e => e.IdEmployee).HasName("PK__Employee__D9EE4F36AB13845A");

            entity.ToTable("Employee");

            entity.HasIndex(e => e.Passport, "UQ_Employee_Passport").IsUnique();

            entity.Property(e => e.IdEmployee).HasColumnName("ID_Employee");
            entity.Property(e => e.IdPosition).HasColumnName("ID_Position");
            entity.Property(e => e.Passport).HasMaxLength(100);

            entity.HasOne(d => d.IdPositionNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.IdPosition)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Employee__ID_Pos__3C69FB99");
        });

        modelBuilder.Entity<EquipmentEntity>(entity =>
        {
            entity.HasKey(e => e.IdEquipment).HasName("PK__Equipmen__4DDD08B2501A5B44");

            entity.HasIndex(e => e.SerialNumber, "UQ_Equipment_SerialNumber").IsUnique();

            entity.Property(e => e.IdEquipment).HasColumnName("ID_Equipment");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.SerialNumber).HasMaxLength(50);
        });

        modelBuilder.Entity<PicketEntity>(entity =>
        {
            entity.HasKey(e => e.IdPicket).HasName("PK__Picket__4B9DFA2E9A93D7D4");

            entity.ToTable("Picket");

            entity.Property(e => e.IdPicket).HasColumnName("ID_Picket");
            entity.Property(e => e.IdProfile).HasColumnName("ID_profile");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.IdProfileNavigation).WithMany(p => p.Pickets)
                .HasForeignKey(d => d.IdProfile)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Picket__ID_profi__4F7CD00D");
        });

        modelBuilder.Entity<PicketCoordinateEntity>(entity =>
        {
            entity.HasKey(e => e.IdRecord).HasName("PK__PicketCo__1070D2CEF7D4EA98");

            entity.Property(e => e.IdRecord).HasColumnName("ID_Record");
            entity.Property(e => e.IdPicket).HasColumnName("ID_Picket");

            entity.HasOne(d => d.IdPicketNavigation).WithMany(p => p.PicketCoordinates)
                .HasForeignKey(d => d.IdPicket)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__PicketCoo__ID_Pi__5CD6CB2B");
        });

        modelBuilder.Entity<PointEntity>(entity =>
        {
            entity.HasKey(e => e.IdPoint).HasName("PK__Point__26035707E6DE79E7");

            entity.ToTable("Point");

            entity.Property(e => e.IdPoint).HasColumnName("ID_Point");
            entity.Property(e => e.Datetime).HasColumnType("datetime");
            entity.Property(e => e.IdEquipment).HasColumnName("ID_Equipment");
            entity.Property(e => e.IdOperator).HasColumnName("ID_Operator");
            entity.Property(e => e.IdPicket).HasColumnName("ID_Picket");

            entity.HasOne(d => d.IdEquipmentNavigation).WithMany(p => p.Points)
                .HasForeignKey(d => d.IdEquipment)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Point__ID_Equipm__534D60F1");

            entity.HasOne(d => d.IdOperatorNavigation).WithMany(p => p.Points)
                .HasForeignKey(d => d.IdOperator)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Point__ID_Operat__52593CB8");

            entity.HasOne(d => d.IdPicketNavigation).WithMany(p => p.Points)
                .HasForeignKey(d => d.IdPicket)
                .HasConstraintName("FK__Point__ID_Picket__5441852A");
        });

        modelBuilder.Entity<PositionEntity>(entity =>
        {
            entity.HasKey(e => e.IdPosition).HasName("PK__Position__8F963ECEB075C8DE");

            entity.ToTable("Position");

            entity.Property(e => e.IdPosition).HasColumnName("ID_Position");
            entity.Property(e => e.IdAccessLevel).HasColumnName("ID_AccessLevel");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.IdAccessLevelNavigation).WithMany(p => p.Positions)
                .HasForeignKey(d => d.IdAccessLevel)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Position__ID_Acc__398D8EEE");
        });

        modelBuilder.Entity<ProfileEntity>(entity =>
        {
            entity.HasKey(e => e.IdProfile).HasName("PK__Profile__CB159EAA0B8D8EC8");

            entity.ToTable("Profile");

            entity.Property(e => e.IdProfile).HasColumnName("ID_profile");
            entity.Property(e => e.IdSquare).HasColumnName("ID_Square");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.IdSquareNavigation).WithMany(p => p.Profiles)
                .HasForeignKey(d => d.IdSquare)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Profile__ID_Squa__4CA06362");
        });

        modelBuilder.Entity<ProfileCoordinateEntity>(entity =>
        {
            entity.HasKey(e => e.IdRecord).HasName("PK__ProfileC__1070D2CE4826E03C");

            entity.Property(e => e.IdRecord).HasColumnName("ID_Record");
            entity.Property(e => e.IdProfile).HasColumnName("ID_profile");

            entity.HasOne(d => d.IdProfileNavigation).WithMany(p => p.ProfileCoordinates)
                .HasForeignKey(d => d.IdProfile)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__ProfileCo__ID_pr__59FA5E80");
        });

        modelBuilder.Entity<ProjectEntity>(entity =>
        {
            entity.HasKey(e => e.IdProject).HasName("PK__Project__D310AEBFA7F4FA18");

            entity.ToTable("Project");

            entity.Property(e => e.IdProject).HasColumnName("ID_Project");
            entity.Property(e => e.IdCustomer).HasColumnName("ID_Customer");
            entity.Property(e => e.IdEmployee).HasColumnName("ID_Employee");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.IdCustomerNavigation).WithMany(p => p.Projects)
                .HasForeignKey(d => d.IdCustomer)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Project__ID_Cust__45F365D3");

            entity.HasOne(d => d.IdEmployeeNavigation).WithMany(p => p.Projects)
                .HasForeignKey(d => d.IdEmployee)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Project__ID_Empl__46E78A0C");
        });

        modelBuilder.Entity<ReportEntity>(entity =>
        {
            entity.HasKey(e => e.IdReport).HasName("PK__Report__C62452942BD28E26");

            entity.ToTable("Report");

            entity.Property(e => e.IdReport).HasColumnName("ID_Report");
            entity.Property(e => e.IdEmployee).HasColumnName("ID_Employee");
            entity.Property(e => e.IdProject).HasColumnName("ID_Project");

            entity.HasOne(d => d.IdEmployeeNavigation).WithMany(p => p.Reports)
                .HasForeignKey(d => d.IdEmployee)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Report__ID_Emplo__5FB337D6");

            entity.HasOne(d => d.IdProjectNavigation).WithMany(p => p.Reports)
                .HasForeignKey(d => d.IdProject)
                .HasConstraintName("FK__Report__ID_Proje__60A75C0F");
        });

        modelBuilder.Entity<SquareEntity>(entity =>
        {
            entity.HasKey(e => e.IdSquare).HasName("PK__Square__02AD7928CA6DD4F9");

            entity.ToTable("Square");

            entity.Property(e => e.IdSquare).HasColumnName("ID_Square");
            entity.Property(e => e.IdProject).HasColumnName("ID_Project");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.IdProjectNavigation).WithMany(p => p.Squares)
                .HasForeignKey(d => d.IdProject)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Square__ID_Proje__49C3F6B7");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
