using System.Collections.Generic;
using System.Linq;

namespace Pagination.WebApi.Wrappers
{
    public class Response<T>
    {
        public Response()
        {

        }
        internal Response(bool succeeded, string message, IEnumerable<string> errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
            Message = message;
        }
        public T Data { get; set; }
        public bool Succeeded { get; set; }

        public string[] Errors { get; set; }
        public string Message { get; set; }

        public static Response<T> Success(string message)
        {
            return new Response<T>(true, message, new string[] { });
        }

        public static Response<T> Failure(IEnumerable<string> errors)
        {
            return new Response<T>(false, string.Empty, errors);
        }
    }
}
