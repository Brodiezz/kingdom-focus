const express = require('express');
const router = express.Router();
const { authMiddleware } = require('../middleware/auth');
const User = require('../models/User');
const Quest = require('../models/Quest');
const Hero = require('../models/Hero');

// Get user analytics
router.get('/:userId', authMiddleware, async (req, res) => {
  try {
    if (req.userId !== parseInt(req.params.userId)) {
      return res.status(403).json({ error: 'Unauthorized' });
    }
    
    const stats = await User.getStats(req.userId);
    const hero = await Hero.getByUserId(req.userId);
    
    res.json({
      stats,
      heroLevel: hero?.level || 1,
      heroExperience: hero?.experience || 0
    });
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

// Get detailed stats
router.get('/:userId/stats', authMiddleware, async (req, res) => {
  try {
    if (req.userId !== parseInt(req.params.userId)) {
      return res.status(403).json({ error: 'Unauthorized' });
    }
    
    const stats = await User.getStats(req.userId);
    const recentQuests = await Quest.getTodayQuests(req.userId);
    const hero = await Hero.getByUserId(req.userId);
    
    const completedQuests = recentQuests.filter(q => q.status === 'Completed');
    const totalFocusToday = completedQuests.reduce((sum, q) => sum + q.duration_minutes, 0);
    
    res.json({
      stats,
      todayStats: {
        questsCompleted: completedQuests.length,
        totalFocusMinutes: totalFocusToday,
        goldEarned: completedQuests.reduce((sum, q) => sum + q.gold_reward, 0),
        xpEarned: completedQuests.reduce((sum, q) => sum + q.xp_reward, 0)
      },
      hero: {
        level: hero?.level || 1,
        experience: hero?.experience || 0,
        health: hero?.health || 100
      }
    });
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

module.exports = router;
