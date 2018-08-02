using Nfield.Stats.ViewModels;

namespace Nfield.Stats.Services
{
    public interface INfieldServer
    {
        SetServerSettingsViewModel Set(string serverSelected);

        GetServerSettingsViewModel Get();

        void Clear();
    }
}
