# 3D Models & Assets Guide for Kingdom Focus

## Overview
This guide covers the 3D models needed for the 2.5D isometric view (Clash of Clans style).

## Building Models

### Early Game Buildings

#### 1. **Barracks**
- **Dimensions**: 4x4 grid units (isometric)
- **Style**: Stone structure with wooden doors
- **Details**:
  - Wooden training dummies outside
  - Flag pole with kingdom colors
  - Torches on sides
- **LOD Levels**: 3 (High, Medium, Low)
- **Textures**: Stone walls, wood planks, metal accents

#### 2. **Woodcutter's Hut**
- **Dimensions**: 3x3 grid units
- **Style**: Rustic wooden structure
- **Details**:
  - Logs stacked outside
  - Axe leaning on wall
  - Wood pile/storage
- **Textures**: Weathered wood, bark textures

#### 3. **Farm**
- **Dimensions**: 4x4 grid units
- **Style**: Agricultural field with barn
- **Details**:
  - Cultivated rows
  - Scarecrow
  - Water trough
  - Crops at different growth stages
- **Textures**: Dirt, crops, wood

### Mid Game Buildings

#### 4. **Tower of Knowledge**
- **Dimensions**: 3x3 grid units
- **Style**: Wizard tower with magical aura
- **Details**:
  - Pointed roof with glowing orb
  - Mystical runes on walls
  - Floating books animation-ready
  - Crystal window
- **Textures**: Stone, magical effects, glowing materials

#### 5. **Forge**
- **Dimensions**: 4x4 grid units
- **Style**: Medieval blacksmith workshop
- **Details**:
  - Anvil visible through door
  - Smoke effect emitters
  - Weapon/armor racks
  - Furnace opening
- **Textures**: Metal, stone, wood, fire effects

#### 6. **Market**
- **Dimensions**: 5x4 grid units
- **Style**: Trading post/marketplace
- **Details**:
  - Market stalls
  - Barrels and crates
  - Hanging signs
  - Multiple vendor areas
- **Textures**: Cloth, wood, various goods

### Late Game Buildings

#### 7. **Castle Throne Room**
- **Dimensions**: 6x6 grid units
- **Style**: Grand castle structure
- **Details**:
  - Large wooden doors
  - Banners and tapestries
  - Stone gargoyles
  - Ornate architecture
- **Textures**: Premium stone, gold accents, royal fabrics

#### 8. **Arcane Tower**
- **Dimensions**: 5x5 grid units
- **Style**: Advanced magical structure
- **Details**:
  - Multiple spires
  - Arcane symbols
  - Floating magical particles
  - Crystal formations
- **Textures**: Crystalline materials, magical auras, ancient stone

#### 9. **Guild Hall**
- **Dimensions**: 6x5 grid units
- **Style**: Large communal structure
- **Details**:
  - Guild emblem above door
  - Large gathering hall interior visible
  - Multiple windows
  - Throne/ceremonial area
- **Textures**: Grand wood, stone, guild colors customizable

## Hero Character Model

### Warrior Hero
- **Height**: Roughly 6-7 feet in-game scale
- **Equipment Slots**:
  - Head (helmet/crown)
  - Torso (armor/tunic)
  - Legs (pants/greaves)
  - Feet (boots)
  - Hands (gauntlets/gloves)
  - Right Hand (sword/staff/mace)
  - Left Hand (shield/spell book)
- **Customization Colors**: Armor tints, cloak colors, emblem colors
- **Animation States**:
  - Idle (resting pose)
  - Walking (8 directional)
  - Running (8 directional)
  - Attack (swing, spell cast)
  - Damaged (flinch reaction)
  - Victory (celebration)
  - Defeat (fallen)

## Props & Environmental Objects

