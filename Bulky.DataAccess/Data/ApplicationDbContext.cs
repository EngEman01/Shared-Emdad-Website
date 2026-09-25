using StyleHub.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using StyleHub.DataAccess;
using StyleHub.Utility;


namespace StyleHub.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppUser> Users { get; set; }

        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<Event> Events { get; set; }

        // Translations
        public DbSet<CategoryTranslation> CategoryTranslaltiosns { get; set; }
        public DbSet<ProductTranslation> ProductTranslations { get; set; }
        public DbSet<AdTranslation> AdTranslations { get; set; }
        public DbSet<EventTranslation> EventTranslations { get; set; }

        // Images
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<AdImage> AdImages { get; set; }
        public DbSet<EventImage> EventImages { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);         // this is to solve the primary key error


            // Relationships ==============================
            // Product -----------------------------------
            // Product -> Category

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // Product -> Images

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Images)
                .WithOne(pi => pi.Product)
                .HasForeignKey(pi => pi.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // Product -> ProductTranslation
            modelBuilder.Entity<Product>()
                .HasMany(p => p.ProductTranslations)
                .WithOne(pt => pt.Product)
                .HasForeignKey(pt => pt.ProductId)
                .OnDelete(DeleteBehavior.Cascade);


            // Category -----------------------------------
            // Category -> SubCategory
            modelBuilder.Entity<Category>()
                .HasMany(c => c.SubCategories)
                .WithOne(sc => sc.ParentCategory)
                .HasForeignKey(sc => sc.ParentCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull);


            // Categoroy -> Parent Category
            modelBuilder.Entity<Category>()
                .HasOne(c => c.ParentCategory)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull);


            // Category -> Product
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);


            // CategoryTranslation
            modelBuilder.Entity<Category>()
                .HasMany(c => c.CategoryTranslations)
                .WithOne(ct => ct.Category)
                .HasForeignKey(ct => ct.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            

            // Advertisement -----------------------------------
            // Images
            modelBuilder.Entity<Advertisement>()
                .HasMany(ad => ad.Images)
                .WithOne(i => i.Advertisement)
                .HasForeignKey(i => i.AdvertisementId)
                .OnDelete(DeleteBehavior.Cascade);



            // Translations
            modelBuilder.Entity<Advertisement>()
                .HasMany(ad => ad.Translations)
                .WithOne(at => at.Advertisement)
                .HasForeignKey(at => at.AdvertisementId)
                .OnDelete(DeleteBehavior.Cascade);


            // Translations
            // Category Translation -----------------------------------


            modelBuilder.Entity<CategoryTranslation>()
                .HasIndex(ct => new { ct.CategoryId, ct.Language })
                .IsUnique(true);

            // Product Translation -----------------------------------
            modelBuilder.Entity<ProductTranslation>()
                .HasIndex(ct => new { ct.ProductId, ct.Language })
                .IsUnique(true);


            // Ad. Translation -----------------------------------
            modelBuilder.Entity<AdTranslation>()
                .HasIndex(ct => new { ct.AdvertisementId, ct.Language })
                .IsUnique(true);



            // Seed Roles
            modelBuilder.Entity<IdentityRole>().HasData(
                SD.UserRole,
                SD.AdminRole
                );

            //Seed Users

            // Default Users
            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(
                new AppUser
                {
                    Id = "1",
                    Email = "info@eltamawiya-emdad.com",
                    NormalizedEmail = "INFO@ELTAMAWIYA-EMDAD.COM",
                    PasswordHash = hasher.HashPassword(null, "Emdad@admin*Main1234"),
                    UserName = "Admin Emdad",
                    NormalizedUserName = "ADMIN EMDAD"
                },
                new AppUser
                {
                    Id = "2",
                    Email = "emdadindustries@eltamawiya-emdad.com",
                    NormalizedEmail = "EMDADINDUSTRIES@ELTAMAWIYA-EMDAD.COM",
                    PasswordHash = hasher.HashPassword(null, "Emdad@user*Main1234"),
                    UserName = "User Emdad",
                    NormalizedUserName = "USER EMDAD"
                }

                );

            // Seed UserRoles = Add Roles to Users
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = "1",
                    RoleId = "1",
                },
                new IdentityUserRole<string>
                {
                    UserId = "2",
                    RoleId = "2",
                });






            ////// Seed Categories with Translations
            //modelBuilder.Entity<Category>().HasData(
            //    new Category
            //    {
            //        Id = 1,
            //        //DefaultName = "Defence Industries",
            //        ImageUrl = "images\\categories\\defence_industries.jpg"
            //    },
            //    new Category
            //    {
            //        Id = 2,
            //        //DefaultName = "Civil Industries",
            //        ImageUrl = "images\\categories\\civil_industries.jpg"
            //    },
            //    new Category
            //    {
            //        Id = 3,
            //        //DefaultName = "Medical Equipment",
            //        ImageUrl = "images\\categories\\medical_equipment.jpg"
            //    },
            //    new Category
            //    {
            //        Id = 4,
            //        //DefaultName = "Sports Equipment",
            //        ImageUrl = "images\\categories\\sports_equipment.jpg"
            //    },
            //    new Category
            //    {
            //        Id = 5,  // Subcategory Id
            //        //DefaultName = "Physical Therapy Devices",
            //        ImageUrl = "images\\categories\\physical_therapy_devices.jpg",
            //        ParentCategoryId = 4  // Set the parent category to Sports Equipment (Id 4)
            //    }
            //);

            //// Seed CategoryTranslations
            //modelBuilder.Entity<CategoryTranslation>().HasData(
            //    new CategoryTranslation
            //    {
            //        Id = 1,
            //        Language = "en-us",
            //        Name = "Defence Industries",
            //        Description = "Industries related to defense and military.",
            //        CategoryId = 1
            //    },
            //    new CategoryTranslation
            //    {
            //        Id = 2,
            //        Language = "ar-eg",
            //        Name = "الصناعات الدفاعية",
            //        Description = "صناعات متعلقة بالدفاع والعسكرية.",
            //        CategoryId = 1
            //    },
            //    new CategoryTranslation
            //    {
            //        Id = 3,
            //        Language = "en-us",
            //        Name = "Civil Industries",
            //        Description = "Industries for civilian applications.",
            //        CategoryId = 2
            //    },
            //    new CategoryTranslation
            //    {
            //        Id = 4,
            //        Language = "ar-eg",
            //        Name = "الصناعات المدنية",
            //        Description = "صناعات للاستخدامات المدنية.",
            //        CategoryId = 2
            //    },
            //    new CategoryTranslation
            //    {
            //        Id = 5,
            //        Language = "en-us",
            //        Name = "Medical Equipment",
            //        Description = "Equipment used in healthcare and medical fields.",
            //        CategoryId = 3
            //    },
            //    new CategoryTranslation
            //    {
            //        Id = 6,
            //        Language = "ar-eg",
            //        Name = "المعدات الطبية",
            //        Description = "المعدات المستخدمة في مجالات الرعاية الصحية والطبية.",
            //        CategoryId = 3
            //    },
            //    new CategoryTranslation
            //    {
            //        Id = 7,
            //        Language = "en-us",
            //        Name = "Sports Equipment",
            //        Description = "Equipment for sports and fitness.",
            //        CategoryId = 4
            //    },
            //    new CategoryTranslation
            //    {
            //        Id = 8,
            //        Language = "ar-eg",
            //        Name = "معدات رياضية",
            //        Description = "معدات الرياضة واللياقة البدنية.",
            //        CategoryId = 4
            //    },
            //    new CategoryTranslation
            //    {
            //        Id = 9,
            //        Language = "en-us",
            //        Name = "Physical Therapy Devices",
            //        Description = "Devices designed for physical therapy and rehabilitation.",
            //        CategoryId = 5
            //    },
            //    new CategoryTranslation
            //    {
            //        Id = 10,
            //        Language = "ar-eg",
            //        Name = "أجهزة العلاج الطبيعي",
            //        Description = "أجهزة مصممة للعلاج الطبيعي وإعادة التأهيل.",
            //        CategoryId = 5
            //    }
            //);

            //// Seed Products
            //modelBuilder.Entity<Product>().HasData(
            //    new Product
            //    {
            //        Id = 1,
            //        CategoryId = 1
            //    },
            //    new Product
            //    {
            //        Id = 2,
            //        CategoryId = 1
            //    },
            //    new Product
            //    {
            //        Id = 3,
            //        CategoryId = 2
            //    },
            //    new Product
            //    {
            //        Id = 4,
            //        CategoryId = 2
            //    },
            //    new Product
            //    {
            //        Id = 5,
            //        CategoryId = 3
            //    },
            //    new Product
            //    {
            //        Id = 6,
            //        CategoryId = 4
            //    },
            //    new Product
            //    {
            //        Id = 7,
            //        CategoryId = 5,

            //    }
            //);

            //// Seed ProductTranslations
            //modelBuilder.Entity<ProductTranslation>().HasData(
            //    new ProductTranslation
            //    {
            //        Id = 1,
            //        Language = "en-US",
            //        Name = "HAFEZ FAMILY",
            //        Description = "General Purpose: Intended for bombing from adequate aircraft of NATO and Russian standards and from the plane of light aviation under specific conditions.",
            //        ProductId = 1
            //    },
            //    new ProductTranslation
            //    {
            //        Id = 2,
            //        Language = "ar-eg",
            //        Name = "عائلة حافظ",
            //        Description = "مصممة للقصف من طائرات الناتو وروسيا أو الطائرات الخفيفة في ظل ظروف معينة.",
            //        ProductId = 1
            //    },
            //    new ProductTranslation
            //    {
            //        Id = 3,
            //        Language = "en-US",
            //        Name = "HAFEZ 4 (AIR TO SURFACE FUZES)",
            //        Description = "A solid-state fuse with a powerful microcontroller for air-to-surface aerial bombs.",
            //        ProductId = 2
            //    },
            //    new ProductTranslation
            //    {
            //        Id = 4,
            //        Language = "ar-eg",
            //        Name = "حافظ 4 (طابة جو/أرض)",
            //        Description = "صمام صلب مع متحكم دقيق قوي للقنابل الجوية جو-أرض.",
            //        ProductId = 2
            //    },
            //    new ProductTranslation
            //    {
            //        Id = 5,
            //        Language = "en-US",
            //        Name = "Power Coaches",
            //        Description = "Ensures a reliable power supply for trains, meeting high safety and quality standards in the railway industry.",
            //        ProductId = 3
            //    },
            //    new ProductTranslation
            //    {
            //        Id = 6,
            //        Language = "ar-eg",
            //        Name = "عربات الطاقة",
            //        Description = "توفر إمدادًا موثوقًا بالطاقة للقطارات مع الالتزام بأعلى معايير السلامة والجودة في صناعة السكك الحديدية.",
            //        ProductId = 3
            //    },
            //    new ProductTranslation
            //    {
            //        Id = 7,
            //        Language = "en-US",
            //        Name = "Tank Wagon",
            //        Description = "Ideal for safe and efficient transportation of liquids, constructed with stainless steel for high cleanliness.",
            //        ProductId = 4
            //    },
            //    new ProductTranslation
            //    {
            //        Id = 8,
            //        Language = "ar-eg",
            //        Name = "عربة خزان",
            //        Description = "مثالية لنقل السوائل بأمان وكفاءة، مصنوعة من الفولاذ المقاوم للصدأ لضمان نظافة عالية.",
            //        ProductId = 4
            //    },
            //    new ProductTranslation
            //    {
            //        Id = 9,
            //        Language = "en-US",
            //        Name = "Infant Incubator",
            //        Description = "Compliant with Egyptian and European safety standards for baby incubators.",
            //        ProductId = 5
            //    },
            //    new ProductTranslation
            //    {
            //        Id = 10,
            //        Language = "ar-eg",
            //        Name = "حاضنة أطفال",
            //        Description = "متوافقة مع المواصفات المصرية والأوروبية لأجهزة حاضنات الأطفال.",
            //        ProductId = 5
            //    },
            //    new ProductTranslation
            //    {
            //        Id = 11,
            //        Language = "en-US",
            //        Name = "Multifunctional Training Device",
            //        Description = "Designed for upper limb training of wheelchair users, with customizable settings and performance tracking.",
            //        ProductId = 6

            //    },
            //    new ProductTranslation
            //    {
            //        Id = 12,
            //        Language = "ar-eg",
            //        Name = "جهاز تدريب متعدد الوظائف",
            //        Description = "مصمم لتدريب الأطراف العلوية لمستخدمي الكراسي المتحركة مع إعدادات قابلة للتخصيص ومتابعة الأداء.",
            //        ProductId = 6
            //    },
            //    new ProductTranslation
            //    {
            //        Id = 13,
            //        Language = "en-US",
            //        Name = "Physical Therapy Devices",
            //        Description = "Devices designed for physical therapy and rehabilitation.",
            //        ProductId = 7
            //    },
            //    new ProductTranslation
            //    {
            //        Id = 14,
            //        Language = "ar-eg",
            //        Name = "أجهزة العلاج الطبيعي",
            //        Description = "أجهزة مصممة للعلاج الطبيعي وإعادة التأهيل.",
            //        ProductId = 7
            //    }
            //);



        }
        
    }
}
