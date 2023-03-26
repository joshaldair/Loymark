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

namespace Application.Features.Activities.Commands
{
    public class DeleteActivityCommand : IRequest
    {
        public int Id { get; set; }
        public DeleteActivityCommand(int id)
        {
            Id = id;
        }

        public class DeleteActivityCommandHandler: IRequestHandler<DeleteActivityCommand>
        {
            private readonly IMapper _mapper;
            private readonly IUnitOfWorkRepository _unitOfWork;

            public DeleteActivityCommandHandler(IMapper mapper, IUnitOfWorkRepository unitOfWork)
            {
                _mapper = mapper;
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
            {
                var activity = await _unitOfWork.Repository<Activity>().GetByIdAsync(request.Id);
                if (activity == null)
                {
                    throw new NotFoundException(nameof(Activity), request.Id);
                }

                _mapper.Map(request, activity);
                await _unitOfWork.Repository<Activity>().DeleteAsync(activity);
                return Unit.Value;
            }
        }
    }
}
