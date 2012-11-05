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
    [System.Runtime.Serialization.DataContractAttribute(Name="ServiceRegistry", Namespace="http://schemas.datacontract.org/2004/07/EIP.ServicesRegistry.Core.Entities")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(EIP.AppC.ServicesRegistry.WebServiceRegistry))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(EIP.AppC.ServicesRegistry.EventRegistry))]
    public partial class ServiceRegistry : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
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
        private string TechnicalDetailsField;
        
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
        public string TechnicalDetails {
            get {
                return this.TechnicalDetailsField;
            }
            set {
                if ((object.ReferenceEquals(this.TechnicalDetailsField, value) != true)) {
                    this.TechnicalDetailsField = value;
                    this.RaisePropertyChanged("TechnicalDetails");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="WebServiceRegistry", Namespace="http://schemas.datacontract.org/2004/07/EIP.ServicesRegistry.Core.Entities")]
    [System.SerializableAttribute()]
    public partial class WebServiceRegistry : EIP.AppC.ServicesRegistry.ServiceRegistry {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string WsdlUrlField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string WsdlUrl {
            get {
                return this.WsdlUrlField;
            }
            set {
                if ((object.ReferenceEquals(this.WsdlUrlField, value) != true)) {
                    this.WsdlUrlField = value;
                    this.RaisePropertyChanged("WsdlUrl");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="EventRegistry", Namespace="http://schemas.datacontract.org/2004/07/EIP.ServicesRegistry.Core.Entities")]
    [System.SerializableAttribute()]
    public partial class EventRegistry : EIP.AppC.ServicesRegistry.ServiceRegistry {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CanonicalDataTypeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CanonicalDataTypeVersionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ProtocolField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CanonicalDataType {
            get {
                return this.CanonicalDataTypeField;
            }
            set {
                if ((object.ReferenceEquals(this.CanonicalDataTypeField, value) != true)) {
                    this.CanonicalDataTypeField = value;
                    this.RaisePropertyChanged("CanonicalDataType");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CanonicalDataTypeVersion {
            get {
                return this.CanonicalDataTypeVersionField;
            }
            set {
                if ((object.ReferenceEquals(this.CanonicalDataTypeVersionField, value) != true)) {
                    this.CanonicalDataTypeVersionField = value;
                    this.RaisePropertyChanged("CanonicalDataTypeVersion");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Protocol {
            get {
                return this.ProtocolField;
            }
            set {
                if ((object.ReferenceEquals(this.ProtocolField, value) != true)) {
                    this.ProtocolField = value;
                    this.RaisePropertyChanged("Protocol");
                }
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServicesRegistry.IServiceRegistry")]
    public interface IServiceRegistry {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceRegistry/FindEventByDataType", ReplyAction="http://tempuri.org/IServiceRegistry/FindEventByDataTypeResponse")]
        EIP.AppC.ServicesRegistry.EventRegistry FindEventByDataType(string dataTypeFullName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceRegistry/SearchEvents", ReplyAction="http://tempuri.org/IServiceRegistry/SearchEventsResponse")]
        EIP.AppC.ServicesRegistry.EventRegistry[] SearchEvents(string term);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceRegistry/SearchWebService", ReplyAction="http://tempuri.org/IServiceRegistry/SearchWebServiceResponse")]
        EIP.AppC.ServicesRegistry.WebServiceRegistry[] SearchWebService(string term);
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
        
        public EIP.AppC.ServicesRegistry.EventRegistry FindEventByDataType(string dataTypeFullName) {
            return base.Channel.FindEventByDataType(dataTypeFullName);
        }
        
        public EIP.AppC.ServicesRegistry.EventRegistry[] SearchEvents(string term) {
            return base.Channel.SearchEvents(term);
        }
        
        public EIP.AppC.ServicesRegistry.WebServiceRegistry[] SearchWebService(string term) {
            return base.Channel.SearchWebService(term);
        }
    }
}