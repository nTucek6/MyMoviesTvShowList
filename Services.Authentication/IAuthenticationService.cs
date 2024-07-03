namespace Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<string> Register(RegisterDTO user);
        Task<string> Login(UserDTO user);
        Task<string> AdminLogin(AdminUserDTO user);
    }
}
