namespace JwtDemoSite.Modules
{
    public interface ISystemAuthorityModule
    {
        bool ValidateUserFunction(int employeeNo, string[] urls);
    }
}