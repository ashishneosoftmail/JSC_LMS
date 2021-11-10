using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Persistence.Repositories
{
    public class SuperadminRepository : BaseRepository<Superadmin>, ISuperadminRepository
    {
        private readonly ILogger _logger;
        public SuperadminRepository(ApplicationDbContext dbContext, ILogger<Superadmin> logger) : base(dbContext, logger)
        {
            _logger = logger;
        }
    }
}
