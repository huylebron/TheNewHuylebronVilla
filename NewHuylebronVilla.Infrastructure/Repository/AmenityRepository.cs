using NewHuylebronVilla . Application . Common . Interface ;
using NewHuylebronVilla . Domain . Entities ;
using NewHuylebronVilla . Infrastructure . Data ;

namespace NewHuylebronVilla.Infrastructure.Repository ;

public class AmenityRepository : Repository<Amenity>, IAmenityRepository
{
    private readonly ApplicationDbContext _db;

    public AmenityRepository(ApplicationDbContext db) : base(db) 
    {
        _db = db;
    }
 
    public void Update(Amenity entity)
    {
        _db.Amenities.Update(entity);
    }
    
}