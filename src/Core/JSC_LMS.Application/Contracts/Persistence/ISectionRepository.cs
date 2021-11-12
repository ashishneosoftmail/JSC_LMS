using JSC_LMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Contracts.Persistence
{
    public interface ISectionRepository : IAsyncRepository<Section>
    {
        Task<bool> IsSectionName(string SectionName,int SchoolId,int ClassId);
    }
}
