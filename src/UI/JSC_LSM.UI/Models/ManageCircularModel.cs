﻿using JSC_LMS.Application.Features.Circulars.Commands.CreateCircular;
using JSC_LMS.Application.Features.Circulars.Queries.GetCircularListByPagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Models
{
    public class ManageCircularModel
    {

        public AddCircular AddCircular { get; set; }
        public List<SelectListItem> Schools { get; set; }
        public Pager Pager { get; set; }
        public IEnumerable<CircularPagination> CircularListPagination { get; set; }
    }
    public class AddCircular : CreateCircularDto
    {
        [Required(ErrorMessage = "File Is Required")]
        public IFormFile fileUpload { get; set; }
    }
    public class CircularPagination : GetAllCircularListByPaginationDto
    {

    }
}
