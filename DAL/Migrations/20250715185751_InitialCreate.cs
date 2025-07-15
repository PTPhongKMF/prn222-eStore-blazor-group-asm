using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    MemberId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    City = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.MemberId);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatus",
                columns: table => new
                {
                    OrderStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatus", x => x.OrderStatusId);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethod",
                columns: table => new
                {
                    PaymentMethodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethod", x => x.PaymentMethodId);
                });

            migrationBuilder.CreateTable(
                name: "PaymentStatus",
                columns: table => new
                {
                    PaymentStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentStatus", x => x.PaymentStatusId);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Weight = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "money", nullable: false),
                    UnitsInStock = table.Column<int>(type: "int", nullable: false),
                    ActiveStatus = table.Column<bool>(type: "bit", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Product_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequiredDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ShippedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Freight = table.Column<decimal>(type: "money", nullable: false),
                    OrderStatusId = table.Column<int>(type: "int", nullable: false),
                    PaymentStatusId = table.Column<int>(type: "int", nullable: false),
                    PaymentMethodId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Order_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_OrderStatus_OrderStatusId",
                        column: x => x.OrderStatusId,
                        principalTable: "OrderStatus",
                        principalColumn: "OrderStatusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_PaymentMethod_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethod",
                        principalColumn: "PaymentMethodId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Order_PaymentStatus_PaymentStatusId",
                        column: x => x.PaymentStatusId,
                        principalTable: "PaymentStatus",
                        principalColumn: "PaymentStatusId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => new { x.MemberId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_Cart_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cart_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "money", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_OrderDetail_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "CategoryId", "CategoryName", "Description", "ImageUrl" },
                values: new object[,]
                {
                    { 1, "Điện thoại", null, null },
                    { 2, "Laptop", null, null },
                    { 3, "Máy tính bảng", null, null },
                    { 4, "Đồng hồ thông minh", null, null },
                    { 5, "Tai nghe", null, null },
                    { 6, "Sạc dự phòng", null, null },
                    { 7, "Ốp lưng", null, null },
                    { 8, "Chuột", null, null },
                    { 9, "Bàn phím", null, null },
                    { 10, "Màn hình", null, null }
                });

            migrationBuilder.InsertData(
                table: "Member",
                columns: new[] { "MemberId", "City", "CompanyName", "Country", "Email", "Password" },
                values: new object[,]
                {
                    { 1, "Hà Nội", "Công ty A", "Việt Nam", "a@a.com", "123456" },
                    { 2, "Hồ Chí Minh", "Công ty B", "Việt Nam", "b@b.com", "123456" },
                    { 3, "Đà Nẵng", "Công ty C", "Việt Nam", "c@c.com", "123456" }
                });

            migrationBuilder.InsertData(
                table: "OrderStatus",
                columns: new[] { "OrderStatusId", "StatusName" },
                values: new object[,]
                {
                    { 1, "Chờ xác nhận" },
                    { 2, "Đang giao hàng" },
                    { 3, "Đã giao hàng" }
                });

            migrationBuilder.InsertData(
                table: "PaymentMethod",
                columns: new[] { "PaymentMethodId", "Name" },
                values: new object[,]
                {
                    { 1, "Tiền mặt khi nhận hàng" },
                    { 2, "Chuyển khoản ngân hàng" },
                    { 3, "Ví điện tử MoMo" },
                    { 4, "Ví ZaloPay" },
                    { 5, "Thẻ tín dụng/ghi nợ" }
                });

            migrationBuilder.InsertData(
                table: "PaymentStatus",
                columns: new[] { "PaymentStatusId", "StatusName" },
                values: new object[,]
                {
                    { 1, "Chưa thanh toán" },
                    { 2, "Đã thanh toán" }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "ActiveStatus", "CategoryId", "ImageUrl", "ProductName", "UnitPrice", "UnitsInStock", "Weight" },
                values: new object[,]
                {
                    { 1, true, 1, null, "iPhone 15 Pro Max 256GB", 34990000m, 50, "220g" },
                    { 2, true, 1, null, "iPhone 15 Pro 256GB", 30990000m, 50, "200g" },
                    { 3, true, 1, null, "Samsung Galaxy S24 Ultra 256GB", 33990000m, 45, "230g" },
                    { 4, true, 1, null, "Samsung Galaxy S24+ 256GB", 29990000m, 45, "210g" },
                    { 5, true, 1, null, "OPPO Find X7 Ultra 256GB", 27990000m, 40, "220g" },
                    { 6, true, 1, null, "Xiaomi 14 Pro 256GB", 24990000m, 40, "210g" },
                    { 7, true, 1, null, "iPhone 14 Pro Max 256GB", 29990000m, 35, "220g" },
                    { 8, true, 1, null, "Samsung Galaxy Z Fold5 512GB", 40990000m, 30, "250g" },
                    { 9, true, 1, null, "OPPO Find N3 Flip 256GB", 22990000m, 35, "200g" },
                    { 10, true, 1, null, "Vivo V29e 128GB", 8990000m, 60, "190g" },
                    { 11, true, 1, null, "Xiaomi Redmi Note 13 Pro 256GB", 8490000m, 60, "190g" },
                    { 12, true, 1, null, "OPPO Reno11 F 256GB", 8290000m, 55, "180g" },
                    { 13, true, 1, null, "Samsung Galaxy A55 5G 256GB", 11990000m, 50, "200g" },
                    { 14, true, 1, null, "iPhone 13 128GB", 16990000m, 40, "190g" },
                    { 15, true, 1, null, "Vivo V29 256GB", 12990000m, 45, "190g" },
                    { 16, true, 1, null, "OPPO A98 5G 256GB", 8490000m, 55, "190g" },
                    { 17, true, 1, null, "Samsung Galaxy S23 FE 256GB", 14990000m, 40, "210g" },
                    { 18, true, 1, null, "Xiaomi 13T Pro 256GB", 15990000m, 35, "200g" },
                    { 19, true, 1, null, "OPPO Find X6 Pro 256GB", 25990000m, 30, "220g" },
                    { 20, true, 1, null, "iPhone 15 128GB", 22990000m, 45, "190g" },
                    { 21, true, 2, null, "MacBook Pro 14 M3 Pro", 49990000m, 30, "1.55kg" },
                    { 22, true, 2, null, "MacBook Air 15 M2", 32990000m, 35, "1.51kg" },
                    { 23, true, 2, null, "Dell XPS 15 9530", 59990000m, 25, "1.75kg" },
                    { 24, true, 2, null, "ASUS ROG Zephyrus G14", 39990000m, 30, "1.65kg" },
                    { 25, true, 2, null, "Lenovo Legion Pro 7", 49990000m, 25, "2.5kg" },
                    { 26, true, 2, null, "HP Envy 16", 35990000m, 30, "2.2kg" },
                    { 27, true, 2, null, "Acer Predator Helios 16", 45990000m, 25, "2.6kg" },
                    { 28, true, 2, null, "MSI Stealth 16", 42990000m, 25, "2.1kg" },
                    { 29, true, 2, null, "Dell Inspiron 16", 19990000m, 40, "1.85kg" },
                    { 30, true, 2, null, "ASUS Vivobook 15", 16990000m, 45, "1.7kg" },
                    { 31, true, 2, null, "HP Pavilion 15", 17990000m, 45, "1.75kg" },
                    { 32, true, 2, null, "Lenovo IdeaPad Slim 5", 18990000m, 40, "1.65kg" },
                    { 33, true, 2, null, "Acer Aspire 7", 19990000m, 35, "2.1kg" },
                    { 34, true, 2, null, "MSI Modern 15", 16990000m, 40, "1.7kg" },
                    { 35, true, 2, null, "MacBook Air 13 M2", 27990000m, 35, "1.24kg" },
                    { 36, true, 2, null, "Dell Vostro 15", 15990000m, 45, "1.8kg" },
                    { 37, true, 2, null, "ASUS TUF Gaming A15", 22990000m, 35, "2.2kg" },
                    { 38, true, 2, null, "Lenovo ThinkPad E15", 20990000m, 35, "1.7kg" },
                    { 39, true, 2, null, "HP ProBook 450 G10", 21990000m, 30, "1.8kg" },
                    { 40, true, 2, null, "MSI Katana 15", 23990000m, 30, "2.25kg" },
                    { 41, true, 3, null, "iPad Pro 12.9 M2 WiFi 256GB", 31990000m, 35, "682g" },
                    { 42, true, 3, null, "iPad Pro 11 M2 WiFi 128GB", 20990000m, 40, "466g" },
                    { 43, true, 3, null, "Samsung Galaxy Tab S9 Ultra", 29990000m, 35, "732g" },
                    { 44, true, 3, null, "iPad Air 5 WiFi 64GB", 15990000m, 45, "461g" },
                    { 45, true, 3, null, "Samsung Galaxy Tab S9+", 24990000m, 40, "581g" },
                    { 46, true, 3, null, "iPad 10 WiFi 64GB", 11990000m, 50, "477g" },
                    { 47, true, 3, null, "Samsung Galaxy Tab S9", 20990000m, 45, "498g" },
                    { 48, true, 3, null, "iPad mini 6 WiFi 64GB", 12990000m, 45, "293g" },
                    { 49, true, 3, null, "Xiaomi Pad 6", 8990000m, 55, "490g" },
                    { 50, true, 3, null, "OPPO Pad 2", 13990000m, 40, "552g" },
                    { 51, true, 3, null, "Samsung Galaxy Tab A9+", 6990000m, 60, "480g" },
                    { 52, true, 3, null, "Lenovo Tab P12 Pro", 14990000m, 35, "565g" },
                    { 53, true, 3, null, "Samsung Galaxy Tab A8", 5990000m, 60, "508g" },
                    { 54, true, 3, null, "Xiaomi Pad 6 Pro", 11990000m, 45, "485g" },
                    { 55, true, 3, null, "OPPO Pad Air", 6990000m, 55, "440g" },
                    { 56, true, 3, null, "iPad 9 WiFi 64GB", 7990000m, 50, "487g" },
                    { 57, true, 3, null, "Lenovo Tab M10", 4990000m, 65, "460g" },
                    { 58, true, 3, null, "Samsung Galaxy Tab A7 Lite", 3990000m, 70, "366g" },
                    { 59, true, 3, null, "Xiaomi Redmi Pad SE", 4490000m, 65, "478g" },
                    { 60, true, 3, null, "Nokia T21", 4990000m, 60, "465g" },
                    { 61, true, 4, null, "Apple Watch Ultra 2 GPS 49mm", 21990000m, 40, "61.4g" },
                    { 62, true, 4, null, "Apple Watch Series 9 GPS 45mm", 11990000m, 50, "38.8g" },
                    { 63, true, 4, null, "Samsung Galaxy Watch6 Classic 47mm", 9990000m, 55, "59g" },
                    { 64, true, 4, null, "Garmin Fenix 7X Sapphire Solar", 23990000m, 35, "89g" },
                    { 65, true, 4, null, "Apple Watch SE 2 GPS 44mm", 6990000m, 60, "33g" },
                    { 66, true, 4, null, "Samsung Galaxy Watch6 44mm", 7490000m, 55, "33.3g" },
                    { 67, true, 4, null, "Garmin Venu 3", 12990000m, 45, "47g" },
                    { 68, true, 4, null, "Huawei Watch GT4 46mm", 6990000m, 50, "48g" },
                    { 69, true, 4, null, "Amazfit GTR 4", 4990000m, 60, "34g" },
                    { 70, true, 4, null, "Xiaomi Watch S3", 3990000m, 65, "44g" },
                    { 71, true, 4, null, "Apple Watch Series 8 GPS 45mm", 9990000m, 45, "38.8g" },
                    { 72, true, 4, null, "Samsung Galaxy Watch5 Pro 45mm", 8990000m, 50, "46.5g" },
                    { 73, true, 4, null, "Garmin Forerunner 965", 16990000m, 40, "53g" },
                    { 74, true, 4, null, "Huawei Watch GT3 Pro", 7990000m, 45, "54g" },
                    { 75, true, 4, null, "Amazfit T-Rex 2", 4490000m, 55, "66.5g" },
                    { 76, true, 4, null, "Xiaomi Watch 2 Pro", 5990000m, 50, "55g" },
                    { 77, true, 4, null, "Garmin Instinct 2X Solar", 14990000m, 40, "67g" },
                    { 78, true, 4, null, "Samsung Galaxy Watch4 Classic 46mm", 6990000m, 45, "52g" },
                    { 79, true, 4, null, "Huawei Band 8", 990000m, 80, "14g" },
                    { 80, true, 4, null, "Xiaomi Smart Band 8 Pro", 1490000m, 75, "22.5g" },
                    { 81, true, 5, null, "AirPods Pro 2", 6490000m, 60, "50.8g" },
                    { 82, true, 5, null, "Sony WH-1000XM5", 8490000m, 50, "250g" },
                    { 83, true, 5, null, "Samsung Galaxy Buds2 Pro", 4990000m, 65, "51.5g" },
                    { 84, true, 5, null, "Apple AirPods Max", 12990000m, 40, "384.8g" },
                    { 85, true, 5, null, "Bose QuietComfort Ultra", 9990000m, 45, "250g" },
                    { 86, true, 5, null, "JBL Tour One M2", 7490000m, 50, "268g" },
                    { 87, true, 5, null, "Sony WF-1000XM5", 6490000m, 55, "41.7g" },
                    { 88, true, 5, null, "AirPods 3", 4490000m, 65, "37.9g" },
                    { 89, true, 5, null, "Jabra Elite 10", 5990000m, 50, "57g" },
                    { 90, true, 5, null, "Samsung Galaxy Buds FE", 2490000m, 70, "40.2g" },
                    { 91, true, 5, null, "Soundcore Space One", 2990000m, 60, "280g" },
                    { 92, true, 5, null, "Huawei FreeBuds Pro 3", 4990000m, 55, "48g" },
                    { 93, true, 5, null, "Nothing Ear (2)", 3490000m, 60, "51.9g" },
                    { 94, true, 5, null, "Sony WH-CH720N", 2490000m, 65, "192g" },
                    { 95, true, 5, null, "JBL Tune 770NC", 2990000m, 60, "220g" },
                    { 96, true, 5, null, "Xiaomi Redmi Buds 4 Pro", 1490000m, 75, "47g" },
                    { 97, true, 5, null, "OPPO Enco Air3 Pro", 1990000m, 70, "46g" },
                    { 98, true, 5, null, "Soundpeats Air3 Deluxe HS", 990000m, 80, "43g" },
                    { 99, true, 5, null, "Edifier WH950NB", 1990000m, 65, "275g" },
                    { 100, true, 5, null, "Havit TW945", 490000m, 85, "45g" },
                    { 101, true, 6, null, "Sạc dự phòng Apple MagSafe 5000mAh", 2490000m, 70, "150g" },
                    { 102, true, 6, null, "Samsung Wireless Battery Pack 10000mAh", 1290000m, 75, "240g" },
                    { 103, true, 6, null, "Anker PowerCore 20000mAh PD", 1490000m, 80, "345g" },
                    { 104, true, 6, null, "Xiaomi Redmi 20000mAh Fast Charge", 790000m, 85, "350g" },
                    { 105, true, 6, null, "Belkin BoostCharge 10K Power Bank", 990000m, 75, "220g" },
                    { 106, true, 6, null, "OPPO VOOC 10000mAh", 890000m, 80, "230g" },
                    { 107, true, 6, null, "Energizer UE20000 20000mAh", 690000m, 90, "360g" },
                    { 108, true, 6, null, "Anker PowerCore Slim 10000mAh PD", 890000m, 85, "210g" },
                    { 109, true, 6, null, "Baseus Adaman 20000mAh", 990000m, 80, "370g" },
                    { 110, true, 6, null, "Mophie Powerstation XXL 20000mAh", 1990000m, 70, "380g" },
                    { 111, true, 6, null, "Xiaomi Mi Power Bank 3 30000mAh", 890000m, 75, "440g" },
                    { 112, true, 6, null, "AUKEY PB-Y36 Sprint 26800mAh", 1190000m, 70, "420g" },
                    { 113, true, 6, null, "RAVPower 15000mAh PD", 790000m, 85, "280g" },
                    { 114, true, 6, null, "ROMOSS Sense 8+ 30000mAh", 690000m, 90, "450g" },
                    { 115, true, 6, null, "Anker PowerCore III 19200mAh", 1690000m, 75, "365g" },
                    { 116, true, 6, null, "Samsung EB-P3300 10000mAh", 890000m, 80, "235g" },
                    { 117, true, 6, null, "Baseus Amblight 20000mAh", 890000m, 85, "375g" },
                    { 118, true, 6, null, "UMETRAVEL 10000mAh Wireless", 590000m, 95, "245g" },
                    { 119, true, 6, null, "Energizer QE10007PQ 10000mAh", 490000m, 100, "225g" },
                    { 120, true, 6, null, "Xmobile PowerLite P181P 18000mAh", 390000m, 100, "340g" },
                    { 121, true, 7, null, "Ốp lưng iPhone 15 Pro Max Apple Silicone Case", 1290000m, 100, "45g" },
                    { 122, true, 7, null, "Ốp lưng Samsung S24 Ultra Led View Cover", 1490000m, 90, "65g" },
                    { 123, true, 7, null, "Ốp lưng iPhone 15 Pro Max Spigen Ultra Hybrid", 590000m, 120, "40g" },
                    { 124, true, 7, null, "Ốp lưng Z Fold5 Samsung Slim Standing Cover", 990000m, 80, "85g" },
                    { 125, true, 7, null, "Ốp lưng iPhone 15 UAG Monarch", 890000m, 95, "50g" },
                    { 126, true, 7, null, "Ốp lưng S24 Ultra Spigen Liquid Air", 490000m, 110, "42g" },
                    { 127, true, 7, null, "Ốp lưng iPhone 15 Pro Max UNIQ Lifepro Xtreme", 790000m, 100, "48g" },
                    { 128, true, 7, null, "Ốp lưng Z Flip5 Samsung Clear Gadget Case", 890000m, 85, "55g" },
                    { 129, true, 7, null, "Ốp lưng iPhone 15 Pro Max Mous Limitless 5.0", 1390000m, 75, "52g" },
                    { 130, true, 7, null, "Ốp lưng S24+ Ringke Fusion", 290000m, 130, "38g" },
                    { 131, true, 7, null, "Ốp lưng iPhone 15 Pro Max NILLKIN Textured", 190000m, 140, "35g" },
                    { 132, true, 7, null, "Ốp lưng S24 Ultra NILLKIN CamShield", 290000m, 120, "45g" },
                    { 133, true, 7, null, "Ốp lưng iPhone 15 Pro Max ESR Classic", 390000m, 125, "32g" },
                    { 134, true, 7, null, "Ốp lưng Xiaomi 14 Pro ZADEZ Hybrid", 190000m, 135, "36g" },
                    { 135, true, 7, null, "Ốp lưng OPPO Find X7 Ultra Memumi Slim", 240000m, 130, "34g" },
                    { 136, true, 7, null, "Ốp lưng iPhone 15 Pro Max Benks Magic OA", 490000m, 110, "42g" },
                    { 137, true, 7, null, "Ốp lưng S24 Ultra X-Level Guardian", 190000m, 140, "38g" },
                    { 138, true, 7, null, "Ốp lưng iPhone 15 Pro Max Jinya Defense", 390000m, 115, "44g" },
                    { 139, true, 7, null, "Ốp lưng Vivo V29e Nắp lưng nhựa cứng", 90000m, 150, "30g" },
                    { 140, true, 7, null, "Ốp lưng OPPO Reno11 F Ốp dẻo trong", 50000m, 200, "25g" },
                    { 141, true, 8, null, "Chuột Logitech G Pro X Superlight", 2990000m, 60, "63g" },
                    { 142, true, 8, null, "Chuột Razer DeathAdder V3 Pro", 3490000m, 55, "64g" },
                    { 143, true, 8, null, "Chuột SteelSeries Aerox 3 Wireless", 1990000m, 70, "68g" },
                    { 144, true, 8, null, "Chuột Logitech MX Master 3S", 2790000m, 65, "141g" },
                    { 145, true, 8, null, "Chuột Razer Basilisk V3 Pro", 3990000m, 50, "112g" },
                    { 146, true, 8, null, "Chuột Corsair Dark Core RGB Pro SE", 2490000m, 60, "133g" },
                    { 147, true, 8, null, "Chuột Pulsar X2 Wireless", 2990000m, 55, "55g" },
                    { 148, true, 8, null, "Chuột Zowie EC2-C", 1890000m, 75, "88g" },
                    { 149, true, 8, null, "Chuột Logitech G502 X Plus", 3290000m, 60, "106g" },
                    { 150, true, 8, null, "Chuột HyperX Pulsefire Haste", 1490000m, 80, "59g" },
                    { 151, true, 8, null, "Chuột Razer Viper V2 Pro", 3490000m, 55, "58g" },
                    { 152, true, 8, null, "Chuột Logitech G304 Lightspeed", 790000m, 100, "99g" },
                    { 153, true, 8, null, "Chuột SteelSeries Prime Wireless", 2490000m, 65, "80g" },
                    { 154, true, 8, null, "Chuột Glorious Model O Wireless", 1990000m, 70, "69g" },
                    { 155, true, 8, null, "Chuột Endgame Gear XM2w", 2790000m, 60, "63g" },
                    { 156, true, 8, null, "Chuột Logitech M590", 690000m, 95, "101g" },
                    { 157, true, 8, null, "Chuột Razer Orochi V2", 1490000m, 80, "60g" },
                    { 158, true, 8, null, "Chuột Corsair Sabre RGB Pro", 1290000m, 85, "74g" },
                    { 159, true, 8, null, "Chuột Dare-U EM901X", 490000m, 110, "69g" },
                    { 160, true, 8, null, "Chuột Fuhlen G90 Pro", 390000m, 120, "75g" },
                    { 161, true, 9, null, "Bàn phím Leopold FC900R PD", 3490000m, 45, "1.2kg" },
                    { 162, true, 9, null, "Bàn phím Keychron Q1 QMK", 4290000m, 40, "1.6kg" },
                    { 163, true, 9, null, "Bàn phím Ducky One 3 RGB", 2990000m, 50, "1.1kg" },
                    { 164, true, 9, null, "Bàn phím GMMK Pro", 4990000m, 35, "1.8kg" },
                    { 165, true, 9, null, "Bàn phím Akko MOD 007", 3990000m, 40, "2.2kg" },
                    { 166, true, 9, null, "Bàn phím Varmilo VA87M", 2890000m, 55, "1.0kg" },
                    { 167, true, 9, null, "Bàn phím Leopold FC750R PD", 2990000m, 50, "0.9kg" },
                    { 168, true, 9, null, "Bàn phím Keychron K8 Pro", 2490000m, 60, "0.8kg" },
                    { 169, true, 9, null, "Bàn phím Akko 3098B", 1990000m, 65, "1.1kg" },
                    { 170, true, 9, null, "Bàn phím Ducky One 2 Mini", 2290000m, 60, "0.6kg" },
                    { 171, true, 9, null, "Bàn phím Royal Kludge RK84", 1490000m, 75, "0.7kg" },
                    { 172, true, 9, null, "Bàn phím FL-Esports CMK87", 1790000m, 70, "0.85kg" },
                    { 173, true, 9, null, "Bàn phím Akko 3068B", 1690000m, 70, "0.65kg" },
                    { 174, true, 9, null, "Bàn phím E-Dra EK387 Pro", 890000m, 90, "0.7kg" },
                    { 175, true, 9, null, "Bàn phím GMMK 2", 2490000m, 55, "0.8kg" },
                    { 176, true, 9, null, "Bàn phím Keychron K2 V2", 1990000m, 65, "0.66kg" },
                    { 177, true, 9, null, "Bàn phím Dare-U DK880", 790000m, 95, "0.75kg" },
                    { 178, true, 9, null, "Bàn phím Fuhlen Eraser", 990000m, 85, "0.9kg" },
                    { 179, true, 9, null, "Bàn phím Rapoo V500Pro", 690000m, 100, "0.95kg" },
                    { 180, true, 9, null, "Bàn phím Newmen GM819", 590000m, 110, "0.85kg" },
                    { 181, true, 10, null, "Màn hình LG 27GP950 UltraGear 4K", 19990000m, 30, "6.2kg" },
                    { 182, true, 10, null, "Màn hình Samsung Odyssey G7 32inch", 16990000m, 35, "7.8kg" },
                    { 183, true, 10, null, "Màn hình Dell Alienware AW3423DW QD-OLED", 29990000m, 25, "9.2kg" },
                    { 184, true, 10, null, "Màn hình ASUS ROG Swift PG32UQ", 24990000m, 30, "8.5kg" },
                    { 185, true, 10, null, "Màn hình MSI Optix MPG321UR-QD", 22990000m, 30, "8.3kg" },
                    { 186, true, 10, null, "Màn hình LG 34WP65C UltraWide", 8990000m, 45, "7.4kg" },
                    { 187, true, 10, null, "Màn hình Samsung Odyssey G5 27inch", 6990000m, 50, "5.6kg" },
                    { 188, true, 10, null, "Màn hình Dell S2722DGM", 7990000m, 45, "5.8kg" },
                    { 189, true, 10, null, "Màn hình ASUS TUF Gaming VG27AQ", 8490000m, 40, "5.5kg" },
                    { 190, true, 10, null, "Màn hình Gigabyte M27Q X", 9990000m, 40, "6.1kg" },
                    { 191, true, 10, null, "Màn hình ViewSonic VX2758-2KP-MHD", 6490000m, 55, "5.2kg" },
                    { 192, true, 10, null, "Màn hình AOC 27G2", 4990000m, 60, "4.9kg" },
                    { 193, true, 10, null, "Màn hình Huawei MateView GT 34inch", 11990000m, 35, "7.8kg" },
                    { 194, true, 10, null, "Màn hình BenQ MOBIUZ EX2710S", 7490000m, 45, "5.4kg" },
                    { 195, true, 10, null, "Màn hình LG 32GN600-B UltraGear", 7990000m, 45, "6.8kg" },
                    { 196, true, 10, null, "Màn hình Dell P2422H", 4990000m, 60, "4.7kg" },
                    { 197, true, 10, null, "Màn hình Samsung LS24R350", 3490000m, 70, "3.2kg" },
                    { 198, true, 10, null, "Màn hình ASUS VA24EHE", 2990000m, 75, "3.4kg" },
                    { 199, true, 10, null, "Màn hình Philips 242V8A", 3290000m, 70, "3.6kg" },
                    { 200, true, 10, null, "Màn hình LG 24MP400-B", 2790000m, 80, "3.1kg" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cart_ProductId",
                table: "Cart",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_MemberId",
                table: "Order",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_OrderStatusId",
                table: "Order",
                column: "OrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_PaymentMethodId",
                table: "Order",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_PaymentStatusId",
                table: "Order",
                column: "PaymentStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_ProductId",
                table: "OrderDetail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryId",
                table: "Product",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Member");

            migrationBuilder.DropTable(
                name: "OrderStatus");

            migrationBuilder.DropTable(
                name: "PaymentMethod");

            migrationBuilder.DropTable(
                name: "PaymentStatus");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
