using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Persistence.Repositories
{

    public class SchoolRepository : BaseRepository<School>, ISchoolRepository
    {

        private readonly ILogger _logger;
        public SchoolRepository(ApplicationDbContext dbContext, ILogger<School> logger) : base(dbContext, logger)
        {
            _logger = logger;
        }

    }
}
