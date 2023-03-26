using Application.Contracts;
using Application.Features.Activities.Queries.DTO;
using Application.Features.Users.Queries.DTO;
using AutoMapper;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries
{
    public class GetUserQuery : IRequest<List<UserDTO>>
    {
        public GetUserQuery()
        {

        }

        public class GetUserQueryHandler : IRequestHandler<GetUserQuery, List<UserDTO>>
        {
            private readonly IUnitOfWorkRepository _unitOfWork;
            private readonly IMapper _mapper;
            public GetUserQueryHandler(IUnitOfWorkRepository unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<List<UserDTO>> Handle(GetUserQuery request, CancellationToken cancellationToken)
            {
                var lista = (await _unitOfWork.Repository<User>().GetAsync(x => x.IsActive == true)).OrderByDescending(c => c.UserName);
                return _mapper.Map<List<UserDTO>>(lista);
            }
        }
    }
}
