using SimpleUber.Errors.ErrorCodes;
using System;
using System.Collections.Generic;

namespace SimpleUberWebApi.Distribution.Client.ServiceResponseException
{
    public class ServiceResponseException : Exception
    {
        public List<ErrorCode> ErrorCodes { get; private set; }

        public ServiceResponseException(List<ErrorCode> errorCodes)
        {
            ErrorCodes = errorCodes;
        }
    }
}
