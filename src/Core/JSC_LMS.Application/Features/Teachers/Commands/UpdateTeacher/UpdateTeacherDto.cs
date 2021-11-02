using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Teachers.Commands.UpdateTeacher
{
    public class UpdateTeacherDto
    {
        public int Id { get; set; }
        public string TeacherName { get; set; }
        public string UserTypeId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public int ClassId { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string UserId { get; set; }
        public int CityId { get; set; }
        public int StateId { get; set; }
        public int ZipId { get; set; }
        public int SectionId { get; set; }
        
        public int SubjectId { get; set; }
        public bool IsActive { get; set; }
    }
}
