using Application.Adapters.Internals;

namespace Application.Interfaces.Internals
{
    public interface IResponseHelper
    {
        public DataResponse errorSimpleServidor(string cErrorCode, string cErrorMessage);
        public DataResponse errorSimpleClient(string cErrorCode, string cErrorMessage);
        public DataResponse errorSimpleClientAuth(string cErrorCode, string cErrorMessage, int nResponseCode);
        public DataResponse errorList(dynamic errorList);
        public DataResponse emptyResponse();
        public DataResponse successResponse(dynamic dataResponse);
    }
}
