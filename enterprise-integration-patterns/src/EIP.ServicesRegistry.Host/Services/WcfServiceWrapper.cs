using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceProcess;
using System.ServiceModel.Description;
using System.ServiceModel.Configuration;
using System.Configuration;
using System.ServiceModel.Channels;

namespace EIP.ServicesRegistry.Host.Services
{
	public class WcfServiceWrapper<TServiceImplementation, TServiceContract>
		: ServiceBase where TServiceImplementation : TServiceContract
	{
		private readonly IList<Uri> _servicesUri;
		private ServiceHost _serviceHost;

		public WcfServiceWrapper(string serviceName, params string[] servicesUri)
		{
			_servicesUri = new List<Uri>();
			servicesUri.ToList().ForEach(uri => _servicesUri.Add(new Uri(uri)));
			ServiceName = serviceName;
		}

		protected override void OnStart(string[] args)
		{
			Start();
		}

		protected override void OnStop()
		{
			Stop();
		}

		public void Start()
		{
			Console.WriteLine(ServiceName + " starting...");
			bool openSucceeded = false;
			try
			{
				if (_serviceHost != null)
					_serviceHost.Close();

				_serviceHost = new ServiceHost(typeof(TServiceImplementation), _servicesUri.ToArray());
			}
			catch (Exception e)
			{
				Console.WriteLine("Caught exception while creating " + ServiceName + ": " + e);
				return;
			}

			try
			{
				ExeConfigurationFileMap endpointFileMap = new ExeConfigurationFileMap();
				endpointFileMap.ExeConfigFilename = "App.config";
				Configuration endpointAppConfig = ConfigurationManager.OpenMappedExeConfiguration(endpointFileMap, ConfigurationUserLevel.None);
				ServiceModelSectionGroup endpointServiceModel = ServiceModelSectionGroup.GetSectionGroup(endpointAppConfig);

				IList<ServiceEndpointElement> endpoints = new List<ServiceEndpointElement>();

				foreach (ServiceElement service in endpointServiceModel.Services.Services)
					foreach (ServiceEndpointElement endpoint in service.Endpoints)
						endpoints.Add(endpoint);

				Configuration bindingsAppConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
				ServiceModelSectionGroup bindingsServiceModel = ServiceModelSectionGroup.GetSectionGroup(bindingsAppConfig);
				BindingsSection bindings = bindingsServiceModel.Bindings;

				foreach (ExtensionElement bindingExtension in bindingsServiceModel.Extensions.BindingExtensions)
				{
					foreach (IBindingConfigurationElement binding in bindings[bindingExtension.Name].ConfiguredBindings)
					{
						Type bindingType = bindings[bindingExtension.Name].BindingType;

						var dynamicBinding = (Binding)Activator.CreateInstance(bindingType, binding.Name);
						string address = (from ep in endpoints where ep.BindingConfiguration.Equals(binding.Name) select ep.Address.ToString()).First();
						_serviceHost.AddServiceEndpoint(typeof(TServiceContract), dynamicBinding, address);
					}
				}

				//Enable metadata exchange
				ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
				smb.HttpGetEnabled = true;
				_serviceHost.Description.Behaviors.Add(smb);
				_serviceHost.Description.Behaviors.Remove(typeof(ServiceDebugBehavior));
				_serviceHost.Description.Behaviors.Add(new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });

				_serviceHost.Open();
				openSucceeded = true;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Caught exception while starting " + ServiceName + ": " + ex);
			}
			finally
			{
				if (!openSucceeded)
				{
					_serviceHost.Abort();
				}
			}

			if (_serviceHost.State == CommunicationState.Opened)
			{
				Console.WriteLine(ServiceName + " started at ");
				foreach (var uri in _servicesUri)
					Console.WriteLine("\t" + uri.ToString());
			}
			else
			{
				Console.WriteLine(ServiceName + " failed to open");
				bool closeSucceeded = false;
				try
				{
					_serviceHost.Close();
					closeSucceeded = true;
				}
				catch (Exception ex)
				{
					Console.WriteLine(ServiceName + " failed to close: " + ex);
				}
				finally
				{
					if (!closeSucceeded)
					{
						_serviceHost.Abort();
					}
				}
			}
		}

		public new void Stop()
		{
			Console.WriteLine(ServiceName + " stopping...");
			try
			{
				if (_serviceHost != null)
				{
					_serviceHost.Close();
					_serviceHost = null;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Caught exception while stopping " + ServiceName + ": " + ex);
			}
			finally
			{
				Console.WriteLine(ServiceName + " stopped...");
			}
		}
	}
}
