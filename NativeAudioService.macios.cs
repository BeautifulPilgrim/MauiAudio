using AudioUnit;
using AVFoundation;
using Foundation;

namespace MauiAudio;

public class NativeAudioService : INativeAudioService
{
    //AVPlayer avPlayer;
    AVAudioPlayer avPlayer;
    string _uri;

    public bool IsPlaying => avPlayer != null
        ? avPlayer.Playing
        : false;

    public double CurrentPosition => avPlayer?.CurrentTime ?? 0;
    public double Duration=>avPlayer?.Duration ?? 0;
    public event EventHandler<bool> IsPlayingChanged;
    public event EventHandler PlayEnded;
    public event EventHandler PlayNext;
    public event EventHandler PlayPrevious;

    public async Task InitializeAsync(string audioURI)
    {
        await InitializeAsync(new MediaPlay() { URL = audioURI });
    }

    public Task PauseAsync()
    {
        avPlayer?.Pause();

        return Task.CompletedTask;
    }

    public Task PlayAsync(double position = 0)
    {
        avPlayer.PlayAtTime(position);
        avPlayer?.Play();
        return Task.CompletedTask;
    }

    public Task SetCurrentTime(double value)
    {
        avPlayer.PlayAtTime(value);
        return Task.CompletedTask;
    }

    public Task SetMuted(bool value)
    {
        if (avPlayer != null)
        {
            avPlayer.Volume = value ? 100 : 0;
        }

        return Task.CompletedTask;
    }

    public Task SetVolume(int value)
    {
        if (avPlayer != null)
        {
            avPlayer.Volume = value;
        }

        return Task.CompletedTask;
    }

    public ValueTask DisposeAsync()
    {
        avPlayer?.Dispose();
        return ValueTask.CompletedTask;
    }

    public async Task InitializeAsync(MediaPlay media)
{
        _uri = media.URL;
        NSUrl fileURL = new NSUrl(_uri.ToString());

        if (avPlayer != null)
        {
            await PauseAsync();
        }

        avPlayer = AVAudioPlayer.FromUrl(fileURL);
        avPlayer.FinishedPlaying += OnPlayerFinishedPlaying;
    }
    void OnPlayerFinishedPlaying(object? sender, AVStatusEventArgs e)
    {
        PlayEnded?.Invoke(this, e);
    }
}