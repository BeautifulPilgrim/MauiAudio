using MauiAudio;
namespace MauiAudio.Sample.Services;

public partial class PlayerService:ObservableObject
{
    private readonly INativeAudioService audioService;

    [ObservableProperty]
    public MediaPlay currentEpisode;
    [ObservableProperty]
    public List<MediaPlay> playList;

    public bool IsPlaying { get; set; }
    public double CurrentPosition => audioService.CurrentPosition;
    public double Duration=>audioService.Duration;

    public event EventHandler NewEpisodeAdded;
    public event EventHandler IsPlayingChanged;

    public PlayerService(INativeAudioService audioService)
    {
        this.audioService = audioService;
        PlayList = new List<MediaPlay>();
        audioService.PlayNext += AudioService_PlayNext;
        audioService.PlayPrevious += AudioService_PlayPrevious;
    }

    private async void AudioService_PlayPrevious(object sender, EventArgs e)
    {
        if (CurrentEpisode == null) return;
        var index = PlayList.IndexOf(CurrentEpisode);
        await PlayAsync(PlayList[(index - 1 + PlayList.Count) % PlayList.Count], true);
    }

    private async void AudioService_PlayNext(object sender, EventArgs e)
    {
        if (CurrentEpisode == null) return;
        var index = PlayList.IndexOf(CurrentEpisode);
        await PlayAsync(PlayList[(index + 1) % PlayList.Count], true);
    }

    async Task PlayAsync(MediaPlay episode, bool isPlaying, double position = 0)
    {
        if (episode == null) { return; }

        var isOtherEpisode = CurrentEpisode != episode;


        if (isOtherEpisode)
        {
            CurrentEpisode = episode;

            if (audioService.IsPlaying)
            {
                await InternalPauseAsync();
            }

            await audioService.InitializeAsync(CurrentEpisode);

            await InternalPlayPauseAsync(isPlaying, position);

            NewEpisodeAdded?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            await InternalPlayPauseAsync(isPlaying, position);
        }

        IsPlayingChanged?.Invoke(this, EventArgs.Empty);
    }
    public async Task ChangePosition(double position = 0)
    {
        await InternalPlayPauseAsync(IsPlaying, position);
    }
    public async Task PlayAsyncTemp(string url)
    {
        await audioService.InitializeAsync(url);
        await InternalPlayAsync(0);
    }
    public Task PlayAsync(MediaPlay episode, List<MediaPlay> items = null)
    {
        if (items == null) { if (!PlayList.Contains(episode)) PlayList.Add(episode); }
        else
        {
            PlayList.Clear();
            PlayList.AddRange(items);
        }
        var isOtherEpisode = CurrentEpisode != episode;
        var isPlaying = isOtherEpisode || !audioService.IsPlaying;
        var position = isOtherEpisode ? 0 : CurrentPosition;

        return PlayAsync(episode, isPlaying, position);
    }
    public async Task resumeEpisode(double position)
    {
        await audioService.InitializeAsync(CurrentEpisode);

        await InternalPlayPauseAsync(true, position);

        IsPlayingChanged?.Invoke(this, EventArgs.Empty);
    }
    private async Task InternalPlayPauseAsync(bool isPlaying, double position)
    {
        if (isPlaying)
        {
            await InternalPlayAsync(position);
        }
        else
        {
            await InternalPauseAsync();
        }
    }
    private async Task InternalPauseAsync()
    {
        await audioService.PauseAsync();
        IsPlaying = false;
    }

    private async Task InternalPlayAsync(double position = 0)
    {
        //var canPlay = Connectivity.Current.NetworkAccess==NetworkAccess.Internet?true:false;

        //if (!canPlay)
        //{
        //    return;
        //}

        await audioService.PlayAsync(position);
        IsPlaying = true;
    }
    public async Task dispose()
    {
        await audioService.DisposeAsync();
    }
}
