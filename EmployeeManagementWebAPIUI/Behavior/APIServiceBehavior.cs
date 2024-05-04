using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Collections.ObjectModel;
using System.ServiceModel.Description;

namespace ASM.EmployeeManagement.WebAPIUI.Behavior
{
    internal sealed class APIServiceBehavior : Attribute, IServiceBehavior
    {
        /// <summary>
        /// Provides the ability to pass custom data to binding elements to support the contract implementation.
        /// </summary>
        /// <param name="serviceDescription"></param>
        /// <param name="serviceHostBase"></param>
        /// <param name="endpoints"></param>
        /// <param name="bindingParameters"></param>
        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
            return;
        }

        /// <summary>
        /// Provides the ability to change run-time property values or insert custom extension objects such as error handlers,
        /// message or parameter interceptors, security extensions, and other custom extension objects.
        /// </summary>
        /// <param name="serviceDescription"></param>
        /// <param name="serviceHostBase"></param>
        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ServiceEndpoint serviceEndpoint in serviceHostBase.Description.Endpoints)
            {
                serviceEndpoint.EndpointBehaviors.Add(new APIEndpointBehavior());
            }
        }

        /// <summary>
        /// Provides the ability to inspect the service host and the service description to
        /// confirm that the service can run successfully.
        /// </summary>
        /// <param name="serviceDescription"></param>
        /// <param name="serviceHostBase"></param>
        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            return;
        }
    }
}