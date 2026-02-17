using Minikanban.Models;

namespace Minikanban.Services
{
    public sealed class KanbanState
    {
        public List<KanbanColumn> Columns { get; } = new()
        {
            new KanbanColumn{ Name = "Todo" },
            new KanbanColumn{ Name = "Doing" },
            new KanbanColumn{ Name ="Done" }
        };

        public event Func<Task>? OnChangeAsync;

        public async Task AddCardAsync(string columnName, KanbanCard card)
        {
            var column = Columns.First(column => column.Name == columnName);
            column.Cards.Add(card);
            await NotifyStateChangedAsync();
        }

        public async Task MoveCardAsync(string from, string to, Guid cardId)
        {
            var source = Columns.First(column => column.Name == from);
            var target = Columns.First(column => column.Name == to);
            var card = source.Cards.First(card => card.Id == cardId);

            source.Cards.Remove(card);
            target.Cards.Add(card);
            await NotifyStateChangedAsync();
        }

        private async Task NotifyStateChangedAsync()
        {
            OnChangeAsync?.Invoke();
        }
    }
}
