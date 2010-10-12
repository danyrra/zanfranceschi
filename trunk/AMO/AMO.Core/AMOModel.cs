using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using AMO.Core.Domain;
using IdSharp.Tagging.ID3v2;
using AMO.Core.Utils;
using IdSharp.Tagging.ID3v1;
using System.ComponentModel;

namespace AMO.Core
{
	[DataObject]
	public class AMOModel
	{
		private string[] fileExtensions;
		
		public AMOModel(string workDirectory, params string[] searchPatterns)
		{
			DirectoryPath = workDirectory;
			fileExtensions = searchPatterns;
		}

		public string DirectoryPath { get; set; }
		
		public void SaveSongID3(Song song)
		{
			string file = song.FileInfo.FullName;

			if (song.FileInfo.IsReadOnly)
				return;

			IID3v2 id3 = ID3v2Helper.CreateID3v2(file);

			id3.Album = song.Album;
			id3.Artist = song.Artist;
			id3.Genre = song.Genre;
			id3.Title = song.Title;
			id3.TrackNumber = song.TrackNumber.ToString();
			id3.Year = song.Year;
			id3.Save(file);
		}

		[DataObjectMethod(DataObjectMethodType.Select)]
		public IList<Song> GetMediaFiles()
		{
			IList<Song> songs = new List<Song>();

			try
			{
				IList<string> files = new List<string>();

				foreach (string extension in fileExtensions)
				{
					string[] list = Directory.GetFiles(DirectoryPath, extension, SearchOption.AllDirectories);
					foreach (string file in list)
						files.Add(file);
				}
				
				Song song = null;

				string album = string.Empty;
				string artist = string.Empty;
				string genre = string.Empty;
				string title = string.Empty;
				int trackNumber = -1;
				string year = string.Empty;
				FileInfo fileInfo = null;

				foreach (string file in files)
				{
					bool hasID3 = false;

					fileInfo = new FileInfo(file);

					if (ID3v2Helper.DoesTagExist(file))
					{
						hasID3 = true;

						IID3v2 id3 = ID3v2Helper.CreateID3v2(file);

						album = id3.Album;
						artist = id3.Artist;
						genre = id3.Genre;
						title = id3.Title;
						trackNumber = ConverterHelper.ToInt(id3.TrackNumber);
						year = id3.Year;
					}
					else if (ID3v1Helper.DoesTagExist(file))
					{
						hasID3 = true;

						IID3v1 id3 = ID3v1Helper.CreateID3v1(file);

						album = id3.Album;
						artist = id3.Artist;
						genre = id3.GenreIndex.ToString();
						title = id3.Title;
						trackNumber = id3.TrackNumber;
						year = id3.Year;
					}

					if (hasID3)
					{
						song = new Song(fileInfo, title, album, artist, genre, year, trackNumber);

						if (song != null && string.IsNullOrEmpty(song.Title))
							song.Title = song.FileInfo.Name.Substring(0, song.FileInfo.Name.LastIndexOf('.'));

						if (song != null)
							songs.Add(song);
					}
					else
					{
						songs.Add(new Song() { FileInfo = fileInfo });
					}
				}
			}
			catch (Exception)
			{
				throw;
			}
			return songs;
		}
	}
}