// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class Datum
{
    public string productId { get; set; }
    public string upc { get; set; }
    public List<AisleLocation> aisleLocations { get; set; }
    public string brand { get; set; }
    public List<string> categories { get; set; }
    public string countryOrigin { get; set; }
    public string description { get; set; }
    public List<Image> images { get; set; }
    public List<Items> items { get; set; }
    public ItemInformation itemInformation { get; set; }
    public Temperature temperature { get; set; }
}

public class Fulfillment
{
    public bool curbside { get; set; }
    public bool delivery { get; set; }
    public bool inStore { get; set; }
    public bool shipToHome { get; set; }
}

public class Image
{
    public string perspective { get; set; }
    public bool featured { get; set; }
    public List<Size> sizes { get; set; }
}

public class Items
{
    public string itemId { get; set; }
    public bool favorite { get; set; }
    public Fulfillment fulfillment { get; set; }
    public string size { get; set; }
    public Prices price { get; set; }
}

public class Prices
{ 
    public decimal regular { get; set; }
    public decimal promo { get; set; }

}

public class ItemInformation
{
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

public class Root
{
    public List<Datum> data { get; set; }
    public Meta meta { get; set; }
}

public class Size
{
    public string size { get; set; }
    public string url { get; set; }
}

public class Temperature
{
    public string indicator { get; set; }
    public bool heatSensitive { get; set; }
}

public class AisleLocation
{
    public string bayNumber { get; set; }
    public string description { get; set; }
    public string  number { get; set; }
    public string numberOfFacings { get; set; }
    public string side { get; set; }
    public string shelfNumber { get; set; }
    public string shelfPositionInBay { get; set; }

}
