using JSC_LMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JSC_LMS.Domain.Entities
{
    public class Feedback : AuditableEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int FeedbackTitleId { get; set; }
        public string FeedbackType { get; set; }
        public int StudentId { get; set; }
        public int ParentId { get; set; }

        public int SectionId { get; set; }
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
        public string FeedbackComments { get; set; }
        public DateTime SendDate { get; set; }
        public bool IsActive { get; set; }
        public FeedbackTitle FeedbackTitle { get; set; }
        public Subject Subject { get; set; }
        public Students Students { get; set; }
        public Parents Parents { get; set; }

        public Class Classes { get; set; }
        public Section Sections { get; set; }
    }
}
