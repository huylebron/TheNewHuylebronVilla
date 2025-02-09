using Microsoft . AspNetCore . Authorization ;
using Microsoft.AspNetCore.Mvc;
using NewHuylebronVilla . Application . Common . Interface ;
using NewHuylebronVilla.Domain.Entities;
using NewHuylebronVilla.Infrastructure.Data;

namespace TheNewHuylebronVilla.Web.Controllers
{  [Authorize]
    public class VillaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork ;
        private readonly IWebHostEnvironment _webHostEnvironment ;

        public VillaController ( IUnitOfWork unitOfWork , IWebHostEnvironment webHostEnvironment) {
            _unitOfWork = unitOfWork ;
            _webHostEnvironment = webHostEnvironment ;
        }

        public IActionResult Index ( ) {
            var villas = _unitOfWork . Villa . GetAll ( ) ;
            return View ( villas ) ;
        }

        public IActionResult Create ( ) {
            return View ( ) ;
        }

        [ HttpPost ]
        public IActionResult Create ( Villa obj ) {

            if ( obj . Name == obj . Description )
            {
                ModelState . AddModelError ( "name" , "The description cannot exactly match the Name." ) ;
            }

            if ( ModelState . IsValid )
            {
                if ( obj . Image != null )
                {
                    // Tạo tên file duy nhất bằng cách kết hợp GUID và phần mở rộng của file gốc
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(obj.Image.FileName);
 
                    // Xác định đường dẫn lưu trữ hình ảnh trong thư mục "images/VillaImage" của ứng dụng web
                    string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, @"Images\VillaImg");

                    // Mở một FileStream để tạo file mới tại đường dẫn đã xác định
                    using var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create);
 
                    // Sao chép nội dung của file tải lên vào FileStream
                    obj.Image.CopyTo(fileStream);

                    // Lưu đường dẫn URL của hình ảnh vào thuộc tính ImageUrl của đối tượng Villa
                    obj.ImageUrl = @"\Images\VillaImg\" + fileName;
                    
                }
                else
                {
                    obj.ImageUrl = "https://placehold.co/600x400";
                }
                _unitOfWork . Villa . Add ( obj ) ;
                _unitOfWork . Save ( ) ;
                TempData [ "success" ] = "The villa has been created successfully." ;
                return RedirectToAction ( nameof ( Index ) ) ;
            }

            return View ( ) ;
        }

        public IActionResult Update ( int villaId ) {
            Villa ? obj = _unitOfWork . Villa . Get ( u => u . Id == villaId ) ;
            //Villa? obj = _db.Villas.Find(villaId);
            //var VillaList = _db.Villas.Where(u => u.Price > 50 && u.Occupancy > 0);
            if ( obj == null )
            {
                return RedirectToAction ( "Error" , "Home" ) ;
            }

            return View ( obj ) ;
        }

        [ HttpPost ]
        public IActionResult Update ( Villa obj ) {
            if ( ModelState . IsValid && obj . Id > 0 )
            {
                if (obj.Image != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(obj.Image.FileName);
                    string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, @"images\VillaImage");

                    if (!string.IsNullOrEmpty(obj.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create);
                    obj.Image.CopyTo(fileStream);

                    obj.ImageUrl = @"\images\VillaImage\" + fileName;
                }
                _unitOfWork . Villa . Update ( obj ) ;
                _unitOfWork . Save ( ) ;
                TempData [ "success" ] = "The villa has been updated successfully." ;
                return RedirectToAction ( nameof ( Index ) ) ;
            }

            return View ( ) ;
        }

        public IActionResult Delete ( int villaId ) {
            Villa ? obj = _unitOfWork . Villa . Get ( u => u . Id == villaId ) ;
            if ( obj is null )
            {
                return RedirectToAction ( "Error" , "Home" ) ;
            }

            return View ( obj ) ;
        }


        [ HttpPost ]
        public IActionResult Delete ( Villa obj ) {
            Villa ? objFromDb = _unitOfWork . Villa . Get ( u => u . Id == obj . Id ) ;
            if ( objFromDb is not null )
            {
                if (!string.IsNullOrEmpty(objFromDb.ImageUrl))
                {
                    var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, objFromDb.ImageUrl.TrimStart('\\'));

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                _unitOfWork . Villa . Remove ( objFromDb ) ;
                _unitOfWork . Save ( ) ;
                TempData [ "success" ] = "The villa has been deleted successfully." ;
                return RedirectToAction ( nameof ( Index ) ) ;
            }

            return View ( ) ;
        }
    }
}
    
    

    