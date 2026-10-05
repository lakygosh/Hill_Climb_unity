# Hill Climb – One-Button Physics Driving Game

A 2D physics-based hill-climb racer built in Unity. The whole game is played with one button, and player profiles, coins, unlocked cars and high scores are stored by a REST backend.

![Unity 2022.3](https://img.shields.io/badge/Unity-2022.3.8f1-000000?style=flat-square&logo=unity&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=flat-square&logo=csharp&logoColor=white)
![URP 2D](https://img.shields.io/badge/URP-2D-000000?style=flat-square&logo=unity&logoColor=white)
![Netcode for GameObjects](https://img.shields.io/badge/Netcode_for_GameObjects-1.5.2-000000?style=flat-square&logo=unity&logoColor=white)
![Spring Boot backend](https://img.shields.io/badge/Backend-Spring_Boot_3-6DB33F?style=flat-square&logo=springboot&logoColor=white)

<!-- TODO: add screenshot / gameplay GIF -->

## Overview

Hill Climb is a side-scrolling driving game in the style of *Hill Climb Racing*. The terrain is generated endlessly with Perlin noise and built from a Unity SpriteShape spline. The car is a 2D rigidbody chassis on two wheel rigidbodies, driven by applying torque to the wheels.

There are two game modes, *Kontrakcija* ("contraction") and *Opuštanje* ("relaxation"), and every action is bound to a single input (left mouse click). Player profiles come from the XML store of a companion `Pendulum_v3FullApp` application; its user model includes diagnosis and range-of-motion progress fields. Together this suggests the game was made as a one-button exercise game for therapy or rehabilitation. It was a team project. The Unity client is in this repo, and the Java backend lives in [HillClimb_Server](https://github.com/AleksandarGazikalovic/HillClimb_Server).

## Gameplay

- **Kontrakcija mode.** The car drives itself at a set speed. When a lava pit comes up, a warning appears, and the player clicks inside the reaction window to jump it. Miss the jump and the car drops into the pit.
- **Opuštanje mode.** Hold the button to accelerate and release it to brake. Difficulty sets how hard the car brakes.
- **Difficulty levels** (Easy / Medium / Hard). On harder levels the jump window is narrower and braking is stronger.
- **Speed slider** in the main menu to cap the car's top speed.
- **Coins and fuel pickups** spread across the track, with a fuel gauge in the HUD.
- **Car shop** with 16 cars. Spend collected coins to unlock them, then pick the one you drive.
- **Leaderboard** showing the top 10 players by best distance.
- **Game over** when the car flips and stays upside down for 3 seconds, or falls off the map (including into a lava pit). A new personal best is highlighted and saved.
- **Pause menu** (Esc) with music and SFX volume sliders (saved in `PlayerPrefs`), plus a parallax sky background.

## Tech Stack

| Area        | Technology                                                                 |
|-------------|----------------------------------------------------------------------------|
| Engine      | Unity 2022.3.8f1 (LTS), Universal Render Pipeline (2D)                     |
| Language    | C#                                                                         |
| Gameplay    | Physics2D (rigidbodies, raycasts), 2D SpriteShape, Tilemap, Cinemachine    |
| Input       | Unity Input System (`CarActions.inputactions`)                             |
| UI          | uGUI, TextMeshPro                                                          |
| Networking  | `UnityWebRequest` + Newtonsoft.Json (REST); Netcode for GameObjects (experimental) |
| Backend     | Java 17, Spring Boot 3.1, JAXB XML persistence ([HillClimb_Server](https://github.com/AleksandarGazikalovic/HillClimb_Server)) |

## Technical Highlights

- **Procedural terrain.** `EnviromentGeneratorKontrakcija` / `EnviromentGeneratorOpustanje` add spline points as the car gets close to the end of the generated track. Heights come from `Mathf.PerlinNoise`, and continuous tangents keep the hills smooth. Hill amplitude grows over time, and in contraction mode a flat stretch with a lava pit is added every few segments.
- **Torque-based vehicle physics.** The car moves through `AddTorque` on the wheel rigidbodies and has a speed limiter. The code adjusts angular drag to brake, freezes chassis rotation just before a jump, and raycasts to the ground to detect landings. Flips are detected with a dot product between the car's up vector and world up.
- **Viewport-based hazard detection.** The game finds the next lava pit by world position and checks whether it is inside a difficulty-dependent part of the camera viewport (`WorldToViewportPoint`). That check decides when the warning shows and when a jump counts.
- **Client–server persistence.** `PlayerManager` loads all players and the selected player over REST on startup. It posts coins, best scores, unlocked cars and the selected car back as JSON, using coroutine-based `UnityWebRequest` calls. The Spring Boot server stores the data as XML and keeps rotating backups.
- **Additive scene flow.** A persistent `Background` scene loads `MainMenu`, `Shop`, `Leaderboard` and `EscapeMenu` additively, so the menu background keeps playing during scene changes.
- **Multiplayer groundwork.** `NetworkPlayer` (a `NetworkBehaviour` with owner-only input) and `StartNetwork` (host / server / client) are built on Netcode for GameObjects. The multiplayer scenes listed in Build Settings are not in this repo, so treat this part as in progress.

## Getting Started

### Requirements

- Unity Hub with **Unity 2022.3.8f1**
- The [HillClimb_Server](https://github.com/AleksandarGazikalovic/HillClimb_Server) backend running on `http://localhost:8080` (Java 17)

### Run the backend

```bash
git clone https://github.com/AleksandarGazikalovic/HillClimb_Server.git
cd HillClimb_Server
./mvnw spring-boot:run
```

The server reads `HCRPlayers.xml` and `SelectedUser.xml` from `%USERPROFILE%\Documents\Pendulum_v3FullApp\`, so those files must exist there before you start a game.

### Open the game

1. In Unity Hub, choose **Add → Add project from disk** and select the `Hill_Climb/` folder (not the repo root).
2. Open `Assets/Scenes/Background.unity`. This is the entry scene, and it loads the main menu additively.
3. Press **Play**. Click (or press `W`) to drive or jump, and press `Esc` to pause.

## Project Structure

```
Hill_Climb/
├── Assets/
│   ├── Scenes/            # Background, MainMenu, KontrakcijaSP, OpustanjeSP, Shop, Leaderboard, EscapeMenu
│   ├── Scripts/           # Gameplay, terrain generation, player data, networking
│   │   └── dto/           # Request/response DTOs for the REST backend
│   ├── PreFab/            # Vehicle, camera, pickups, input actions
│   │   └── Cars/          # 16 car sprite sets (chassis + tires)
│   ├── Sprites/           # Backgrounds, parallax layers, game elements
│   ├── Audio/             # Music and SFX
│   └── PhysicsMaterial/   # 2D physics materials
├── Packages/              # Unity package manifest
└── ProjectSettings/
```

Third-party asset packs (GUI kit, animated coins, lava shader, terrain textures) are kept in their own folders under `Assets/`.

## Author

**Lazar Gošić** — GitHub [@lakygosh](https://github.com/lakygosh)
