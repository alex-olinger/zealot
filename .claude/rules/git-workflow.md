# Git Workflow

## Branching

- Create a feature branch for each major task or phase (e.g. `player-movement`, `combat-system`)
- Branch naming: kebab-case, descriptive (e.g. `initial-setup`, `hdrp-postprocessing`)

## Pushing & PRs

- **Never push to `main` directly.** Always push to a feature branch and open a PR.
- A PR requires user review before merging.
- Only the user should merge PRs — do not merge autonomously.

## Commits

- Never commit autonomously. Ask the user: "Ready to commit?" before running `git commit`.
- Each commit should be logically coherent — group related changes.
