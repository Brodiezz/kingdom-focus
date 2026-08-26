const express = require('express');
const router = express.Router();
const { authMiddleware } = require('../middleware/auth');
const Kingdom = require('../models/Kingdom');
const Building = require('../models/Building');
const User = require('../models/User');

// Get kingdom details
router.get('/:userId', authMiddleware, async (req, res) => {
  try {
    const kingdom = await Kingdom.getByUserId(req.params.userId);
    if (!kingdom) {
      return res.status(404).json({ error: 'Kingdom not found' });
    }
    
    const buildings = await Building.getByKingdom(kingdom.id);
    const totalGoldPerHour = await Building.calculateTotalGoldPerHour(kingdom.id);
    
    res.json({
      kingdom,
      buildings,
      totalGoldPerHour
    });
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

// Construct building
router.post('/:userId/buildings', authMiddleware, async (req, res) => {
  try {
    if (req.userId !== parseInt(req.params.userId)) {
      return res.status(403).json({ error: 'Unauthorized' });
    }
    
    const { buildingType, gridX, gridZ, width, height } = req.body;
    const kingdom = await Kingdom.getByUserId(req.userId);
    
    // Deduct gold
    const buildingCost = 500;
    const updatedKingdom = await Kingdom.spendGold(kingdom.id, buildingCost);
    if (!updatedKingdom) {
      return res.status(400).json({ error: 'Insufficient gold' });
    }
    
    // Create building
    const building = await Building.create(
      kingdom.id,
      buildingType,
      gridX,
      gridZ,
      width || 4,
      height || 4
    );
    
    // Start construction
    const constructionDuration = 60; // seconds
    await Building.startConstruction(building.id, constructionDuration);
    
    res.status(201).json({
      message: 'Building construction started',
      building,
      remainingGold: updatedKingdom.gold
    });
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

// Upgrade building
router.put('/:userId/buildings/:buildingId', authMiddleware, async (req, res) => {
  try {
    if (req.userId !== parseInt(req.params.userId)) {
      return res.status(403).json({ error: 'Unauthorized' });
    }
    
    const building = await Building.getById(req.params.buildingId);
    const kingdom = await Kingdom.getByUserId(req.userId);
    
    if (!building || building.kingdom_id !== kingdom.id) {
      return res.status(404).json({ error: 'Building not found' });
    }
    
    // Deduct gold for upgrade
    const upgradeCost = building.upgrade_cost;
    const updatedKingdom = await Kingdom.spendGold(kingdom.id, upgradeCost);
    if (!updatedKingdom) {
      return res.status(400).json({ error: 'Insufficient gold for upgrade' });
    }
    
    // Upgrade building
    const upgradedBuilding = await Building.upgrade(building.id);
    
    res.json({
      message: 'Building upgraded successfully',
      building: upgradedBuilding,
      remainingGold: updatedKingdom.gold
    });
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

// Get building details
router.get('/:userId/buildings/:buildingId', authMiddleware, async (req, res) => {
  try {
    const building = await Building.getById(req.params.buildingId);
    if (!building) {
      return res.status(404).json({ error: 'Building not found' });
    }
    
    res.json({ building });
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

module.exports = router;
