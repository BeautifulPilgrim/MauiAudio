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
        public byte[] ImageBytes { get; set; }
    }
}
