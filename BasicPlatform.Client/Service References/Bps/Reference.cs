﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18052
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace BasicPlatform.Client.Bps {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="User", Namespace="http://schemas.datacontract.org/2004/07/BasicPlatform.Web.Models")]
    [System.SerializableAttribute()]
    internal partial class User : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.List<BasicPlatform.Client.Bps.ObjectId> AppsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EmailField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private BasicPlatform.Client.Bps.ObjectId IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LabelField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PasswordField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.List<BasicPlatform.Client.Bps.ObjectId> RolesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UsernameField;
        
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
        internal System.Collections.Generic.List<BasicPlatform.Client.Bps.ObjectId> Apps {
            get {
                return this.AppsField;
            }
            set {
                if ((object.ReferenceEquals(this.AppsField, value) != true)) {
                    this.AppsField = value;
                    this.RaisePropertyChanged("Apps");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        internal string Email {
            get {
                return this.EmailField;
            }
            set {
                if ((object.ReferenceEquals(this.EmailField, value) != true)) {
                    this.EmailField = value;
                    this.RaisePropertyChanged("Email");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        internal BasicPlatform.Client.Bps.ObjectId Id {
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
        internal string Label {
            get {
                return this.LabelField;
            }
            set {
                if ((object.ReferenceEquals(this.LabelField, value) != true)) {
                    this.LabelField = value;
                    this.RaisePropertyChanged("Label");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        internal string Password {
            get {
                return this.PasswordField;
            }
            set {
                if ((object.ReferenceEquals(this.PasswordField, value) != true)) {
                    this.PasswordField = value;
                    this.RaisePropertyChanged("Password");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        internal System.Collections.Generic.List<BasicPlatform.Client.Bps.ObjectId> Roles {
            get {
                return this.RolesField;
            }
            set {
                if ((object.ReferenceEquals(this.RolesField, value) != true)) {
                    this.RolesField = value;
                    this.RaisePropertyChanged("Roles");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        internal string Username {
            get {
                return this.UsernameField;
            }
            set {
                if ((object.ReferenceEquals(this.UsernameField, value) != true)) {
                    this.UsernameField = value;
                    this.RaisePropertyChanged("Username");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="ObjectId", Namespace="http://schemas.datacontract.org/2004/07/MongoDB.Bson")]
    [System.SerializableAttribute()]
    internal partial struct ObjectId : System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private int _incrementField;
        
        private int _machineField;
        
        private short _pidField;
        
        private int _timestampField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        internal int _increment {
            get {
                return this._incrementField;
            }
            set {
                if ((this._incrementField.Equals(value) != true)) {
                    this._incrementField = value;
                    this.RaisePropertyChanged("_increment");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        internal int _machine {
            get {
                return this._machineField;
            }
            set {
                if ((this._machineField.Equals(value) != true)) {
                    this._machineField = value;
                    this.RaisePropertyChanged("_machine");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        internal short _pid {
            get {
                return this._pidField;
            }
            set {
                if ((this._pidField.Equals(value) != true)) {
                    this._pidField = value;
                    this.RaisePropertyChanged("_pid");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        internal int _timestamp {
            get {
                return this._timestampField;
            }
            set {
                if ((this._timestampField.Equals(value) != true)) {
                    this._timestampField = value;
                    this.RaisePropertyChanged("_timestamp");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="App", Namespace="http://schemas.datacontract.org/2004/07/BasicPlatform.Web.Models")]
    [System.SerializableAttribute()]
    internal partial class App : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool AccessableField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ConnectUrlField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private BasicPlatform.Client.Bps.ObjectId IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LabelField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.List<BasicPlatform.Client.Bps.ObjectId> RolesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SecretField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool SelfConnectableField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UrlField;
        
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
        internal bool Accessable {
            get {
                return this.AccessableField;
            }
            set {
                if ((this.AccessableField.Equals(value) != true)) {
                    this.AccessableField = value;
                    this.RaisePropertyChanged("Accessable");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        internal string ConnectUrl {
            get {
                return this.ConnectUrlField;
            }
            set {
                if ((object.ReferenceEquals(this.ConnectUrlField, value) != true)) {
                    this.ConnectUrlField = value;
                    this.RaisePropertyChanged("ConnectUrl");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        internal BasicPlatform.Client.Bps.ObjectId Id {
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
        internal string Label {
            get {
                return this.LabelField;
            }
            set {
                if ((object.ReferenceEquals(this.LabelField, value) != true)) {
                    this.LabelField = value;
                    this.RaisePropertyChanged("Label");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        internal string Name {
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
        internal System.Collections.Generic.List<BasicPlatform.Client.Bps.ObjectId> Roles {
            get {
                return this.RolesField;
            }
            set {
                if ((object.ReferenceEquals(this.RolesField, value) != true)) {
                    this.RolesField = value;
                    this.RaisePropertyChanged("Roles");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        internal string Secret {
            get {
                return this.SecretField;
            }
            set {
                if ((object.ReferenceEquals(this.SecretField, value) != true)) {
                    this.SecretField = value;
                    this.RaisePropertyChanged("Secret");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        internal bool SelfConnectable {
            get {
                return this.SelfConnectableField;
            }
            set {
                if ((this.SelfConnectableField.Equals(value) != true)) {
                    this.SelfConnectableField = value;
                    this.RaisePropertyChanged("SelfConnectable");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        internal string Url {
            get {
                return this.UrlField;
            }
            set {
                if ((object.ReferenceEquals(this.UrlField, value) != true)) {
                    this.UrlField = value;
                    this.RaisePropertyChanged("Url");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Bps.IService")]
    internal interface IService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetUserByAlias", ReplyAction="http://tempuri.org/IService/GetUserByAliasResponse")]
        BasicPlatform.Client.Bps.User GetUserByAlias(string alias, string appKey, string appSecret);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetUser", ReplyAction="http://tempuri.org/IService/GetUserResponse")]
        BasicPlatform.Client.Bps.User GetUser(string username, string appKey, string appSecret);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetUserById", ReplyAction="http://tempuri.org/IService/GetUserByIdResponse")]
        BasicPlatform.Client.Bps.User GetUserById(string id, string appKey, string appSecret);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/ValidateUser", ReplyAction="http://tempuri.org/IService/ValidateUserResponse")]
        bool ValidateUser(string username, string password, string appKey, string appSecret);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetUsers", ReplyAction="http://tempuri.org/IService/GetUsersResponse")]
        System.Collections.Generic.List<BasicPlatform.Client.Bps.User> GetUsers(out long totalRecords, long pageIndex, long pageSize, string appKey, string appSecret);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetUsersInRole", ReplyAction="http://tempuri.org/IService/GetUsersInRoleResponse")]
        System.Collections.Generic.List<string> GetUsersInRole(string roleName, string username, string appKey, string appSecret);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetRoles", ReplyAction="http://tempuri.org/IService/GetRolesResponse")]
        System.Collections.Generic.List<string> GetRoles(string appKey, string appSecret);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetRolesForUser", ReplyAction="http://tempuri.org/IService/GetRolesForUserResponse")]
        System.Collections.Generic.List<string> GetRolesForUser(string username, string appKey, string appSecret);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/IsUserInRole", ReplyAction="http://tempuri.org/IService/IsUserInRoleResponse")]
        bool IsUserInRole(string username, string roleName, string appKey, string appSecret);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/IsRoleExists", ReplyAction="http://tempuri.org/IService/IsRoleExistsResponse")]
        bool IsRoleExists(string roleName, string appKey, string appSecret);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetApp", ReplyAction="http://tempuri.org/IService/GetAppResponse")]
        BasicPlatform.Client.Bps.App GetApp(string appKey, string appSecret);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetUserByAccessToken", ReplyAction="http://tempuri.org/IService/GetUserByAccessTokenResponse")]
        BasicPlatform.Client.Bps.User GetUserByAccessToken(string clientIdentifier, string accessToken, string appKey, string appSecret);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/SetAliasByAccessToken", ReplyAction="http://tempuri.org/IService/SetAliasByAccessTokenResponse")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="ok")]
        bool SetAliasByAccessToken(string clientIdentifier, string accessToken, string alias, string appKey, string appSecret);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetAliasByAccessToken", ReplyAction="http://tempuri.org/IService/GetAliasByAccessTokenResponse")]
        string GetAliasByAccessToken(out bool ok, string clientIdentifier, string accessToken, string appKey, string appSecret);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/SetAliasOfUserByAccessToken", ReplyAction="http://tempuri.org/IService/SetAliasOfUserByAccessTokenResponse")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="ok")]
        bool SetAliasOfUserByAccessToken(string clientIdentifier, string accessToken, string username, string alias, string appKey, string appSecret);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetAliasOfUserByAccessToken", ReplyAction="http://tempuri.org/IService/GetAliasOfUserByAccessTokenResponse")]
        string GetAliasOfUserByAccessToken(out bool ok, string clientIdentifier, string accessToken, string username, string appKey, string appSecret);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetActions", ReplyAction="http://tempuri.org/IService/GetActionsResponse")]
        string GetActions(string username, string appKey, string appSecret);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    internal interface IServiceChannel : BasicPlatform.Client.Bps.IService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    internal partial class ServiceClient : System.ServiceModel.ClientBase<BasicPlatform.Client.Bps.IService>, BasicPlatform.Client.Bps.IService {
        
        public ServiceClient() {
        }
        
        public ServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public BasicPlatform.Client.Bps.User GetUserByAlias(string alias, string appKey, string appSecret) {
            return base.Channel.GetUserByAlias(alias, appKey, appSecret);
        }
        
        public BasicPlatform.Client.Bps.User GetUser(string username, string appKey, string appSecret) {
            return base.Channel.GetUser(username, appKey, appSecret);
        }
        
        public BasicPlatform.Client.Bps.User GetUserById(string id, string appKey, string appSecret) {
            return base.Channel.GetUserById(id, appKey, appSecret);
        }
        
        public bool ValidateUser(string username, string password, string appKey, string appSecret) {
            return base.Channel.ValidateUser(username, password, appKey, appSecret);
        }
        
        public System.Collections.Generic.List<BasicPlatform.Client.Bps.User> GetUsers(out long totalRecords, long pageIndex, long pageSize, string appKey, string appSecret) {
            return base.Channel.GetUsers(out totalRecords, pageIndex, pageSize, appKey, appSecret);
        }
        
        public System.Collections.Generic.List<string> GetUsersInRole(string roleName, string username, string appKey, string appSecret) {
            return base.Channel.GetUsersInRole(roleName, username, appKey, appSecret);
        }
        
        public System.Collections.Generic.List<string> GetRoles(string appKey, string appSecret) {
            return base.Channel.GetRoles(appKey, appSecret);
        }
        
        public System.Collections.Generic.List<string> GetRolesForUser(string username, string appKey, string appSecret) {
            return base.Channel.GetRolesForUser(username, appKey, appSecret);
        }
        
        public bool IsUserInRole(string username, string roleName, string appKey, string appSecret) {
            return base.Channel.IsUserInRole(username, roleName, appKey, appSecret);
        }
        
        public bool IsRoleExists(string roleName, string appKey, string appSecret) {
            return base.Channel.IsRoleExists(roleName, appKey, appSecret);
        }
        
        public BasicPlatform.Client.Bps.App GetApp(string appKey, string appSecret) {
            return base.Channel.GetApp(appKey, appSecret);
        }
        
        public BasicPlatform.Client.Bps.User GetUserByAccessToken(string clientIdentifier, string accessToken, string appKey, string appSecret) {
            return base.Channel.GetUserByAccessToken(clientIdentifier, accessToken, appKey, appSecret);
        }
        
        public bool SetAliasByAccessToken(string clientIdentifier, string accessToken, string alias, string appKey, string appSecret) {
            return base.Channel.SetAliasByAccessToken(clientIdentifier, accessToken, alias, appKey, appSecret);
        }
        
        public string GetAliasByAccessToken(out bool ok, string clientIdentifier, string accessToken, string appKey, string appSecret) {
            return base.Channel.GetAliasByAccessToken(out ok, clientIdentifier, accessToken, appKey, appSecret);
        }
        
        public bool SetAliasOfUserByAccessToken(string clientIdentifier, string accessToken, string username, string alias, string appKey, string appSecret) {
            return base.Channel.SetAliasOfUserByAccessToken(clientIdentifier, accessToken, username, alias, appKey, appSecret);
        }
        
        public string GetAliasOfUserByAccessToken(out bool ok, string clientIdentifier, string accessToken, string username, string appKey, string appSecret) {
            return base.Channel.GetAliasOfUserByAccessToken(out ok, clientIdentifier, accessToken, username, appKey, appSecret);
        }
        
        public string GetActions(string username, string appKey, string appSecret) {
            return base.Channel.GetActions(username, appKey, appSecret);
        }
    }
}
