
namespace AikoLearning.Core.Application.Base;

public class PagedResult<T>
{
    public IEnumerable<T>? Items { get; set; }
    public int PageNumber { get; set; }
    public int PageLimit { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageLimit);

    public PagedResult(IEnumerable<T> items, int pageNumber, int pageLimit, int totalCount)
    {
        Items = items;
        PageNumber = pageNumber;
        PageLimit = pageLimit;
        TotalCount = totalCount;
    }
}