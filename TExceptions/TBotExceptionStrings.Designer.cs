﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TExceptions {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class TBotExceptionStrings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal TBotExceptionStrings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("TExceptions.TBotExceptionStrings", typeof(TBotExceptionStrings).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to There was an error while creating the DataBase.
        /// </summary>
        public static string ExceptionDBCreatingDB {
            get {
                return ResourceManager.GetString("ExceptionDBCreatingDB", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An Error ocurred while trying to create a new user.
        /// </summary>
        public static string ExceptionDBInsertingUser {
            get {
                return ResourceManager.GetString("ExceptionDBInsertingUser", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You cant Remove the Admin.
        /// </summary>
        public static string ExceptionDBRemoveAdmin {
            get {
                return ResourceManager.GetString("ExceptionDBRemoveAdmin", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An Error ocurred while trying to remove the user.
        /// </summary>
        public static string ExceptionDBRemovingUser {
            get {
                return ResourceManager.GetString("ExceptionDBRemovingUser", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An Error ocurred while trying to update the user.
        /// </summary>
        public static string ExceptionDBUpdatingUser {
            get {
                return ResourceManager.GetString("ExceptionDBUpdatingUser", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User not found.
        /// </summary>
        public static string ExceptionDBUserNotFound {
            get {
                return ResourceManager.GetString("ExceptionDBUserNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error ocurred while parsing google results.
        /// </summary>
        public static string ExceptionGoogleParserError {
            get {
                return ResourceManager.GetString("ExceptionGoogleParserError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No Google Results found for query: {0}.
        /// </summary>
        public static string ExceptionGoogleResultsNULL {
            get {
                return ResourceManager.GetString("ExceptionGoogleResultsNULL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Original Exception is -&gt; .
        /// </summary>
        public static string ExceptionOriginal {
            get {
                return ResourceManager.GetString("ExceptionOriginal", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to There was an error getting the reply from the chatbot API.
        /// </summary>
        public static string ExceptionTBotchatBotReply {
            get {
                return ResourceManager.GetString("ExceptionTBotchatBotReply", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed to parse the Group chat ID: {0} .
        /// </summary>
        public static string ExceptionTBotConfigFailedParseChatID {
            get {
                return ResourceManager.GetString("ExceptionTBotConfigFailedParseChatID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An Error ocurred while trying to send an msg.
        /// </summary>
        public static string ExceptionTBotSendMSG {
            get {
                return ResourceManager.GetString("ExceptionTBotSendMSG", resourceCulture);
            }
        }
    }
}
