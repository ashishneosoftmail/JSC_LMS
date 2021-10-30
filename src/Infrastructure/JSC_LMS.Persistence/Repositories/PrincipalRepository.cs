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

    public class PrincipalRepository : BaseRepository<Principal>, IPrincipalRepository
    {
        private readonly ILogger _logger;
        public PrincipalRepository(ApplicationDbContext dbContext, ILogger<Principal> logger) : base(dbContext, logger)
        {
            _logger = logger;
        }
        protected override IQueryable<Principal> GetQueryable()
        {
            return _dbContext.Set<Principal>().Include(x => x.City).Include(y => y.State).Include(z => z.Zip).Include(a => a.School);
        }
    }
}
