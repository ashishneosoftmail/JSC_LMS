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
        Task<AddKnowledgeBaseResponseModel> AddKnowledgeBase(string baseurl, CreateKnowledgeBaseDto createKnowledgeBaseDto);
        Task<UpdateKnowledgeBaseResponseModel> EditKnowledgeBase(string baseurl, UpdateKnowledgeBaseDto updateKnowledgeBaseDto);
        Task<GetKnowledgeBaseByIdResponseModel> GetKnowlegebaseById(string baseurl, int id);

        Task<GetAllKnowledgeBasePaginationResponseModel> GetAllKnowledgeBaseByPagination(string baseurl, int page, int size);
        Task<GetAllKnowledgeBaseFilterResponseModel> GetAllKnowledgeBaseByFilters(string baseurl, string title, string subtitle, string category);
        Task<GetAllKnowledgeBaseListResponseModel> GetAllKnowledgeBaseList(string baseurl);
        Task DeleteKnowledgeBase(string baseurl, int id);
    }
}
