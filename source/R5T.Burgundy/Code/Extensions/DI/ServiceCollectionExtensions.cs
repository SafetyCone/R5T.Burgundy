using System;

using Microsoft.Extensions.DependencyInjection;

using R5T.Alamania;
using R5T.Alamania.Bulgaria;
using R5T.Bulgaria;
using R5T.Bulgaria.Default.Local;
using R5T.Costobocia;
using R5T.Costobocia.Default;
using R5T.Frisia;
using R5T.Frisia.Suebia;
using R5T.Jutland;
using R5T.Jutland.Newtonsoft;
using R5T.Lombardy;
using R5T.Pictia;
using R5T.Pictia.Frisia;
using R5T.Suebia;
using R5T.Suebia.Alamania;
using R5T.Visigothia;
using R5T.Visigothia.Default.Local;


namespace R5T.Burgundy.Code.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UseSftpClientWrapper(this IServiceCollection services)
        {
            services
                .AddSingleton<ISftpClientWrapperProvider, FrisiaSftpClientWrapperProvider>()
                .AddSingleton<IAwsEc2ServerSecretsProvider, SuebiaAwsEc2ServerSecretsProvider>()
                .AddSingleton<IAwsEc2ServerSecretsFileNameProvider, HardCodedAwsEc2ServerSecretsFileNameProvider>()
                .AddSingleton<ISecretsFilePathProvider, AlamaniaSecretsFilePathProvider>()
                .AddSingleton<IJsonFileSerializationOperator, NewtonsoftJsonFileSerializationOperator>()
                .AddSingleton<IAwsEc2ServerHostFriendlyNameProvider>((serviceProviderInstance) =>
                {
                    var output = new InstanceAwsEc2ServerHostFriendlyNameProvider("TempTest");
                    return output;
                })
                .AddSingleton<ISecretsDirectoryPathProvider, AlamaniaSecretsDirectoryPathProvider>()
                .AddSingleton<IStringlyTypedPathOperator, StringlyTypedPathOperator>()
                .AddSingleton<IRivetOrganizationDirectoryPathProvider, BulgariaRivetOrganizationDirectoryPathProvider>()
                .AddSingleton<IDropboxDirectoryPathProvider, DefaultLocalDropboxDirectoryPathProvider>()
                .AddSingleton<IOrganizationStringlyTypedPathOperator, DefaultOrganizationStringlyTypedPathOperator>()
                .AddSingleton<IUserProfileDirectoryPathProvider, DefaultLocalUserProfileDirectoryPathProvider>()
                .AddSingleton<IOrganizationsStringlyTypedPathOperator, DefaultOrganizationsStringlyTypedPathOperator>()
                .AddSingleton<IOrganizationDirectoryNameProvider, DefaultOrganizationDirectoryNameProvider>()


                .BuildServiceProvider()
                ;

            return services;
        }
    }
}
