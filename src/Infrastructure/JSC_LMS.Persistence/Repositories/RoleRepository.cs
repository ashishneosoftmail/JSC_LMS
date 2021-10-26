using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSC_LMS.Persistence.Repositories
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        private readonly ILogger _logger;
        public RoleRepository(ApplicationDbContext dbContext, ILogger<Role> logger) : base(dbContext, logger)
        {
            _logger = logger;
        }
        public Task<bool> IsRoleName(string name)
        {
            _logger.LogInformation("GetRoles Initiated");
            var matches = _dbContext.Roles.Any(e => e.RoleName.Equals(name));
            _logger.LogInformation("GetRoles Completed");
            return Task.FromResult(matches);
        }
    }
}
