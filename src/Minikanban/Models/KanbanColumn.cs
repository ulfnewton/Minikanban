namespace Minikanban.Models;

public class KanbanColumn
{
    public string Name { get; set; } = "";
    public List<KanbanCard> Cards { get; set; } = new();
}