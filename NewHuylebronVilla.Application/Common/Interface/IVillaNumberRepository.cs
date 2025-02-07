using NewHuylebronVilla . Domain . Entities ;

namespace NewHuylebronVilla.Application.Common.Interface ;

public interface IVillaNumberRepository: IRepository<VillaNumber>
{
    void Update(VillaNumber entity);
    
}