﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Xml.Serialization;

// 
// This source code was auto-generated by xsd, Version=4.6.1055.0.
// 


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.garmin.com/xmlschemas/GpxExtensions/v3")]
[System.Xml.Serialization.XmlRootAttribute("WaypointExtension", Namespace="http://www.garmin.com/xmlschemas/GpxExtensions/v3", IsNullable=false)]
public partial class WaypointExtension_t {
    
    private double proximityField;
    
    private bool proximityFieldSpecified;
    
    private double temperatureField;
    
    private bool temperatureFieldSpecified;
    
    private double depthField;
    
    private bool depthFieldSpecified;
    
    private DisplayMode_t displayModeField;
    
    private bool displayModeFieldSpecified;
    
    private string[] categoriesField;
    
    private Address_t addressField;
    
    private PhoneNumber_t[] phoneNumberField;
    
    private Extensions_t extensionsField;
    
    /// <remarks/>
    public double Proximity {
        get {
            return this.proximityField;
        }
        set {
            this.proximityField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool ProximitySpecified {
        get {
            return this.proximityFieldSpecified;
        }
        set {
            this.proximityFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    public double Temperature {
        get {
            return this.temperatureField;
        }
        set {
            this.temperatureField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool TemperatureSpecified {
        get {
            return this.temperatureFieldSpecified;
        }
        set {
            this.temperatureFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    public double Depth {
        get {
            return this.depthField;
        }
        set {
            this.depthField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool DepthSpecified {
        get {
            return this.depthFieldSpecified;
        }
        set {
            this.depthFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    public DisplayMode_t DisplayMode {
        get {
            return this.displayModeField;
        }
        set {
            this.displayModeField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool DisplayModeSpecified {
        get {
            return this.displayModeFieldSpecified;
        }
        set {
            this.displayModeFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Category", IsNullable=false)]
    public string[] Categories {
        get {
            return this.categoriesField;
        }
        set {
            this.categoriesField = value;
        }
    }
    
    /// <remarks/>
    public Address_t Address {
        get {
            return this.addressField;
        }
        set {
            this.addressField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("PhoneNumber")]
    public PhoneNumber_t[] PhoneNumber {
        get {
            return this.phoneNumberField;
        }
        set {
            this.phoneNumberField = value;
        }
    }
    
    /// <remarks/>
    public Extensions_t Extensions {
        get {
            return this.extensionsField;
        }
        set {
            this.extensionsField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.garmin.com/xmlschemas/GpxExtensions/v3")]
public enum DisplayMode_t {
    
    /// <remarks/>
    SymbolOnly,
    
    /// <remarks/>
    SymbolAndName,
    
    /// <remarks/>
    SymbolAndDescription,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.garmin.com/xmlschemas/GpxExtensions/v3")]
public partial class Address_t {
    
    private string[] streetAddressField;
    
    private string cityField;
    
    private string stateField;
    
    private string countryField;
    
    private string postalCodeField;
    
    private Extensions_t extensionsField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("StreetAddress", DataType="token")]
    public string[] StreetAddress {
        get {
            return this.streetAddressField;
        }
        set {
            this.streetAddressField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType="token")]
    public string City {
        get {
            return this.cityField;
        }
        set {
            this.cityField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType="token")]
    public string State {
        get {
            return this.stateField;
        }
        set {
            this.stateField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType="token")]
    public string Country {
        get {
            return this.countryField;
        }
        set {
            this.countryField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType="token")]
    public string PostalCode {
        get {
            return this.postalCodeField;
        }
        set {
            this.postalCodeField = value;
        }
    }
    
    /// <remarks/>
    public Extensions_t Extensions {
        get {
            return this.extensionsField;
        }
        set {
            this.extensionsField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.garmin.com/xmlschemas/GpxExtensions/v3")]
public partial class Extensions_t {
    
    private System.Xml.XmlElement[] anyField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAnyElementAttribute()]
    public System.Xml.XmlElement[] Any {
        get {
            return this.anyField;
        }
        set {
            this.anyField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.garmin.com/xmlschemas/GpxExtensions/v3")]
public partial class AutoroutePoint_t {
    
    private byte[] subclassField;
    
    private decimal latField;
    
    private decimal lonField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType="hexBinary")]
    public byte[] Subclass {
        get {
            return this.subclassField;
        }
        set {
            this.subclassField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal lat {
        get {
            return this.latField;
        }
        set {
            this.latField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal lon {
        get {
            return this.lonField;
        }
        set {
            this.lonField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.garmin.com/xmlschemas/GpxExtensions/v3")]
public partial class PhoneNumber_t {
    
    private string categoryField;
    
    private string valueField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute(DataType="token")]
    public string Category {
        get {
            return this.categoryField;
        }
        set {
            this.categoryField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute(DataType="token")]
    public string Value {
        get {
            return this.valueField;
        }
        set {
            this.valueField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.garmin.com/xmlschemas/GpxExtensions/v3")]
[System.Xml.Serialization.XmlRootAttribute("RouteExtension", Namespace="http://www.garmin.com/xmlschemas/GpxExtensions/v3", IsNullable=false)]
public partial class RouteExtension_t {
    
    private bool isAutoNamedField;
    
    private DisplayColor_t displayColorField;
    
    private bool displayColorFieldSpecified;
    
    private Extensions_t extensionsField;
    
    /// <remarks/>
    public bool IsAutoNamed {
        get {
            return this.isAutoNamedField;
        }
        set {
            this.isAutoNamedField = value;
        }
    }
    
    /// <remarks/>
    public DisplayColor_t DisplayColor {
        get {
            return this.displayColorField;
        }
        set {
            this.displayColorField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool DisplayColorSpecified {
        get {
            return this.displayColorFieldSpecified;
        }
        set {
            this.displayColorFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    public Extensions_t Extensions {
        get {
            return this.extensionsField;
        }
        set {
            this.extensionsField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.garmin.com/xmlschemas/GpxExtensions/v3")]
public enum DisplayColor_t {
    
    /// <remarks/>
    Black,
    
    /// <remarks/>
    DarkRed,
    
    /// <remarks/>
    DarkGreen,
    
    /// <remarks/>
    DarkYellow,
    
    /// <remarks/>
    DarkBlue,
    
    /// <remarks/>
    DarkMagenta,
    
    /// <remarks/>
    DarkCyan,
    
    /// <remarks/>
    LightGray,
    
    /// <remarks/>
    DarkGray,
    
    /// <remarks/>
    Red,
    
    /// <remarks/>
    Green,
    
    /// <remarks/>
    Yellow,
    
    /// <remarks/>
    Blue,
    
    /// <remarks/>
    Magenta,
    
    /// <remarks/>
    Cyan,
    
    /// <remarks/>
    White,
    
    /// <remarks/>
    Transparent,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.garmin.com/xmlschemas/GpxExtensions/v3")]
[System.Xml.Serialization.XmlRootAttribute("RoutePointExtension", Namespace="http://www.garmin.com/xmlschemas/GpxExtensions/v3", IsNullable=false)]
public partial class RoutePointExtension_t {
    
    private byte[] subclassField;
    
    private AutoroutePoint_t[] rptField;
    
    private Extensions_t extensionsField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType="hexBinary")]
    public byte[] Subclass {
        get {
            return this.subclassField;
        }
        set {
            this.subclassField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("rpt")]
    public AutoroutePoint_t[] rpt {
        get {
            return this.rptField;
        }
        set {
            this.rptField = value;
        }
    }
    
    /// <remarks/>
    public Extensions_t Extensions {
        get {
            return this.extensionsField;
        }
        set {
            this.extensionsField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.garmin.com/xmlschemas/GpxExtensions/v3")]
[System.Xml.Serialization.XmlRootAttribute("TrackExtension", Namespace="http://www.garmin.com/xmlschemas/GpxExtensions/v3", IsNullable=false)]
public partial class TrackExtension_t {
    
    private DisplayColor_t displayColorField;
    
    private bool displayColorFieldSpecified;
    
    private Extensions_t extensionsField;
    
    /// <remarks/>
    public DisplayColor_t DisplayColor {
        get {
            return this.displayColorField;
        }
        set {
            this.displayColorField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool DisplayColorSpecified {
        get {
            return this.displayColorFieldSpecified;
        }
        set {
            this.displayColorFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    public Extensions_t Extensions {
        get {
            return this.extensionsField;
        }
        set {
            this.extensionsField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.garmin.com/xmlschemas/GpxExtensions/v3")]
[System.Xml.Serialization.XmlRootAttribute("TrackPointExtension", Namespace="http://www.garmin.com/xmlschemas/GpxExtensions/v3", IsNullable=false)]
public partial class TrackPointExtension_t {
    
    private double temperatureField;
    
    private bool temperatureFieldSpecified;
    
    private double depthField;
    
    private bool depthFieldSpecified;
    
    private Extensions_t extensionsField;
    
    /// <remarks/>
    public double Temperature {
        get {
            return this.temperatureField;
        }
        set {
            this.temperatureField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool TemperatureSpecified {
        get {
            return this.temperatureFieldSpecified;
        }
        set {
            this.temperatureFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    public double Depth {
        get {
            return this.depthField;
        }
        set {
            this.depthField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool DepthSpecified {
        get {
            return this.depthFieldSpecified;
        }
        set {
            this.depthFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    public Extensions_t Extensions {
        get {
            return this.extensionsField;
        }
        set {
            this.extensionsField = value;
        }
    }
}
