using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Models
{
    public class Pager
    {
        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int Endpage { get; private set; }
        public Pager()
        {

        }
        public Pager(int totalitems, int page, int pageSize = 5)
        {
            int totalPages = (int)Math.Ceiling((decimal)totalitems / (decimal)pageSize);
            int currentPage = page;
            int startpage = currentPage - 5;
            int endPage = currentPage + 4;
            if (startpage <= 0)
            {
                endPage = endPage - (startpage - 1);
                startpage = 1;
            }
            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {
                    startpage = endPage - 9;
                }
            }
            TotalItems = totalitems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startpage;
            Endpage = endPage;
        }
    }
}
