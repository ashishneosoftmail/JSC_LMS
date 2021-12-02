using JSC_LMS.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using JSC_LMS.Application.Contracts.Persistence;

namespace JSC_LMS.Persistence.Repositories
{
  public  class GallaryRepository : BaseRepository<Gallary>, IGallaryRepository
    {

        private readonly ILogger _logger;
        public GallaryRepository(ApplicationDbContext dbContext, ILogger<Gallary> logger) : base(dbContext, logger)
        {
            _logger = logger;
        }
    }
}
