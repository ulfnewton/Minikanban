
# Dataflöde i Blazor

[⟵ Till översikten](README.md)

## Unidirektionellt dataflöde (Parent → Child)

```mermaid
flowchart LR
    Parent[Parent Component Data items] -->|Parameter| Child1[Child Component Items Parameter]
    Parent -->|Parameter| Child2[Child Component Items Parameter]
    Child1 -.->|EventCallback| Parent
    Child2 -.->|EventCallback| Parent

    style Parent fill:#1976D2,color:#fff,stroke:#0D47A1,stroke-width:2px
    style Child1 fill:#F57C00,color:#fff,stroke:#E65100,stroke-width:2px
    style Child2 fill:#F57C00,color:#fff,stroke:#E65100,stroke-width:2px
```

**Princip:** Data flödar nedåt via `Parameter`/`CascadingParameter`; händelser flödar uppåt via `EventCallback`.

### 1. Parameters (Parent → Child)

```razor
<!-- Parent Component -->
<ChildComponent Title="Min Titel" Count="@itemCount" />

<!-- Child Component -->
@code {
    [Parameter] public string Title { get; set; } = string.Empty;
    [Parameter] public int Count { get; set; }
}
```

### 2. EventCallback (Child → Parent)

```csharp
// Child Component
[Parameter] public EventCallback<string> OnItemClicked { get; set; }

private async Task HandleClick(string item)
{
    await OnItemClicked.InvokeAsync(item);
}
```

```razor
<!-- Parent Component -->
<ChildComponent OnItemClicked="HandleItemClicked" />

@code {
    private void HandleItemClicked(string item)
    {
        // Hantera event från child
    }
}
```

### 3. Cascading Parameters (Ancestor → Descendants)

```mermaid
flowchart TD
    Root[Root Component CascadingValue] ==> |State| Level1A[Component A]
    Root ==> |State| Level1B[Component B]
    Level1A ==> |State| Level2A[Component C]
    Level1A ==> |State| Level2B[Component D]
    Level1B ==> |State| Level2C[Component E]

    style Root fill:#388E3C,color:#fff,stroke:#1B5E20,stroke-width:2px
    style Level1A fill:#F57C00,color:#fff,stroke:#E65100,stroke-width:2px
    style Level1B fill:#F57C00,color:#fff,stroke:#E65100,stroke-width:2px
    style Level2A fill:#D84315,color:#fff,stroke:#BF360C,stroke-width:2px
    style Level2B fill:#D84315,color:#fff,stroke:#BF360C,stroke-width:2px
    style Level2C fill:#D84315,color:#fff,stroke:#BF360C,stroke-width:2px
```

**Regel:** Cascading parameters kan inte passera över render mode-gränser.

```razor
<!-- Provider (Ancestor) -->
<CascadingValue Value="@sharedState">
    @ChildContent
</CascadingValue>

<!-- Consumer (Descendant) -->
@code {
    [CascadingParameter] public SharedState State { get; set; } = default!;
}
```

### 4. Observerbart state (service + notifikation)

```mermaid
sequenceDiagram
    participant User
    participant Component
    participant State
    participant Others

    User->>Component: Interagerar
    Component->>State: UpdateAsync()
    State->>State: Uppdatera data
    State->>State: NotifyStateChangedAsync()
    State-->>Component: OnChangeAsync event
    State-->>Others: OnChangeAsync event
    Component->>Component: StateHasChanged()
    Others->>Others: StateHasChanged()
```

```csharp
public class AppState
{
    public event Func<Task>? OnChangeAsync;

    public async Task UpdateAsync()
    {
        // Uppdatera data
        await NotifyStateChangedAsync();
    }

    private Task NotifyStateChangedAsync() =>
        OnChangeAsync?.Invoke() ?? Task.CompletedTask;
}

protected override void OnInitialized()
{
    State.OnChangeAsync += HandleStateChangedAsync;
}

private async Task HandleStateChangedAsync()
{
    await InvokeAsync(StateHasChanged);
}

public void Dispose()
{
    State.OnChangeAsync -= HandleStateChangedAsync;
}
```
