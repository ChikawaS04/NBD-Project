using Microsoft.EntityFrameworkCore;
using NBD3.Models;
using System.Diagnostics;

namespace NBD3.Data
{
    public static class NBDinitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            NBDContext context = applicationBuilder.ApplicationServices.CreateScope()
                .ServiceProvider.GetRequiredService<NBDContext>();

            try
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                //context.Database.Migrate();

                // Seed Clients
                if (!context.Clients.Any())
                {
                    context.Clients.AddRange(
                       new Client
                       {
                           ClientCommpanyName = "Key Venture",
                           ClientStreetAddress = "7th Floor, West Tower",
                           ClientCityAddress = "Toronto",
                           ClientCountryAddress = "Canada",
                           ClientPostalCode = "M8X 2X2",
                           ClientPhone = "(800)-316-2759",
                           ClientEmail = "design@keyventure.ca",
                           ClientFirstName = "Sandra",
                           ClientMiddleName = "T",
                           ClientLastName = "Smith"
                       },
                       new Client
                       {
                           ClientCommpanyName = "Roo Construction",
                           ClientStreetAddress = "Building 225-4N-14",
                           ClientCityAddress = "St Paul",
                           ClientCountryAddress = "Canada",
                           ClientPostalCode = "M1M 1M1",
                           ClientPhone = "(800)-328-0067",
                           ClientEmail = "marketing@rooconstruction.ca",
                           ClientFirstName = "John",
                           ClientMiddleName = "A",
                           ClientLastName = "Chevalier"
                       },
                       new Client
                       {
                           ClientCommpanyName = "Modern Structure",
                           ClientStreetAddress = "8833 Mansfield Ave.",
                           ClientCityAddress = "Morton Grove",
                           ClientCountryAddress = "Canada",
                           ClientPostalCode = "K2K 2K2",
                           ClientPhone = "(505)-265-3591",
                           ClientEmail = "sales@modernstructures.ca",
                           ClientFirstName = "George",
                           ClientMiddleName = "",
                           ClientLastName = "Hamilton"
                       },
                       new Client
                       {
                           ClientCommpanyName = "Omega Design",
                           ClientStreetAddress = "101 Wolf Dr.",
                           ClientCityAddress = "Thorofare",
                           ClientCountryAddress = "Canada",
                           ClientPostalCode = "L3L 3L3",
                           ClientPhone = "(800)-776-6939",
                           ClientEmail = "sales@omegadesign.ca",
                           ClientFirstName = "Charles",
                           ClientMiddleName = "",
                           ClientLastName = "Baute"
                       },
                       new Client
                       {
                           ClientCommpanyName = "Diamond Shine",
                           ClientStreetAddress = "721 Clinton Ave. Suite 11",
                           ClientCityAddress = "Huntsville",
                           ClientCountryAddress = "Canada",
                           ClientPostalCode = "V4V 4V4",
                           ClientPhone = "(800)-866-9797",
                           ClientEmail = "biosis.custserv@thomson.com",
                           ClientFirstName = "Thomas",
                           ClientMiddleName = "",
                           ClientLastName = "Vachon"
                       });
                    context.SaveChanges();
                }

                // Locations.
                if (!context.Locations.Any())
                {
                    context.Locations.AddRange(
                        new Models.Location
                        {
                            LocationName = "Burnaby, BC",
                            LocationStreetAddress = "123 Main St",
                            LocationPostalCode = "V5G 2J3",
                            LocationCityAddress = "Burnaby",
                            LocationCountryAddress = "Canada",
                            LocationPhone = "1234567890",
                            LocationContactPer = "John Doe"
                        },
                        new Models.Location
                        {
                            LocationName = "Highway 401",
                            LocationStreetAddress = "456 Highway Ave",
                            LocationPostalCode = "M9W 5M3",
                            LocationCityAddress = "Toronto",
                            LocationCountryAddress = "Canada",
                            LocationPhone = "1234567890",
                            LocationContactPer = "Jane Smith"
                        },
                        new Models.Location
                        {
                            LocationName = "Blanchard House",
                            LocationStreetAddress = "789 Blanchard St",
                            LocationPostalCode = "V8W 2G1",
                            LocationCityAddress = "Victoria",
                            LocationCountryAddress = "Canada",
                            LocationPhone = "1234567890",
                            LocationContactPer = "Chris Johnson"
                        },
                        new Models.Location
                        {
                            LocationName = "N. Vancouver, BC",
                            LocationStreetAddress = "321 Mountain Blvd",
                            LocationPostalCode = "V7R 2M1",
                            LocationCityAddress = "North Vancouver",
                            LocationCountryAddress = "Canada",
                            LocationPhone = "1234567890",
                            LocationContactPer = "Emily Davis"
                        });
                    context.SaveChanges();
                }

