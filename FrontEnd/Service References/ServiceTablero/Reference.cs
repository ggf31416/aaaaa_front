﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FrontEnd.ServiceTablero {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="InfoCelda", Namespace="http://schemas.datacontract.org/2004/07/Shared.Entities")]
    [System.SerializableAttribute()]
    public partial class InfoCelda : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> PosXField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> PosYField;
        
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
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> PosX {
            get {
                return this.PosXField;
            }
            set {
                if ((this.PosXField.Equals(value) != true)) {
                    this.PosXField = value;
                    this.RaisePropertyChanged("PosX");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> PosY {
            get {
                return this.PosYField;
            }
            set {
                if ((this.PosYField.Equals(value) != true)) {
                    this.PosYField = value;
                    this.RaisePropertyChanged("PosY");
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://localhost:8836/tsi1/", ConfigurationName="ServiceTablero.IServiceTablero")]
    public interface IServiceTablero {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://localhost:8836/tsi1/IServiceTablero/JugarUnidad", ReplyAction="http://localhost:8836/tsi1/IServiceTablero/JugarUnidadResponse")]
        void JugarUnidad(FrontEnd.ServiceTablero.InfoCelda infoCelda);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://localhost:8836/tsi1/IServiceTablero/JugarUnidad", ReplyAction="http://localhost:8836/tsi1/IServiceTablero/JugarUnidadResponse")]
        System.Threading.Tasks.Task JugarUnidadAsync(FrontEnd.ServiceTablero.InfoCelda infoCelda);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://localhost:8836/tsi1/IServiceTablero/Accion", ReplyAction="http://localhost:8836/tsi1/IServiceTablero/AccionResponse")]
        void Accion(string json);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://localhost:8836/tsi1/IServiceTablero/Accion", ReplyAction="http://localhost:8836/tsi1/IServiceTablero/AccionResponse")]
        System.Threading.Tasks.Task AccionAsync(string json);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceTableroChannel : FrontEnd.ServiceTablero.IServiceTablero, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceTableroClient : System.ServiceModel.ClientBase<FrontEnd.ServiceTablero.IServiceTablero>, FrontEnd.ServiceTablero.IServiceTablero {
        
        public ServiceTableroClient() {
        }
        
        public ServiceTableroClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceTableroClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceTableroClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceTableroClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void JugarUnidad(FrontEnd.ServiceTablero.InfoCelda infoCelda) {
            base.Channel.JugarUnidad(infoCelda);
        }
        
        public System.Threading.Tasks.Task JugarUnidadAsync(FrontEnd.ServiceTablero.InfoCelda infoCelda) {
            return base.Channel.JugarUnidadAsync(infoCelda);
        }
        
        public void Accion(string json) {
            base.Channel.Accion(json);
        }
        
        public System.Threading.Tasks.Task AccionAsync(string json) {
            return base.Channel.AccionAsync(json);
        }
    }
}