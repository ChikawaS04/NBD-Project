using Microsoft.EntityFrameworkCore.Migrations;

namespace NBD3.Data
{
    public static class ExtraMigration
    {

        public static void Steps(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql(
                 @"
                            Drop View IF EXISTS [BidMSummaries];
		Create View BidMSummaries as
        Select p.ID, 
            p.InventoryCode, 
            p.InventoryPriceList, 
            p.InventoryDescription,
            b.CategoryName, 
            Sum(a.Quantity) as NumberOfAppointments, 
            Sum(a.extraFee) as TotalMaterial 
            From Bids 
            p Join Inventorys 
            a on p.ID = a.BidID
            Group By p.ID, p.InventoryCode, p.InventoryPriceList, p.InventoryDescription, b.CategoryName, a.Quantity;
                ");

        }

    }
}
