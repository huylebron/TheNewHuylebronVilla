using NewHuylebronVilla . Application . Common . Interface ;
using NewHuylebronVilla . Domain . Entities ;
using NewHuylebronVilla . Infrastructure . Data ;

namespace NewHuylebronVilla.Infrastructure.Repository ;

public class BookingRepository : Repository<Booking>, IBookingRepository

{
    private readonly ApplicationDbContext _db;
    public BookingRepository(ApplicationDbContext db) : base(db) 
    {
        _db = db;
    }
     
    public void Update(Booking entity)
    {
        _db.Bookings.Update(entity);
    }
    
    
    
}