using JSC_LMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Contracts.Persistence
{
    
    public interface IStudentRepository : IAsyncRepository<Students>
    {
        Task<IReadOnlyList<Students>> StudentGetPagedReponseAsyncBySchoolId(int page, int size, int schoolid);
    }
}
