using Application.Contracts;
using Application.Features.Users.Queries;
using Application.Features.Users.Queries.DTO;
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
using static Application.Features.Users.Queries.GetUserQuery;

namespace UnitTest.Features.User;

public class GetUserQueryUnitTest
{
    private readonly IMapper _mapper;
    private readonly Mock<UnitOfWorkRepository> _mock;
    public GetUserQueryUnitTest()
    {
        //_mock = MokUnitOfWork.GetUnitOfWorkListadoUsuarios();
        _mock = MokUnitOfWork.GetUnitOfWork();
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<MappingProfile>();
        });
        _mapper = mapperConfig.CreateMapper();
       
        
        MokcUserRepository.GetUserRepository();
        
    }

    [Fact]
    public async Task GetUser()
    {
        var handler = new GetUserQueryHandler( _mock.Object,_mapper);
        var result = await handler.Handle(new GetUserQuery { }, CancellationToken.None);

        result.ShouldBeOfType<List<UserDTO>>();
        
    }
}
