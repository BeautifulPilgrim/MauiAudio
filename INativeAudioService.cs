namespace MauiAudio;

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