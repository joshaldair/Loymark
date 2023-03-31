using Application.Features.Activities.Commands;
using Application.Mappings;
using AutoMapper;
using Infrastructure.Repositories;
using MediatR;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTest.Mocks;
using Xunit;
using static Application.Features.Activities.Commands.UpdateActivityCommand;

namespace UnitTest.Features.Activity;

public class UpdateActivityCommandUnitTest
{
    private readonly IMapper _mapper;
    private readonly Mock<UnitOfWorkRepository> _unitOfWork;
    public UpdateActivityCommandUnitTest()
    {
        _unitOfWork = MokUnitOfWork.GetUnitOfWork();
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<MappingProfile>();
        });
        _mapper = mapperConfig.CreateMapper();
        MokcUserRepository.AddDataUserRepository(_unitOfWork.Object.Context);

        MockActivityRepository.AddDataActivityRepository(_unitOfWork.Object.Context);
    }

    [Fact]
    public async Task UpdateArticuloCommand_ReturnsUnit()
    {
        var articulo = new UpdateActivityCommand
        {
            ActivityName = "2",
            UserId = 8001,
            Id = 8001
        };

        var handler = new UpdateActivityCommandHandler( _unitOfWork.Object, _mapper);
        var result = await handler.Handle(articulo, CancellationToken.None);
        result.ShouldBeOfType<Unit>();
    }
}
