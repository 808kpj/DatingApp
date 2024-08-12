using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class PaginationHeader(int CurrentPage, int itemsPerPage, int totalItems, int totalPages)
    {
        public int CurrentPage { get; set; } = CurrentPage;
        public int itemsPerPage { get; set; } = itemsPerPage;
        public int totalItems { get; set; } = totalItems;
        public int TotalPages { get; set; } = totalPages;
    }
}