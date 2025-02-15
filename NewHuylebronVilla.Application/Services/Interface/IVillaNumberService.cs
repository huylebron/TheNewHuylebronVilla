using NewHuylebronVilla . Domain . Entities ;

namespace NewHuylebronVilla.Application.Services.Interface ;

public interface IVillaNumberService
{
    IEnumerable<VillaNumber> GetAllVillaNumbers();
    VillaNumber GetVillaNumberById(int id);
    void CreateVillaNumber(VillaNumber villaNumber);
    void UpdateVillaNumber(VillaNumber villaNumber);
    bool DeleteVillaNumber(int id);

    bool CheckVillaNumberExists(int villa_Number);
}