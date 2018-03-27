using System;
using System.Threading.Tasks;

namespace FMS.Infrastructure
{
    public interface IDetailViewModel
    {
        int Id { get; }
        Task LoadAsync(int id);

        bool HasChanges { get; }

        event Action OnClose;
    }
}