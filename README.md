# MauiAudio-Cross platform audio plugin for MAUI

[![.NET](https://github.com/BeautifulPilgrim/MauiAudio/actions/workflows/dotnet.yml/badge.svg)](https://github.com/BeautifulPilgrim/MauiAudio/actions/workflows/dotnet.yml) [![Nuget](https://img.shields.io/nuget/v/Plugin.MauiAudio)](https://www.nuget.org/packages/Plugin.MauiAudio/) ![Nuget](https://img.shields.io/nuget/dt/Plugin.MauiAudio)

Based from [.NET Podcasts - Sample Application](https://github.com/microsoft/dotnet-podcasts#net-podcasts---sample-application)

A code novice, please forgive me if the documentation or code is not standardized. If you can help improve it, I would be very grateful!

## Installation

Add the [NuGet package](https://www.nuget.org/packages/Plugin.MauiAudio/) to the projects you want to use it in.

- Select the Browse tab, search for Plugin.MauiAudio
- Select Plugin.MauiAudio

## Init
In CreateMauiApp()[MauiProgram.cs]

#### Version ≥ 1.0.3：

```c#
using MauiAudio;

builder.UseMauiAudio()
```
#### Version < 1.0.3:

```c#
#if WINDOWS
        builder.Services.TryAddSingleton<MauiAudio.INativeAudioService, MauiAudio.Platforms.Windows.NativeAudioService>();
#elif ANDROID
        builder.Services.TryAddSingleton<MauiAudio.INativeAudioService, MauiAudio.Platforms.Android.NativeAudioService>();
#elif MACCATALYST
        builder.Services.TryAddSingleton<MauiAudio.INativeAudioService, MauiAudio.Platforms.MacCatalyst.NativeAudioService>();
        builder.Services.TryAddSingleton< Platforms.MacCatalyst.ConnectivityService>();
#elif IOS
        builder.Services.TryAddSingleton<MauiAudio.INativeAudioService, MauiAudio.Platforms.iOS.NativeAudioService>();
#endif
```

### Platform/Android

AndroidManifest.xml

```
<uses-permission android:name="android.permission.WAKE_LOCK" />
```

MainApplication.cs

```c#
using Android.Content;
using MauiAudio.Platforms.Android;
using MauiAudio.Platforms.Android.CurrentActivity;

public class MainActivity : MauiAppCompatActivity,IAudioActivity
{
    MediaPlayerServiceConnection mediaPlayerServiceConnection;

    public MediaPlayerServiceBinder Binder { get; set; }

    public event StatusChangedEventHandler StatusChanged;
    public event CoverReloadedEventHandler CoverReloaded;
    public event PlayingEventHandler Playing;
    public event BufferingEventHandler Buffering;

    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        CrossCurrentActivity.Current.Init(this, savedInstanceState);
        NotificationHelper.CreateNotificationChannel(ApplicationContext);
        if (mediaPlayerServiceConnection == null)
            InitializeMedia();
    }

    private void InitializeMedia()
    {
        mediaPlayerServiceConnection = new MediaPlayerServiceConnection(this);
        var mediaPlayerServiceIntent = new Intent(ApplicationContext, typeof(MediaPlayerService));
        BindService(mediaPlayerServiceIntent, mediaPlayerServiceConnection, Bind.AutoCreate);
    }
}
```
## Usage

```c#
private readonly INativeAudioService audioService;
public async Task PlayAsync(string url)
    {
        await audioService.InitializeAsync(url);
        await audioService.PlayAsync(position);
    }
```

or use MediaPlay:

```c#
private readonly INativeAudioService audioService;
public class MediaPlay
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string URL { get; set; } # URL is must
        public string Image { get; set; }
    }

MediaPlay media=new(){...};
await audioService.InitializeAsync(media);
# play 
# position is double (second)
await InternalPlayAsync(position);
# pause
await audioService.PauseAsync();
```

## Interface

### Version ≥ 1.0.3

```c#
public interface INativeAudioService
{
    Task InitializeAsync(string audioURI);
    Task InitializeAsync(MediaPlay media);
    Task PlayAsync(double position = 0);

    Task PauseAsync();
    ///<Summary>
    /// Set the current playback position (in seconds).
    ///</Summary>
    Task SetCurrentTime(double value);

    Task DisposeAsync();
    ///<Summary>
    /// Gets a value indicating whether the currently loaded audio file is playing.
    ///</Summary>
    bool IsPlaying { get; }
    ///<Summary>
    /// Gets the current position of audio playback in seconds.
    ///</Summary>
    double CurrentPosition { get; }
    ///<Summary>
    /// Gets the length of audio in seconds.
    ///</Summary>
    double Duration { get; }
    ///<Summary>
    /// Gets or sets the playback volume 0 to 1 where 0 is no-sound and 1 is full volume.
    ///</Summary>
    double Volume { get; set; }
    /// <summary>
    /// Gets or sets the playback volume muted. false means not mute; true means mute.
    /// </summary>
    bool Muted { get; set; }

    ///<Summary>
    /// Gets or sets the balance left/right: -1 is 100% left : 0% right, 1 is 100% right : 0% left, 0 is equal volume left/right.
    ///</Summary>
    double Balance { get; set; }

    event EventHandler<bool> IsPlayingChanged;
    event EventHandler PlayEnded;
    event EventHandler PlayNext;
    event EventHandler PlayPrevious;
}
```



### version<1.0.6

```c#
public interface INativeAudioService
{
    Task InitializeAsync(string audioURI);
    Task InitializeAsync(MediaPlay media);
    Task PlayAsync(double position = 0);

    Task PauseAsync();

    Task SetMuted(bool value);

    Task SetVolume(int value);

    Task SetCurrentTime(double value);

    ValueTask DisposeAsync();

    bool IsPlaying { get; }

    double CurrentPosition { get; }
    double Duration { get; }

    event EventHandler<bool> IsPlayingChanged;
    event EventHandler PlayEnded;
    event EventHandler PlayNext;
    event EventHandler PlayPrevious;
}
```

## Notify

If you want to process the player's previous or next song:(only Android and Windows available now), preprocess the four EventHandlers.

```c#
    event EventHandler<bool> IsPlayingChanged;
    event EventHandler PlayEnded;
    event EventHandler PlayNext;
    event EventHandler PlayPrevious;
```

## Sample

### Windows

![Snipaste_2022-10-11_21-35-57](https://github.com/BeautifulPilgrim/MauiAudio/raw/master/README.assets/Snipaste_2022-10-11_21-35-57.png)

### Android

![sample_android](https://github.com/BeautifulPilgrim/MauiAudio/raw/master/README.assets/sample_android.jpg)
