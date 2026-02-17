
# Felsökning

[⟵ Till översikten](README.md)

## Vanliga problem

### CascadingParameter är `null`
- Orsak: `CascadingValue` placerad utanför interaktivt renderingsläge.
- Åtgärd: Flytta till exempel från `App.razor` till `MainLayout.razor`.

### UI uppdateras inte
- Orsaker: Saknad eventregistrering, ingen `StateHasChanged`, eller direkt mutation.
- Åtgärd: Gå via state-tjänsten och säkerställ registrering + `InvokeAsync(StateHasChanged)`.

### Minnesläckor
- Orsak: Glömda avregistreringar av events.
- Åtgärd: Implementera `Dispose` och avregistrera.
