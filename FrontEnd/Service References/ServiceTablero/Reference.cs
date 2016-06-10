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
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://localhost:8836/tsi1/", ConfigurationName="ServiceTablero.IServiceTablero")]
    public interface IServiceTablero {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://localhost:8836/tsi1/IServiceTablero/JugarUnidad", ReplyAction="http://localhost:8836/tsi1/IServiceTablero/JugarUnidadResponse")]
        void JugarUnidad(Shared.Entities.InfoCelda infoCelda);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://localhost:8836/tsi1/IServiceTablero/JugarUnidad", ReplyAction="http://localhost:8836/tsi1/IServiceTablero/JugarUnidadResponse")]
        System.Threading.Tasks.Task JugarUnidadAsync(Shared.Entities.InfoCelda infoCelda);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://localhost:8836/tsi1/IServiceTablero/Accion", ReplyAction="http://localhost:8836/tsi1/IServiceTablero/AccionResponse")]
        void Accion(string json);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://localhost:8836/tsi1/IServiceTablero/Accion", ReplyAction="http://localhost:8836/tsi1/IServiceTablero/AccionResponse")]
        System.Threading.Tasks.Task AccionAsync(string json);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://localhost:8836/tsi1/IServiceTablero/GetListaDeJugadoresAtacables", ReplyAction="http://localhost:8836/tsi1/IServiceTablero/GetListaDeJugadoresAtacablesResponse")]
        Shared.Entities.JugadorBasico[] GetListaDeJugadoresAtacables(string jugadorAt);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://localhost:8836/tsi1/IServiceTablero/GetListaDeJugadoresAtacables", ReplyAction="http://localhost:8836/tsi1/IServiceTablero/GetListaDeJugadoresAtacablesResponse")]
        System.Threading.Tasks.Task<Shared.Entities.JugadorBasico[]> GetListaDeJugadoresAtacablesAsync(string jugadorAt);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://localhost:8836/tsi1/IServiceTablero/RegistrarJugador", ReplyAction="http://localhost:8836/tsi1/IServiceTablero/RegistrarJugadorResponse")]
        void RegistrarJugador(string nombre);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://localhost:8836/tsi1/IServiceTablero/RegistrarJugador", ReplyAction="http://localhost:8836/tsi1/IServiceTablero/RegistrarJugadorResponse")]
        System.Threading.Tasks.Task RegistrarJugadorAsync(string nombre);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://localhost:8836/tsi1/IServiceTablero/IniciarAtaque", ReplyAction="http://localhost:8836/tsi1/IServiceTablero/IniciarAtaqueResponse")]
        void IniciarAtaque(Shared.Entities.InfoAtaque info);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://localhost:8836/tsi1/IServiceTablero/IniciarAtaque", ReplyAction="http://localhost:8836/tsi1/IServiceTablero/IniciarAtaqueResponse")]
        System.Threading.Tasks.Task IniciarAtaqueAsync(Shared.Entities.InfoAtaque info);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://localhost:8836/tsi1/IServiceTablero/login", ReplyAction="http://localhost:8836/tsi1/IServiceTablero/loginResponse")]
        void login(Shared.Entities.Cliente cliente, int idJuego);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://localhost:8836/tsi1/IServiceTablero/login", ReplyAction="http://localhost:8836/tsi1/IServiceTablero/loginResponse")]
        System.Threading.Tasks.Task loginAsync(Shared.Entities.Cliente cliente, int idJuego);
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
        
        public void JugarUnidad(Shared.Entities.InfoCelda infoCelda) {
            base.Channel.JugarUnidad(infoCelda);
        }
        
        public System.Threading.Tasks.Task JugarUnidadAsync(Shared.Entities.InfoCelda infoCelda) {
            return base.Channel.JugarUnidadAsync(infoCelda);
        }
        
        public void Accion(string json) {
            base.Channel.Accion(json);
        }
        
        public System.Threading.Tasks.Task AccionAsync(string json) {
            return base.Channel.AccionAsync(json);
        }
        
        public Shared.Entities.JugadorBasico[] GetListaDeJugadoresAtacables(string jugadorAt) {
            return base.Channel.GetListaDeJugadoresAtacables(jugadorAt);
        }
        
        public System.Threading.Tasks.Task<Shared.Entities.JugadorBasico[]> GetListaDeJugadoresAtacablesAsync(string jugadorAt) {
            return base.Channel.GetListaDeJugadoresAtacablesAsync(jugadorAt);
        }
        
        public void RegistrarJugador(string nombre) {
            base.Channel.RegistrarJugador(nombre);
        }
        
        public System.Threading.Tasks.Task RegistrarJugadorAsync(string nombre) {
            return base.Channel.RegistrarJugadorAsync(nombre);
        }
        
        public void IniciarAtaque(Shared.Entities.InfoAtaque info) {
            base.Channel.IniciarAtaque(info);
        }
        
        public System.Threading.Tasks.Task IniciarAtaqueAsync(Shared.Entities.InfoAtaque info) {
            return base.Channel.IniciarAtaqueAsync(info);
        }
        
        public void login(Shared.Entities.Cliente cliente, int idJuego) {
            base.Channel.login(cliente, idJuego);
        }
        
        public System.Threading.Tasks.Task loginAsync(Shared.Entities.Cliente cliente, int idJuego) {
            return base.Channel.loginAsync(cliente, idJuego);
        }
    }
}
