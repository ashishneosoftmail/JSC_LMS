using JSC_LMS.Application.Features.Feedback.Commands.CreateFeedback;
using JSC_LMS.Application.Features.Feedback.Queries.GetFeedbackList;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Models
{
    public class FeedbackModel : CreateFeedbackDto
    {
        public List<SelectListItem> FeedbackTitles { get; set; }
        public List<SelectListItem> Subjects { get; set; }
        public List<SelectListItem> Parents { get; set; }
        public List<SelectListItem> Students { get; set; }
        public List<SelectListItem> Sections { get; set; }
        public List<SelectListItem> Classes { get; set; }

        public AddNewFeedback AddNewFeedbacks { get; set; }
        public List<GetAllFeedbackDetails> GetFeedbackList { get; set; }
    }
        public class GetAllFeedbackDetails : GetAllFeedbackListVm
        {
          
            public string ClassName { get; set; }
            public string SectionName { get; set; }
            public string SubjectName { get; set; }
        public string StudentName { get; set; }
        public string ParentName { get; set; }

        public string feedbackTitle { get; set; }

    }



        public class AddNewFeedback : CreateFeedbackDto
        {
          
            public string SubjectName { get; set; }
            public string Comments { get; set; }
           
            public int SubjectId { get; set; }
           

        }




    
}
