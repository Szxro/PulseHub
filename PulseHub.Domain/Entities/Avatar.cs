using PulseHub.SharedKernel;

namespace PulseHub.Domain.Entities;

public class Avatar : Entity
{
    public string FileName { get; set; } = string.Empty;

    public string Path { get; set; } = string.Empty;

    public long TotalSize { get; set; }

    public int Height { get; set; }

    public int Width { get; set; }

    public string Format { get; set; } = string.Empty;

    public int UserId { get; set; }

    public User? User { get; set; }
}
