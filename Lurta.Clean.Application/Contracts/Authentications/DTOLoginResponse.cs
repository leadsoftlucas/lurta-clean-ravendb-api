using System.Runtime.Serialization;

namespace Lurta.Clean.Application.Contracts.Authentications
{
    [Serializable]
    [DataContract]
    public record DTOLoginResponse(string jwt, string username)
    {
        [DataMember]
        public string Username { get; private set; } = username;

        [DataMember]
        public string Jwt { get; private set; } = jwt;

        [DataMember]
        public static DateTimeOffset When { get => DateTimeOffset.UtcNow; }
    }
}
