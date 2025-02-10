using NewHuylebronVilla . Domain . Entities ;

namespace NewHuylebronVilla.Application.Common.Interface ;

public interface IBookingRepository : IRepository<Booking>
{
    void Update(Booking entity); 
    
}