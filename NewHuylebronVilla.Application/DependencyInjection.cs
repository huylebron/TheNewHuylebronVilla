using Microsoft . Extensions . DependencyInjection ;
using NewHuylebronVilla . Application . Common . Interface ;
using NewHuylebronVilla . Application . Services . Implementation ;
using NewHuylebronVilla . Application . Services . Interface ;

namespace NewHuylebronVilla.Application ;

public  static  class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Đăng ký FileService
        services.AddScoped<IFileService, FileService>();
        
        // Đăng ký các dịch vụ khác
        services.AddScoped<IVillaService, VillaService>();
        services.AddScoped<IVillaNumberService, VillaNumberService>();
        services.AddScoped<IAmenityService, AmenityService>();
        services.AddScoped<IBookingService, BookingService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IDashboardService, DashboardService>();
        
        return services;
    }   
}