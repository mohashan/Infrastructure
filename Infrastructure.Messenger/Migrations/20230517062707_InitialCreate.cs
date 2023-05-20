﻿using System;
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
                name: "Groups",
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
                    table.PrimaryKey("PK_Groups", x => x.Id);
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
                    ContactTypeId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_ContactTypes_ContactTypeId",
                        column: x => x.ContactTypeId,
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
                    AuthorizationToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "ContactGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactId = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactGroups_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContactGroups_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
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
                    SentText = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                table: "ContactTypes",
                columns: new[] { "Id", "DeleteDate", "InsertDate", "IsDeleted", "Title" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2023, 5, 17, 9, 57, 7, 448, DateTimeKind.Local).AddTicks(5283), false, "People" },
                    { 2, null, new DateTime(2023, 5, 17, 9, 57, 7, 448, DateTimeKind.Local).AddTicks(5294), false, "Service" },
                    { 3, null, new DateTime(2023, 5, 17, 9, 57, 7, 448, DateTimeKind.Local).AddTicks(5295), false, "Device" }
                });

            migrationBuilder.InsertData(
                table: "Features",
                columns: new[] { "Id", "DataType", "DeleteDate", "Description", "InsertDate", "IsDeleted", "Title" },
                values: new object[,]
                {
                    { 1, "System.String", null, null, new DateTime(2023, 5, 17, 9, 57, 7, 448, DateTimeKind.Local).AddTicks(7537), false, "FullName" },
                    { 2, "System.String", null, null, new DateTime(2023, 5, 17, 9, 57, 7, 448, DateTimeKind.Local).AddTicks(7541), false, "NationalCode" },
                    { 3, "System.String", null, null, new DateTime(2023, 5, 17, 9, 57, 7, 448, DateTimeKind.Local).AddTicks(7542), false, "Address" },
                    { 4, "System.String", null, null, new DateTime(2023, 5, 17, 9, 57, 7, 448, DateTimeKind.Local).AddTicks(7593), false, "Gender" },
                    { 5, "System.String", null, null, new DateTime(2023, 5, 17, 9, 57, 7, 448, DateTimeKind.Local).AddTicks(7595), false, "e-Mail" },
                    { 6, "System.String", null, null, new DateTime(2023, 5, 17, 9, 57, 7, 448, DateTimeKind.Local).AddTicks(7596), false, "MobileNumber" }
                });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "DeleteDate", "InsertDate", "IsDeleted", "Title" },
                values: new object[] { 1, null, new DateTime(2023, 5, 17, 9, 57, 7, 448, DateTimeKind.Local).AddTicks(5496), false, "G1" });

            migrationBuilder.InsertData(
                table: "Templates",
                columns: new[] { "Id", "Body", "DeleteDate", "InsertDate", "IsDeleted", "Title" },
                values: new object[,]
                {
                    { 1, "Server @param0 has a problem: @param1", null, new DateTime(2023, 5, 17, 9, 57, 7, 448, DateTimeKind.Local).AddTicks(7784), false, "Server_Monitoring" },
                    { 2, "Service @param0 has a problem: @param1", null, new DateTime(2023, 5, 17, 9, 57, 7, 448, DateTimeKind.Local).AddTicks(7785), false, "Service_Monitoring" }
                });

            migrationBuilder.InsertData(
                table: "Channels",
                columns: new[] { "Id", "AuthorizationToken", "DeleteDate", "Description", "EndPoint", "FeatureId", "HttpRequestBody", "InsertDate", "IsDeleted", "Title" },
                values: new object[,]
                {
                    { 1, null, null, null, "https://sms.MyServices.com/Send", 6, "{to:'@to',text:'@text'}", new DateTime(2023, 5, 17, 9, 57, 7, 448, DateTimeKind.Local).AddTicks(7700), false, "SMS" },
                    { 2, null, null, null, "https://EMail.MyServices.com/Send", 5, "{to:'@to',text:'@text'}", new DateTime(2023, 5, 17, 9, 57, 7, 448, DateTimeKind.Local).AddTicks(7703), false, "e-Mail" }
                });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "ContactTypeId", "DeleteDate", "InsertDate", "IsDeleted", "Title" },
                values: new object[,]
                {
                    { 1, 1, null, new DateTime(2023, 5, 17, 9, 57, 7, 448, DateTimeKind.Local).AddTicks(5587), false, "Admin" },
                    { 2, 1, null, new DateTime(2023, 5, 17, 9, 57, 7, 448, DateTimeKind.Local).AddTicks(5589), false, "SupportUser" }
                });

            migrationBuilder.InsertData(
                table: "ContactFeatures",
                columns: new[] { "Id", "ContactId", "DeleteDate", "FeatureId", "InsertDate", "IsDeleted", "Title", "Value" },
                values: new object[,]
                {
                    { 1, 1, null, 1, new DateTime(2023, 5, 17, 9, 57, 7, 448, DateTimeKind.Local).AddTicks(6760), false, "FullName", "Administrator" },
                    { 2, 1, null, 5, new DateTime(2023, 5, 17, 9, 57, 7, 448, DateTimeKind.Local).AddTicks(6764), false, "e-Mail", "Admin@MyServices.com" },
                    { 3, 1, null, 6, new DateTime(2023, 5, 17, 9, 57, 7, 448, DateTimeKind.Local).AddTicks(6765), false, "MobileNumber", "+9898765432101" },
                    { 4, 2, null, 1, new DateTime(2023, 5, 17, 9, 57, 7, 448, DateTimeKind.Local).AddTicks(6766), false, "FullName", "Support User" },
                    { 5, 2, null, 5, new DateTime(2023, 5, 17, 9, 57, 7, 448, DateTimeKind.Local).AddTicks(6766), false, "e-Mail", "Support@MyServices.com" },
                    { 6, 2, null, 6, new DateTime(2023, 5, 17, 9, 57, 7, 448, DateTimeKind.Local).AddTicks(6767), false, "MobileNumber", "+9898765432102" }
                });

            migrationBuilder.InsertData(
                table: "ContactGroups",
                columns: new[] { "Id", "ContactId", "DeleteDate", "GroupId", "InsertDate", "IsDeleted", "Title" },
                values: new object[,]
                {
                    { 1, 1, null, 1, new DateTime(2023, 5, 17, 9, 57, 7, 448, DateTimeKind.Local).AddTicks(5669), false, "Admin" },
                    { 2, 2, null, 1, new DateTime(2023, 5, 17, 9, 57, 7, 448, DateTimeKind.Local).AddTicks(5671), false, "SupportUser" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Channels_FeatureId",
                table: "Channels",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactFeatures_ContactId_FeatureId",
                table: "ContactFeatures",
                columns: new[] { "ContactId", "FeatureId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContactFeatures_FeatureId",
                table: "ContactFeatures",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactGroups_ContactId_GroupId",
                table: "ContactGroups",
                columns: new[] { "ContactId", "GroupId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContactGroups_GroupId",
                table: "ContactGroups",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_ContactTypeId",
                table: "Contacts",
                column: "ContactTypeId");

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
                name: "ContactGroups");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Channels");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Templates");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "ContactTypes");
        }
    }
}