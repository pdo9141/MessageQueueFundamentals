﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClientConsole.HelloWorldBridge {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="HelloWorldBridge.IHelloWorld")]
    public interface IHelloWorld {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHelloWorld/DoWork", ReplyAction="http://tempuri.org/IHelloWorld/DoWorkResponse")]
        void DoWork(string message);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHelloWorld/DoWork", ReplyAction="http://tempuri.org/IHelloWorld/DoWorkResponse")]
        System.Threading.Tasks.Task DoWorkAsync(string message);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IHelloWorldChannel : ClientConsole.HelloWorldBridge.IHelloWorld, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class HelloWorldClient : System.ServiceModel.ClientBase<ClientConsole.HelloWorldBridge.IHelloWorld>, ClientConsole.HelloWorldBridge.IHelloWorld {
        
        public HelloWorldClient() {
        }
        
        public HelloWorldClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public HelloWorldClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public HelloWorldClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public HelloWorldClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void DoWork(string message) {
            base.Channel.DoWork(message);
        }
        
        public System.Threading.Tasks.Task DoWorkAsync(string message) {
            return base.Channel.DoWorkAsync(message);
        }
    }
}
