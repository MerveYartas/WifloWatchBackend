using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WifloWatchBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddWatchHistoryDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WatchHistory_Movies_MovieId",
                table: "WatchHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_WatchHistory_Users_UserId",
                table: "WatchHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WatchHistory",
                table: "WatchHistory");

            migrationBuilder.RenameTable(
                name: "WatchHistory",
                newName: "WatchHistories");

            migrationBuilder.RenameIndex(
                name: "IX_WatchHistory_UserId",
                table: "WatchHistories",
                newName: "IX_WatchHistories_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_WatchHistory_MovieId",
                table: "WatchHistories",
                newName: "IX_WatchHistories_MovieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WatchHistories",
                table: "WatchHistories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WatchHistories_Movies_MovieId",
                table: "WatchHistories",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WatchHistories_Users_UserId",
                table: "WatchHistories",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WatchHistories_Movies_MovieId",
                table: "WatchHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_WatchHistories_Users_UserId",
                table: "WatchHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WatchHistories",
                table: "WatchHistories");

            migrationBuilder.RenameTable(
                name: "WatchHistories",
                newName: "WatchHistory");

            migrationBuilder.RenameIndex(
                name: "IX_WatchHistories_UserId",
                table: "WatchHistory",
                newName: "IX_WatchHistory_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_WatchHistories_MovieId",
                table: "WatchHistory",
                newName: "IX_WatchHistory_MovieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WatchHistory",
                table: "WatchHistory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WatchHistory_Movies_MovieId",
                table: "WatchHistory",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WatchHistory_Users_UserId",
                table: "WatchHistory",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
