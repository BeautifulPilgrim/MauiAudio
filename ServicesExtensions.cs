﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAudio;

public static class ServicesExtensions
{
    public static MauiAppBuilder UseMauiAudio(this MauiAppBuilder builder)
    {
#if WINDOWS
        builder.Services.AddSingleton<MauiAudio.INativeAudioService, MauiAudio.Platforms.Windows.NativeAudioService>();
#elif ANDROID
        builder.Services.AddSingleton<MauiAudio.INativeAudioService, MauiAudio.Platforms.Android.NativeAudioService>();
#elif MACCATALYST
        builder.Services.AddSingleton<MauiAudio.INativeAudioService, MauiAudio.Platforms.MacCatalyst.NativeAudioService>();
#elif IOS
        builder.Services.AddSingleton<MauiAudio.INativeAudioService, MauiAudio.Platforms.iOS.NativeAudioService>();
#endif
        return builder;
    }
}
