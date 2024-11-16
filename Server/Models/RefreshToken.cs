using Server.Models;

public class RefreshToken
{
    public int Id { get; set; }
    public int UserId { get; set; }  // ID de l'utilisateur auquel le token est associé
    public User? User;
    public string? Token { get; set; }  // Le jeton lui-même
    public DateTime Expires { get; set; }  // Date d'expiration du token
    public bool IsRevoked { get; set; }  // Indique si le token a été révoqué
    public DateTime Created { get; set; }  // Date de création du token
    public DateTime? Revoked { get; set; }  // Date de révocation (si applicable)

    // Relation avec la classe User si nécessaire
    
}