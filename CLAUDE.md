# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

**Zealot** is a Unity 6 (6000.3.11f1) 3D game project using the **High Definition Render Pipeline (HDRP 17.3.0)**. The project is early-stage — currently a configured HDRP template with infrastructure in place but minimal game logic implemented.

## Unity-Specific Workflow

Unity projects are edited primarily through the Unity Editor GUI, not via CLI. There is no build command to run from the terminal. Common workflows:

- **Open project:** Launch Unity Hub → open `F:\00_Unity\zealot` with Unity 6000.3.11f1
- **Main scene:** `Assets/OutdoorsScene.unity`
- **Build:** Unity Editor → File → Build Settings → Build (target: StandaloneWindows64)
- **Run tests:** Unity Editor → Window → General → Test Runner
- **IDE:** Visual Studio or Rider (both configured); open `zealot.sln`

C# scripts are compiled automatically by Unity on save. No manual compilation step is needed.

## Architecture & Key Systems

### Rendering
- **Pipeline:** HDRP with three quality presets in `Assets/Settings/`: Performant, Balanced, High Fidelity
- **Post-processing:** Global volume on the "Sky and Fog Volume" GameObject in the scene
- **Color space:** Linear
- **Anti-aliasing:** Temporal AA (TAA) configured on the main camera

### Input
- **System:** Unity Input System 1.19.0 (NOT legacy `Input.GetKey`)
- **Actions asset:** `Assets/InputSystem_Actions.inputactions`
- Use `PlayerInput` component or `InputActionAsset` references in scripts

### Key Packages
| Package | Version | Purpose |
|---------|---------|---------|
| `com.unity.render-pipelines.high-definition` | 17.3.0 | HDRP rendering |
| `com.unity.inputsystem` | 1.19.0 | Input handling |
| `com.unity.timeline` | 1.8.11 | Cutscenes/animation sequencing |
| `com.unity.burst` | 1.8.28 | Performance-critical code compilation |
| `com.unity.collections` | 2.6.2 | High-performance native collections |
| `com.unity.mathematics` | 1.3.3 | SIMD-friendly math types |

### Script Organization
- `Assets/Scripts/` — game scripts (to be created)
- `Assets/TutorialInfo/Scripts/Editor/` — editor-only scripts go in an `Editor/` subfolder
- C# 9.0 language features are available (.NET Framework 4.7.1 runtime)
- No namespace conventions established yet — define them as the project grows

### HDRP-Specific Notes
- Use `HDAdditionalLightData` component when modifying lights via script (standard `Light` component alone is insufficient)
- Use `HDAdditionalCameraData` for camera settings
- Post-processing effects require a `Volume` component with an `HDRenderPipelineVolumeProfile`
- Shader development should target HDRP Lit/Unlit shaders, not URP or Built-in shaders

---

## Working Style

### Planning
- Enter plan mode for any non-trivial task (3+ steps or architectural decisions)
- If something goes sideways, stop and re-plan — don't keep pushing
- Write a plan to `tasks/todo.md` with checkable items before implementing; check in before starting

### Task Tracking
- Mark `tasks/todo.md` items complete as work progresses
- After any correction from the user, update `tasks/lessons.md` with the pattern to avoid repeating it

### Code Quality
- **Flat before layered.** Write the simplest direct implementation first. Do not introduce abstraction layers unless a concrete, present need requires them.
- **No speculative generalization.** No base classes, generic factories, or strategy patterns unless more than one concrete use case exists right now.
- **One layer of indirection is usually enough.** Do not add Repository + UnitOfWork + AbstractBase on top of something that already handles the concern.
- **No interfaces with a single implementation** unless a test double explicitly requires it.

Before presenting code, check:
1. Could this be written with one fewer layer? If yes — write that version.
2. Is every abstraction justified by something that exists today?
3. Would a new team member understand this without reading three other files first?

### Bug Fixing
- When given a bug report: fix it autonomously. Point at logs/errors, resolve them, don't ask for hand-holding.

### Verification
- Never mark a task complete without proving it works.
- Run tests, check logs, demonstrate correctness.
