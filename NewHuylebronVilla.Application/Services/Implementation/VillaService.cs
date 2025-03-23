using Microsoft . AspNetCore . Hosting ;
using NewHuylebronVilla . Application . Common . Division ;
using NewHuylebronVilla . Application . Common . Interface ;
using NewHuylebronVilla . Application . Services . Interface ;
using NewHuylebronVilla . Domain . Entities ;

namespace NewHuylebronVilla.Application.Services.Implementation ;

public class VillaService : IVillaService
{
  

     private readonly IUnitOfWork _unitOfWork;
    private readonly IFileService _fileService;
    private const string VillaImageFolder = "VillaImage";
    private const string DefaultImageUrl = "https://placehold.co/600x400";

    public VillaService(IUnitOfWork unitOfWork, IFileService fileService)
    {
        _unitOfWork = unitOfWork;
        _fileService = fileService;
    }

    public async Task<Villa> CreateVillaAsync(Villa villa)
    {
        // Xử lý tải lên hình ảnh
        if (villa.Image != null)
        {
            try
            {
                villa.ImageUrl = await _fileService.UploadFileAsync(villa.Image, VillaImageFolder);
            }
            catch (Exception ex)
            {
                // Ghi nhật ký lỗi ở đây nếu cần
                villa.ImageUrl = DefaultImageUrl;
            }
        }
        else
        {
            villa.ImageUrl = DefaultImageUrl;
        }

        _unitOfWork.Villa.Add(villa);
        _unitOfWork.Save();
        return villa;
    }

    public bool DeleteVilla(int id)
    {
        try
        {
            Villa? objFromDb = _unitOfWork.Villa.Get(u => u.Id == id);
            if (objFromDb is not null)
            {
                // Xóa tệp hình ảnh cũ nếu có
                if (!string.IsNullOrEmpty(objFromDb.ImageUrl) && objFromDb.ImageUrl != DefaultImageUrl)
                {
                    _fileService.DeleteFile(objFromDb.ImageUrl);
                }
                
                _unitOfWork.Villa.Remove(objFromDb);
                _unitOfWork.Save();
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }   
    }

    public IEnumerable<Villa> GetAllVillas()
    {
        return _unitOfWork.Villa.GetAll(includeProperties: "VillaAmenity");
    }

    public Villa GetVillaById(int id)
    {
        return _unitOfWork.Villa.Get(u => u.Id == id, includeProperties: "VillaAmenity");
    }

    public IEnumerable<Villa> GetVillasAvailabilityByDate(int nights, DateOnly checkInDate)
    {
        var villaList = _unitOfWork.Villa.GetAll(includeProperties: "VillaAmenity").ToList();
        var villaNumbersList = _unitOfWork.VillaNumber.GetAll().ToList();
        var bookedVillas = _unitOfWork.Booking.GetAll(u => u.Status == Claim.StatusApproved ||
        u.Status == Claim.StatusCheckedIn).ToList();

        foreach (var villa in villaList)
        {
            int roomAvailable = Claim.VillaRoomsAvailable_Count
                (villa.Id, villaNumbersList, checkInDate, nights, bookedVillas);

            villa.IsAvailable = roomAvailable > 0;
        }

        return villaList;
    }

    public bool IsVillaAvailableByDate(int villaId, int nights, DateOnly checkInDate)
    {
        var villaNumbersList = _unitOfWork.VillaNumber.GetAll().ToList();
        var bookedVillas = _unitOfWork.Booking.GetAll(u => u.Status == Claim.StatusApproved ||
        u.Status == Claim.StatusCheckedIn).ToList();

        int roomAvailable = Claim.VillaRoomsAvailable_Count
            (villaId, villaNumbersList, checkInDate, nights, bookedVillas);

        return roomAvailable > 0;
    }

    public async Task<Villa> UpdateVillaAsync(Villa villa)
    {
        var existingVilla = _unitOfWork.Villa.Get(u => u.Id == villa.Id, tracked: true);
        if (existingVilla == null)
        {
            throw new KeyNotFoundException($"Không tìm thấy Villa với ID {villa.Id}");
        }

        // Xử lý tải lên hình ảnh mới nếu có
        if (villa.Image != null)
        {
            try
            {
                // Xóa hình ảnh cũ nếu có
                if (!string.IsNullOrEmpty(existingVilla.ImageUrl) && existingVilla.ImageUrl != DefaultImageUrl)
                {
                    _fileService.DeleteFile(existingVilla.ImageUrl);
                }

                // Tải lên hình ảnh mới
                villa.ImageUrl = await _fileService.UploadFileAsync(villa.Image, VillaImageFolder);
            }
            catch (Exception ex)
            {
                // Giữ nguyên hình ảnh cũ nếu có lỗi
                villa.ImageUrl = existingVilla.ImageUrl;
            }
        }
        else
        {
            // Giữ nguyên hình ảnh cũ
            villa.ImageUrl = existingVilla.ImageUrl;
        }

        // Cập nhật các thuộc tính khác
        existingVilla.Name = villa.Name;
        existingVilla.Description = villa.Description;
        existingVilla.Price = villa.Price;
        existingVilla.Occupancy = villa.Occupancy;
        existingVilla.Sqft = villa.Sqft;
        existingVilla.ImageUrl = villa.ImageUrl;
        existingVilla.Updated_Date = DateTime.Now;

        _unitOfWork.Save();
        return existingVilla;
    }

    // Triển khai giao diện đồng bộ hiện tại của IVillaService
    public void CreateVilla(Villa villa)
    {
        CreateVillaAsync(villa).GetAwaiter().GetResult();
    }

    public void UpdateVilla(Villa villa)
    {
        UpdateVillaAsync(villa).GetAwaiter().GetResult();
    }
}