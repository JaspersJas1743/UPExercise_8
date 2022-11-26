using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class IncorrectWord
{
    [JsonPropertyName("word")] public string Value { get; set; }
    [JsonPropertyName("s")] public IEnumerable<string> PossibleWay { get; set; }
}
