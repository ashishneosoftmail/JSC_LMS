﻿using JSC_LMS.Application.Features.Common.Categories.Commands;
using JSC_LMS.Application.Features.KnowledgeBase.Commands.CreateKnowledgeBase;
using JSC_LMS.Application.Features.KnowledgeBase.Commands.UpdateKnowledgeBase;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Models
{
    public class KnowledgeBaseViewModel
    {
        public AddCategory AddCategory { get; set; }
        public AddKnowledgeBase AddKnowledgeBase { get; set; }
        public UpdateKnowledgeBase UpdateKnowledgeBase { get; set; }
        public List<SelectListItem> Categories { get; set; }
    }
    public class AddCategory : CreateCategoryDto
    {
    }
    public class AddKnowledgeBase : CreateKnowledgeBaseDto
    {

    }
    public class UpdateKnowledgeBase : UpdateKnowledgeBaseDto
    {

    }
}
