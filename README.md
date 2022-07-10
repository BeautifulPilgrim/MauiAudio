# MauiAudio-Cross platform audio plugin for MAUI

[![.NET](https://github.com/BeautifulPilgrim/MauiAudio/actions/workflows/dotnet.yml/badge.svg)](https://github.com/BeautifulPilgrim/MauiAudio/actions/workflows/dotnet.yml)

Based from [.NET Podcasts - Sample Application](https://github.com/microsoft/dotnet-podcasts#net-podcasts---sample-application)


# Usage

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

