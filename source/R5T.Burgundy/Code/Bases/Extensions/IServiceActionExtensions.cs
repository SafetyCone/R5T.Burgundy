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
using R5T.Suebia.Default;
using R5T.Visigothia;
using R5T.Visigothia.Default.Local;

using R5T.T0062;
using R5T.T0063;

namespace R5T.Burgundy
{
    public static class IServiceActionExtensions
    {
        public static (
            IServiceAction<IAwsEc2ServerHostFriendlyNameProvider> awsEc2ServerHostFriendlyNameProviderAction,
            IServiceAction<IAwsEc2ServerSecretsFileNameProvider> awsEc2ServerSecretsFileNameProviderAction,
            IServiceAction<IAwsEc2ServerSecretsProvider> awsEc2ServerSecretsProviderAction,
            IServiceAction<IDropboxDirectoryPathProvider> dropboxDirectoryPathProviderAction,
            IServiceAction<IJsonFileSerializationOperator> jsonFileSerializationOperatorAction,
            IServiceAction<IOrganizationDirectoryNameProvider> organizationDirectoryNameProviderAction,
            IServiceAction<IOrganizationsStringlyTypedPathOperator> organizationsStringlyTypedPathOperatorAction,
            IServiceAction<IOrganizationStringlyTypedPathOperator> organizationStringlyTypedPathOperatorAction,
            IServiceAction<IRivetOrganizationDirectoryPathProvider> rivetOrganizationDirectoryPathProviderAction,
            IServiceAction<IRivetOrganizationSecretsDirectoryPathProvider> rivetOrganizationSecretsDirectoryPathProviderAction,
            IServiceAction<ISecretsDirectoryFilePathProvider> secretsDirectoryFilePathProviderAction,
            IServiceAction<ISecretsDirectoryPathProvider> secretsDirectoryPathProviderAction,
            IServiceAction<SftpClientWrapper> sftpClientWrapperAction,
            IServiceAction<ISftpClientWrapperProvider> sftpClientWrapperProviderAction,
            IServiceAction<IUserProfileDirectoryPathProvider> userProfileDirectoryPathProviderAction
            )
        AddSftpClientWrapper(this IServiceAction _,
            IServiceAction<IStringlyTypedPathOperator> stringlyTypedPathOperatorAction)
        {
            var awsEc2ServerHostFriendlyNameProviderAction = _.AddInstanceAwsEc2ServerHostFriendlyNameProviderAction("TempTest");
            var awsEc2ServerSecretsFileNameProviderAction = _.AddHardCodedAwsEc2ServerSecretsFileNameProviderAction();
            var jsonFileSerializationOperatorAction = _.AddNewtonsoftJsonFileSerializationOperatorAction();
            var organizationDirectoryNameProviderAction = _.AddOrganizationDirectoryNameProviderAction();
            var userProfileDirectoryPathProviderAction = _.AddLocalUserProfileDirectoryPathProviderAction();

            var dropboxDirectoryPathProviderAction = _.AddLocalDropboxDirectoryPathProviderAction(
                stringlyTypedPathOperatorAction,
                userProfileDirectoryPathProviderAction);
            var organizationsStringlyTypedPathOperatorAction = _.AddOrganizationsStringlyTypedPathOperatorAction(
                stringlyTypedPathOperatorAction);

            var organizationStringlyTypedPathOperatorAction = _.AddOrganizationStringlyTypedPathOperatorAction(
                organizationDirectoryNameProviderAction,
                organizationsStringlyTypedPathOperatorAction,
                stringlyTypedPathOperatorAction);

            var rivetOrganizationDirectoryPathProviderAction = _.AddRivetOrganizationDirectoryPathProviderAction(
                dropboxDirectoryPathProviderAction,
                organizationStringlyTypedPathOperatorAction);

            var rivetOrganizationSecretsDirectoryPathProviderAction = _.AddRivetOrganizationSecretsDirectoryPathProviderAction(
                rivetOrganizationDirectoryPathProviderAction,
                stringlyTypedPathOperatorAction);

            var secretsDirectoryPathProviderAction = _.ForwardToSecretsDirectoryPathProviderAction(
                rivetOrganizationSecretsDirectoryPathProviderAction);

            var secretsDirectoryFilePathProviderAction = _.AddSecretsDirectoryFilePathProviderAction(
                secretsDirectoryPathProviderAction,
                stringlyTypedPathOperatorAction);

            var awsEc2ServerSecretsProviderAction = _.AddSuebiaAwsEc2ServerSecretsProviderAction(
                awsEc2ServerHostFriendlyNameProviderAction,
                awsEc2ServerSecretsFileNameProviderAction,
                secretsDirectoryFilePathProviderAction,
                jsonFileSerializationOperatorAction);

            var sftpClientWrapperProviderAction = _.AddFrisiaSftpClientWrapperProviderAction(
                awsEc2ServerSecretsProviderAction);

            var sftpClientWrapperAction = _.ForwardToSftpClientWrapperAction(
                sftpClientWrapperProviderAction);

            return (
                awsEc2ServerHostFriendlyNameProviderAction,
                awsEc2ServerSecretsFileNameProviderAction,
                awsEc2ServerSecretsProviderAction,
                dropboxDirectoryPathProviderAction,
                jsonFileSerializationOperatorAction,
                organizationDirectoryNameProviderAction,
                organizationsStringlyTypedPathOperatorAction,
                organizationStringlyTypedPathOperatorAction,
                rivetOrganizationDirectoryPathProviderAction,
                rivetOrganizationSecretsDirectoryPathProviderAction,
                secretsDirectoryFilePathProviderAction,
                secretsDirectoryPathProviderAction,
                sftpClientWrapperAction,
                sftpClientWrapperProviderAction,
                userProfileDirectoryPathProviderAction);
        }

        /// <summary>
        /// Forwards the <see cref="ISftpClientWrapperProvider"/> service to <see cref="SftpClientWrapper"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<SftpClientWrapper> ForwardToSftpClientWrapperAction(this IServiceAction _,
            IServiceAction<ISftpClientWrapperProvider> sftpClientWrapperProviderAction)
        {
            var serviceAction = _.New<SftpClientWrapper>(services => services.AddSftpClientWrapper(
                sftpClientWrapperProviderAction));

            return serviceAction;
        }
    }
}
