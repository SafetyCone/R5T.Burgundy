using System;

using Microsoft.Extensions.DependencyInjection;

using R5T.Pictia;

using R5T.T0063;


namespace R5T.Burgundy
{
    public static partial class IServiceCollectionExtensions
    {
        public static IServiceCollection AddSftpClientWrapper(this IServiceCollection services,
            IServiceAction<ISftpClientWrapperProvider> sftpClientWrapperProviderAction)
        {
            services
                .Run(sftpClientWrapperProviderAction)
                .AddTransient<SftpClientWrapper>(serviceProvider =>
                {
                    var sftpClientWrapperProvider = serviceProvider.GetRequiredService<ISftpClientWrapperProvider>();

                    var sftpClientWrapper = sftpClientWrapperProvider.GetSftpClientWrapper();
                    return sftpClientWrapper;
                })
                ;

            return services;
        }
    }
}
