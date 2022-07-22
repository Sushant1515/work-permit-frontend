using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkFlowEntry.Models
{
    //public class APIResponse
    //{
    //    public List<WorkFlowMaster> Data { get; set; }
    //    public bool Succeeded { get; set; }
    //    public string Message { get; set; }

    //    public int StatusCode { get; set; }

    //    public static APIResponse Fail(string errorMessage)
    //    {
    //        return new APIResponse { Succeeded = false, Message = errorMessage };
    //    }
    //    public static APIResponse Success (List<WorkFlowMaster>  data)
    //    {
    //        return new APIResponse { Succeeded = true, Data = data };
    //    }
    //}
    public class APIResponse
    {
        private int _ResponseCode;

        private string _ResponseMessage;

        public int ResponseCode
        {
            get { return _ResponseCode; }
            set { _ResponseCode = value; }
        }

        public string ResponseMessage
        {
            get { return _ResponseMessage; }
            set { _ResponseMessage = value; }
        }

        private object _ResponseObject;
        public object ResponseObject
        {
            get { return _ResponseObject; }
            set { _ResponseObject = value; }
        }
    }
    public enum enumResponseCode
    {
        // Success COde
        OK = 200,
        OtpSent = 201,
        Success = 202,
        DataSaved = 203,

        // Error Code
        AccessDenied = 401,
        ErrorOccour = 402,
        NotFound = 404,
        DataAlreadyExist = 409,
        PasswordDoesNotMatch = 421,
        CartEmpty = 422,
        RequiredValuNotEntered = 423,
        DataFormatInvalid = 424,
        NotProcessed = 425,
        TransactionPending = 426,
        TransactionFailed = 427,
        InternalServerError=500

    }

}
