using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Persistence.Repositories
{
    public class StateRepository : BaseRepository<State>, IStateRepository
    {

        private readonly ILogger _logger;
        public StateRepository(ApplicationDbContext dbContext, ILogger<State> logger) : base(dbContext, logger)
        {
            _logger = logger;
        }

    }
}
