const express = require('express');
const router = express.Router();
const { authMiddleware } = require('../middleware/auth');
const Guild = require('../models/Guild');

// List all guilds
router.get('/', authMiddleware, async (req, res) => {
  try {
    const guilds = await Guild.getAll();
    res.json({ guilds });
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

// Create new guild
router.post('/', authMiddleware, async (req, res) => {
  try {
    const { guildName, description } = req.body;
    
    const guild = await Guild.create(guildName, description, req.userId);
    
    // Add leader as member
    await Guild.addMember(guild.id, req.userId, 'Leader');
    
    res.status(201).json({
      message: 'Guild created successfully',
      guild
    });
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

// Get guild details
router.get('/:guildId', authMiddleware, async (req, res) => {
  try {
    const guild = await Guild.getById(req.params.guildId);
    if (!guild) {
      return res.status(404).json({ error: 'Guild not found' });
    }
    
    const members = await Guild.getMembers(req.params.guildId);
    
    res.json({
      guild,
      members,
      memberCount: members.length
    });
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

// Join guild
router.post('/:guildId/join', authMiddleware, async (req, res) => {
  try {
    const guild = await Guild.getById(req.params.guildId);
    if (!guild) {
      return res.status(404).json({ error: 'Guild not found' });
    }
    
    const member = await Guild.addMember(req.params.guildId, req.userId);
    
    res.json({
      message: 'Joined guild successfully',
      member
    });
  } catch (error) {
    res.status(400).json({ error: error.message });
  }
});

// Leave guild
router.post('/:guildId/leave', authMiddleware, async (req, res) => {
  try {
    await Guild.removeMember(req.params.guildId, req.userId);
    
    res.json({ message: 'Left guild successfully' });
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

// Get guild members
router.get('/:guildId/members', authMiddleware, async (req, res) => {
  try {
    const members = await Guild.getMembers(req.params.guildId);
    res.json({ members });
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

module.exports = router;
