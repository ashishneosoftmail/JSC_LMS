using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Persistence.Repositories
{

    public class TemporaryPasswordRepository : BaseRepository<TemporaryPassword>, ITemporaryPasswordRepository
    {

        private readonly ILogger _logger;
        public TemporaryPasswordRepository(ApplicationDbContext dbContext, ILogger<TemporaryPassword> logger) : base(dbContext, logger)
        {
            _logger = logger;
        }

    }
}
