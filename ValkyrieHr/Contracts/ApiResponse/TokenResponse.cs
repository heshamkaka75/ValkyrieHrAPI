namespace ValkyrieHr.Contracts.ApiResponse
{
    public class TokenResponse : BaseResponse
    {
        public TokenResponse(string token, string userId, string username, string phoneNumber, bool success, int statusCode, string message) : base(success, statusCode, message)
        {
            UserId = userId;
            Username = username;
            PhoneNumber = phoneNumber;
            Token = token;
        }
        public string UserId { get; set; }
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public string Token { get; set; }
    }
}