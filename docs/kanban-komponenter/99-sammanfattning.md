
# Sammanfattning

[⟵ Till översikten](README.md)

- KanbanBoard och KanbanColumn lyssnar på en gemensam `KanbanState` via `CascadingValue` i `MainLayout`.
- Uppdateringar går alltid via state-tjänsten som triggar `OnChangeAsync` → komponenter återrenderas.
- `StateAwareComponent` minskar duplicering och säkerställer korrekt livscykelhantering.
