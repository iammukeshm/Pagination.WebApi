using Pagination.WebApi.Filter;
using Pagination.WebApi.Services;
using Pagination.WebApi.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pagination.WebApi.Helpers
{
    public static class PaginationHelper
    {
        public static PagedResponse<List<T>> CreatePagedReponse<T>(List<T> data,PaginationFilter filter, IUriService uriService, string route)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = data
               .Skip((filter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               .ToList();

            var respose = new PagedResponse<List<T>>(pagedData, validFilter.PageNumber, validFilter.PageSize);
            var totalRecords = data.Count;
            var totalPages = (totalRecords / validFilter.PageSize);
            respose.NextPage =
                validFilter.PageNumber >= 1 && validFilter.PageNumber < totalPages
                ? uriService.GetPageUri(new PaginationFilter(validFilter.PageNumber + 1, validFilter.PageSize), route)
                : null;
            respose.PreviousPage =
                validFilter.PageNumber - 1 >= 1 && validFilter.PageNumber <= totalPages
                ? uriService.GetPageUri(new PaginationFilter(validFilter.PageNumber - 1, validFilter.PageSize), route)
                : null;



            respose.FirstPage = uriService.GetPageUri(new PaginationFilter(1, validFilter.PageSize), route);
            respose.LastPage = uriService.GetPageUri(new PaginationFilter(totalPages, validFilter.PageSize), route);
            respose.TotalPages = totalPages;
            respose.TotalRecords = totalRecords;
            return respose;
        }
    }
}
