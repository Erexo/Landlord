using Autofac;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.IoC.Modules
{
    public class DapperModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(new SqlConnection("user id=SA;password=Easy300c;Data Source=vps.ntsw.pl;Database=Landlord;")).As<IDbConnection>().SingleInstance();
        }
    }
}
