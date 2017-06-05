using Autofac;
using AutoMapper;
using Core.Domain;
using Infrastructure.DTO;

namespace Infrastructure.IoC.Modules
{
    public class AutoMapperModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(new MapperConfiguration(config =>
            {
                config.CreateMap<User, UserDTO>();
                config.CreateMap<User, ExtendedUserDTO>();
            })
            .CreateMapper()).SingleInstance();
        }
    }
}
