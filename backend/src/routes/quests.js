const express = require('express');
const router = express.Router();

// Placeholder routes
// GET /api/quests - Get available quests
router.get('/', (req, res) => {
  res.json({ message: 'Get quests endpoint - to be implemented' });
});

// POST /api/quests - Create new quest (session)
router.post('/', (req, res) => {
  res.json({ message: 'Create quest endpoint - to be implemented' });
});

// PUT /api/quests/:questId - Complete quest
router.put('/:questId/complete', (req, res) => {
  res.json({ message: 'Complete quest endpoint - to be implemented' });
});

// GET /api/quests/:userId/streak - Get current streak
router.get('/:userId/streak', (req, res) => {
  res.json({ message: 'Get streak endpoint - to be implemented' });
});

module.exports = router;
