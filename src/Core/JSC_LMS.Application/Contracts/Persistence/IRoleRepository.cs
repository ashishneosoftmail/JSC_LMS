using JSC_LMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Contracts.Persistence
{
    public interface IRoleRepository : IAsyncRepository<Role>
    {
        Task<bool> IsRoleName(string name);
    }
}
