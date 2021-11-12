using JSC_LMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Contracts.Persistence
{
    public interface IClassRepository:IAsyncRepository<Class>
    {
        Task<bool> IsClassName(string ClassName,int id);
    }
}
