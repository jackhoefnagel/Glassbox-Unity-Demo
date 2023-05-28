using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public class ProfilePartial : MonoBehaviour
{
    public partial class Profiel
    {
        [JsonProperty("event")]
        public Event Event { get; set; }

        [JsonProperty("chromosome")]
        public Chromosome Chromosome { get; set; }

        [JsonProperty("fitness")]
        public double Fitness { get; set; }

        [JsonProperty("p_id")]
        public long PId { get; set; }

        [JsonProperty("p_list_target")]
        public PListTarget PListTarget { get; set; }
    }

    public partial class Chromosome
    {
        [JsonProperty("columns")]
        public string[][] Columns { get; set; }

        [JsonProperty("colindex")]
        public Colindex Colindex { get; set; }
    }

    public partial class Colindex
    {
        [JsonProperty("lookup")]
        public Lookup Lookup { get; set; }

        [JsonProperty("names")]
        public Name[] Names { get; set; }
    }

    public partial class Lookup
    {
        [JsonProperty("A")]
        public long A { get; set; }

        [JsonProperty("V")]
        public long V { get; set; }
    }

    public enum Name { A, V };

    public enum Event { Add, Init, Update };

    public partial struct PListTarget
    {
        public long? Integer;
        public long[] IntegerArray;

        public static implicit operator PListTarget(long Integer) => new PListTarget { Integer = Integer };
        public static implicit operator PListTarget(long[] IntegerArray) => new PListTarget { IntegerArray = IntegerArray };
    }

    public partial class Profiel
    {
        //public static Profiel[] FromJson(string json) => JsonConvert.DeserializeObject<Profiel[]>(json, QuickType.Converter.Settings);          
        public static Profiel[] FromJson(string json) => JsonConvert.DeserializeObject<Profiel[]>(json, Converter.Settings);
    }

    //public static class Serialize
    //{
    //    public static string ToJson(this Profiel[] self) => JsonConvert.SerializeObject(self);
    //}

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                NameConverter.Singleton,
                EventConverter.Singleton,
                PListTargetConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class NameConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Name) || t == typeof(Name?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "A":
                    return Name.A;
                case "V":
                    return Name.V;
            }
            throw new Exception("Cannot unmarshal type Name");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Name)untypedValue;
            switch (value)
            {
                case Name.A:
                    serializer.Serialize(writer, "A");
                    return;
                case Name.V:
                    serializer.Serialize(writer, "V");
                    return;
            }
            throw new Exception("Cannot marshal type Name");
        }

        public static readonly NameConverter Singleton = new NameConverter();
    }

    internal class EventConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Event) || t == typeof(Event?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "add":
                    return Event.Add;
                case "init":
                    return Event.Init;
                case "update":
                    return Event.Update;
            }
            throw new Exception("Cannot unmarshal type Event");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Event)untypedValue;
            switch (value)
            {
                case Event.Add:
                    serializer.Serialize(writer, "add");
                    return;
                case Event.Init:
                    serializer.Serialize(writer, "init");
                    return;
                case Event.Update:
                    serializer.Serialize(writer, "update");
                    return;
            }
            throw new Exception("Cannot marshal type Event");
        }

        public static readonly EventConverter Singleton = new EventConverter();
    }

    internal class PListTargetConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(PListTarget) || t == typeof(PListTarget?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Integer:
                    var integerValue = serializer.Deserialize<long>(reader);
                    return new PListTarget { Integer = integerValue };
                case JsonToken.StartArray:
                    var arrayValue = serializer.Deserialize<long[]>(reader);
                    return new PListTarget { IntegerArray = arrayValue };
            }
            throw new Exception("Cannot unmarshal type PListTarget");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (PListTarget)untypedValue;
            if (value.Integer != null)
            {
                serializer.Serialize(writer, value.Integer.Value);
                return;
            }
            if (value.IntegerArray != null)
            {
                serializer.Serialize(writer, value.IntegerArray);
                return;
            }
            throw new Exception("Cannot marshal type PListTarget");
        }

        public static readonly PListTargetConverter Singleton = new PListTargetConverter();
    }

}
