const express = require('express');
const cors = require('cors');
const path = require('path');
require('dotenv').config();

const app = express();
const PORT = process.env.PORT || 3000;

// Middleware
app.use(cors());
app.use(express.json());
app.use(express.static(path.join(__dirname, '../public')));

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

// Health check
app.get('/health', (req, res) => {
  res.json({ status: 'Kingdom Focus API is running! 🏰' });
});

// Serve index.html for web version
app.get('/', (req, res) => {
  res.sendFile(path.join(__dirname, '../public/index.html'));
});

// Error handling
app.use((err, req, res, next) => {
  console.error(err.stack);
  res.status(500).json({ error: 'Internal server error' });
});

app.listen(PORT, () => {
  console.log(`🏰 Kingdom Focus API running on port ${PORT}`);
  console.log(`📱 Web version: http://localhost:${PORT}`);
  console.log(`📚 API docs: http://localhost:${PORT}/api`);
});

module.exports = app;
