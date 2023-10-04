namespace ValkyrieHr.Domain.Dtos.Account
{
    public class UserImageUpdateDto
    {
        public Guid Id { get; set; }
        public IFormFile UserImage { get; set; }
    }
}
