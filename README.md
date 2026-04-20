# 🧪 VR Molecular Chemistry Lab

## 📌 Overview
This project is a **Virtual Reality (VR) Molecular Chemistry Lab** built using Unity and XR Interaction Toolkit.  
It allows users to interact with atoms, form molecules, and visualize chemical bonding in an immersive 3D environment.

---

## 🎯 Features

### 🔹 Core Interactions
- Grab and manipulate atoms (H, O, C, N) using XR controllers
- Dynamic atom spawning on first interaction
- Proximity-based bonding system
- Controlled bonding (only when actively holding an atom)

---

### 🔹 Molecule Formation
Supports the following molecules:
- H₂
- O₂
- H₂O
- CO₂
- NH₃
- CH₄
- N₂

Features:
- Validates combinations using **MoleculeDatabase (ScriptableObject)**
- Displays molecule prefabs with correct structure
- Prevents invalid or duplicate bonding

---

### 🔹 VR UI System
- Fully **World-Space UI** (VR-friendly)
- Molecule Info Panel displays:
  - Molecule Name
  - Chemical Formula (with subscript formatting)
  - Bond Type (single/double/triple)
  - Description
- Smooth UI animations using LeanTween

---

### 🔹 Molecule Library
- Tracks discovered molecules dynamically
- Displays:
  - Molecule Name
  - Formula
  - Molecule Image
- Prevents duplicate entries using HashSet
- Accessible via controller input

---

### 🔹 Reset System
- Reset molecules back into individual atoms
- Clears bonds and hierarchy
- Restores interaction state

---

### 🔹 Audio Feedback
- Plays sound on successful molecule formation

---

### 🔹 Performance & Stability
- Prevents atom drifting using Rigidbody constraints
- Auto-cleans unused atoms after a time interval
- Smooth fade-out before destruction
- Stable grouping system (no nested hierarchy issues)

---

## 🧱 Architecture

The project follows a modular architecture:

- **MoleculeDatabase (ScriptableObject)**  
  Defines valid molecule combinations and corresponding prefabs

- **AtomController**  
  Handles XR interaction, spawning, bonding eligibility

- **BondManager**  
  Manages bonding logic, grouping, and molecule validation

- **MoleculeController**  
  Controls molecule lifecycle and reset behavior

- **UIManager**  
  Handles VR UI panels and molecule display

- **AudioManager**  
  Manages sound feedback

---

## 🎮 Controls

- **Grab Atom:** XR Controller (Trigger / Grip)
- **Form Bond:** Bring atoms close while holding one
- **Open Library:** Controller button (assigned input)
- **Reset Molecule:** UI button

---

## 🛠️ Tech Stack

- Unity (URP)
- XR Interaction Toolkit
- OpenXR
- TextMeshPro
- LeanTween

---

## ⚠️ Current Limitations

- Bond angles are simplified (not chemically accurate)
- Molecules are prefab-based (not dynamically generated)
- Limited to predefined molecules (7 total)
- No advanced chemistry rules (valency, polarity, etc.)
- Simplified physics for stability

---

## 🚀 Future Improvements

- Dynamic bond angle calculation (real chemistry simulation)
- Procedural molecule generation
- Advanced chemistry rules (valency, electron configuration)
- Improved visual effects (bond glow, transitions)
- Hand tracking support
- Multiplayer collaborative lab
- Voice-guided learning system
- Optimization for standalone VR devices (Meta Quest)

---

## 🙌 Conclusion

This project demonstrates:
- XR interaction design
- Real-time simulation systems
- Modular architecture
- VR UI/UX best practices

---
