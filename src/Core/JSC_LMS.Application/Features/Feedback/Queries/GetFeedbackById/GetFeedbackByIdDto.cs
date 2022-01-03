using JSC_LMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Feedback.Queries.GetFeedbackById
{
  
        public class GetFeedbackByIdDto
        {
            public int Id { get; set; }
           public int SchoolId { get; set; }
            public int FeedbackTitleId { get; set; }
          
            public int StudentId { get; set; }
           
            public int ParentId { get; set; }         

            public int SubjectId { get; set; }
         
            public string Type { get; set; }

            public string FeedbackComments { get; set; }
           
            public DateTime SendDate { get; set; }
            public bool IsActive { get; set; }
            public FeedbackTitleDto feedbackTitle { get; set; }

            public SubjectDto Subject { get; set; }

            public StudentDto Students { get; set; }

            public ParentDto Parents { get; set; }

            public ClassDto Classes { get; set; }
            public SectionDto Section { get; set; }
        public SchoolDto School { get; set; }
    }
    
}
