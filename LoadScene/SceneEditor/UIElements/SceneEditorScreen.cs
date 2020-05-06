using LoadScene.NavigationElements;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Engine.Screens;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View.Screen;

namespace LoadScene.SceneEditor.UIElements
{
    // Screen to Load 
    [GameStateScreen(typeof(CustomEditorState))]
    class SceneEditorScreen : ScreenBase, IGameStateListener
    {
        private SceneLayer _sceneLayer;
        private Camera _camera;
        private float _cameraBearing;
        private float _cameraDistance;
        private float _cameraElevation;
        private Ray _mouseRay;
        private CustomEditorState _state;
        
        public SceneEditorScreen Instance { get; internal set; }

        public SceneEditorScreen(CustomEditorState state)
        {
            this._state = state;
            
         
        }

        void IGameStateListener.OnActivate()
        {
            base.OnActivate();
            //Configure ScreenLayer
            this._sceneLayer = new SceneLayer();
            this._sceneLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericCampaignPanelsGameKeyCategory"));
            this.AddLayer((ScreenLayer) this._sceneLayer);
            
            //this._sceneLayer.SetScene(Mission.Current.Scene);
            
            //Configure Camera
            this._camera = Camera.CreateCamera();
            this._camera.SetViewVolume(true, -0.1f, 0.1f, -0.07f, 0.07f, 0.2f, 300f);
            this._camera.Position = new Vec3(0.0f, 0.0f, 10f, -1f);
            this._cameraBearing = 0.0f;
            this._cameraElevation = 1f;
            this._cameraDistance = 2.5f;
            
            
            //Configure MouseRay
            this._mouseRay = new Ray(Vec3.Zero,Vec3.Up,float.MaxValue);
        
        }

        void IGameStateListener.OnInitialize()
        {
       
    


        }
        
        void IGameStateListener.OnDeactivate(){}
        
        void IGameStateListener.OnFinalize(){}

        //Get SceneLayer
        public SceneLayer SceneLayer
        {
            get => _sceneLayer;
            set => _sceneLayer = value;
        }
        
        
        //Will Do Something With This Later 
        protected override void OnFrameTick(float dt)
        {
            base.OnFrameTick(dt);
            
        }
    }
}

