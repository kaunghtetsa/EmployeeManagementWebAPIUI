using ASM.EmployeeManagement.Common.Validation;
using ASM.EmployeeManagement.WebAPIUI.Common.Exception;

namespace ASM.EmployeeManagement.WebAPIUI.Validation
{
    public abstract class APIValidatorBase
    {
        /// <summary>
        /// IsNullOrEmptyAndValidLength
        /// </summary>
        /// <param name="value"></param>
        /// <param name="minVal"></param>
        /// <param name="maxVal"></param>
        /// <param name="fieldName"></param>
        protected static void IsNullOrEmptyAndValidLength(string value, int minVal, int maxVal, string fieldName)
        {
            try
            {
                InputParameterException ex = null;

                // Check empty string
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    ex = new InputParameterException(InputParameterException.MessageIDType.E000, new string[] { fieldName });
                }
                // Check valid length
                else if(!ValidationUtil.IsValidLen(value, minVal, maxVal))
                {
                    ex = new InputParameterException(InputParameterException.MessageIDType.E000, new string[] { fieldName });
                }

                if(ex != null)
                {
                    throw ex;
                }
            }
            catch (InputParameterException)
            {
                throw;
            }
        }

		/// <summary>
		/// IsValidIntervalValue
		/// </summary>
		/// <param name="value"></param>
		/// <param name="minVal"></param>
		/// <param name="maxVal"></param>
		/// <param name="fieldName"></param>
		protected static void IsValidIntervalValue(int value, int minVal, int maxVal, string fieldName)
		{
			try
			{
				InputParameterException ex = null;

				// Check interval
				if (value<minVal || value> maxVal)
				{
					ex = new InputParameterException(InputParameterException.MessageIDType.E000, new string[] { fieldName });
				}
				

				if (ex != null)
				{
					throw ex;
				}
			}
			catch (InputParameterException)
			{
				throw;
			}
		}
	}
}