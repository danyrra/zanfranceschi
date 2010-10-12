using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AMO.Core.Domain;
using AMO.Core;

namespace AMO.GUI.Desktop
{
	public partial class ID3Edit : Form
	{
		IList<Song> songs;
		AMOModel model;

		string title = string.Empty;
		string artist = string.Empty;
		string album = string.Empty;
		string genre = string.Empty;
		int track;
		string year = string.Empty;
		
		public ID3Edit()
		{
			InitializeComponent();
		}

		public ID3Edit(IList<Song> source, AMOModel model)
		{
			InitializeComponent();
			
			songs = source;
			this.model = model;

			var titles = from a in songs group a.Title by a.Title into g select new { Value = g };
			var artists = from a in songs group a.Artist by a.Artist into g select new { Value = g };
			var albums = from a in songs group a.Album by a.Album into g select new { Value = g };
			var genres = from a in songs group a.Genre by a.Genre into g select new { Value = g };
			var tracks = from a in songs group a.TrackNumber by a.TrackNumber into g select new { Value = g };
			var years = from a in songs group a.Year by a.Year into g select new { Value = g };

			if (titles.Count() == 1)
				title = titles.First().Value.Key;

			if (artists.Count() == 1)
				artist = artists.First().Value.Key;

			if (albums.Count() == 1)
				album = albums.First().Value.Key;

			if (genres.Count() == 1)
				genre = genres.First().Value.Key;

			if (tracks.Count() == 1)
				track = tracks.First().Value.Key;

			if (years.Count() == 1)
				year = years.First().Value.Key;

			FillForm();
		}

		private void FillForm()
		{
			txtTitle.Text = title;
			txtArtist.Text = artist;
			txtAlbum.Text = album;
			txtTrack.Text = track.ToString();
			txtYear.Text = year;
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (songs != null)
			{
				foreach (Song song in songs)
				{
					song.Year = txtYear.Text;
					model.SaveSongID3(song);
				}
				this.Close();
			}
		}
	}
}
