using AutoMapper;
using JSC_LMS.Application.Contracts.Persistence;
using Moq;
using System;
using JSC_LMS.Application.UnitTests.Mocks;
using JSC_LMS.Application.Profiles;
using Xunit;
using System.Threading.Tasks;
using JSC_LMS.Application.Features.Events.Commands.CreateEvent;
using System.Threading;
using Shouldly;
using Microsoft.Extensions.Logging;
using JSC_LMS.Application.Contracts.Infrastructure;

namespace JSC_LMS.Application.UnitTests.Event.Commands
{
    public class CreateEventTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IEventRepository> _mockEventRepository;
        private readonly Mock<IEmailService> _emailService;
        private readonly Mock<ILogger<CreateEventCommandHandler>> _logger;

        public CreateEventTests()
        {
            _mockEventRepository = EventRepositoryMocks.GetEventRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _logger = new Mock<ILogger<CreateEventCommandHandler>>();
            _mapper = configurationProvider.CreateMapper();
            _emailService = EmailServiceMocks.GetEmailService();
        }

        [Fact]
        public async Task Handle_ValidEvent_AddedToEventRepo()
        {
            var handler = new CreateEventCommandHandler(_mapper, _mockEventRepository.Object, _emailService.Object, _logger.Object);

            await handler.Handle(new CreateEventCommand()
            {
                Name = "Test",
                Price = 25,
                Artist = "test",
                Date = new DateTime(2027, 1, 18),
                Description = "description",
                ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/GloboTicket/musical.jpg",
                CategoryId = Guid.Parse("{6313179F-7837-473A-A4D5-A5571B43E6A6}")
            }, CancellationToken.None);

            var allEvents = await _mockEventRepository.Object.ListAllAsync();
            allEvents.Count.ShouldBe(3);
        }
    }
}
