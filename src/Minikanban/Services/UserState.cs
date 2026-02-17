
using System;
using System.Threading.Tasks;

namespace Minikanban.Services;

public class UserState
{
    public string Role { get; private set; } = "Visitor";

    public event Func<Task>? OnChangeAsync;

    public async Task SetRoleAsync(string role)
    {
        Role = role;
        await (OnChangeAsync?.Invoke() ?? Task.CompletedTask);
    }
}
