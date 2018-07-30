namespace Nfield.Stats.Models
{
    public class AuthorizationModel
    {
        public string AuthenticationToken { get; set; }
        public string Message { get; set; }
        public NfieldErrorCode NfieldErrorCode { get; set; }
    }
}