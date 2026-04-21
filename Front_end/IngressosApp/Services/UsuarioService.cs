namespace IngressosApp.Services;

public class Usuario
{
    public string Nome { get; set; } = "";
    public string Email { get; set; } = "";
    public string CPF { get; set; } = "";
    public string Senha { get; set; } = "";
}

public class UsuarioService
{
    private List<Usuario> _usuarios = new();

    public List<Usuario> GetUsuarios() => _usuarios;

    public string AdicionarUsuario(Usuario usuario)
    {
        if (!usuario.Email.Contains("@"))
            return "Erro: o email informado é inválido.";

        if (_usuarios.Any(u => u.Email == usuario.Email))
            return $"Erro: já existe um usuário cadastrado com o email '{usuario.Email}'.";

        if (_usuarios.Any(u => u.CPF == usuario.CPF))
            return $"Erro: já existe um usuário cadastrado com o CPF '{usuario.CPF}'.";

        _usuarios.Add(usuario);
        return "ok";
    }

    public void RemoverUsuario(Usuario usuario)
    {
        _usuarios.Remove(usuario);
    }
}