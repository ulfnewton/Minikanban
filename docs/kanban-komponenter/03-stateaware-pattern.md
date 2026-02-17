
# StateAwareComponent-pattern

[⟵ Till översikten](README.md)

```mermaid
classDiagram
    class ComponentBase {
        <<Blazor Base>>
        +StateHasChanged()
        +InvokeAsync()
    }

    class IDisposable {
        <<interface>>
        +Dispose()
    }

    class StateAwareComponent {
        <<abstract>>
        +KanbanState State
        #OnInitialized()
        #StateChanged() Task
        -HandleStateChangedAsync() Task
        +Dispose()
    }

    class KanbanBoard {
        #StateChanged() Task
    }

    class KanbanColumn {
        +KanbanColumn Column
        #StateChanged() Task
    }

    ComponentBase <|-- StateAwareComponent
    IDisposable <|.. StateAwareComponent
    StateAwareComponent <|-- KanbanBoard
    StateAwareComponent <|-- KanbanColumn

    StateAwareComponent --> KanbanState : uses
```

**Syfte:**
- Minska duplicering av eventregistrering och -avregistrering
- Säkerställa konsekvent state-lyssning och återrendering
- Tillhandahålla en överstyrbar hook (`StateChanged`) för anpassning
