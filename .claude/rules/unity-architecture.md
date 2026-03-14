# Unity Architecture

## Workflow

Unity projects are edited through the Unity Editor GUI — no CLI build commands.

- **Open:** Unity Hub → add the project folder → open with Unity 6000.3.11f1
- **Build:** File → Build Settings → Build
- **Tests:** Window → General → Test Runner
- C# scripts compile automatically on save — no manual step needed

## Rendering

- **Pipeline:** HDRP with three quality presets in `Assets/Settings/`: Performant, Balanced, High Fidelity
- **Post-processing:** Global volume on the "Sky and Fog Volume" GameObject
- **Color space:** Linear
- **Anti-aliasing:** Temporal AA (TAA) on the main camera

## Input

- Use Unity Input System — NOT legacy `Input.GetKey`
- Actions asset: `Assets/InputSystem_Actions.inputactions`
- Use `PlayerInput` component or `InputActionAsset` references in scripts

## Key Packages

| Package | Purpose |
|---------|---------|
| `com.unity.render-pipelines.high-definition` | HDRP rendering |
| `com.unity.inputsystem` | Input handling |
| `com.unity.timeline` | Cutscenes/animation sequencing |
| `com.unity.burst` | Performance-critical code compilation |
| `com.unity.collections` | High-performance native collections |
| `com.unity.mathematics` | SIMD-friendly math types |

Exact versions are in `Packages/manifest.json`.

## Script Organization

- Editor-only scripts go in an `Editor/` subfolder within their feature folder
- C# 9.0 / .NET Framework 4.7.1
- No namespace conventions established yet — define them as the project grows

## HDRP-Specific

- Use `HDAdditionalLightData` when modifying lights via script — `Light` alone is insufficient
- Use `HDAdditionalCameraData` for camera settings
- Post-processing requires a `Volume` with an `HDRenderPipelineVolumeProfile`
- Shaders must target HDRP Lit/Unlit — not URP or Built-in
