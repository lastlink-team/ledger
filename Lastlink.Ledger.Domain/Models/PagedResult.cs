namespace Lastlink.Ledger.Domain.Models;

public class PagedResult<T>
{
    private List<T>? _items;

    public List<T> Items
    {
        get => _items ?? [];
        init => _items = value;
    }

    public int Page { get; init; }
    public int Limit { get; init; }
}
