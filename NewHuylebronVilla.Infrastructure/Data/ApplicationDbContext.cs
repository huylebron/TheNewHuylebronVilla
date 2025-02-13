using Microsoft . AspNetCore . Identity . EntityFrameworkCore ;
using Microsoft . EntityFrameworkCore ;
using NewHuylebronVilla . Domain . Entities ;

namespace NewHuylebronVilla . Infrastructure . Data
{
    public class ApplicationDbContext : IdentityDbContext < ApplicationUser >
    {
        public ApplicationDbContext ( DbContextOptions < ApplicationDbContext > options ) : base ( options ) {
        }

        public DbSet < Villa > Villas { get ; set ; }
        public DbSet < VillaNumber > VillaNumbers { get ; set ; }

        public DbSet < Amenity > Amenities { get ; set ; }

        public DbSet < ApplicationUser > ApplicationUsers { get ; set ; }

        public DbSet < Booking > Bookings { get ; set ; }


        protected override void OnModelCreating ( ModelBuilder modelBuilder ) {
            base . OnModelCreating ( modelBuilder ) ;

            
             modelBuilder.Entity<Villa>().HasData(
     new Villa
     {
         Id = 1,
         Name = "Biệt Thự Hoàng Gia",
         Description =
             "Một không gian sang trọng với thiết kế đẳng cấp, mang lại sự thoải mái và tiện nghi tối đa. Hoàn hảo cho kỳ nghỉ thư giãn và tận hưởng không gian yên bình.",
         ImageUrl = "https://placehold.co/600x400",
         Occupancy = 4,
         Price = 250,
         Sqft = 600,
     },
     new Villa
     {
         Id = 2,
         Name = "Biệt Thự Hồ Bơi Cao Cấp",
         Description =
             "Trải nghiệm đẳng cấp với hồ bơi riêng, không gian rộng rãi và đầy đủ tiện nghi. Lựa chọn hoàn hảo cho gia đình hoặc nhóm bạn cùng tận hưởng kỳ nghỉ lý tưởng.",
         ImageUrl = "https://placehold.co/600x401",
         Occupancy = 4,
         Price = 350,
         Sqft = 700,
     },
     new Villa
     {
         Id = 3,
         Name = "Biệt Thự Hồ Bơi Sang Trọng",
         Description =
             "Không gian nghỉ dưỡng tuyệt vời với bể bơi riêng, ban công hướng biển và nội thất hiện đại. Đem đến trải nghiệm xa hoa và thư giãn tuyệt đối.",
         ImageUrl = "https://placehold.co/600x402",
         Occupancy = 4,
         Price = 500,
         Sqft = 900,
     },
     new Villa
     {
         Id = 4,
         Name = "Biệt Thự Ven Biển",
         Description =
             "Thiết kế mở đón ánh nắng tự nhiên, tầm nhìn tuyệt đẹp ra biển xanh. Lý tưởng cho kỳ nghỉ trọn vẹn bên gia đình và bạn bè.",
         ImageUrl = "https://placehold.co/600x403",
         Occupancy = 6,
         Price = 600,
         Sqft = 1000,
     },
     new Villa
     {
         Id = 5,
         Name = "Biệt Thự Nhiệt Đới",
         Description =
             "Một không gian xanh mát giữa thiên nhiên, kết hợp hồ bơi và khu vườn nhiệt đới riêng tư.",
         ImageUrl = "https://placehold.co/600x404",
         Occupancy = 5,
         Price = 450,
         Sqft = 800,
     },
     new Villa
     {
         Id = 6,
         Name = "Biệt Thự Cao Nguyên",
         Description =
             "Nằm giữa núi đồi hùng vĩ, căn biệt thự mang lại không gian nghỉ dưỡng yên bình với không khí trong lành.",
         ImageUrl = "https://placehold.co/600x405",
         Occupancy = 4,
         Price = 380,
         Sqft = 750,
     },
     new Villa
     {
         Id = 7,
         Name = "Biệt Thự Thiên Đường",
         Description =
             "Hòa mình vào thiên nhiên với hồ bơi vô cực và nội thất hiện đại, sang trọng bậc nhất.",
         ImageUrl = "https://placehold.co/600x406",
         Occupancy = 6,
         Price = 750,
         Sqft = 1200,
     },
     new Villa
     {
         Id = 8,
         Name = "Biệt Thự Mặt Hồ",
         Description =
             "Biệt thự nằm ngay cạnh hồ lớn, không gian thoáng mát với khu vực thư giãn ngoài trời tuyệt đẹp.",
         ImageUrl = "https://placehold.co/600x407",
         Occupancy = 5,
         Price = 500,
         Sqft = 900,
     }
 );

 modelBuilder.Entity<VillaNumber>().HasData(
     new VillaNumber { Villa_Number = 101, VillaId = 1 },
     new VillaNumber { Villa_Number = 102, VillaId = 1 },
     new VillaNumber { Villa_Number = 103, VillaId = 1 },
     new VillaNumber { Villa_Number = 104, VillaId = 1 },
     new VillaNumber { Villa_Number = 201, VillaId = 2 },
     new VillaNumber { Villa_Number = 202, VillaId = 2 },
     new VillaNumber { Villa_Number = 203, VillaId = 2 },
     new VillaNumber { Villa_Number = 301, VillaId = 3 },
     new VillaNumber { Villa_Number = 302, VillaId = 3 },
     new VillaNumber { Villa_Number = 401, VillaId = 4 },
     new VillaNumber { Villa_Number = 402, VillaId = 4 },
     new VillaNumber { Villa_Number = 501, VillaId = 5 },
     new VillaNumber { Villa_Number = 502, VillaId = 5 },
     new VillaNumber { Villa_Number = 601, VillaId = 6 },
     new VillaNumber { Villa_Number = 602, VillaId = 6 },
     new VillaNumber { Villa_Number = 701, VillaId = 7 },
     new VillaNumber { Villa_Number = 702, VillaId = 7 },
     new VillaNumber { Villa_Number = 801, VillaId = 8 },
     new VillaNumber { Villa_Number = 802, VillaId = 8 }
 );

 modelBuilder.Entity<Amenity>().HasData(
     new Amenity { Id = 1, VillaId = 1, Name = "Hồ bơi riêng" },
     new Amenity { Id = 2, VillaId = 1, Name = "Lò vi sóng" },
     new Amenity { Id = 3, VillaId = 1, Name = "Ban công riêng" },
     new Amenity { Id = 4, VillaId = 1, Name = "1 giường King và 1 ghế sofa giường" },
     new Amenity { Id = 5, VillaId = 2, Name = "Hồ bơi riêng biệt" },
     new Amenity { Id = 6, VillaId = 2, Name = "Lò vi sóng & tủ lạnh mini" },
     new Amenity { Id = 7, VillaId = 2, Name = "Ban công riêng hướng vườn" },
     new Amenity { Id = 8, VillaId = 2, Name = "1 giường King hoặc 2 giường đôi" },
     new Amenity { Id = 9, VillaId = 3, Name = "Hồ bơi vô cực" },
     new Amenity { Id = 10, VillaId = 3, Name = "Bồn tắm Jacuzzi" },
     new Amenity { Id = 11, VillaId = 3, Name = "Ban công riêng hướng biển" },
     new Amenity { Id = 12, VillaId = 4, Name = "Phòng tắm lộ thiên" },
     new Amenity { Id = 13, VillaId = 4, Name = "Quầy bar mini" },
     new Amenity { Id = 14, VillaId = 5, Name = "Sân vườn rộng rãi" },
     new Amenity { Id = 15, VillaId = 5, Name = "Khu vực BBQ ngoài trời" },
     new Amenity { Id = 16, VillaId = 6, Name = "Lò sưởi" },
     new Amenity { Id = 17, VillaId = 6, Name = "Ghế massage thư giãn" },
     new Amenity { Id = 18, VillaId = 7, Name = "Rạp chiếu phim riêng" },
     new Amenity { Id = 19, VillaId = 7, Name = "Bể sục nước nóng ngoài trời" },
     new Amenity { Id = 20, VillaId = 8, Name = "Thuyền kayak riêng" },
     new Amenity { Id = 21, VillaId = 8, Name = "Ghế dài tắm nắng cạnh hồ" }
 );
         

        }
    }
}