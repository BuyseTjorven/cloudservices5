using Newtonsoft.Json;
using System;
using System.Collections.Generic;


public class Location
{
    [JsonProperty("city")]
    public string city { get; set; }
    [JsonProperty("street")]
    public string street { get; set; }
}

public class Person
{
    [JsonProperty("firstName")]
    public string firstName { get; set; }
    [JsonProperty("lastName")]
    public string lastName { get; set; }
    [JsonProperty("eMail")]
    public string eMail { get; set; }
    [JsonProperty("age")]
    public int age { get; set; }
    public List<Location> locations { get; set; }
}