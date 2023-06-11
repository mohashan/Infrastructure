using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.BaseUserManager.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_UserTypes_UserTypeId",
                        column: x => x.UserTypeId,
                        principalTable: "UserTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserFeatures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FeatureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFeatures_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFeatures_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserGroups_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGroups_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Features",
                columns: new[] { "Id", "DataType", "DeleteDate", "Description", "InsertDate", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { new Guid("4366318f-fc11-4909-96be-e6d00c07b6a3"), "System.String", null, null, new DateTime(2023, 6, 12, 0, 10, 1, 744, DateTimeKind.Local).AddTicks(372), false, "FullName" },
                    { new Guid("45058ee7-bf5f-4c94-bd40-4a928aeb2901"), "System.String", null, null, new DateTime(2023, 6, 12, 0, 10, 1, 744, DateTimeKind.Local).AddTicks(386), false, "MobileNumber" },
                    { new Guid("5f6e5a83-4fd7-43f0-87b7-876423eaea28"), "System.String", null, null, new DateTime(2023, 6, 12, 0, 10, 1, 744, DateTimeKind.Local).AddTicks(379), false, "NationalCode" },
                    { new Guid("95b6e8fd-67d9-4a24-aa00-44728e270963"), "System.String", null, null, new DateTime(2023, 6, 12, 0, 10, 1, 744, DateTimeKind.Local).AddTicks(380), false, "Address" },
                    { new Guid("e4662c7b-97a4-4a62-9644-50498f81e565"), "System.String", null, null, new DateTime(2023, 6, 12, 0, 10, 1, 744, DateTimeKind.Local).AddTicks(382), false, "Gender" },
                    { new Guid("ea8b4275-d881-4577-bf45-696e5320b7e3"), "System.String", null, null, new DateTime(2023, 6, 12, 0, 10, 1, 744, DateTimeKind.Local).AddTicks(384), false, "e-Mail" }
                });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "DeleteDate", "InsertDate", "IsDeleted", "Name" },
                values: new object[] { new Guid("f656b877-99ec-4034-b824-8be4da79d345"), null, new DateTime(2023, 6, 12, 0, 10, 1, 743, DateTimeKind.Local).AddTicks(7019), false, "G1" });

            migrationBuilder.InsertData(
                table: "UserTypes",
                columns: new[] { "Id", "DeleteDate", "InsertDate", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { new Guid("9ea818c5-089f-4b17-aec1-ce91758ada43"), null, new DateTime(2023, 6, 12, 0, 10, 1, 743, DateTimeKind.Local).AddTicks(6445), false, "Service" },
                    { new Guid("d6fb8547-d54c-4193-9b72-e3a67ad62708"), null, new DateTime(2023, 6, 12, 0, 10, 1, 743, DateTimeKind.Local).AddTicks(6425), false, "People" },
                    { new Guid("e3aa6baf-6bae-4425-b45b-e3b4efc61a0e"), null, new DateTime(2023, 6, 12, 0, 10, 1, 743, DateTimeKind.Local).AddTicks(6447), false, "Device" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DeleteDate", "InsertDate", "IsDeleted", "Name", "UserTypeId" },
                values: new object[] { new Guid("93cbfd70-522d-4c56-9290-5b4311afea30"), null, new DateTime(2023, 6, 12, 0, 10, 1, 743, DateTimeKind.Local).AddTicks(7283), false, "SupportUser", new Guid("d6fb8547-d54c-4193-9b72-e3a67ad62708") });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DeleteDate", "InsertDate", "IsDeleted", "Name", "UserTypeId" },
                values: new object[] { new Guid("b1008c58-1d88-4898-99c0-3ff4f9a87ee2"), null, new DateTime(2023, 6, 12, 0, 10, 1, 743, DateTimeKind.Local).AddTicks(7280), false, "Admin", new Guid("d6fb8547-d54c-4193-9b72-e3a67ad62708") });

            migrationBuilder.InsertData(
                table: "UserFeatures",
                columns: new[] { "Id", "DeleteDate", "FeatureId", "InsertDate", "IsDeleted", "Name", "UserId", "Value" },
                values: new object[,]
                {
                    { new Guid("0422cf02-7930-4ed9-9c60-f87db1e682b6"), null, new Guid("45058ee7-bf5f-4c94-bd40-4a928aeb2901"), new DateTime(2023, 6, 12, 0, 10, 1, 744, DateTimeKind.Local).AddTicks(556), false, "MobileNumber", new Guid("93cbfd70-522d-4c56-9290-5b4311afea30"), "+9898765432102" },
                    { new Guid("11231a07-7c1c-4f28-b28d-04bfa86e7ebb"), null, new Guid("ea8b4275-d881-4577-bf45-696e5320b7e3"), new DateTime(2023, 6, 12, 0, 10, 1, 744, DateTimeKind.Local).AddTicks(554), false, "e-Mail", new Guid("93cbfd70-522d-4c56-9290-5b4311afea30"), "Support@MyServices.com" },
                    { new Guid("3b869f1f-cb36-45c1-931b-97388148051f"), null, new Guid("ea8b4275-d881-4577-bf45-696e5320b7e3"), new DateTime(2023, 6, 12, 0, 10, 1, 744, DateTimeKind.Local).AddTicks(547), false, "e-Mail", new Guid("b1008c58-1d88-4898-99c0-3ff4f9a87ee2"), "Admin@MyServices.com" },
                    { new Guid("62b35656-9357-42f5-8bdf-3ceac9dca45c"), null, new Guid("45058ee7-bf5f-4c94-bd40-4a928aeb2901"), new DateTime(2023, 6, 12, 0, 10, 1, 744, DateTimeKind.Local).AddTicks(550), false, "MobileNumber", new Guid("b1008c58-1d88-4898-99c0-3ff4f9a87ee2"), "+9898765432101" },
                    { new Guid("6c41eed0-16ce-46e1-8915-6119b6fb3275"), null, new Guid("4366318f-fc11-4909-96be-e6d00c07b6a3"), new DateTime(2023, 6, 12, 0, 10, 1, 744, DateTimeKind.Local).AddTicks(552), false, "FullName", new Guid("93cbfd70-522d-4c56-9290-5b4311afea30"), "Support User" },
                    { new Guid("fa5ada69-7f37-47eb-a849-db6a1ab3de70"), null, new Guid("4366318f-fc11-4909-96be-e6d00c07b6a3"), new DateTime(2023, 6, 12, 0, 10, 1, 744, DateTimeKind.Local).AddTicks(540), false, "FullName", new Guid("b1008c58-1d88-4898-99c0-3ff4f9a87ee2"), "Administrator" }
                });

            migrationBuilder.InsertData(
                table: "UserGroups",
                columns: new[] { "Id", "DeleteDate", "GroupId", "InsertDate", "IsDeleted", "Name", "UserId" },
                values: new object[,]
                {
                    { new Guid("360d02e7-1577-426d-9c25-7a891452d7a1"), null, new Guid("f656b877-99ec-4034-b824-8be4da79d345"), new DateTime(2023, 6, 12, 0, 10, 1, 743, DateTimeKind.Local).AddTicks(7696), false, "SupportUser", new Guid("93cbfd70-522d-4c56-9290-5b4311afea30") },
                    { new Guid("39a8a732-6c8d-4759-85a3-8c3afa2030b6"), null, new Guid("f656b877-99ec-4034-b824-8be4da79d345"), new DateTime(2023, 6, 12, 0, 10, 1, 743, DateTimeKind.Local).AddTicks(7688), false, "Admin", new Guid("b1008c58-1d88-4898-99c0-3ff4f9a87ee2") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserFeatures_FeatureId",
                table: "UserFeatures",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFeatures_UserId_FeatureId",
                table: "UserFeatures",
                columns: new[] { "UserId", "FeatureId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserGroups_GroupId",
                table: "UserGroups",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroups_UserId_GroupId",
                table: "UserGroups",
                columns: new[] { "UserId", "GroupId" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserTypeId",
                table: "Users",
                column: "UserTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFeatures");

            migrationBuilder.DropTable(
                name: "UserGroups");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserTypes");
        }
    }
}
