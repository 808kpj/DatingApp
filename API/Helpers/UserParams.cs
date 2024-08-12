using Microsoft.AspNetCore.Mvc.Formatters;

namespace API.Helpers
{
    public class UserParams
    {
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            //will check the current set value "(value)" then check to see if it is above the max number of pages
            ///if it is it will change if its lower then MaxPageSize
            set =>  _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public string? Gender { get; set; }
        public string? CurrentUsername { get; set; }
        public int MinAge { get; set; } = 18;
        public int MaxAge { get; set; } = 100;
        public string OrderBy { get; set; } = "lastActive";

    }
}