                //Material Category
                if (!context.MaterialCategorys.Any())
                {
                    context.MaterialCategorys.AddRange(
                        new MaterialCategory
                        {
                            CategoryName = "Plan"
                        },
                        new MaterialCategory
                        {
                            CategoryName = "Pottery"
                        },
                        new MaterialCategory
                        {
                            CategoryName = "Materials"
                        });
                    context.SaveChanges();
                }

                //Status options
                if (!context.Statuss.Any())
                {
                    context.Statuss.AddRange(
                        new Models.Status
                        {
                            StatusName = "design stage"
						},
                        new Status
                        {
                            StatusName = "Waiting approval"
						},
                        new Status
                        {
                            StatusName = "approved by client"
                        },
                        new Status
                        {
                            StatusName = "approved by NBD"
                        },
                        new Status
                        {
                            StatusName = "needs revision"
                        });
                    context.SaveChanges();
                }

                //Staff
                if (!context.Staffs.Any())
                {
                    context.Staffs.AddRange(
                        new Staff
                        {
                            StaffTitle = "Designer",
                            StaffFirstName = "Tamara ",
                            StaffLastname = "Bakken,",
                            StaffPhone = "5878956141",
                            StaffEmail = "tbakken@nbd.ca"
                        },
                        new Staff
                        {
                            StaffTitle = "General Manager",
                            StaffFirstName = "Stan",
                            StaffLastname = "Fenton",
                            StaffPhone = "9042289637",
                            StaffEmail = "sfenton@nbd.ca"
                        },
                        new Staff
                        {
                            StaffTitle = "Design Manager",
                            StaffFirstName = "Keri",
                            StaffLastname = "Yamaguchi",
                            StaffPhone = "2899638417",
                            StaffEmail = "kyamaguchi@nbd.ca"
                        },
                        new Staff
                        {
                            StaffTitle = "Group Manager",
                            StaffFirstName = "Cheryl",
                            StaffLastname = "Poy",
                            StaffPhone = "9057418529",
                            StaffEmail = "cpoy@nbd.ca"
                        },
                        new Staff
                        {
                            StaffTitle = "Admin Assistant",
                            StaffFirstName = "Connie",
                            StaffLastname = "Nguyen",
                            StaffPhone = "2598889634",
                            StaffEmail = "cnguyen@nbd.ca"
                        },
                        new Staff
                        {
                            StaffTitle = "Sales Assoc",
                            StaffFirstName = "Bob",
                            StaffLastname = "Reinhardt",
                            StaffPhone = "9058529634",
                            StaffEmail = "breinhardt@nbd.ca"
                        },
                        new Staff
                        {
                            StaffTitle = "Group Manager",
                            StaffFirstName = "Sue",
                            StaffLastname = "Kaufman",
                            StaffPhone = "9057896542",
                            StaffEmail = "skaufman@nbd.ca"
                        },
                        new Staff
                        {
                            StaffTitle = "Production Worker",
                            StaffFirstName = "Monica",
                            StaffLastname = "Goce",
                            StaffPhone = "2897891232",
                            StaffEmail = "mgoce@nbd.ca"
                        },
                        new Staff
                        {
                            StaffTitle = "Production Worker",
                            StaffFirstName = "Bert",
                            StaffLastname = "Swenson",
                            StaffPhone = "9051236541",
                            StaffEmail = "bswenson@nbd.ca"
                        },
                        new Staff
                        {
                            StaffTitle = "Designer",
                            StaffFirstName = "De",
                            StaffLastname = "Signer01",
                            StaffPhone = "90651236941",
                            StaffEmail = "designer1@nbd.ca"
                        },
                        new Staff
                        {
                            StaffTitle = "Designer",
                            StaffFirstName = "De",
                            StaffLastname = "Signer02",
                            StaffPhone = "90258236941",
                            StaffEmail = "designer2@nbd.ca"
                        });

                    context.SaveChanges();
                }

