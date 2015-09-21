namespace ROOT.CIMV2.Win32 {
    using System;
    using System.ComponentModel;
    using System.Management;
    using System.Collections;
    using System.Globalization;
    
    
    // Functions ShouldSerialize<PropertyName> are functions used by VS property browser to check if a particular property has to be serialized. These functions are added for all ValueType properties ( properties of type Int32, BOOL etc.. which cannot be set to null). These functions use Is<PropertyName>Null function. These functions are also used in the TypeConverter implementation for the properties to check for NULL value of property so that an empty value can be shown in Property browser in case of Drag and Drop in Visual studio.
    // Functions Is<PropertyName>Null() are used to check if a property is NULL.
    // Functions Reset<PropertyName> are added for Nullable Read/Write properties. These functions are used by VS designer in property browser to set a property to NULL.
    // Every property added to the class for WMI property has attributes set to define its behavior in Visual Studio designer and also to define a TypeConverter to be used.
    // Datetime conversion functions ToDateTime and ToDmtfDateTime are added to the class to convert DMTF datetime to System.DateTime and vice-versa.
    // An Early Bound class generated for the WMI class.Win32_NetworkAdapterConfiguration
    public class NetworkAdapterConfiguration : System.ComponentModel.Component {
        
        // Private property to hold the WMI namespace in which the class resides.
        private static string CreatedWmiNamespace = "root\\CimV2";
        
        // Private property to hold the name of WMI class which created this class.
        private static string CreatedClassName = "Win32_NetworkAdapterConfiguration";
        
        // Private member variable to hold the ManagementScope which is used by the various methods.
        private static System.Management.ManagementScope statMgmtScope = null;
        
        private ManagementSystemProperties PrivateSystemProperties;
        
        // Underlying lateBound WMI object.
        private System.Management.ManagementObject PrivateLateBoundObject;
        
        // Member variable to store the 'automatic commit' behavior for the class.
        private bool AutoCommitProp;
        
        // Private variable to hold the embedded property representing the instance.
        private System.Management.ManagementBaseObject embeddedObj;
        
        // The current WMI object used
        private System.Management.ManagementBaseObject curObj;
        
        // Flag to indicate if the instance is an embedded object.
        private bool isEmbedded;
        
        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public NetworkAdapterConfiguration() {
            this.InitializeObject(null, null, null);
        }
        
        public NetworkAdapterConfiguration(uint keyIndex) {
            this.InitializeObject(null, new System.Management.ManagementPath(NetworkAdapterConfiguration.ConstructPath(keyIndex)), null);
        }
        
        public NetworkAdapterConfiguration(System.Management.ManagementScope mgmtScope, uint keyIndex) {
            this.InitializeObject(((System.Management.ManagementScope)(mgmtScope)), new System.Management.ManagementPath(NetworkAdapterConfiguration.ConstructPath(keyIndex)), null);
        }
        
        public NetworkAdapterConfiguration(System.Management.ManagementPath path, System.Management.ObjectGetOptions getOptions) {
            this.InitializeObject(null, path, getOptions);
        }
        
        public NetworkAdapterConfiguration(System.Management.ManagementScope mgmtScope, System.Management.ManagementPath path) {
            this.InitializeObject(mgmtScope, path, null);
        }
        
        public NetworkAdapterConfiguration(System.Management.ManagementPath path) {
            this.InitializeObject(null, path, null);
        }
        
        public NetworkAdapterConfiguration(System.Management.ManagementScope mgmtScope, System.Management.ManagementPath path, System.Management.ObjectGetOptions getOptions) {
            this.InitializeObject(mgmtScope, path, getOptions);
        }
        
        public NetworkAdapterConfiguration(System.Management.ManagementObject theObject) {
            Initialize();
            if ((CheckIfProperClass(theObject) == true)) {
                PrivateLateBoundObject = theObject;
                PrivateSystemProperties = new ManagementSystemProperties(PrivateLateBoundObject);
                curObj = PrivateLateBoundObject;
            }
            else {
                throw new System.ArgumentException("Class name does not match.");
            }
        }
        
        public NetworkAdapterConfiguration(System.Management.ManagementBaseObject theObject) {
            Initialize();
            if ((CheckIfProperClass(theObject) == true)) {
                embeddedObj = theObject;
                PrivateSystemProperties = new ManagementSystemProperties(theObject);
                curObj = embeddedObj;
                isEmbedded = true;
            }
            else {
                throw new System.ArgumentException("Class name does not match.");
            }
        }
        
        // Property returns the namespace of the WMI class.
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string OriginatingNamespace {
            get {
                return "root\\CimV2";
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ManagementClassName {
            get {
                string strRet = CreatedClassName;
                if ((curObj != null)) {
                    if ((curObj.ClassPath != null)) {
                        strRet = ((string)(curObj["__CLASS"]));
                        if (((strRet == null) 
                                    || (strRet == string.Empty))) {
                            strRet = CreatedClassName;
                        }
                    }
                }
                return strRet;
            }
        }
        
        // Property pointing to an embedded object to get System properties of the WMI object.
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ManagementSystemProperties SystemProperties {
            get {
                return PrivateSystemProperties;
            }
        }
        
        // Property returning the underlying lateBound object.
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Management.ManagementBaseObject LateBoundObject {
            get {
                return curObj;
            }
        }
        
        // ManagementScope of the object.
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Management.ManagementScope Scope {
            get {
                if ((isEmbedded == false)) {
                    return PrivateLateBoundObject.Scope;
                }
                else {
                    return null;
                }
            }
            set {
                if ((isEmbedded == false)) {
                    PrivateLateBoundObject.Scope = value;
                }
            }
        }
        
        // Property to show the commit behavior for the WMI object. If true, WMI object will be automatically saved after each property modification.(ie. Put() is called after modification of a property).
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool AutoCommit {
            get {
                return AutoCommitProp;
            }
            set {
                AutoCommitProp = value;
            }
        }
        
        // The ManagementPath of the underlying WMI object.
        [Browsable(true)]
        public System.Management.ManagementPath Path {
            get {
                if ((isEmbedded == false)) {
                    return PrivateLateBoundObject.Path;
                }
                else {
                    return null;
                }
            }
            set {
                if ((isEmbedded == false)) {
                    if ((CheckIfProperClass(null, value, null) != true)) {
                        throw new System.ArgumentException("Class name does not match.");
                    }
                    PrivateLateBoundObject.Path = value;
                }
            }
        }
        
        // Public static scope property which is used by the various methods.
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static System.Management.ManagementScope StaticScope {
            get {
                return statMgmtScope;
            }
            set {
                statMgmtScope = value;
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsArpAlwaysSourceRouteNull {
            get {
                if ((curObj["ArpAlwaysSourceRoute"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"The ArpAlwaysSourceRoute property indicates whether the Address Resolution Protocol (ARP) must always use source routing. If this property is TRUE, TCP/IP will transmit ARP queries with source routing enabled on Token Ring networks. By default, ARP first queries without source routing, and retries with source routing enabled if no reply was received. Source routing allows the routing of network packets across different types of networks. Default: FALSE.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool ArpAlwaysSourceRoute {
            get {
                if ((curObj["ArpAlwaysSourceRoute"] == null)) {
                    return System.Convert.ToBoolean(0);
                }
                return ((bool)(curObj["ArpAlwaysSourceRoute"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsArpUseEtherSNAPNull {
            get {
                if ((curObj["ArpUseEtherSNAP"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"The ArpUseEtherSNAP property indicates whether Ethernet packets follow the IEEE 802.3 Sub-Network Access Protocol (SNAP) encoding. Setting this parameter to 1 will force TCP/IP to transmit Ethernet packets using 802.3 SNAP encoding. By default, the stack transmits packets in DIX Ethernet format. Windows NT/Windows 2000 systems are able to receive both formats. Default: FALSE.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool ArpUseEtherSNAP {
            get {
                if ((curObj["ArpUseEtherSNAP"] == null)) {
                    return System.Convert.ToBoolean(0);
                }
                return ((bool)(curObj["ArpUseEtherSNAP"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("A short textual description (one-line string) of the CIM_Setting object.")]
        public string Caption {
            get {
                return ((string)(curObj["Caption"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"The DatabasePath property indicates a valid Windows file path to standard Internet database files (HOSTS, LMHOSTS, NETWORKS, PROTOCOLS).  The file path is used by the Windows Sockets interface. This property is only available on Windows NT/Windows 2000 systems.")]
        public string DatabasePath {
            get {
                return ((string)(curObj["DatabasePath"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDeadGWDetectEnabledNull {
            get {
                if ((curObj["DeadGWDetectEnabled"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"The DeadGWDetectEnabled property indicates whether dead gateway detection occurs. Setting this parameter to TRUE causes TCP to perform Dead Gateway Detection. With this feature enabled, TCP will ask IP to change to a backup gateway if it retransmits a segment several times without receiving a response. Default: TRUE.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool DeadGWDetectEnabled {
            get {
                if ((curObj["DeadGWDetectEnabled"] == null)) {
                    return System.Convert.ToBoolean(0);
                }
                return ((bool)(curObj["DeadGWDetectEnabled"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The DefaultIPGateway property contains a list of IP addresses of default gateways" +
            " used by the computer system.\nExample: 194.161.12.1 194.162.46.1")]
        public string[] DefaultIPGateway {
            get {
                return ((string[])(curObj["DefaultIPGateway"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDefaultTOSNull {
            get {
                if ((curObj["DefaultTOS"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The DefaultTOS property indicates the default Type Of Service (TOS) value set in " +
            "the header of outgoing IP packets. RFC 791 defines the values. Default: 0, Valid" +
            " Range: 0 - 255.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public byte DefaultTOS {
            get {
                if ((curObj["DefaultTOS"] == null)) {
                    return System.Convert.ToByte(0);
                }
                return ((byte)(curObj["DefaultTOS"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDefaultTTLNull {
            get {
                if ((curObj["DefaultTTL"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"The DefaultTTL property indicates the default Time To Live (TTL) value set in the header of outgoing IP packets. The TTL specifies the number of routers an IP packet may pass through to reach its destination before being discarded. Each router decrements the TTL count of a packet by one as it passes through and discards the packets if the TTL is 0. Default: 32, Valid Range: 1 - 255.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public byte DefaultTTL {
            get {
                if ((curObj["DefaultTTL"] == null)) {
                    return System.Convert.ToByte(0);
                }
                return ((byte)(curObj["DefaultTTL"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("A textual description of the CIM_Setting object.")]
        public string Description {
            get {
                return ((string)(curObj["Description"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDHCPEnabledNull {
            get {
                if ((curObj["DHCPEnabled"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The DHCPEnabled property indicates whether the dynamic host configuration protoco" +
            "l  (DHCP) server automatically assigns an IP address to the computer system when" +
            " establishing a network connection.\nValues: TRUE or FALSE. If TRUE, DHCP is enab" +
            "led.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool DHCPEnabled {
            get {
                if ((curObj["DHCPEnabled"] == null)) {
                    return System.Convert.ToBoolean(0);
                }
                return ((bool)(curObj["DHCPEnabled"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDHCPLeaseExpiresNull {
            get {
                if ((curObj["DHCPLeaseExpires"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The DHCPLeaseExpires property indicates the expiration date and time for a leased" +
            " IP address that was assigned to the computer by the dynamic host configuration " +
            "protocol (DHCP) server.\nExample: 20521201000230.000000000")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public System.DateTime DHCPLeaseExpires {
            get {
                if ((curObj["DHCPLeaseExpires"] != null)) {
                    return ToDateTime(((string)(curObj["DHCPLeaseExpires"])));
                }
                else {
                    return System.DateTime.MinValue;
                }
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDHCPLeaseObtainedNull {
            get {
                if ((curObj["DHCPLeaseObtained"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The DHCPLeaseObtained property indicates the date and time the lease was obtained" +
            " for the IP address assigned to the computer by the dynamic host configuration p" +
            "rotocol (DHCP) server. \nExample: 19521201000230.000000000")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public System.DateTime DHCPLeaseObtained {
            get {
                if ((curObj["DHCPLeaseObtained"] != null)) {
                    return ToDateTime(((string)(curObj["DHCPLeaseObtained"])));
                }
                else {
                    return System.DateTime.MinValue;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The DHCPServer property indicates the IP address of the dynamic host configuratio" +
            "n protocol (DHCP) server.\nExample: 154.55.34")]
        public string DHCPServer {
            get {
                return ((string)(curObj["DHCPServer"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"The DNSDomain property indicates an organization name followed by a period and an extension that indicates the type of organization, such as microsoft.com. The name can be any combination of the letters A through Z, the numerals 0 through 9, and the hyphen (-), plus the period (.) character used as a separator.
Example: microsoft.com")]
        public string DNSDomain {
            get {
                return ((string)(curObj["DNSDomain"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"The DNSDomainSuffixSearchOrder property specifies the DNS domain suffixes to be appended to the end of host names during name resolution. When attempting to resolve a fully qualified domain name (FQDN) from a host only name, the system will first append the local domain name. If this is not successful, the system will use the domain suffix list to create additional FQDNs in the order listed and query DNS servers for each.
Example: samples.microsoft.com example.microsoft.com")]
        public string[] DNSDomainSuffixSearchOrder {
            get {
                return ((string[])(curObj["DNSDomainSuffixSearchOrder"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDNSEnabledForWINSResolutionNull {
            get {
                if ((curObj["DNSEnabledForWINSResolution"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"The DNSEnabledForWINSResolution property indicates whether the Domain Name System (DNS) is enabled for name resolution over Windows Internet Naming Service (WINS) resolution. If the name cannot be resolved using DNS, the name request is forwarded to WINS for resolution.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool DNSEnabledForWINSResolution {
            get {
                if ((curObj["DNSEnabledForWINSResolution"] == null)) {
                    return System.Convert.ToBoolean(0);
                }
                return ((bool)(curObj["DNSEnabledForWINSResolution"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"The DNSHostName property indicates the host name used to identify the local computer for authentication by some utilities. Other TCP/IP-based utilities can use this value to acquire the name of the local computer. Host names are stored on DNS servers in a table that maps names to IP addresses for use by DNS. The name can be any combination of the letters A through Z, the numerals 0 through 9, and the hyphen (-), plus the period (.) character used as a separator. By default, this value is the Microsoft networking computer name, but the network administrator can assign another host name without affecting the computer name.
Example: corpdns")]
        public string DNSHostName {
            get {
                return ((string)(curObj["DNSHostName"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The DNSServerSearchOrder property indicates an ordered list of server IP addresse" +
            "s to be used in querying for DNS Servers.")]
        public string[] DNSServerSearchOrder {
            get {
                return ((string[])(curObj["DNSServerSearchOrder"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDomainDNSRegistrationEnabledNull {
            get {
                if ((curObj["DomainDNSRegistrationEnabled"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"The DomainDNSRegistrationEnabled property specifies whether the IP addresses for this connection are registered in DNS under the domain name of this connection, in addition to registering under the computer's full DNS name. The domain name of this connection is either set via the method SetDNSDomain() or assigned by DHCP. The registered name is the host name of the computer with the domain name appended. Windows 2000 only.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool DomainDNSRegistrationEnabled {
            get {
                if ((curObj["DomainDNSRegistrationEnabled"] == null)) {
                    return System.Convert.ToBoolean(0);
                }
                return ((bool)(curObj["DomainDNSRegistrationEnabled"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsForwardBufferMemoryNull {
            get {
                if ((curObj["ForwardBufferMemory"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"The ForwardBufferMemory property indicates how much memory IP allocates to store packet data in the router packet queue. When this buffer space is filled, the router begins discarding packets at random from its queue. Packet queue data buffers are 256 bytes in length, so the value of this parameter should be a multiple of 256. Multiple buffers are chained together for larger packets. The IP header for a packet is stored separately. This parameter is ignored and no buffers are allocated if the IP router is not enabled. The buffer size can range from the network MTU to the a value smaller than 0xFFFFFFFF. Default: 74240 (fifty 1480-byte packets, rounded to a multiple of 256).")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint ForwardBufferMemory {
            get {
                if ((curObj["ForwardBufferMemory"] == null)) {
                    return System.Convert.ToUInt32(0);
                }
                return ((uint)(curObj["ForwardBufferMemory"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsFullDNSRegistrationEnabledNull {
            get {
                if ((curObj["FullDNSRegistrationEnabled"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"The FullDNSRegistrationEnabled property specifies whether the IP addresses for this connection are registered in DNS under the computer's full DNS name. The full DNS name of the computer is displayed on the Network Identification tab of the System Control Panel. Windows 2000 only.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool FullDNSRegistrationEnabled {
            get {
                if ((curObj["FullDNSRegistrationEnabled"] == null)) {
                    return System.Convert.ToBoolean(0);
                }
                return ((bool)(curObj["FullDNSRegistrationEnabled"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The GatewayCostMetric reflects an integer cost metric (ranging from 1 to 9999) to" +
            " be used in calculating the fastest, most reliable, and/or least expensive route" +
            "s. This argument has a one to one correspondence with the DefaultIPGateway. Wind" +
            "ows 2000 only.")]
        public ushort[] GatewayCostMetric {
            get {
                return ((ushort[])(curObj["GatewayCostMetric"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsIGMPLevelNull {
            get {
                if ((curObj["IGMPLevel"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"The IGMPLevel property indicates the extent to which the system supports IP multicast and participates in the Internet Group Management Protocol. At level 0, the system provides no multicast support. At level 1, the system may only send IP multicast packets. At level 2, the system may send IP multicast packets and fully participate in IGMP to receive multicast packets. Default: 2")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public IGMPLevelValues IGMPLevel {
            get {
                if ((curObj["IGMPLevel"] == null)) {
                    return ((IGMPLevelValues)(System.Convert.ToInt32(3)));
                }
                return ((IGMPLevelValues)(System.Convert.ToInt32(curObj["IGMPLevel"])));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsIndexNull {
            get {
                if ((curObj["Index"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The Index property specifies the index number of the Win32 network adapter config" +
            "uration. The index number is used when there is more than one configuration avai" +
            "lable.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint Index {
            get {
                if ((curObj["Index"] == null)) {
                    return System.Convert.ToUInt32(0);
                }
                return ((uint)(curObj["Index"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsInterfaceIndexNull {
            get {
                if ((curObj["InterfaceIndex"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The InterfaceIndex property contains the index value that uniquely identifies the" +
            " local interface.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint InterfaceIndex {
            get {
                if ((curObj["InterfaceIndex"] == null)) {
                    return System.Convert.ToUInt32(0);
                }
                return ((uint)(curObj["InterfaceIndex"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The IPAddress property contains a list of all of the IP addresses associated with" +
            " the current network adapter.\nExample: 155.34.22.0")]
        public string[] IPAddress {
            get {
                return ((string[])(curObj["IPAddress"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsIPConnectionMetricNull {
            get {
                if ((curObj["IPConnectionMetric"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"The IPConnectionMetric indicates the cost of using the configured routes for this IP bound adapter and is the weighted value for those routes in the IP routing table. If there are multiple routes to a destination in the IP routing table, the route with the lowest metric is used. The default value is 1.Windows 2000 only.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint IPConnectionMetric {
            get {
                if ((curObj["IPConnectionMetric"] == null)) {
                    return System.Convert.ToUInt32(0);
                }
                return ((uint)(curObj["IPConnectionMetric"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsIPEnabledNull {
            get {
                if ((curObj["IPEnabled"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The IPEnabled property indicates whether TCP/IP is bound and enabled on this netw" +
            "ork adapt.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool IPEnabled {
            get {
                if ((curObj["IPEnabled"] == null)) {
                    return System.Convert.ToBoolean(0);
                }
                return ((bool)(curObj["IPEnabled"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsIPFilterSecurityEnabledNull {
            get {
                if ((curObj["IPFilterSecurityEnabled"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"The IPFilterSecurityEnabled property indicates whether IP port security is enabled globally across all IP-bound network adapters. This property is used in conjunction with IPSecPermitTCPPorts, IPSecPermitUDPPorts, and IPSecPermitIPProtocols. A value of TRUE indicates that IP port security is enabled and that the security values associated with individual network adapters are in effect. A value of FALSE indicates IP filter security is disabled across all network adapters and allows all port and protocol traffic to flow unfiltered.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool IPFilterSecurityEnabled {
            get {
                if ((curObj["IPFilterSecurityEnabled"] == null)) {
                    return System.Convert.ToBoolean(0);
                }
                return ((bool)(curObj["IPFilterSecurityEnabled"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsIPPortSecurityEnabledNull {
            get {
                if ((curObj["IPPortSecurityEnabled"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The IPPortSecurityEnabled property indicates whether IP port security is enabled " +
            "globally across all IP-bound network adapters. This property has been deprecated" +
            " in favor of IPFilterSecurityEnabled.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool IPPortSecurityEnabled {
            get {
                if ((curObj["IPPortSecurityEnabled"] == null)) {
                    return System.Convert.ToBoolean(0);
                }
                return ((bool)(curObj["IPPortSecurityEnabled"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"The IPSecPermitIPProtocols property lists the protocols permitted to run over the IP. The list of protocols is defined using the EnableIPSec method. The list will either be empty or contain numeric values. A numeric value of zero indicates access permission is granted for all protocols. An empty string indicates that no protocols are permitted to run when IPFilterSecurityEnabled is TRUE.")]
        public string[] IPSecPermitIPProtocols {
            get {
                return ((string[])(curObj["IPSecPermitIPProtocols"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"The IPSecPermitTCPPorts property lists the ports that will be granted access permission for TCP. The list of protocols is defined using the EnableIPSec method. The list will either be empty or contain numeric values. A numeric value of zero indicates access permission is granted for all ports. An empty string indicates that no ports are granted access permission when IPFilterSecurityEnabled is TRUE.")]
        public string[] IPSecPermitTCPPorts {
            get {
                return ((string[])(curObj["IPSecPermitTCPPorts"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"The IPSecPermitUDPPorts property lists the ports that will be granted User Datagram Protocol (UDP) access permission. The list of protocols is defined using the EnableIPSec method. The list will either be empty or contain numeric values. A numeric value of zero indicates access permission is granted for all ports. An empty string indicates that no ports are granted access permission when IPFilterSecurityEnabled is TRUE.")]
        public string[] IPSecPermitUDPPorts {
            get {
                return ((string[])(curObj["IPSecPermitUDPPorts"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The IPSubnet property contains a list of all the subnet masks associated with the" +
            " current network adapter.\nExample: 255.255.0")]
        public string[] IPSubnet {
            get {
                return ((string[])(curObj["IPSubnet"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsIPUseZeroBroadcastNull {
            get {
                if ((curObj["IPUseZeroBroadcast"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"The IPUseZeroBroadcast property indicates whether IP zeros-broadcasts are used. If this parameter is set TRUE, then IP uses zeros-broadcasts (0.0.0.0), and the system uses ones-broadcasts (255.255.255.255). Computer systems generally use ones-broadcasts, but those derived from BSD implementations use zeros-broadcasts. Systems that do not use that same broadcasts will not interoperate on the same network. Default: FALSE.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool IPUseZeroBroadcast {
            get {
                if ((curObj["IPUseZeroBroadcast"] == null)) {
                    return System.Convert.ToBoolean(0);
                }
                return ((bool)(curObj["IPUseZeroBroadcast"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The IPXAddress property indicates the Internetworking Packet Exchange (IPX) addre" +
            "ss of the network adapter. The IPX address identifies a computer system on a net" +
            "work using the IPX protocol.")]
        public string IPXAddress {
            get {
                return ((string)(curObj["IPXAddress"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsIPXEnabledNull {
            get {
                if ((curObj["IPXEnabled"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The IPXEnabled property determines whether the or Internetwork Packet Exchange (I" +
            "PX) protocol is bound and enabled for this adapter.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool IPXEnabled {
            get {
                if ((curObj["IPXEnabled"] == null)) {
                    return System.Convert.ToBoolean(0);
                }
                return ((bool)(curObj["IPXEnabled"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The IPXFrameType property represents an integer array of frame type identifiers. " +
            "The values in this array correspond to the elements in the IPXNetworkNumber.")]
        public IPXFrameTypeValues[] IPXFrameType {
            get {
                System.Array arrEnumVals = ((System.Array)(curObj["IPXFrameType"]));
                IPXFrameTypeValues[] enumToRet = new IPXFrameTypeValues[arrEnumVals.Length];
                int counter = 0;
                for (counter = 0; (counter < arrEnumVals.Length); counter = (counter + 1)) {
                    enumToRet[counter] = ((IPXFrameTypeValues)(System.Convert.ToInt32(arrEnumVals.GetValue(counter))));
                }
                return enumToRet;
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsIPXMediaTypeNull {
            get {
                if ((curObj["IPXMediaType"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The IPXMediaType property represents an Internetworking Packet Exchange (IPX) med" +
            "ia type identifier.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public IPXMediaTypeValues IPXMediaType {
            get {
                if ((curObj["IPXMediaType"] == null)) {
                    return ((IPXMediaTypeValues)(System.Convert.ToInt32(0)));
                }
                return ((IPXMediaTypeValues)(System.Convert.ToInt32(curObj["IPXMediaType"])));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"The IPXNetworkNumber property represents an array of characters that uniquely identifies a frame/network adapter combination on the computer system. The NetWare Link (NWLink) IPX/SPX-compatible transport in Windows 2000 and Windows NT 4.0 and greater uses two distinctly different types of network numbers. This number is sometimes referred to as the external network number. It must be unique for each network segment. The order in this string list will correspond item-for-item with the elements in the IPXFrameType property.")]
        public string[] IPXNetworkNumber {
            get {
                return ((string[])(curObj["IPXNetworkNumber"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"The IPXVirtualNetNumber property uniquely identifies the computer system on the network. It is represented in the form of an eight-character hexadecimal digit. Windows NT/2000 uses the virtual network number (also known as an internal network number) for internal routing.")]
        public string IPXVirtualNetNumber {
            get {
                return ((string)(curObj["IPXVirtualNetNumber"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsKeepAliveIntervalNull {
            get {
                if ((curObj["KeepAliveInterval"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"The KeepAliveInterval property indicates the interval separating Keep Alive Retransmissions until a response is received. Once a response is received, the delay until the next Keep Alive Transmission is again controlled by the value of KeepAliveTime. The connection will be aborted after the number of retransmissions specified by TcpMaxDataRetransmissions have gone unanswered. Default: 1000, Valid Range: 1 - 0xFFFFFFFF.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint KeepAliveInterval {
            get {
                if ((curObj["KeepAliveInterval"] == null)) {
                    return System.Convert.ToUInt32(0);
                }
                return ((uint)(curObj["KeepAliveInterval"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsKeepAliveTimeNull {
            get {
                if ((curObj["KeepAliveTime"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"The KeepAliveTime property indicates how often the TCP attempts to verify that an idle connection is still intact by sending a Keep Alive Packet. A remote system that is reachable will acknowledge the keep alive transmission. Keep Alive packets are not sent by default. This feature may be enabled in a connection by an application. Default: 7,200,000 (two hours)")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint KeepAliveTime {
            get {
                if ((curObj["KeepAliveTime"] == null)) {
                    return System.Convert.ToUInt32(0);
                }
                return ((uint)(curObj["KeepAliveTime"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The MACAddress property indicates the Media Access Control (MAC) address of the n" +
            "etwork adapter. A MAC address is assigned by the manufacturer to uniquely identi" +
            "fy the network adapter.\nExample: 00:80:C7:8F:6C:96")]
        public string MACAddress {
            get {
                return ((string)(curObj["MACAddress"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsMTUNull {
            get {
                if ((curObj["MTU"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"The MTU property overrides the default Maximum Transmission Unit (MTU) for a network interface. The MTU is the maximum packet size (including the transport header) that the transport will transmit over the underlying network. The IP datagram can span multiple packets. The range of this value spans the minimum packet size (68) to the MTU supported by the underlying network.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint MTU {
            get {
                if ((curObj["MTU"] == null)) {
                    return System.Convert.ToUInt32(0);
                }
                return ((uint)(curObj["MTU"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsNumForwardPacketsNull {
            get {
                if ((curObj["NumForwardPackets"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"The NumForwardPackets property indicates the number of IP packet headers allocated for the router packet queue. When all headers are in use, the router will begin to discard packets from the queue at random. This value should be at least as large as the ForwardBufferMemory value divided by the maximum IP data size of the networks connected to the router. It should be no larger than the ForwardBufferMemory value divided by 256, since at least 256 bytes of forward buffer memory are used for each packet. The optimal number of forward packets for a given ForwardBufferMemory size depends on the type of traffic carried on the network. It will lie somewhere between these two values. If the router is not enabled, this parameter is ignored and no headers are allocated. Default: 50, Valid Range: 1 - 0xFFFFFFFE.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint NumForwardPackets {
            get {
                if ((curObj["NumForwardPackets"] == null)) {
                    return System.Convert.ToUInt32(0);
                }
                return ((uint)(curObj["NumForwardPackets"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsPMTUBHDetectEnabledNull {
            get {
                if ((curObj["PMTUBHDetectEnabled"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"The PMTUBHDetectEnabled property indicates whether detection of black hole routers occurs. Setting this parameter to TRUE causes TCP to try to detect black hole routers while discovering the path of the Maximum Transmission Unit. A black hole router does not return ICMP Destination Unreachable messages when it needs to fragment an IP datagram with the Don't Fragment bit set. TCP depends on receiving these messages to perform Path MTU Discovery. With this feature enabled, TCP will try to send segments without the Don't Fragment bit set if several retransmissions of a segment go unacknowledged. If the segment is acknowledged as a result, the MSS will be decreased and the Don't Fragment bit will be set in future packets on the connection. Enabling black hole detection increases the maximum number of retransmissions performed for a given segment. The default value of this property is FALSE.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool PMTUBHDetectEnabled {
            get {
                if ((curObj["PMTUBHDetectEnabled"] == null)) {
                    return System.Convert.ToBoolean(0);
                }
                return ((bool)(curObj["PMTUBHDetectEnabled"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsPMTUDiscoveryEnabledNull {
            get {
                if ((curObj["PMTUDiscoveryEnabled"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"The PMTUDiscoveryEnabled property indicates whether the Maximum Transmission Unit (MTU) path is discovered. Setting this parameter to TRUE causes TCP to attempt to discover the MTU (the largest packet size) over the path to a remote host. By discovering the MTU path and limiting TCP segments to this size, TCP can eliminate fragmentation at routers along the path that connect networks with different MTUs. Fragmentation adversely affects TCP throughput and network congestion. Setting this parameter to FALSE causes an MTU of 576 bytes to be used for all connections that are not to machines on the local subnet. Default: TRUE.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool PMTUDiscoveryEnabled {
            get {
                if ((curObj["PMTUDiscoveryEnabled"] == null)) {
                    return System.Convert.ToBoolean(0);
                }
                return ((bool)(curObj["PMTUDiscoveryEnabled"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The ServiceName property indicates the service name of the network adapter. This " +
            "name is usually shorter than the full product name. \nExample: Elnkii.")]
        public string ServiceName {
            get {
                return ((string)(curObj["ServiceName"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The identifier by which the CIM_Setting object is known.")]
        public string SettingID {
            get {
                return ((string)(curObj["SettingID"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsTcpipNetbiosOptionsNull {
            get {
                if ((curObj["TcpipNetbiosOptions"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The TcpipNetbiosOptions property specifies a bitmap of the possible settings rela" +
            "ted to NetBIOS over TCP/IP. Windows 2000 only.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public TcpipNetbiosOptionsValues TcpipNetbiosOptions {
            get {
                if ((curObj["TcpipNetbiosOptions"] == null)) {
                    return ((TcpipNetbiosOptionsValues)(System.Convert.ToInt32(3)));
                }
                return ((TcpipNetbiosOptionsValues)(System.Convert.ToInt32(curObj["TcpipNetbiosOptions"])));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsTcpMaxConnectRetransmissionsNull {
            get {
                if ((curObj["TcpMaxConnectRetransmissions"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"The TcpMaxConnectRetransmissions property indicates the number of times TCP will attempt to retransmit a Connect Request before terminating the connection. The initial retransmission timeout is 3 seconds. The retransmission timeout doubles for each attempt. Default: 3, Valid Range: 0 - 0xFFFFFFFF.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint TcpMaxConnectRetransmissions {
            get {
                if ((curObj["TcpMaxConnectRetransmissions"] == null)) {
                    return System.Convert.ToUInt32(0);
                }
                return ((uint)(curObj["TcpMaxConnectRetransmissions"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsTcpMaxDataRetransmissionsNull {
            get {
                if ((curObj["TcpMaxDataRetransmissions"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"The TcpMaxDataRetransmissions property indicates the number of times TCP will retransmit an individual data segment (non-connect segment) before terminating the connection. The retransmission timeout doubles with each successive retransmission on a connection. Default: 5, Valid Range: 0 - 0xFFFFFFFF.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint TcpMaxDataRetransmissions {
            get {
                if ((curObj["TcpMaxDataRetransmissions"] == null)) {
                    return System.Convert.ToUInt32(0);
                }
                return ((uint)(curObj["TcpMaxDataRetransmissions"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsTcpNumConnectionsNull {
            get {
                if ((curObj["TcpNumConnections"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The TcpNumConnections property indicates the maximum number of connections that T" +
            "CP can have open simultaneously. Default: 0xFFFFFE, Valid Range: 0 - 0xFFFFFE.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint TcpNumConnections {
            get {
                if ((curObj["TcpNumConnections"] == null)) {
                    return System.Convert.ToUInt32(0);
                }
                return ((uint)(curObj["TcpNumConnections"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsTcpUseRFC1122UrgentPointerNull {
            get {
                if ((curObj["TcpUseRFC1122UrgentPointer"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"The TcpUseRFC1122UrgentPointer property indicates whether TCP uses the RFC 1122 specification or the mode used by Berkeley Software Design (BSD) derived systems, for urgent data. The two mechanisms interpret the urgent pointer differently and are not interoperable. Windows 2000 and Windows NT version 3.51 and higher defaults to BSD mode. If TRUE, urgent data is sent in RFC 1122 mode. Default: FALSE.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool TcpUseRFC1122UrgentPointer {
            get {
                if ((curObj["TcpUseRFC1122UrgentPointer"] == null)) {
                    return System.Convert.ToBoolean(0);
                }
                return ((bool)(curObj["TcpUseRFC1122UrgentPointer"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsTcpWindowSizeNull {
            get {
                if ((curObj["TcpWindowSize"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"The TcpWindowSize property contains the maximum TCP Receive  Window size offered by the system. The Receive Window specifies the number of bytes a sender may transmit without receiving an acknowledgment. In general, larger receiving windows will improve performance over high delay and high bandwidth networks. For efficiency, the receiving window should be an even multiple of the TCP Maximum Segment Size (MSS). Default: Four times the maximum TCP data size or an even multiple of TCP data size rounded up to the nearest multiple of 8192. Ethernet networks default to 8760. Valid Range: 0 - 65535.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ushort TcpWindowSize {
            get {
                if ((curObj["TcpWindowSize"] == null)) {
                    return System.Convert.ToUInt16(0);
                }
                return ((ushort)(curObj["TcpWindowSize"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsWINSEnableLMHostsLookupNull {
            get {
                if ((curObj["WINSEnableLMHostsLookup"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The WINSEnableLMHostsLookup property indicates whether local lookup files are use" +
            "d. Lookup files will contain a map of IP addresses to host names. If they exist " +
            "on the local system, they will be found in %SystemRoot%\\system32\\drivers\\etc.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool WINSEnableLMHostsLookup {
            get {
                if ((curObj["WINSEnableLMHostsLookup"] == null)) {
                    return System.Convert.ToBoolean(0);
                }
                return ((bool)(curObj["WINSEnableLMHostsLookup"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"The WINSHostLookupFile property contains a path to a WINS lookup file on the local system. This file will contain a map of IP addresses to host names. If the file specified in this property is found, it will be copied to the %SystemRoot%\system32\drivers\etc folder of the local system. Valid only if the WINSEnableLMHostsLookup property is TRUE.")]
        public string WINSHostLookupFile {
            get {
                return ((string)(curObj["WINSHostLookupFile"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The WINSPrimaryServer property indicates the IP address for the primary WINS serv" +
            "er. ")]
        public string WINSPrimaryServer {
            get {
                return ((string)(curObj["WINSPrimaryServer"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"The WINSScopeID property provides a way to isolate a group of computer systems that communicate with each other only. The Scope ID is a character string value that is appended to the end of the NetBIOS name. It is used for all NetBIOS transactions  over TCP/IP communications from that computer system. Computers configured with identical Scope IDs are able to communicate with this computer. TCP/IP clients with different Scope IDs disregard packets from computers with this Scope ID. Valid only when the EnableWINS method executes successfully.")]
        public string WINSScopeID {
            get {
                return ((string)(curObj["WINSScopeID"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The WINSSecondaryServer property indicates the IP address for the secondary WINS " +
            "server. ")]
        public string WINSSecondaryServer {
            get {
                return ((string)(curObj["WINSSecondaryServer"]));
            }
        }
        
        private bool CheckIfProperClass(System.Management.ManagementScope mgmtScope, System.Management.ManagementPath path, System.Management.ObjectGetOptions OptionsParam) {
            if (((path != null) 
                        && (string.Compare(path.ClassName, this.ManagementClassName, true, System.Globalization.CultureInfo.InvariantCulture) == 0))) {
                return true;
            }
            else {
                return CheckIfProperClass(new System.Management.ManagementObject(mgmtScope, path, OptionsParam));
            }
        }
        
        private bool CheckIfProperClass(System.Management.ManagementBaseObject theObj) {
            if (((theObj != null) 
                        && (string.Compare(((string)(theObj["__CLASS"])), this.ManagementClassName, true, System.Globalization.CultureInfo.InvariantCulture) == 0))) {
                return true;
            }
            else {
                System.Array parentClasses = ((System.Array)(theObj["__DERIVATION"]));
                if ((parentClasses != null)) {
                    int count = 0;
                    for (count = 0; (count < parentClasses.Length); count = (count + 1)) {
                        if ((string.Compare(((string)(parentClasses.GetValue(count))), this.ManagementClassName, true, System.Globalization.CultureInfo.InvariantCulture) == 0)) {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        
        private bool ShouldSerializeArpAlwaysSourceRoute() {
            if ((this.IsArpAlwaysSourceRouteNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeArpUseEtherSNAP() {
            if ((this.IsArpUseEtherSNAPNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeDeadGWDetectEnabled() {
            if ((this.IsDeadGWDetectEnabledNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeDefaultTOS() {
            if ((this.IsDefaultTOSNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeDefaultTTL() {
            if ((this.IsDefaultTTLNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeDHCPEnabled() {
            if ((this.IsDHCPEnabledNull == false)) {
                return true;
            }
            return false;
        }
        
        // Converts a given datetime in DMTF format to System.DateTime object.
        static System.DateTime ToDateTime(string dmtfDate) {
            System.DateTime initializer = System.DateTime.MinValue;
            int year = initializer.Year;
            int month = initializer.Month;
            int day = initializer.Day;
            int hour = initializer.Hour;
            int minute = initializer.Minute;
            int second = initializer.Second;
            long ticks = 0;
            string dmtf = dmtfDate;
            System.DateTime datetime = System.DateTime.MinValue;
            string tempString = string.Empty;
            if ((dmtf == null)) {
                throw new System.ArgumentOutOfRangeException();
            }
            if ((dmtf.Length == 0)) {
                throw new System.ArgumentOutOfRangeException();
            }
            if ((dmtf.Length != 25)) {
                throw new System.ArgumentOutOfRangeException();
            }
            try {
                tempString = dmtf.Substring(0, 4);
                if (("****" != tempString)) {
                    year = int.Parse(tempString);
                }
                tempString = dmtf.Substring(4, 2);
                if (("**" != tempString)) {
                    month = int.Parse(tempString);
                }
                tempString = dmtf.Substring(6, 2);
                if (("**" != tempString)) {
                    day = int.Parse(tempString);
                }
                tempString = dmtf.Substring(8, 2);
                if (("**" != tempString)) {
                    hour = int.Parse(tempString);
                }
                tempString = dmtf.Substring(10, 2);
                if (("**" != tempString)) {
                    minute = int.Parse(tempString);
                }
                tempString = dmtf.Substring(12, 2);
                if (("**" != tempString)) {
                    second = int.Parse(tempString);
                }
                tempString = dmtf.Substring(15, 6);
                if (("******" != tempString)) {
                    ticks = (long.Parse(tempString) * ((long)((System.TimeSpan.TicksPerMillisecond / 1000))));
                }
                if (((((((((year < 0) 
                            || (month < 0)) 
                            || (day < 0)) 
                            || (hour < 0)) 
                            || (minute < 0)) 
                            || (minute < 0)) 
                            || (second < 0)) 
                            || (ticks < 0))) {
                    throw new System.ArgumentOutOfRangeException();
                }
            }
            catch (System.Exception e) {
                throw new System.ArgumentOutOfRangeException(null, e.Message);
            }
            datetime = new System.DateTime(year, month, day, hour, minute, second, 0);
            datetime = datetime.AddTicks(ticks);
            System.TimeSpan tickOffset = System.TimeZone.CurrentTimeZone.GetUtcOffset(datetime);
            int UTCOffset = 0;
            int OffsetToBeAdjusted = 0;
            long OffsetMins = ((long)((tickOffset.Ticks / System.TimeSpan.TicksPerMinute)));
            tempString = dmtf.Substring(22, 3);
            if ((tempString != "******")) {
                tempString = dmtf.Substring(21, 4);
                try {
                    UTCOffset = int.Parse(tempString);
                }
                catch (System.Exception e) {
                    throw new System.ArgumentOutOfRangeException(null, e.Message);
                }
                OffsetToBeAdjusted = ((int)((OffsetMins - UTCOffset)));
                datetime = datetime.AddMinutes(((double)(OffsetToBeAdjusted)));
            }
            return datetime;
        }
        
        // Converts a given System.DateTime object to DMTF datetime format.
        static string ToDmtfDateTime(System.DateTime date) {
            string utcString = string.Empty;
            System.TimeSpan tickOffset = System.TimeZone.CurrentTimeZone.GetUtcOffset(date);
            long OffsetMins = ((long)((tickOffset.Ticks / System.TimeSpan.TicksPerMinute)));
            if ((System.Math.Abs(OffsetMins) > 999)) {
                date = date.ToUniversalTime();
                utcString = "+000";
            }
            else {
                if ((tickOffset.Ticks >= 0)) {
                    utcString = string.Concat("+", ((long)((tickOffset.Ticks / System.TimeSpan.TicksPerMinute))).ToString().PadLeft(3, '0'));
                }
                else {
                    string strTemp = ((long)(OffsetMins)).ToString();
                    utcString = string.Concat("-", strTemp.Substring(1, (strTemp.Length - 1)).PadLeft(3, '0'));
                }
            }
            string dmtfDateTime = ((int)(date.Year)).ToString().PadLeft(4, '0');
            dmtfDateTime = string.Concat(dmtfDateTime, ((int)(date.Month)).ToString().PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, ((int)(date.Day)).ToString().PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, ((int)(date.Hour)).ToString().PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, ((int)(date.Minute)).ToString().PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, ((int)(date.Second)).ToString().PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, ".");
            System.DateTime dtTemp = new System.DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, 0);
            long microsec = ((long)((((date.Ticks - dtTemp.Ticks) 
                        * 1000) 
                        / System.TimeSpan.TicksPerMillisecond)));
            string strMicrosec = ((long)(microsec)).ToString();
            if ((strMicrosec.Length > 6)) {
                strMicrosec = strMicrosec.Substring(0, 6);
            }
            dmtfDateTime = string.Concat(dmtfDateTime, strMicrosec.PadLeft(6, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, utcString);
            return dmtfDateTime;
        }
        
        private bool ShouldSerializeDHCPLeaseExpires() {
            if ((this.IsDHCPLeaseExpiresNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeDHCPLeaseObtained() {
            if ((this.IsDHCPLeaseObtainedNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeDNSEnabledForWINSResolution() {
            if ((this.IsDNSEnabledForWINSResolutionNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeDomainDNSRegistrationEnabled() {
            if ((this.IsDomainDNSRegistrationEnabledNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeForwardBufferMemory() {
            if ((this.IsForwardBufferMemoryNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeFullDNSRegistrationEnabled() {
            if ((this.IsFullDNSRegistrationEnabledNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeIGMPLevel() {
            if ((this.IsIGMPLevelNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeIndex() {
            if ((this.IsIndexNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeInterfaceIndex() {
            if ((this.IsInterfaceIndexNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeIPConnectionMetric() {
            if ((this.IsIPConnectionMetricNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeIPEnabled() {
            if ((this.IsIPEnabledNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeIPFilterSecurityEnabled() {
            if ((this.IsIPFilterSecurityEnabledNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeIPPortSecurityEnabled() {
            if ((this.IsIPPortSecurityEnabledNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeIPUseZeroBroadcast() {
            if ((this.IsIPUseZeroBroadcastNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeIPXEnabled() {
            if ((this.IsIPXEnabledNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeIPXMediaType() {
            if ((this.IsIPXMediaTypeNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeKeepAliveInterval() {
            if ((this.IsKeepAliveIntervalNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeKeepAliveTime() {
            if ((this.IsKeepAliveTimeNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeMTU() {
            if ((this.IsMTUNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeNumForwardPackets() {
            if ((this.IsNumForwardPacketsNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializePMTUBHDetectEnabled() {
            if ((this.IsPMTUBHDetectEnabledNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializePMTUDiscoveryEnabled() {
            if ((this.IsPMTUDiscoveryEnabledNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeTcpipNetbiosOptions() {
            if ((this.IsTcpipNetbiosOptionsNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeTcpMaxConnectRetransmissions() {
            if ((this.IsTcpMaxConnectRetransmissionsNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeTcpMaxDataRetransmissions() {
            if ((this.IsTcpMaxDataRetransmissionsNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeTcpNumConnections() {
            if ((this.IsTcpNumConnectionsNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeTcpUseRFC1122UrgentPointer() {
            if ((this.IsTcpUseRFC1122UrgentPointerNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeTcpWindowSize() {
            if ((this.IsTcpWindowSizeNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeWINSEnableLMHostsLookup() {
            if ((this.IsWINSEnableLMHostsLookupNull == false)) {
                return true;
            }
            return false;
        }
        
        [Browsable(true)]
        public void CommitObject() {
            if ((isEmbedded == false)) {
                PrivateLateBoundObject.Put();
            }
        }
        
        [Browsable(true)]
        public void CommitObject(System.Management.PutOptions putOptions) {
            if ((isEmbedded == false)) {
                PrivateLateBoundObject.Put(putOptions);
            }
        }
        
        private void Initialize() {
            AutoCommitProp = true;
            isEmbedded = false;
        }
        
        private static string ConstructPath(uint keyIndex) {
            string strPath = "root\\CimV2:Win32_NetworkAdapterConfiguration";
            strPath = string.Concat(strPath, string.Concat(".Index=", ((uint)(keyIndex)).ToString()));
            return strPath;
        }
        
        private void InitializeObject(System.Management.ManagementScope mgmtScope, System.Management.ManagementPath path, System.Management.ObjectGetOptions getOptions) {
            Initialize();
            if ((path != null)) {
                if ((CheckIfProperClass(mgmtScope, path, getOptions) != true)) {
                    throw new System.ArgumentException("Class name does not match.");
                }
            }
            PrivateLateBoundObject = new System.Management.ManagementObject(mgmtScope, path, getOptions);
            PrivateSystemProperties = new ManagementSystemProperties(PrivateLateBoundObject);
            curObj = PrivateLateBoundObject;
        }
        
        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static NetworkAdapterConfigurationCollection GetInstances() {
            return GetInstances(null, null, null);
        }
        
        public static NetworkAdapterConfigurationCollection GetInstances(string condition) {
            return GetInstances(null, condition, null);
        }
        
        public static NetworkAdapterConfigurationCollection GetInstances(string[] selectedProperties) {
            return GetInstances(null, null, selectedProperties);
        }
        
        public static NetworkAdapterConfigurationCollection GetInstances(string condition, string[] selectedProperties) {
            return GetInstances(null, condition, selectedProperties);
        }
        
        public static NetworkAdapterConfigurationCollection GetInstances(System.Management.ManagementScope mgmtScope, System.Management.EnumerationOptions enumOptions) {
            if ((mgmtScope == null)) {
                if ((statMgmtScope == null)) {
                    mgmtScope = new System.Management.ManagementScope();
                    mgmtScope.Path.NamespacePath = "root\\CimV2";
                }
                else {
                    mgmtScope = statMgmtScope;
                }
            }
            System.Management.ManagementPath pathObj = new System.Management.ManagementPath();
            pathObj.ClassName = "Win32_NetworkAdapterConfiguration";
            pathObj.NamespacePath = "root\\CimV2";
            System.Management.ManagementClass clsObject = new System.Management.ManagementClass(mgmtScope, pathObj, null);
            if ((enumOptions == null)) {
                enumOptions = new System.Management.EnumerationOptions();
                enumOptions.EnsureLocatable = true;
            }
            return new NetworkAdapterConfigurationCollection(clsObject.GetInstances(enumOptions));
        }
        
        public static NetworkAdapterConfigurationCollection GetInstances(System.Management.ManagementScope mgmtScope, string condition) {
            return GetInstances(mgmtScope, condition, null);
        }
        
        public static NetworkAdapterConfigurationCollection GetInstances(System.Management.ManagementScope mgmtScope, string[] selectedProperties) {
            return GetInstances(mgmtScope, null, selectedProperties);
        }
        
        public static NetworkAdapterConfigurationCollection GetInstances(System.Management.ManagementScope mgmtScope, string condition, string[] selectedProperties) {
            if ((mgmtScope == null)) {
                if ((statMgmtScope == null)) {
                    mgmtScope = new System.Management.ManagementScope();
                    mgmtScope.Path.NamespacePath = "root\\CimV2";
                }
                else {
                    mgmtScope = statMgmtScope;
                }
            }
            System.Management.ManagementObjectSearcher ObjectSearcher = new System.Management.ManagementObjectSearcher(mgmtScope, new SelectQuery("Win32_NetworkAdapterConfiguration", condition, selectedProperties));
            System.Management.EnumerationOptions enumOptions = new System.Management.EnumerationOptions();
            enumOptions.EnsureLocatable = true;
            ObjectSearcher.Options = enumOptions;
            return new NetworkAdapterConfigurationCollection(ObjectSearcher.Get());
        }
        
        [Browsable(true)]
        public static NetworkAdapterConfiguration CreateInstance() {
            System.Management.ManagementScope mgmtScope = null;
            if ((statMgmtScope == null)) {
                mgmtScope = new System.Management.ManagementScope();
                mgmtScope.Path.NamespacePath = CreatedWmiNamespace;
            }
            else {
                mgmtScope = statMgmtScope;
            }
            System.Management.ManagementPath mgmtPath = new System.Management.ManagementPath(CreatedClassName);
            System.Management.ManagementClass tmpMgmtClass = new System.Management.ManagementClass(mgmtScope, mgmtPath, null);
            return new NetworkAdapterConfiguration(tmpMgmtClass.CreateInstance());
        }
        
        [Browsable(true)]
        public void Delete() {
            PrivateLateBoundObject.Delete();
        }
        
        public uint DisableIPSec() {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("DisableIPSec", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public uint EnableDHCP() {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("EnableDHCP", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public static uint EnableDNS(string DNSDomain, string[] DNSDomainSuffixSearchOrder, string DNSHostName, string[] DNSServerSearchOrder) {
            bool IsMethodStatic = true;
            if ((IsMethodStatic == true)) {
                System.Management.ManagementBaseObject inParams = null;
                System.Management.ManagementPath mgmtPath = new System.Management.ManagementPath(CreatedClassName);
                System.Management.ManagementClass classObj = new System.Management.ManagementClass(statMgmtScope, mgmtPath, null);
                inParams = classObj.GetMethodParameters("EnableDNS");
                inParams["DNSDomain"] = ((string)(DNSDomain));
                inParams["DNSDomainSuffixSearchOrder"] = ((string[])(DNSDomainSuffixSearchOrder));
                inParams["DNSHostName"] = ((string)(DNSHostName));
                inParams["DNSServerSearchOrder"] = ((string[])(DNSServerSearchOrder));
                System.Management.ManagementBaseObject outParams = classObj.InvokeMethod("EnableDNS", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public static uint EnableIPFilterSec(bool IPFilterSecurityEnabled) {
            bool IsMethodStatic = true;
            if ((IsMethodStatic == true)) {
                System.Management.ManagementBaseObject inParams = null;
                System.Management.ManagementPath mgmtPath = new System.Management.ManagementPath(CreatedClassName);
                System.Management.ManagementClass classObj = new System.Management.ManagementClass(statMgmtScope, mgmtPath, null);
                inParams = classObj.GetMethodParameters("EnableIPFilterSec");
                inParams["IPFilterSecurityEnabled"] = ((bool)(IPFilterSecurityEnabled));
                System.Management.ManagementBaseObject outParams = classObj.InvokeMethod("EnableIPFilterSec", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public uint EnableIPSec(string[] IPSecPermitIPProtocols, string[] IPSecPermitTCPPorts, string[] IPSecPermitUDPPorts) {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                inParams = PrivateLateBoundObject.GetMethodParameters("EnableIPSec");
                inParams["IPSecPermitIPProtocols"] = ((string[])(IPSecPermitIPProtocols));
                inParams["IPSecPermitTCPPorts"] = ((string[])(IPSecPermitTCPPorts));
                inParams["IPSecPermitUDPPorts"] = ((string[])(IPSecPermitUDPPorts));
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("EnableIPSec", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public uint EnableStatic(string[] IPAddress, string[] SubnetMask) {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                inParams = PrivateLateBoundObject.GetMethodParameters("EnableStatic");
                inParams["IPAddress"] = ((string[])(IPAddress));
                inParams["SubnetMask"] = ((string[])(SubnetMask));
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("EnableStatic", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public static uint EnableWINS(bool DNSEnabledForWINSResolution, bool WINSEnableLMHostsLookup, string WINSHostLookupFile, string WINSScopeID) {
            bool IsMethodStatic = true;
            if ((IsMethodStatic == true)) {
                System.Management.ManagementBaseObject inParams = null;
                System.Management.ManagementPath mgmtPath = new System.Management.ManagementPath(CreatedClassName);
                System.Management.ManagementClass classObj = new System.Management.ManagementClass(statMgmtScope, mgmtPath, null);
                inParams = classObj.GetMethodParameters("EnableWINS");
                inParams["DNSEnabledForWINSResolution"] = ((bool)(DNSEnabledForWINSResolution));
                inParams["WINSEnableLMHostsLookup"] = ((bool)(WINSEnableLMHostsLookup));
                inParams["WINSHostLookupFile"] = ((string)(WINSHostLookupFile));
                inParams["WINSScopeID"] = ((string)(WINSScopeID));
                System.Management.ManagementBaseObject outParams = classObj.InvokeMethod("EnableWINS", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public uint ReleaseDHCPLease() {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("ReleaseDHCPLease", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public static uint ReleaseDHCPLeaseAll() {
            bool IsMethodStatic = true;
            if ((IsMethodStatic == true)) {
                System.Management.ManagementBaseObject inParams = null;
                System.Management.ManagementPath mgmtPath = new System.Management.ManagementPath(CreatedClassName);
                System.Management.ManagementClass classObj = new System.Management.ManagementClass(statMgmtScope, mgmtPath, null);
                System.Management.ManagementBaseObject outParams = classObj.InvokeMethod("ReleaseDHCPLeaseAll", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public uint RenewDHCPLease() {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("RenewDHCPLease", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public static uint RenewDHCPLeaseAll() {
            bool IsMethodStatic = true;
            if ((IsMethodStatic == true)) {
                System.Management.ManagementBaseObject inParams = null;
                System.Management.ManagementPath mgmtPath = new System.Management.ManagementPath(CreatedClassName);
                System.Management.ManagementClass classObj = new System.Management.ManagementClass(statMgmtScope, mgmtPath, null);
                System.Management.ManagementBaseObject outParams = classObj.InvokeMethod("RenewDHCPLeaseAll", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public static uint SetArpAlwaysSourceRoute(bool ArpAlwaysSourceRoute) {
            bool IsMethodStatic = true;
            if ((IsMethodStatic == true)) {
                System.Management.ManagementBaseObject inParams = null;
                System.Management.ManagementPath mgmtPath = new System.Management.ManagementPath(CreatedClassName);
                System.Management.ManagementClass classObj = new System.Management.ManagementClass(statMgmtScope, mgmtPath, null);
                inParams = classObj.GetMethodParameters("SetArpAlwaysSourceRoute");
                inParams["ArpAlwaysSourceRoute"] = ((bool)(ArpAlwaysSourceRoute));
                System.Management.ManagementBaseObject outParams = classObj.InvokeMethod("SetArpAlwaysSourceRoute", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public static uint SetArpUseEtherSNAP(bool ArpUseEtherSNAP) {
            bool IsMethodStatic = true;
            if ((IsMethodStatic == true)) {
                System.Management.ManagementBaseObject inParams = null;
                System.Management.ManagementPath mgmtPath = new System.Management.ManagementPath(CreatedClassName);
                System.Management.ManagementClass classObj = new System.Management.ManagementClass(statMgmtScope, mgmtPath, null);
                inParams = classObj.GetMethodParameters("SetArpUseEtherSNAP");
                inParams["ArpUseEtherSNAP"] = ((bool)(ArpUseEtherSNAP));
                System.Management.ManagementBaseObject outParams = classObj.InvokeMethod("SetArpUseEtherSNAP", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public static uint SetDatabasePath(string DatabasePath) {
            bool IsMethodStatic = true;
            if ((IsMethodStatic == true)) {
                System.Management.ManagementBaseObject inParams = null;
                System.Management.ManagementPath mgmtPath = new System.Management.ManagementPath(CreatedClassName);
                System.Management.ManagementClass classObj = new System.Management.ManagementClass(statMgmtScope, mgmtPath, null);
                inParams = classObj.GetMethodParameters("SetDatabasePath");
                inParams["DatabasePath"] = ((string)(DatabasePath));
                System.Management.ManagementBaseObject outParams = classObj.InvokeMethod("SetDatabasePath", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public static uint SetDeadGWDetect(bool DeadGWDetectEnabled) {
            bool IsMethodStatic = true;
            if ((IsMethodStatic == true)) {
                System.Management.ManagementBaseObject inParams = null;
                System.Management.ManagementPath mgmtPath = new System.Management.ManagementPath(CreatedClassName);
                System.Management.ManagementClass classObj = new System.Management.ManagementClass(statMgmtScope, mgmtPath, null);
                inParams = classObj.GetMethodParameters("SetDeadGWDetect");
                inParams["DeadGWDetectEnabled"] = ((bool)(DeadGWDetectEnabled));
                System.Management.ManagementBaseObject outParams = classObj.InvokeMethod("SetDeadGWDetect", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public static uint SetDefaultTOS(byte DefaultTOS) {
            bool IsMethodStatic = true;
            if ((IsMethodStatic == true)) {
                System.Management.ManagementBaseObject inParams = null;
                System.Management.ManagementPath mgmtPath = new System.Management.ManagementPath(CreatedClassName);
                System.Management.ManagementClass classObj = new System.Management.ManagementClass(statMgmtScope, mgmtPath, null);
                inParams = classObj.GetMethodParameters("SetDefaultTOS");
                inParams["DefaultTOS"] = ((byte)(DefaultTOS));
                System.Management.ManagementBaseObject outParams = classObj.InvokeMethod("SetDefaultTOS", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public static uint SetDefaultTTL(byte DefaultTTL) {
            bool IsMethodStatic = true;
            if ((IsMethodStatic == true)) {
                System.Management.ManagementBaseObject inParams = null;
                System.Management.ManagementPath mgmtPath = new System.Management.ManagementPath(CreatedClassName);
                System.Management.ManagementClass classObj = new System.Management.ManagementClass(statMgmtScope, mgmtPath, null);
                inParams = classObj.GetMethodParameters("SetDefaultTTL");
                inParams["DefaultTTL"] = ((byte)(DefaultTTL));
                System.Management.ManagementBaseObject outParams = classObj.InvokeMethod("SetDefaultTTL", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public uint SetDNSDomain(string DNSDomain) {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                inParams = PrivateLateBoundObject.GetMethodParameters("SetDNSDomain");
                inParams["DNSDomain"] = ((string)(DNSDomain));
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("SetDNSDomain", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public uint SetDNSServerSearchOrder(string[] DNSServerSearchOrder) {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                inParams = PrivateLateBoundObject.GetMethodParameters("SetDNSServerSearchOrder");
                inParams["DNSServerSearchOrder"] = ((string[])(DNSServerSearchOrder));
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("SetDNSServerSearchOrder", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public static uint SetDNSSuffixSearchOrder(string[] DNSDomainSuffixSearchOrder) {
            bool IsMethodStatic = true;
            if ((IsMethodStatic == true)) {
                System.Management.ManagementBaseObject inParams = null;
                System.Management.ManagementPath mgmtPath = new System.Management.ManagementPath(CreatedClassName);
                System.Management.ManagementClass classObj = new System.Management.ManagementClass(statMgmtScope, mgmtPath, null);
                inParams = classObj.GetMethodParameters("SetDNSSuffixSearchOrder");
                inParams["DNSDomainSuffixSearchOrder"] = ((string[])(DNSDomainSuffixSearchOrder));
                System.Management.ManagementBaseObject outParams = classObj.InvokeMethod("SetDNSSuffixSearchOrder", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public uint SetDynamicDNSRegistration(bool DomainDNSRegistrationEnabled, bool FullDNSRegistrationEnabled) {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                inParams = PrivateLateBoundObject.GetMethodParameters("SetDynamicDNSRegistration");
                inParams["DomainDNSRegistrationEnabled"] = ((bool)(DomainDNSRegistrationEnabled));
                inParams["FullDNSRegistrationEnabled"] = ((bool)(FullDNSRegistrationEnabled));
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("SetDynamicDNSRegistration", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public static uint SetForwardBufferMemory(uint ForwardBufferMemory) {
            bool IsMethodStatic = true;
            if ((IsMethodStatic == true)) {
                System.Management.ManagementBaseObject inParams = null;
                System.Management.ManagementPath mgmtPath = new System.Management.ManagementPath(CreatedClassName);
                System.Management.ManagementClass classObj = new System.Management.ManagementClass(statMgmtScope, mgmtPath, null);
                inParams = classObj.GetMethodParameters("SetForwardBufferMemory");
                inParams["ForwardBufferMemory"] = ((uint)(ForwardBufferMemory));
                System.Management.ManagementBaseObject outParams = classObj.InvokeMethod("SetForwardBufferMemory", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public uint SetGateways(string[] DefaultIPGateway, ushort[] GatewayCostMetric) {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                inParams = PrivateLateBoundObject.GetMethodParameters("SetGateways");
                inParams["DefaultIPGateway"] = ((string[])(DefaultIPGateway));
                inParams["GatewayCostMetric"] = ((ushort[])(GatewayCostMetric));
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("SetGateways", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public static uint SetIGMPLevel(byte IGMPLevel) {
            bool IsMethodStatic = true;
            if ((IsMethodStatic == true)) {
                System.Management.ManagementBaseObject inParams = null;
                System.Management.ManagementPath mgmtPath = new System.Management.ManagementPath(CreatedClassName);
                System.Management.ManagementClass classObj = new System.Management.ManagementClass(statMgmtScope, mgmtPath, null);
                inParams = classObj.GetMethodParameters("SetIGMPLevel");
                inParams["IGMPLevel"] = ((byte)(IGMPLevel));
                System.Management.ManagementBaseObject outParams = classObj.InvokeMethod("SetIGMPLevel", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public uint SetIPConnectionMetric(uint IPConnectionMetric) {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                inParams = PrivateLateBoundObject.GetMethodParameters("SetIPConnectionMetric");
                inParams["IPConnectionMetric"] = ((uint)(IPConnectionMetric));
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("SetIPConnectionMetric", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public static uint SetIPUseZeroBroadcast(bool IPUseZeroBroadcast) {
            bool IsMethodStatic = true;
            if ((IsMethodStatic == true)) {
                System.Management.ManagementBaseObject inParams = null;
                System.Management.ManagementPath mgmtPath = new System.Management.ManagementPath(CreatedClassName);
                System.Management.ManagementClass classObj = new System.Management.ManagementClass(statMgmtScope, mgmtPath, null);
                inParams = classObj.GetMethodParameters("SetIPUseZeroBroadcast");
                inParams["IPUseZeroBroadcast"] = ((bool)(IPUseZeroBroadcast));
                System.Management.ManagementBaseObject outParams = classObj.InvokeMethod("SetIPUseZeroBroadcast", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public uint SetIPXFrameTypeNetworkPairs(uint[] IPXFrameType, string[] IPXNetworkNumber) {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                inParams = PrivateLateBoundObject.GetMethodParameters("SetIPXFrameTypeNetworkPairs");
                inParams["IPXFrameType"] = ((uint[])(IPXFrameType));
                inParams["IPXNetworkNumber"] = ((string[])(IPXNetworkNumber));
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("SetIPXFrameTypeNetworkPairs", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public static uint SetIPXVirtualNetworkNumber(string IPXVirtualNetNumber) {
            bool IsMethodStatic = true;
            if ((IsMethodStatic == true)) {
                System.Management.ManagementBaseObject inParams = null;
                System.Management.ManagementPath mgmtPath = new System.Management.ManagementPath(CreatedClassName);
                System.Management.ManagementClass classObj = new System.Management.ManagementClass(statMgmtScope, mgmtPath, null);
                inParams = classObj.GetMethodParameters("SetIPXVirtualNetworkNumber");
                inParams["IPXVirtualNetNumber"] = ((string)(IPXVirtualNetNumber));
                System.Management.ManagementBaseObject outParams = classObj.InvokeMethod("SetIPXVirtualNetworkNumber", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public static uint SetKeepAliveInterval(uint KeepAliveInterval) {
            bool IsMethodStatic = true;
            if ((IsMethodStatic == true)) {
                System.Management.ManagementBaseObject inParams = null;
                System.Management.ManagementPath mgmtPath = new System.Management.ManagementPath(CreatedClassName);
                System.Management.ManagementClass classObj = new System.Management.ManagementClass(statMgmtScope, mgmtPath, null);
                inParams = classObj.GetMethodParameters("SetKeepAliveInterval");
                inParams["KeepAliveInterval"] = ((uint)(KeepAliveInterval));
                System.Management.ManagementBaseObject outParams = classObj.InvokeMethod("SetKeepAliveInterval", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public static uint SetKeepAliveTime(uint KeepAliveTime) {
            bool IsMethodStatic = true;
            if ((IsMethodStatic == true)) {
                System.Management.ManagementBaseObject inParams = null;
                System.Management.ManagementPath mgmtPath = new System.Management.ManagementPath(CreatedClassName);
                System.Management.ManagementClass classObj = new System.Management.ManagementClass(statMgmtScope, mgmtPath, null);
                inParams = classObj.GetMethodParameters("SetKeepAliveTime");
                inParams["KeepAliveTime"] = ((uint)(KeepAliveTime));
                System.Management.ManagementBaseObject outParams = classObj.InvokeMethod("SetKeepAliveTime", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public static uint SetMTU(uint MTU) {
            bool IsMethodStatic = true;
            if ((IsMethodStatic == true)) {
                System.Management.ManagementBaseObject inParams = null;
                System.Management.ManagementPath mgmtPath = new System.Management.ManagementPath(CreatedClassName);
                System.Management.ManagementClass classObj = new System.Management.ManagementClass(statMgmtScope, mgmtPath, null);
                inParams = classObj.GetMethodParameters("SetMTU");
                inParams["MTU"] = ((uint)(MTU));
                System.Management.ManagementBaseObject outParams = classObj.InvokeMethod("SetMTU", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public static uint SetNumForwardPackets(uint NumForwardPackets) {
            bool IsMethodStatic = true;
            if ((IsMethodStatic == true)) {
                System.Management.ManagementBaseObject inParams = null;
                System.Management.ManagementPath mgmtPath = new System.Management.ManagementPath(CreatedClassName);
                System.Management.ManagementClass classObj = new System.Management.ManagementClass(statMgmtScope, mgmtPath, null);
                inParams = classObj.GetMethodParameters("SetNumForwardPackets");
                inParams["NumForwardPackets"] = ((uint)(NumForwardPackets));
                System.Management.ManagementBaseObject outParams = classObj.InvokeMethod("SetNumForwardPackets", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public static uint SetPMTUBHDetect(bool PMTUBHDetectEnabled) {
            bool IsMethodStatic = true;
            if ((IsMethodStatic == true)) {
                System.Management.ManagementBaseObject inParams = null;
                System.Management.ManagementPath mgmtPath = new System.Management.ManagementPath(CreatedClassName);
                System.Management.ManagementClass classObj = new System.Management.ManagementClass(statMgmtScope, mgmtPath, null);
                inParams = classObj.GetMethodParameters("SetPMTUBHDetect");
                inParams["PMTUBHDetectEnabled"] = ((bool)(PMTUBHDetectEnabled));
                System.Management.ManagementBaseObject outParams = classObj.InvokeMethod("SetPMTUBHDetect", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public static uint SetPMTUDiscovery(bool PMTUDiscoveryEnabled) {
            bool IsMethodStatic = true;
            if ((IsMethodStatic == true)) {
                System.Management.ManagementBaseObject inParams = null;
                System.Management.ManagementPath mgmtPath = new System.Management.ManagementPath(CreatedClassName);
                System.Management.ManagementClass classObj = new System.Management.ManagementClass(statMgmtScope, mgmtPath, null);
                inParams = classObj.GetMethodParameters("SetPMTUDiscovery");
                inParams["PMTUDiscoveryEnabled"] = ((bool)(PMTUDiscoveryEnabled));
                System.Management.ManagementBaseObject outParams = classObj.InvokeMethod("SetPMTUDiscovery", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public uint SetTcpipNetbios(uint TcpipNetbiosOptions) {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                inParams = PrivateLateBoundObject.GetMethodParameters("SetTcpipNetbios");
                inParams["TcpipNetbiosOptions"] = ((uint)(TcpipNetbiosOptions));
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("SetTcpipNetbios", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public static uint SetTcpMaxConnectRetransmissions(uint TcpMaxConnectRetransmissions) {
            bool IsMethodStatic = true;
            if ((IsMethodStatic == true)) {
                System.Management.ManagementBaseObject inParams = null;
                System.Management.ManagementPath mgmtPath = new System.Management.ManagementPath(CreatedClassName);
                System.Management.ManagementClass classObj = new System.Management.ManagementClass(statMgmtScope, mgmtPath, null);
                inParams = classObj.GetMethodParameters("SetTcpMaxConnectRetransmissions");
                inParams["TcpMaxConnectRetransmissions"] = ((uint)(TcpMaxConnectRetransmissions));
                System.Management.ManagementBaseObject outParams = classObj.InvokeMethod("SetTcpMaxConnectRetransmissions", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public static uint SetTcpMaxDataRetransmissions(uint TcpMaxDataRetransmissions) {
            bool IsMethodStatic = true;
            if ((IsMethodStatic == true)) {
                System.Management.ManagementBaseObject inParams = null;
                System.Management.ManagementPath mgmtPath = new System.Management.ManagementPath(CreatedClassName);
                System.Management.ManagementClass classObj = new System.Management.ManagementClass(statMgmtScope, mgmtPath, null);
                inParams = classObj.GetMethodParameters("SetTcpMaxDataRetransmissions");
                inParams["TcpMaxDataRetransmissions"] = ((uint)(TcpMaxDataRetransmissions));
                System.Management.ManagementBaseObject outParams = classObj.InvokeMethod("SetTcpMaxDataRetransmissions", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public static uint SetTcpNumConnections(uint TcpNumConnections) {
            bool IsMethodStatic = true;
            if ((IsMethodStatic == true)) {
                System.Management.ManagementBaseObject inParams = null;
                System.Management.ManagementPath mgmtPath = new System.Management.ManagementPath(CreatedClassName);
                System.Management.ManagementClass classObj = new System.Management.ManagementClass(statMgmtScope, mgmtPath, null);
                inParams = classObj.GetMethodParameters("SetTcpNumConnections");
                inParams["TcpNumConnections"] = ((uint)(TcpNumConnections));
                System.Management.ManagementBaseObject outParams = classObj.InvokeMethod("SetTcpNumConnections", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public static uint SetTcpUseRFC1122UrgentPointer(bool TcpUseRFC1122UrgentPointer) {
            bool IsMethodStatic = true;
            if ((IsMethodStatic == true)) {
                System.Management.ManagementBaseObject inParams = null;
                System.Management.ManagementPath mgmtPath = new System.Management.ManagementPath(CreatedClassName);
                System.Management.ManagementClass classObj = new System.Management.ManagementClass(statMgmtScope, mgmtPath, null);
                inParams = classObj.GetMethodParameters("SetTcpUseRFC1122UrgentPointer");
                inParams["TcpUseRFC1122UrgentPointer"] = ((bool)(TcpUseRFC1122UrgentPointer));
                System.Management.ManagementBaseObject outParams = classObj.InvokeMethod("SetTcpUseRFC1122UrgentPointer", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public static uint SetTcpWindowSize(ushort TcpWindowSize) {
            bool IsMethodStatic = true;
            if ((IsMethodStatic == true)) {
                System.Management.ManagementBaseObject inParams = null;
                System.Management.ManagementPath mgmtPath = new System.Management.ManagementPath(CreatedClassName);
                System.Management.ManagementClass classObj = new System.Management.ManagementClass(statMgmtScope, mgmtPath, null);
                inParams = classObj.GetMethodParameters("SetTcpWindowSize");
                inParams["TcpWindowSize"] = ((ushort)(TcpWindowSize));
                System.Management.ManagementBaseObject outParams = classObj.InvokeMethod("SetTcpWindowSize", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public uint SetWINSServer(string WINSPrimaryServer, string WINSSecondaryServer) {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                inParams = PrivateLateBoundObject.GetMethodParameters("SetWINSServer");
                inParams["WINSPrimaryServer"] = ((string)(WINSPrimaryServer));
                inParams["WINSSecondaryServer"] = ((string)(WINSSecondaryServer));
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("SetWINSServer", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public enum IGMPLevelValues {
            
            No_Multicast = 0,
            
            IP_Multicast = 1,
            
            IP_IGMP_multicast = 2,
            
            NULL_ENUM_VALUE = 3,
        }
        
        public enum IPXFrameTypeValues {
            
            Ethernet_II = 0,
            
            Ethernet_802_3 = 1,
            
            Ethernet_802_2 = 2,
            
            Ethernet_SNAP = 3,
            
            AUTO = 255,
            
            NULL_ENUM_VALUE = 256,
        }
        
        public enum IPXMediaTypeValues {
            
            Ethernet = 1,
            
            Token_ring = 2,
            
            FDDI = 3,
            
            ARCNET = 8,
            
            NULL_ENUM_VALUE = 0,
        }
        
        public enum TcpipNetbiosOptionsValues {
            
            EnableNetbiosViaDhcp = 0,
            
            EnableNetbios = 1,
            
            DisableNetbios = 2,
            
            NULL_ENUM_VALUE = 3,
        }
        
        // Enumerator implementation for enumerating instances of the class.
        public class NetworkAdapterConfigurationCollection : object, ICollection {
            
            private ManagementObjectCollection privColObj;
            
            public NetworkAdapterConfigurationCollection(ManagementObjectCollection objCollection) {
                privColObj = objCollection;
            }
            
            public virtual int Count {
                get {
                    return privColObj.Count;
                }
            }
            
            public virtual bool IsSynchronized {
                get {
                    return privColObj.IsSynchronized;
                }
            }
            
            public virtual object SyncRoot {
                get {
                    return this;
                }
            }
            
            public virtual void CopyTo(System.Array array, int index) {
                privColObj.CopyTo(array, index);
                int nCtr;
                for (nCtr = 0; (nCtr < array.Length); nCtr = (nCtr + 1)) {
                    array.SetValue(new NetworkAdapterConfiguration(((System.Management.ManagementObject)(array.GetValue(nCtr)))), nCtr);
                }
            }
            
            public virtual System.Collections.IEnumerator GetEnumerator() {
                return new NetworkAdapterConfigurationEnumerator(privColObj.GetEnumerator());
            }
            
            public class NetworkAdapterConfigurationEnumerator : object, System.Collections.IEnumerator {
                
                private ManagementObjectCollection.ManagementObjectEnumerator privObjEnum;
                
                public NetworkAdapterConfigurationEnumerator(ManagementObjectCollection.ManagementObjectEnumerator objEnum) {
                    privObjEnum = objEnum;
                }
                
                public virtual object Current {
                    get {
                        return new NetworkAdapterConfiguration(((System.Management.ManagementObject)(privObjEnum.Current)));
                    }
                }
                
                public virtual bool MoveNext() {
                    return privObjEnum.MoveNext();
                }
                
                public virtual void Reset() {
                    privObjEnum.Reset();
                }
            }
        }
        
        // TypeConverter to handle null values for ValueType properties
        public class WMIValueTypeConverter : TypeConverter {
            
            private TypeConverter baseConverter;
            
            private System.Type baseType;
            
            public WMIValueTypeConverter(System.Type inBaseType) {
                baseConverter = TypeDescriptor.GetConverter(inBaseType);
                baseType = inBaseType;
            }
            
            public override bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Type srcType) {
                return baseConverter.CanConvertFrom(context, srcType);
            }
            
            public override bool CanConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Type destinationType) {
                return baseConverter.CanConvertTo(context, destinationType);
            }
            
            public override object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value) {
                return baseConverter.ConvertFrom(context, culture, value);
            }
            
            public override object CreateInstance(System.ComponentModel.ITypeDescriptorContext context, System.Collections.IDictionary dictionary) {
                return baseConverter.CreateInstance(context, dictionary);
            }
            
            public override bool GetCreateInstanceSupported(System.ComponentModel.ITypeDescriptorContext context) {
                return baseConverter.GetCreateInstanceSupported(context);
            }
            
            public override PropertyDescriptorCollection GetProperties(System.ComponentModel.ITypeDescriptorContext context, object value, System.Attribute[] attributeVar) {
                return baseConverter.GetProperties(context, value, attributeVar);
            }
            
            public override bool GetPropertiesSupported(System.ComponentModel.ITypeDescriptorContext context) {
                return baseConverter.GetPropertiesSupported(context);
            }
            
            public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(System.ComponentModel.ITypeDescriptorContext context) {
                return baseConverter.GetStandardValues(context);
            }
            
            public override bool GetStandardValuesExclusive(System.ComponentModel.ITypeDescriptorContext context) {
                return baseConverter.GetStandardValuesExclusive(context);
            }
            
            public override bool GetStandardValuesSupported(System.ComponentModel.ITypeDescriptorContext context) {
                return baseConverter.GetStandardValuesSupported(context);
            }
            
            public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, System.Type destinationType) {
                if ((baseType.BaseType == typeof(System.Enum))) {
                    if ((value.GetType() == destinationType)) {
                        return value;
                    }
                    if ((((value == null) 
                                && (context != null)) 
                                && (context.PropertyDescriptor.ShouldSerializeValue(context.Instance) == false))) {
                        return  "NULL_ENUM_VALUE" ;
                    }
                    return baseConverter.ConvertTo(context, culture, value, destinationType);
                }
                if (((baseType == typeof(bool)) 
                            && (baseType.BaseType == typeof(System.ValueType)))) {
                    if ((((value == null) 
                                && (context != null)) 
                                && (context.PropertyDescriptor.ShouldSerializeValue(context.Instance) == false))) {
                        return "";
                    }
                    return baseConverter.ConvertTo(context, culture, value, destinationType);
                }
                if (((context != null) 
                            && (context.PropertyDescriptor.ShouldSerializeValue(context.Instance) == false))) {
                    return "";
                }
                return baseConverter.ConvertTo(context, culture, value, destinationType);
            }
        }
        
        // Embedded class to represent WMI system Properties.
        [TypeConverter(typeof(System.ComponentModel.ExpandableObjectConverter))]
        public class ManagementSystemProperties {
            
            private System.Management.ManagementBaseObject PrivateLateBoundObject;
            
            public ManagementSystemProperties(System.Management.ManagementBaseObject ManagedObject) {
                PrivateLateBoundObject = ManagedObject;
            }
            
            [Browsable(true)]
            public int GENUS {
                get {
                    return ((int)(PrivateLateBoundObject["__GENUS"]));
                }
            }
            
            [Browsable(true)]
            public string CLASS {
                get {
                    return ((string)(PrivateLateBoundObject["__CLASS"]));
                }
            }
            
            [Browsable(true)]
            public string SUPERCLASS {
                get {
                    return ((string)(PrivateLateBoundObject["__SUPERCLASS"]));
                }
            }
            
            [Browsable(true)]
            public string DYNASTY {
                get {
                    return ((string)(PrivateLateBoundObject["__DYNASTY"]));
                }
            }
            
            [Browsable(true)]
            public string RELPATH {
                get {
                    return ((string)(PrivateLateBoundObject["__RELPATH"]));
                }
            }
            
            [Browsable(true)]
            public int PROPERTY_COUNT {
                get {
                    return ((int)(PrivateLateBoundObject["__PROPERTY_COUNT"]));
                }
            }
            
            [Browsable(true)]
            public string[] DERIVATION {
                get {
                    return ((string[])(PrivateLateBoundObject["__DERIVATION"]));
                }
            }
            
            [Browsable(true)]
            public string SERVER {
                get {
                    return ((string)(PrivateLateBoundObject["__SERVER"]));
                }
            }
            
            [Browsable(true)]
            public string NAMESPACE {
                get {
                    return ((string)(PrivateLateBoundObject["__NAMESPACE"]));
                }
            }
            
            [Browsable(true)]
            public string PATH {
                get {
                    return ((string)(PrivateLateBoundObject["__PATH"]));
                }
            }
        }
    }
}
