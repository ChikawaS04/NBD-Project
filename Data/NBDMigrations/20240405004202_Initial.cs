using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NBD3.Data.NBDMigrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClientCommpanyName = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false),
                    ClientFirstName = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    ClientMiddleName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    ClientLastName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ClientPhone = table.Column<string>(type: "TEXT", nullable: false),
                    ClientEmail = table.Column<string>(type: "TEXT", nullable: false),
                    ClientStreetAddress = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ClientPostalCode = table.Column<string>(type: "TEXT", nullable: false),
                    ClientCityAddress = table.Column<string>(type: "TEXT", nullable: false),
                    ClientCountryAddress = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "Labours",
                columns: table => new
                {
                    LabourID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LabourType = table.Column<string>(type: "TEXT", maxLength: 80, nullable: false),
                    LabourPriceHour = table.Column<double>(type: "REAL", nullable: false),
                    LabourCostHour = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Labours", x => x.LabourID);
                });

            migrationBuilder.CreateTable(
                name: "MaterialCategorys",
                columns: table => new
                {
                    MaterialCategoryID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CategoryName = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialCategorys", x => x.MaterialCategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Staffs",
                columns: table => new
                {
                    StaffID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StaffTitle = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    StaffFirstName = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    StaffLastname = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    StaffPhone = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    StaffEmail = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.StaffID);
                });

            migrationBuilder.CreateTable(
                name: "Statuss",
                columns: table => new
                {
                    StatusID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StatusName = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuss", x => x.StatusID);
                });

            migrationBuilder.CreateTable(
                name: "Inventorys",
                columns: table => new
                {
                    InventoryID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InventoryCode = table.Column<string>(type: "TEXT", nullable: false),
                    InventoryDescription = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false),
                    InventoryQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    InventoryUnitType = table.Column<string>(type: "TEXT", maxLength: 25, nullable: false),
                    InventoryPriceList = table.Column<double>(type: "REAL", nullable: false),
                    MaterialCategoryID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventorys", x => x.InventoryID);
                    table.ForeignKey(
                        name: "FK_Inventorys_MaterialCategorys_MaterialCategoryID",
                        column: x => x.MaterialCategoryID,
                        principalTable: "MaterialCategorys",
                        principalColumn: "MaterialCategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StaffPhotos",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Content = table.Column<byte[]>(type: "BLOB", nullable: true),
                    MimeType = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    StaffID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffPhotos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StaffPhotos_Staffs_StaffID",
                        column: x => x.StaffID,
                        principalTable: "Staffs",
                        principalColumn: "StaffID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StaffThumbnails",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Content = table.Column<byte[]>(type: "BLOB", nullable: true),
                    MimeType = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    StaffID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffThumbnails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StaffThumbnails_Staffs_StaffID",
                        column: x => x.StaffID,
                        principalTable: "Staffs",
                        principalColumn: "StaffID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bids",
                columns: table => new
                {
                    BidId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BidAmount = table.Column<double>(type: "REAL", nullable: false),
                    BidDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    BidCost = table.Column<double>(type: "REAL", nullable: false),
                    BidApprove = table.Column<bool>(type: "INTEGER", nullable: false),
                    BidRejectedManager = table.Column<bool>(type: "INTEGER", nullable: false),
                    BidApproveClient = table.Column<bool>(type: "INTEGER", nullable: false),
                    BidRejectedClient = table.Column<bool>(type: "INTEGER", nullable: false),
                    BidStatus = table.Column<string>(type: "TEXT", nullable: true),
                    BidStatusList = table.Column<int>(type: "INTEGER", nullable: false),
                    BidNoteReason = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true),
                    ProjectID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bids", x => x.BidId);
                });

            migrationBuilder.CreateTable(
                name: "LabourDetails",
                columns: table => new
                {
                    BidID = table.Column<int>(type: "INTEGER", nullable: false),
                    LabourID = table.Column<int>(type: "INTEGER", nullable: false),
                    LabourDetailId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabourDetails", x => new { x.LabourID, x.BidID });
                    table.ForeignKey(
                        name: "FK_LabourDetails_Bids_BidID",
                        column: x => x.BidID,
                        principalTable: "Bids",
                        principalColumn: "BidId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LabourDetails_Labours_LabourID",
                        column: x => x.LabourID,
                        principalTable: "Labours",
                        principalColumn: "LabourID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterialDetails",
                columns: table => new
                {
                    BidID = table.Column<int>(type: "INTEGER", nullable: false),
                    InventoryID = table.Column<int>(type: "INTEGER", nullable: false),
                    MaterialDetailId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialDetails", x => new { x.InventoryID, x.BidID });
                    table.ForeignKey(
                        name: "FK_MaterialDetails_Bids_BidID",
                        column: x => x.BidID,
                        principalTable: "Bids",
                        principalColumn: "BidId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialDetails_Inventorys_InventoryID",
                        column: x => x.InventoryID,
                        principalTable: "Inventorys",
                        principalColumn: "InventoryID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StaffDetails",
                columns: table => new
                {
                    BidID = table.Column<int>(type: "INTEGER", nullable: false),
                    StaffID = table.Column<int>(type: "INTEGER", nullable: false),
                    StaffDetailId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffDetails", x => new { x.StaffID, x.BidID });
                    table.ForeignKey(
                        name: "FK_StaffDetails_Bids_BidID",
                        column: x => x.BidID,
                        principalTable: "Bids",
                        principalColumn: "BidId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StaffDetails_Staffs_StaffID",
                        column: x => x.StaffID,
                        principalTable: "Staffs",
                        principalColumn: "StaffID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tracks",
                columns: table => new
                {
                    TrackID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TrackNotes = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true),
                    TrackDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    BidID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tracks", x => x.TrackID);
                    table.ForeignKey(
                        name: "FK_Tracks_Bids_BidID",
                        column: x => x.BidID,
                        principalTable: "Bids",
                        principalColumn: "BidId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LocationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LocationName = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false),
                    LocationPhone = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    LocationContactPer = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false),
                    LocationStreetAddress = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    LocationPostalCode = table.Column<string>(type: "TEXT", nullable: false),
                    LocationCityAddress = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    LocationCountryAddress = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ProjectId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LocationId);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProjectName = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false),
                    ProjectDescription = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true),
                    ProjectStartDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    ProjectEndDate = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    ClientId = table.Column<int>(type: "INTEGER", nullable: false),
                    LocationId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProjectStatus = table.Column<string>(type: "TEXT", nullable: false),
                    ProjectStatusList = table.Column<int>(type: "INTEGER", nullable: false),
                    ProjectApprove = table.Column<bool>(type: "INTEGER", nullable: false),
                    ProjectReject = table.Column<bool>(type: "INTEGER", nullable: false),
                    ProjectNoteReason = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true),
                    ProjectEndDateValue = table.Column<DateOnly>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_Projects_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Projects_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectPhotos",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Content = table.Column<byte[]>(type: "BLOB", nullable: true),
                    MimeType = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    ProjectID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectPhotos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProjectPhotos_Projects_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectThumbnails",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Content = table.Column<byte[]>(type: "BLOB", nullable: true),
                    MimeType = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    ProjectID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectThumbnails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProjectThumbnails_Projects_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bids_ProjectID",
                table: "Bids",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_ClientCommpanyName",
                table: "Clients",
                column: "ClientCommpanyName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inventorys_MaterialCategoryID",
                table: "Inventorys",
                column: "MaterialCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_LabourDetails_BidID",
                table: "LabourDetails",
                column: "BidID");

            migrationBuilder.CreateIndex(
                name: "IX_Labours_LabourType",
                table: "Labours",
                column: "LabourType",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_LocationName",
                table: "Locations",
                column: "LocationName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_ProjectId",
                table: "Locations",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialDetails_BidID",
                table: "MaterialDetails",
                column: "BidID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPhotos_ProjectID",
                table: "ProjectPhotos",
                column: "ProjectID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ClientId",
                table: "Projects",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_LocationId",
                table: "Projects",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectName",
                table: "Projects",
                column: "ProjectName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectThumbnails_ProjectID",
                table: "ProjectThumbnails",
                column: "ProjectID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StaffDetails_BidID",
                table: "StaffDetails",
                column: "BidID");

            migrationBuilder.CreateIndex(
                name: "IX_StaffPhotos_StaffID",
                table: "StaffPhotos",
                column: "StaffID");

            migrationBuilder.CreateIndex(
                name: "IX_StaffThumbnails_StaffID",
                table: "StaffThumbnails",
                column: "StaffID");

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_BidID",
                table: "Tracks",
                column: "BidID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Projects_ProjectID",
                table: "Bids",
                column: "ProjectID",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Projects_ProjectId",
                table: "Locations",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Projects_ProjectId",
                table: "Locations");

            migrationBuilder.DropTable(
                name: "LabourDetails");

            migrationBuilder.DropTable(
                name: "MaterialDetails");

            migrationBuilder.DropTable(
                name: "ProjectPhotos");

            migrationBuilder.DropTable(
                name: "ProjectThumbnails");

            migrationBuilder.DropTable(
                name: "StaffDetails");

            migrationBuilder.DropTable(
                name: "StaffPhotos");

            migrationBuilder.DropTable(
                name: "StaffThumbnails");

            migrationBuilder.DropTable(
                name: "Statuss");

            migrationBuilder.DropTable(
                name: "Tracks");

            migrationBuilder.DropTable(
                name: "Labours");

            migrationBuilder.DropTable(
                name: "Inventorys");

            migrationBuilder.DropTable(
                name: "Staffs");

            migrationBuilder.DropTable(
                name: "Bids");

            migrationBuilder.DropTable(
                name: "MaterialCategorys");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
