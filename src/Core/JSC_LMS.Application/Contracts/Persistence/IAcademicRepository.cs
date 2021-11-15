using JSC_LMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Contracts.Persistence
{
    public interface IAcademicRepository : IAsyncRepository<Academics>
    {
    }
}
