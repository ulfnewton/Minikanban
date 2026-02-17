using Microsoft.AspNetCore.Components;
using Minikanban.Services;

namespace Minikanban.Components.Shared;

public abstract partial class StateAwareComponent : ComponentBase, IDisposable
{
    [CascadingParameter] public KanbanState State { get; set; } = default!;

    protected override void OnInitialized()
    {
        State.OnChangeAsync += HandleStateChangesAsync;
    }

    private async Task HandleStateChangesAsync()
    {
        await StateChanged();
        await InvokeAsync(StateHasChanged);
    }

    protected virtual Task StateChanged() => Task.CompletedTask;

    public void Dispose()
    {
        if (State is not null)
        {
            State.OnChangeAsync -= HandleStateChangesAsync;
        }
    }
}
