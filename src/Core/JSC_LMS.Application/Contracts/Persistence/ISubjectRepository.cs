using JSC_LMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Contracts.Persistence
{
   public interface ISubjectRepository : IAsyncRepository<Subject>
    {
        public Task<bool> IsSubjectName(string SubjectName, int SchoolId, int ClassId, int SectionId);
    }
}
