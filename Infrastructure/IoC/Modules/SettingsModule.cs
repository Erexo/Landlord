﻿using Autofac;
using Infrastructure.Extensions;
using Infrastructure.Settings;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.IoC.Modules
{
    public class SettingsModule : Autofac.Module
    {
        private readonly IConfiguration _configuration;

        public SettingsModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(_configuration.GetSettings<UserSettings>())
                   .SingleInstance();
            builder.RegisterInstance(_configuration.GetSettings<AuthenticationSettings>())
                   .SingleInstance();
            builder.RegisterInstance(_configuration.GetSettings<MongoSettings>())
                   .SingleInstance();
        }
    }
}
