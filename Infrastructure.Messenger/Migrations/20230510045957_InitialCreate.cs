using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Messenger.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_ContactTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "ContactTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Channels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndPoint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChannelRequestType = table.Column<int>(type: "int", nullable: false),
                    FeatureId = table.Column<int>(type: "int", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Channels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Channels_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactId = table.Column<int>(type: "int", nullable: false),
                    FeatureId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactFeatures_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContactFeatures_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ContactTypes",
                columns: new[] { "Id", "DeleteDate", "InsertDate", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2023, 5, 10, 8, 29, 57, 29, DateTimeKind.Local).AddTicks(3644), false, "People" },
                    { 2, null, new DateTime(2023, 5, 10, 8, 29, 57, 29, DateTimeKind.Local).AddTicks(3653), false, "Service" },
                    { 3, null, new DateTime(2023, 5, 10, 8, 29, 57, 29, DateTimeKind.Local).AddTicks(3653), false, "Device" }
                });

            migrationBuilder.InsertData(
                table: "Features",
                columns: new[] { "Id", "DataType", "DeleteDate", "Description", "InsertDate", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, "System.String", null, null, new DateTime(2023, 5, 10, 8, 29, 57, 29, DateTimeKind.Local).AddTicks(3784), false, "FullName" },
                    { 2, "System.String", null, null, new DateTime(2023, 5, 10, 8, 29, 57, 29, DateTimeKind.Local).AddTicks(3786), false, "NationalCode" },
                    { 3, "System.String", null, null, new DateTime(2023, 5, 10, 8, 29, 57, 29, DateTimeKind.Local).AddTicks(3787), false, "Address" },
                    { 4, "System.String", null, null, new DateTime(2023, 5, 10, 8, 29, 57, 29, DateTimeKind.Local).AddTicks(3788), false, "Gender" },
                    { 5, "System.String", null, null, new DateTime(2023, 5, 10, 8, 29, 57, 29, DateTimeKind.Local).AddTicks(3789), false, "e-Mail" },
                    { 6, "System.String", null, null, new DateTime(2023, 5, 10, 8, 29, 57, 29, DateTimeKind.Local).AddTicks(3790), false, "MobileNumber" }
                });

            migrationBuilder.InsertData(
                table: "Channels",
                columns: new[] { "Id", "Body", "ChannelRequestType", "DeleteDate", "Description", "EndPoint", "FeatureId", "InsertDate", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, "{to:@MobileNumber,text:@Text}", 1, null, null, "https://sms.MyServices.com/Send", 6, new DateTime(2023, 5, 10, 8, 29, 57, 29, DateTimeKind.Local).AddTicks(3801), false, "SMS" },
                    { 2, null, 1, null, null, "https://EMail.MyServices.com/Send", 5, new DateTime(2023, 5, 10, 8, 29, 57, 29, DateTimeKind.Local).AddTicks(3803), false, "e-Mail" }
                });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "DeleteDate", "InsertDate", "IsDeleted", "Name", "TypeId" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2023, 5, 10, 8, 29, 57, 29, DateTimeKind.Local).AddTicks(3745), false, "Admin", 1 },
                    { 2, null, new DateTime(2023, 5, 10, 8, 29, 57, 29, DateTimeKind.Local).AddTicks(3748), false, "SupportUser", 1 }
                });

            migrationBuilder.InsertData(
                table: "ContactFeatures",
                columns: new[] { "Id", "ContactId", "DeleteDate", "FeatureId", "InsertDate", "IsDeleted", "Value" },
                values: new object[,]
                {
                    { 1, 1, null, 1, new DateTime(2023, 5, 10, 8, 29, 57, 29, DateTimeKind.Local).AddTicks(3761), false, "Administrator" },
                    { 2, 1, null, 5, new DateTime(2023, 5, 10, 8, 29, 57, 29, DateTimeKind.Local).AddTicks(3764), false, "Admin@MyServices.com" },
                    { 3, 1, null, 6, new DateTime(2023, 5, 10, 8, 29, 57, 29, DateTimeKind.Local).AddTicks(3765), false, "+9898765432101" },
                    { 4, 2, null, 1, new DateTime(2023, 5, 10, 8, 29, 57, 29, DateTimeKind.Local).AddTicks(3765), false, "Support User" },
                    { 5, 2, null, 5, new DateTime(2023, 5, 10, 8, 29, 57, 29, DateTimeKind.Local).AddTicks(3766), false, "Support@MyServices.com" },
                    { 6, 2, null, 6, new DateTime(2023, 5, 10, 8, 29, 57, 29, DateTimeKind.Local).AddTicks(3767), false, "+9898765432102" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Channels_FeatureId",
                table: "Channels",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactFeatures_ContactId",
                table: "ContactFeatures",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactFeatures_FeatureId",
                table: "ContactFeatures",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_TypeId",
                table: "Contacts",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Channels");

            migrationBuilder.DropTable(
                name: "ContactFeatures");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "ContactTypes");
        }
    }
}
