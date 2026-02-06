
using Minikanban.Models;

namespace Minikanban.Services;

public class KanbanState
{
    public List<KanbanColumn> Columns { get; } =
    [
        new KanbanColumn { Name = "Todo" },
        new KanbanColumn { Name = "Doing" },
        new KanbanColumn { Name = "Done" }
    ];

    public event Func<Task>? OnChangeAsync;

    public async Task AddCardAsync(string columnName, KanbanCard card)
    {
        var column = Columns.First(c => c.Name == columnName);
        column.Cards.Add(card);
        await NotifyStateChangedAsync();
    }

    public async Task MoveCardAsync(string from, string to, Guid cardId)
    {
        var source = Columns.First(c => c.Name == from);
        var target = Columns.First(c => c.Name == to);
        var card = source.Cards.First(c => c.Id == cardId);
        source.Cards.Remove(card);
        target.Cards.Add(card);
        await NotifyStateChangedAsync();
    }

    private Task NotifyStateChangedAsync() => OnChangeAsync?.Invoke() ?? Task.CompletedTask;
}
