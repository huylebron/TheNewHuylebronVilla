﻿namespace TheNewHuylebronVilla.Web.ViewModels ;

public class RadialBarChartVM
{
    public decimal TotalCount { get; set; }
    public decimal IncreaseDecreaseAmount { get; set; }
    public bool HasRatioIncreased { get; set; }
    public int[] Series { get; set; }
    public decimal CountInCurrentMonth { get; set; }
}