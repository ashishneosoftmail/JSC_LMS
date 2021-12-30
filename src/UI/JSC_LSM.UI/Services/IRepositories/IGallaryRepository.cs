using JSC_LMS.Application.Features.Gallary.Commands.UploadImage;
using JSC_LSM.UI.ResponseModels.GallaryResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Services.IRepositories
{
  public  interface IGallaryRepository
    {
        Task<AddGallaryResponseModel> AddGallary(string baseurl, UploadImageDto uploadImageDto);
        Task<GetAllGallaryListResponseModel> GetGallaryList(string baseurl);
        Task<GetGallaryListByIdResponseModel> GetGallaryById(string baseurl, int Id);
        Task<GetGallaryListBySchoolIdResponseModel> GetGallaryBySchoolId(string baseurl, int SchoolId);
        Task DeleteGallary(string baseurl, int id);
        Task DeleteAllGallary(string baseurl);
    }
}
