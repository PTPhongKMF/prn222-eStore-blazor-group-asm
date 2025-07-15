using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public static class DBDataSeeder
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        SeedStatuses(modelBuilder);
        SeedCategories(modelBuilder);
        SeedProducts(modelBuilder);
        SeedMembers(modelBuilder);
        SeedPaymentMethods(modelBuilder);
    }

    private static void SeedStatuses(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderStatus>().HasData(
            new OrderStatus { OrderStatusId = 1, StatusName = "Chờ xác nhận" },
            new OrderStatus { OrderStatusId = 2, StatusName = "Đang giao hàng" },
            new OrderStatus { OrderStatusId = 3, StatusName = "Đã giao hàng" }
        );

        modelBuilder.Entity<PaymentStatus>().HasData(
            new PaymentStatus { PaymentStatusId = 1, StatusName = "Chưa thanh toán" },
            new PaymentStatus { PaymentStatusId = 2, StatusName = "Đã thanh toán" }
        );
    }

    private static void SeedCategories(ModelBuilder modelBuilder)
    {
        var categories = new[]
        {
            new Category { CategoryId = 1, CategoryName = "Điện thoại" },
            new Category { CategoryId = 2, CategoryName = "Laptop" },
            new Category { CategoryId = 3, CategoryName = "Máy tính bảng" },
            new Category { CategoryId = 4, CategoryName = "Đồng hồ thông minh" },
            new Category { CategoryId = 5, CategoryName = "Tai nghe" },
            new Category { CategoryId = 6, CategoryName = "Sạc dự phòng" },
            new Category { CategoryId = 7, CategoryName = "Ốp lưng" },
            new Category { CategoryId = 8, CategoryName = "Chuột" },
            new Category { CategoryId = 9, CategoryName = "Bàn phím" },
            new Category { CategoryId = 10, CategoryName = "Màn hình" }
        };

        modelBuilder.Entity<Category>().HasData(categories);
    }

    private static void SeedProducts(ModelBuilder modelBuilder)
    {
        var products = new List<Product>();
        
        // Category 1: Điện thoại
        products.AddRange(new[]
        {
            new Product { ProductId = 1, CategoryId = 1, ProductName = "iPhone 15 Pro Max 256GB", Weight = "220g", UnitPrice = 34990000, UnitsInStock = 50 },
            new Product { ProductId = 2, CategoryId = 1, ProductName = "iPhone 15 Pro 256GB", Weight = "200g", UnitPrice = 30990000, UnitsInStock = 50 },
            new Product { ProductId = 3, CategoryId = 1, ProductName = "Samsung Galaxy S24 Ultra 256GB", Weight = "230g", UnitPrice = 33990000, UnitsInStock = 45 },
            new Product { ProductId = 4, CategoryId = 1, ProductName = "Samsung Galaxy S24+ 256GB", Weight = "210g", UnitPrice = 29990000, UnitsInStock = 45 },
            new Product { ProductId = 5, CategoryId = 1, ProductName = "OPPO Find X7 Ultra 256GB", Weight = "220g", UnitPrice = 27990000, UnitsInStock = 40 },
            new Product { ProductId = 6, CategoryId = 1, ProductName = "Xiaomi 14 Pro 256GB", Weight = "210g", UnitPrice = 24990000, UnitsInStock = 40 },
            new Product { ProductId = 7, CategoryId = 1, ProductName = "iPhone 14 Pro Max 256GB", Weight = "220g", UnitPrice = 29990000, UnitsInStock = 35 },
            new Product { ProductId = 8, CategoryId = 1, ProductName = "Samsung Galaxy Z Fold5 512GB", Weight = "250g", UnitPrice = 40990000, UnitsInStock = 30 },
            new Product { ProductId = 9, CategoryId = 1, ProductName = "OPPO Find N3 Flip 256GB", Weight = "200g", UnitPrice = 22990000, UnitsInStock = 35 },
            new Product { ProductId = 10, CategoryId = 1, ProductName = "Vivo V29e 128GB", Weight = "190g", UnitPrice = 8990000, UnitsInStock = 60 },
            new Product { ProductId = 11, CategoryId = 1, ProductName = "Xiaomi Redmi Note 13 Pro 256GB", Weight = "190g", UnitPrice = 8490000, UnitsInStock = 60 },
            new Product { ProductId = 12, CategoryId = 1, ProductName = "OPPO Reno11 F 256GB", Weight = "180g", UnitPrice = 8290000, UnitsInStock = 55 },
            new Product { ProductId = 13, CategoryId = 1, ProductName = "Samsung Galaxy A55 5G 256GB", Weight = "200g", UnitPrice = 11990000, UnitsInStock = 50 },
            new Product { ProductId = 14, CategoryId = 1, ProductName = "iPhone 13 128GB", Weight = "190g", UnitPrice = 16990000, UnitsInStock = 40 },
            new Product { ProductId = 15, CategoryId = 1, ProductName = "Vivo V29 256GB", Weight = "190g", UnitPrice = 12990000, UnitsInStock = 45 },
            new Product { ProductId = 16, CategoryId = 1, ProductName = "OPPO A98 5G 256GB", Weight = "190g", UnitPrice = 8490000, UnitsInStock = 55 },
            new Product { ProductId = 17, CategoryId = 1, ProductName = "Samsung Galaxy S23 FE 256GB", Weight = "210g", UnitPrice = 14990000, UnitsInStock = 40 },
            new Product { ProductId = 18, CategoryId = 1, ProductName = "Xiaomi 13T Pro 256GB", Weight = "200g", UnitPrice = 15990000, UnitsInStock = 35 },
            new Product { ProductId = 19, CategoryId = 1, ProductName = "OPPO Find X6 Pro 256GB", Weight = "220g", UnitPrice = 25990000, UnitsInStock = 30 },
            new Product { ProductId = 20, CategoryId = 1, ProductName = "iPhone 15 128GB", Weight = "190g", UnitPrice = 22990000, UnitsInStock = 45 }
        });

        // Category 2: Laptop
        products.AddRange(new[]
        {
            new Product { ProductId = 21, CategoryId = 2, ProductName = "MacBook Pro 14 M3 Pro", Weight = "1.55kg", UnitPrice = 49990000, UnitsInStock = 30 },
            new Product { ProductId = 22, CategoryId = 2, ProductName = "MacBook Air 15 M2", Weight = "1.51kg", UnitPrice = 32990000, UnitsInStock = 35 },
            new Product { ProductId = 23, CategoryId = 2, ProductName = "Dell XPS 15 9530", Weight = "1.75kg", UnitPrice = 59990000, UnitsInStock = 25 },
            new Product { ProductId = 24, CategoryId = 2, ProductName = "ASUS ROG Zephyrus G14", Weight = "1.65kg", UnitPrice = 39990000, UnitsInStock = 30 },
            new Product { ProductId = 25, CategoryId = 2, ProductName = "Lenovo Legion Pro 7", Weight = "2.5kg", UnitPrice = 49990000, UnitsInStock = 25 },
            new Product { ProductId = 26, CategoryId = 2, ProductName = "HP Envy 16", Weight = "2.2kg", UnitPrice = 35990000, UnitsInStock = 30 },
            new Product { ProductId = 27, CategoryId = 2, ProductName = "Acer Predator Helios 16", Weight = "2.6kg", UnitPrice = 45990000, UnitsInStock = 25 },
            new Product { ProductId = 28, CategoryId = 2, ProductName = "MSI Stealth 16", Weight = "2.1kg", UnitPrice = 42990000, UnitsInStock = 25 },
            new Product { ProductId = 29, CategoryId = 2, ProductName = "Dell Inspiron 16", Weight = "1.85kg", UnitPrice = 19990000, UnitsInStock = 40 },
            new Product { ProductId = 30, CategoryId = 2, ProductName = "ASUS Vivobook 15", Weight = "1.7kg", UnitPrice = 16990000, UnitsInStock = 45 },
            new Product { ProductId = 31, CategoryId = 2, ProductName = "HP Pavilion 15", Weight = "1.75kg", UnitPrice = 17990000, UnitsInStock = 45 },
            new Product { ProductId = 32, CategoryId = 2, ProductName = "Lenovo IdeaPad Slim 5", Weight = "1.65kg", UnitPrice = 18990000, UnitsInStock = 40 },
            new Product { ProductId = 33, CategoryId = 2, ProductName = "Acer Aspire 7", Weight = "2.1kg", UnitPrice = 19990000, UnitsInStock = 35 },
            new Product { ProductId = 34, CategoryId = 2, ProductName = "MSI Modern 15", Weight = "1.7kg", UnitPrice = 16990000, UnitsInStock = 40 },
            new Product { ProductId = 35, CategoryId = 2, ProductName = "MacBook Air 13 M2", Weight = "1.24kg", UnitPrice = 27990000, UnitsInStock = 35 },
            new Product { ProductId = 36, CategoryId = 2, ProductName = "Dell Vostro 15", Weight = "1.8kg", UnitPrice = 15990000, UnitsInStock = 45 },
            new Product { ProductId = 37, CategoryId = 2, ProductName = "ASUS TUF Gaming A15", Weight = "2.2kg", UnitPrice = 22990000, UnitsInStock = 35 },
            new Product { ProductId = 38, CategoryId = 2, ProductName = "Lenovo ThinkPad E15", Weight = "1.7kg", UnitPrice = 20990000, UnitsInStock = 35 },
            new Product { ProductId = 39, CategoryId = 2, ProductName = "HP ProBook 450 G10", Weight = "1.8kg", UnitPrice = 21990000, UnitsInStock = 30 },
            new Product { ProductId = 40, CategoryId = 2, ProductName = "MSI Katana 15", Weight = "2.25kg", UnitPrice = 23990000, UnitsInStock = 30 }
        });

        // Category 3: Máy tính bảng
        products.AddRange(new[]
        {
            new Product { ProductId = 41, CategoryId = 3, ProductName = "iPad Pro 12.9 M2 WiFi 256GB", Weight = "682g", UnitPrice = 31990000, UnitsInStock = 35 },
            new Product { ProductId = 42, CategoryId = 3, ProductName = "iPad Pro 11 M2 WiFi 128GB", Weight = "466g", UnitPrice = 20990000, UnitsInStock = 40 },
            new Product { ProductId = 43, CategoryId = 3, ProductName = "Samsung Galaxy Tab S9 Ultra", Weight = "732g", UnitPrice = 29990000, UnitsInStock = 35 },
            new Product { ProductId = 44, CategoryId = 3, ProductName = "iPad Air 5 WiFi 64GB", Weight = "461g", UnitPrice = 15990000, UnitsInStock = 45 },
            new Product { ProductId = 45, CategoryId = 3, ProductName = "Samsung Galaxy Tab S9+", Weight = "581g", UnitPrice = 24990000, UnitsInStock = 40 },
            new Product { ProductId = 46, CategoryId = 3, ProductName = "iPad 10 WiFi 64GB", Weight = "477g", UnitPrice = 11990000, UnitsInStock = 50 },
            new Product { ProductId = 47, CategoryId = 3, ProductName = "Samsung Galaxy Tab S9", Weight = "498g", UnitPrice = 20990000, UnitsInStock = 45 },
            new Product { ProductId = 48, CategoryId = 3, ProductName = "iPad mini 6 WiFi 64GB", Weight = "293g", UnitPrice = 12990000, UnitsInStock = 45 },
            new Product { ProductId = 49, CategoryId = 3, ProductName = "Xiaomi Pad 6", Weight = "490g", UnitPrice = 8990000, UnitsInStock = 55 },
            new Product { ProductId = 50, CategoryId = 3, ProductName = "OPPO Pad 2", Weight = "552g", UnitPrice = 13990000, UnitsInStock = 40 },
            new Product { ProductId = 51, CategoryId = 3, ProductName = "Samsung Galaxy Tab A9+", Weight = "480g", UnitPrice = 6990000, UnitsInStock = 60 },
            new Product { ProductId = 52, CategoryId = 3, ProductName = "Lenovo Tab P12 Pro", Weight = "565g", UnitPrice = 14990000, UnitsInStock = 35 },
            new Product { ProductId = 53, CategoryId = 3, ProductName = "Samsung Galaxy Tab A8", Weight = "508g", UnitPrice = 5990000, UnitsInStock = 60 },
            new Product { ProductId = 54, CategoryId = 3, ProductName = "Xiaomi Pad 6 Pro", Weight = "485g", UnitPrice = 11990000, UnitsInStock = 45 },
            new Product { ProductId = 55, CategoryId = 3, ProductName = "OPPO Pad Air", Weight = "440g", UnitPrice = 6990000, UnitsInStock = 55 },
            new Product { ProductId = 56, CategoryId = 3, ProductName = "iPad 9 WiFi 64GB", Weight = "487g", UnitPrice = 7990000, UnitsInStock = 50 },
            new Product { ProductId = 57, CategoryId = 3, ProductName = "Lenovo Tab M10", Weight = "460g", UnitPrice = 4990000, UnitsInStock = 65 },
            new Product { ProductId = 58, CategoryId = 3, ProductName = "Samsung Galaxy Tab A7 Lite", Weight = "366g", UnitPrice = 3990000, UnitsInStock = 70 },
            new Product { ProductId = 59, CategoryId = 3, ProductName = "Xiaomi Redmi Pad SE", Weight = "478g", UnitPrice = 4490000, UnitsInStock = 65 },
            new Product { ProductId = 60, CategoryId = 3, ProductName = "Nokia T21", Weight = "465g", UnitPrice = 4990000, UnitsInStock = 60 }
        });

        // Category 4: Đồng hồ thông minh
        products.AddRange(new[]
        {
            new Product { ProductId = 61, CategoryId = 4, ProductName = "Apple Watch Ultra 2 GPS 49mm", Weight = "61.4g", UnitPrice = 21990000, UnitsInStock = 40 },
            new Product { ProductId = 62, CategoryId = 4, ProductName = "Apple Watch Series 9 GPS 45mm", Weight = "38.8g", UnitPrice = 11990000, UnitsInStock = 50 },
            new Product { ProductId = 63, CategoryId = 4, ProductName = "Samsung Galaxy Watch6 Classic 47mm", Weight = "59g", UnitPrice = 9990000, UnitsInStock = 55 },
            new Product { ProductId = 64, CategoryId = 4, ProductName = "Garmin Fenix 7X Sapphire Solar", Weight = "89g", UnitPrice = 23990000, UnitsInStock = 35 },
            new Product { ProductId = 65, CategoryId = 4, ProductName = "Apple Watch SE 2 GPS 44mm", Weight = "33g", UnitPrice = 6990000, UnitsInStock = 60 },
            new Product { ProductId = 66, CategoryId = 4, ProductName = "Samsung Galaxy Watch6 44mm", Weight = "33.3g", UnitPrice = 7490000, UnitsInStock = 55 },
            new Product { ProductId = 67, CategoryId = 4, ProductName = "Garmin Venu 3", Weight = "47g", UnitPrice = 12990000, UnitsInStock = 45 },
            new Product { ProductId = 68, CategoryId = 4, ProductName = "Huawei Watch GT4 46mm", Weight = "48g", UnitPrice = 6990000, UnitsInStock = 50 },
            new Product { ProductId = 69, CategoryId = 4, ProductName = "Amazfit GTR 4", Weight = "34g", UnitPrice = 4990000, UnitsInStock = 60 },
            new Product { ProductId = 70, CategoryId = 4, ProductName = "Xiaomi Watch S3", Weight = "44g", UnitPrice = 3990000, UnitsInStock = 65 },
            new Product { ProductId = 71, CategoryId = 4, ProductName = "Apple Watch Series 8 GPS 45mm", Weight = "38.8g", UnitPrice = 9990000, UnitsInStock = 45 },
            new Product { ProductId = 72, CategoryId = 4, ProductName = "Samsung Galaxy Watch5 Pro 45mm", Weight = "46.5g", UnitPrice = 8990000, UnitsInStock = 50 },
            new Product { ProductId = 73, CategoryId = 4, ProductName = "Garmin Forerunner 965", Weight = "53g", UnitPrice = 16990000, UnitsInStock = 40 },
            new Product { ProductId = 74, CategoryId = 4, ProductName = "Huawei Watch GT3 Pro", Weight = "54g", UnitPrice = 7990000, UnitsInStock = 45 },
            new Product { ProductId = 75, CategoryId = 4, ProductName = "Amazfit T-Rex 2", Weight = "66.5g", UnitPrice = 4490000, UnitsInStock = 55 },
            new Product { ProductId = 76, CategoryId = 4, ProductName = "Xiaomi Watch 2 Pro", Weight = "55g", UnitPrice = 5990000, UnitsInStock = 50 },
            new Product { ProductId = 77, CategoryId = 4, ProductName = "Garmin Instinct 2X Solar", Weight = "67g", UnitPrice = 14990000, UnitsInStock = 40 },
            new Product { ProductId = 78, CategoryId = 4, ProductName = "Samsung Galaxy Watch4 Classic 46mm", Weight = "52g", UnitPrice = 6990000, UnitsInStock = 45 },
            new Product { ProductId = 79, CategoryId = 4, ProductName = "Huawei Band 8", Weight = "14g", UnitPrice = 990000, UnitsInStock = 80 },
            new Product { ProductId = 80, CategoryId = 4, ProductName = "Xiaomi Smart Band 8 Pro", Weight = "22.5g", UnitPrice = 1490000, UnitsInStock = 75 }
        });

        // Category 5: Tai nghe
        products.AddRange(new[]
        {
            new Product { ProductId = 81, CategoryId = 5, ProductName = "AirPods Pro 2", Weight = "50.8g", UnitPrice = 6490000, UnitsInStock = 60 },
            new Product { ProductId = 82, CategoryId = 5, ProductName = "Sony WH-1000XM5", Weight = "250g", UnitPrice = 8490000, UnitsInStock = 50 },
            new Product { ProductId = 83, CategoryId = 5, ProductName = "Samsung Galaxy Buds2 Pro", Weight = "51.5g", UnitPrice = 4990000, UnitsInStock = 65 },
            new Product { ProductId = 84, CategoryId = 5, ProductName = "Apple AirPods Max", Weight = "384.8g", UnitPrice = 12990000, UnitsInStock = 40 },
            new Product { ProductId = 85, CategoryId = 5, ProductName = "Bose QuietComfort Ultra", Weight = "250g", UnitPrice = 9990000, UnitsInStock = 45 },
            new Product { ProductId = 86, CategoryId = 5, ProductName = "JBL Tour One M2", Weight = "268g", UnitPrice = 7490000, UnitsInStock = 50 },
            new Product { ProductId = 87, CategoryId = 5, ProductName = "Sony WF-1000XM5", Weight = "41.7g", UnitPrice = 6490000, UnitsInStock = 55 },
            new Product { ProductId = 88, CategoryId = 5, ProductName = "AirPods 3", Weight = "37.9g", UnitPrice = 4490000, UnitsInStock = 65 },
            new Product { ProductId = 89, CategoryId = 5, ProductName = "Jabra Elite 10", Weight = "57g", UnitPrice = 5990000, UnitsInStock = 50 },
            new Product { ProductId = 90, CategoryId = 5, ProductName = "Samsung Galaxy Buds FE", Weight = "40.2g", UnitPrice = 2490000, UnitsInStock = 70 },
            new Product { ProductId = 91, CategoryId = 5, ProductName = "Soundcore Space One", Weight = "280g", UnitPrice = 2990000, UnitsInStock = 60 },
            new Product { ProductId = 92, CategoryId = 5, ProductName = "Huawei FreeBuds Pro 3", Weight = "48g", UnitPrice = 4990000, UnitsInStock = 55 },
            new Product { ProductId = 93, CategoryId = 5, ProductName = "Nothing Ear (2)", Weight = "51.9g", UnitPrice = 3490000, UnitsInStock = 60 },
            new Product { ProductId = 94, CategoryId = 5, ProductName = "Sony WH-CH720N", Weight = "192g", UnitPrice = 2490000, UnitsInStock = 65 },
            new Product { ProductId = 95, CategoryId = 5, ProductName = "JBL Tune 770NC", Weight = "220g", UnitPrice = 2990000, UnitsInStock = 60 },
            new Product { ProductId = 96, CategoryId = 5, ProductName = "Xiaomi Redmi Buds 4 Pro", Weight = "47g", UnitPrice = 1490000, UnitsInStock = 75 },
            new Product { ProductId = 97, CategoryId = 5, ProductName = "OPPO Enco Air3 Pro", Weight = "46g", UnitPrice = 1990000, UnitsInStock = 70 },
            new Product { ProductId = 98, CategoryId = 5, ProductName = "Soundpeats Air3 Deluxe HS", Weight = "43g", UnitPrice = 990000, UnitsInStock = 80 },
            new Product { ProductId = 99, CategoryId = 5, ProductName = "Edifier WH950NB", Weight = "275g", UnitPrice = 1990000, UnitsInStock = 65 },
            new Product { ProductId = 100, CategoryId = 5, ProductName = "Havit TW945", Weight = "45g", UnitPrice = 490000, UnitsInStock = 85 }
        });

        // Category 6: Sạc dự phòng
        products.AddRange(new[]
        {
            new Product { ProductId = 101, CategoryId = 6, ProductName = "Sạc dự phòng Apple MagSafe 5000mAh", Weight = "150g", UnitPrice = 2490000, UnitsInStock = 70 },
            new Product { ProductId = 102, CategoryId = 6, ProductName = "Samsung Wireless Battery Pack 10000mAh", Weight = "240g", UnitPrice = 1290000, UnitsInStock = 75 },
            new Product { ProductId = 103, CategoryId = 6, ProductName = "Anker PowerCore 20000mAh PD", Weight = "345g", UnitPrice = 1490000, UnitsInStock = 80 },
            new Product { ProductId = 104, CategoryId = 6, ProductName = "Xiaomi Redmi 20000mAh Fast Charge", Weight = "350g", UnitPrice = 790000, UnitsInStock = 85 },
            new Product { ProductId = 105, CategoryId = 6, ProductName = "Belkin BoostCharge 10K Power Bank", Weight = "220g", UnitPrice = 990000, UnitsInStock = 75 },
            new Product { ProductId = 106, CategoryId = 6, ProductName = "OPPO VOOC 10000mAh", Weight = "230g", UnitPrice = 890000, UnitsInStock = 80 },
            new Product { ProductId = 107, CategoryId = 6, ProductName = "Energizer UE20000 20000mAh", Weight = "360g", UnitPrice = 690000, UnitsInStock = 90 },
            new Product { ProductId = 108, CategoryId = 6, ProductName = "Anker PowerCore Slim 10000mAh PD", Weight = "210g", UnitPrice = 890000, UnitsInStock = 85 },
            new Product { ProductId = 109, CategoryId = 6, ProductName = "Baseus Adaman 20000mAh", Weight = "370g", UnitPrice = 990000, UnitsInStock = 80 },
            new Product { ProductId = 110, CategoryId = 6, ProductName = "Mophie Powerstation XXL 20000mAh", Weight = "380g", UnitPrice = 1990000, UnitsInStock = 70 },
            new Product { ProductId = 111, CategoryId = 6, ProductName = "Xiaomi Mi Power Bank 3 30000mAh", Weight = "440g", UnitPrice = 890000, UnitsInStock = 75 },
            new Product { ProductId = 112, CategoryId = 6, ProductName = "AUKEY PB-Y36 Sprint 26800mAh", Weight = "420g", UnitPrice = 1190000, UnitsInStock = 70 },
            new Product { ProductId = 113, CategoryId = 6, ProductName = "RAVPower 15000mAh PD", Weight = "280g", UnitPrice = 790000, UnitsInStock = 85 },
            new Product { ProductId = 114, CategoryId = 6, ProductName = "ROMOSS Sense 8+ 30000mAh", Weight = "450g", UnitPrice = 690000, UnitsInStock = 90 },
            new Product { ProductId = 115, CategoryId = 6, ProductName = "Anker PowerCore III 19200mAh", Weight = "365g", UnitPrice = 1690000, UnitsInStock = 75 },
            new Product { ProductId = 116, CategoryId = 6, ProductName = "Samsung EB-P3300 10000mAh", Weight = "235g", UnitPrice = 890000, UnitsInStock = 80 },
            new Product { ProductId = 117, CategoryId = 6, ProductName = "Baseus Amblight 20000mAh", Weight = "375g", UnitPrice = 890000, UnitsInStock = 85 },
            new Product { ProductId = 118, CategoryId = 6, ProductName = "UMETRAVEL 10000mAh Wireless", Weight = "245g", UnitPrice = 590000, UnitsInStock = 95 },
            new Product { ProductId = 119, CategoryId = 6, ProductName = "Energizer QE10007PQ 10000mAh", Weight = "225g", UnitPrice = 490000, UnitsInStock = 100 },
            new Product { ProductId = 120, CategoryId = 6, ProductName = "Xmobile PowerLite P181P 18000mAh", Weight = "340g", UnitPrice = 390000, UnitsInStock = 100 }
        });

        // Category 7: Ốp lưng
        products.AddRange(new[]
        {
            new Product { ProductId = 121, CategoryId = 7, ProductName = "Ốp lưng iPhone 15 Pro Max Apple Silicone Case", Weight = "45g", UnitPrice = 1290000, UnitsInStock = 100 },
            new Product { ProductId = 122, CategoryId = 7, ProductName = "Ốp lưng Samsung S24 Ultra Led View Cover", Weight = "65g", UnitPrice = 1490000, UnitsInStock = 90 },
            new Product { ProductId = 123, CategoryId = 7, ProductName = "Ốp lưng iPhone 15 Pro Max Spigen Ultra Hybrid", Weight = "40g", UnitPrice = 590000, UnitsInStock = 120 },
            new Product { ProductId = 124, CategoryId = 7, ProductName = "Ốp lưng Z Fold5 Samsung Slim Standing Cover", Weight = "85g", UnitPrice = 990000, UnitsInStock = 80 },
            new Product { ProductId = 125, CategoryId = 7, ProductName = "Ốp lưng iPhone 15 UAG Monarch", Weight = "50g", UnitPrice = 890000, UnitsInStock = 95 },
            new Product { ProductId = 126, CategoryId = 7, ProductName = "Ốp lưng S24 Ultra Spigen Liquid Air", Weight = "42g", UnitPrice = 490000, UnitsInStock = 110 },
            new Product { ProductId = 127, CategoryId = 7, ProductName = "Ốp lưng iPhone 15 Pro Max UNIQ Lifepro Xtreme", Weight = "48g", UnitPrice = 790000, UnitsInStock = 100 },
            new Product { ProductId = 128, CategoryId = 7, ProductName = "Ốp lưng Z Flip5 Samsung Clear Gadget Case", Weight = "55g", UnitPrice = 890000, UnitsInStock = 85 },
            new Product { ProductId = 129, CategoryId = 7, ProductName = "Ốp lưng iPhone 15 Pro Max Mous Limitless 5.0", Weight = "52g", UnitPrice = 1390000, UnitsInStock = 75 },
            new Product { ProductId = 130, CategoryId = 7, ProductName = "Ốp lưng S24+ Ringke Fusion", Weight = "38g", UnitPrice = 290000, UnitsInStock = 130 },
            new Product { ProductId = 131, CategoryId = 7, ProductName = "Ốp lưng iPhone 15 Pro Max NILLKIN Textured", Weight = "35g", UnitPrice = 190000, UnitsInStock = 140 },
            new Product { ProductId = 132, CategoryId = 7, ProductName = "Ốp lưng S24 Ultra NILLKIN CamShield", Weight = "45g", UnitPrice = 290000, UnitsInStock = 120 },
            new Product { ProductId = 133, CategoryId = 7, ProductName = "Ốp lưng iPhone 15 Pro Max ESR Classic", Weight = "32g", UnitPrice = 390000, UnitsInStock = 125 },
            new Product { ProductId = 134, CategoryId = 7, ProductName = "Ốp lưng Xiaomi 14 Pro ZADEZ Hybrid", Weight = "36g", UnitPrice = 190000, UnitsInStock = 135 },
            new Product { ProductId = 135, CategoryId = 7, ProductName = "Ốp lưng OPPO Find X7 Ultra Memumi Slim", Weight = "34g", UnitPrice = 240000, UnitsInStock = 130 },
            new Product { ProductId = 136, CategoryId = 7, ProductName = "Ốp lưng iPhone 15 Pro Max Benks Magic OA", Weight = "42g", UnitPrice = 490000, UnitsInStock = 110 },
            new Product { ProductId = 137, CategoryId = 7, ProductName = "Ốp lưng S24 Ultra X-Level Guardian", Weight = "38g", UnitPrice = 190000, UnitsInStock = 140 },
            new Product { ProductId = 138, CategoryId = 7, ProductName = "Ốp lưng iPhone 15 Pro Max Jinya Defense", Weight = "44g", UnitPrice = 390000, UnitsInStock = 115 },
            new Product { ProductId = 139, CategoryId = 7, ProductName = "Ốp lưng Vivo V29e Nắp lưng nhựa cứng", Weight = "30g", UnitPrice = 90000, UnitsInStock = 150 },
            new Product { ProductId = 140, CategoryId = 7, ProductName = "Ốp lưng OPPO Reno11 F Ốp dẻo trong", Weight = "25g", UnitPrice = 50000, UnitsInStock = 200 }
        });

        // Category 8: Chuột
        products.AddRange(new[]
        {
            new Product { ProductId = 141, CategoryId = 8, ProductName = "Chuột Logitech G Pro X Superlight", Weight = "63g", UnitPrice = 2990000, UnitsInStock = 60 },
            new Product { ProductId = 142, CategoryId = 8, ProductName = "Chuột Razer DeathAdder V3 Pro", Weight = "64g", UnitPrice = 3490000, UnitsInStock = 55 },
            new Product { ProductId = 143, CategoryId = 8, ProductName = "Chuột SteelSeries Aerox 3 Wireless", Weight = "68g", UnitPrice = 1990000, UnitsInStock = 70 },
            new Product { ProductId = 144, CategoryId = 8, ProductName = "Chuột Logitech MX Master 3S", Weight = "141g", UnitPrice = 2790000, UnitsInStock = 65 },
            new Product { ProductId = 145, CategoryId = 8, ProductName = "Chuột Razer Basilisk V3 Pro", Weight = "112g", UnitPrice = 3990000, UnitsInStock = 50 },
            new Product { ProductId = 146, CategoryId = 8, ProductName = "Chuột Corsair Dark Core RGB Pro SE", Weight = "133g", UnitPrice = 2490000, UnitsInStock = 60 },
            new Product { ProductId = 147, CategoryId = 8, ProductName = "Chuột Pulsar X2 Wireless", Weight = "55g", UnitPrice = 2990000, UnitsInStock = 55 },
            new Product { ProductId = 148, CategoryId = 8, ProductName = "Chuột Zowie EC2-C", Weight = "88g", UnitPrice = 1890000, UnitsInStock = 75 },
            new Product { ProductId = 149, CategoryId = 8, ProductName = "Chuột Logitech G502 X Plus", Weight = "106g", UnitPrice = 3290000, UnitsInStock = 60 },
            new Product { ProductId = 150, CategoryId = 8, ProductName = "Chuột HyperX Pulsefire Haste", Weight = "59g", UnitPrice = 1490000, UnitsInStock = 80 },
            new Product { ProductId = 151, CategoryId = 8, ProductName = "Chuột Razer Viper V2 Pro", Weight = "58g", UnitPrice = 3490000, UnitsInStock = 55 },
            new Product { ProductId = 152, CategoryId = 8, ProductName = "Chuột Logitech G304 Lightspeed", Weight = "99g", UnitPrice = 790000, UnitsInStock = 100 },
            new Product { ProductId = 153, CategoryId = 8, ProductName = "Chuột SteelSeries Prime Wireless", Weight = "80g", UnitPrice = 2490000, UnitsInStock = 65 },
            new Product { ProductId = 154, CategoryId = 8, ProductName = "Chuột Glorious Model O Wireless", Weight = "69g", UnitPrice = 1990000, UnitsInStock = 70 },
            new Product { ProductId = 155, CategoryId = 8, ProductName = "Chuột Endgame Gear XM2w", Weight = "63g", UnitPrice = 2790000, UnitsInStock = 60 },
            new Product { ProductId = 156, CategoryId = 8, ProductName = "Chuột Logitech M590", Weight = "101g", UnitPrice = 690000, UnitsInStock = 95 },
            new Product { ProductId = 157, CategoryId = 8, ProductName = "Chuột Razer Orochi V2", Weight = "60g", UnitPrice = 1490000, UnitsInStock = 80 },
            new Product { ProductId = 158, CategoryId = 8, ProductName = "Chuột Corsair Sabre RGB Pro", Weight = "74g", UnitPrice = 1290000, UnitsInStock = 85 },
            new Product { ProductId = 159, CategoryId = 8, ProductName = "Chuột Dare-U EM901X", Weight = "69g", UnitPrice = 490000, UnitsInStock = 110 },
            new Product { ProductId = 160, CategoryId = 8, ProductName = "Chuột Fuhlen G90 Pro", Weight = "75g", UnitPrice = 390000, UnitsInStock = 120 }
        });

        // Category 9: Bàn phím
        products.AddRange(new[]
        {
            new Product { ProductId = 161, CategoryId = 9, ProductName = "Bàn phím Leopold FC900R PD", Weight = "1.2kg", UnitPrice = 3490000, UnitsInStock = 45 },
            new Product { ProductId = 162, CategoryId = 9, ProductName = "Bàn phím Keychron Q1 QMK", Weight = "1.6kg", UnitPrice = 4290000, UnitsInStock = 40 },
            new Product { ProductId = 163, CategoryId = 9, ProductName = "Bàn phím Ducky One 3 RGB", Weight = "1.1kg", UnitPrice = 2990000, UnitsInStock = 50 },
            new Product { ProductId = 164, CategoryId = 9, ProductName = "Bàn phím GMMK Pro", Weight = "1.8kg", UnitPrice = 4990000, UnitsInStock = 35 },
            new Product { ProductId = 165, CategoryId = 9, ProductName = "Bàn phím Akko MOD 007", Weight = "2.2kg", UnitPrice = 3990000, UnitsInStock = 40 },
            new Product { ProductId = 166, CategoryId = 9, ProductName = "Bàn phím Varmilo VA87M", Weight = "1.0kg", UnitPrice = 2890000, UnitsInStock = 55 },
            new Product { ProductId = 167, CategoryId = 9, ProductName = "Bàn phím Leopold FC750R PD", Weight = "0.9kg", UnitPrice = 2990000, UnitsInStock = 50 },
            new Product { ProductId = 168, CategoryId = 9, ProductName = "Bàn phím Keychron K8 Pro", Weight = "0.8kg", UnitPrice = 2490000, UnitsInStock = 60 },
            new Product { ProductId = 169, CategoryId = 9, ProductName = "Bàn phím Akko 3098B", Weight = "1.1kg", UnitPrice = 1990000, UnitsInStock = 65 },
            new Product { ProductId = 170, CategoryId = 9, ProductName = "Bàn phím Ducky One 2 Mini", Weight = "0.6kg", UnitPrice = 2290000, UnitsInStock = 60 },
            new Product { ProductId = 171, CategoryId = 9, ProductName = "Bàn phím Royal Kludge RK84", Weight = "0.7kg", UnitPrice = 1490000, UnitsInStock = 75 },
            new Product { ProductId = 172, CategoryId = 9, ProductName = "Bàn phím FL-Esports CMK87", Weight = "0.85kg", UnitPrice = 1790000, UnitsInStock = 70 },
            new Product { ProductId = 173, CategoryId = 9, ProductName = "Bàn phím Akko 3068B", Weight = "0.65kg", UnitPrice = 1690000, UnitsInStock = 70 },
            new Product { ProductId = 174, CategoryId = 9, ProductName = "Bàn phím E-Dra EK387 Pro", Weight = "0.7kg", UnitPrice = 890000, UnitsInStock = 90 },
            new Product { ProductId = 175, CategoryId = 9, ProductName = "Bàn phím GMMK 2", Weight = "0.8kg", UnitPrice = 2490000, UnitsInStock = 55 },
            new Product { ProductId = 176, CategoryId = 9, ProductName = "Bàn phím Keychron K2 V2", Weight = "0.66kg", UnitPrice = 1990000, UnitsInStock = 65 },
            new Product { ProductId = 177, CategoryId = 9, ProductName = "Bàn phím Dare-U DK880", Weight = "0.75kg", UnitPrice = 790000, UnitsInStock = 95 },
            new Product { ProductId = 178, CategoryId = 9, ProductName = "Bàn phím Fuhlen Eraser", Weight = "0.9kg", UnitPrice = 990000, UnitsInStock = 85 },
            new Product { ProductId = 179, CategoryId = 9, ProductName = "Bàn phím Rapoo V500Pro", Weight = "0.95kg", UnitPrice = 690000, UnitsInStock = 100 },
            new Product { ProductId = 180, CategoryId = 9, ProductName = "Bàn phím Newmen GM819", Weight = "0.85kg", UnitPrice = 590000, UnitsInStock = 110 }
        });

        // Category 10: Màn hình
        products.AddRange(new[]
        {
            new Product { ProductId = 181, CategoryId = 10, ProductName = "Màn hình LG 27GP950 UltraGear 4K", Weight = "6.2kg", UnitPrice = 19990000, UnitsInStock = 30 },
            new Product { ProductId = 182, CategoryId = 10, ProductName = "Màn hình Samsung Odyssey G7 32inch", Weight = "7.8kg", UnitPrice = 16990000, UnitsInStock = 35 },
            new Product { ProductId = 183, CategoryId = 10, ProductName = "Màn hình Dell Alienware AW3423DW QD-OLED", Weight = "9.2kg", UnitPrice = 29990000, UnitsInStock = 25 },
            new Product { ProductId = 184, CategoryId = 10, ProductName = "Màn hình ASUS ROG Swift PG32UQ", Weight = "8.5kg", UnitPrice = 24990000, UnitsInStock = 30 },
            new Product { ProductId = 185, CategoryId = 10, ProductName = "Màn hình MSI Optix MPG321UR-QD", Weight = "8.3kg", UnitPrice = 22990000, UnitsInStock = 30 },
            new Product { ProductId = 186, CategoryId = 10, ProductName = "Màn hình LG 34WP65C UltraWide", Weight = "7.4kg", UnitPrice = 8990000, UnitsInStock = 45 },
            new Product { ProductId = 187, CategoryId = 10, ProductName = "Màn hình Samsung Odyssey G5 27inch", Weight = "5.6kg", UnitPrice = 6990000, UnitsInStock = 50 },
            new Product { ProductId = 188, CategoryId = 10, ProductName = "Màn hình Dell S2722DGM", Weight = "5.8kg", UnitPrice = 7990000, UnitsInStock = 45 },
            new Product { ProductId = 189, CategoryId = 10, ProductName = "Màn hình ASUS TUF Gaming VG27AQ", Weight = "5.5kg", UnitPrice = 8490000, UnitsInStock = 40 },
            new Product { ProductId = 190, CategoryId = 10, ProductName = "Màn hình Gigabyte M27Q X", Weight = "6.1kg", UnitPrice = 9990000, UnitsInStock = 40 },
            new Product { ProductId = 191, CategoryId = 10, ProductName = "Màn hình ViewSonic VX2758-2KP-MHD", Weight = "5.2kg", UnitPrice = 6490000, UnitsInStock = 55 },
            new Product { ProductId = 192, CategoryId = 10, ProductName = "Màn hình AOC 27G2", Weight = "4.9kg", UnitPrice = 4990000, UnitsInStock = 60 },
            new Product { ProductId = 193, CategoryId = 10, ProductName = "Màn hình Huawei MateView GT 34inch", Weight = "7.8kg", UnitPrice = 11990000, UnitsInStock = 35 },
            new Product { ProductId = 194, CategoryId = 10, ProductName = "Màn hình BenQ MOBIUZ EX2710S", Weight = "5.4kg", UnitPrice = 7490000, UnitsInStock = 45 },
            new Product { ProductId = 195, CategoryId = 10, ProductName = "Màn hình LG 32GN600-B UltraGear", Weight = "6.8kg", UnitPrice = 7990000, UnitsInStock = 45 },
            new Product { ProductId = 196, CategoryId = 10, ProductName = "Màn hình Dell P2422H", Weight = "4.7kg", UnitPrice = 4990000, UnitsInStock = 60 },
            new Product { ProductId = 197, CategoryId = 10, ProductName = "Màn hình Samsung LS24R350", Weight = "3.2kg", UnitPrice = 3490000, UnitsInStock = 70 },
            new Product { ProductId = 198, CategoryId = 10, ProductName = "Màn hình ASUS VA24EHE", Weight = "3.4kg", UnitPrice = 2990000, UnitsInStock = 75 },
            new Product { ProductId = 199, CategoryId = 10, ProductName = "Màn hình Philips 242V8A", Weight = "3.6kg", UnitPrice = 3290000, UnitsInStock = 70 },
            new Product { ProductId = 200, CategoryId = 10, ProductName = "Màn hình LG 24MP400-B", Weight = "3.1kg", UnitPrice = 2790000, UnitsInStock = 80 }
        });

        modelBuilder.Entity<Product>().HasData(products);
    }

    private static void SeedMembers(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Member>().HasData(
            new Member 
            { 
                MemberId = 1, 
                Email = "a@a.com",
                Password = "123456",
                CompanyName = "Công ty A",
                City = "Hà Nội",
                Country = "Việt Nam"
            },
            new Member 
            { 
                MemberId = 2, 
                Email = "b@b.com",
                Password = "123456",
                CompanyName = "Công ty B",
                City = "Hồ Chí Minh",
                Country = "Việt Nam"
            },
            new Member 
            { 
                MemberId = 3, 
                Email = "c@c.com",
                Password = "123456",
                CompanyName = "Công ty C",
                City = "Đà Nẵng",
                Country = "Việt Nam"
            }
        );
    }

    private static void SeedPaymentMethods(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PaymentMethod>().HasData(
            new PaymentMethod { PaymentMethodId = 1, Name = "Tiền mặt khi nhận hàng" },
            new PaymentMethod { PaymentMethodId = 2, Name = "Chuyển khoản ngân hàng" },
            new PaymentMethod { PaymentMethodId = 3, Name = "Ví điện tử MoMo" },
            new PaymentMethod { PaymentMethodId = 4, Name = "Ví ZaloPay" },
            new PaymentMethod { PaymentMethodId = 5, Name = "Thẻ tín dụng/ghi nợ" }
        );
    }
}