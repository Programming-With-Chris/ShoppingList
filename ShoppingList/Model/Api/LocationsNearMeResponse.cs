using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Model.Api; 

public class LocationsNearMeResponse
{
    Rootobject _ro; 
}


public class Rootobject
{
    public Datum[] data { get; set; }
    public Meta meta { get; set; }
}

public class Meta
{
    public Pagination pagination { get; set; }
}

public class Pagination
{
    public int start { get; set; }
    public int limit { get; set; }
    public int total { get; set; }
}

public class Datum
{
    public string locationId { get; set; }
    public string chain { get; set; }
    public Address address { get; set; }
    public Geolocation geolocation { get; set; }
    public string name { get; set; }
    public Department[] departments { get; set; }
    public Hours hours { get; set; }
    public string phone { get; set; }
}

public class Address
{
    public string addressLine1 { get; set; }
    public string city { get; set; }
    public string state { get; set; }
    public string zipCode { get; set; }
    public string county { get; set; }
}

public class Geolocation
{
    public float latitude { get; set; }
    public float longitude { get; set; }
    public string latLng { get; set; }
}

public class Hours
{
    public string timezone { get; set; }
    public string gmtOffset { get; set; }
    public bool open24 { get; set; }
    public Monday monday { get; set; }
    public Tuesday tuesday { get; set; }
    public Wednesday wednesday { get; set; }
    public Thursday thursday { get; set; }
    public Friday friday { get; set; }
    public Saturday saturday { get; set; }
    public Sunday sunday { get; set; }
}

public class Monday
{
    public string open { get; set; }
    public string close { get; set; }
    public bool open24 { get; set; }
}

public class Tuesday
{
    public string open { get; set; }
    public string close { get; set; }
    public bool open24 { get; set; }
}

public class Wednesday
{
    public string open { get; set; }
    public string close { get; set; }
    public bool open24 { get; set; }
}

public class Thursday
{
    public string open { get; set; }
    public string close { get; set; }
    public bool open24 { get; set; }
}

public class Friday
{
    public string open { get; set; }
    public string close { get; set; }
    public bool open24 { get; set; }
}

public class Saturday
{
    public string open { get; set; }
    public string close { get; set; }
    public bool open24 { get; set; }
}

public class Sunday
{
    public string open { get; set; }
    public string close { get; set; }
    public bool open24 { get; set; }
}

public class Department
{
    public string departmentId { get; set; }
    public string name { get; set; }
    public string phone { get; set; }
    public Hours1 hours { get; set; }
    public Address1 address { get; set; }
    public Geolocation1 geolocation { get; set; }
    public bool offsite { get; set; }
}

public class Hours1
{
    public bool open24 { get; set; }
    public Monday1 monday { get; set; }
    public Tuesday1 tuesday { get; set; }
    public Wednesday1 wednesday { get; set; }
    public Thursday1 thursday { get; set; }
    public Friday1 friday { get; set; }
    public Saturday1 saturday { get; set; }
    public Sunday1 sunday { get; set; }
}

public class Monday1
{
    public string open { get; set; }
    public string close { get; set; }
    public bool open24 { get; set; }
}

public class Tuesday1
{
    public string open { get; set; }
    public string close { get; set; }
    public bool open24 { get; set; }
}

public class Wednesday1
{
    public string open { get; set; }
    public string close { get; set; }
    public bool open24 { get; set; }
}

public class Thursday1
{
    public string open { get; set; }
    public string close { get; set; }
    public bool open24 { get; set; }
}

public class Friday1
{
    public string open { get; set; }
    public string close { get; set; }
    public bool open24 { get; set; }
}

public class Saturday1
{
    public string open { get; set; }
    public string close { get; set; }
    public bool open24 { get; set; }
}

public class Sunday1
{
    public string open { get; set; }
    public string close { get; set; }
    public bool open24 { get; set; }
}

public class Address1
{
    public string addressLine1 { get; set; }
    public string city { get; set; }
    public string state { get; set; }
    public string zipCode { get; set; }
}

public class Geolocation1
{
    public float latitude { get; set; }
    public float longitude { get; set; }
    public string latLng { get; set; }
}

