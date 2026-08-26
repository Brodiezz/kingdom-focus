# Kingdom Focus

🏰 A medieval-themed productivity game that turns deep work into an epic quest!

## 🎮 Features

- **2.5D Isometric Graphics** (Clash of Clans style)
- **Kingdom Building System** - Construct and upgrade medieval structures
- **Quest-Based Sessions** - Turn focus time into game sessions
- **Hero Progression** - Level up your character through productivity
- **Guild System** - Join guilds and compete with other players
- **Leaderboards** - Global, guild, and friends rankings
- **Streak Tracking** - Maintain daily focus streaks for rewards
- **Analytics Dashboard** - Track your productivity journey
- **Achievement System** - Unlock badges and milestones

## 🚀 Quick Start

### Backend
```bash
cd backend
npm install
cp .env.example .env
# Edit .env with database credentials
npm run dev
```

### Frontend (Unity)
```bash
cd frontend
# Open with Unity 2022 LTS+
# Configure API URL in ApiService.cs
# Press Play in Editor
```

## 📚 Documentation

- [Setup Guide](docs/SETUP_GUIDE.md) - Complete installation & configuration
- [API Documentation](docs/API_DOCUMENTATION.md) - Full API reference
- [Architecture](docs/ARCHITECTURE.md) - System design overview
- [3D Models Guide](docs/3D_MODELS_GUIDE.md) - Asset specifications

## 🏗️ Project Structure

```
kingdom-focus/
├── backend/              # Node.js Express API
│   ├── src/
│   │   ├── routes/      # API endpoints
│   │   ├── models/      # Database models
│   │   ├── middleware/  # Auth & validation
│   │   └── db/          # Database config
│   └── package.json
├── frontend/             # Unity 3D Game
│   ├── Assets/
│   │   ├── Scripts/
│   │   │   ├── Managers/
│   │   │   ├── Network/
│   │   │   ├── UI/
│   │   │   ├── Systems/
│   │   │   └── ...
│   │   ├── Models/
│   │   ├── Materials/
│   │   └── Scenes/
│   └── ProjectSettings/
└── docs/                # Documentation
```

## 🛠️ Tech Stack

**Backend**
- Node.js + Express.js
- PostgreSQL
- JWT Authentication

**Frontend**
- Unity 3D (C#)
- 2.5D Isometric Rendering
- TextMesh Pro UI

## 📊 Progression System

### Same as Focus Jungle
1. **Schedule Quests** - Plan deep work sessions (15 min - 4 hours)
2. **Complete Sessions** - Focus during quest time
3. **Earn Rewards** - Gold, XP, loot
4. **Build Kingdom** - Construct medieval buildings
5. **Level Up** - Improve hero skills and abilities
6. **Unlock Features** - New buildings, abilities at higher levels
7. **Join Guilds** - Collaborate with other focused players
8. **Climb Leaderboards** - Compete globally or in guilds

## 🎨 Medieval Theme

**Buildings**
- Early: Barracks, Woodcutter's Hut, Farm
- Mid: Tower of Knowledge, Forge, Market
- Late: Castle Throne Room, Arcane Tower, Guild Hall

**Heroes**
- Customizable warrior character
- Equipment slots (armor, weapons, accessories)
- Special abilities unlock at higher levels

## 🔐 Security

- Bcrypt password hashing
- JWT token authentication
- Input validation & sanitization
- SQL injection prevention
- CORS configuration

## 📈 Performance

- Optimized database queries with indexes
- Connection pooling
- LOD models for efficient rendering
- Texture compression
- Response caching

## 🚢 Deployment

### Backend
```bash
# Heroku
git push heroku main
```

### Frontend
```bash
# Build Standalone
File > Build Settings > Build

# Build WebGL
File > Build Settings > WebGL > Build
```

## 🤝 Contributing

1. Fork the repository
2. Create feature branch (`git checkout -b feature/amazing-feature`)
3. Commit changes (`git commit -m 'Add amazing feature'`)
4. Push to branch (`git push origin feature/amazing-feature`)
5. Open Pull Request

## 📝 Roadmap

- [x] Core project structure
- [x] Backend API setup
- [x] Database schema
- [x] Frontend UI panels
- [ ] 3D models & textures
- [ ] Animations
- [ ] Sound & music
- [ ] Polished UI/UX
- [ ] Mobile optimization
- [ ] Multiplayer raids
- [ ] In-game shop
- [ ] Season system

## 📄 License

MIT License - see LICENSE file for details

## 🏰 Vision

"Transform your focus time into an epic kingdom-building adventure. Every session completed brings you closer to becoming a legendary ruler. Whether you're coding, writing, studying, or creating - Kingdom Focus gamifies your productivity and connects you with a community of focused individuals."

---

**Build your kingdom. Master your focus. Conquer the realm.** 🏰⚔️
