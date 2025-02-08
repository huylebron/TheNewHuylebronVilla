using Microsoft . AspNetCore . Mvc . ModelBinding . Validation ;
using Microsoft . AspNetCore . Mvc . Rendering ;
using NewHuylebronVilla . Domain . Entities ;

namespace TheNewHuylebronVilla.Web.ViewModels ;

public class AmenityVM
{
    public Amenity? Amenity { get; set; }
    [ValidateNever]
    public IEnumerable<SelectListItem>? VillaList { get; set; }
    
}