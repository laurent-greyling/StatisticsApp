using Nfield.Stats.Entities;
using Nfield.Stats.ViewModels;

namespace Nfield.Stats.Services
{
    public interface INfieldServer
    {
        void Set(string serverSelected);

        ServerEntity Get();

        void ClearAll();
    }
}
