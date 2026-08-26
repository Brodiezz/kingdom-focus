const express = require('express');
const router = express.Router();

// Placeholder routes
// GET /api/leaderboards/global - Global leaderboard
router.get('/global', (req, res) => {
  res.json({ message: 'Global leaderboard endpoint - to be implemented' });
});

// GET /api/leaderboards/guild/:guildId - Guild leaderboard
router.get('/guild/:guildId', (req, res) => {
  res.json({ message: 'Guild leaderboard endpoint - to be implemented' });
});

// GET /api/leaderboards/friends/:userId - Friends leaderboard
router.get('/friends/:userId', (req, res) => {
  res.json({ message: 'Friends leaderboard endpoint - to be implemented' });
});

module.exports = router;
