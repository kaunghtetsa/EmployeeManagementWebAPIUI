using System;
using System.IO;
using System.Xml;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

using ASM.EmployeeManagement.WebAPIUI.Model.Common;
using ASM.EmployeeManagement.WebAPIUI.Common.Logger;
using ASM.EmployeeManagement.WebAPIUI.Common.Defines;

namespace ASM.EmployeeManagement.WebAPIUI.Behavior
{
    /// <summary>
    /// Defines methods that deserialize request messages and serialize response messages
    /// in a service application.
    /// </summary>
    internal class APIDispatchMessageFormatter : IDispatchMessageFormatter
    {
        #region Private Members

        private readonly IDispatchMessageFormatter _objMessageFormatter;

        #endregion

        #region Constructor

        /// <summary>
        /// The WCF's default MessageFormatter
        /// </summary>
        /// <param name="objMessageFormatter"></param>
        public APIDispatchMessageFormatter(IDispatchMessageFormatter objMessageFormatter)
        {
            this._objMessageFormatter = objMessageFormatter;
        }

        #endregion

        #region Serialization

        /// <summary>
        /// Deserializes a message into an array of parameters.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="parameters"></param>
        public void DeserializeRequest(Message message, object[] parameters)
        {
            try
            {
                string sServiceReferenceVersion = null;
                Message objMessage = RecreateRequestMessage(message, out sServiceReferenceVersion);
                this._objMessageFormatter.DeserializeRequest(objMessage, parameters);

                foreach (object parm in parameters)
                {
                    if (parm is ClientInfo)
                    {
                        ClientInfo info = parm as ClientInfo;
                        info.ServiceReferenceVersion = sServiceReferenceVersion;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPIHelper.Error(this, ex);
            }
        }

        /// <summary>
        /// Serializes a reply message from a specified message version, array of parameters,
        /// and a return value.
        /// </summary>
        /// <param name="messageVersion"></param>
        /// <param name="parameters"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public Message SerializeReply(MessageVersion messageVersion, object[] parameters, object result)
        {
            Message message = this._objMessageFormatter.SerializeReply(messageVersion, parameters, result);

            string sServiceReferenceVersion = APIInfo.CurrentServiceVersion;

            if (result is ClientInfo)
            {
                ClientInfo info = result as ClientInfo;
                if (string.IsNullOrEmpty(info.ServiceReferenceVersion) == false)
                {
                    sServiceReferenceVersion = info.ServiceReferenceVersion;
                }
            }

            return RecreateResponseMessage(message, sServiceReferenceVersion);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// It will upgarde the message to the current version from the client version
        /// </summary>
        /// <param name="message"></param>
        /// <param name="clientVersion"></param>
        /// <returns></returns>
        private Message RecreateRequestMessage(Message message, out string clientVersion)
        {
            try
            {
                clientVersion = null;

                XDocument objXDocument = XDocument.Load(new StringReader(message.ToString()), LoadOptions.PreserveWhitespace);
                IEnumerable<XElement> lstXElement = objXDocument.Root
                    .Descendants()
                    .Where(W => W.Name.Namespace.ToString().Contains(APIInfo.NameSpace));

                foreach (XElement element in lstXElement)
                {
                    string sXMLNS = APIInfo.URLUpgrade(element.Name.Namespace.ToString(), out clientVersion);
                    element.Attributes("xmlns").Remove();
                    element.Name = string.Concat("{", sXMLNS, "}", element.Name.LocalName);
                }

                XmlReader objNewReader = XmlReader.Create(new StringReader(objXDocument.ToString()));
                Message objNewMessage = Message.CreateMessage(objNewReader, int.MaxValue, message.Version);
                return objNewMessage;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// It will downgrade the the message from the current version to the client version
        /// </summary>
        /// <param name="message"></param>
        /// <param name="clientVersion"></param>
        /// <returns></returns>
        private Message RecreateResponseMessage(Message message, string clientVersion)
        {
            try
            {
                XDocument objXDocument = XDocument.Load(new StringReader(message.ToString()));

                IEnumerable<XElement> lstXElement = objXDocument.Root
                    .Descendants()
                    .Where(W => W.Name.Namespace.ToString().Contains(APIInfo.NameSpace) || W.Name.LocalName == "Action");

                foreach (XElement element in lstXElement)
                {
                    if (element.Name.Namespace.ToString().Contains(APIInfo.NameSpace))
                    {
                        string sXMLNS = APIInfo.URLDowngrade(element.Name.Namespace.ToString(), clientVersion);
                        element.Attributes("xmlns").Remove();
                        element.Name = string.Concat("{", sXMLNS, "}", element.Name.LocalName);
                    }
                    else if (element.Name.LocalName == "Action")
                    {
                        element.Value = APIInfo.URLDowngrade(element.Value, clientVersion);
                    }
                }

                XmlReader objNewReader = XmlReader.Create(new StringReader(objXDocument.ToString()));
                Message objNewMessage = Message.CreateMessage(objNewReader, int.MaxValue, message.Version);
                return objNewMessage;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}