﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.431
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProvisioningPrototype {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class RegexResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal RegexResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ProvisioningPrototype.RegexResource", typeof(RegexResource).Assembly);
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
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to window\.ComponentArt_Storage_ctl10_wT_wTt = (.*?);.
        /// </summary>
        internal static string ctl10_wT_wTt_DataRegex {
            get {
                return ResourceManager.GetString("ctl10_wT_wTt_DataRegex", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to \[HOST\].
        /// </summary>
        internal static string HostRegex {
            get {
                return ResourceManager.GetString("HostRegex", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;p_(10T4.*?)&apos;.
        /// </summary>
        internal static string Replace_1Regex {
            get {
                return ResourceManager.GetString("Replace_1Regex", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 10T(.*)$.
        /// </summary>
        internal static string Replace_2Regex {
            get {
                return ResourceManager.GetString("Replace_2Regex", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 10T(.*)$.
        /// </summary>
        internal static string Replace_3Regex {
            get {
                return ResourceManager.GetString("Replace_3Regex", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;p_(10T7.*?)&apos;.
        /// </summary>
        internal static string Replace_4Regex {
            get {
                return ResourceManager.GetString("Replace_4Regex", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to .reqid=(.*?);.
        /// </summary>
        internal static string ReqIdRegex {
            get {
                return ResourceManager.GetString("ReqIdRegex", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SessionId=(.*?);.
        /// </summary>
        internal static string SessionIdRegex {
            get {
                return ResourceManager.GetString("SessionIdRegex", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to id=&quot;TestLink&quot;.*&gt;(.*?)\&lt;/a\&gt;.
        /// </summary>
        internal static string TestLinkRegex {
            get {
                return ResourceManager.GetString("TestLinkRegex", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to VCPanelAuth=(.*?);.
        /// </summary>
        internal static string VcAuthenticationRegex {
            get {
                return ResourceManager.GetString("VcAuthenticationRegex", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to vcmach=(.*?);.
        /// </summary>
        internal static string VcMachRegex {
            get {
                return ResourceManager.GetString("VcMachRegex", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to id=&quot;__VIEWSTATE&quot; value=&quot;/(.*?)&quot; /&gt;.
        /// </summary>
        internal static string ViewStateRegex {
            get {
                return ResourceManager.GetString("ViewStateRegex", resourceCulture);
            }
        }
    }
}
