using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace AMO.Core.Domain
{
	public class Song
	{
		public FileInfo FileInfo { get; set; }
		public string Title { get; set; }
		public string Album { get; set; }
		public string Artist { get; set; }
		public string Genre { get; set; }
		public string Year { get; set; }
		public int TrackNumber { get; set; }

		public Song() { }
		
		public Song(
			FileInfo fileInfo,
			string title,
			string album,
			string artist,
			string genre,
			string year,
			int trackNumber)
		{
			FileInfo = fileInfo;
			Title = title;
			Album = album;
			Artist = artist;
			Genre = genre;
			Year = year;
			TrackNumber = trackNumber;
		}
	}
}