using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LMS.Persistence.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {

        private readonly ILogger _logger;
        public CategoryRepository(ApplicationDbContext dbContext, ILogger<Category> logger) : base(dbContext, logger)
        {
            _logger = logger;
        }

    }
}
