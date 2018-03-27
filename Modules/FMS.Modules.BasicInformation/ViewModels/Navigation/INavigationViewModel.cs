
using FMS.Infrastructure;
using System.Threading.Tasks;

namespace FMS.Modules.BasicInformation
{
    public interface INavigationViewModel : IViewModel
    {
        Task LoadAsync();
    }
}