                //Labour
                if (!context.Labours.Any())
                {
                    context.Labours.AddRange(
                        new Labour
                        {
                            LabourPriceHour = 30.00,
                            LabourCostHour = 18.00,
                            LabourType = "Production Worker"
                        },
                        new Labour
                        {
                            LabourPriceHour = 65.00,
                            LabourCostHour = 40.00,
                            LabourType = "Designer"
                        },
                        new Labour
                        {
                            LabourPriceHour = 65.00,
                            LabourCostHour = 45.00,
                            LabourType = "Equipment operator"
                        },
                        new Labour
                        {
                            LabourPriceHour = 75.00,
                            LabourCostHour = 50.00,
                            LabourType = "Botanist"
                        });

                    context.SaveChanges();
                }

                //Inventory
                if (!context.Inventorys.Any())
                {
                    context.Inventorys.AddRange(
                        new Models.Inventory
                        {
                            InventoryCode = "lacco",
                            InventoryDescription = "Lacco Australasica",
                            InventoryQuantity = 10,
                            InventoryUnitType = "15 Gallon",
                            InventoryPriceList = 450.00,
                            MaterialCategoryID = 1
                        },
                        new Models.Inventory
                        {
                            InventoryCode = "arenga",
                            InventoryDescription = "arenga pinnata",
                            InventoryQuantity = 10,
                            InventoryUnitType = "15Gallon",
                            InventoryPriceList = 310.00,
                            MaterialCategoryID = 1
                        },
                        new Models.Inventory
                        {
                            InventoryCode = "cham",
                            InventoryDescription = "chamaedorea",
                            InventoryQuantity = 10,
                            InventoryUnitType = "15 Gallon",
                            InventoryPriceList = 300.00,
                            MaterialCategoryID = 1
                        },
                        new Models.Inventory
                        {
                            InventoryCode = "cera",
                            InventoryDescription = "ceratozamia molongo",
                            InventoryQuantity = 10,
                            InventoryUnitType = "14 Inch",
                            InventoryPriceList = 240.00,
                            MaterialCategoryID = 1
                        },
                        new Models.Inventory
                        {
                            InventoryCode = "areca",
                            InventoryDescription = "arecastum coco",
                            InventoryQuantity = 10,
                            InventoryUnitType = "15 Gallon",
                            InventoryPriceList = 275.00,
                            MaterialCategoryID = 1
                        },
                        new Models.Inventory
                        {
                            InventoryCode = "cary",
                            InventoryDescription = "caryota mitis",
                            InventoryQuantity = 10,
                            InventoryUnitType = "7 Gallon",
                            InventoryPriceList = 140.00,
                            MaterialCategoryID = 1
                        },
                        new Models.Inventory
                        {
                            InventoryCode = "gmti5",
                            InventoryDescription = "green ti",
                            InventoryQuantity = 10,
                            InventoryUnitType = "5 Gallon",
                            InventoryPriceList = 92.00,
                            MaterialCategoryID = 1
                        },
                        new Models.Inventory
                        {
                            InventoryCode = "gmti7",
                            InventoryDescription = "green ti",
                            InventoryQuantity = 10,
                            InventoryUnitType = "7 Gallon",
                            InventoryPriceList = 140.00,
                            MaterialCategoryID = 1
                        },
                        new Models.Inventory
                        {
                            InventoryCode = "ficus 14",
                            InventoryDescription = "ficus green gem",
                            InventoryQuantity = 10,
                            InventoryUnitType = "14 Inch",
                            InventoryPriceList = 90.00,
                            MaterialCategoryID = 1
                        },
                        new Models.Inventory
                        {
                            InventoryCode = "ficus 17",
                            InventoryDescription = "ficus green gem",
                            InventoryQuantity = 10,
                            InventoryUnitType = "17 Inch",
                            InventoryPriceList = 240.00,
                            MaterialCategoryID = 1
                        },
                        new Models.Inventory
                        {
                            InventoryCode = "margi7",
                            InventoryDescription = "marginata",
                            InventoryQuantity = 10,
                            InventoryUnitType = "2 Gallon",
                            InventoryPriceList = 45.00,
                            MaterialCategoryID = 1
                        },
                        new Models.Inventory
                        {
                            InventoryCode = "TCP50",
                            InventoryDescription = "t/c pot Hand-painted",
                            InventoryQuantity = 10,
                            InventoryUnitType = "50 Gallon",
                            InventoryPriceList = 53.95,
                            MaterialCategoryID = 2
                        },
                        new Models.Inventory
                        {
                            InventoryCode = "GP50",
                            InventoryDescription = "granite pot Oblate",
                            InventoryQuantity = 10,
                            InventoryUnitType = "50 Gallon",
                            InventoryPriceList = 110.00,
                            MaterialCategoryID = 2
                        },
                        new Models.Inventory
                        {
                            InventoryCode = "TCF03",
                            InventoryDescription = "t/c figure-swan Glazed",
                            InventoryQuantity = 10,
                            InventoryUnitType = "Blank",
                            InventoryPriceList = 25.50,
                            MaterialCategoryID = 2
                        },
                        new Models.Inventory
                        {
                            InventoryCode = "MBB30",
                            InventoryDescription = "marble bird bath Doric base",
                            InventoryQuantity = 10,
                            InventoryUnitType = "30 Inches",
                            InventoryPriceList = 128.50,
                            MaterialCategoryID = 2
                        },
                        new Models.Inventory
                        {
                            InventoryCode = "GFN48",
                            InventoryDescription = "granite foundation Fluted basin",
                            InventoryQuantity = 10,
                            InventoryUnitType = "48 Inches",
                            InventoryPriceList = 457.50,
                            MaterialCategoryID = 2
                        },
                        new Models.Inventory
                        {
                            InventoryCode = "CBRK5",
                            InventoryDescription = "Decorative cedar bark",
                            InventoryQuantity = 10,
                            InventoryUnitType = "Bag (5 cu ft)",
                            InventoryPriceList = 7.50,
                            MaterialCategoryID = 3
                        },
                        new Models.Inventory
                        {
                            InventoryCode = "CRGRN",
                            InventoryDescription = "Crushed granite",
                            InventoryQuantity = 10,
                            InventoryUnitType = "Yard",
                            InventoryPriceList = 7.50,
                            MaterialCategoryID = 3
                        },
                        new Models.Inventory
                        {
                            InventoryCode = "PGRV",
                            InventoryDescription = "Pea gravel",
                            InventoryQuantity = 10,
                            InventoryUnitType = "Yard",
                            InventoryPriceList = 8.00,
                            MaterialCategoryID = 3
                        },
                        new Models.Inventory
                        {
                            InventoryCode = "GRV1",
                            InventoryDescription = "1\" gravel",
                            InventoryQuantity = 10,
                            InventoryUnitType = "Yard",
                            InventoryPriceList = 5.90,
                            MaterialCategoryID = 3
                        },
                        new Models.Inventory
                        {
                            InventoryCode = "TSOIL",
                            InventoryDescription = "Topsoill",
                            InventoryQuantity = 10,
                            InventoryUnitType = "Yard",
                            InventoryPriceList = 12.50,
                            MaterialCategoryID = 3
                        },
                        new Models.Inventory
                        {
                            InventoryCode = "PBLKG",
                            InventoryDescription = "Patio block-grey",
                            InventoryQuantity = 10,
                            InventoryUnitType = "Each",
                            InventoryPriceList = 0.56,
                            MaterialCategoryID = 3
                        },
                        new Models.Inventory
                        {
                            InventoryCode = "PBLKR",
                            InventoryDescription = "Patio block-red",
                            InventoryQuantity = 10,
                            InventoryUnitType = "Each",
                            InventoryPriceList = 0.56,
                            MaterialCategoryID = 3
                        });
                    context.SaveChanges();
                }


