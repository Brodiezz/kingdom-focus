const express = require('express');
const router = express.Router();
const { authMiddleware } = require('../middleware/auth');
const db = require('../db/connection');

// Global leaderboard
router.get('/global', authMiddleware, async (req, res) => {
  try {
    const limit = req.query.limit || 100;
    const query = `
      SELECT u.id, u.username, COALESCE(us.total_focus_time_minutes, 0) as focus_time,
             h.level, COUNT(*) as rank
      FROM users u
      LEFT JOIN user_stats us ON u.id = us.user_id
      LEFT JOIN heroes h ON u.id = h.user_id
      GROUP BY u.id, u.username, us.total_focus_time_minutes, h.level
      ORDER BY focus_time DESC, h.level DESC
      LIMIT $1
    `;
    
    const result = await db.query(query, [limit]);
    
    // Add rank
    const leaderboard = result.rows.map((row, index) => ({
      ...row,
      rank: index + 1
    }));
    
    res.json({ leaderboard });
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

// Guild leaderboard
router.get('/guild/:guildId', authMiddleware, async (req, res) => {
  try {
    const query = `
      SELECT u.id, u.username, COALESCE(us.total_focus_time_minutes, 0) as focus_time,
             h.level
      FROM guild_members gm
      JOIN users u ON gm.user_id = u.id
      LEFT JOIN user_stats us ON u.id = us.user_id
      LEFT JOIN heroes h ON u.id = h.user_id
      WHERE gm.guild_id = $1
      ORDER BY focus_time DESC, h.level DESC
    `;
    
    const result = await db.query(query, [req.params.guildId]);
    
    const leaderboard = result.rows.map((row, index) => ({
      ...row,
      rank: index + 1
    }));
    
    res.json({ leaderboard });
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

// Friends leaderboard
router.get('/friends/:userId', authMiddleware, async (req, res) => {
  try {
    // Get user's guild friends
    const query = `
      SELECT u.id, u.username, COALESCE(us.total_focus_time_minutes, 0) as focus_time,
             h.level
      FROM guild_members gm
      JOIN users u ON gm.user_id = u.id
      LEFT JOIN user_stats us ON u.id = us.user_id
      LEFT JOIN heroes h ON u.id = h.user_id
      WHERE gm.guild_id IN (
        SELECT gm2.guild_id FROM guild_members gm2 WHERE gm2.user_id = $1
      )
      AND u.id != $1
      ORDER BY focus_time DESC, h.level DESC
    `;
    
    const result = await db.query(query, [req.params.userId]);
    
    const leaderboard = result.rows.map((row, index) => ({
      ...row,
      rank: index + 1
    }));
    
    res.json({ leaderboard });
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

module.exports = router;
