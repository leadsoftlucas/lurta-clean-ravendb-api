using System.Runtime.Serialization;

namespace Lurta.Clean.Application.Contracts.Usuarios
{
    [Serializable]
    [DataContract]
    public partial class DTOUserResponse
    {
        [DataMember]
        public string Id { get; set; } = null;

        [DataMember]
        public string Username { get; set; } = string.Empty;
    }
}