                // Seed Projects
                if (!context.Projects.Any())
                {
                    context.Projects.AddRange(
                    new Project
                    {
                        ProjectName = "Botanical Wonderland",
                        ProjectDescription = "Lurie Garden is Millennium Park’s ‘secret garden’. This naturalistic garden is a place of rest and renewal for humans and wildlife alike. ",
                        ProjectStartDate = DateOnly.Parse("2024-07-02"),
                        ProjectEndDate = DateOnly.Parse("2024-07-28"),
                        ClientId = 1,
                        LocationId = 2,
                        ProjectStatus = "Design Stage"

                    },
                    new Models.Project
                    {
                        ProjectName = "Kenrokuen Garden",
                        ProjectDescription = "he name Kenrokuen literally means \"Garden of the six sublimities\", referring to spaciousness, seclusion, artificiality, antiquity, abundant water and broad views, which according to Chinese landscape theory are the six essential attributes that make up a perfect garden. ",
                        ProjectStartDate = DateOnly.Parse("2024-03-11"),
                        ProjectEndDate = DateOnly.Parse("2024-04-01"),
                        ClientId = 2,
                        LocationId = 4,
                        ProjectStatusList = ProjectStatusList.Approved
                    },
                    new Models.Project
                    {
                        ProjectName = "High Line Park",
                        ProjectDescription = "he High Line is almost entirely supported by people like you. As a nonprofit, we need your support to keep this public space free—and extraordinary—for everyone.",
                        ProjectStartDate = DateOnly.Parse("2024-04-13"),
                        ProjectEndDate = DateOnly.Parse("2024-04-30"),
                        ClientId = 1,
                        LocationId = 3,
                        ProjectStatusList = ProjectStatusList.OnDesign
                    },
                    new Models.Project
                    {
                        ProjectName = "Garden of Australian Dreams",
                        ProjectDescription = "laza Euskadi connects the nineteenth century section of the city called “El Ensanche” to the new section of Bilbao, Deusto university campus, the Guggenheim Museum, and the Nervión River. The Plaza is a pivot, unifying diverse elements of the city.",
                        ProjectStartDate = DateOnly.Parse("2024-06-05"),
                        ProjectEndDate = DateOnly.Parse("2024-07-01"),
                        ClientId = 5,
                        LocationId = 1,
                        ProjectStatus = "Design Stage"
                    },
                    new Models.Project
                    {
                        ProjectName = "TerraTints Turf",
                        ProjectDescription = "laza Euskadi connects the nineteenth century section of the city called “El Ensanche” to the new section of Bilbao, Deusto university campus, the Guggenheim Museum, and the Nervión River. The Plaza is a pivot, unifying diverse elements of the city.",
                        ProjectStartDate = DateOnly.Parse("2024-05-01"),
                        ProjectEndDate = DateOnly.Parse("2024-05-18"),
                        ClientId = 5,
                        LocationId = 1,
                        ProjectStatus = "Design Stage"
                    },
                    new Models.Project
                    {
                        ProjectName = "Elite Blue Oak",
                        ProjectDescription = "laza Euskadi connects the nineteenth century section of the city called “El Ensanche” to the new section of Bilbao, Deusto university campus, the Guggenheim Museum, and the Nervión River. The Plaza is a pivot, unifying diverse elements of the city.",
                        ProjectStartDate = DateOnly.Parse("2024-02-15"),
                        ProjectEndDate = DateOnly.Parse("2024-03-01"),
                        ClientId = 4,
                        LocationId = 1,
                        ProjectStatus = "Design Stage"
                    },
                    new Models.Project
                    {
                        ProjectName = "GreenGrace Gardens",
                        ProjectDescription = "laza Euskadi connects the nineteenth century section of the city called “El Ensanche” to the new section of Bilbao, Deusto university campus, the Guggenheim Museum, and the Nervión River. The Plaza is a pivot, unifying diverse elements of the city.",
                        ProjectStartDate = DateOnly.Parse("2024-10-09"),
                        ProjectEndDate = DateOnly.Parse("2024-11-02"),
                        ClientId = 3,
                        LocationId = 1,
                        ProjectStatus = "Design Stage"
                    },
                    new Models.Project
                    {
                        ProjectName = "Big Valley",
                        ProjectDescription = "laza Euskadi connects the nineteenth century section of the city called “El Ensanche” to the new section of Bilbao, Deusto university campus, the Guggenheim Museum, and the Nervión River. The Plaza is a pivot, unifying diverse elements of the city.",
                        ProjectStartDate = DateOnly.Parse("2024-12-08"),
                        ProjectEndDate = DateOnly.Parse("2025-01-17"),
                        ClientId = 3,
                        LocationId = 3,
                        ProjectStatus = "Design Stage"
                    },
                    new Models.Project
                    {
                        ProjectName = "Aqua Ambiance",
                        ProjectDescription = "laza Euskadi connects the nineteenth century section of the city called “El Ensanche” to the new section of Bilbao, Deusto university campus, the Guggenheim Museum, and the Nervión River. The Plaza is a pivot, unifying diverse elements of the city.",
                        ProjectStartDate = DateOnly.Parse("2024-12-09"),
                        ProjectEndDate = DateOnly.Parse("2024-12-28"),
                        ClientId = 2,
                        LocationId = 1,
                        ProjectStatus = "Design Stage"
                    },
                    new Models.Project
                    {
                        ProjectName = "Prospect Park",
                        ProjectDescription = "Prospect Park Alliance is the non-profit organization that sustains, restores and advances Brooklyn's Backyard, in partnership with the City of New York.",
                        ProjectStartDate = DateOnly.Parse("2024-08-03"),
                        ProjectEndDate = DateOnly.Parse("2024-09-04"),
                        ClientId = 4,
                        LocationId = 2,
                        ProjectStatus = "Design Stage"
                    }); ;
                    context.SaveChanges();
                }


