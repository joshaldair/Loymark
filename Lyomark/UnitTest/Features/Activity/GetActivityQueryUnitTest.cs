using Application.Contracts;
using Application.Features.Activities.Queries;
using Application.Features.Activities.Queries.DTO;
using Application.Mappings;
using AutoMapper;
using Infrastructure.Repositories;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTest.Mocks;
using Xunit;
using static Application.Features.Activities.Queries.GetActivityQuery;

namespace UnitTest.Features.Activity
{
    public  class GetActivityQueryUnitTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<UnitOfWorkRepository> _mock;

        public GetActivityQueryUnitTest()
        {
            _mock = MokUnitOfWork.GetUnitOfWork();
            //_mock = MokUnitOfWork.GetUnitOfWorkListadoActitidades();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
            MockActivityRepository.AddDataActivityRepository(_mock.Object.Context);

        }


        [Fact]
        public async Task GetActivity()
        {
            var handler = new GetActivityQueryHandler(_mock.Object, _mapper);
            var result = await handler.Handle(new GetActivityQuery { }, CancellationToken.None);

            result.ShouldBeOfType<List<ActivityDTO>>();
            //result.Count.ShouldBe(5);
        }
    }
}
