using JSC_LSM.UI.Helpers;
using JSC_LSM.UI.Models;
using JSC_LSM.UI.Services.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Controllers
{
    public class StudentController : BaseController
    {
        private readonly IStateRepository _stateRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly JSC_LSM.UI.Common.Common _common;
        private readonly IOptions<ApiBaseUrl> _apiBaseUrl;
        private readonly ISchoolRepository _schoolRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IClassRepository _classRepository;
        private readonly IAnnouncementRepository _announcementRepository;

        public StudentController(IStateRepository stateRepository, ISchoolRepository schoolRepository, JSC_LSM.UI.Common.Common common, IOptions<ApiBaseUrl> apiBaseUrl, ITeacherRepository teacherRepository, IClassRepository classRepository, ISectionRepository sectionRepository, ISubjectRepository subjectRepository, IAnnouncementRepository announcementRepository)
        {
            _stateRepository = stateRepository;
            _teacherRepository = teacherRepository;
            _common = common;
            _apiBaseUrl = apiBaseUrl;
            _schoolRepository = schoolRepository;
            _classRepository = classRepository;
            _sectionRepository = sectionRepository;
            _subjectRepository = subjectRepository;
            _announcementRepository = announcementRepository;

        }


        [HttpGet]
        public async Task<IActionResult> ManageAnnouncement(int page = 1, int size = 20)
        {

            int recsCount = (await _announcementRepository.GetAnnouncementList()).data.Count();
            if (page < 1)
                page = 1;
            var pager = new Pager(recsCount, page, size);
            ViewBag.Pager = pager;
            ManageAnnouncementModel model = new ManageAnnouncementModel();
            model.Pager = pager;
            model.Classes = await _common.GetClass();
            model.Sections = await _common.GetSection();
            model.Subjects = await _common.GetSubject();
            var paginationData = await _announcementRepository.GetAnnouncementListByPagination(page, size);
            List<AnnouncementPagination> pagedData = new List<AnnouncementPagination>();
            foreach (var data in paginationData.data)
            {
                pagedData.Add(new AnnouncementPagination()
                {
                    Id = data.Id,
                    AnnouncementTitle = data.AnnouncementTitle,
                    AnnouncementContent = data.AnnouncementContent,
                    //Class = data.Class,
                    //Section = data.Section,
                    Subject = data.Subject,
                    CreatedDate = data.CreatedDate,

                });
            }
            model.AnnouncementPagination = pagedData;
            return View(model);
        }

    }
}
