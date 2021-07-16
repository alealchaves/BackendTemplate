namespace BackendTemplate.Domain.Core.i18n
{
    public interface IGlobalizationResource
    {
        string this[string key, params object[] arguments] { get; }
    }
}
