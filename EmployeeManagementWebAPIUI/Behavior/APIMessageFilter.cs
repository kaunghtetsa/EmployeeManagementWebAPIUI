using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

using ASM.EmployeeManagement.WebAPIUI.Common.Defines;

namespace ASM.EmployeeManagement.WebAPIUI.Behavior
{
    /// <summary>
    /// APIMessageFilter
    /// </summary>
    internal class APIMessageFilter : MessageFilter
    {
        /// <summary>
        /// Tests whether a message satisfies the filter criteria.
        /// The body cannot be examined.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public override bool Match(Message message)
        {
            string sOldVersion = string.Empty;
            message.Headers.Action = APIInfo.URLUpgrade(message.Headers.Action, out sOldVersion);
            return true;
        }

        /// <summary>
        /// Tests whether a buffered message satisfies the criteria of a filter.
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public override bool Match(MessageBuffer buffer)
        {
            return true;
        }
    }
}