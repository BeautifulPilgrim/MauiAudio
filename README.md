# MauiAudio-Cross platform audio plugin for MAUI

[![.NET](https://github.com/BeautifulPilgrim/MauiAudio/actions/workflows/dotnet.yml/badge.svg)](https://github.com/BeautifulPilgrim/MauiAudio/actions/workflows/dotnet.yml) ![Nuget](https://img.shields.io/nuget/v/Plugin.MauiAudio) ![Nuget](https://img.shields.io/nuget/dt/Plugin.MauiAudio)

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

### Android

```c#
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
}
```

## Notify

If you want to process the player's previous or next song:(only Android available now)

```c#
UnFinished
```

