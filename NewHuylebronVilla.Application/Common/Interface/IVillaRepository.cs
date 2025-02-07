using System . Linq . Expressions ;
using NewHuylebronVilla . Domain . Entities ;

namespace NewHuylebronVilla.Application.Common.Interface ;

public interface IVillaRepository : IRepository<Villa>
{
   
    void Update(Villa entity);
    //void Remove(Villa entity);
  

    
}