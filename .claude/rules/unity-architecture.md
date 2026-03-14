# Unity Architecture

## Workflow

Unity projects are edited through the Unity Editor GUI — no CLI build commands.

- **Open:** Unity Hub → `F:\00_Unity\zealot` → Unity 6000.3.11f1
- **Build:** File → Build Settings → Build
- **Tests:** Window → General → Test Runner
- C# scripts compile automatically on save — no manual step needed

## Rendering

- **Pipeline:** HDRP with three quality presets in `Assets/Settings/`: Performant, Balanced, High Fidelity
- **Post-processing:** Global volume on the "Sky and Fog Volume" GameObject
- **Color space:** Linear
- **Anti-aliasing:** Temporal AA (TAA) on the main camera

## Input

- Use Unity Input System 1.19.0 — NOT legacy `Input.GetKey`
- Actions asset: `Assets/InputSystem_Actions.inputactions`
- Use `PlayerInput` component or `InputActionAsset` references in scripts

## Key Packages

| Package | Version | Purpose |
|---------|---------|---------|
| `com.unity.render-pipelines.high-definition` | 17.3.0 | HDRP rendering |
| `com.unity.inputsystem` | 1.19.0 | Input handling |
| `com.unity.timeline` | 1.8.11 | Cutscenes/animation sequencing |
| `com.unity.burst` | 1.8.28 | Performance-critical code compilation |
| `com.unity.collections` | 2.6.2 | High-performance native collections |
| `com.unity.mathematics` | 1.3.3 | SIMD-friendly math types |

## Script Organization

- `Assets/Scripts/` — game scripts (to be created)
- Editor-only scripts go in an `Editor/` subfolder
- C# 9.0 / .NET Framework 4.7.1
- No namespace conventions established yet — define them as the project grows

## HDRP-Specific

- Use `HDAdditionalLightData` when modifying lights via script — `Light` alone is insufficient
- Use `HDAdditionalCameraData` for camera settings
- Post-processing requires a `Volume` with an `HDRenderPipelineVolumeProfile`
- Shaders must target HDRP Lit/Unlit — not URP or Built-in
