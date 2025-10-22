namespace WebApplication1.Models
{
    public interface IGuidService
    {
        Guid GetGuid();
    }
    public class GuidService: IGuidService
    {
        private readonly Guid _guid = Guid.NewGuid();
        public Guid GetGuid() => _guid;
    }

    public class GuidService2 : IGuidService
    {
        private readonly Guid _guid = Guid.Empty;
        public Guid GetGuid() => _guid;
    }
}
