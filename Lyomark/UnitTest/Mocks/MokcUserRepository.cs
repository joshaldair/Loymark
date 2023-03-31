using Application.Contracts;
using AutoFixture;
using Domain;
using Infrastructure.Persistance;
using Moq;

namespace UnitTest.Mocks;

public class MokcUserRepository
{
    public static Mock<IUserRepository> GetUserRepository()
    {
        //Creando data
        var fixture = new Fixture();
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        var articulos = fixture.CreateMany<User>(5).ToList();
        var mockRepository = new Mock<IUserRepository>();
        mockRepository.Setup(x => x.GetAsync(f => f.IsActive == true)).ReturnsAsync(articulos);
        return mockRepository;
    }

    public static void AddDataUserRepository(Context lunesContext)
    {
        var fixture = new Fixture();
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        var users = fixture.CreateMany<User>().ToList();
        users.Add(fixture.Build<User>()
         .With(tr => tr.Id, 8001)
         .Create()
     );

        lunesContext.Users!.AddRange(users);
        lunesContext.SaveChanges();
    }
}
