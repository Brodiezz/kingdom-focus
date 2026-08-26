# Kingdom Focus - Architecture Overview

## System Architecture

```
┌─────────────────────────────────────────────────────────┐
│                    Unity Frontend                        │
│  (2.5D Isometric Game - Clash of Clans Style)          │
│                                                          │
│  ├─ Login/Registration UI                              │
│  ├─ Kingdom Building System                            │
│  ├─ Quest/Session Management                           │
│  ├─ Hero Character & Equipment                         │
│  ├─ Leaderboards & Analytics                           │
│  └─ Guild System                                        │
└─────────────────────────────────────────────────────────┘
                         ↓
                    HTTP/REST API
                    JWT Authentication
                         ↓
┌─────────────────────────────────────────────────────────┐
│                   Node.js Backend                        │
│            (Express.js REST API Server)                │
│                                                          │
│  ├─ Authentication Service                             │
│  ├─ Kingdom Management                                 │
│  ├─ Quest Tracking & Rewards                           │
│  ├─ Guild System                                        │
│  ├─ Leaderboard Generation                             │
│  └─ Analytics & User Stats                             │
└─────────────────────────────────────────────────────────┘
                         ↓
┌─────────────────────────────────────────────────────────┐
│                  PostgreSQL Database                     │
│                                                          │
│  ├─ Users & Authentication                             │
│  ├─ Kingdoms & Buildings                               │
│  ├─ Heroes & Equipment                                 │
│  ├─ Quests & Sessions                                  │
│  ├─ Guilds & Members                                   │
│  ├─ User Statistics                                    │
│  └─ Achievements                                        │
└─────────────────────────────────────────────────────────┘
```

## Frontend Architecture (Unity)

### Scene Structure
```
LoginScene/
  ├─ LoginPanel
  ├─ ApiService
  └─ Canvas

GameScene/
  ├─ GameManager (Game Controller)
  ├─ Player (Hero Character)
  ├─ Kingdom (Building Manager)
  ├─ GridSystem (Building Placement)
  ├─ IsometricCamera (Main Camera)
  ├─ QuestManager
  ├─ Canvas (UI)
  │  ├─ LoginPanel
  │  ├─ KingdomPanel
  ��  ├─ QuestPanel
  │  ├─ LeaderboardPanel
  │  └─ AnalyticsPanel
  ├─ AchievementSystem
  ├─ SoundManager
  └─ EventManager
```

### Component Hierarchy

**Managers (Singletons)**
- `GameManager` - Central game controller
- `ApiService` - Network communication
- `EventManager` - Game events bus
- `SoundManager` - Audio management
- `AchievementSystem` - Achievement tracking

**Game Systems**
- `Player` - Hero progression
- `Kingdom` - Resource management
- `Building` - Structure management
- `Quest` - Session tracking
- `QuestManager` - Quest lifecycle
- `GridSystem` - Placement validation

**Rendering & Camera**
- `IsometricCamera` - 2.5D camera control
- `IsometricRenderer` - Orthographic setup
- `BuildingAnimator` - Building animations
- `HeroAnimator` - Character animations

**UI Panels**
- `LoginPanel` - Authentication
- `KingdomPanel` - Kingdom view
- `QuestPanel` - Quest scheduling
- `LeaderboardPanel` - Rankings
- `AnalyticsPanel` - Stats display

## Backend Architecture (Node.js)

### API Layer
```
routes/
  ├─ auth.js (Authentication)
  ├─ users.js (User profiles)
  ├─ kingdom.js (Building & resources)
  ├─ quests.js (Quest management)
  ├─ guilds.js (Guild system)
  ├─ leaderboards.js (Rankings)
  └─ analytics.js (Statistics)
```

### Data Layer
```
models/
  ├─ User.js (User authentication & profiles)
  ├─ Kingdom.js (Kingdom state)
  ├─ Building.js (Building management)
  ├─ Quest.js (Quest tracking)
  ├─ Hero.js (Character progression)
  └─ Guild.js (Guild management)
```

### Middleware
```
middleware/
  └─ auth.js (JWT verification)
```

### Database Layer
```
db/
  ├─ connection.js (Pool management)
  └─ schema.sql (Database schema)
```

## Data Flow

### Authentication Flow
1. Player enters credentials in LoginPanel
2. ApiService sends POST to `/auth/login`
3. Backend verifies password with bcrypt
4. JWT token generated and returned
5. Token stored in PlayerPrefs
6. All future requests include token in header

### Quest Completion Flow
1. Player completes timer in QuestPanel
2. Calls `CompleteQuest()` via ApiService
3. Backend calculates rewards (gold, XP)
4. Updates Hero experience
5. Increments user streak
6. Updates kingdom gold
7. Response includes all updated stats
8. Frontend updates UI with EventManager notifications

### Building Construction Flow
1. Player clicks build button in KingdomPanel
2. Selects building type and grid position
3. ApiService calls `/kingdom/:userId/buildings` (POST)
4. Backend validates placement and gold
5. Deducts gold from kingdom
6. Creates building with construction timer
7. Frontend receives building data
8. BuildingAnimator plays construction animation
9. On completion, building appears in GridSystem

## Database Schema

### Key Tables
- **users** - Player accounts
- **kingdoms** - Kingdom state per user
- **buildings** - Constructed buildings
- **heroes** - Character progression
- **quests** - Session tracking
- **user_stats** - Aggregate statistics
- **guilds** - Guild management
- **guild_members** - Guild membership
- **achievements** - Badge system
- **user_achievements** - Achievement tracking

## Security

### Authentication
- Passwords hashed with bcrypt (10 rounds)
- JWT tokens with 7-day expiration
- Tokens validated on all protected endpoints

### Authorization
- Users can only access their own data
- userId validation on all requests
- Guild membership verified for guild operations

### Data Validation
- Input validation with express-validator
- SQL injection prevention via parameterized queries
- CORS enabled for frontend domain only
- Rate limiting implemented

## Scalability

### Current Limitations
- Single server deployment
- No horizontal scaling
- Basic leaderboard (could use caching)

### Future Improvements
- Redis for caching
- Load balancing
- Database replication
- CDN for assets
- WebSocket for real-time updates

## Performance Considerations

### Backend
- Database indexes on foreign keys
- Query optimization
- Connection pooling
- Efficient JSON responses

### Frontend
- LOD models for 2.5D rendering
- Object pooling for particles
- Lazy loading of UI panels
- Texture compression

---

For detailed implementation, see code files and docs/
