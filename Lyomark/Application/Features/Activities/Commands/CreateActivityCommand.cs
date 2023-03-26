using Application.Contracts;
using Application.Exceptions;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;

namespace Application.Features.Activities.Commands;

public class CreateActivityCommand : IRequest<int>
{
    public int UserId { get; set; }
    public string ActivityName { get; set; }

    public class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand, int>
    {
        private readonly IUnitOfWorkRepository _unitOfWork;
        private readonly IMapper _mapper;

        public CreateActivityCommandHandler(IUnitOfWorkRepository unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Repository<User>().GetByIdAsync(request.UserId);
            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.UserId);
            }

            var entity = _mapper.Map<Activity>(request);
            var response = await _unitOfWork.Repository<Activity>().AddAsync(entity);
            return response.Id;
        }
    }

    public class CreateActivityCommandValidator : AbstractValidator<CreateActivityCommand>
    {
        public CreateActivityCommandValidator()
        {
            RuleFor(p => p.ActivityName)
                .NotEmpty()
                .WithMessage("Activity Name is required")
                .NotNull()
                .MaximumLength(90).WithMessage("Name cannot exceed 90 characters");

            RuleFor(p => p.UserId)
                .NotNull().WithMessage("User cannot be null")
                .NotEmpty().WithMessage("User is required");
        }
    }
}
