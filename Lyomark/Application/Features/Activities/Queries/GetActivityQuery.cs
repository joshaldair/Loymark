using Application.Contracts;
using Application.Features.Activities.Queries.DTO;
using AutoMapper;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Activities.Queries
{
    public class GetActivityQuery : IRequest<List<ActivityDTO>>
    {
        public GetActivityQuery()
        {

        }
         
        public class GetActivityQueryHandler : IRequestHandler<GetActivityQuery, List<ActivityDTO>>
        {
            private readonly IUnitOfWorkRepository _unitOfWork;
            private readonly IMapper _mapper;

            public GetActivityQueryHandler(IUnitOfWorkRepository unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<List<ActivityDTO>> Handle(GetActivityQuery request, CancellationToken cancellationToken)
            {
                var lista = (await _unitOfWork.Repository<Activity>().GetAsync(x => x.IsActive == true)).OrderByDescending(c => c.CreatedDate);
                return _mapper.Map<List<ActivityDTO>>(lista);
            }
        }
    }
}
