using LeadSoft.Common.Library.Constants;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Lurta.Clean.Application.Contracts.Authentications
{
    [Serializable]
    [DataContract]
    public record DTOLoginRequest
    {
        [DataMember]
        [Required(ErrorMessage = Constant.RequiredField)]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; } = string.Empty;

        [DataMember]
        [Required(ErrorMessage = Constant.RequiredField)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