                //Bids
                if (!context.Bids.Any())
                {
                    context.Bids.AddRange(
                        new Models.Bid
                        {
                            ProjectID = 2,
                            BidAmount = 1000,
                            BidDate = DateOnly.Parse("2024-09-01"),
                            BidCost = 600,
                            BidApprove = false
                        },
                        new Models.Bid
                        {
                            ProjectID = 3,
                            BidAmount = 1000,
                            BidDate = DateOnly.Parse("2024-09-02"),
                            BidCost = 500,
                            BidApprove = false
                        },
                        new Models.Bid
                        {
                            ProjectID = 5,
                            BidAmount = 800,
                            BidDate = DateOnly.Parse("2024-09-03"),
                            BidCost = 200,
                            BidApprove = true
                        },
                        new Models.Bid
                        {
                            ProjectID = 6,
                            BidAmount = 10000,
                            BidDate = DateOnly.Parse("2024-09-04"),
                            BidCost = 7000,
                            BidApprove = false
                        },
                        new Models.Bid
                        {
                            ProjectID = 2,
                            BidAmount = 21000,
                            BidDate = DateOnly.Parse("2024-09-05"),
                            BidCost = 6900,
                            BidApprove = false
                        },
                        new Models.Bid
                        {
                            ProjectID = 3,
                            BidAmount = 11000,
                            BidDate = DateOnly.Parse("2024-09-06"),
                            BidCost = 5700,
                            BidApprove = false
                        },
                        new Models.Bid
                        {
                            ProjectID = 5,
                            BidAmount = 3800,
                            BidDate = DateOnly.Parse("2024-09-07"),
                            BidCost = 2200,
                            BidApprove = true
                        },
                        new Models.Bid
                        {
                            ProjectID = 6,
                            BidAmount = 100000,
                            BidDate = DateOnly.Parse("2024-09-08"),
                            BidCost = 71000,
                            BidApprove = false
                        },
                        new Models.Bid
                        {
                            ProjectID = 1,
                            BidAmount = 81500,
                            BidDate = DateOnly.Parse("2024-09-09"),
                            BidCost = 3800,
                            BidApprove = true
                        });
                    context.SaveChanges();
                }


