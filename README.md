# 💎 Diamond Dasher Cheat

A trainer/cheat for [Diamond Dasher](https://ttvideogamedesign.itch.io/diamond-dasher) (Unity Mono game) featuring level skip and camera zoom controls.

![Unity](https://img.shields.io/badge/Unity-Mono-blue)
![C#](https://img.shields.io/badge/C%23-.NET%20Framework%204.8-green)
![License](https://img.shields.io/badge/License-MIT-yellow)

---

## ✨ Features

| Feature | Description |
|---------|-------------|
| **Level Skip** | Instantly jump to any level (0-7) |
| **Camera Zoom** | Adjust orthographic camera size (1.0 - 20.0) |
| **Toggle Menu** | Press `INSERT` to show/hide |

---

## 📦 Installation

### Requirements
- [SharpMonoInjector](https://github.com/warbler/SharpMonoInjector) (or any Mono injector)
- Diamond Dasher game

### Steps

1. Download `DiamondDasherMod.dll` from [Releases](../../releases)
2. Start the game (`GroupPlatformer.exe`)
3. Open **SharpMonoInjector**
4. Select the game process
5. Configure injection:

   | Setting | Value |
   |---------|-------|
   | Assembly | `DiamondDasherMod.dll` |
   | Namespace | `DiamondDasherMod` |
   | Class | `Loader` |
   | Method | `Load` |

6. Click **Inject**
7. Press `INSERT` to toggle the cheat menu

---

## 🎮 Controls

| Key | Action |
|-----|--------|
| `INSERT` | Toggle cheat menu |
| Click level buttons | Skip to that level |
| Drag slider | Adjust camera zoom |

---

## 🛠️ Building from Source

### Prerequisites
- .NET SDK 6.0+ (targets .NET Framework 4.8)
- Diamond Dasher game files (for Unity references)

### Build

```bash
cd DiamondDasherMod
dotnet build -c Release
```

Output: `bin/Release/DiamondDasherMod.dll`

---

## 📁 Project Structure

```
DiamondDasherMod/
├── DiamondDasherMod.csproj    # Project file with Unity references
├── Loader.cs                   # Entry point + cheat menu
└── bin/Release/
    └── DiamondDasherMod.dll    # Built assembly
```

---

## 🔍 Technical Details

This cheat works by:
1. Injecting a C# assembly into the Unity Mono runtime
2. Creating a persistent `GameObject` with a `MonoBehaviour`
3. Using Unity's `OnGUI` for the menu interface
4. Calling `SceneManager.LoadScene()` for level skips
5. Modifying `Camera.main.orthographicSize` for zoom

### Game Analysis

| Item | Details |
|------|---------|
| Engine | Unity (Mono) |
| Camera | Orthographic 2D |
| Levels | 8 scenes (0 = Menu, 1-7 = Gameplay) |
| Key Classes | `PlayerMovement`, `CameraController`, `LevelSender` |

---

## ⚠️ Disclaimer

This project is for **educational purposes only**. Use at your own risk. Not intended for use in online/multiplayer scenarios.

---

## 📄 License

MIT License - See [LICENSE](LICENSE) for details.
