using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSC_LMS.Persistence.Repositories
{
   public class SubjectRepository : BaseRepository<Subject>, ISubjectRepository
    {
        private readonly ILogger _logger;
        public SubjectRepository(ApplicationDbContext dbContext, ILogger<Subject> logger) : base(dbContext, logger)
        {
            _logger = logger;
        }

        public Task<bool> IsSubjectName(string SubjectName, int SchoolId, int ClassId,int SectionId)
        {
            _logger.LogInformation("GetSubject Initiated");
            var matches = _dbContext.Subject.Any(e => e.SubjectName.Equals(SubjectName) && e.SchoolId.Equals(SchoolId) && e.ClassId.Equals(ClassId) && e.SectionId.Equals(SectionId));
            _logger.LogInformation("GetSubject Completed");
            return Task.FromResult(matches);
        }

    }
}
