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
     new VillaNumber { Villa_Number = 302, VillaId = 3 }
    
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
     new Amenity { Id = 11, VillaId = 3, Name = "Ban công riêng hướng biển" }
    
 );
         

        }
    }
}