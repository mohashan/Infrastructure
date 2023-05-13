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
                name: "ContactGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Templates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Templates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    ContactGroupId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_ContactGroups_ContactGroupId",
                        column: x => x.ContactGroupId,
                        principalTable: "ContactGroups",
                        principalColumn: "Id");
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
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndPoint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FeatureId = table.Column<int>(type: "int", nullable: false),
                    HttpRequestBody = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
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

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChannelId = table.Column<int>(type: "int", nullable: false),
                    ContactId = table.Column<int>(type: "int", nullable: false),
                    TemplateId = table.Column<int>(type: "int", nullable: false),
                    Parameters = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<int>(type: "int", nullable: false),
                    Response = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Channels_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "Channels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_Templates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "Templates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ContactGroups",
                columns: new[] { "Id", "DeleteDate", "InsertDate", "IsDeleted", "Title" },
                values: new object[] { 1, null, new DateTime(2023, 5, 13, 19, 45, 27, 925, DateTimeKind.Local).AddTicks(8353), false, "G1" });

            migrationBuilder.InsertData(
                table: "ContactTypes",
                columns: new[] { "Id", "DeleteDate", "InsertDate", "IsDeleted", "Title" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2023, 5, 13, 19, 45, 27, 925, DateTimeKind.Local).AddTicks(8108), false, "People" },
                    { 2, null, new DateTime(2023, 5, 13, 19, 45, 27, 925, DateTimeKind.Local).AddTicks(8118), false, "Service" },
                    { 3, null, new DateTime(2023, 5, 13, 19, 45, 27, 925, DateTimeKind.Local).AddTicks(8119), false, "Device" }
                });

            migrationBuilder.InsertData(
                table: "Features",
                columns: new[] { "Id", "DataType", "DeleteDate", "Description", "InsertDate", "IsDeleted", "Title" },
                values: new object[,]
                {
                    { 1, "System.String", null, null, new DateTime(2023, 5, 13, 19, 45, 27, 925, DateTimeKind.Local).AddTicks(8603), false, "FullName" },
                    { 2, "System.String", null, null, new DateTime(2023, 5, 13, 19, 45, 27, 925, DateTimeKind.Local).AddTicks(8606), false, "NationalCode" },
                    { 3, "System.String", null, null, new DateTime(2023, 5, 13, 19, 45, 27, 925, DateTimeKind.Local).AddTicks(8607), false, "Address" },
                    { 4, "System.String", null, null, new DateTime(2023, 5, 13, 19, 45, 27, 925, DateTimeKind.Local).AddTicks(8608), false, "Gender" },
                    { 5, "System.String", null, null, new DateTime(2023, 5, 13, 19, 45, 27, 925, DateTimeKind.Local).AddTicks(8609), false, "e-Mail" },
                    { 6, "System.String", null, null, new DateTime(2023, 5, 13, 19, 45, 27, 925, DateTimeKind.Local).AddTicks(8610), false, "MobileNumber" }
                });

            migrationBuilder.InsertData(
                table: "Templates",
                columns: new[] { "Id", "Body", "DeleteDate", "InsertDate", "IsDeleted", "Title" },
                values: new object[,]
                {
                    { 1, "Server @param0 has a problem: @param1", null, new DateTime(2023, 5, 13, 19, 45, 27, 925, DateTimeKind.Local).AddTicks(8769), false, "Server_Monitoring" },
                    { 2, "Service @param0 has a problem: @param1", null, new DateTime(2023, 5, 13, 19, 45, 27, 925, DateTimeKind.Local).AddTicks(8772), false, "Service_Monitoring" }
                });

            migrationBuilder.InsertData(
                table: "Channels",
                columns: new[] { "Id", "DeleteDate", "Description", "EndPoint", "FeatureId", "HttpRequestBody", "InsertDate", "IsDeleted", "Title" },
                values: new object[,]
                {
                    { 1, null, null, "https://sms.MyServices.com/Send", 6, "{to:'@to',text:'@text'}", new DateTime(2023, 5, 13, 19, 45, 27, 925, DateTimeKind.Local).AddTicks(8691), false, "SMS" },
                    { 2, null, null, "https://EMail.MyServices.com/Send", 5, "{to:'@to',text:'@text'}", new DateTime(2023, 5, 13, 19, 45, 27, 925, DateTimeKind.Local).AddTicks(8693), false, "e-Mail" }
                });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "ContactGroupId", "DeleteDate", "InsertDate", "IsDeleted", "Title", "TypeId" },
                values: new object[,]
                {
                    { 1, 1, null, new DateTime(2023, 5, 13, 19, 45, 27, 925, DateTimeKind.Local).AddTicks(8443), false, "Admin", 1 },
                    { 2, null, null, new DateTime(2023, 5, 13, 19, 45, 27, 925, DateTimeKind.Local).AddTicks(8445), false, "SupportUser", 1 }
                });

            migrationBuilder.InsertData(
                table: "ContactFeatures",
                columns: new[] { "Id", "ContactId", "DeleteDate", "FeatureId", "InsertDate", "IsDeleted", "Title", "Value" },
                values: new object[,]
                {
                    { 1, 1, null, 1, new DateTime(2023, 5, 13, 19, 45, 27, 925, DateTimeKind.Local).AddTicks(8520), false, "FullName", "Administrator" },
                    { 2, 1, null, 5, new DateTime(2023, 5, 13, 19, 45, 27, 925, DateTimeKind.Local).AddTicks(8522), false, "e-Mail", "Admin@MyServices.com" },
                    { 3, 1, null, 6, new DateTime(2023, 5, 13, 19, 45, 27, 925, DateTimeKind.Local).AddTicks(8523), false, "MobileNumber", "+9898765432101" },
                    { 4, 2, null, 1, new DateTime(2023, 5, 13, 19, 45, 27, 925, DateTimeKind.Local).AddTicks(8525), false, "FullName", "Support User" },
                    { 5, 2, null, 5, new DateTime(2023, 5, 13, 19, 45, 27, 925, DateTimeKind.Local).AddTicks(8526), false, "e-Mail", "Support@MyServices.com" },
                    { 6, 2, null, 6, new DateTime(2023, 5, 13, 19, 45, 27, 925, DateTimeKind.Local).AddTicks(8526), false, "MobileNumber", "+9898765432102" }
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
                name: "IX_Contacts_ContactGroupId",
                table: "Contacts",
                column: "ContactGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_TypeId",
                table: "Contacts",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChannelId",
                table: "Messages",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ContactId",
                table: "Messages",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_TemplateId",
                table: "Messages",
                column: "TemplateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactFeatures");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Channels");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Templates");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "ContactGroups");

            migrationBuilder.DropTable(
                name: "ContactTypes");
        }
    }
}
