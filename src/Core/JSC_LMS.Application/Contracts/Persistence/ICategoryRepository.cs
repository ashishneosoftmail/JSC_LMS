using JSC_LMS.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Contracts.Persistence
{
    public interface ICategoryRepository : IAsyncRepository<Category>
    {
    }
}
