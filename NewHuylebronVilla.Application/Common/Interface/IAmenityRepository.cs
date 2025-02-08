using NewHuylebronVilla . Domain . Entities ;

namespace NewHuylebronVilla.Application.Common.Interface ;

public interface IAmenityRepository : IRepository<Amenity>
{
    void Update(Amenity entity);
    
}