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
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.garmin.com/xmlschemas/UserProfile/v1")]
[System.Xml.Serialization.XmlRootAttribute("Profile", Namespace="http://www.garmin.com/xmlschemas/UserProfile/v1", IsNullable=false)]
public partial class ProfileData_t {
    
    private System.DateTime birthDateField;
    
    private double weightKilogramsField;
    
    private Gender_t genderField;
    
    private AbstractProfileActivity_t[] activitiesField;
    
    private Extensions_t extensionsField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType="date")]
    public System.DateTime BirthDate {
        get {
            return this.birthDateField;
        }
        set {
            this.birthDateField = value;
        }
    }
    
    /// <remarks/>
    public double WeightKilograms {
        get {
            return this.weightKilogramsField;
        }
        set {
            this.weightKilogramsField = value;
        }
    }
    
    /// <remarks/>
    public Gender_t Gender {
        get {
            return this.genderField;
        }
        set {
            this.genderField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Activities")]
    public AbstractProfileActivity_t[] Activities {
        get {
            return this.activitiesField;
        }
        set {
            this.activitiesField = value;
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
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.garmin.com/xmlschemas/UserProfile/v1")]
public enum Gender_t {
    
    /// <remarks/>
    Male,
    
    /// <remarks/>
    Female,
}

/// <remarks/>
[System.Xml.Serialization.XmlIncludeAttribute(typeof(BikeProfileActivity_t))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ProfileActivity_t))]
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.garmin.com/xmlschemas/UserProfile/v1")]
public abstract partial class AbstractProfileActivity_t {
    
    private HeartRateInBeatsPerMinute_t maximumHeartRateBpmField;
    
    private double gearWeightKilogramsField;
    
    private ProfileHeartRateZone_t[] heartRateZonesField;
    
    private ProfileSpeedZone_t[] speedZonesField;
    
    private Extensions_t extensionsField;
    
    /// <remarks/>
    public HeartRateInBeatsPerMinute_t MaximumHeartRateBpm {
        get {
            return this.maximumHeartRateBpmField;
        }
        set {
            this.maximumHeartRateBpmField = value;
        }
    }
    
    /// <remarks/>
    public double GearWeightKilograms {
        get {
            return this.gearWeightKilogramsField;
        }
        set {
            this.gearWeightKilogramsField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("HeartRateZones")]
    public ProfileHeartRateZone_t[] HeartRateZones {
        get {
            return this.heartRateZonesField;
        }
        set {
            this.heartRateZonesField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("SpeedZones")]
    public ProfileSpeedZone_t[] SpeedZones {
        get {
            return this.speedZonesField;
        }
        set {
            this.speedZonesField = value;
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
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.garmin.com/xmlschemas/UserProfile/v1")]
public partial class HeartRateInBeatsPerMinute_t {
    
    private byte valueField;
    
    /// <remarks/>
    public byte Value {
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
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.garmin.com/xmlschemas/UserProfile/v1")]
public partial class WheelData_t {
    
    private string sizeMillimetersField;
    
    private Extensions_t extensionsField;
    
    private bool autoWheelSizeField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType="positiveInteger")]
    public string SizeMillimeters {
        get {
            return this.sizeMillimetersField;
        }
        set {
            this.sizeMillimetersField = value;
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
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public bool AutoWheelSize {
        get {
            return this.autoWheelSizeField;
        }
        set {
            this.autoWheelSizeField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.garmin.com/xmlschemas/UserProfile/v1")]
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
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.garmin.com/xmlschemas/UserProfile/v1")]
public partial class BikeData_t {
    
    private string nameField;
    
    private double odometerMetersField;
    
    private double weightKilogramsField;
    
    private WheelData_t wheelSizeField;
    
    private Extensions_t extensionsField;
    
    private bool hasCadenceSensorField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType="token")]
    public string Name {
        get {
            return this.nameField;
        }
        set {
            this.nameField = value;
        }
    }
    
    /// <remarks/>
    public double OdometerMeters {
        get {
            return this.odometerMetersField;
        }
        set {
            this.odometerMetersField = value;
        }
    }
    
    /// <remarks/>
    public double WeightKilograms {
        get {
            return this.weightKilogramsField;
        }
        set {
            this.weightKilogramsField = value;
        }
    }
    
    /// <remarks/>
    public WheelData_t WheelSize {
        get {
            return this.wheelSizeField;
        }
        set {
            this.wheelSizeField = value;
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
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public bool HasCadenceSensor {
        get {
            return this.hasCadenceSensorField;
        }
        set {
            this.hasCadenceSensorField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.garmin.com/xmlschemas/UserProfile/v1")]
public partial class CustomSpeedZone_t {
    
    private SpeedType_t viewAsField;
    
    private double lowInMetersPerSecondField;
    
    private double highInMetersPerSecondField;
    
    /// <remarks/>
    public SpeedType_t ViewAs {
        get {
            return this.viewAsField;
        }
        set {
            this.viewAsField = value;
        }
    }
    
    /// <remarks/>
    public double LowInMetersPerSecond {
        get {
            return this.lowInMetersPerSecondField;
        }
        set {
            this.lowInMetersPerSecondField = value;
        }
    }
    
    /// <remarks/>
    public double HighInMetersPerSecond {
        get {
            return this.highInMetersPerSecondField;
        }
        set {
            this.highInMetersPerSecondField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.garmin.com/xmlschemas/UserProfile/v1")]
public enum SpeedType_t {
    
    /// <remarks/>
    Pace,
    
    /// <remarks/>
    Speed,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.garmin.com/xmlschemas/UserProfile/v1")]
public partial class ProfileSpeedZone_t {
    
    private string numberField;
    
    private string nameField;
    
    private CustomSpeedZone_t valueField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType="positiveInteger")]
    public string Number {
        get {
            return this.numberField;
        }
        set {
            this.numberField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType="token")]
    public string Name {
        get {
            return this.nameField;
        }
        set {
            this.nameField = value;
        }
    }
    
    /// <remarks/>
    public CustomSpeedZone_t Value {
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
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.garmin.com/xmlschemas/UserProfile/v1")]
public partial class ProfileHeartRateZone_t {
    
    private string numberField;
    
    private HeartRateType_t viewAsField;
    
    private HeartRateInBeatsPerMinute_t lowField;
    
    private HeartRateInBeatsPerMinute_t highField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType="positiveInteger")]
    public string Number {
        get {
            return this.numberField;
        }
        set {
            this.numberField = value;
        }
    }
    
    /// <remarks/>
    public HeartRateType_t ViewAs {
        get {
            return this.viewAsField;
        }
        set {
            this.viewAsField = value;
        }
    }
    
    /// <remarks/>
    public HeartRateInBeatsPerMinute_t Low {
        get {
            return this.lowField;
        }
        set {
            this.lowField = value;
        }
    }
    
    /// <remarks/>
    public HeartRateInBeatsPerMinute_t High {
        get {
            return this.highField;
        }
        set {
            this.highField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.garmin.com/xmlschemas/UserProfile/v1")]
public enum HeartRateType_t {
    
    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("Percent Max")]
    PercentMax,
    
    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("Beats Per Minute")]
    BeatsPerMinute,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.garmin.com/xmlschemas/UserProfile/v1")]
public partial class BikeProfileActivity_t : AbstractProfileActivity_t {
    
    private BikeData_t[] bikeField;
    
    private Sport_t sportField;
    
    public BikeProfileActivity_t() {
        this.sportField = Sport_t.Biking;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Bike")]
    public BikeData_t[] Bike {
        get {
            return this.bikeField;
        }
        set {
            this.bikeField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public Sport_t Sport {
        get {
            return this.sportField;
        }
        set {
            this.sportField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.garmin.com/xmlschemas/UserProfile/v1")]
public enum Sport_t {
    
    /// <remarks/>
    Running,
    
    /// <remarks/>
    Biking,
    
    /// <remarks/>
    Other,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.garmin.com/xmlschemas/UserProfile/v1")]
public partial class ProfileActivity_t : AbstractProfileActivity_t {
    
    private Sport_t sportField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public Sport_t Sport {
        get {
            return this.sportField;
        }
        set {
            this.sportField = value;
        }
    }
}