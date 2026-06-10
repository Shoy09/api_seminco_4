namespace Seminco.Application.Planes;

public interface IFechaPlanMensualService
{
    Task<List<FechaPlanMensualDto>> GetAllAsync(CancellationToken ct);
    Task<FechaPlanMensualDto> CreateAsync(string mes, CancellationToken ct);
    Task<UltimaFechaDto?> GetUltimaAsync(CancellationToken ct);
}
