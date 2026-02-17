
# Integration — Identity-light i Minikanban

## Program.cs (patch)
Se `patches/Program.IdentityLight.patch`.

## MainLayout.razor
Infoga enligt `patches/MainLayout.IdentityLight.sample.razor`.

## Komponenter
- Lägg `RoleSelector.razor`, `RoleGate.razor` och `UserAwareComponent.cs` i `Components/Shared`.
- Lägg `AdminPanel.razor` i `Components/Admin`.
- Lägg/uppdatera `AddCardForm.razor` i `Components/Kanban`.

## Terminologi
- Använd "UI-behörighetsstyrning" i stället för "UI-gating" i dokument och kodkommentarer.
