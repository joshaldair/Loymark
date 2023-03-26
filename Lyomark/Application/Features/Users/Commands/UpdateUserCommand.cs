using Application.Contracts;
using Application.Exceptions;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;

namespace Application.Features.Users.Commands;

public class UpdateUserCommand : IRequest
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime BirthDay { get; set; }
    public int? Cellphone { get; set; }
    public string Country { get; set; }
    public bool ContactInfo { get; set; }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IUnitOfWorkRepository _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateUserCommandHandler(IUnitOfWorkRepository unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Repository<User>().GetByIdAsync(request.Id);
            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.Id);
            }

            user.BirthDay = request.BirthDay;
            user.UserName = request.UserName;
            user.LastName = request.LastName;
            user.Cellphone = request.Cellphone;
            user.Country = request.Country;
            user.ContactInfo = request.ContactInfo;
            user.Email = request.Email;

            await _unitOfWork.Repository<User>().UpdateAsync(user);

            return Unit.Value;
        }
    }

    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(p => p.UserName)
                  .NotNull().WithMessage("Name cannot be null")
                  .NotEmpty().WithMessage("Name is required");

            RuleFor(p => p.Email)
               .NotNull().WithMessage("Email cannot be null")
               .EmailAddress().WithMessage("A valid email is required")
               .NotEmpty().WithMessage("Email is required");

            RuleFor(p => p.LastName)
               .NotNull().WithMessage("Last Name cannot be null")
               .NotEmpty().WithMessage("Last Name is required");

            RuleFor(p => p.Country)
               .NotNull().WithMessage("Country cannot be null")
               .MaximumLength(3).WithMessage("Country code incorrect")
               .MinimumLength(3).WithMessage("Country code incorrect")
               .NotEmpty().WithMessage("Country is required");

            RuleFor(p => p.BirthDay)
               .NotNull().WithMessage("Birth Day cannot be null")
               .NotEmpty().WithMessage("Birth Day is required");

            RuleFor(p => p.Id)
             .NotNull()
             .NotEmpty().WithMessage("User Id is required");
        }
    }
}
