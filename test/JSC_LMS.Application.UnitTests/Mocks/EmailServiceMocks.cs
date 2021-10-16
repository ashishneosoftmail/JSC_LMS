using JSC_LMS.Application.Contracts.Infrastructure;
using JSC_LMS.Application.Models.Mail;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.UnitTests.Mocks
{
    public class EmailServiceMocks
    {
        public static Mock<IEmailService> GetEmailService()
        {
            var mockEmailService = new Mock<IEmailService>();
            mockEmailService.Setup(x => x.SendEmail(It.IsAny<Email>())).ReturnsAsync(true);
            return mockEmailService;
        }
    }
}
