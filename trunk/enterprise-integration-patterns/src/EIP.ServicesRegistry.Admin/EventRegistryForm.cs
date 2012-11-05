using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using EIP.CanonicalModel.Events;
using EIP.ServicesRegistry.Core.Entities;
using EIP.ServicesRegistry.Core;

namespace EIP.ServicesRegistry.Admin
{
	public partial class EventRegistryForm : Form
	{
		protected IList<string> services;

		private ServiceSrv service;

		private EventRegistry currentEvent;

		public EventRegistryForm()
		{

			service = SrvFactory.GetServiceSrv();

			InitializeComponent();

			btnDel.Enabled = false;

			var types = from type in Assembly.GetAssembly(typeof(BaseEvent)).GetTypes()
						where !type.IsAbstract
						select type;

			types.ToList().ForEach(t => ddlTypes.Items.Add(t.FullName));
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			EventRegistry registry = EventFromForm();

			if (currentEvent == null)
			{
				service.CreateEventRegistry(registry);
			}
			else
			{
				registry.Id = currentEvent.Id;
				service.UpdateEventRegistry(registry);
			}
			ClearForm();
			button1_Click(null, null);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			gridEvents.DataSource = service.SearchEvents(txtTerm.Text);
		}

		private void EventToForm()
		{
			if (currentEvent != null)
			{
				txtAddress.Text = currentEvent.Address;
				ddlTypes.Text = currentEvent.CanonicalDataType;
				txtVersion.Text = currentEvent.CanonicalDataTypeVersion;
				txtDescription.Text = currentEvent.Description;
				txtName.Text = currentEvent.Name;
				ddlProtocol.Text = currentEvent.Protocol;
				txtTechincalDetails.Text = currentEvent.TechnicalDetails;
				btnDel.Enabled = true;
			}
		}

		private EventRegistry EventFromForm()
		{
			EventRegistry registry = new EventRegistry
			{
				Address = txtAddress.Text,
				CanonicalDataType = ddlTypes.Text,
				CanonicalDataTypeVersion = txtVersion.Text,
				Description = txtDescription.Text,
				Name = txtName.Text,
				Protocol = ddlProtocol.Text,
				TechnicalDetails = txtTechincalDetails.Text,
			};
			return registry;
		}


		private void ClearForm()
		{
			txtTechincalDetails.Text = 
				ddlProtocol.Text = 
				txtName.Text = 
				txtDescription.Text = 
				txtVersion.Text = 
				ddlTypes.Text = 
				txtAddress.Text = string.Empty;
			btnDel.Enabled = false;
		}

		private void btnNew_Click(object sender, EventArgs e)
		{
			ClearForm();
			currentEvent = null;
		}

		private void gridEvents_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex > -1)
			{
				currentEvent = (EventRegistry)gridEvents.Rows[e.RowIndex].DataBoundItem;
				EventToForm();
			}
		}

		private void btnDel_Click(object sender, EventArgs e)
		{
			service.RemoveEventRegistry(currentEvent.Id);
			ClearForm();
		}
	}
}
