const express = require('express');
const router = express.Router();
const User = require('../models/User');
const Kingdom = require('../models/Kingdom');
const Hero = require('../models/Hero');
const { generateToken } = require('../middleware/auth');
const { body, validationResult } = require('express-validator');

// Register new player
router.post('/register', [
  body('username').isLength({ min: 3 }).withMessage('Username must be at least 3 characters'),
  body('email').isEmail().withMessage('Valid email required'),
  body('password').isLength({ min: 6 }).withMessage('Password must be at least 6 characters')
], async (req, res) => {
  const errors = validationResult(req);
  if (!errors.isEmpty()) {
    return res.status(400).json({ errors: errors.array() });
  }
  
  try {
    const { username, email, password, kingdomName, heroName } = req.body;
    
    // Create user
    const user = await User.create(username, email, password);
    
    // Create kingdom
    const kingdom = await Kingdom.create(user.id, kingdomName || `${username}'s Kingdom`);
    
    // Create hero
    const hero = await Hero.create(user.id, heroName || username);
    
    // Generate token
    const token = generateToken(user.id, user.username);
    
    res.status(201).json({
      message: 'Player registered successfully',
      token,
      user: {
        id: user.id,
        username: user.username,
        email: user.email
      },
      kingdom: { id: kingdom.id, name: kingdom.name },
      hero: { id: hero.id, name: hero.hero_name }
    });
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

// Login player
router.post('/login', [
  body('username').notEmpty(),
  body('password').notEmpty()
], async (req, res) => {
  const errors = validationResult(req);
  if (!errors.isEmpty()) {
    return res.status(400).json({ errors: errors.array() });
  }
  
  try {
    const { username, password } = req.body;
    
    const user = await User.verifyPassword(username, password);
    if (!user) {
      return res.status(401).json({ error: 'Invalid credentials' });
    }
    
    // Update last login
    await User.updateLastLogin(user.id);
    
    // Generate token
    const token = generateToken(user.id, user.username);
    
    res.json({
      message: 'Login successful',
      token,
      user
    });
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

router.post('/logout', (req, res) => {
  res.json({ message: 'Logout successful' });
});

module.exports = router;
