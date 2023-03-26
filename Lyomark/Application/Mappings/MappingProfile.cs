using Application.Features.Activities.Commands;
using Application.Features.Activities.Queries.DTO;
using Application.Features.Users.Commands;
using Application.Features.Users.Queries.DTO;
using Domain;


namespace Application.Mappings
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            // Ativity
            CreateMap<CreateActivityCommand, Activity>();
            CreateMap<DeleteActivityCommand, Activity>();
            CreateMap<Activity, ActivityDTO>();

            // User
            CreateMap<CreateUserCommand, User>();
            CreateMap<DeleteUserCommand, Activity>();
            CreateMap<User, UserDTO>();
        }
    }
}
