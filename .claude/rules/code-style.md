# C# Code Style

## Naming

| Symbol | Convention | Example |
|--------|------------|---------|
| Class, struct, enum | `PascalCase` | `PlayerController` |
| Method, property | `PascalCase` | `TakeDamage()`, `IsGrounded` |
| Private / protected field | `_camelCase` | `_moveSpeed` |
| Local variable, parameter | `camelCase` | `hitPoint` |
| Constant | `PascalCase` | `MaxHealth` |
| Event | `PascalCase`, verb phrase | `OnDeath`, `OnHealthChanged` |

## Fields & Serialization

- Prefer `private` fields with `[SerializeField]` over `public` fields.
- Use `[Header("Section Name")]` to group related fields in the Inspector.
- Use `[Tooltip("...")]` on non-obvious serialized fields.
- Cache component references in `Awake` — no `GetComponent` calls in `Update`.
- Never use `FindObjectOfType` or `GameObject.Find` at runtime. Inject references via the Inspector or initialize in `Awake`/`Start`.

```csharp
[Header("Movement")]
[SerializeField] private float _moveSpeed = 5f;
[SerializeField, Tooltip("Force applied on jump")] private float _jumpForce = 8f;
```

## MonoBehaviour Lifecycle Order

Declare lifecycle methods in this order:

1. `Awake`
2. `OnEnable`
3. `Start`
4. `Update` / `FixedUpdate` / `LateUpdate`
5. `OnDisable`
6. `OnDestroy`

## General C#

- Avoid `var` — prefer explicit types for clarity, except where the type is immediately obvious from the right-hand side (e.g., `var rb = GetComponent<Rigidbody>()`).
- Use `?.` and `??` for null checks rather than explicit `if (x == null)` guards where idiomatic.
- No regions (`#region`). If a file needs regions to stay readable, split it instead.
- One class per file. File name must match the class name.
- Keep `MonoBehaviour` classes focused on a single responsibility. Extract pure logic into plain C# classes when it can be unit-tested in isolation.

## Comments

- XML doc comments (`/// <summary>`) on public methods and properties that are part of a stable API.
- Inline comments only for non-obvious logic — do not comment what the code already says clearly.
- No commented-out code committed to the repo.
