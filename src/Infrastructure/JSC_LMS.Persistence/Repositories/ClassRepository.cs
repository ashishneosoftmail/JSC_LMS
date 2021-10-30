﻿using JSC_LMS.Domain.Entities;
using JSC_LMS.Application.Contracts.Persistence;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Persistence.Repositories
{
   public class ClassRepository : BaseRepository<Class>, IClassRepository
    {
        private readonly ILogger _logger;
        public ClassRepository(ApplicationDbContext dbContext, ILogger<Class> logger) : base(dbContext, logger)
        {
            _logger = logger;
        }
    }
}