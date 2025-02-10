using NewHuylebronVilla . Domain . Entities ;

namespace NewHuylebronVilla.Application.Common.Interface ;

public interface IBookingRepository : IRepository<Booking>
{
    void Update(Booking entity); 
    void UpdateStatus(int bookingId, string bookingStatus);
    void UpdateStripePaymentID(int bookingId, string sessionId, string paymentIntentId);
    
}