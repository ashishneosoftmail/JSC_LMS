using JSC_LMS.Application.Features.KnowledgeBase.Commands.CreateKnowledgeBase;
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
    }
}
