const express = require('express');
const router = express.Router();
const { authMiddleware } = require('../middleware/auth');
const User = require('../models/User');

// Get user profile
router.get('/profile', authMiddleware, async (req, res) => {
  try {
    const user = await User.findById(req.userId);
    const stats = await User.getStats(req.userId);
    
    res.json({
      user,
      stats
    });
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

// Get user by ID
router.get('/:userId', authMiddleware, async (req, res) => {
  try {
    const user = await User.findById(req.params.userId);
    if (!user) {
      return res.status(404).json({ error: 'User not found' });
    }
    
    const stats = await User.getStats(req.params.userId);
    
    res.json({ user, stats });
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

// Update user profile
router.put('/:userId', authMiddleware, async (req, res) => {
  try {
    if (req.userId !== parseInt(req.params.userId)) {
      return res.status(403).json({ error: 'Unauthorized' });
    }
    
    // Add profile update logic here
    res.json({ message: 'Profile updated successfully' });
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

module.exports = router;
