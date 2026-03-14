# Working Style

## Planning

- Enter plan mode for any non-trivial task (3+ steps or architectural decisions)
- If something goes sideways, stop and re-plan — don't keep pushing
- Write a plan to `todo/` as a new aptly-named `.md` file (e.g. `todo/player-movement.md`) with checkable items; check in before starting implementation
- Each major feature or phase gets its own file — do not pile everything into a single doc
- Refer to `todo/INDEX.md` for the directory structure

## Task Tracking

- Mark items complete within their respective `todo/` file as work progresses
- After any correction from the user, add the pattern to `todo/lessons.md` to avoid repeating it

## Code Quality

- **Flat before layered.** Write the simplest direct implementation first. No abstraction layers unless a concrete, present need requires them.
- **No speculative generalization.** No base classes, manager hierarchies, or strategy patterns unless more than one concrete use case exists right now.
- **One layer of indirection is usually enough.** In Unity terms: don't wrap a `MonoBehaviour` in an abstract manager chain or service locator unless the problem genuinely requires it.
- **No interfaces with a single implementation** unless a test double explicitly requires it.

Before presenting code, check:
1. Could this be written with one fewer layer? If yes — write that version.
2. Is every abstraction justified by something that exists today?
3. Would a new team member understand this without reading three other files first?

## Bug Fixing & Verification

Fix autonomously — point at logs/errors and resolve them without hand-holding. Never mark a task complete without proving it works: run tests, check logs, demonstrate correctness.

## Git Commits

Never commit autonomously. When work is at a logical save point, ask the user: "Ready to commit?" and wait for confirmation before running any `git commit`.
