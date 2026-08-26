# API Documentation

## Authentication

All protected endpoints require a JWT token in the Authorization header:

```
Authorization: Bearer <token>
```

## Base URL

```
http://localhost:3000/api
```

## Error Responses

All errors follow this format:

```json
{
  "error": "Error message",
  "status": 400
}
```

## Endpoints

### Authentication

#### Register Player
```
POST /auth/register

Body:
{
  "username": "string",
  "email": "string",
  "password": "string",
  "kingdomName": "string",
  "heroName": "string"
}

Response (201):
{
  "message": "Player registered successfully",
  "token": "jwt_token",
  "user": { "id": 1, "username": "...", "email": "..." },
  "kingdom": { "id": 1, "name": "..." },
  "hero": { "id": 1, "name": "..." }
}
```

#### Login
```
POST /auth/login

Body:
{
  "username": "string",
  "password": "string"
}

Response (200):
{
  "message": "Login successful",
  "token": "jwt_token",
  "user": { "id": 1, "username": "...", "email": "..." }
}
```

### Kingdom

#### Get Kingdom
```
GET /kingdom/:userId

Response (200):
{
  "kingdom": {
    "id": 1,
    "user_id": 1,
    "name": "My Kingdom",
    "level": 1,
    "gold": 1000,
    "gold_per_hour": 10
  },
  "buildings": [...],
  "totalGoldPerHour": 50
}
```

#### Build Building
```
POST /kingdom/:userId/buildings

Body:
{
  "buildingType": "Barracks",
  "gridX": 0,
  "gridZ": 0,
  "width": 4,
  "height": 4
}

Response (201):
{
  "message": "Building construction started",
  "building": { "id": 1, ... },
  "remainingGold": 500
}
```

#### Upgrade Building
```
PUT /kingdom/:userId/buildings/:buildingId

Response (200):
{
  "message": "Building upgraded successfully",
  "building": { "id": 1, "level": 2, ... },
  "remainingGold": 250
}
```

### Quests

#### Create Quest
```
POST /quests

Body:
{
  "questName": "Code Review",
  "description": "Review pull requests",
  "durationMinutes": 45,
  "difficulty": "Medium"
}

Response (201):
{
  "message": "Quest scheduled successfully",
  "quest": {
    "id": 1,
    "user_id": 1,
    "quest_name": "Code Review",
    "status": "Scheduled",
    "gold_reward": 90,
    "xp_reward": 225
  }
}
```

#### Start Quest
```
POST /quests/:questId/start

Response (200):
{
  "message": "Quest started",
  "quest": { "id": 1, "status": "Active", ... }
}
```

#### Complete Quest
```
POST /quests/:questId/complete

Response (200):
{
  "message": "Quest completed successfully",
  "quest": { "id": 1, "status": "Completed", ... },
  "rewards": {
    "gold": 100,
    "xp": 250
  },
  "hero": { "level": 2, "experience": 500, ... },
  "stats": { "current_streak": 1, ... }
}
```

#### Get Current Streak
```
GET /quests/:userId/streak

Response (200):
{
  "currentStreak": 5,
  "longestStreak": 12
}
```

### Guilds

#### List Guilds
```
GET /guilds

Response (200):
{
  "guilds": [
    {
      "id": 1,
      "name": "Dragon Slayers",
      "description": "For the focused",
      "level": 5,
      "gold_treasury": 5000
    }
  ]
}
```

#### Create Guild
```
POST /guilds

Body:
{
  "guildName": "Dragon Slayers",
  "description": "For the focused"
}

Response (201):
{
  "message": "Guild created successfully",
  "guild": { "id": 1, ... }
}
```

#### Join Guild
```
POST /guilds/:guildId/join

Response (200):
{
  "message": "Joined guild successfully",
  "member": { "id": 1, "guild_id": 1, "user_id": 1, "role": "Member" }
}
```

### Leaderboards

#### Global Leaderboard
```
GET /leaderboards/global?limit=100

Response (200):
{
  "leaderboard": [
    {
      "id": 1,
      "username": "TopFocuser",
      "focus_time": 5000,
      "level": 20,
      "rank": 1
    }
  ]
}
```

#### Guild Leaderboard
```
GET /leaderboards/guild/:guildId

Response (200):
{
  "leaderboard": [...]
}
```

### Analytics

#### Get User Analytics
```
GET /analytics/:userId

Response (200):
{
  "stats": {
    "current_streak": 5,
    "longest_streak": 12,
    "total_quests_completed": 50,
    "total_focus_time_minutes": 5000
  },
  "heroLevel": 15,
  "heroExperience": 2500
}
```

#### Get Detailed Stats
```
GET /analytics/:userId/stats

Response (200):
{
  "stats": { ... },
  "todayStats": {
    "questsCompleted": 3,
    "totalFocusMinutes": 120,
    "goldEarned": 300,
    "xpEarned": 750
  },
  "hero": {
    "level": 15,
    "experience": 2500,
    "health": 100
  }
}
```

## Rate Limiting

API endpoints are rate limited:
- 100 requests per minute for authenticated users
- 10 requests per minute for unauthenticated users

## Pagination

Where applicable, endpoints support pagination:
```
?page=1&limit=20
```

---

For more examples, see `backend/postman_collection.json`
