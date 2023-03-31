using Application.Features.Activities.Commands;
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
using static Application.Features.Activities.Commands.CreateActivityCommand;

namespace UnitTest.Features.Activity;


public class CreateActivityCommandUnitTest
{
    private readonly IMapper _mapper;
    private readonly Mock<UnitOfWorkRepository> _unitOfWork;

    public CreateActivityCommandUnitTest()
    {
        
        _unitOfWork = MokUnitOfWork.GetUnitOfWork();
        MokcUserRepository.AddDataUserRepository(_unitOfWork.Object.Context);
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<MappingProfile>();
        });
        _mapper = mapperConfig.CreateMapper();
        
    }

    [Fact]
    public async Task CreateActivityCommand_ReturnsNumber()
    {
        var activ = new CreateActivityCommand
        {
            ActivityName = "1",
            UserId = 8001
        };

        var handler = new CreateActivityCommandHandler( _unitOfWork.Object, _mapper);
        var result = await handler.Handle(activ, CancellationToken.None);
        result.ShouldBeOfType<int>();
    }
}
