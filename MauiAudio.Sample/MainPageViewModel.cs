using CommunityToolkit.Mvvm.Input;
using MauiAudio.Sample.Services;
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
    [ObservableProperty]
    string url= "https://dkihjuum4jcjr.cloudfront.net/ES_ITUNES/Thunderbird/ES_Thunderbird.mp3";
    public MainPageViewModel(PlayerService player)
    {
        playerService = player;
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
        await playerService.PlayAsync(new() { Stream=stream, Name = "TempUrl", Author = "TempAuthor" });
    }
}
