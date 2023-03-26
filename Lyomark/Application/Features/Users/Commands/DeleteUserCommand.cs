using Application.Contracts;
using Application.Exceptions;
using AutoMapper;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands
{
    public class DeleteUserCommand : IRequest
    {
        public int Id { get; set; }
        public DeleteUserCommand(int id)
        {
            Id = id;
        }

        public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
        {
            private readonly IMapper _mapper;
            private readonly IUnitOfWorkRepository _unitOfWork;

            public DeleteUserCommandHandler(IMapper mapper, IUnitOfWorkRepository unitOfWork)
            {
                _mapper = mapper;
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _unitOfWork.Repository<User>().GetByIdAsync(request.Id);
                if (user == null)
                {
                    throw new NotFoundException(nameof(User), request.Id);
                }
                _mapper.Map(request, user);
                await _unitOfWork.Repository<User>().DeleteAsync(user);
                return Unit.Value;
            }
        }
    }
}
