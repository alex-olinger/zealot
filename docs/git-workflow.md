# Git Workflow

## Branching

- `main` is the stable branch — never push directly to it
- Each feature or phase gets its own branch: `kebab-case`, descriptive (e.g. `player-movement`, `hdrp-postprocessing`)
- Branch from `main`, open a PR when ready for review

## Pull Requests

- All changes go through a PR — no direct merges to main
- PRs are reviewed and merged by the project owner

## Commits

- Commits should be logically coherent — group related changes
- Write commit messages in the imperative: "Add player controller" not "Added player controller"

## LFS

- Binary assets are stored in Git LFS (see `docs/asset-pipeline.md`)
- Run `git lfs ls-files` to verify LFS is tracking files correctly after adding new asset types
