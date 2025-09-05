using Application.Adapters.Internals;
using Application.Interfaces.Internals;
using Domain.Exceptions;

namespace Application.Helpers
{
    public class ResponseHelper : IResponseHelper
    {
        public DataResponse errorSimpleServidor(string cErrorCode, string cErrorMessage)
        {
            var result = new DataResponse();
            var errorMessage = new List<ErrorResponse>();

            errorMessage.Add(new ErrorResponse
            {
                Code = cErrorCode,
                Message = cErrorMessage
            });

            result.ResponseCode = 501;
            result.Success = 0;
            result.Message = MessageException.GetErrorByCode(501);
            result.Errors = errorMessage;
            result.Data = null;

            return result;
        }

        public DataResponse errorSimpleClient(string cErrorCode, string cErrorMessage)
        {
            var result = new DataResponse();
            var errorMessage = new List<ErrorResponse>();

            errorMessage.Add(new ErrorResponse
            {
                Code = cErrorCode,
                Message = cErrorMessage
            });

            result.ResponseCode = 409;
            result.Success = 0;
            result.Message = MessageException.GetErrorByCode(409);
            result.Errors = errorMessage;
            result.Data = null;

            return result;
        }

        public DataResponse errorSimpleClientAuth(string cErrorCode, string cErrorMessage, int nResponseCode)
        {
            var result = new DataResponse();
            var errorMessage = new List<ErrorResponse>();

            errorMessage.Add(new ErrorResponse
            {
                Code = cErrorCode,
                Message = cErrorMessage
            });

            result.ResponseCode = nResponseCode;
            result.Success = 0;
            result.Message = MessageException.GetErrorByCode(400);
            result.Errors = errorMessage;
            result.Data = null;

            return result;
        }

        public DataResponse errorList(dynamic errorList)
        {
            var result = new DataResponse();

            result.ResponseCode = 400;
            result.Success = 0;
            result.Message = MessageException.GetErrorByCode(400);
            result.Errors = errorList;
            result.Data = null;

            return result;
        }
        public DataResponse emptyResponse()
        {
            var result = new DataResponse();

            result.ResponseCode = 204;
            result.Success = 2;
            result.Message = MessageException.GetErrorByCode(204);
            result.Errors = null;
            result.Data = null;

            return result;
        }

        public DataResponse successResponse(dynamic dataResponse)
        {
            var result = new DataResponse();

            result.ResponseCode = 200;
            result.Success = 1;
            result.Message = MessageException.GetErrorByCode(200);
            result.Data = dataResponse;
            result.Errors = null;

            return result;
        }
    }
}