### Decorative Props
- **Torches** - Multiple states (lit/unlit, flickering)
- **Flags** - Kingdom colors, animated
- **Stones** - Various sizes for paths
- **Trees** - Seasonal variants
- **Rocks** - Boulder formations
- **Water** - Wells, fountains, streams
- **Fences** - Wooden and stone variants

### Interactive Props
- **Chests** - Treasure/loot containers
- **Barrels** - Storage items
- **Crates** - Resource stacks
- **Cauldrons** - Magical/cooking
- **Anvils** - Crafting station
- **Shelves** - Library/storage

## Environmental Elements

### Terrain & Ground
- **Grass tiles** - Base ground
- **Stone paths** - Kingdom roads
- **Water tiles** - Rivers, lakes
- **Sand tiles** - Desert areas
- **Snow tiles** - Winter areas (future expansion)

### Sky & Atmosphere
- **Sky box** - Medieval fantasy setting
- **Clouds** - Animated parallax
- **Lighting** - Day/night cycle (future)
- **Fog** - Distance fog for depth

## Asset Creation Standards

### Polygon Count Guidelines
- **Buildings**: 3K - 8K polygons (LOD optimized)
- **Hero**: 5K - 15K polygons
- **Props**: 500 - 2K polygons
- **Terrain**: 1K - 4K per tile

### Texture Standards
- **Resolution**: 1024x1024 or 2048x2048
- **Format**: PNG with alpha channel
- **PBR Materials**: Diffuse, Normal, Roughness, Metallic
- **Optimization**: Texture atlasing where possible

### Naming Convention
```
[ObjectType]_[Name]_[Variant]_[LOD].fbx

Examples:
- BLDG_Barracks_Stone_LOD0.fbx
- CHAR_Hero_Warrior_LOD0.fbx
- PROP_Torch_Lit_LOD0.fbx
- ENV_Tree_Oak_LOD1.fbx
```

## Animation Specifications

### Building Animations
- Construction progression (0-100%)
- Damage states (visual wear)
- Upgrade sparkle effects
- Idle animations (smoke, lights, etc.)

### Hero Animations
- All animations should be 60 FPS
- Locomotion: 30 frame walk cycle, 20 frame run cycle
- Combat: 25-40 frames per action
- Emotes: 20-60 frames

### Particle Effects
- Level-up sparkles
- Gold collection particles
- Magical spell effects
- Construction dust clouds
- Destruction effects

## Asset Sources & Tools

### Recommended Tools
- **3D Modeling**: Blender 3.5+ (free) or Maya
- **Texturing**: Substance Painter or Blender
- **Animation**: Blender or MotionBuilder
- **Optimization**: LOD generation tools

### Asset Marketplaces
- Unity Asset Store
- Sketchfab (free models)
- CGTrader
- TurboSquid

### Free Asset Packs
- Medieval Fantasy Kit (Unity Asset Store)
- Low Poly Medieval Village
- Isometric Terrain Tiles

## Import Settings for Unity

### FBX Import Settings
```
Model:
- Scale Factor: 1.0
- Mesh Compression: High
- Read/Write Enabled: OFF (for optimization)
- Import Normals: Import Normals and Tangents

Rig:
- Animation Type: Humanoid (for hero)
- Avatar Definition: Create From This Model

Animations:
- Import Animations: Checked
- Bake Animation: OFF
```

### Texture Import Settings
```
Texture Type: Default/Sprite (based on use)
Compression: Normal
Format: RGBA 32-bit or RGB 24-bit
Mipmaps: Enabled
Filtering: Bilinear or Trilinear
```

## Next Steps

1. **Gather/Create Models**: Use asset packs or create custom models
2. **Import to Unity**: Set up proper import settings
3. **Create Prefabs**: Build reusable building/character prefabs
4. **Set Up Materials**: Create proper PBR materials
5. **Test Isometric View**: Verify 2.5D perspective
6. **Optimize**: LOD, batching, culling setup

---

**Estimated Timeline**: 4-8 weeks depending on asset source (purchase vs. custom creation)