                //Staff Details
                if (!context.StaffDetails.Any())
                {
                    context.StaffDetails.AddRange(
                        new StaffDetail
                        {
                            BidID = 3,
                            StaffID = 1
                        },
                        new StaffDetail
                        {
                            BidID = 3,
                            StaffID = 2
                        },
                        new StaffDetail
                        {
                            BidID = 2,
                            StaffID = 1
                        },
                        new StaffDetail
                        {
                            BidID = 2,
                            StaffID = 2
                        },
                        new StaffDetail
                        {
                            BidID = 1,
                            StaffID = 3
                        });
                    context.SaveChanges();
                }

                //Labours Details
                if (!context.LabourDetails.Any())
                {
                    context.LabourDetails.AddRange(
                        new LabourDetail
                        {
                            BidID = 2,
                            LabourID = 1,
                            Quantity = 10
                        },
                        new LabourDetail
                        {
                            BidID = 2,
                            LabourID = 3,
                            Quantity = 10
                        },
                        new LabourDetail
                        {
                            BidID = 2,
                            LabourID = 4,
                            Quantity = 10
                        },
                        new LabourDetail
                        {
                            BidID = 2,
                            LabourID = 2,
                            Quantity = 10
                        },
                        new LabourDetail
                        {
                            BidID = 3,
                            LabourID = 1,
                            Quantity = 20
                        },
                        new LabourDetail
                        {
                            BidID = 3,
                            LabourID = 3,
                            Quantity = 20
                        },
                        new LabourDetail
                        {
                            BidID = 3,
                            LabourID = 4,
                            Quantity = 20
                        },
                        new LabourDetail
                        {
                            BidID = 3,
                            LabourID = 2,
                            Quantity = 20
                        },
                         new LabourDetail
                         {
                             BidID = 1,
                             LabourID = 3,
                             Quantity = 15
                         },
                        new LabourDetail
                        {
                            BidID = 1,
                            LabourID = 1,
                            Quantity = 15
                        },
                        new LabourDetail
                        {
                            BidID = 1,
                            LabourID = 2,
                            Quantity = 15
                        },
                        new LabourDetail
                        {
                            BidID = 5,
                            LabourID = 4,
                            Quantity = 18
                        },
                         new LabourDetail
                         {
                             BidID = 5,
                             LabourID = 3,
                             Quantity = 18
                         },
                        new LabourDetail
                        {
                            BidID = 5,
                            LabourID = 1,
                            Quantity = 18
                        },
                        new LabourDetail
                        {
                            BidID = 4,
                            LabourID = 2,
                            Quantity = 30
                        },
                        new LabourDetail
                        {
                            BidID = 4,
                            LabourID = 3,
                            Quantity = 30
                        },
                        new LabourDetail
                        {
                            BidID = 4,
                            LabourID = 1,
                            Quantity = 30
                        });
                    context.SaveChanges();
                }

