using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Persistence.Repositories
{
    public class TeacherRepository : BaseRepository<Teacher>, ITeacherRepository
    {
        private readonly ILogger _logger;
        public TeacherRepository(ApplicationDbContext dbContext, ILogger<Teacher> logger) : base(dbContext, logger)
        {
            _logger = logger;
        }
    
    }
}
