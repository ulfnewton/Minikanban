
# MiniKanban (.NET 8, Blazor Server)

En komplett, körbar undervisningsapp som demonstrerar komponenter, livscykler och dataflöde med delat state via `CascadingValue` och asynkron notifiering.

## Krav
- .NET SDK 8.0 (global.json pekar på 8.0.417)

## Kör
```bash
cd src/Minikanban
dotnet run
```
Öppna sedan den adress som skrivs ut i terminalen (t.ex. `https://localhost:7287`).

## Nyckelidéer
- `App.razor` tillhandahåller `KanbanState` som `CascadingValue` till hela komponentträdet.
- `StateAwareComponent` visar prenumeration/avregistrering (OnInitialized/Dispose) och triggar omrendering säkert.
- `KanbanState` har `event Func<Task>? OnChangeAsync` för att undvika `async void`.
- UI‑metoderna (`AddCardAsync`, `MoveToAsync`) är små och visar tydlig anropsordning.
