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
        [Required(ErrorMessage = "Feedback Is Required")]
        public int FeedbackTitleId { get; set; }
        [Required(ErrorMessage = "Student Is Required")]
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Parent Is Required")]
        public int ParentId { get; set; }
        [Required(ErrorMessage = "Subject Is Required")]

        public int SubjectId { get; set; }
        [Required(ErrorMessage = "Section Is Required")]
        public int SectionId { get; set; }
        [Required(ErrorMessage = "Class Is Required")]
        public int ClassId { get; set; }


        [Required(ErrorMessage = "FeedbackType Is Required")]
        public string FeedbackType { get; set; }
 
        [Required(ErrorMessage = "FeedbackComments Is Required")]

        public string FeedbackComments { get; set; }

        [Required(ErrorMessage = "SendDate Is Required")]
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
