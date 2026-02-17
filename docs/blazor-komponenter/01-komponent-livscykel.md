
# Komponentlivscykel

[⟵ Till översikten](README.md)

## Blazor Komponentlivscykel – faser

```mermaid
flowchart TD
    Start([Komponent skapas]) --> SetParams[SetParametersAsync]
    SetParams --> OnInit{Första renderingen?}
    OnInit -->|Ja| OnInitialized[OnInitialized]
    OnInit -->|Nej| OnParamsSet[OnParametersSet]
    OnInitialized --> OnInitializedAsync[OnInitializedAsync]
    OnInitializedAsync --> OnParamsSet
    OnParamsSet --> OnParamsSetAsync[OnParametersSetAsync]
    OnParamsSetAsync --> ShouldRender{ShouldRender?}
    ShouldRender -->|Ja| BuildRenderTree[BuildRenderTree]
    ShouldRender -->|Nej| Wait[Vänta på nästa uppdatering]
    BuildRenderTree --> OnAfterRender[OnAfterRender]
    OnAfterRender --> OnAfterRenderAsync[OnAfterRenderAsync]
    OnAfterRenderAsync --> Wait
    Wait --> ParameterChange{Parameter ändrad?}
    ParameterChange -->|Ja| SetParams
    ParameterChange -->|Nej| StateChange{StateHasChanged anropad?}
    StateChange -->|Ja| ShouldRender
    StateChange -->|Nej| Wait
    Wait --> Dispose{Komponent tas bort?}
    Dispose -->|Ja| DisposeAsync[DisposeAsync / Dispose]
    DisposeAsync --> End([Komponent borttagen])

    style Start fill:#388E3C,color:#fff,stroke:#1B5E20,stroke-width:2px
    style SetParams fill:#1976D2,color:#fff,stroke:#0D47A1,stroke-width:2px
    style OnInit fill:#F57C00,color:#fff,stroke:#E65100,stroke-width:2px
    style OnInitialized fill:#1976D2,color:#fff,stroke:#0D47A1,stroke-width:2px
    style OnInitializedAsync fill:#1976D2,color:#fff,stroke:#0D47A1,stroke-width:2px
    style OnParamsSet fill:#1976D2,color:#fff,stroke:#0D47A1,stroke-width:2px
    style OnParamsSetAsync fill:#1976D2,color:#fff,stroke:#0D47A1,stroke-width:2px
    style ShouldRender fill:#F57C00,color:#fff,stroke:#E65100,stroke-width:2px
    style BuildRenderTree fill:#8E24AA,color:#fff,stroke:#6A1B9A,stroke-width:2px
    style OnAfterRender fill:#8E24AA,color:#fff,stroke:#6A1B9A,stroke-width:2px
    style OnAfterRenderAsync fill:#8E24AA,color:#fff,stroke:#6A1B9A,stroke-width:2px
    style Wait fill:#616161,color:#fff,stroke:#424242,stroke-width:2px
    style ParameterChange fill:#F57C00,color:#fff,stroke:#E65100,stroke-width:2px
    style StateChange fill:#F57C00,color:#fff,stroke:#E65100,stroke-width:2px
    style Dispose fill:#F57C00,color:#fff,stroke:#E65100,stroke-width:2px
    style DisposeAsync fill:#C62828,color:#fff,stroke:#B71C1C,stroke-width:2px
    style End fill:#757575,color:#fff,stroke:#616161,stroke-width:2px
```

## Livscykelmetoder i detalj

### 1. `SetParametersAsync`
- Första metoden som anropas
- Tar emot parametrar från parent-komponenten
- Kan avbrytas för anpassad parameterhantering
- Anropas varje gång parametrar ändras

```csharp
public override async Task SetParametersAsync(ParameterView parameters)
{
    // Logik innan parametrar sätts
    await base.SetParametersAsync(parameters);
    // Logik efter att parametrar satts
}
```

### 2. `OnInitialized` / `OnInitializedAsync`
- Körs en gång när komponenten initialiseras
- Bra för att registrera event handlers och initiera data
- Cascading parameters finns tillgängliga här
- Synkron `OnInitialized` kör före `OnInitializedAsync`

```csharp
protected override void OnInitialized()
{
    State.OnChangeAsync += HandleStateChangedAsync;
}

protected override async Task OnInitializedAsync()
{
    await LoadDataAsync();
}
```

### 3. `OnParametersSet` / `OnParametersSetAsync`
- Körs efter `OnInitialized` och när parametrar ändras
- Reagera på parameterändringar och uppdatera internt state

```csharp
protected override void OnParametersSet()
{
    if (Column != null)
    {
        // Uppdatera internt state
    }
}
```

### 4. `OnAfterRender` / `OnAfterRenderAsync`
- Körs efter varje rendering
- Bra för JavaScript interop
- Använd `firstRender` för engångsinitiering
- Undvik att kalla `StateHasChanged` direkt här

```csharp
protected override async Task OnAfterRenderAsync(bool firstRender)
{
    if (firstRender)
    {
        await JSRuntime.InvokeVoidAsync("initializeComponent");
    }
}
```

### 5. `Dispose` / `DisposeAsync`
- Körs när komponenten tas bort
- Avregistrera events för att undvika minnesläckor

```csharp
public void Dispose()
{
    if (State is not null)
    {
        State.OnChangeAsync -= HandleStateChangedAsync;
    }
}
```
