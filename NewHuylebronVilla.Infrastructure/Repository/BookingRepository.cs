﻿using NewHuylebronVilla . Application . Common . Division ;
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
    public void UpdateStatus(int bookingId, string bookingStatus , int villaNumber = 0)
    {
        var bookingFromDb = _db.Bookings.FirstOrDefault(m => m.Id == bookingId);
        if (bookingFromDb != null)
        {
            bookingFromDb.Status = bookingStatus;
            if (bookingStatus == Claim.StatusCheckedIn)
            {
                bookingFromDb.VillaNumber = villaNumber;
                bookingFromDb.ActualCheckInDate = DateTime.Now;
            }
            if (bookingStatus == Claim.StatusCompleted)
            {
                bookingFromDb.ActualCheckOutDate = DateTime.Now;
            }
        }
    }

    public void UpdateStripePaymentID(int bookingId, string sessionId, string paymentIntentId)
    {
        var bookingFromDb = _db.Bookings.FirstOrDefault(m => m.Id == bookingId);
        if (bookingFromDb != null)
        {
            if (!string.IsNullOrEmpty(sessionId))
            {
                bookingFromDb.StripeSessionId = sessionId;
            }
            if (!string.IsNullOrEmpty(paymentIntentId))
            {
                bookingFromDb.StripePaymentIntentId = paymentIntentId;
                bookingFromDb.PaymentDate=DateTime.Now;
                bookingFromDb.IsPaymentSuccessful = true;
            }
        }
    }
    
    
    
}