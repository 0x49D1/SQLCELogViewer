﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

[assembly: EdmSchemaAttribute()]
namespace SQLCELogViewer
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class LoggerEntities : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new LoggerEntities object using the connection string found in the 'LoggerEntities' section of the application configuration file.
        /// </summary>
        public LoggerEntities() : base("name=LoggerEntities", "LoggerEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new LoggerEntities object.
        /// </summary>
        public LoggerEntities(string connectionString) : base(connectionString, "LoggerEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new LoggerEntities object.
        /// </summary>
        public LoggerEntities(EntityConnection connection) : base(connection, "LoggerEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<LogEntry> LogEntries
        {
            get
            {
                if ((_LogEntries == null))
                {
                    _LogEntries = base.CreateObjectSet<LogEntry>("LogEntries");
                }
                return _LogEntries;
            }
        }
        private ObjectSet<LogEntry> _LogEntries;

        #endregion

        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the LogEntries EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToLogEntries(LogEntry logEntry)
        {
            base.AddObject("LogEntries", logEntry);
        }

        #endregion

    }

    #endregion

    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="LoggerModel", Name="LogEntry")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class LogEntry : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new LogEntry object.
        /// </summary>
        /// <param name="id">Initial value of the id property.</param>
        public static LogEntry CreateLogEntry(global::System.Int32 id)
        {
            LogEntry logEntry = new LogEntry();
            logEntry.id = id;
            return logEntry;
        }

        #endregion

        #region Simple Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 id
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id != value)
                {
                    OnidChanging(value);
                    ReportPropertyChanging("id");
                    _id = StructuralObject.SetValidValue(value, "id");
                    ReportPropertyChanged("id");
                    OnidChanged();
                }
            }
        }
        private global::System.Int32 _id;
        partial void OnidChanging(global::System.Int32 value);
        partial void OnidChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.DateTime> TimeStamp
        {
            get
            {
                return _TimeStamp;
            }
            set
            {
                OnTimeStampChanging(value);
                ReportPropertyChanging("TimeStamp");
                _TimeStamp = StructuralObject.SetValidValue(value, "TimeStamp");
                ReportPropertyChanged("TimeStamp");
                OnTimeStampChanged();
            }
        }
        private Nullable<global::System.DateTime> _TimeStamp;
        partial void OnTimeStampChanging(Nullable<global::System.DateTime> value);
        partial void OnTimeStampChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Message
        {
            get
            {
                return _Message;
            }
            set
            {
                OnMessageChanging(value);
                ReportPropertyChanging("Message");
                _Message = StructuralObject.SetValidValue(value, true, "Message");
                ReportPropertyChanged("Message");
                OnMessageChanged();
            }
        }
        private global::System.String _Message;
        partial void OnMessageChanging(global::System.String value);
        partial void OnMessageChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String level
        {
            get
            {
                return _level;
            }
            set
            {
                OnlevelChanging(value);
                ReportPropertyChanging("level");
                _level = StructuralObject.SetValidValue(value, true, "level");
                ReportPropertyChanged("level");
                OnlevelChanged();
            }
        }
        private global::System.String _level;
        partial void OnlevelChanging(global::System.String value);
        partial void OnlevelChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String logger
        {
            get
            {
                return _logger;
            }
            set
            {
                OnloggerChanging(value);
                ReportPropertyChanging("logger");
                _logger = StructuralObject.SetValidValue(value, true, "logger");
                ReportPropertyChanged("logger");
                OnloggerChanged();
            }
        }
        private global::System.String _logger;
        partial void OnloggerChanging(global::System.String value);
        partial void OnloggerChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Exception
        {
            get
            {
                return _Exception;
            }
            set
            {
                OnExceptionChanging(value);
                ReportPropertyChanging("Exception");
                _Exception = StructuralObject.SetValidValue(value, true, "Exception");
                ReportPropertyChanged("Exception");
                OnExceptionChanged();
            }
        }
        private global::System.String _Exception;
        partial void OnExceptionChanging(global::System.String value);
        partial void OnExceptionChanged();

        #endregion

    }

    #endregion

}
