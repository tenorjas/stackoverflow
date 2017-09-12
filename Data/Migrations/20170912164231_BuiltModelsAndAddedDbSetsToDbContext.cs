using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace stackoverflow.Data.Migrations
{
    public partial class BuiltModelsAndAddedDbSetsToDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsModerator",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "QuestionModel",
                columns: table => new
                {
                    QuestionID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ApplicationUserId = table.Column<string>(type: "TEXT", nullable: true),
                    Body = table.Column<string>(type: "TEXT", nullable: true),
                    PostDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: true),
                    VoteCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionModel", x => x.QuestionID);
                    table.ForeignKey(
                        name: "FK_QuestionModel_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TagModel",
                columns: table => new
                {
                    TagID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TagName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagModel", x => x.TagID);
                });

            migrationBuilder.CreateTable(
                name: "AnswerModel",
                columns: table => new
                {
                    AnswerID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ApplicationUserId = table.Column<string>(type: "TEXT", nullable: true),
                    Body = table.Column<string>(type: "TEXT", nullable: true),
                    PostDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    QuestionID = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: true),
                    VoteCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerModel", x => x.AnswerID);
                    table.ForeignKey(
                        name: "FK_AnswerModel_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnswerModel_QuestionModel_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "QuestionModel",
                        principalColumn: "QuestionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QtieModel",
                columns: table => new
                {
                    QtieID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    QuestionID = table.Column<int>(type: "INTEGER", nullable: false),
                    TagID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QtieModel", x => x.QtieID);
                    table.ForeignKey(
                        name: "FK_QtieModel_QuestionModel_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "QuestionModel",
                        principalColumn: "QuestionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QtieModel_TagModel_TagID",
                        column: x => x.TagID,
                        principalTable: "TagModel",
                        principalColumn: "TagID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommentModel",
                columns: table => new
                {
                    CommentID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AnswerID = table.Column<int>(type: "INTEGER", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "TEXT", nullable: true),
                    Body = table.Column<string>(type: "TEXT", nullable: true),
                    PostDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    QuestionID = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentModel", x => x.CommentID);
                    table.ForeignKey(
                        name: "FK_CommentModel_AnswerModel_AnswerID",
                        column: x => x.AnswerID,
                        principalTable: "AnswerModel",
                        principalColumn: "AnswerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommentModel_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommentModel_QuestionModel_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "QuestionModel",
                        principalColumn: "QuestionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnswerModel_ApplicationUserId",
                table: "AnswerModel",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerModel_QuestionID",
                table: "AnswerModel",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_CommentModel_AnswerID",
                table: "CommentModel",
                column: "AnswerID");

            migrationBuilder.CreateIndex(
                name: "IX_CommentModel_ApplicationUserId",
                table: "CommentModel",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentModel_QuestionID",
                table: "CommentModel",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_QtieModel_QuestionID",
                table: "QtieModel",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_QtieModel_TagID",
                table: "QtieModel",
                column: "TagID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionModel_ApplicationUserId",
                table: "QuestionModel",
                column: "ApplicationUserId");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CommentModel");

            migrationBuilder.DropTable(
                name: "QtieModel");

            migrationBuilder.DropTable(
                name: "AnswerModel");

            migrationBuilder.DropTable(
                name: "TagModel");

            migrationBuilder.DropTable(
                name: "QuestionModel");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsModerator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");
        }
    }
}
