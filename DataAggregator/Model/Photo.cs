﻿namespace DataAggregator.Model
{
    public class Photo
    {
        public int Id { get; set; }

        public int AlbumId { get; set; }

        public string Url { get; set; }

        public string ThumbnailUrl { get; set; }
    }
}
