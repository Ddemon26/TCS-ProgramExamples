# TCS AudioManager

![GitHub Forks](https://img.shields.io/github/forks/Ddemon26/TCS-AudioManager)
![GitHub Contributors](https://img.shields.io/github/contributors/Ddemon26/TCS-AudioManager)
![GitHub Stars](https://img.shields.io/github/stars/Ddemon26/TCS-AudioManager)
![GitHub Repo Size](https://img.shields.io/github/repo-size/Ddemon26/TCS-AudioManager)

[![Join our Discord](https://img.shields.io/badge/Discord-Join%20Us-7289DA?logo=discord&logoColor=white)](https://discord.gg/knwtcq3N2a)
![Discord](https://img.shields.io/discord/1047781241010794506)

## Overview

**TCS AudioManager** is a comprehensive and scalable audio management system for Unity projects. It features advanced audio event handling, pooling of sound sources, centralized control via the AudioManager, and support for audio logging. This system is designed to enhance performance while maintaining flexibility for projects of all sizes.

## Features

- **Audio Event System:** Define and play complex audio events with custom properties.
- **Audio Pooling:** Efficient management of sound instances for better performance.
- **Centralized Audio Control:** Singleton-based AudioManager to handle all audio operations.
- **Global Audio Settings:** Use `AudioManagerSettings` to adjust volumes and global settings.
- **Debug Logging:** Integrated logger for tracking audio-related actions and errors.
- **Benchmarking:** Built-in tests and tools for performance evaluation.

## Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/Ddemon26/TCS-AudioManager.git
   ```

2. Add the cloned folder to your Unity project’s `Assets` folder.

3. Ensure the `TCS.AudioManager.asmdef` is included in your Unity assemblies.

## Usage

### Initializing AudioManager

Ensure the `AudioManager` is initialized at the start of your project lifecycle:

```csharp
AudioManager.Instance.Initialize();
```

### Creating and Playing Audio Events

Create a custom `AudioEvent` using the builder pattern, where you can configure properties such as volume, pitch, and looping:

```csharp
AudioEvent audioEvent = new AudioEvent.Builder()
    .WithClip(yourAudioClip)  // Set your AudioClip
    .WithVolume(1.0f)         // Volume control
    .WithPitch(1.0f)          // Pitch control
    .WithLoop(false)          // Enable or disable looping
    .WithFadeInDuration(0.5f) // Optional fade-in effect
    .Build();

// Play the event using AudioManager
AudioManager.Instance.PlaySound(audioEvent);
```

### Using the Audio Pooler

For efficient audio management, leverage the `AudioPooler` to reuse sound instances:

```csharp
AudioPooler pooler = AudioManager.Instance.AudioPooler;
SoundInstance soundInstance = pooler.GetPooledAudio("SoundEffect");
soundInstance.Play();
```

This ensures that audio sources are reused, reducing memory usage and improving performance.

### Adjusting Global Audio Settings

Customize global audio settings, such as master volume, via the `AudioManagerSettings` class:

```csharp
AudioManager.Instance.Settings.MasterVolume = 0.75f;
AudioManager.Instance.Settings.SFXVolume = 0.85f;
AudioManager.Instance.Settings.MusicVolume = 0.65f;
```

These settings will apply globally, providing control over various audio channels.

### Enabling Logging

Enable detailed logging to track audio events and debug issues within the system:

```csharp
Logger.EnableLogging(true);
Logger.Log("Audio event triggered successfully!");
```

This outputs logs related to audio playback, warnings, and errors.

## Testing

This project includes test scripts and benchmarking tools located in the `Tests` folder. These tests can be run in Unity’s test runner to validate the system's performance and functionality under different conditions.

## Contribution

Contributions are encouraged! If you find issues or have ideas for improvements, feel free to submit a pull request or open an issue.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.

## Support

Join our community on Discord for support, feedback, and discussions:  
[![Join our Discord](https://img.shields.io/badge/Discord-Join%20Us-7289DA?logo=discord&logoColor=white)](https://discord.gg/knwtcq3N2a)

