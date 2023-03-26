using Application.Contracts;
using Application.Exceptions;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Activities.Commands;

public class UpdateActivityCommand : IRequest
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string ActivityName { get; set; }

    public class UpdateActivityCommandHandler : IRequestHandler<UpdateActivityCommand>
    {
        private readonly IUnitOfWorkRepository _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateActivityCommandHandler(IUnitOfWorkRepository unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateActivityCommand request, CancellationToken cancellationToken)
        {
            var activity = await _unitOfWork.Repository<Activity>().GetByIdAsync(request.Id);
            if (activity == null)
            {
                throw new NotFoundException(nameof(Activity), request.Id);
            }

            var user = await _unitOfWork.Repository<User>().GetByIdAsync(request.UserId);
            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.UserId);
            }

            activity.ActivityName = request.ActivityName;
            activity.UserId = request.UserId;
             
            await _unitOfWork.Repository<Activity>().UpdateAsync(activity);

            return Unit.Value;
        }

        public class UpdateActivityCommandValidator : AbstractValidator<UpdateActivityCommand>
        {
            public UpdateActivityCommandValidator()
            {
                RuleFor(p => p.ActivityName)
                       .NotEmpty()
                       .WithMessage("Activity Name is required")
                       .NotNull()
                       .MaximumLength(90).WithMessage("Name cannot exceed 90 characters");

                RuleFor(p => p.UserId)
                    .NotNull().WithMessage("User cannot be null")
                    .NotEmpty().WithMessage("User is required");

                RuleFor(p => p.Id)
                    .NotNull()
                    .NotEmpty().WithMessage("Activity Id is required");
            }
        }
    }
}
