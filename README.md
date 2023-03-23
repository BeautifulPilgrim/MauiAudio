# MauiAudio-Cross platform audio plugin for MAUI

[![.NET](https://github.com/BeautifulPilgrim/MauiAudio/actions/workflows/dotnet.yml/badge.svg)](https://github.com/BeautifulPilgrim/MauiAudio/actions/workflows/dotnet.yml) [![Nuget](https://img.shields.io/nuget/v/Plugin.MauiAudio)](https://www.nuget.org/packages/Plugin.MauiAudio/) ![Nuget](https://img.shields.io/nuget/dt/Plugin.MauiAudio)

Based from [.NET Podcasts - Sample Application](https://github.com/microsoft/dotnet-podcasts#net-podcasts---sample-application)

## Intro

An Audio Plugin in MAUI with native control.

A code novice, please forgive me if the documentation or code is not standardized. If you can help improve it, I would be very grateful!

## Init

In CreateMauiApp()[MauiProgram.cs]

```c#
using MauiAudio;

builder.UseMauiAudio()
```

### Platform/Android

AndroidManifest.xml

```
<uses-permission android:name="android.permission.WAKE_LOCK" />
```

MainActivity.cs

```c#
using Android.Content;
using MauiAudio.Platforms.Android;

public class MainActivity : MauiAudioActivity
{
}
```

## Usage
use MediaPlay:

```c#
private INativeAudioService audioService;
public void Init()
{
    audioService = this.GetMauiAudioService();
}

public void PlayMedia(string url)
{


    audioService.LaunchMedia(new MediaContent(url));
}

public void PlayPlaylist(string url)
{
    var audioService = this.GetMauiAudioService();

    var playlist = new List<MediaContent>
                {
                    new MediaContent(url),
                    new MediaContent(url)
                };

    audioService.LaunchPlaylist(playlist, playlist.First());
}
```

## Interface

```c#
public interface INativeAudioService : INotifyPropertyChanged, IDisposable
{
    public event EventHandler PlayingStarted;
    public event EventHandler PlayingPaused;
    public event EventHandler PlayingEnded;
    public event EventHandler<MediaContent> PreviousMediaAccepted;
    public event EventHandler<MediaContent> NextMediaAccepted;
    public event EventHandler<TimeSpan> DurationAccepted;
    public event EventHandler<double> BuffCoeffAccepted;

    public ObservableCollection<MediaContent> Playlist { get; }

    public MediaContent MediaPrevious { get; }
    public MediaContent MediaCurrent { get; }
    public MediaContent MediaNext { get; }

    public double Volume { get; set; }
    public double Balance { get; set; }
    public bool Muted { get; set; }

    public abstract bool IsPlaying { get; }
    public abstract double BufferedCoeff { get; }
    public abstract TimeSpan Duration { get; }
    public abstract bool TryGetPosition(out TimeSpan position);

    public bool LaunchPlaylist<T>(List<T> playlist, MediaContent currentMedia, TimeSpan? position = null) where T : MediaContent;
    public bool LaunchPlaylist(List<MediaContent> playlist, MediaContent currentMedia, TimeSpan? position = null);
    public bool LaunchMedia(MediaContent media, TimeSpan? position = null);

    public abstract void Play();
    public abstract void Pause();
    public abstract void Stop();
    public abstract void SeekTo(TimeSpan position);
    public abstract void Next();
    public abstract void Previous();

    public bool Shuffled { get; set; }
    public abstract bool LoopMedia { get; set; }
}
```
## Notify

If you want to process the player's previous or next song:(only Android and Windows available now), preprocess the four EventHandlers.

```c#
    public event EventHandler PlayingStarted;
    public event EventHandler PlayingPaused;
    public event EventHandler PlayingEnded;
    public event EventHandler<MediaContent> PreviousMediaAccepted;
    public event EventHandler<MediaContent> NextMediaAccepted;
    public event EventHandler<TimeSpan> DurationAccepted;
    public event EventHandler<double> BuffCoeffAccepted;
```

## Sample

### Windows

![Snipaste_2022-10-11_21-35-57](https://github.com/BeautifulPilgrim/MauiAudio/raw/master/README.assets/Snipaste_2022-10-11_21-35-57.png)

### Android

![sample_android](https://github.com/BeautifulPilgrim/MauiAudio/raw/master/README.assets/sample_android.jpg)
