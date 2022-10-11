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
    string url= "https://www.soundhelix.com/examples/mp3/SoundHelix-Song-2.mp3";
    public MainPageViewModel(PlayerService player)
    {
        playerService = player;
    }
    [RelayCommand]
    void Play()
    {
        playerService.PlayAsync(new() { URL = Url, Name = "TempUrl",Author="TempAuthor" });
    }
}
