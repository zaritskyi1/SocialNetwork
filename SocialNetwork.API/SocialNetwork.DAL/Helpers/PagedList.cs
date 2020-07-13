using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SocialNetwork.DAL.Helpers
{
    public class PagedList<T>
    {
        public PagedListInfo Information { get; }
        public List<T> Result { get; }

        public PagedList(List<T> result, int count, int pageNumber, int pageSize)
        {
            Information = new PagedListInfo()
            {
                TotalItems = count,
                ItemsPerPage = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };
            
            Result = result;
        }

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, QueryOptions queryOptions)
        {
            var count = await source.CountAsync();
            var items = await source
                .Skip((queryOptions.PageNumber - 1) * queryOptions.PageSize)
                .Take(queryOptions.PageSize)
                .ToListAsync();

            return new PagedList<T>(items, count, queryOptions.PageNumber, queryOptions.PageSize);
        }
    }
}
