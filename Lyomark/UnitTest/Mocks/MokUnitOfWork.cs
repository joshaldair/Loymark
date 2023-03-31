using Application.Contracts;
using Infrastructure.Persistance;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Mocks
{
    public class MokUnitOfWork
    {

        public static Mock<UnitOfWorkRepository> GetUnitOfWork()
        {
            Guid dbContextId = Guid.NewGuid();
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: $"DbContext-{dbContextId}")
                .Options;

            var streamerDbContextFake = new Context(options);
            streamerDbContextFake.Database.EnsureDeleted();
            var mockUnitOfWork = new Mock<UnitOfWorkRepository>(streamerDbContextFake);
            return mockUnitOfWork;
        }

        public static Mock<IUnitOfWorkRepository> GetUnitOfWorkListadoActitidades()
        {
            var mockUnitOfWork = new Mock<IUnitOfWorkRepository>();
            var mockRepository = MockActivityRepository.GetActivityRepository();
            mockUnitOfWork.Setup(x => x.ActivityRepository).Returns(mockRepository.Object);
            return mockUnitOfWork;
        }

        public static Mock<IUnitOfWorkRepository> GetUnitOfWorkListadoUsuarios()
        {
            var mockUnitOfWork = new Mock<IUnitOfWorkRepository>();
            var mockUserRepository = MokcUserRepository.GetUserRepository();
            mockUnitOfWork.Setup(x => x.UserRepository).Returns(mockUserRepository.Object);
            return mockUnitOfWork;
        }
    }
}
