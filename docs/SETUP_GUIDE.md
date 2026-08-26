# Development Setup Guide

## Prerequisites
- Node.js 18+ ([Download](https://nodejs.org/))
- PostgreSQL 14+ ([Download](https://www.postgresql.org/download/))
- Unity 2022 LTS+ ([Download](https://unity.com/download))
- Git ([Download](https://git-scm.com/))

## Backend Setup

### 1. Database Setup

```bash
# Create database
psql -U postgres
CREATE DATABASE kingdom_focus;
\c kingdom_focus

# Run schema
\i backend/src/db/schema.sql
```

### 2. Backend Installation

```bash
cd backend

# Install dependencies
npm install

# Create .env file
cp .env.example .env

# Edit .env with your database credentials
nano .env

# Start development server
npm run dev
```

**Expected output:**
```
🏰 Kingdom Focus API running on port 3000
```

### Backend API Routes

#### Authentication
- `POST /api/auth/register` - Register new player
- `POST /api/auth/login` - Login player
- `POST /api/auth/logout` - Logout

#### Kingdom
- `GET /api/kingdom/:userId` - Get kingdom details
- `POST /api/kingdom/:userId/buildings` - Construct building
- `PUT /api/kingdom/:userId/buildings/:buildingId` - Upgrade building

#### Quests
- `GET /api/quests` - Get all quests
- `POST /api/quests` - Create quest
- `POST /api/quests/:questId/start` - Start quest
- `POST /api/quests/:questId/complete` - Complete quest
- `GET /api/quests/:userId/streak` - Get streak

#### Guilds
- `GET /api/guilds` - List guilds
- `POST /api/guilds` - Create guild
- `GET /api/guilds/:guildId` - Get guild details
- `POST /api/guilds/:guildId/join` - Join guild
- `POST /api/guilds/:guildId/leave` - Leave guild

#### Leaderboards
- `GET /api/leaderboards/global` - Global leaderboard
- `GET /api/leaderboards/guild/:guildId` - Guild leaderboard
- `GET /api/leaderboards/friends/:userId` - Friends leaderboard

#### Analytics
- `GET /api/analytics/:userId` - User analytics
- `GET /api/analytics/:userId/stats` - Detailed stats

## Frontend Setup (Unity)

### 1. Create Project

```bash
# Clone/open in Unity
cd frontend

# Open with Unity Hub - Import as 3D Project
```

### 2. Install Required Packages

In Unity Package Manager:
- TextMesh Pro (included)
- Newtonsoft JSON (via NuGet)

```bash
cd Assets
# Create Plugins folder for DLLs if needed
```

### 3. Configure API Service

In `Assets/Scripts/Network/ApiService.cs`, update:
```csharp
private string apiUrl = "http://YOUR_API_URL/api";
```

### 4. Setup Scenes

**LoginScene:**
- Create new scene
- Add LoginPanel script
- Configure input fields and buttons

**GameScene:**
- Add GameManager
- Add IsometricCamera
- Add GridSystem
- Add UI Panels (Kingdom, Quest, Leaderboard)
- Add ApiService

### 5. Run in Editor

- Press Play in Unity Editor
- Test login/registration
- Verify API calls in browser console

## Deployment

### Backend Deployment (Heroku)

```bash
cd backend

# Install Heroku CLI
# (https://devcenter.heroku.com/articles/heroku-cli)

# Login
heroku login

# Create app
heroku create kingdom-focus-api

# Set environment variables
heroku config:set DATABASE_URL=your_postgres_url
heroku config:set JWT_SECRET=your_secret_key

# Deploy
git push heroku main

# View logs
heroku logs --tail
```

### Frontend Deployment (Unity Cloud / WebGL)

#### Option 1: Build Standalone
```
File > Build Settings
- Select Platform (Windows/Mac/Linux)
- Click Build
```

#### Option 2: WebGL Build
```
File > Build Settings
- Select WebGL
- Player Settings > Publishing Settings
- Build and upload to hosting (GitHub Pages, Netlify, etc.)
```

#### Option 3: Mobile Build
```
File > Build Settings
- Select iOS or Android
- Configure build settings
- Build to device/emulator
```

## Environment Variables

### Backend (.env)
```
DATABASE_URL=postgresql://user:password@localhost:5432/kingdom_focus
DB_HOST=localhost
DB_PORT=5432
DB_NAME=kingdom_focus
DB_USER=postgres
DB_PASSWORD=your_password

NODE_ENV=development
PORT=3000
API_URL=http://localhost:3000

JWT_SECRET=your_super_secret_key
JWT_EXPIRATION=7d

FRONTEND_URL=http://localhost:3000

ENABLE_GUILDS=true
ENABLE_LEADERBOARDS=true
ENABLE_MULTIPLAYER=false
```

## Testing

### Backend Tests
```bash
cd backend
npm test
```

### API Testing with Postman

1. Download [Postman](https://www.postman.com/)
2. Import `backend/postman_collection.json`
3. Set environment variables
4. Run requests

## Troubleshooting

### Database Connection Error
```bash
# Check PostgreSQL is running
psql -U postgres -d postgres

# Verify credentials in .env
```

### API Not Responding
```bash
# Check server is running
curl http://localhost:3000/health

# View server logs
npm run dev
```

### CORS Issues
```bash
# Enable CORS in backend/src/index.js
app.use(cors({ origin: 'http://localhost:3000' }));
```

### Unity API Errors
- Check API URL in ApiService.cs
- Verify AuthToken is set after login
- Check network conditions in browser DevTools

## Performance Optimization

### Backend
- Enable query caching
- Add database indexes (already in schema)
- Implement rate limiting
- Use connection pooling

### Frontend (Unity)
- Use LOD models for buildings
- Implement object pooling for particles
- Batch UI updates
- Compress textures to appropriate sizes

## Next Steps

1. **Implement 3D Models** - Add building and hero models
2. **Add Animations** - Create building construction and hero animations
3. **Sound & Music** - Add audio assets
4. **Polish UI** - Refine user experience
5. **Test & Debug** - Thorough testing across platforms
6. **Deploy** - Launch to production

---

**Need help?** Check docs/ folder or open an issue on GitHub!
