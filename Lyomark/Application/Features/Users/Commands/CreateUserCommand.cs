using Application.Contracts;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands
{
    public class CreateUserCommand : IRequest<int>
    {
        public string UserName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDay { get; set; }
        public int? Cellphone { get; set; }
        public string Country { get; set; }
        public bool ContactInfo { get; set; }

        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
        {
            private readonly IUnitOfWorkRepository _unitOfWork;
            private readonly IMapper _mapper;
            public CreateUserCommandHandler(IUnitOfWorkRepository unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var entity = _mapper.Map<User>(request);
                var response = await _unitOfWork.Repository<User>().AddAsync(entity);
                return response.Id;
            }

            public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
            {
                public CreateUserCommandValidator()
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

                }
            }
        }
    }
}
