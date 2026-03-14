# Asset Pipeline

## Binary Asset Storage

Binary assets (textures, audio, meshes, etc.) are stored in **Git LFS**. This keeps the main repository lean while still versioning assets alongside code.

### LFS Storage Limits (GitHub Free)
- **Storage:** 1 GB
- **Bandwidth:** 1 GB/month
- Pushes will fail (not silently charge) if the limit is exceeded
- Monitor usage at: `github.com/settings/billing`

### Tracked File Types

All binary asset types are routed through LFS via `.gitattributes`:

| Category | Extensions |
|----------|------------|
| Textures | `.png` `.jpg` `.jpeg` `.gif` `.tga` `.tif` `.tiff` `.psd` `.hdr` `.exr` |
| Audio | `.mp3` `.wav` `.ogg` |
| Video | `.mp4` `.mov` |
| Meshes | `.fbx` `.obj` |
| Libraries | `.dll` `.so` `.dylib` |
| Packages | `.zip` `.unitypackage` |

### Adding New Asset Types

If a new binary format needs to be tracked, add an entry to `.gitattributes`:

```
*.ext filter=lfs diff=lfs merge=lfs -text
```

Then run:

```bash
git add .gitattributes
git lfs track "*.ext"
```
