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


namespace R5T.Burgundy.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds services necessary to acquire an <see cref="SftpClientWrapper"/> instance configured using AWS EC2 secrets from the Rivet organization directory in Dropbox.
        /// </summary>
        public static IServiceCollection UseSftpClientWrapper(this IServiceCollection services)
        {
            services
                .AddSingleton<SftpClientWrapper>(serviceProvider =>
                {
                    var sftpClientWrapperProvider = serviceProvider.GetRequiredService<ISftpClientWrapperProvider>();

                    var sftpClientWrapper = sftpClientWrapperProvider.GetSftpClientWrapper();
                    return sftpClientWrapper;
                })
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
                ;

            return services;
        }
    }
}