                //Labours Material
                if (!context.MaterialDetails.Any())
                {
                    context.MaterialDetails.AddRange(
                        new MaterialDetail
                        {
                            BidID = 3,
                            InventoryID = 1,
                            Quantity = 2
                        },
                        new MaterialDetail
                        {
                            BidID = 3,
                            InventoryID = 17,
                            Quantity = 6
                        },
                        new MaterialDetail
                        {
                            BidID = 3,
                            InventoryID = 3,
                            Quantity = 2
                        },
                        new MaterialDetail
                        {
                            BidID = 2,
                            InventoryID = 1,
                            Quantity = 5
                        },
                        new MaterialDetail
                        {
                            BidID = 2,
                            InventoryID = 13,
                            Quantity = 4
                        },
                        new MaterialDetail
                        {
                            BidID = 4,
                            InventoryID = 14,
                            Quantity = 2
                        },
                        new MaterialDetail
                        {
                            BidID = 4,
                            InventoryID = 1,
                            Quantity = 3
                        },
                        new MaterialDetail
                        {
                            BidID = 4,
                            InventoryID = 3,
                            Quantity = 2
                        },
                        new MaterialDetail
                        {
                            BidID = 4,
                            InventoryID = 18,
                            Quantity = 1
                        });

                    context.SaveChanges();
                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetBaseException().Message);
            }
        }
    }
}