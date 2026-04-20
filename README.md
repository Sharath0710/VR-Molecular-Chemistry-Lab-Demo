# VR-Molecular-Chemistry-Lab
🧪 VR Molecular Chemistry Lab
📌 Overview

This project is a Virtual Reality (VR) Molecular Chemistry Lab built using Unity and XR Interaction Toolkit.
It allows users to interact with atoms, form molecular structures, and visualize chemical bonding in an immersive 3D environment.

🎯 Features
🔹 Core Interactions
Grab and manipulate atoms (H, O, C, N) using VR controllers
Dynamic atom spawning on first interaction
Proximity-based bonding system
Controlled bonding (only when actively interacting)
🔹 Molecule Formation
Supports molecule creation:
H₂
O₂
H₂O
CO₂
NH₃
CH₄
N₂
Validates combinations using a MoleculeDatabase (ScriptableObject)
Displays prebuilt molecule prefabs with correct structure
🔹 Visual & UI System
Fully World-Space UI (VR-friendly)
Molecule Info Panel displaying:
Name
Formula (with subscript formatting)
Bond type (single/double/triple)
Description
Smooth UI animations using LeanTween
🔹 Molecule Library
Tracks all discovered molecules
Displays:
Molecule name
Formula
Visual representation
Prevents duplicate entries
Accessible via controller input
🔹 Reset Mechanism
Reset molecules back into atoms
Clears bonding and grouping
Restores interaction state
🔹 Audio Feedback
Sound effect on successful molecule formation
🔹 Performance & Stability
Prevents atom drifting using physics constraints
Automatic cleanup of unused atoms
Fade-out destruction for better UX
Controlled grouping system to avoid hierarchy issues
🧱 Architecture

The project follows a modular architecture:

MoleculeDatabase (ScriptableObject)
Defines valid molecule combinations and prefabs
AtomController
Handles XR interaction, spawning, bonding eligibility
BondManager
Manages bonding logic and molecule validation
MoleculeController
Handles molecule lifecycle and reset logic
UIManager
Controls VR UI panels and molecule display
AudioManager
Manages audio feedback
🎮 Controls
Grab: XR Controller Grip/Trigger
Form Bonds: Bring atoms close while holding one
Open Library: Controller button (configured input)
Reset Molecule: UI button
🛠️ Tech Stack
Unity (URP)
XR Interaction Toolkit
OpenXR
TextMeshPro
LeanTween
⚠️ Current Limitations
Bond geometry is simplified (not physically accurate angles)
Molecules are prefab-based rather than dynamically generated
No advanced chemical rules (e.g., electron shells, polarity)
Limited molecule set (7 predefined molecules)
Physics-based snapping is simplified for stability
🚀 Future Improvements
Dynamic bond angle calculation (real chemistry simulation)
Procedural molecule generation
Advanced chemistry rules (valency, electron sharing)
Improved visual effects (bond glow, energy transfer)
Hand tracking support
Multiplayer collaborative lab
Voice-guided learning system
Performance optimization for standalone VR devices (Quest)
🙌 Conclusion

This project demonstrates:

XR interaction design
Real-time simulation logic
Modular architecture
VR UI/UX best practices
