const express = require('express');
const cors = require('cors');
const path = require('path');
require('dotenv').config();

const app = express();

// Middleware
app.use(cors({ origin: '*' }));
app.use(express.json());
app.use(express.static(path.join(__dirname, '../frontend')));

// Health check
app.get('/health', (req, res) => {
  res.json({ status: 'Kingdom Focus API is running 🏰' });
});

// Routes
const authRoutes = require('./routes/auth');
const userRoutes = require('./routes/users');
const kingdomRoutes = require('./routes/kingdom');
const questRoutes = require('./routes/quests');
const guildRoutes = require('./routes/guilds');
const leaderboardRoutes = require('./routes/leaderboards');
const analyticsRoutes = require('./routes/analytics');

app.use('/api/auth', authRoutes);
app.use('/api/users', userRoutes);
app.use('/api/kingdom', kingdomRoutes);
app.use('/api/quests', questRoutes);
app.use('/api/guilds', guildRoutes);
app.use('/api/leaderboards', leaderboardRoutes);
app.use('/api/analytics', analyticsRoutes);

// Serve HTML game client
app.get('/', (req, res) => {
  res.sendFile(path.join(__dirname, '../frontend/index.html'));
});

// Error handling
app.use((err, req, res, next) => {
  console.error(err.stack);
  res.status(500).json({ error: 'Internal server error', message: err.message });
});

const PORT = process.env.PORT || 3000;
app.listen(PORT, () => {
  console.log(`
🏰 Kingdom Focus API Server`);
  console.log(`📍 Running on http://localhost:${PORT}`);
  console.log(`🎮 Game at http://localhost:${PORT}/game.html`);
  console.log(`📚 API docs at http://localhost:${PORT}/api/docs`);
  console.log(`\n✅ Ready for conquest!\n`);
});

module.exports = app;
