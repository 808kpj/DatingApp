using System.Text.Json;
using API.Helpers;

namespace API.Extentions
{
    public static class HttpExtensions
    {
        public static void AddPaginationHeader<T>(this HttpResponse response, PagedList<T> data)
        {
            var PaginationHeader = new PaginationHeader(data.CurrentPage, data.PageSize, data.TotalCount, data.TotalPages);

            var jsonOptions = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};

            //adds the pagination settings to the http headers
            response.Headers.Append("Pagination", JsonSerializer.Serialize(PaginationHeader, jsonOptions));

            //adds the security access to the response header for pagination
            //if not correct the client wont have access to the pagination
            response.Headers.Append("Access-Control-Expose-Headers", "Pagination");

        }
    }
}