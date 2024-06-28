using CommunityToolkit.Mvvm.Input;
using MauiAudio.Sample.Services;
using Microsoft.Maui.Dispatching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAudio.Sample;
[ObservableObject]
public partial class MainPageViewModel
{
    PlayerService playerService;
    public IDispatcherTimer timer;
    [ObservableProperty]
    string currentTime;
    [ObservableProperty]
    string url= "https://dkihjuum4jcjr.cloudfront.net/ES_ITUNES/Thunderbird/ES_Thunderbird.mp3";
    public MainPageViewModel(PlayerService player)
    {
        playerService = player;
        timer = Application.Current.Dispatcher.CreateTimer();
        timer.Interval = TimeSpan.FromSeconds(0.2);
        timer.Tick += OnTimeChanging;
    }
    [RelayCommand]
    void Play()
    {
        playerService.PlayAsync(new() { URL = Url, Name = "TempUrl",Author="TempAuthor" });
    }
    [RelayCommand]
    async void PlayInStream()
    {
        var stream = await FileSystem.OpenAppPackageFileAsync("sample.mp3");
        var img = await GetCoverImage();
        await playerService.PlayAsync(new() { Stream=stream, Name = "TempUrl", Author = "TempAuthor", ImageBytes = img });
    }

    private void OnTimeChanging(object sender, EventArgs e)
    {
        var current = TimeSpan.FromSeconds(playerService.CurrentPosition).ToString("hh\\:mm\\:ss");
        var duration = TimeSpan.FromSeconds(playerService.Duration).ToString("hh\\:mm\\:ss");
        CurrentTime = $"{current}/{duration}";
    }
    [RelayCommand]
    async void ChangeTime()
    {
        var result=await App.Current.MainPage.DisplayPromptAsync("change time", $"all time: {playerService.Duration}", placeholder: playerService.CurrentPosition.ToString());
        if (result != null)
        {
            await playerService.ChangePosition(double.Parse(result));
        }
    }
    [RelayCommand]
    async void StopPlay()
    {
        await playerService.dispose();
    }
    async Task<byte[]>? GetCoverImage(string filePath = null)
    {
        using var imageStream = await FileSystem.OpenAppPackageFileAsync("shadow.png");
        
        using var memStream = new MemoryStream();
        imageStream.CopyTo(memStream);

        return memStream.ToArray();
    }
}
