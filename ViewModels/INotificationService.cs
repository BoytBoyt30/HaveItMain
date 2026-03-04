using System.Threading.Tasks;

namespace HaveItMain.ViewModels;

public interface INotificationService
{
    Task ShowMessageAsync(string message);
}