using JSC_LMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

#region - Interface repository for Zipcode entity : by Shivani Goswami
namespace JSC_LMS.Application.Contracts.Persistence
{
   public interface IZipRepository : IAsyncRepository<Zip>
    {
    }
}
#endregion