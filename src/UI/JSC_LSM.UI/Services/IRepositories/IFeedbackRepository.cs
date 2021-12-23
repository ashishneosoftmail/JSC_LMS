using JSC_LMS.Application.Features.Feedback.Commands.CreateFeedback;
using JSC_LSM.UI.Models;
using JSC_LSM.UI.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Services.IRepositories
{
    public interface IFeedbackRepository
    {

        Task<FeedbackResponseModel> AddNewFeedback(CreateFeedbackDto createSectionDto);

        Task<GetFeedbackByIdResponseModel> GetFeedbackById(int Id);

        Task<GetAllFeedbackListResponseModel> GetAllFeedbackDetails();
        //Task<GetAllFeedbackByFiltersResponseModel> GetFeedbackByFilters(string StudentName, string ClassName, string SectionName, string ParentName, string SubjectName, string Type, DateTime SendDate, bool IsActive);

        //Task<GetAllFeedbackByPaginationResponseModel> GetFeedbackByPagination(int page, int size);



        //Task<GetAllFeedbackResponseModel> GetAllFeedback();

        //Task<AddFeedbackResponseModel> AddFeedbackData(CreateFeedbackDto createFeedbackDto);

    }
}
