
# Identity-light och UI-behörighetsstyrning — kort teori

## Varför?
Snabbt träna rollstyrd UI utan backend/Identity-konfiguration.

## Översikt
```mermaid
flowchart TD
    U[User] --> RS[RoleSelector]
    RS --> US[UserState singleton]
    US -->|CascadingParameter| ML[MainLayout]
    ML --> KB[KanbanBoard]
    KB --> KC[KanbanColumn]
    KC --> AF[AddCardForm]
```

## Händelseflöde
```mermaid
sequenceDiagram
    participant User
    participant RoleSelector
    participant UserState
    participant Components as Kanban komponenter
    User->>RoleSelector: väljer roll
    RoleSelector->>UserState: SetRoleAsync role
    UserState-->>Components: OnChangeAsync
    Components->>Components: StateHasChanged
    Components-->>User: UI uppdateras
```

## Begrepp
- **UI-behörighetsstyrning**: Hide/Disable baserat på roll.
- **UserState**: simulerad roll, notifierar vid ändring.
