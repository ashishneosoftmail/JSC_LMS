using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Persistence.Repositories
{
   public class SubjectRepository : BaseRepository<Subject>, ISubjectRepository
    {
        private readonly ILogger _logger;
        public SubjectRepository(ApplicationDbContext dbContext, ILogger<Subject> logger) : base(dbContext, logger)
        {
            _logger = logger;
        }

    }
}
