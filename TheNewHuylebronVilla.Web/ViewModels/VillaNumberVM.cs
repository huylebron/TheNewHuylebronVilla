using Microsoft . AspNetCore . Mvc . Rendering ;
using NewHuylebronVilla . Domain . Entities ;

namespace TheNewHuylebronVilla.Web.ViewModels ;

public class VillaNumberVM
{
    public VillaNumber ? VillaNumber { get; set; }
    public IEnumerable <SelectListItem> VillaList { get; set; }
    
}