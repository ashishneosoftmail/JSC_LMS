﻿using AutoMapper;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Exceptions;
using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Institutes.Commands.UpdateInstitute
{


    public class UpdateInstituteCommandHandler : IRequestHandler<UpdateInstituteCommand, Response<int>>
    {
        private readonly IInstituteRepository _instituteRepository;
        private readonly IMapper _mapper;

        public UpdateInstituteCommandHandler(IMapper mapper, IInstituteRepository instituteRepository)
        {
            _mapper = mapper;
            _instituteRepository = instituteRepository;
        }

        public async Task<Response<int>> Handle(UpdateInstituteCommand request, CancellationToken cancellationToken)
        {
            var instituteToUpdate = await _instituteRepository.GetByIdAsync(request.updateInstituteDto.Id);

            if (instituteToUpdate == null)
            {
                throw new NotFoundException(nameof(JSC_LMS.Domain.Entities.Institute), request.updateInstituteDto.Id);
            }

            var validator = new UpdateInstituteCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            instituteToUpdate.InstituteName = request.updateInstituteDto.InstituteName;
            instituteToUpdate.AddressLine1 = request.updateInstituteDto.AddressLine1;
            instituteToUpdate.AddressLine2 = request.updateInstituteDto.AddressLine2;
            instituteToUpdate.ContactPerson = request.updateInstituteDto.ContactPerson;
            instituteToUpdate.Mobile = request.updateInstituteDto.Mobile;
            instituteToUpdate.LicenseExpiry = request.updateInstituteDto.LicenseExpiry;
            instituteToUpdate.CityId = request.updateInstituteDto.CityId;
            instituteToUpdate.StateId = request.updateInstituteDto.StateId;
            instituteToUpdate.ZipId = request.updateInstituteDto.ZipId;
            instituteToUpdate.InstituteURL = request.updateInstituteDto.InstituteURL;
            instituteToUpdate.IsActive = request.updateInstituteDto.IsActive;
            await _instituteRepository.UpdateAsync(instituteToUpdate);

            return new Response<int>(request.updateInstituteDto.Id, "Updated successfully ");

        }

    }
}
