using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Feedback.Queries.GetFeedbackList
{
    public class GetAllFeedbackListVm
    {
        public int Id { get; set; }
        public int FeedbackTitleId { get; set; }
        public int SchoolId { get; set; }
        public int StudentId { get; set; }
        public int ParentId { get; set; }
        public int SubjectId { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public string FeedbackType { get; set; }
        public string FeedbackComments { get; set; }
        public DateTime SendDate { get; set; }
        public bool IsActive { get; set; }
        public bool Status { get; set; }
        public FeedbackTitleDto feedbackTitle { get; set; }
        public SubjectDto Subject { get; set; }
        public StudentDto Students { get; set; }
        public ParentDto Parents { get; set; }
        public ClassDto Classes { get; set; }
        public SectionDto Section { get; set; }
        public SchoolDto School { get; set; }

    }
}
