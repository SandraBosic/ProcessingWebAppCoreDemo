namespace Core.Interfaces.ProcessingWeb
{
    public interface IConfigureDomainGroup
    {
        string VpsUserDomainGroupName();

        string VpsAdminDomainGroupName();

        string LogApplicationDomainGroupName();
    }
}
