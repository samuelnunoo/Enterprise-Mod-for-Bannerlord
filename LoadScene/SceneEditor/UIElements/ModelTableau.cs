using System;

using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace LoadScene.SceneEditor.UIElements
{
    public class ModelTableau
    {
        private Scene _tableauScene;
        private Camera _camera;
        private float _curZoomSpeed;
        private Vec3 _curCamDisplacement;
        private bool _initialized;
        private Vec3 _midPoint;
        private bool _isEnabled;
        private bool _isRotatingByDefault;
        private bool _isRotating;
        private string _stringId;
        private GameEntity _model;
        private MatrixFrame _initialFrame;
        private float _panRotation;
        private float _tiltRotation;
        private int _tableauSizeX;
        private int _tableauSizeY;
        private float _cameraRatio;

        public Scene TableauScene => _tableauScene;
        public ModelTableau()
        {
            this.SetEnabled(true);
        }
        public Texture Texture { get; private set; }
        private TableauView View
        {
            get
            {
                return (NativeObject) this.Texture != (NativeObject) null ? this.Texture.TableauView : (TableauView) null;
            }
        }
        public void Initialize()
        {
            
            //Create Scene
            this._tableauScene = Scene.CreateNewScene(true);
            this._tableauScene.SetName( nameof (_model));
            this._tableauScene.DisableStaticShadows(true);
            this._tableauScene.SetAtmosphereWithName("character_menu_a");
            Vec3 direction = new Vec3(1f,-1f,-1f,-1f);
            this._tableauScene.SetSunDirection(ref direction);
            //this._midPoint = (this._model.GlobalBoxMax + this._model.GlobalBoxMin) * 0.5f + (globalBoxMax + globalBoxMin - this._model.GlobalBoxMax - this._model.GlobalBoxMin) * 0.5f;
            
            
            //Reset Camera and Init
            this.ResetCamera();
            this._initialized = true;
            



        }
        
        
        public void SetModel(GameEntity model)
        {
            this._model = model; 
            this.RefreshModelTableau();
        }
        
        private void RefreshModelTableau()
        {
            if (!this._initialized)
                this.Initialize();

       
            TableauView view = this.View;
            if ((NativeObject) view != (NativeObject) null)
            {
                float radius = (this._model.GetBoundingBoxMax() - this._model.GetBoundingBoxMin()).Length * 2f;
                Vec3 origin = this._model.GetGlobalFrame().origin;
                view.SetFocusedShadowmap(true, ref origin, radius);
            }

            this._initialFrame = this._model.GetFrame();
            Vec3 eulerAngles = this._initialFrame.rotation.GetEulerAngles();
            this._panRotation = eulerAngles.x;
            this._tiltRotation = eulerAngles.z;
        }
        protected void SetEnabled(bool enabled)
        {
            this._isRotatingByDefault = true;
            this._isRotating = false;
            this.ResetCamera();
            this._isEnabled = enabled;
            TableauView view = this.View;
            if (!((NativeObject) view != (NativeObject) null))
                return;
            view.SetEnable(this._isEnabled);
        }
        
        public void SetTargetSize(int width, int height)
        {
            this._isRotating = false;
            if (width <= 0 || height <= 0)
            {
                this._tableauSizeX = 10;
                this._tableauSizeY = 10;
            }
            else
            {
                this._tableauSizeX = width;
                this._tableauSizeY = height;
            }
            this._cameraRatio = (float) this._tableauSizeX / (float) this._tableauSizeY;
            TableauView view = this.View;
            if ((NativeObject) view != (NativeObject) null)
                view.SetEnable(false);
            this.Texture = TableauView.AddTableau(new RenderTargetComponent.TextureUpdateEventHandler(this.TableauMaterialTabInventoryItemTooltipOnRender), (object) this._tableauScene, this._tableauSizeX, this._tableauSizeY);
        }
        private void TableauMaterialTabInventoryItemTooltipOnRender(Texture sender, EventArgs e)
        {
          TableauView tableauView = this.View;
          if ((NativeObject) tableauView == (NativeObject) null)
          {
            tableauView = sender.TableauView;
            tableauView.SetEnable(this._isEnabled);
          }
       
          
            tableauView.SetRenderWithPostfx(true);
            tableauView.SetClearColor(0U);
            tableauView.SetScene(this._tableauScene);
            if ((NativeObject) this._camera == (NativeObject) null)
            {
              this._camera = Camera.CreateCamera();
              this._camera.SetViewVolume(true, -0.5f, 0.5f, -0.5f, 0.5f, 0.01f, 100f);
              this.ResetCamera();
              tableauView.SetSceneUsesSkybox(false);
            }
            tableauView.SetCamera(this._camera);
            if (this._isRotatingByDefault)
              this.UpdateRotation(1f, 0.0f);
            tableauView.SetDeleteAfterRendering(false);
            tableauView.SetContinuousRendering(true);
          
        }
        
   
     
        
        
        
        private void SetCamFovHorizontal(float camFov)
        {
            this._camera.SetFovHorizontal(camFov, 1f, 0.1f, 50f);
        }
        private void MakeCameraLookMidPoint()
        {
            this._camera.Position = this._midPoint + this._camera.Frame.rotation.TransformToParent(this._curCamDisplacement) - this._camera.Direction * (this._midPoint.Length * 0.5263158f);
        }
        private void ResetCamera()
        {
            this._curCamDisplacement = Vec3.Zero;
            this._curZoomSpeed = 0.0f;
            if (!((NativeObject) this._camera != (NativeObject) null))
                return;
            this._camera.Frame = MatrixFrame.Identity;
            this.SetCamFovHorizontal(1f);
            this.MakeCameraLookMidPoint();
        }
        private void UpdateRotation(float mouseMoveX, float mouseMoveY)
        {
            if (!this._initialized)
                return;
            this._panRotation += mouseMoveX * ((float) Math.PI / 720f);
            this._tiltRotation += mouseMoveY * ((float) Math.PI / 720f);
            this._tiltRotation = MathF.Clamp(this._tiltRotation, -2.984513f, -0.1570796f);
            MatrixFrame frame1 = this._model.GetFrame();
            Vec3 vec3 = (this._model.GetBoundingBoxMax() + this._model.GetBoundingBoxMin()) * 0.5f;
            MatrixFrame identity1 = MatrixFrame.Identity;
            identity1.origin = vec3;
            MatrixFrame identity2 = MatrixFrame.Identity;
            identity2.origin = -vec3;
            MatrixFrame matrixFrame = frame1 * identity1;
            matrixFrame.rotation = Mat3.Identity;
            matrixFrame.rotation.ApplyScaleLocal(this._initialFrame.rotation.GetScaleVector());
            matrixFrame.rotation.RotateAboutSide(this._tiltRotation);
            matrixFrame.rotation.RotateAboutUp(this._panRotation);
            MatrixFrame frame2 = matrixFrame * identity2;
            this._model.SetFrame(ref frame2);
        }
        
        public Texture Test()
        {
            var x = 0;
            var y = 0;
            var z = 0;
            var frame = MatrixFrame.Identity;
            var cx = x + frame.origin.x;
            var cy = y + frame.origin.y;
            var cz = z + frame.origin.z;
            Vec3 position = new Vec3(cx, cy, cz);
            Mat3 rotation = frame.rotation;
            MatrixFrame matrix = new MatrixFrame(rotation,position);
            var modelTableau = new ModelTableau();
            modelTableau.Initialize();
            GameEntity entity = GameEntity.Instantiate(modelTableau.TableauScene, "ship_a", matrix);
            modelTableau.SetModel(entity);
            modelTableau.SetTargetSize(400,800);

            return modelTableau.Texture;


        }

    }
    
}