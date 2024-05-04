using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Description;

namespace ASM.EmployeeManagement.WebAPIUI.Behavior
{
    /// <summary>
    /// Implements methods that can be used to extend run-time behavior
    /// for an operation in either a service or client application.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    internal sealed class APIOperationBehavior : Attribute, IOperationBehavior
    {
        /// <summary>
        /// Implement to pass data at runtime to bindings to support custom behavior.
        /// </summary>
        /// <param name="operationDescription"></param>
        /// <param name="bindingParameters"></param>
        public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
        {
            return;
        }

        /// <summary>
        /// Implements a modification or extension of the client across an operation.
        /// </summary>
        /// <param name="operationDescription"></param>
        /// <param name="clientOperation"></param>
        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {
            return;
        }

        /// <summary>
        /// Implements a modification or extension of the service across an operation.
        /// </summary>
        /// <param name="operationDescription"></param>
        /// <param name="dispatchOperation"></param>
        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            try
            {
                IOperationBehavior objSerializerBehavior = operationDescription.OperationBehaviors[typeof(DataContractSerializerOperationBehavior)];

                if (dispatchOperation.Formatter == null)
                {
                    objSerializerBehavior.ApplyDispatchBehavior(operationDescription, dispatchOperation);
                }

                IDispatchMessageFormatter objMessageFormatter = dispatchOperation.Formatter;

                dispatchOperation.Formatter = new APIDispatchMessageFormatter(objMessageFormatter);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Implement to confirm that the operation meets some intended criteria.
        /// </summary>
        /// <param name="operationDescription"></param>
        public void Validate(OperationDescription operationDescription)
        {
            return;
        }
    }
}