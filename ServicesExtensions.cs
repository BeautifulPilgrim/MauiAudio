using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAudio;

public static class AppBuilderExtensions
{
    public static MauiAppBuilder UseMauiAudio(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<MauiAudio.INativeAudioService, MauiAudio.NativeAudioService>();
        return builder;
    }
}
