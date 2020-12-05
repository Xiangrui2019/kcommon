using System;
using KCommon.Web.ErrorCode;
using KCommon.Web.Models.Message;
using Microsoft.AspNetCore.Http;

namespace Handler.Exceptions
{
    public class UnexpectedException : Exception
    {
        public MessageModel Response { get; set; }
        public ErrorType Code => Response.Code;

        public UnexpectedException(MessageModel model) : base(model.Message)
        {
            Response = model;
        }
    }
}