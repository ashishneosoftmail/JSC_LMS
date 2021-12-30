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

        Task<FeedbackResponseModel> AddNewFeedback(string baseurl, CreateFeedbackDto createSectionDto);

        Task<GetFeedbackByIdResponseModel> GetFeedbackById(string baseurl, int Id);

        Task<GetAllFeedbackListResponseModel> GetAllFeedbackDetails(string baseurl);
        //Task<GetAllFeedbackByFiltersResponseModel> GetFeedbackByFilters(string baseurl,string StudentName, string ClassName, string SectionName, string ParentName, string SubjectName, string Type, DateTime SendDate, bool IsActive);

        //Task<GetAllFeedbackByPaginationResponseModel> GetFeedbackByPagination(string baseurl,int page, int size);



        //Task<GetAllFeedbackResponseModel> GetAllFeedback(string baseurl);

        //Task<AddFeedbackResponseModel> AddFeedbackData(string baseurl,CreateFeedbackDto createFeedbackDto);

    }
}
