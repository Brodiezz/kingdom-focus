const express = require('express');
const router = express.Router();
const { authMiddleware } = require('../middleware/auth');
const Quest = require('../models/Quest');
const User = require('../models/User');
const Kingdom = require('../models/Kingdom');
const Hero = require('../models/Hero');

// Get available quests
router.get('/', authMiddleware, async (req, res) => {
  try {
    const quests = await Quest.getByUserId(req.userId);
    res.json({ quests });
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

// Create new quest (schedule session)
router.post('/', authMiddleware, async (req, res) => {
  try {
    const { questName, description, durationMinutes, difficulty = 'Medium' } = req.body;
    
    const quest = await Quest.create(
      req.userId,
      questName,
      description,
      durationMinutes,
      difficulty
    );
    
    res.status(201).json({
      message: 'Quest scheduled successfully',
      quest
    });
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

// Start quest
router.post('/:questId/start', authMiddleware, async (req, res) => {
  try {
    const quest = await Quest.getById(req.params.questId);
    if (!quest || quest.user_id !== req.userId) {
      return res.status(404).json({ error: 'Quest not found' });
    }
    
    const activeQuest = await Quest.startQuest(req.params.questId);
    
    res.json({
      message: 'Quest started',
      quest: activeQuest
    });
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

// Complete quest
router.post('/:questId/complete', authMiddleware, async (req, res) => {
  try {
    const quest = await Quest.getById(req.params.questId);
    if (!quest || quest.user_id !== req.userId) {
      return res.status(404).json({ error: 'Quest not found' });
    }
    
    // Complete quest
    const completedQuest = await Quest.completeQuest(req.params.questId);
    
    // Award gold and XP
    const kingdom = await Kingdom.getByUserId(req.userId);
    const hero = await Hero.getByUserId(req.userId);
    
    await Kingdom.addGold(kingdom.id, completedQuest.gold_reward);
    const updatedHero = await Hero.addExperience(hero.id, completedQuest.xp_reward);
    
    // Update streak
    let stats = await User.getStats(req.userId);
    if (!stats) {
      // Create stats if doesn't exist
      await User.updateStats(req.userId, { current_streak: 1 });
      stats = await User.getStats(req.userId);
    } else {
      stats = await User.updateStats(req.userId, {
        current_streak: stats.current_streak + 1,
        total_quests_completed: (stats.total_quests_completed || 0) + 1,
        total_focus_time_minutes: (stats.total_focus_time_minutes || 0) + completedQuest.duration_minutes
      });
    }
    
    res.json({
      message: 'Quest completed successfully',
      quest: completedQuest,
      rewards: {
        gold: completedQuest.gold_reward,
        xp: completedQuest.xp_reward
      },
      hero: updatedHero,
      stats
    });
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

// Get today's quests
router.get('/today', authMiddleware, async (req, res) => {
  try {
    const quests = await Quest.getTodayQuests(req.userId);
    res.json({ quests });
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

// Get current streak
router.get('/:userId/streak', authMiddleware, async (req, res) => {
  try {
    const stats = await User.getStats(req.params.userId);
    res.json({
      currentStreak: stats?.current_streak || 0,
      longestStreak: stats?.longest_streak || 0
    });
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

module.exports = router;
