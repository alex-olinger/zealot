# Working Style

## Planning

- Enter plan mode for any non-trivial task (3+ steps or architectural decisions)
- If something goes sideways, stop and re-plan — don't keep pushing
- Write a plan to `tasks/todo.md` with checkable items before implementing; check in before starting

## Task Tracking

- Mark `tasks/todo.md` items complete as work progresses
- After any correction from the user, update `tasks/lessons.md` with the pattern to avoid repeating it

## Code Quality

- **Flat before layered.** Write the simplest direct implementation first. No abstraction layers unless a concrete, present need requires them.
- **No speculative generalization.** No base classes, factories, or strategy patterns unless more than one concrete use case exists right now.
- **One layer of indirection is usually enough.** Don't stack Repository + UnitOfWork + AbstractBase on top of something that already handles the concern.
- **No interfaces with a single implementation** unless a test double explicitly requires it.

Before presenting code, check:
1. Could this be written with one fewer layer? If yes — write that version.
2. Is every abstraction justified by something that exists today?
3. Would a new team member understand this without reading three other files first?

## Bug Fixing

Fix autonomously. Point at logs/errors and resolve them — no hand-holding needed.

## Verification

Never mark a task complete without proving it works. Run tests, check logs, demonstrate correctness.
