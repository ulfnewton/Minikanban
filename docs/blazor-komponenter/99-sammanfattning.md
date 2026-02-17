
# Sammanfattning

[⟵ Till översikten](README.md)

- Livscykeln börjar vid `SetParametersAsync`, fortsätter via `OnInitialized`/`OnInitializedAsync`, re-renderas via `ShouldRender` och avslutas med `Dispose`.
- Data går nedåt via `Parameter`/`CascadingParameter`. Händelser går uppåt via `EventCallback`.
- Dela state via tjänster som notifierar förändring; komponenter återrenderas via `InvokeAsync(StateHasChanged)`.
