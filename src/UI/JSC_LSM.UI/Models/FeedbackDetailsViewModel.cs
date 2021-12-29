using JSC_LMS.Application.Features.Feedback.Queries.GetFeedbackList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Models
{
    public class FeedbackDetailsViewModel
    {
        public int Id { get; set; }

       
        public string Type { get; set; }

        public DateTime SendDate { get; set; }

        public string ClassName { get; set; }
        public string StudentName { get; set; }
        public string SubjectName { get; set; }
        public string SectionName { get; set; }
        public string ParentName { get; set; }
        
        public bool IsActive { get; set; }

        public string feedbackTitle { get; set; }
    }
   
}
