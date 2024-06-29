﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAudio
{
    public class MediaPlay
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string URL { get; set; }
        public Stream Stream { get; set; }
        public string Image { get; set; }
        /// <summary>
        /// Get/Set Album Cover in Byte[] to put on Notification 
        /// </summary>
        public byte[] ImageBytes { get; set; }
        /// <summary>
        /// Get/Set the VALUE of the Duration, solely used for the Notification bar to show Progress.
        /// </summary>
        public long DurationInMs { get; set; } = 0;
    }
}
