using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace FMS.ServiceHosting.Behaviors.Behaviors
{
    internal class ServiceBehaviorErrorHandler : IServiceBehavior, IErrorHandler
    {
        public void AddBindingParameters(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
            return;
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher chanDisp in serviceHostBase.ChannelDispatchers)
            {
                chanDisp.ErrorHandlers.Add(this);
            }
        }

        public void Validate(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {
            return;

            //Validating each operation description.

            //foreach (var svcEndPt in serviceDescription.Endpoints)
            //{
            //    if (svcEndPt.Contract.Name != "IMetadataExchange")
            //    {
            //        foreach (var opDesc in svcEndPt.Contract.Operations)
            //        {
            //            if (opDesc.IsOneWay == false && opDesc.Faults.Count == 0)
            //            {
            //                string msg = string.Format("Requires a faultcontract(typeof(BusError)). The '{0}' contains no faultcontracts ", opDesc.Name);
            //                throw new InvalidOperationException(msg);
            //            }
            //        }
            //    }
            //}
        }

        public bool HandleError(Exception error)
        {
            if (error is FaultException)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void ProvideFault(Exception error, System.ServiceModel.Channels.MessageVersion version, ref System.ServiceModel.Channels.Message fault)
        {
            if (!(error is FaultException))
            {
                MessageFault msgFault = null;

                //BusError busError = new BusError();

                //busError.Errors.Add(new ErrorInfo(1, error.Message, error.Source));

                //FaultException<BusError> fex = new FaultException<BusError>(busError, "Some thing bad happened", new FaultCode("Application fault"));

                //msgFault = fex.CreateMessageFault();

                //fault = Message.CreateMessage(version, msgFault, typeof(BusError).Name); // "http://tempuri.org/IServerStock/a"
            }
        }
    }
}
