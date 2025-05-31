namespace FarmaciaApi.Entities;

public class Usuario
{
	public int Id { get; set; }
	public string Nombre { get; set; } = null!;
	public string Email { get; set; } = null!;
	public string PasswordHash { get; set; } = null!;
	public string Rol { get; set; } = "User"; // User | Admin, etc.
}