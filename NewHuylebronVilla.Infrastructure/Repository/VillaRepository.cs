using System . Linq . Expressions ;
using Microsoft . EntityFrameworkCore ;
using NewHuylebronVilla . Application . Common . Interface ;
using NewHuylebronVilla . Domain . Entities ;
using NewHuylebronVilla . Infrastructure . Data ;

namespace NewHuylebronVilla.Infrastructure.Repository ;

public class VillaRepository : Repository <Villa> , IVillaRepository
{
    private readonly ApplicationDbContext _db;
    private IVillaRepository _villaRepositoryImplementation ;


    public VillaRepository ( ApplicationDbContext db ) : base ( db ) {
        _db = db;
    }


    public void Update ( Villa entity ) {
        _db.Villas.Update(entity);
    }
}