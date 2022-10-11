using CommunityToolkit.Maui;
using MauiAudio.Sample.Services;

namespace MauiAudio.Sample;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiAudio()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
        builder.Services.AddSingleton<PlayerService>();

        builder.Services.AddSingletonWithShellRoute<MainPage, MainPageViewModel>(nameof(MainPage));
		return builder.Build();
	}
}
