using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AMO.Core;
using AMO.Core.Domain;

namespace AMO.GUI.Desktop
{
	public partial class AMO : Form
	{
		private AMOModel model;
		private ID3Edit editForm;
		IList<Song> songs;
		IList<Song> editingSongs;


		string title = string.Empty;
		string artist = string.Empty;
		string album = string.Empty;
		string genre = string.Empty;
		int track = 0;
		string year = string.Empty;


		public AMO()
		{
			InitializeComponent();

			string dir = @"C:\Users\zanfranceschi\Desktop\misc";
			lblDirectory.Text = dir;

			grid.AutoGenerateColumns = false;

			model = new AMOModel(dir, "*.mp3");

			songs = model.GetMediaFiles();



			grid.DataSource = songs;

		}

		private void editToolStripMenuItem_Click(object sender, EventArgs e)
		{
			editingSongs = new List<Song>();

			DataGridViewSelectedCellCollection cells = grid.SelectedCells;

			foreach (DataGridViewCell cell in cells)
			{
				editingSongs.Add((Song)cell.OwningRow.DataBoundItem);
			}

			if (editingSongs.Count > 0)
			{
				LoadEditionForm();
			}
		}

		private void grid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			Song song = (Song)grid.CurrentRow.DataBoundItem;
			SaveSongID3(song);
		}

		private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
		{

			DataGridViewSelectedCellCollection cells = grid.SelectedCells;

			IList<DataGridViewRow> rows = new List<DataGridViewRow>();

			if (Clipboard.ContainsText(TextDataFormat.Text))
			{
				object text = Clipboard.GetText();

				foreach (DataGridViewCell cell in cells)
				{
					cell.Value = text;

					if (!rows.Contains(cell.OwningRow))
						rows.Add(cell.OwningRow);
				}
			}
			else
			{
				foreach (DataGridViewCell cell in cells)
				{
					cell.Value = string.Empty;

					if (!rows.Contains(cell.OwningRow))
						rows.Add(cell.OwningRow);
				}
			}

			foreach (DataGridViewRow row in rows)
			{
				SaveSongID3((Song)row.DataBoundItem);
			}
		}

		private void copyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DataGridViewSelectedCellCollection cells = grid.SelectedCells;

			if (cells.Count > 0)
			{
				foreach (DataGridViewCell cell in cells)
				{
					if (cell.Value != null)
					{
						Clipboard.SetText(cell.Value.ToString());
						continue;
					}
				}
			}
		}

		private void SaveSongID3(Song song)
		{
			model.SaveSongID3(song);
		}

		private void btnPickDir_Click(object sender, EventArgs e)
		{
			fbdMediaDirectory.ShowNewFolderButton = false;
			fbdMediaDirectory.ShowDialog(this);
			string path = fbdMediaDirectory.SelectedPath;
			lblDirectory.Text = path;
			LoadGrid(path);
		}

		private void LoadGrid(string dir)
		{
			model.DirectoryPath = dir;
			grid.DataSource = model.GetMediaFiles();
		}

		private void ClearEditionForm()
		{
			txtYear.Text = txtTrack.Text = ddlAlbum.Text = ddlArtist.Text = txtTitle.Text = string.Empty;
			ckbGenre.Checked = ckbArtist.Checked = ckbAlbum.Checked = ckbYear.Checked = false;
		}

		private void LoadEditionForm()
		{
			ClearEditionForm();

			var titles = from a in editingSongs group a.Title by a.Title into g select new { Value = g };
			var artists = from a in editingSongs group a.Artist by a.Artist into g select new { Value = g };
			var albums = from a in editingSongs group a.Album by a.Album into g select new { Value = g };
			var genres = from a in editingSongs group a.Genre by a.Genre into g select new { Value = g };
			var tracks = from a in editingSongs group a.TrackNumber by a.TrackNumber into g select new { Value = g };
			var years = from a in editingSongs group a.Year by a.Year into g select new { Value = g };


			var full_titles = from a in songs group a.Title by a.Title into g select new { Value = g };
			var full_artists = from a in songs group a.Artist by a.Artist into g select new { Value = g };
			var full_albums = from a in songs group a.Album by a.Album into g select new { Value = g };
			var full_genres = from a in songs group a.Genre by a.Genre into g select new { Value = g };
			var full_tracks = from a in songs group a.TrackNumber by a.TrackNumber into g select new { Value = g };
			var full_years = from a in songs group a.Year by a.Year into g select new { Value = g };

			if (titles.Count() == 1)
				txtTitle.Text = titles.First().Value.Key;

			if (artists.Count() == 1)
				ddlArtist.Text = artists.First().Value.Key;

			if (albums.Count() == 1)
				ddlAlbum.Text = albums.First().Value.Key;

			if (genres.Count() == 1)
				ddlGenre.Text = genres.First().Value.Key;

			if (tracks.Count() == 1)
				txtTrack.Text = tracks.First().Value.Key.ToString();

			if (years.Count() == 1)
				txtYear.Text = years.First().Value.Key;

			ddlGenre.DisplayMember = "Genre";

			ddlGenre.DataSource = songs.Distinct().ToList();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (editingSongs != null)
			{
				foreach (Song song in editingSongs)
				{
					if (ckbGenre.Checked)
						song.Genre = ddlGenre.Text;

					if (ckbArtist.Checked)
						song.Artist = ddlArtist.Text;

					if (ckbAlbum.Checked)
						song.Album = ddlAlbum.Text;

					if (ckbYear.Checked)
						song.Year = txtYear.Text;

					model.SaveSongID3(song);
				}
			}
			ClearEditionForm();
		}
	}
}