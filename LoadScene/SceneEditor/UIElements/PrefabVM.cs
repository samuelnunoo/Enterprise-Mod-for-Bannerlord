using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace LoadScene.SceneEditor.UIElements
{
    public class PrefabVM : ViewModel
    {

        private List<ImageIdentifier> _prefabimage;
        private List<string> _prefabs;


        public PrefabVM()
        {
            
            _prefabs = new List<string>();
            _prefabimage = new List<ImageIdentifier>();
            _prefabs.Add("aserai_castle_barn_a_13");

            foreach (string prefab in _prefabs)
            {
                _prefabimage.Add(new ImageIdentifier(prefab,ImageIdentifierType.Item));
            }
       
                
            
        }


        [DataSourceProperty]
        public List<ImageIdentifier> GetImages
        {
            get { return _prefabimage; }
            
        }
        
        

    }
}