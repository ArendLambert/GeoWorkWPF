﻿// <auto-generated />
using System;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccessLayer.Migrations
{
    [DbContext(typeof(GravitySurveyOnDeleteNoAction))]
    partial class GravitySurveyOnDeleteNoActionModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DataAccessLayer.Entities.AccessLevelEntity", b =>
                {
                    b.Property<int>("IdAccessLevel")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_AccessLevel");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdAccessLevel"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("IdAccessLevel")
                        .HasName("PK_AccessLevel");

                    b.ToTable("AccessLevel", (string)null);
                });

            modelBuilder.Entity("DataAccessLayer.Entities.AreaCoordinateEntity", b =>
                {
                    b.Property<int>("IdRecord")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_Record");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdRecord"));

                    b.Property<int?>("IdSquare")
                        .HasColumnType("int")
                        .HasColumnName("ID_Square");

                    b.Property<double>("X")
                        .HasColumnType("float");

                    b.Property<double>("Y")
                        .HasColumnType("float");

                    b.HasKey("IdRecord")
                        .HasName("PK_AreaCoordinate");

                    b.HasIndex("IdSquare");

                    b.ToTable("AreaCoordinates");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.AuditLogsEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ChangeDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ChangeLocation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NewValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OldValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AuditLogs");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.CustomerEntity", b =>
                {
                    b.Property<int>("IdCustomer")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_Customer");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCustomer"));

                    b.Property<int?>("IdType")
                        .HasColumnType("int")
                        .HasColumnName("ID_Type");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("IdCustomer")
                        .HasName("PK_Customer");

                    b.HasIndex("IdType");

                    b.ToTable("Customer", (string)null);
                });

            modelBuilder.Entity("DataAccessLayer.Entities.CustomerTypeEntity", b =>
                {
                    b.Property<int>("IdCustomerType")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_CustomerType");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCustomerType"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("IdCustomerType")
                        .HasName("PK_CustomerType");

                    b.ToTable("CustomerType", (string)null);
                });

            modelBuilder.Entity("DataAccessLayer.Entities.EmployeeEntity", b =>
                {
                    b.Property<int>("IdEmployee")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_Employee");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdEmployee"));

                    b.Property<int?>("IdPosition")
                        .HasColumnType("int")
                        .HasColumnName("ID_Position");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Passport")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("IdEmployee")
                        .HasName("PK_Employee");

                    b.HasIndex("IdPosition");

                    b.HasIndex("Passport")
                        .IsUnique()
                        .HasDatabaseName("UQ_Employee_Passport");

                    b.ToTable("Employee", (string)null);
                });

            modelBuilder.Entity("DataAccessLayer.Entities.EquipmentEntity", b =>
                {
                    b.Property<int>("IdEquipment")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_Equipment");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdEquipment"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("SerialNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("IdEquipment")
                        .HasName("PK_Equipment");

                    b.HasIndex("SerialNumber")
                        .IsUnique()
                        .HasDatabaseName("UQ_Equipment_SerialNumber");

                    b.ToTable("Equipment");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.PicketCoordinateEntity", b =>
                {
                    b.Property<int>("IdRecord")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_Record");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdRecord"));

                    b.Property<int?>("IdPicket")
                        .HasColumnType("int")
                        .HasColumnName("ID_Picket");

                    b.Property<double>("X")
                        .HasColumnType("float");

                    b.Property<double>("Y")
                        .HasColumnType("float");

                    b.HasKey("IdRecord")
                        .HasName("PK_PicketCoordinate");

                    b.HasIndex("IdPicket");

                    b.ToTable("PicketCoordinates");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.PicketEntity", b =>
                {
                    b.Property<int>("IdPicket")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_Picket");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPicket"));

                    b.Property<int?>("IdProfile")
                        .HasColumnType("int")
                        .HasColumnName("ID_profile");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("IdPicket")
                        .HasName("PK_Picket");

                    b.HasIndex("IdProfile");

                    b.ToTable("Picket", null, t =>
                        {
                            t.HasTrigger("TR_Picket_Delete");
                        });

                    b.HasAnnotation("SqlServer:UseSqlOutputClause", false);
                });

            modelBuilder.Entity("DataAccessLayer.Entities.PointEntity", b =>
                {
                    b.Property<int>("IdPoint")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_Point");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPoint"));

                    b.Property<double>("Amendments")
                        .HasColumnType("float");

                    b.Property<DateTime>("Datetime")
                        .HasColumnType("datetime");

                    b.Property<double>("Gravity")
                        .HasColumnType("float");

                    b.Property<double>("GravityAnomaly")
                        .HasColumnType("float");

                    b.Property<int?>("IdEquipment")
                        .HasColumnType("int")
                        .HasColumnName("ID_Equipment");

                    b.Property<int?>("IdOperator")
                        .HasColumnType("int")
                        .HasColumnName("ID_Operator");

                    b.Property<int?>("IdPicket")
                        .HasColumnType("int")
                        .HasColumnName("ID_Picket");

                    b.Property<double>("X")
                        .HasColumnType("float");

                    b.Property<double>("Y")
                        .HasColumnType("float");

                    b.HasKey("IdPoint")
                        .HasName("PK_Point");

                    b.HasIndex("IdEquipment");

                    b.HasIndex("IdOperator");

                    b.HasIndex("IdPicket");

                    b.ToTable("Point", (string)null);
                });

            modelBuilder.Entity("DataAccessLayer.Entities.PositionEntity", b =>
                {
                    b.Property<int>("IdPosition")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_Position");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPosition"));

                    b.Property<int?>("IdAccessLevel")
                        .HasColumnType("int")
                        .HasColumnName("ID_AccessLevel");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Salary")
                        .HasColumnType("int");

                    b.HasKey("IdPosition")
                        .HasName("PK_Position");

                    b.HasIndex("IdAccessLevel");

                    b.ToTable("Position", (string)null);
                });

            modelBuilder.Entity("DataAccessLayer.Entities.ProfileCoordinateEntity", b =>
                {
                    b.Property<int>("IdRecord")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_Record");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdRecord"));

                    b.Property<int?>("IdProfile")
                        .HasColumnType("int")
                        .HasColumnName("ID_profile");

                    b.Property<double>("X")
                        .HasColumnType("float");

                    b.Property<double>("Y")
                        .HasColumnType("float");

                    b.HasKey("IdRecord")
                        .HasName("PK_ProfileCoordinate");

                    b.HasIndex("IdProfile");

                    b.ToTable("ProfileCoordinates");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.ProfileEntity", b =>
                {
                    b.Property<int>("IdProfile")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_profile");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdProfile"));

                    b.Property<int?>("IdSquare")
                        .HasColumnType("int")
                        .HasColumnName("ID_Square");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("IdProfile")
                        .HasName("PK_Profile");

                    b.HasIndex("IdSquare");

                    b.ToTable("Profile", (string)null);
                });

            modelBuilder.Entity("DataAccessLayer.Entities.ProjectEntity", b =>
                {
                    b.Property<int>("IdProject")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_Project");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdProject"));

                    b.Property<int?>("IdCustomer")
                        .HasColumnType("int")
                        .HasColumnName("ID_Customer");

                    b.Property<int?>("IdEmployee")
                        .HasColumnType("int")
                        .HasColumnName("ID_Employee");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("IdProject")
                        .HasName("PK_Project");

                    b.HasIndex("IdCustomer");

                    b.HasIndex("IdEmployee");

                    b.ToTable("Project", (string)null);
                });

            modelBuilder.Entity("DataAccessLayer.Entities.ReportEntity", b =>
                {
                    b.Property<int>("IdReport")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_Report");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdReport"));

                    b.Property<int?>("IdEmployee")
                        .HasColumnType("int")
                        .HasColumnName("ID_Employee");

                    b.Property<int?>("IdProject")
                        .HasColumnType("int")
                        .HasColumnName("ID_Project");

                    b.Property<string>("ReportFile")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdReport")
                        .HasName("PK_Report");

                    b.HasIndex("IdEmployee");

                    b.HasIndex("IdProject");

                    b.ToTable("Report", (string)null);
                });

            modelBuilder.Entity("DataAccessLayer.Entities.SquareEntity", b =>
                {
                    b.Property<int>("IdSquare")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_Square");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdSquare"));

                    b.Property<int>("Alitude")
                        .HasColumnType("int");

                    b.Property<int?>("IdProject")
                        .HasColumnType("int")
                        .HasColumnName("ID_Project");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("IdSquare")
                        .HasName("PK_Square");

                    b.HasIndex("IdProject");

                    b.ToTable("Square", (string)null);
                });

            modelBuilder.Entity("DataAccessLayer.Entities.AreaCoordinateEntity", b =>
                {
                    b.HasOne("DataAccessLayer.Entities.SquareEntity", "IdSquareNavigation")
                        .WithMany("AreaCoordinates")
                        .HasForeignKey("IdSquare")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK_AreaCoordinate_Square");

                    b.Navigation("IdSquareNavigation");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.CustomerEntity", b =>
                {
                    b.HasOne("DataAccessLayer.Entities.CustomerTypeEntity", "IdTypeNavigation")
                        .WithMany("Customers")
                        .HasForeignKey("IdType")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK_Customer_CustomerType");

                    b.Navigation("IdTypeNavigation");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.EmployeeEntity", b =>
                {
                    b.HasOne("DataAccessLayer.Entities.PositionEntity", "IdPositionNavigation")
                        .WithMany("Employees")
                        .HasForeignKey("IdPosition")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK_Employee_Position");

                    b.Navigation("IdPositionNavigation");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.PicketCoordinateEntity", b =>
                {
                    b.HasOne("DataAccessLayer.Entities.PicketEntity", "IdPicketNavigation")
                        .WithMany("PicketCoordinates")
                        .HasForeignKey("IdPicket")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK_PicketCoordinate_Picket");

                    b.Navigation("IdPicketNavigation");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.PicketEntity", b =>
                {
                    b.HasOne("DataAccessLayer.Entities.ProfileEntity", "IdProfileNavigation")
                        .WithMany("Pickets")
                        .HasForeignKey("IdProfile")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("FK_Picket_Profile");

                    b.Navigation("IdProfileNavigation");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.PointEntity", b =>
                {
                    b.HasOne("DataAccessLayer.Entities.EquipmentEntity", "IdEquipmentNavigation")
                        .WithMany("Points")
                        .HasForeignKey("IdEquipment")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK_Point_Equipment");

                    b.HasOne("DataAccessLayer.Entities.EmployeeEntity", "IdOperatorNavigation")
                        .WithMany("Points")
                        .HasForeignKey("IdOperator")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK_Point_Employee");

                    b.HasOne("DataAccessLayer.Entities.PicketEntity", "IdPicketNavigation")
                        .WithMany("Points")
                        .HasForeignKey("IdPicket")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("FK_Point_Picket");

                    b.Navigation("IdEquipmentNavigation");

                    b.Navigation("IdOperatorNavigation");

                    b.Navigation("IdPicketNavigation");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.PositionEntity", b =>
                {
                    b.HasOne("DataAccessLayer.Entities.AccessLevelEntity", "IdAccessLevelNavigation")
                        .WithMany("Positions")
                        .HasForeignKey("IdAccessLevel")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK_Position_AccessLevel");

                    b.Navigation("IdAccessLevelNavigation");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.ProfileCoordinateEntity", b =>
                {
                    b.HasOne("DataAccessLayer.Entities.ProfileEntity", "IdProfileNavigation")
                        .WithMany("ProfileCoordinates")
                        .HasForeignKey("IdProfile")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK_ProfileCoordinate_Profile");

                    b.Navigation("IdProfileNavigation");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.ProfileEntity", b =>
                {
                    b.HasOne("DataAccessLayer.Entities.SquareEntity", "IdSquareNavigation")
                        .WithMany("Profiles")
                        .HasForeignKey("IdSquare")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK_Profile_Square");

                    b.Navigation("IdSquareNavigation");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.ProjectEntity", b =>
                {
                    b.HasOne("DataAccessLayer.Entities.CustomerEntity", "IdCustomerNavigation")
                        .WithMany("Projects")
                        .HasForeignKey("IdCustomer")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK_Project_Customer");

                    b.HasOne("DataAccessLayer.Entities.EmployeeEntity", "IdEmployeeNavigation")
                        .WithMany("Projects")
                        .HasForeignKey("IdEmployee")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK_Project_Employee");

                    b.Navigation("IdCustomerNavigation");

                    b.Navigation("IdEmployeeNavigation");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.ReportEntity", b =>
                {
                    b.HasOne("DataAccessLayer.Entities.EmployeeEntity", "IdEmployeeNavigation")
                        .WithMany("Reports")
                        .HasForeignKey("IdEmployee")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK_Report_Employee");

                    b.HasOne("DataAccessLayer.Entities.ProjectEntity", "IdProjectNavigation")
                        .WithMany("Reports")
                        .HasForeignKey("IdProject")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("FK_Report_Project");

                    b.Navigation("IdEmployeeNavigation");

                    b.Navigation("IdProjectNavigation");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.SquareEntity", b =>
                {
                    b.HasOne("DataAccessLayer.Entities.ProjectEntity", "IdProjectNavigation")
                        .WithMany("Squares")
                        .HasForeignKey("IdProject")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK_Square_Project");

                    b.Navigation("IdProjectNavigation");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.AccessLevelEntity", b =>
                {
                    b.Navigation("Positions");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.CustomerEntity", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.CustomerTypeEntity", b =>
                {
                    b.Navigation("Customers");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.EmployeeEntity", b =>
                {
                    b.Navigation("Points");

                    b.Navigation("Projects");

                    b.Navigation("Reports");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.EquipmentEntity", b =>
                {
                    b.Navigation("Points");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.PicketEntity", b =>
                {
                    b.Navigation("PicketCoordinates");

                    b.Navigation("Points");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.PositionEntity", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.ProfileEntity", b =>
                {
                    b.Navigation("Pickets");

                    b.Navigation("ProfileCoordinates");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.ProjectEntity", b =>
                {
                    b.Navigation("Reports");

                    b.Navigation("Squares");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.SquareEntity", b =>
                {
                    b.Navigation("AreaCoordinates");

                    b.Navigation("Profiles");
                });
#pragma warning restore 612, 618
        }
    }
}
