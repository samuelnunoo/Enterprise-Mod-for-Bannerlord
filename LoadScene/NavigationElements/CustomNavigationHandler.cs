using LoadScene.ModelObjects;
using TaleWorlds.CampaignSystem;

namespace LoadScene.NavigationElements
{
    public interface CustomNavigationHandler : INavigationHandler
    {
        void OpenCustom();
        void OpenOverview(Worker worker);
        
    }
}