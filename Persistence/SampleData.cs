using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class SampleData
    {
        public static void InitializeAsync(ModelBuilder modelBuilder)
        {
            //Seeding a  'Administrator' role to AspNetRoles table
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole {Id = "2c5e174e-3b0e-446f-86af-483d56fd7210", Name = "Administrator", NormalizedName = "ADMINISTRATOR".ToUpper() });


            //a hasher to hash the password before seeding the user to the db
            var hasher = new PasswordHasher<IdentityUser>();


            //Seeding the User to AspNetUsers table
            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = "8e445865-a24d-4543-a6c6-9443d048cdb9", // primary key
                    UserName = "myuser",
                    NormalizedUserName = "MYUSER",
                    PasswordHash = hasher.HashPassword(null, "Pa$$w0rd")
                }
            );


            //Seeding the relation between our user and role to AspNetUserRoles table
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210", 
                    UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
                }
            );
            
            // Seeding Category event
            SeedCategories(modelBuilder);
            SeedEvents(modelBuilder);
        }

        private static void SeedCategories(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryEvents>().HasData(
                new CategoryEvents
                {
                    Id = 1,
                    Name = "Business & Seminars",
                    Slug = "business-&-seminars",
                    Description = "abc",
                    ThumbnailUrl = "categories/business&seminars.webp",
                    Status = true
                },
                new CategoryEvents
                {
                    Id = 2,
                    Name = "Yoga & Health",
                    Slug = "yoga-&-health",
                    Description = "abc",
                    ThumbnailUrl = "categories/yoga&health.webp",
                    Status = true
                },
                new CategoryEvents
                {
                    Id = 3,
                    Name = "Sports & Fitness",
                    Slug = "sports-&-fitness",
                    Description = "abc",
                    ThumbnailUrl = "categories/sports&fitness.webp",
                    Status = true
                },
                new CategoryEvents
                {
                    Id = 4,
                    Name = "Music & Concerts",
                    Slug = "music-&-concerts",
                    Description = "abc",
                    ThumbnailUrl = "/categories/music&concerts.webp",
                    Status = true
                },
                new CategoryEvents
                {
                    Id = 5,
                    Name = "Science & Tech",
                    Slug = "science-&-tech",
                    Description = "abcd",
                    ThumbnailUrl = "categories/science&tech.webp",
                    Status = true
                }
            );
        }

        private static void SeedEvents(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Events>().HasData(
                new Events
                {
                    Id = 1,
                    Slug = "lululola-show-ha-nhi-toi-cho-em",
                    OrganizerId = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                    CategoryId = 4,
                    Title = "LULULOLA SHOW HÀ NHI | TỘI CHO EM",
                    Description = "Liveshow Hà Nhi | TỘI CHO EM\nLululola Show - Hơn cả âm nhạc, không gian lãng mạn đậm chất thơ Đà Lạt bao trọn hình ảnh thung lũng Đà Lạt, được ngắm nhìn khoảng khắc hoàng hôn thơ mộng đến khi Đà Lạt về đêm siêu lãng mạn, được giao lưu với thần tượng một cách chân thật và gần gũi nhất trong không gian ấm áp và không khí se lạnh của Đà Lạt. Tất cả sẽ  mang đến một đêm nhạc ấn tượng mà bạn không thể quên khi đến với Đà Lạt.",
                    IsPublic = true,
                    StartDate = new DateTime(2024, 12, 27),
                    EndDate = new DateTime(2024, 12, 27),
                    StartTime = new TimeSpan(17, 30, 0),
                    EndTime = new TimeSpan(19, 30, 0),
                    VenueName = "lululola coffee",
                    City = "Tp. Đà Lạt",
                    District = "",
                    Ward = "Phường 3",
                    Address = "Đường 3/4",
                    PostalCode = "",
                    Latitude = 11.919983199999999m,
                    Longitude = 108.4421036m,
                    PhoneNumber = "",
                    WebsiteUrl = "",
                    ThumbnailUrl = "/images/events/hanhitoichoem.png",
                    CoverUrl = "/images/events/hanhitoichoem.png"
                },
                new Events
    {
                Id = 2,
                Slug = "yoga-for-beginners",
                OrganizerId = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                CategoryId = 2, // Yoga & Health
                Title = "Yoga for Beginners",
                Description = "A gentle introduction to yoga.",
                IsPublic = true,
                StartDate = new DateTime(2024, 4, 10),
                EndDate = new DateTime(2024, 4, 10),
                StartTime = new TimeSpan(10, 0, 0),
                EndTime = new TimeSpan(12, 0, 0),
                VenueName = "Zen Yoga Studio",
                City = "Đà Nẵng",
                District = "Hải Châu",
                Ward = "",
                Address = "456 Đường Yên Bái, Đà Nẵng",
                PostalCode = "550000",
                Latitude = 16.0678m,
                Longitude = 108.2208m,
                PhoneNumber = "0987654321",
                WebsiteUrl = "https://example.com/yoga-for-beginners",
                ThumbnailUrl = "/images/events/yoga-for-beginners.jpg",
                CoverUrl = "/images/events/yoga-for-beginners-cover.jpg"
            },
            new Events
            {
                Id = 3,
                Slug = "marathon-training",
                OrganizerId = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                CategoryId = 3, // Sports & Fitness
                Title = "Marathon Training Program",
                Description = "Get ready for your next marathon with our expert trainers.",
                IsPublic = true,
                StartDate = new DateTime(2024, 5, 1),
                EndDate = new DateTime(2024, 5, 1),
                StartTime = new TimeSpan(6, 0, 0),
                EndTime = new TimeSpan(9, 0, 0),
                VenueName = "City Sports Complex",
                City = "Hồ Chí Minh",
                District = "Quận 1",
                Ward = "",
                Address = "789 Đường Lê Duẩn, Hồ Chí Minh",
                PostalCode = "700000",
                Latitude = 10.7769m,
                Longitude = 106.6955m,
                PhoneNumber = "0123456789",
                WebsiteUrl = "https://example.com/marathon-training",
                ThumbnailUrl = "/images/events/marathon-training.jpg",
                CoverUrl = "/images/events/marathon-training-cover.jpg"
            },
            new Events
            {
                Id = 4,
                Slug = "live-music-concert",
                OrganizerId = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                CategoryId = 4, // Music & Concerts
                Title = "Live Music Concert",
                Description = "Enjoy live music from up-and-coming artists.",
                IsPublic = true,
                StartDate = new DateTime(2024, 6, 15),
                EndDate = new DateTime(2024, 6, 15),
                StartTime = new TimeSpan(19, 0, 0),
                EndTime = new TimeSpan(22, 0, 0),
                VenueName = "Main City Park",
                City = "Hà Nội",
                District = "Đống Đa",
                Ward = "",
                Address = "101 Đường Nguyễn Thái Học, Hà Nội",
                PostalCode = "100000",
                Latitude = 21.0202m,
                Longitude = 105.7692m,
                PhoneNumber = "0123456789",
                WebsiteUrl = "https://example.com/live-music-concert",
                ThumbnailUrl = "/images/events/live-music-concert.jpg",
                CoverUrl = "/images/events/live-music-concert-cover.jpg"
            },
            new Events
            {
               Id=5,
               Slug="tech-innovation-summit", 
               OrganizerId="8e445865-a24d-4543-a6c6-9443d048cdb9", 
               CategoryId=5, // Science & Tech
               Title="Tech Innovation Summit", 
               Description="Explore the latest innovations in technology.", 
               IsPublic=true,
               StartDate=new DateTime(2024, 7, 20), 
               EndDate=new DateTime(2024, 7, 21), 
               StartTime=new TimeSpan(9,0,0), 
               EndTime=new TimeSpan(17,0,0), 
               VenueName="Tech Convention Center", 
               City="TP. Hồ Chí Minh", 
               District="Quận 1", 
               Ward="", 
               Address="456 Đường Nguyễn Thị Minh Khai, TP. Hồ Chí Minh", 
               PostalCode="700000", 
               Latitude=10.7769m,
               Longitude=106.6955m,
               PhoneNumber="0123456789", 
               WebsiteUrl="https://example.com/tech-innovation-summit", 
               ThumbnailUrl="/images/events/tech-innovation-summit.jpg", 
               CoverUrl="/images/events/tech-innovation-summit-cover.jpg"
            },
            new Events
            {
               Id=6,
               Slug="business-leadership-forum", 
               OrganizerId="8e445865-a24d-4543-a6c6-9443d048cdb9", 
               CategoryId=1, // Business & Seminars
               Title="Business Leadership Forum", 
               Description="A forum for business leaders to share insights.", 
               IsPublic=true,
               StartDate=new DateTime(2024, 8, 5), 
               EndDate=new DateTime(2024, 8, 5), 
               StartTime=new TimeSpan(8,0,0), 
               EndTime=new TimeSpan(15,0,0), 
               VenueName="Royal Business Hotel", 
               City="Hà Nội", 
               District="Hoàn Kiếm", 
               Ward="", 
               Address="123 Đường Tràng Tiền, Hà Nội", 
               PostalCode="100000", 
               Latitude=21.0285m,
               Longitude=105.8040m,
               PhoneNumber="0123456789", 
               WebsiteUrl="https://example.com/business-leadership-forum", 
               ThumbnailUrl="/images/events/business-leadership-forum.jpg", 
               CoverUrl="/images/events/business-leadership-forum-cover.jpg"
            },
            new Events
            {
               Id=7,
               Slug="wellness-weekend", 
               OrganizerId="8e445865-a24d-4543-a6c6-9443d048cdb9", 
               CategoryId=2,// Yoga & Health
               Title="Wellness Weekend Getaway", 
               Description="A weekend focused on wellness and relaxation.", 
               IsPublic=true,
               StartDate=new DateTime(2024, 9, 15), 
               EndDate=new DateTime(2024, 9, 17), 
               StartTime=new TimeSpan(10,0,0), 
               EndTime=new TimeSpan(16,0,0), 
               VenueName="Serenity Spa Resort", 
               City="Nha Trang", 
               District="", 
               Ward="", 
               Address="456 Đường Trần Phú, Nha Trang", 
               PostalCode="", 
               Latitude=12.2385m,
               Longitude=109.1967m,
               PhoneNumber="", 
               WebsiteUrl="", 
               ThumbnailUrl="/images/events/wellness-weekend.jpg", 
               CoverUrl="/images/events/wellness-weekend-cover.jpg"
            },
            new Events
            {
              Id=8,
              Slug="sports-competition",  
              OrganizerId="8e445865-a24d-4543-a6c6-9443d048cdb9",  
              CategoryId=3,// Sports & Fitness
              Title="City Sports Competition",  
              Description="Compete with other athletes in various sports.",  
              IsPublic=true,
              StartDate=new DateTime(2024, 10, 1),  
              EndDate=new DateTime(2024, 10, 2),  
              StartTime=new TimeSpan(8,0,0),  
              EndTime=new TimeSpan(17,0,0),  
              VenueName="City Stadium",  
              City="Hồ Chí Minh",  
              District="",  
              Ward="",  
              Address="789 Đường Lê Duẩn, Hồ Chí Minh",  
              PostalCode="",  
              Latitude=10.7769m,
              Longitude=106.6955m,
              PhoneNumber="",  
              WebsiteUrl="",  
              ThumbnailUrl="/images/events/sports-competition.jpg",  
              CoverUrl="/images/events/sports-competition-cover.jpg"  
            },
            new Events
            {
              Id=9,
              Slug="music-performance",  
              OrganizerId="8e445865-a24d-4543-a6c6-9443d048cdb9",  
              CategoryId=4,// Music & Concerts
              Title="Evening Music Performance",  
              Description="An evening of beautiful music performances.",  
              IsPublic=true,
              StartDate=new DateTime(2024, 11, 20),  
              EndDate=new DateTime(2024, 11, 20),  
              StartTime=new TimeSpan(18,0,0),  
              EndTime=new TimeSpan(21,0,0),  
              VenueName="Musical Theatre",  
              City="Đà Nẵng",  
              District="",  
              Ward="",  
              Address="101 Đường Hoàng Sa, Đà Nẵng",  
              PostalCode="",  
              Latitude=16.0678m,
              Longitude=108.2208m,
              PhoneNumber="",  
              WebsiteUrl="",  
              ThumbnailUrl="/images/events/music-performance.jpg",  
              CoverUrl="/images/events/music-performance-cover.jpg"  
            },
            new Events
            {
              Id=10,
              Slug="science-discovery-day",  
              OrganizerId="8e445865-a24d-4543-a6c6-9443d048cdb9",  
              CategoryId=5,// Science & Tech
              Title="Science Discovery Day",  
              Description="A day of science experiments and discoveries.",  
              IsPublic=true,
              StartDate=new DateTime(2024, 12, 5),  
              EndDate=new DateTime(2024, 12, 5),  
              StartTime=new TimeSpan(9,0,0),  
              EndTime=new TimeSpan(16,0,0),  
              VenueName="Science Museum",  
              City="Hà Nội",  
              District="",  
              Ward="",  
              Address="456 Đường Trần Quốc Toản, Hà Nội",  
              PostalCode="",  
              Latitude=21.0285m,
              Longitude=105.8040m,
              PhoneNumber="",  
              WebsiteUrl="",  
              ThumbnailUrl="/images/events/science-discovery-day.jpg",  
              CoverUrl="/images/events/science-discovery-day-cover.jpg"  
            }
                );
        }
    }
}