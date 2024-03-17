using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pharma_quick_test2.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActiveIngredients",
                columns: table => new
                {
                    IngredientID = table.Column<int>(type: "int", nullable: false),
                    IngredientName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ActiveIn__BEAEB27AA35F184D", x => x.IngredientID);
                });

            migrationBuilder.CreateTable(
                name: "MedicationCategories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    CategoryName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Medicati__19093A2BC6D087EB", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Medications",
                columns: table => new
                {
                    MedicationID = table.Column<int>(type: "int", nullable: false),
                    MedicationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UseOfMedications = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContraindicationsForUse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Precautions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SideEffects = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medications", x => x.MedicationID);
                    table.ForeignKey(
                        name: "FK__Medicatio__Categ__267ABA7A",
                        column: x => x.CategoryID,
                        principalTable: "MedicationCategories",
                        principalColumn: "CategoryID");
                });

            migrationBuilder.CreateTable(
                name: "MedicationActiveIngredients",
                columns: table => new
                {
                    MedicationID = table.Column<int>(type: "int", nullable: false),
                    IngredientID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Medicati__D906F1FDC044A754", x => new { x.MedicationID, x.IngredientID });
                    table.ForeignKey(
                        name: "FK__Medicatio__Ingre__2C3393D0",
                        column: x => x.IngredientID,
                        principalTable: "ActiveIngredients",
                        principalColumn: "IngredientID");
                    table.ForeignKey(
                        name: "FK__Medicatio__Medic__2B3F6F97",
                        column: x => x.MedicationID,
                        principalTable: "Medications",
                        principalColumn: "MedicationID");
                });

            migrationBuilder.CreateTable(
                name: "MedicationReplacements",
                columns: table => new
                {
                    medId = table.Column<int>(type: "int", nullable: false),
                    replacementId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Medicati__5A96AEE443919F42", x => new { x.medId, x.replacementId });
                    table.ForeignKey(
                        name: "FK__Medicatio__medId__6477ECF3",
                        column: x => x.medId,
                        principalTable: "Medications",
                        principalColumn: "MedicationID");
                    table.ForeignKey(
                        name: "FK__Medicatio__repla__656C112C",
                        column: x => x.replacementId,
                        principalTable: "Medications",
                        principalColumn: "MedicationID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicationActiveIngredients_IngredientID",
                table: "MedicationActiveIngredients",
                column: "IngredientID");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationReplacements_replacementId",
                table: "MedicationReplacements",
                column: "replacementId");

            migrationBuilder.CreateIndex(
                name: "IX_Medications_CategoryID",
                table: "Medications",
                column: "CategoryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicationActiveIngredients");

            migrationBuilder.DropTable(
                name: "MedicationReplacements");

            migrationBuilder.DropTable(
                name: "ActiveIngredients");

            migrationBuilder.DropTable(
                name: "Medications");

            migrationBuilder.DropTable(
                name: "MedicationCategories");
        }
    }
}
