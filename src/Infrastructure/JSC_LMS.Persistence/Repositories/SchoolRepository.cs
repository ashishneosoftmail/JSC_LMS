using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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
        protected override IQueryable<School> GetQueryable()
        {
            return _dbContext.Set<School>().Include(a => a.City).Include(a => a.State).Include(a => a.Zip).Include(a => a.Institute).Include(a => a.Principal);
        }
    }
}
