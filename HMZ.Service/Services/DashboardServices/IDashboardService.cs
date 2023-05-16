namespace HMZ.Service.Services.DashboardServices
{
    public interface IDashboardService
    {
        Task<int> GetAll(string dashboardId);
    }
}
