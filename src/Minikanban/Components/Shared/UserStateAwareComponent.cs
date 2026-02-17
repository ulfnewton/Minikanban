using Microsoft.AspNetCore.Components;
using Minikanban.Services;

namespace Minikanban.Components.Shared;

public abstract class UserStateAwareComponent : ComponentBase, IDisposable
{
    [CascadingParameter] public UserState User { get; set; } = default!;

    protected override void OnInitialized()
    {
        User.OnChangeAsync += HandleUserChangedAsync;
    }

    protected virtual Task UserChanged() => Task.CompletedTask;

    private async Task HandleUserChangedAsync()
    {
        await UserChanged();
        await InvokeAsync(StateHasChanged);
    }

    public void Dispose()
    {
        if (User is not null)
        {
            User.OnChangeAsync -= HandleUserChangedAsync;
        }
    }
}
