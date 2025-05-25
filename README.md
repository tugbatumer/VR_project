# Project Title: [Dungeon Escape RPG]
This game is developed in Unity editor version 6000.0.41f1 for Oculus Quest 2 headsets.
## Repository Structure

```plaintext
/Assets
├── /Scenes
│   └── FinalScene
├── /Scripts
│   └── all_scripts
├── /Settings
├── /Audios
│   └── all_audio_clips
├── /Art
│   └── sprites, background_images (Used As Is)
├── /ImportedAssets
│   └── all_Unity_Store_assets (Used As Is or Modified)
├── /Materials
│   └── modified_materials
├── /Prefabs
│   └── modified_prefabs
├── /Resources
│   └── Used As Is
├── /Samples
│   └── XR Interaction Toolkit
├── /TextMeshPro
│   └── Used As Is
├── /Textures
│   └── Used As Is
├── /XR
├── /XRI
```

## Rebuilding Instructions

To rebuild and run the game:

1. Clone this repository.
2. Open it in Unity Hub using the specified version.
3. Ensure your VR setup (e.g., OpenXR, Oculus Integration) is properly configured.
4. Open the main scene: `Scenes/MainScene.unity`.
5. Press Play in Unity (with a VR headset connected, if required).

---

## Assets & Scripts Classification

### Produced (Original)

These were entirely created by our team.

**Scripts**:
All the scripts used in this project are created from scratch (except one, please see below) by our team. Therefore, all the core mechanics used in the scripts are implemented by our team, with some occasonal help from online sources for bug fixing purposes.

**Assets**:
Artwork used in this project, such as 3D objects, audio clips, textures and materials are NOT created by our team. However, most of the artwork is modified in some form by adding animations, bones for the bow prefab, custom coloring, etc.

---

### Adapted (Modified from External Sources)

**Scripts**:
No scripts were adapted.

**Assets**:

/Assets/Prefabs/Bow.prefab was modified with the addition of ``bones'' to the strategic places in the 3D mesh of the object in Blender. After importing this object in Unity, when a position of a bone changes by some amount, the rest of the string also moves with it accordingly, creating a realistic string pulling visual effect.

### Unmodified (Used As-Is)

**Scripts**:
Assets/ImportedAssets/QuickOutline/Scripts/Outline.cs is used as is from the ["Quick Outline" Unity Store Asset](https://assetstore.unity.com/packages/tools/particles-effects/quick-outline-115488) to highlight the collectible and some other interactable objects such as doors with blue lines to guide the players.

**Assets**:
All the assets in the directory Assets/ImportedAssets are assets used as they are from the Unity Asset Store.


## Acknowledgments

Throughout the development of our game, we incorporated various external resources to enhance the visual and auditory experience. We would like to acknowledge and thank the creators of these assets for their contributions.

### Unity Asset Store Resources

We used the following assets from the Unity Asset Store:

- **[Lite Dungeon Pack - Low Poly 3D Art by Gridness](https://assetstore.unity.com/packages/3d/environments/dungeons/lite-dungeon-pack-low-poly-3d-art-by-gridness-242692)**  
  Provided prefabs for building the dungeon rooms, doors and corridors, and decoration items.

- **[Low Poly Dungeons Lite](https://assetstore.unity.com/packages/3d/environments/dungeons/low-poly-dungeons-lite-177937)**  
  Provided prefabs for building the dungeon rooms, mostly decoration items.

- **[Potions, Coin And Box of Pandora Pack](https://assetstore.unity.com/packages/3d/props/potions-coin-and-box-of-pandora-pack-71778)**  
  Mana potion prefab.

- **[Targets vol pack](https://assetstore.unity.com/packages/3d/environments/targets-vol-pack-176827)**  
  Shooting target prefabs for the first archery puzzle.

- **[Simple Water Shader URP](https://assetstore.unity.com/packages/2d/textures-materials/water/simple-water-shader-urp-191449)**  
  Water shader used in swimming areas.

- **[Quick Outline](https://assetstore.unity.com/packages/tools/particles-effects/quick-outline-115488)**  
  Outlining tool to highlight the collectible and some other interactable objects such as doors with blue lines to guide the players.

---

### Other Resources

Beyond the Unity Asset Store, we also used royalty-free prefabs, sound effects, and educational materials from the following sources:

- **["Deathcard Cabin" Soundtrack from the Video Game *Inscryption*](https://jonahsenzel.bandcamp.com/track/01-deathcard-cabin)**  
  We used the "Deathcard Cabin" soundtrack from the *Inscryption Original Soundtrack* by Jonah Senzel, which we purchased via Steam.

- **[CGTrader](https://www.cgtrader.com)**  
  Some minor 3D models such as feather and crystal collectibles.

- **[Pixabay](https://pixabay.com)**  
  Most of the sound effects, such as door opening and closing, footsteps, arrow shooting, collecting items, etc.

- **[Fist Full of Shrimp Tutorials](https://www.youtube.com/@FistFullofShrimp)**  
  Helpful tutorials on XR locomotion and object interaction fundamentals.

- **[YouTube](https://www.youtube.com)**  
  Additional sound effects like locked door sounds and puzzle success/failure sounds.

- **[ChatGPT](https://chatgpt.com)**  
  Used for learning how to implement Unity functionalities such as creating animations, configuring button bindings, and adjusting UI/Image settings.
