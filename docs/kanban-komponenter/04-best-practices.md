
# Best Practices för Minikanban

[⟵ Till översikten](README.md)

## Rekommenderade mönster
- Lägg `CascadingValue` innanför interaktivt renderingsläge (t.ex. i `MainLayout`).
- Registrera event i `OnInitialized` och avregistrera i `Dispose`.
- Anropa `InvokeAsync(StateHasChanged)` från event.
- Låt state-tjänsten exponera metoder som både muterar data och notiferar.

## Undvik
- `StateHasChanged` i `OnAfterRenderAsync`.
- Direkt mutation av listor i komponenter utan notifikation.
- Synkron blockering i `async`-metoder med `.Result` eller `.Wait()`.

## Prestanda
- Implementera `ShouldRender` om rendering är dyr och förändringarna begränsade.
- Använd `<Virtualize>` för stora listor.
- Aktivera streaming rendering där det passar.
