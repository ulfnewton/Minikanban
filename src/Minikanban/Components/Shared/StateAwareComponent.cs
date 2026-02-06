
using Microsoft.AspNetCore.Components;
using Minikanban.Services;
using System;
using System.Threading.Tasks;

namespace Minikanban.Components.Shared;

public abstract class StateAwareComponent : ComponentBase, IDisposable
{
    [CascadingParameter] public KanbanState State { get; set; } = default!;

    protected override void OnInitialized()
    {
        State.OnChangeAsync += HandleStateChangedAsync;
    }

    protected virtual Task StateChanged() => Task.CompletedTask;

    private async Task HandleStateChangedAsync()
    {
        await StateChanged();
        await InvokeAsync(StateHasChanged);
    }

    public void Dispose()
    {
        if (State is not null)
        {
            State.OnChangeAsync -= HandleStateChangedAsync;
        }
    }
}
