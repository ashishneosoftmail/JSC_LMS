using JSC_LMS.Application.CommonDtos;
using System;
using System.Collections.Generic;
using System.Text;

#region- View model for Get Institute details by using Id: by Shivani Goswami
namespace JSC_LMS.Application.Features.Institutes.Queries.GetInstituteById
{
    public class GetInstituteByIdVm
    {
        public int Id { get; set; }
        public string InstituteName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string ContactPerson { get; set; }
        public string Mobile { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public CityDto City { get; set; }
        public StateDto State { get; set; }
        public ZipDto Zip { get; set; }
        public DateTime LicenseExpiry { get; set; }        
        public DateTime CreatedDate { get; set; }
        public string InstituteURL { get; set; }
        public bool IsActive { get; set; }

    }
}
#endregion