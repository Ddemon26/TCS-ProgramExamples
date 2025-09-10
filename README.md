# TCS ProgramExamples

![GitHub Forks](https://img.shields.io/github/forks/Ddemon26/TCS-ProgramExamples)
![GitHub Contributors](https://img.shields.io/github/contributors/Ddemon26/TCS-ProgramExamples)
![GitHub Stars](https://img.shields.io/github/stars/Ddemon26/TCS-ProgramExamples)
![GitHub Repo Size](https://img.shields.io/github/repo-size/Ddemon26/TCS-ProgramExamples)

[![Join our Discord](https://img.shields.io/badge/Discord-Join%20Us-7289DA?logo=discord&logoColor=white)](https://discord.gg/knwtcq3N2a)
![Discord](https://img.shields.io/discord/1047781241010794506)

## Overview

**TCS ProgramExamples** is a comprehensive collection of Unity programming examples and educational resources designed to help developers learn and implement various programming concepts in Unity. This repository covers advanced topics including blockchain implementation, input system management, matrix operations, C# records usage, and Unity editor development.

## Features

- **Blockchain Implementation:** Complete blockchain system with blocks, transactions, wallets, and UTXO management
- **Input System Examples:** Unity Input System examples with rebinding UI for version 1.11.2
- **Matrix Operations:** BSP Dungeon Generator and Inventory Matrix system implementations
- **C# Records Examples:** Comprehensive examples of C# 9.0 Records usage in Unity (high, medium, and low-level)
- **Editor Tools:** Unity Editor toolbar overlay examples and custom editor development
- **Educational Focus:** Well-documented examples designed for learning and teaching programming concepts

## Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/Ddemon26/TCS-ProgramExamples.git
   ```

2. Add the cloned folder to your Unity project’s `Assets` folder.

3. Each example category has its own assembly definition file (.asmdef) for modular usage:
   - `TCS.Examples.Blockchain.asmdef` - Blockchain examples
   - `TCS.Examples.Matrix.asmdef` - Matrix operation examples  
   - `TCS.Examples.Records.asmdef` - C# Records examples

## Usage Examples

This repository contains several categories of programming examples for Unity development:

### Blockchain Examples (`Runtime/Blockchain/`)

Complete blockchain implementation demonstrating:
- **Block creation and mining** with proof-of-work consensus
- **Transaction management** with input/output validation  
- **UTXO (Unspent Transaction Output)** management system
- **Wallet functionality** for managing keys and balances
- **Node management** for peer-to-peer networking

```csharp
// Example: Creating a simple blockchain transaction
var blockchain = new Blockchain();
var wallet = new Wallet();
var transaction = wallet.CreateTransaction("recipientAddress", 50.0f, blockchain);
blockchain.AddTransaction(transaction);
```

### C# Records Examples (`Runtime/RecordExamples/`)

Comprehensive examples of C# 9.0 Records usage across three complexity levels:
- **High-Level Examples**: Event bus systems, friend request management
- **Mid-Level Examples**: Inventory systems, trade management, achievements  
- **Low-Level Examples**: AI settings, game events, difficulty configuration

Detailed documentation available in: `Runtime/RecordExamples/README.md`

### Matrix Examples (`Runtime/MatrixExamples/`)

Advanced matrix operations and systems:
- **BSP Dungeon Generator**: Binary space partitioning for procedural dungeon generation
- **Inventory Matrix System**: Grid-based inventory management with spatial logic

### Input System Examples (`Runtime/Input System/`)

Unity Input System integration examples:
- **Rebinding UI**: Runtime key binding modification interface  
- **Version 1.11.2 Compatibility**: Examples for current Unity Input System

### Editor Examples (`Editor/ToolbarOverlayExample/`)

Unity Editor customization examples:
- **Custom Toolbar Overlays**: Scene view toolbar extensions
- **Dropdown Controls**: Interactive editor UI elements
- **Toggle Systems**: Editor state management

## Learning Resources

Each example category includes:
- **Comprehensive Documentation**: Detailed README files explaining concepts and usage
- **Well-Commented Code**: Clear explanations of implementation details
- **Progressive Complexity**: Examples ranging from basic to advanced implementations
- **Real-World Applications**: Practical use cases for game development scenarios

## Getting Started

1. **Browse Examples**: Explore the `Runtime/` and `Editor/` folders to find examples of interest
2. **Read Documentation**: Check individual README files for detailed explanations
3. **Import Selectively**: Use assembly definitions to import only needed examples
4. **Experiment**: Modify and extend examples to fit your specific needs


## Contribution

Contributions are encouraged! If you find issues or have ideas for improvements, feel free to submit a pull request or open an issue.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.

## Support

Join our community on Discord for support, feedback, and discussions:  
[![Join our Discord](https://img.shields.io/badge/Discord-Join%20Us-7289DA?logo=discord&logoColor=white)](https://discord.gg/knwtcq3N2a)

