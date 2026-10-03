using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StyleHub.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class seedCategoreisProductsWithTranslationsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "DefaultDescription", "DefaultName", "ImageUrl", "ParentCategoryId" },
                values: new object[,]
                {
                    { 1, "", "Defence Industries", "images\\categories\\defence_industries.jpg", null },
                    { 2, "", "Civil Industries", "images\\categories\\civil_industries.jpg", null },
                    { 3, "", "Medical Equipment", "images\\categories\\medical_equipment.jpg", null },
                    { 4, "", "Sports Equipment", "images\\categories\\sports_equipment.jpg", null },
                    { 5, "", "Physical Therapy Devices", "images\\categories\\physical_therapy_devices.jpg", 4 }
                });

            migrationBuilder.InsertData(
                table: "CategoryTranslaltiosns",
                columns: new[] { "Id", "CategoryId", "Description", "IsDefault", "Language", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Industries related to defense and military.", true, "en-us", "Defence Industries" },
                    { 2, 1, "صناعات متعلقة بالدفاع والعسكرية.", false, "ar-eg", "الصناعات الدفاعية" },
                    { 3, 2, "Industries for civilian applications.", true, "en-us", "Civil Industries" },
                    { 4, 2, "صناعات للاستخدامات المدنية.", false, "ar-eg", "الصناعات المدنية" },
                    { 5, 3, "Equipment used in healthcare and medical fields.", true, "en-us", "Medical Equipment" },
                    { 6, 3, "المعدات المستخدمة في مجالات الرعاية الصحية والطبية.", false, "ar-eg", "المعدات الطبية" },
                    { 7, 4, "Equipment for sports and fitness.", true, "en-us", "Sports Equipment" },
                    { 8, 4, "معدات الرياضة واللياقة البدنية.", false, "ar-eg", "معدات رياضية" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "DefaultDescription", "DefaultName", "Price", "Rate", "Stock", "SubCategoryId" },
                values: new object[,]
                {
                    { 1, 1, null, null, 0.0, 5, 0, null },
                    { 2, 1, null, null, 0.0, 5, 0, null },
                    { 3, 2, null, null, 0.0, 5, 0, null },
                    { 4, 2, null, null, 0.0, 5, 0, null },
                    { 5, 3, null, null, 0.0, 5, 0, null },
                    { 6, 4, null, null, 0.0, 5, 0, null }
                });

            migrationBuilder.InsertData(
                table: "CategoryTranslaltiosns",
                columns: new[] { "Id", "CategoryId", "Description", "IsDefault", "Language", "Name" },
                values: new object[,]
                {
                    { 9, 5, "Devices designed for physical therapy and rehabilitation.", true, "en-us", "Physical Therapy Devices" },
                    { 10, 5, "أجهزة مصممة للعلاج الطبيعي وإعادة التأهيل.", false, "ar-eg", "أجهزة العلاج الطبيعي" }
                });

            migrationBuilder.InsertData(
                table: "ProductTranslations",
                columns: new[] { "Id", "Description", "IsDefault", "Language", "Name", "ProductId" },
                values: new object[,]
                {
                    { 1, "General Purpose: Intended for bombing from adequate aircraft of NATO and Russian standards and from the plane of light aviation under specific conditions.", true, "en-US", "HAFEZ FAMILY", 1 },
                    { 2, "مصممة للقصف من طائرات الناتو وروسيا أو الطائرات الخفيفة في ظل ظروف معينة.", false, "ar-eg", "عائلة حافظ", 1 },
                    { 3, "A solid-state fuse with a powerful microcontroller for air-to-surface aerial bombs.", true, "en-US", "HAFEZ 4 (AIR TO SURFACE FUZES)", 2 },
                    { 4, "صمام صلب مع متحكم دقيق قوي للقنابل الجوية جو-أرض.", false, "ar-eg", "حافظ 4 (طابة جو/أرض)", 2 },
                    { 5, "Ensures a reliable power supply for trains, meeting high safety and quality standards in the railway industry.", true, "en-US", "Power Coaches", 3 },
                    { 6, "توفر إمدادًا موثوقًا بالطاقة للقطارات مع الالتزام بأعلى معايير السلامة والجودة في صناعة السكك الحديدية.", false, "ar-eg", "عربات الطاقة", 3 },
                    { 7, "Ideal for safe and efficient transportation of liquids, constructed with stainless steel for high cleanliness.", true, "en-US", "Tank Wagon", 4 },
                    { 8, "مثالية لنقل السوائل بأمان وكفاءة، مصنوعة من الفولاذ المقاوم للصدأ لضمان نظافة عالية.", false, "ar-eg", "عربة خزان", 4 },
                    { 9, "Compliant with Egyptian and European safety standards for baby incubators.", true, "en-US", "Infant Incubator", 5 },
                    { 10, "متوافقة مع المواصفات المصرية والأوروبية لأجهزة حاضنات الأطفال.", false, "ar-eg", "حاضنة أطفال", 5 },
                    { 11, "Designed for upper limb training of wheelchair users, with customizable settings and performance tracking.", true, "en-US", "Multifunctional Training Device", 6 },
                    { 12, "مصمم لتدريب الأطراف العلوية لمستخدمي الكراسي المتحركة مع إعدادات قابلة للتخصيص ومتابعة الأداء.", false, "ar-eg", "جهاز تدريب متعدد الوظائف", 6 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "DefaultDescription", "DefaultName", "Price", "Rate", "Stock", "SubCategoryId" },
                values: new object[] { 7, 4, null, null, 0.0, 5, 0, 5 });

            migrationBuilder.InsertData(
                table: "ProductTranslations",
                columns: new[] { "Id", "Description", "IsDefault", "Language", "Name", "ProductId" },
                values: new object[,]
                {
                    { 13, "Devices designed for physical therapy and rehabilitation.", true, "en-US", "Physical Therapy Devices", 7 },
                    { 14, "أجهزة مصممة للعلاج الطبيعي وإعادة التأهيل.", false, "ar-eg", "أجهزة العلاج الطبيعي", 7 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CategoryTranslaltiosns",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CategoryTranslaltiosns",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CategoryTranslaltiosns",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CategoryTranslaltiosns",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CategoryTranslaltiosns",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "CategoryTranslaltiosns",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "CategoryTranslaltiosns",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "CategoryTranslaltiosns",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "CategoryTranslaltiosns",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "CategoryTranslaltiosns",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
