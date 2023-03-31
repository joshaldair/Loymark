using Application.Features.Activities.Commands;
using Application.Mappings;
using AutoMapper;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTest.Mocks;
using Xunit;
using static Application.Features.Activities.Commands.DeleteActivityCommand;

namespace UnitTest.Features.Activity;

public class DeleteActivityCommandUnitTest
{
    private readonly Mock<UnitOfWorkRepository> _unitOfWork;
    private readonly Mock<ILogger<DeleteActivityCommandHandler>> _logger;
    private readonly IMapper _mapper;
    public DeleteActivityCommandUnitTest()
    {
        _unitOfWork = MokUnitOfWork.GetUnitOfWork();
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<MappingProfile>();
        });
        _mapper = mapperConfig.CreateMapper();


        MockActivityRepository.AddDataActivityRepository(_unitOfWork.Object.Context);
    }


    [Fact]
    public async Task DeleteArticuloCommand()
    {
        var articulo = new DeleteActivityCommand(8001);
        var handler = new DeleteActivityCommandHandler(_mapper, _unitOfWork.Object);
        var result = await handler.Handle(articulo, CancellationToken.None);
        result.ShouldBeOfType<Unit>();
    }
}
