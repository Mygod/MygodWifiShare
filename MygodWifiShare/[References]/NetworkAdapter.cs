namespace ROOT.CIMV2.Win32 {
    using System;
    using System.ComponentModel;
    using System.Management;
    using System.Collections;
    using System.Globalization;
    
    
    // ShouldSerialize<PropertyName> 函数是 VS 属性浏览器用来检查某个特定属性是否必须序列化的函数。为所有 ValueType 属性(类型为 Int32、BOOL 等的属性，不能设置为空)添加这些函数。这些函数使用 Is<PropertyName>Null 函数。在属性的 TypeConverter 实现中也使用这些函数来检查属性的 NULL 值，以便在 Visual Studio 中进行拖放操作时可以在属性浏览器中显示空值。
    // 函数 Is<PropertyName>Null() 用于检查属性是否为 NULL。
    // 为可为空值的读/写属性添加 Reset<PropertyName> 函数。VS 设计器在属性浏览器中使用这些函数将属性设置为 NULL。
    // 添加到 WMI 属性的类的每个属性都具有特性集，用于定义它在 Visual Studio 设计器中的行为，并定义要使用的 TypeConverter。
    // 日期时间转换函数 ToDateTime 和 ToDmtfDateTime 添加到类，以便将 DMTF 日期时间转换为 System.DateTime (或相反)。
    // 为 WMI 类生成的早期绑定类。Win32_NetworkAdapter
    public class NetworkAdapter : System.ComponentModel.Component {
        
        // 用于保存驻留该类的 WMI 命名空间的私有属性。
        private static string CreatedWmiNamespace = "root\\CimV2";
        
        // 用于保存创建此类的 WMI 类名称的私有属性。
        private static string CreatedClassName = "Win32_NetworkAdapter";
        
        // 用于保存由各种方法使用的 ManagementScope 的私有成员变量。
        private static System.Management.ManagementScope statMgmtScope = null;
        
        private ManagementSystemProperties PrivateSystemProperties;
        
        // 基础 lateBound WMI 对象。
        private System.Management.ManagementObject PrivateLateBoundObject;
        
        // 存储类的“自动提交”行为的成员变量。
        private bool AutoCommitProp;
        
        // 用于保存表示实例的嵌入属性的私有变量。
        private System.Management.ManagementBaseObject embeddedObj;
        
        // 所使用的当前 WMI 对象
        private System.Management.ManagementBaseObject curObj;
        
        // 用于指示实例是否为嵌入对象的标志。
        private bool isEmbedded;
        
        // 以下是用 WMI 对象初始化类实例的构造函数的不同重载。
        public NetworkAdapter() {
            this.InitializeObject(null, null, null);
        }
        
        public NetworkAdapter(string keyDeviceID) {
            this.InitializeObject(null, new System.Management.ManagementPath(NetworkAdapter.ConstructPath(keyDeviceID)), null);
        }
        
        public NetworkAdapter(System.Management.ManagementScope mgmtScope, string keyDeviceID) {
            this.InitializeObject(((System.Management.ManagementScope)(mgmtScope)), new System.Management.ManagementPath(NetworkAdapter.ConstructPath(keyDeviceID)), null);
        }
        
        public NetworkAdapter(System.Management.ManagementPath path, System.Management.ObjectGetOptions getOptions) {
            this.InitializeObject(null, path, getOptions);
        }
        
        public NetworkAdapter(System.Management.ManagementScope mgmtScope, System.Management.ManagementPath path) {
            this.InitializeObject(mgmtScope, path, null);
        }
        
        public NetworkAdapter(System.Management.ManagementPath path) {
            this.InitializeObject(null, path, null);
        }
        
        public NetworkAdapter(System.Management.ManagementScope mgmtScope, System.Management.ManagementPath path, System.Management.ObjectGetOptions getOptions) {
            this.InitializeObject(mgmtScope, path, getOptions);
        }
        
        public NetworkAdapter(System.Management.ManagementObject theObject) {
            Initialize();
            if ((CheckIfProperClass(theObject) == true)) {
                PrivateLateBoundObject = theObject;
                PrivateSystemProperties = new ManagementSystemProperties(PrivateLateBoundObject);
                curObj = PrivateLateBoundObject;
            }
            else {
                throw new System.ArgumentException("类名不匹配。");
            }
        }
        
        public NetworkAdapter(System.Management.ManagementBaseObject theObject) {
            Initialize();
            if ((CheckIfProperClass(theObject) == true)) {
                embeddedObj = theObject;
                PrivateSystemProperties = new ManagementSystemProperties(theObject);
                curObj = embeddedObj;
                isEmbedded = true;
            }
            else {
                throw new System.ArgumentException("类名不匹配。");
            }
        }
        
        // 属性返回 WMI 类的命名空间。
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
        
        // 指向嵌入对象以获取 WMI 对象的 System 属性的属性。
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ManagementSystemProperties SystemProperties {
            get {
                return PrivateSystemProperties;
            }
        }
        
        // 返回基础 lateBound 对象的属性。
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Management.ManagementBaseObject LateBoundObject {
            get {
                return curObj;
            }
        }
        
        // 对象的 ManagementScope。
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
        
        // 显示 WMI 对象的提交行为的属性。如果为 true，则每次属性被修改后都会自动保存 WMI 对象(即在修改属性后调用 Put())。
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
        
        // 基础 WMI 对象的 ManagementPath。
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
                        throw new System.ArgumentException("类名不匹配。");
                    }
                    PrivateLateBoundObject.Path = value;
                }
            }
        }
        
        // 由各种方法使用的公共静态范围属性。
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
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("AdapterType 属性反映正在使用的网络媒体。此属性可能不适用于该类中列出的所有网络适配器类型。仅适用于 Windows NT。")]
        public string AdapterType {
            get {
                return ((string)(curObj["AdapterType"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsAdapterTypeIdNull {
            get {
                if ((curObj["AdapterTypeId"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"AdapterTypeId 属性反映正在使用的网络媒体。该属性与 AdapterType 属性提供的信息相同，但对应以下几项并且以整数值形式返回的信息除外: 
0 - 以太网 802.3
1 - 令牌环网 802.5
2 - 分布式光纤数据接口(FDDI)
3 - 广域网(WAN)
4 - LocalTalk
5 - 使用 DIX 标头格式的以太网
6 - ARCNET
7 - ARCNET (878.2)
8 - ATM
9 - 无线
10 - 红外无线
11 - Bpc
12 - CoWan
13 - 1394
此属性可能并不适用于该类中列出的所有网络适配器类型。仅适用于 Windows NT。")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public AdapterTypeIdValues AdapterTypeId {
            get {
                if ((curObj["AdapterTypeId"] == null)) {
                    return ((AdapterTypeIdValues)(System.Convert.ToInt32(14)));
                }
                return ((AdapterTypeIdValues)(System.Convert.ToInt32(curObj["AdapterTypeId"])));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsAutoSenseNull {
            get {
                if ((curObj["AutoSense"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("布尔值，用于指明 NetworkAdapter 能否自动确定连接网络媒体的速度或其他通信特征。")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool AutoSense {
            get {
                if ((curObj["AutoSense"] == null)) {
                    return System.Convert.ToBoolean(0);
                }
                return ((bool)(curObj["AutoSense"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsAvailabilityNull {
            get {
                if ((curObj["Availability"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"设备的可用性和状态。例如: Availability 属性可以指明设备正在运行并且为全功率(值=3)、或者处于警告(4)、测试(5)、降级(10)或节能状态(值为 13-15 和 17)。有关节能状态，其定义如下所示: 值 13 (“节能 - 未知”)表示设备处于节能模式，但是它在该模式中的准确状态未知；14 (“节能 - 低功耗模式”)表示设备处于节能状态，但仍然正常运转，可能会出现性能下降；15 (“节能 - 待机”)表示设备没有正常运转，但是可以“快速”转入全功率工作状态；值为 17 (“节能 - 警告”)时表示设备虽然处于节能模式，但它的状态是警告状态。")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public AvailabilityValues Availability {
            get {
                if ((curObj["Availability"] == null)) {
                    return ((AvailabilityValues)(System.Convert.ToInt32(0)));
                }
                return ((AvailabilityValues)(System.Convert.ToInt32(curObj["Availability"])));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Caption 属性是关于对象的简短文字描述(只有一行的字符串)。")]
        public string Caption {
            get {
                return ((string)(curObj["Caption"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsConfigManagerErrorCodeNull {
            get {
                if ((curObj["ConfigManagerErrorCode"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"指明 Win32 配置管理器错误代码。可能返回下列值: 
0     此设备运行正常。
1     此设备的配置不正确。
2     Windows 无法为此设备加载驱动程序。
3     此设备的驱动程序可能已损坏，或者系统内存或其他资源不足。
4     此设备不能正常运行。某个驱动程序或注册表可能已损坏。
5     此设备的驱动程序需要一个 Windows 无法管理的资源。
6     此设备的引导配置与其他设备冲突。
7     无法筛选。
8     找不到此设备的驱动程序加载器。
9     由于控制固件没有正确报告此设备的资源，所以该设备无法正常运行。
10     此设备无法启动。
11     此设备失败。
12     此设备无法找到足够的可用空闲资源。
13     Windows 无法识别此设备的资源。
14     重新启动计算机之前，此设备无法正常运行。
15     由于可能存在重新枚举的问题，所以此设备无法正常运行。
16     Windows 无法识别此设备使用的所有资源。
17     此设备正在请求未知的资源类型。
18     为此设备重新安装驱动程序。
19     你的注册表可能已损坏。
20     使用 VxD 加载器失败。
21     系统失败: 请尝试更改此设备的驱动程序。如果仍然无效，请参阅你的硬件文档。Windows 正在删除此设备。
22     此设备已被禁用。
23     系统失败: 请尝试更改此设备的驱动程序。如果仍然无效，请参阅你的硬件文档。
24     此设备不存在、无法正常运行或者没有安装所有的驱动程序。
25     Windows 仍在安装此设备。
26     Windows 仍在安装此设备。
27     此设备的日志配置无效。
28     没有安装此设备的驱动程序。
29     由于此设备的固件没有提供所需资源，所以此设备已被禁用。
30     此设备正在使用另一台设备使用的中断请求(IRQ)资源。
31     由于 Windows 无法加载此设备所需的驱动程序，所以此设备无法正常运行。")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ConfigManagerErrorCodeValues ConfigManagerErrorCode {
            get {
                if ((curObj["ConfigManagerErrorCode"] == null)) {
                    return ((ConfigManagerErrorCodeValues)(System.Convert.ToInt32(32)));
                }
                return ((ConfigManagerErrorCodeValues)(System.Convert.ToInt32(curObj["ConfigManagerErrorCode"])));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsConfigManagerUserConfigNull {
            get {
                if ((curObj["ConfigManagerUserConfig"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("指明设备是否使用用户定义的配置。")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool ConfigManagerUserConfig {
            get {
                if ((curObj["ConfigManagerUserConfig"] == null)) {
                    return System.Convert.ToBoolean(0);
                }
                return ((bool)(curObj["ConfigManagerUserConfig"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("CreationClassName 指明创建实例时使用的类或子类的名称。与此类的其他键属性一起使用时，此属性可唯一标识此类及其子类的所有实例。")]
        public string CreationClassName {
            get {
                return ((string)(curObj["CreationClassName"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Description 属性提供关于对象的文字描述。")]
        public string Description {
            get {
                return ((string)(curObj["Description"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("DeviceID 属性包含可唯一标识网络适配器的一个字符串，此字符串将网络适配器与系统上的其他设备区分开。")]
        public string DeviceID {
            get {
                return ((string)(curObj["DeviceID"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsErrorClearedNull {
            get {
                if ((curObj["ErrorCleared"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("ErrorCleared 是一个布尔值属性，指明 LastErrorCode 属性中报告的错误现已清除。")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool ErrorCleared {
            get {
                if ((curObj["ErrorCleared"] == null)) {
                    return System.Convert.ToBoolean(0);
                }
                return ((bool)(curObj["ErrorCleared"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("ErrorDescription 是一个自由格式字符串，提供有关 LastErrorCode 属性中记录的错误的详细信息，以及有关可以采取的纠正措施的信息。")]
        public string ErrorDescription {
            get {
                return ((string)(curObj["ErrorDescription"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("GUID 属性指定该连接的全局唯一标识符。")]
        public string GUID {
            get {
                return ((string)(curObj["GUID"]));
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
        [Description("Index 属性指明网络适配器的索引号，该索引号通常存储在系统注册表中。\n例如: 0")]
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
        public bool IsInstallDateNull {
            get {
                if ((curObj["InstallDate"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("InstallDate 属性是一个日期/时间值，指明对象的安装时间。缺少该值并不表示没有安装此对象。")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public System.DateTime InstallDate {
            get {
                if ((curObj["InstallDate"] != null)) {
                    return ToDateTime(((string)(curObj["InstallDate"])));
                }
                else {
                    return System.DateTime.MinValue;
                }
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsInstalledNull {
            get {
                if ((curObj["Installed"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Installed 属性确定系统上是否已安装网络适配器。\n值: TRUE 或 FALSE。值 TRUE 表示已安装网络适配器。\nInstalled 属性已经被弃用" +
            "。此属性没有替换值，并且现已被认为过时。")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool Installed {
            get {
                if ((curObj["Installed"] == null)) {
                    return System.Convert.ToBoolean(0);
                }
                return ((bool)(curObj["Installed"]));
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
        [Description("InterfaceIndex 属性包含可唯一标识本地接口的索引值。")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint InterfaceIndex {
            get {
                if ((curObj["InterfaceIndex"] == null)) {
                    return System.Convert.ToUInt32(0);
                }
                return ((uint)(curObj["InterfaceIndex"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsLastErrorCodeNull {
            get {
                if ((curObj["LastErrorCode"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("LastErrorCode 捕获由逻辑设备报告的最后一个错误代码。")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint LastErrorCode {
            get {
                if ((curObj["LastErrorCode"] == null)) {
                    return System.Convert.ToUInt32(0);
                }
                return ((uint)(curObj["LastErrorCode"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("MACAddress 属性指明此网络适配器的媒体访问控制地址。MAC 地址是由制造商分配给网络适配器的一个 48 位数字，它具有唯一性。它可以唯一标识此网络适配器" +
            "并用于映射 TCP/IP 网络通信。")]
        public string MACAddress {
            get {
                return ((string)(curObj["MACAddress"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Manufacturer 属性指明网络适配器的制造商名称。\n例如: 3COM")]
        public string Manufacturer {
            get {
                return ((string)(curObj["Manufacturer"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsMaxNumberControlledNull {
            get {
                if ((curObj["MaxNumberControlled"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("MaxNumberControlled 属性指明此网络适配器支持的可直接寻址端口的最大数量。如果该数量未知，则值为零。")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint MaxNumberControlled {
            get {
                if ((curObj["MaxNumberControlled"] == null)) {
                    return System.Convert.ToUInt32(0);
                }
                return ((uint)(curObj["MaxNumberControlled"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsMaxSpeedNull {
            get {
                if ((curObj["MaxSpeed"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("网络适配器的最大速度(位/秒)。")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ulong MaxSpeed {
            get {
                if ((curObj["MaxSpeed"] == null)) {
                    return System.Convert.ToUInt64(0);
                }
                return ((ulong)(curObj["MaxSpeed"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Name 属性定义用于识别对象的名称标签。在派生子类时，Name 属性可以替代为 Key 属性。")]
        public string Name {
            get {
                return ((string)(curObj["Name"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("NetConnectionID 属性指定“网络连接”文件夹中显示的网络连接名称。")]
        public string NetConnectionID {
            get {
                return ((string)(curObj["NetConnectionID"]));
            }
            set {
                curObj["NetConnectionID"] = value;
                if (((isEmbedded == false) 
                            && (AutoCommitProp == true))) {
                    PrivateLateBoundObject.Put();
                }
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsNetConnectionStatusNull {
            get {
                if ((curObj["NetConnectionStatus"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("NetConnectionStatus 是指明网络适配器的网络连接状态的字符串。该属性的值有如下几种:\n0 - 未连接\n1 - 正在连接\n2 - 已连接\n3 - " +
            "正在断开连接\n4 - 硬件不存在\n5 - 硬件被禁用\n6 - 硬件无法正常工作\n7 - 媒体已断开连接\n8 - 正在进行身份验证\n9 - 身份验证成功\n10 -" +
            " 身份验证失败\n11 - 无效的地址\n12 - 需要凭据\n.. - 其他 - 有关上列值以外的整数值，请参阅 Win32 错误代码文档。")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ushort NetConnectionStatus {
            get {
                if ((curObj["NetConnectionStatus"] == null)) {
                    return System.Convert.ToUInt16(0);
                }
                return ((ushort)(curObj["NetConnectionStatus"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsNetEnabledNull {
            get {
                if ((curObj["NetEnabled"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("NetEnabled 属性指定是否启用网络连接。")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool NetEnabled {
            get {
                if ((curObj["NetEnabled"] == null)) {
                    return System.Convert.ToBoolean(0);
                }
                return ((bool)(curObj["NetEnabled"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("一个字符串数组，用于指明适配器网络地址。")]
        public string[] NetworkAddresses {
            get {
                return ((string[])(curObj["NetworkAddresses"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("PermanentAddress 定义了以硬编码形式编入到适配器中的网络地址。这个“硬编码”地址可能通过固件升级或软件配置进行更改。如果是这样，该字段应该在发生更" +
            "改时进行更新。如果网络适配器不存在“硬编码”地址，那么 PermanentAddress 应保留为空。")]
        public string PermanentAddress {
            get {
                return ((string)(curObj["PermanentAddress"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsPhysicalAdapterNull {
            get {
                if ((curObj["PhysicalAdapter"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("PhysicalAdapter 属性指定适配器是物理适配器还是逻辑适配器。")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool PhysicalAdapter {
            get {
                if ((curObj["PhysicalAdapter"] == null)) {
                    return System.Convert.ToBoolean(0);
                }
                return ((bool)(curObj["PhysicalAdapter"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("指明逻辑设备的 Win32 即插即用设备 ID。例如: *PNP030b")]
        public string PNPDeviceID {
            get {
                return ((string)(curObj["PNPDeviceID"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"指明逻辑设备中与电源有关的特定功能。数组值 0=“未知”，1=“不支持”和 2=“已禁用”代表的含义一目了然。值 3=“已启用”表示当前已启用电源管理功能，但是具体的功能集未知或者信息无效。“自动进入节能模式”(4)表示设备可根据用途或其他条件更改其电源状态。“可设置电源状态”(5)表示支持 SetPowerState 方法。“支持电源重启”(6)表示可以通过将 PowerState 输入变量设置为 5 (“电源重启”)来调用 SetPowerState 方法。“支持定时通电”(7)表示可以通过将 PowerState 输入变量设置为 5 (“电源重启”)、Time 参数设置为具体的通电日期和时间或间隔，调用 SetPowerState 方法。")]
        public PowerManagementCapabilitiesValues[] PowerManagementCapabilities {
            get {
                System.Array arrEnumVals = ((System.Array)(curObj["PowerManagementCapabilities"]));
                if (arrEnumVals == null) return null;
                PowerManagementCapabilitiesValues[] enumToRet = new PowerManagementCapabilitiesValues[arrEnumVals.Length];
                int counter = 0;
                for (counter = 0; (counter < arrEnumVals.Length); counter = (counter + 1)) {
                    enumToRet[counter] = ((PowerManagementCapabilitiesValues)(System.Convert.ToInt32(arrEnumVals.GetValue(counter))));
                }
                return enumToRet;
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsPowerManagementSupportedNull {
            get {
                if ((curObj["PowerManagementSupported"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("布尔值，指明设备是否支持电源管理，即进入节能状态。该布尔值并不表示当前启用了电源管理功能；或者，如果已经启用了电源管理功能，该布尔值也并不表示支持具体的功能。相关" +
            "信息，请参阅 PowerManagementCapabilities 数组。如果此布尔值为 False，那么在 PowerManagementCapabilit" +
            "ies 数组中，整数值为 1 的字符串“不支持”应是仅有的条目。")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool PowerManagementSupported {
            get {
                if ((curObj["PowerManagementSupported"] == null)) {
                    return System.Convert.ToBoolean(0);
                }
                return ((bool)(curObj["PowerManagementSupported"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("ProductName 属性指明网络适配器的产品名称。\n例如: Fast EtherLink XL")]
        public string ProductName {
            get {
                return ((string)(curObj["ProductName"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("ServiceName 属性指明网络适配器的服务名称。此名称通常短于产品的完整名称。\n例如: Elnkii")]
        public string ServiceName {
            get {
                return ((string)(curObj["ServiceName"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsSpeedNull {
            get {
                if ((curObj["Speed"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("对当前带宽的估计(以“位/秒”为单位)。对于带宽会发生变化或无法进行准确估计的终结点，此属性应该包含名义带宽。")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ulong Speed {
            get {
                if ((curObj["Speed"] == null)) {
                    return System.Convert.ToUInt64(0);
                }
                return ((ulong)(curObj["Speed"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Status 属性是一个指明对象当前状态的字符串。可定义各种运行和非运行状态。运行状态包括“良好”、“已降级”和“预见故障”。其中，“预见故障”表示元素目前虽正常" +
            "运行，但是预见不久会出现故障。示例是一个启用了 SMART 的硬盘驱动器。它还可以指定非运行状态。这些状态包括“错误”、“正在启动”、“正在停止”和“服务”。后" +
            "者(“服务”)可以应用在磁盘镜像、重载用户权限列表或其他管理作业中。并非所有这类作业都是联机的；但是，被管理元素的状态不是“良好”也不是其他任何状态。")]
        public string Status {
            get {
                return ((string)(curObj["Status"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsStatusInfoNull {
            get {
                if ((curObj["StatusInfo"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("StatusInfo 是一个字符串，指明逻辑设备是处于启用(值 = 3)、禁用(值 = 4)、其他(1)还是未知(2)状态。如果这个属性不适用于逻辑设备，则值应该" +
            "为 5 (“不适用”)。")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public StatusInfoValues StatusInfo {
            get {
                if ((curObj["StatusInfo"] == null)) {
                    return ((StatusInfoValues)(System.Convert.ToInt32(0)));
                }
                return ((StatusInfoValues)(System.Convert.ToInt32(curObj["StatusInfo"])));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("作用域系统的 CreationClassName。")]
        public string SystemCreationClassName {
            get {
                return ((string)(curObj["SystemCreationClassName"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("作用域系统的名称。")]
        public string SystemName {
            get {
                return ((string)(curObj["SystemName"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsTimeOfLastResetNull {
            get {
                if ((curObj["TimeOfLastReset"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("TimeOfLastReset 属性指明上次重置网络适配器的时间。")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public System.DateTime TimeOfLastReset {
            get {
                if ((curObj["TimeOfLastReset"] != null)) {
                    return ToDateTime(((string)(curObj["TimeOfLastReset"])));
                }
                else {
                    return System.DateTime.MinValue;
                }
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
        
        private bool ShouldSerializeAdapterTypeId() {
            if ((this.IsAdapterTypeIdNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeAutoSense() {
            if ((this.IsAutoSenseNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeAvailability() {
            if ((this.IsAvailabilityNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeConfigManagerErrorCode() {
            if ((this.IsConfigManagerErrorCodeNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeConfigManagerUserConfig() {
            if ((this.IsConfigManagerUserConfigNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeErrorCleared() {
            if ((this.IsErrorClearedNull == false)) {
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
        
        // 将 DMTF 格式的给定日期时间转换为 System.DateTime 对象。
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
        
        // 将给定的 System.DateTime 对象转换为 DMTF 日期时间格式。
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
        
        private bool ShouldSerializeInstallDate() {
            if ((this.IsInstallDateNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeInstalled() {
            if ((this.IsInstalledNull == false)) {
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
        
        private bool ShouldSerializeLastErrorCode() {
            if ((this.IsLastErrorCodeNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeMaxNumberControlled() {
            if ((this.IsMaxNumberControlledNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeMaxSpeed() {
            if ((this.IsMaxSpeedNull == false)) {
                return true;
            }
            return false;
        }
        
        private void ResetNetConnectionID() {
            curObj["NetConnectionID"] = null;
            if (((isEmbedded == false) 
                        && (AutoCommitProp == true))) {
                PrivateLateBoundObject.Put();
            }
        }
        
        private bool ShouldSerializeNetConnectionStatus() {
            if ((this.IsNetConnectionStatusNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeNetEnabled() {
            if ((this.IsNetEnabledNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializePhysicalAdapter() {
            if ((this.IsPhysicalAdapterNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializePowerManagementSupported() {
            if ((this.IsPowerManagementSupportedNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeSpeed() {
            if ((this.IsSpeedNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeStatusInfo() {
            if ((this.IsStatusInfoNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeTimeOfLastReset() {
            if ((this.IsTimeOfLastResetNull == false)) {
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
        
        private static string ConstructPath(string keyDeviceID) {
            string strPath = "root\\CimV2:Win32_NetworkAdapter";
            strPath = string.Concat(strPath, string.Concat(".DeviceID=", string.Concat("\"", string.Concat(keyDeviceID, "\""))));
            return strPath;
        }
        
        private void InitializeObject(System.Management.ManagementScope mgmtScope, System.Management.ManagementPath path, System.Management.ObjectGetOptions getOptions) {
            Initialize();
            if ((path != null)) {
                if ((CheckIfProperClass(mgmtScope, path, getOptions) != true)) {
                    throw new System.ArgumentException("类名不匹配。");
                }
            }
            PrivateLateBoundObject = new System.Management.ManagementObject(mgmtScope, path, getOptions);
            PrivateSystemProperties = new ManagementSystemProperties(PrivateLateBoundObject);
            curObj = PrivateLateBoundObject;
        }
        
        // GetInstances() 的不同重载帮助枚举 WMI 类的实例。
        public static NetworkAdapterCollection GetInstances() {
            return GetInstances(null, null, null);
        }
        
        public static NetworkAdapterCollection GetInstances(string condition) {
            return GetInstances(null, condition, null);
        }
        
        public static NetworkAdapterCollection GetInstances(string[] selectedProperties) {
            return GetInstances(null, null, selectedProperties);
        }
        
        public static NetworkAdapterCollection GetInstances(string condition, string[] selectedProperties) {
            return GetInstances(null, condition, selectedProperties);
        }
        
        public static NetworkAdapterCollection GetInstances(System.Management.ManagementScope mgmtScope, System.Management.EnumerationOptions enumOptions) {
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
            pathObj.ClassName = "Win32_NetworkAdapter";
            pathObj.NamespacePath = "root\\CimV2";
            System.Management.ManagementClass clsObject = new System.Management.ManagementClass(mgmtScope, pathObj, null);
            if ((enumOptions == null)) {
                enumOptions = new System.Management.EnumerationOptions();
                enumOptions.EnsureLocatable = true;
            }
            return new NetworkAdapterCollection(clsObject.GetInstances(enumOptions));
        }
        
        public static NetworkAdapterCollection GetInstances(System.Management.ManagementScope mgmtScope, string condition) {
            return GetInstances(mgmtScope, condition, null);
        }
        
        public static NetworkAdapterCollection GetInstances(System.Management.ManagementScope mgmtScope, string[] selectedProperties) {
            return GetInstances(mgmtScope, null, selectedProperties);
        }
        
        public static NetworkAdapterCollection GetInstances(System.Management.ManagementScope mgmtScope, string condition, string[] selectedProperties) {
            if ((mgmtScope == null)) {
                if ((statMgmtScope == null)) {
                    mgmtScope = new System.Management.ManagementScope();
                    mgmtScope.Path.NamespacePath = "root\\CimV2";
                }
                else {
                    mgmtScope = statMgmtScope;
                }
            }
            System.Management.ManagementObjectSearcher ObjectSearcher = new System.Management.ManagementObjectSearcher(mgmtScope, new SelectQuery("Win32_NetworkAdapter", condition, selectedProperties));
            System.Management.EnumerationOptions enumOptions = new System.Management.EnumerationOptions();
            enumOptions.EnsureLocatable = true;
            ObjectSearcher.Options = enumOptions;
            return new NetworkAdapterCollection(ObjectSearcher.Get());
        }
        
        [Browsable(true)]
        public static NetworkAdapter CreateInstance() {
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
            return new NetworkAdapter(tmpMgmtClass.CreateInstance());
        }
        
        [Browsable(true)]
        public void Delete() {
            PrivateLateBoundObject.Delete();
        }
        
        public uint Disable() {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("Disable", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public uint Enable() {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("Enable", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public uint Reset() {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("Reset", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public uint SetPowerState(ushort PowerState, System.DateTime Time) {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                inParams = PrivateLateBoundObject.GetMethodParameters("SetPowerState");
                inParams["PowerState"] = ((ushort)(PowerState));
                inParams["Time"] = ToDmtfDateTime(((System.DateTime)(Time)));
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("SetPowerState", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public enum AdapterTypeIdValues {
            
            以太网_802_3 = 0,
            
            令牌环_802_5 = 1,
            
            光纤分布式数据接口_FDDI_ = 2,
            
            广域网_WAN_ = 3,
            
            LocalTalk = 4,
            
            使用_DIX_标头格式的以太网 = 5,
            
            ARCNET = 6,
            
            ARCNET_878_2_ = 7,
            
            ATM = 8,
            
            无线 = 9,
            
            红外无线 = 10,
            
            Bpc = 11,
            
            CoWan = 12,
            
            Val_1394 = 13,
            
            NULL_ENUM_VALUE = 14,
        }
        
        public enum AvailabilityValues {
            
            其他 = 1,
            
            未知 = 2,
            
            正在运行_全功耗 = 3,
            
            警告 = 4,
            
            正在测试 = 5,
            
            不适用 = 6,
            
            关闭电源 = 7,
            
            脱机 = 8,
            
            休息 = 9,
            
            已降级 = 10,
            
            未安装 = 11,
            
            安装错误 = 12,
            
            节能_未知 = 13,
            
            节能_低功耗模式 = 14,
            
            节能_待机 = 15,
            
            电源重启 = 16,
            
            节能_警告 = 17,
            
            已暂停 = 18,
            
            未就绪 = 19,
            
            未配置 = 20,
            
            静止 = 21,
            
            NULL_ENUM_VALUE = 0,
        }
        
        public enum ConfigManagerErrorCodeValues {
            
            该设备正常工作_ = 0,
            
            未正确配置该设备_ = 1,
            
            Windows_无法为此设备加载驱动程序_ = 2,
            
            该设备的驱动程序可能已损坏_或者系统的内存或其他资源不足_ = 3,
            
            该设备无法正常工作_某个驱动程序或注册表可能已损坏_ = 4,
            
            Windows_无法管理该设备的驱动程序所需的资源_ = 5,
            
            该设备的引导配置与其他设备发生冲突_ = 6,
            
            无法筛选_ = 7,
            
            找不到设备的驱动程序加载器_ = 8,
            
            由于控制固件没有正确报告此设备的资源_所以该设备无法正常运行_ = 9,
            
            该设备无法启动_ = 10,
            
            该设备发生故障_ = 11,
            
            该设备找不到足够的可用资源_ = 12,
            
            Windows_无法验证该设备的资源_ = 13,
            
            在重新启动计算机之前_该设备无法正常工作_ = 14,
            
            该设备未正常工作_可能是因为出现了重新枚举问题_ = 15,
            
            Windows_无法识别该设备使用的所有资源_ = 16,
            
            该设备正在请求未知的资源类型_ = 17,
            
            重新安装该设备的驱动程序_ = 18,
            
            使用_VxD_加载器失败_ = 19,
            
            注册表可能已损坏_ = 20,
            
            系统失败_请尝试更改此设备的驱动程序_如果仍然无效_请参阅你的硬件文档_Windows_正在删除此设备_ = 21,
            
            已禁用该设备_ = 22,
            
            系统故障_尝试更改该设备的驱动程序_如果不起作用_请参阅硬件文档_ = 23,
            
            该设备不存在_未正常工作或没有安装其所有驱动程序_ = 24,
            
            Windows_仍在设置该设备_ = 25,
            
            Windows_仍在设置该设备_0 = 26,
            
            该设备没有有效的日志配置_ = 27,
            
            未安装该设备的驱动程序_ = 28,
            
            已禁用该设备_因为设备固件没有提供所需的资源_ = 29,
            
            该设备使用的中断请求_IRQ_资源正在被另一个设备所使用_ = 30,
            
            该设备未正常工作_因为_Windows_无法加载该设备所需的驱动程序_ = 31,
            
            NULL_ENUM_VALUE = 32,
        }
        
        public enum PowerManagementCapabilitiesValues {
            
            未知 = 0,
            
            不支持 = 1,
            
            已禁用 = 2,
            
            已启用 = 3,
            
            自动进入节能模式 = 4,
            
            可设置电源状态 = 5,
            
            支持电源重启 = 6,
            
            支持定时通电 = 7,
            
            NULL_ENUM_VALUE = 8,
        }
        
        public enum StatusInfoValues {
            
            其他 = 1,
            
            未知 = 2,
            
            已启用 = 3,
            
            已禁用 = 4,
            
            不适用 = 5,
            
            NULL_ENUM_VALUE = 0,
        }
        
        // 用于枚举该类的实例的枚举数实现。
        public class NetworkAdapterCollection : object, ICollection {
            
            private ManagementObjectCollection privColObj;
            
            public NetworkAdapterCollection(ManagementObjectCollection objCollection) {
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
                    array.SetValue(new NetworkAdapter(((System.Management.ManagementObject)(array.GetValue(nCtr)))), nCtr);
                }
            }
            
            public virtual System.Collections.IEnumerator GetEnumerator() {
                return new NetworkAdapterEnumerator(privColObj.GetEnumerator());
            }
            
            public class NetworkAdapterEnumerator : object, System.Collections.IEnumerator {
                
                private ManagementObjectCollection.ManagementObjectEnumerator privObjEnum;
                
                public NetworkAdapterEnumerator(ManagementObjectCollection.ManagementObjectEnumerator objEnum) {
                    privObjEnum = objEnum;
                }
                
                public virtual object Current {
                    get {
                        return new NetworkAdapter(((System.Management.ManagementObject)(privObjEnum.Current)));
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
        
        // 处理 ValueType 属性的空值的 TypeConverter
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
        
        // 表示 WMI 系统属性的嵌入类。
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
