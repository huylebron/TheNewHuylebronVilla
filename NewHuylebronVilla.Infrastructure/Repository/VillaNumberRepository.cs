using NewHuylebronVilla . Application . Common . Interface ;
using NewHuylebronVilla . Domain . Entities ;
using NewHuylebronVilla . Infrastructure . Data ;

namespace NewHuylebronVilla.Infrastructure.Repository ;


public class VillaNumberRepository : Repository<VillaNumber>, IVillaNumberRepository
{
    private readonly ApplicationDbContext _db;

    public VillaNumberRepository(ApplicationDbContext db) : base(db) 
    {
        _db = db;
    }
        
    public void Update(VillaNumber entity)
    {
        _db.VillaNumbers.Update(entity);
    }
}


