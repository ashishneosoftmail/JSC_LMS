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
        Task<AddGallaryResponseModel> AddGallary(UploadImageDto uploadImageDto);
        Task<GetAllGallaryListResponseModel> GetGallaryList();
        Task<GetGallaryListByIdResponseModel> GetGallaryById(int Id);
    }
}
