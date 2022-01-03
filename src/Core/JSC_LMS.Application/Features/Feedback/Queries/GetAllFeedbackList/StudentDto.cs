using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Feedback.Queries.GetFeedbackList
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string StudentName { get; set; }


    }
    public class ParentDto
    {
        public int parentId { get; set; }
        public string ParentName { get; set; }


    }
    public class SubjectDto
    {
        public int subjectId { get; set; }
        public string SubjectName { get; set; }

    }

    public class FeedbackTitleDto
    {
        public int feedbacktitleId { get; set; }
        public string Feedback_Title { get; set; }


    }


    public class ClassDto
    {
        public int classId { get; set; }
        public string ClassName { get; set; }


    }

    public class SectionDto
    {
        public int sectionId { get; set; }
        public string SectionName { get; set; }


    }
    public class SchoolDto
    {
        public int SchoolId { get; set; }
        public string SchoolName { get; set; }


    }
}
