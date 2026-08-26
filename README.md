# Kingdom Focus

A medieval-themed productivity game with 2.5D isometric visuals (Clash of Clans style). Build your kingdom, complete quests, level up your hero, and conquer the realm through focused deep work sessions.

## Features

### Core Mechanics (Same as Focus Jungle)
- **Quest Planning**: Schedule deep work sessions as kingdom quests
- **Deep Work Tracking**: Track focused time to earn gold and XP
- **Progression System**: Level up your hero, unlock buildings, and expand your kingdom
- **Streaks & Milestones**: Maintain daily focus streaks to unlock rewards
- **Guild System**: Join guilds to compete and collaborate with other players
- **Leaderboards**: Compete with friends on focus time and achievements
- **Analytics**: Detailed stats on your productivity journey

### Medieval Theme
- **2.5D Isometric Graphics**: Clash of Clans-style detailed 3D models
- **Kingdom Building**: Construct and upgrade medieval buildings (Barracks, Tower, Treasury, Library, etc.)
- **Hero Character**: Customize and level up your medieval hero
- **Quests & Missions**: Deep work sessions framed as kingdom quests
- **Guild Halls**: Join guilds with unique aesthetics and collective goals
- **Realm Map**: Explore and unlock new areas as you progress

## Project Structure

```
kingdom-focus/
├── frontend/          # Unity 3D / Game Engine (2.5D isometric)
│   ├── Assets/
│   │   ├── Models/    # 3D models for buildings, characters, props
│   │   ├── Materials/ # Textures and shaders
│   │   ├── Scripts/   # Game logic
│   │   └── Scenes/    # Game scenes
│   └── ProjectSettings/
├── backend/           # Node.js / Python API
│   ├── routes/        # API endpoints
│   ├── models/        # Database schemas
│   ├── controllers/   # Business logic
│   └── middleware/    # Authentication, validation
├── database/          # PostgreSQL / Firebase
└── docs/              # Documentation
```

## Getting Started

### Prerequisites
- Unity 2022 LTS or higher
- Node.js 18+
- PostgreSQL 14+

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/Brodiezz/kingdom-focus.git
   cd kingdom-focus
   ```

2. **Frontend Setup (Unity)**
   ```bash
   cd frontend
   # Open with Unity Hub and import the project
   ```

3. **Backend Setup**
   ```bash
   cd backend
   npm install
   cp .env.example .env
   # Configure your database and API keys
   npm run dev
   ```

## Gameplay Flow

1. **Create Your Kingdom**: Customize your hero and name your kingdom
2. **Schedule Quests**: Plan deep work sessions (15 min - 4 hours)
3. **Complete Sessions**: Focus during your quest time
4. **Earn Rewards**: Gold, XP, and loot for completed sessions
5. **Build & Upgrade**: Use gold to construct and upgrade kingdom buildings
6. **Level Up**: Increase your hero level to unlock new abilities
7. **Join Guild**: Team up with others for cooperative quests
8. **Conquer Realm**: Progress through the map and unlock new territories

## Buildings & Progression

### Early Game
- **Barracks**: Unlock basic warrior abilities
- **Woodcutter's Hut**: Generate passive gold
- **Farm**: Sustain your kingdom

### Mid Game
- **Tower of Knowledge**: Unlock study bonuses
- **Forge**: Craft equipment to boost productivity
- **Market**: Trade with other players

### Late Game
- **Castle Throne Room**: Unlock leadership abilities
- **Arcane Tower**: Advanced spell learning
- **Kingdom Hall**: Massive guild bonuses

## Tech Stack

- **Frontend**: Unity 3D (C#)
- **Backend**: Node.js / Express
- **Database**: PostgreSQL
- **Authentication**: JWT
- **Real-time**: WebSockets for leaderboards & guild chat

## Contributing

Contributions are welcome! Please follow these steps:
1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit changes (`git commit -m 'Add amazing feature'`)
4. Push to branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## License

MIT License - see LICENSE file for details

## Roadmap

- [x] Project setup and structure
- [ ] Core 2.5D rendering pipeline
- [ ] Hero creation and customization
- [ ] Kingdom building system
- [ ] Quest scheduling and tracking
- [ ] Backend API development
- [ ] User authentication
- [ ] Guild system
- [ ] Leaderboards
- [ ] Analytics dashboard
- [ ] Mobile optimization
- [ ] Multiplayer raid system

## Support

For issues and feature requests, please open an issue on GitHub.

---

**Build your kingdom. Master your focus. Conquer the realm.**