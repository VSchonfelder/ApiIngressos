using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiIngressos.Models;

public class Reserva
{
    public int Id { get; set; }

    [Required]
    [MaxLength(11)]
    public string UsuarioCpf { get; set; } = string.Empty;

    public int EventoId { get; set; }

    [MaxLength(50)]
    public string? CupomUtilizado { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal ValorFinalPago { get; set; }

    public Usuario? Usuario { get; set; }
    public Evento? Evento { get; set; }
    public Cupom? Cupom { get; set; }
}
