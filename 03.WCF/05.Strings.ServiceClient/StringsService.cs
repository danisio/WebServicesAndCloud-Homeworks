﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------



[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.ServiceContractAttribute(ConfigurationName="IStringsService")]
public interface IStringsService
{
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStringsService/FindSubstrings", ReplyAction="http://tempuri.org/IStringsService/FindSubstringsResponse")]
    int FindSubstrings(string text, string subString);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStringsService/FindSubstrings", ReplyAction="http://tempuri.org/IStringsService/FindSubstringsResponse")]
    System.Threading.Tasks.Task<int> FindSubstringsAsync(string text, string subString);
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public interface IStringsServiceChannel : IStringsService, System.ServiceModel.IClientChannel
{
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public partial class StringsServiceClient : System.ServiceModel.ClientBase<IStringsService>, IStringsService
{
    
    public StringsServiceClient()
    {
    }
    
    public StringsServiceClient(string endpointConfigurationName) : 
            base(endpointConfigurationName)
    {
    }
    
    public StringsServiceClient(string endpointConfigurationName, string remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
    {
    }
    
    public StringsServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
    {
    }
    
    public StringsServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(binding, remoteAddress)
    {
    }
    
    public int FindSubstrings(string text, string subString)
    {
        return base.Channel.FindSubstrings(text, subString);
    }
    
    public System.Threading.Tasks.Task<int> FindSubstringsAsync(string text, string subString)
    {
        return base.Channel.FindSubstringsAsync(text, subString);
    }
}