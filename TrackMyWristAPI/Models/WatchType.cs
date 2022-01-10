using System.Text.Json.Serialization;

namespace TrackMyWristAPI.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum WatchType
    {
        Manual,
        Automatic,
        Quartz
    }
}
