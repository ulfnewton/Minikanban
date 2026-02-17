
# Identity-light — intro

**Mål:** Åskådliggöra hur roll påverkar UI i Kanban utan riktig Identity.

**Principer:**
- `UserState` (singleton) håller `Role`.
- `CascadingValue` i `MainLayout` gör `UserState` tillgänglig överallt.
- Komponenter lyssnar på `OnChangeAsync` och återrenderar.

**Termer:**
- UI-behörighetsstyrning (sv. för "UI-gating").
- Hide vs Disable — visuellt ansvar i komponent.
