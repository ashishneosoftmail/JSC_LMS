using AutoMapper;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Circulars.Commands.UpdateCircular
{
    public class UpdateCircularCommandHandler : IRequestHandler<UpdateCircularCommand, Response<int>>
    {
        private readonly ICircularRepository _circularRepository;
        private readonly IMapper _mapper;
        public UpdateCircularCommandHandler(IMapper mapper, ICircularRepository circularRepository)
        {
            _mapper = mapper;
            _circularRepository = circularRepository;
        }

        public async Task<Response<int>> Handle(UpdateCircularCommand request, CancellationToken cancellationToken)
        {
            var circularUpdate = await _circularRepository.GetByIdAsync(request.updateCircularDto.Id);
            Response<int> UpdateCircularCommandResponse = new Response<int>();
            if (circularUpdate == null)
            {
                UpdateCircularCommandResponse.Message = "No Data Found";
                return UpdateCircularCommandResponse;
            }
            circularUpdate.CircularTitle = request.updateCircularDto.CircularTitle;
            circularUpdate.Description = request.updateCircularDto.Description;
            circularUpdate.File = request.updateCircularDto.File;
            circularUpdate.SchoolId = request.updateCircularDto.SchoolId;
            circularUpdate.Status = request.updateCircularDto.Status;
            circularUpdate.IsActive = request.updateCircularDto.IsActive;
            await _circularRepository.UpdateAsync(circularUpdate);

            return new Response<int>(request.updateCircularDto.Id, "Updated successfully ");
        }
    }
}
