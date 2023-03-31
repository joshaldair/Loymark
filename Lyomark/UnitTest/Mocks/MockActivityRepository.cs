using Application.Contracts;
using AutoFixture;
using Domain;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Mocks;

public class MockActivityRepository
{
    public static Mock<IActivityRepository> GetActivityRepository()
    {
        //Creando data
        var fixture = new Fixture();
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        var actividades = fixture.CreateMany<Activity>(5).ToList();
        actividades.ForEach(a => a.UserId = 8001);
        actividades.ForEach(d => d.User = new User { LastName ="good", UserName = "is well",Id = 8001});


        var mockRepository = new Mock<IActivityRepository>();
        mockRepository.Setup(x => x.GetAsync(f => f.IsActive == true)).ReturnsAsync(actividades);
        return mockRepository;
    }

    public static void AddDataActivityRepository(Context lunesContext)
    {
        var fixture = new Fixture();
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        var actividades = fixture.CreateMany<Activity>().ToList();
        actividades.Add(fixture.Build<Activity>()
         .With(tr => tr.Id, 8001)
         .Create()
     );

        lunesContext.Activities!.AddRange(actividades);
        lunesContext.SaveChanges();
    }
}
