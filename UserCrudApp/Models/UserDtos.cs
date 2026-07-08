namespace UserCrudApp.Models;

public record CreateUserRequest(string Name, string Email);
public record UpdateUserRequest(string Name, string Email);
