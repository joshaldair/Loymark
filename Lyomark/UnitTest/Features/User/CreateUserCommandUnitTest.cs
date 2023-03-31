using Application.Mappings;
using AutoMapper;
using Infrastructure.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTest.Mocks;
using Xunit;
using Application.Features.Users.Commands;
using static Application.Features.Users.Commands.CreateUserCommand;
using Shouldly;

namespace UnitTest.Features.User
{
    public class CreateUserCommandUnitTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<UnitOfWorkRepository> _unitOfWork;

        public CreateUserCommandUnitTest()
        {
            _unitOfWork = MokUnitOfWork.GetUnitOfWork();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
        }


        [Fact]
        public async Task CreateUserCommand_ReturnsNumber()
        {
            var activ = new CreateUserCommand
            {
                    UserName="string",
                    LastName ="lastSitring",
                    Email= "email@gmail.com",
                    BirthDay= DateTime.Now,
                    Cellphone = 1234345,
                    Country ="COL",
                    ContactInfo =true
             };

            var handler = new CreateUserCommandHandler(_unitOfWork.Object, _mapper);
            var result = await handler.Handle(activ, CancellationToken.None);
            result.ShouldBeOfType<int>();
        }
    }
}
