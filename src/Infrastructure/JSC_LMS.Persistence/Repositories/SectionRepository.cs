using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSC_LMS.Persistence.Repositories
{
    public class SectionRepository : BaseRepository<Section>, ISectionRepository
    {

        private readonly ILogger _logger;
        protected readonly ApplicationDbContext _dbContext;
        public SectionRepository(ApplicationDbContext dbContext, ILogger<Section> logger) : base(dbContext, logger)
        {
            _logger = logger;
        }

   
        public override async Task<Section> GetByIdAsync(int id)
        {
            return await GetQueryable().FirstOrDefaultAsync(i => i.Id == id);

        }



    }
}
