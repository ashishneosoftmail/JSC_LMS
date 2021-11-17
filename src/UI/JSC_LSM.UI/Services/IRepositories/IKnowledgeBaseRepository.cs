using JSC_LMS.Application.Features.KnowledgeBase.Commands.CreateKnowledgeBase;
using JSC_LMS.Application.Features.KnowledgeBase.Commands.UpdateKnowledgeBase;
using JSC_LSM.UI.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Services.IRepositories
{
    public interface IKnowledgeBaseRepository
    {
        Task<AddKnowledgeBaseResponseModel> AddKnowledgeBase(CreateKnowledgeBaseDto createKnowledgeBaseDto);
        Task<UpdateKnowledgeBaseResponseModel> EditKnowledgeBase(UpdateKnowledgeBaseDto updateKnowledgeBaseDto);
        Task<GetKnowledgeBaseByIdResponseModel> GetKnowlegebaseById(int id);

        Task<GetAllKnowledgeBasePaginationResponseModel> GetAllKnowledgeBaseByPagination(int page, int size);
        Task<GetAllKnowledgeBaseFilterResponseModel> GetAllKnowledgeBaseByFilters(string title, string subtitle, string category);
        Task<GetAllKnowledgeBaseListResponseModel> GetAllKnowledgeBaseList();
        Task DeleteKnowledgeBase(int id);
    }
}
