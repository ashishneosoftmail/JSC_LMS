using JSC_LMS.Application.Features.Feedback.Queries.GetFeedbackList;
using JSC_LMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JSC_LMS.Application.Features.Feedback.Commands.CreateFeedback
{
    public class CreateFeedbackDto
    {
        public int SchoolId { get; set; }
   
        public int FeedbackTitleId { get; set; }
     
        public int StudentId { get; set; }
     
        public int ParentId { get; set; }
        [Required(ErrorMessage = "Subject Is Required")]

        public int SubjectId { get; set; }

        public int SectionId { get; set; }
 
        public int ClassId { get; set; }


  
        public string FeedbackType { get; set; }
 
     

        public string FeedbackComments { get; set; }

   
        public DateTime SendDate { get; set; }
        public bool IsActive { get; set; }

        public bool Status { get; set; }
        //public string FeedbackTitle { get; set; }

        //public string Subject { get; set; }

        //public string Students { get; set; }

        //public string Parents { get; set; }

        //public string Classes { get; set; }

        //public string Sections { get; set; }

    }
}
