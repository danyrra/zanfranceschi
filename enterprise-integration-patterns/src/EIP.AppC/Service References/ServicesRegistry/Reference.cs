﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EIP.AppC.ServicesRegistry {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Service", Namespace="http://schemas.datacontract.org/2004/07/EIP.ServicesRegistry.Core.Entities")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(EIP.AppC.ServicesRegistry.EventService))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(EIP.AppC.ServicesRegistry.RequestService))]
    public partial class Service : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AddressField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ServiceTypeField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Address {
            get {
                return this.AddressField;
            }
            set {
                if ((object.ReferenceEquals(this.AddressField, value) != true)) {
                    this.AddressField = value;
                    this.RaisePropertyChanged("Address");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Id {
            get {
                return this.IdField;
            }
            set {
                if ((object.ReferenceEquals(this.IdField, value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ServiceType {
            get {
                return this.ServiceTypeField;
            }
            set {
                if ((object.ReferenceEquals(this.ServiceTypeField, value) != true)) {
                    this.ServiceTypeField = value;
                    this.RaisePropertyChanged("ServiceType");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="EventService", Namespace="http://schemas.datacontract.org/2004/07/EIP.ServicesRegistry.Core.Entities")]
    [System.SerializableAttribute()]
    public partial class EventService : EIP.AppC.ServicesRegistry.Service {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DataTypeField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DataType {
            get {
                return this.DataTypeField;
            }
            set {
                if ((object.ReferenceEquals(this.DataTypeField, value) != true)) {
                    this.DataTypeField = value;
                    this.RaisePropertyChanged("DataType");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="RequestService", Namespace="http://schemas.datacontract.org/2004/07/EIP.ServicesRegistry.Core.Entities")]
    [System.SerializableAttribute()]
    public partial class RequestService : EIP.AppC.ServicesRegistry.Service {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DefinitionUrlField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DefinitionUrl {
            get {
                return this.DefinitionUrlField;
            }
            set {
                if ((object.ReferenceEquals(this.DefinitionUrlField, value) != true)) {
                    this.DefinitionUrlField = value;
                    this.RaisePropertyChanged("DefinitionUrl");
                }
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServicesRegistry.IServiceRegistry")]
    public interface IServiceRegistry {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceRegistry/Create", ReplyAction="http://tempuri.org/IServiceRegistry/CreateResponse")]
        string Create(EIP.AppC.ServicesRegistry.Service service);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceRegistry/CreateEventService", ReplyAction="http://tempuri.org/IServiceRegistry/CreateEventServiceResponse")]
        string CreateEventService(string name, string description, string address, string dataType);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceRegistry/CreateRequestService", ReplyAction="http://tempuri.org/IServiceRegistry/CreateRequestServiceResponse")]
        string CreateRequestService(string name, string description, string address, string definitionUrl);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceRegistry/GetAllEventServices", ReplyAction="http://tempuri.org/IServiceRegistry/GetAllEventServicesResponse")]
        EIP.AppC.ServicesRegistry.EventService[] GetAllEventServices();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceRegistry/GetAllRequestServices", ReplyAction="http://tempuri.org/IServiceRegistry/GetAllRequestServicesResponse")]
        EIP.AppC.ServicesRegistry.RequestService[] GetAllRequestServices();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceRegistry/GetById", ReplyAction="http://tempuri.org/IServiceRegistry/GetByIdResponse")]
        EIP.AppC.ServicesRegistry.Service GetById(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceRegistry/FindOneByDataType", ReplyAction="http://tempuri.org/IServiceRegistry/FindOneByDataTypeResponse")]
        EIP.AppC.ServicesRegistry.EventService FindOneByDataType(string dataTypeFullName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceRegistry/Remove", ReplyAction="http://tempuri.org/IServiceRegistry/RemoveResponse")]
        void Remove(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceRegistry/Search", ReplyAction="http://tempuri.org/IServiceRegistry/SearchResponse")]
        EIP.AppC.ServicesRegistry.Service[] Search(string term);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceRegistry/Update", ReplyAction="http://tempuri.org/IServiceRegistry/UpdateResponse")]
        void Update(EIP.AppC.ServicesRegistry.Service service);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceRegistry/UpdateEventService", ReplyAction="http://tempuri.org/IServiceRegistry/UpdateEventServiceResponse")]
        void UpdateEventService(string id, string name, string description, string address, string dataType);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceRegistry/UpdateRequestService", ReplyAction="http://tempuri.org/IServiceRegistry/UpdateRequestServiceResponse")]
        void UpdateRequestService(string id, string name, string description, string address, string definitionUrl);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceRegistryChannel : EIP.AppC.ServicesRegistry.IServiceRegistry, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceRegistryClient : System.ServiceModel.ClientBase<EIP.AppC.ServicesRegistry.IServiceRegistry>, EIP.AppC.ServicesRegistry.IServiceRegistry {
        
        public ServiceRegistryClient() {
        }
        
        public ServiceRegistryClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceRegistryClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceRegistryClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceRegistryClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string Create(EIP.AppC.ServicesRegistry.Service service) {
            return base.Channel.Create(service);
        }
        
        public string CreateEventService(string name, string description, string address, string dataType) {
            return base.Channel.CreateEventService(name, description, address, dataType);
        }
        
        public string CreateRequestService(string name, string description, string address, string definitionUrl) {
            return base.Channel.CreateRequestService(name, description, address, definitionUrl);
        }
        
        public EIP.AppC.ServicesRegistry.EventService[] GetAllEventServices() {
            return base.Channel.GetAllEventServices();
        }
        
        public EIP.AppC.ServicesRegistry.RequestService[] GetAllRequestServices() {
            return base.Channel.GetAllRequestServices();
        }
        
        public EIP.AppC.ServicesRegistry.Service GetById(string id) {
            return base.Channel.GetById(id);
        }
        
        public EIP.AppC.ServicesRegistry.EventService FindOneByDataType(string dataTypeFullName) {
            return base.Channel.FindOneByDataType(dataTypeFullName);
        }
        
        public void Remove(string id) {
            base.Channel.Remove(id);
        }
        
        public EIP.AppC.ServicesRegistry.Service[] Search(string term) {
            return base.Channel.Search(term);
        }
        
        public void Update(EIP.AppC.ServicesRegistry.Service service) {
            base.Channel.Update(service);
        }
        
        public void UpdateEventService(string id, string name, string description, string address, string dataType) {
            base.Channel.UpdateEventService(id, name, description, address, dataType);
        }
        
        public void UpdateRequestService(string id, string name, string description, string address, string definitionUrl) {
            base.Channel.UpdateRequestService(id, name, description, address, definitionUrl);
        }
    }
}
