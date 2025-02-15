using NewHuylebronVilla . Application . Common . DTO ;

namespace NewHuylebronVilla.Application.Services.Interface ;

public interface IDashboardService
{
    Task<RadialBarChartDto> GetTotalBookingRadialChartData();
    Task<RadialBarChartDto> GetRegisteredUserChartData();
    Task<RadialBarChartDto> GetRevenueChartData();
    Task<PieChartDto> GetBookingPieChartData();
    Task<LineChartDto> GetMemberAndBookingLineChartData();
}