using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccessLevel",
                columns: table => new
                {
                    ID_AccessLevel = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessLevel", x => x.ID_AccessLevel);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChangeDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OldValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NewValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChangeLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerType",
                columns: table => new
                {
                    ID_CustomerType = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerType", x => x.ID_CustomerType);
                });

            migrationBuilder.CreateTable(
                name: "Equipment",
                columns: table => new
                {
                    ID_Equipment = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment", x => x.ID_Equipment);
                });

            migrationBuilder.CreateTable(
                name: "Position",
                columns: table => new
                {
                    ID_Position = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Salary = table.Column<int>(type: "int", nullable: false),
                    ID_AccessLevel = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Position", x => x.ID_Position);
                    table.ForeignKey(
                        name: "FK_Position_AccessLevel",
                        column: x => x.ID_AccessLevel,
                        principalTable: "AccessLevel",
                        principalColumn: "ID_AccessLevel",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    ID_Customer = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Login = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ID_Type = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.ID_Customer);
                    table.ForeignKey(
                        name: "FK_Customer_CustomerType",
                        column: x => x.ID_Type,
                        principalTable: "CustomerType",
                        principalColumn: "ID_CustomerType",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    ID_Employee = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Passport = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Login = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ID_Position = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.ID_Employee);
                    table.ForeignKey(
                        name: "FK_Employee_Position",
                        column: x => x.ID_Position,
                        principalTable: "Position",
                        principalColumn: "ID_Position",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    ID_Project = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ID_Customer = table.Column<int>(type: "int", nullable: true),
                    ID_Employee = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.ID_Project);
                    table.ForeignKey(
                        name: "FK_Project_Customer",
                        column: x => x.ID_Customer,
                        principalTable: "Customer",
                        principalColumn: "ID_Customer",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Project_Employee",
                        column: x => x.ID_Employee,
                        principalTable: "Employee",
                        principalColumn: "ID_Employee",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Report",
                columns: table => new
                {
                    ID_Report = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Employee = table.Column<int>(type: "int", nullable: true),
                    ID_Project = table.Column<int>(type: "int", nullable: true),
                    ReportFile = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.ID_Report);
                    table.ForeignKey(
                        name: "FK_Report_Employee",
                        column: x => x.ID_Employee,
                        principalTable: "Employee",
                        principalColumn: "ID_Employee",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Report_Project",
                        column: x => x.ID_Project,
                        principalTable: "Project",
                        principalColumn: "ID_Project",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Square",
                columns: table => new
                {
                    ID_Square = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Alitude = table.Column<int>(type: "int", nullable: false),
                    ID_Project = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Square", x => x.ID_Square);
                    table.ForeignKey(
                        name: "FK_Square_Project",
                        column: x => x.ID_Project,
                        principalTable: "Project",
                        principalColumn: "ID_Project",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "AreaCoordinates",
                columns: table => new
                {
                    ID_Record = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Square = table.Column<int>(type: "int", nullable: true),
                    X = table.Column<double>(type: "float", nullable: false),
                    Y = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreaCoordinate", x => x.ID_Record);
                    table.ForeignKey(
                        name: "FK_AreaCoordinate_Square",
                        column: x => x.ID_Square,
                        principalTable: "Square",
                        principalColumn: "ID_Square",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Profile",
                columns: table => new
                {
                    ID_profile = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ID_Square = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profile", x => x.ID_profile);
                    table.ForeignKey(
                        name: "FK_Profile_Square",
                        column: x => x.ID_Square,
                        principalTable: "Square",
                        principalColumn: "ID_Square",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Picket",
                columns: table => new
                {
                    ID_Picket = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ID_profile = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Picket", x => x.ID_Picket);
                    table.ForeignKey(
                        name: "FK_Picket_Profile",
                        column: x => x.ID_profile,
                        principalTable: "Profile",
                        principalColumn: "ID_profile",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProfileCoordinates",
                columns: table => new
                {
                    ID_Record = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_profile = table.Column<int>(type: "int", nullable: true),
                    X = table.Column<double>(type: "float", nullable: false),
                    Y = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileCoordinate", x => x.ID_Record);
                    table.ForeignKey(
                        name: "FK_ProfileCoordinate_Profile",
                        column: x => x.ID_profile,
                        principalTable: "Profile",
                        principalColumn: "ID_profile",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "PicketCoordinates",
                columns: table => new
                {
                    ID_Record = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Picket = table.Column<int>(type: "int", nullable: true),
                    X = table.Column<double>(type: "float", nullable: false),
                    Y = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PicketCoordinate", x => x.ID_Record);
                    table.ForeignKey(
                        name: "FK_PicketCoordinate_Picket",
                        column: x => x.ID_Picket,
                        principalTable: "Picket",
                        principalColumn: "ID_Picket",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Point",
                columns: table => new
                {
                    ID_Point = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    X = table.Column<double>(type: "float", nullable: false),
                    Y = table.Column<double>(type: "float", nullable: false),
                    Gravity = table.Column<double>(type: "float", nullable: false),
                    GravityAnomaly = table.Column<double>(type: "float", nullable: false),
                    Amendments = table.Column<double>(type: "float", nullable: false),
                    Datetime = table.Column<DateTime>(type: "datetime", nullable: false),
                    ID_Operator = table.Column<int>(type: "int", nullable: true),
                    ID_Equipment = table.Column<int>(type: "int", nullable: true),
                    ID_Picket = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Point", x => x.ID_Point);
                    table.ForeignKey(
                        name: "FK_Point_Employee",
                        column: x => x.ID_Operator,
                        principalTable: "Employee",
                        principalColumn: "ID_Employee",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Point_Equipment",
                        column: x => x.ID_Equipment,
                        principalTable: "Equipment",
                        principalColumn: "ID_Equipment",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Point_Picket",
                        column: x => x.ID_Picket,
                        principalTable: "Picket",
                        principalColumn: "ID_Picket",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AreaCoordinates_ID_Square",
                table: "AreaCoordinates",
                column: "ID_Square");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_ID_Type",
                table: "Customer",
                column: "ID_Type");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_ID_Position",
                table: "Employee",
                column: "ID_Position");

            migrationBuilder.CreateIndex(
                name: "UQ_Employee_Passport",
                table: "Employee",
                column: "Passport",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Equipment_SerialNumber",
                table: "Equipment",
                column: "SerialNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Picket_ID_profile",
                table: "Picket",
                column: "ID_profile");

            migrationBuilder.CreateIndex(
                name: "IX_PicketCoordinates_ID_Picket",
                table: "PicketCoordinates",
                column: "ID_Picket");

            migrationBuilder.CreateIndex(
                name: "IX_Point_ID_Equipment",
                table: "Point",
                column: "ID_Equipment");

            migrationBuilder.CreateIndex(
                name: "IX_Point_ID_Operator",
                table: "Point",
                column: "ID_Operator");

            migrationBuilder.CreateIndex(
                name: "IX_Point_ID_Picket",
                table: "Point",
                column: "ID_Picket");

            migrationBuilder.CreateIndex(
                name: "IX_Position_ID_AccessLevel",
                table: "Position",
                column: "ID_AccessLevel");

            migrationBuilder.CreateIndex(
                name: "IX_Profile_ID_Square",
                table: "Profile",
                column: "ID_Square");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileCoordinates_ID_profile",
                table: "ProfileCoordinates",
                column: "ID_profile");

            migrationBuilder.CreateIndex(
                name: "IX_Project_ID_Customer",
                table: "Project",
                column: "ID_Customer");

            migrationBuilder.CreateIndex(
                name: "IX_Project_ID_Employee",
                table: "Project",
                column: "ID_Employee");

            migrationBuilder.CreateIndex(
                name: "IX_Report_ID_Employee",
                table: "Report",
                column: "ID_Employee");

            migrationBuilder.CreateIndex(
                name: "IX_Report_ID_Project",
                table: "Report",
                column: "ID_Project");

            migrationBuilder.CreateIndex(
                name: "IX_Square_ID_Project",
                table: "Square",
                column: "ID_Project");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AreaCoordinates");

            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "PicketCoordinates");

            migrationBuilder.DropTable(
                name: "Point");

            migrationBuilder.DropTable(
                name: "ProfileCoordinates");

            migrationBuilder.DropTable(
                name: "Report");

            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.DropTable(
                name: "Picket");

            migrationBuilder.DropTable(
                name: "Profile");

            migrationBuilder.DropTable(
                name: "Square");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "CustomerType");

            migrationBuilder.DropTable(
                name: "Position");

            migrationBuilder.DropTable(
                name: "AccessLevel");
        }
    }
}
