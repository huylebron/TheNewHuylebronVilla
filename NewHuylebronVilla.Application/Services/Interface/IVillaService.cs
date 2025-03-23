﻿using NewHuylebronVilla . Domain . Entities ;

namespace NewHuylebronVilla.Application.Services.Interface ;

public interface IVillaService
{
    IEnumerable<Villa> GetAllVillas();
    Villa GetVillaById(int id);
    void CreateVilla(Villa villa);
    void UpdateVilla(Villa villa);
    bool DeleteVilla(int id);
    IEnumerable<Villa> GetVillasAvailabilityByDate(int nights, DateOnly checkInDate);
    bool IsVillaAvailableByDate(int villaId, int nights, DateOnly checkInDate);
    
  
    Task<Villa> CreateVillaAsync(Villa villa);
    Task<Villa> UpdateVillaAsync(Villa villa);
}