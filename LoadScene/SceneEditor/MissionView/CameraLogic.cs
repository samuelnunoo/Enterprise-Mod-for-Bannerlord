using System;
using System.CodeDom;

using SandBox.View.Map;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace LoadScene.SceneEditor.MissionViews

//Camera and Cursor 
{
    partial class SceneEditorMissionView 
        {
            private float _cameraBearingVelocity;
            private Vec2 _clickedPositionPixel;
            private bool _ignoreLeftMouseRelease;
            private Vec3  _clickedPosition;
            private bool _leftButtonDraggingMode;
            private float _targetCameraDistance;
            private GameEntity _cursor;
            private Camera _camera;
            private Vec3 _cameraTarget;
            private float _cameraBearing;
            private float _cameraElevation;
            private Vec2 _lastUsedIdealCameraTarget;
            private MatrixFrame _cameraFrame;
            private float _additionalElevation;
            private Vec3 _idealCameraTarget;
            


            private void CameraLogicInit()
            {
              
              
              this.MissionScreen.MouseVisible = true;
              this.MissionScreen.SceneLayer.ActiveCursor = CursorType.Default;
              


              this._camera = Camera.CreateCamera();
         
            
              this._cameraFrame = MatrixFrame.Identity;
              this._idealCameraTarget = _cameraFrame.origin;
             this._cursor = GameEntity.CreateEmpty(this.Mission.Scene);
            this._cursor.AddComponent((GameEntityComponent) MetaMesh.GetCopy("order_arrow_a", true, false));
            }

            private void CameraLogicTick()
            {
             CameraInputLogic();  
             UpdateMapCamera();
              HandleCursor();
              Move();

            }


            private void Move()
            {
           
             // this.MissionScreen.UpdateFreeCamera(this._cameraFrame);
              
            }

           
            public float CameraDistance { get; set; }
            
         


            public void CameraInputLogic()
            {
                //Variables
                Vec2 vec2_1 = Vec2.Zero;
                float collisionDistance;
                GameEntity collidedEntity;
                Vec2 vec2_2;  
                
            
                
      
                //LeftMouseButtonEvent
                if (this.MissionScreen.SceneLayer.Input.IsKeyPressed(InputKey.LeftMouseButton))
                {
                  this._clickedPositionPixel = this.MissionScreen.SceneLayer.Input.GetMousePositionPixel();
                  this.Mission.Scene.RayCastForClosestEntityOrTerrain(this._mouseRay.Origin, this._mouseRay.EndPoint, out collisionDistance, out this._clickedPosition, out collidedEntity, 0.01f, BodyFlags.CameraCollisionRayCastExludeFlags);
             
                  this._leftButtonDraggingMode = false;
                }
                else
                {

                  //LeftDraftEvent
                  if (this.MissionScreen.SceneLayer.Input.IsKeyDown(InputKey.LeftMouseButton) && !this.MissionScreen.SceneLayer.Input.IsKeyReleased(InputKey.LeftMouseButton))
                  {

                    //Gets MousePositionPixel
                    vec2_2 = this.MissionScreen.SceneLayer.Input.GetMousePositionPixel();

                    //Why The MenuViewContex?
                    if (((double) vec2_2.DistanceSquared(this._clickedPositionPixel) > 300.0 || this._leftButtonDraggingMode))
                    {

                      //Sets Dragging to True!!!
                      this._leftButtonDraggingMode = true;

                      //Sets TranslateMouse 
                      Vec3 zero1 = Vec3.Zero;
                      Vec3 zero2 = Vec3.Zero;
                      this.MissionScreen.SceneLayer.SceneView.TranslateMouse(ref zero1, ref zero2, -1f);


                      //Gets RayDirection???? 
                      Vec3 rayDirection = (zero2 - zero1).NormalizedCopy();

                      //Gets WHat?
                      Vec3 planeNormal = -Vec3.Up;


                      //IDK WHat this does soooooooooooooooooooo
                      float t;
                      if (MBMath.GetRayPlaneIntersectionPoint(ref planeNormal, ref this._clickedPosition, ref zero1, ref rayDirection, out t))
                      {
                        vec2_1 = this._clickedPosition.AsVec2 - (zero1 + rayDirection * t).AsVec2;
                        goto label_21;
                      }
                      else
                        goto label_21;
                    }
                  }

                  //Dragging Mode Set to False 
                  if (this._leftButtonDraggingMode)
                  {
                    this._leftButtonDraggingMode = false;
                    this._ignoreLeftMouseRelease = true;
                  }
                } 
                
                label_21:


                  //Zoom Event 
                  if (this.MissionScreen.SceneLayer.Input.IsKeyDown(InputKey.MiddleMouseButton))
                    this._targetCameraDistance += (float) (0.00999999977648258 * ((double) this.CameraDistance + 20.0)) * this.MissionScreen.SceneLayer.Input.GetMouseSensivity() * this.MissionScreen.SceneLayer.Input.GetMouseMoveY();
                  
                  //Sets Click Position
                  if (this.MissionScreen.SceneLayer.Input.IsKeyReleased(InputKey.LeftMouseButton))
                    this._clickedPositionPixel = this.MissionScreen.SceneLayer.Input.GetMousePositionPixel();

                  

                  // Right Mouse Drag Event
                  if (this.MissionScreen.SceneLayer.Input.IsKeyDown(InputKey.RightMouseButton))
                    this._cameraBearingVelocity += 0.01f * this.MissionScreen.SceneLayer.Input.GetMouseSensivity() * this.MissionScreen.SceneLayer.Input.GetMouseMoveX();

                
      
            }
            
            
            public void HandleCursor()
            {
              Vec3 zero1 = Vec3.Zero;
              Vec3 zero2 = Vec3.Zero;
              Vec2 viewportPoint = this.MissionScreen.SceneLayer.SceneView.ScreenPointToViewportPoint(new Vec2(0.5f, 0.5f));
              this.MissionScreen.CombatCamera.ViewportPointToWorldRay(ref zero1, ref zero2, viewportPoint);
              PathFaceRecord currentFace = new PathFaceRecord();
              Vec3 intersectionPoint;
              GetCursorIntersectionPoint(ref zero1, ref zero2, out float _, out intersectionPoint,ref currentFace,BodyFlags.CommonFocusRayCastExcludeFlags);
              this.MissionScreen.SceneLayer.SceneView.ProjectedMousePositionOnGround(ref intersectionPoint, false, false);
              MatrixFrame frame = this._cursor.GetFrame();
              frame.origin = intersectionPoint;
              frame.rotation.f = this.MissionScreen.CombatCamera.Frame.rotation.f;
              frame.rotation.f.z = 0.0f;
              double num1 = (double) frame.rotation.f.Normalize();
              frame.rotation.s = this.MissionScreen.CombatCamera.Frame.rotation.s;
              frame.rotation.s.z = 0.0f;
              double num2 = (double) frame.rotation.s.Normalize();
              frame.rotation.u = Vec3.CrossProduct(frame.rotation.s, frame.rotation.f);
              frame.rotation.RotateAboutSide(-1.570796f);
              BoundingBox boundingBox = this._cursor.GetMetaMesh(0).GetBoundingBox();
              float num3 = boundingBox.max.y - boundingBox.min.y;
              frame.Advance(-num3);
              this._cursor.SetFrame(ref frame);
              this._cursor.GetFirstMesh().SetMeshRenderOrder(200);
            

            }
            
            
            private static float CalculateCameraElevation(float cameraDistance)
            {
              return (float) ((double) cameraDistance * 0.5 * 0.0149999996647239 + 0.349999994039536);
            }
            
           private void UpdateMapCamera()
              {
           
                //Camera with Updated Values 
                MatrixFrame mapCamera = ComputeMapCamera(ref this._cameraTarget, this._cameraBearing, this._cameraElevation, this.CameraDistance, ref this._lastUsedIdealCameraTarget);
                
                //Bools 
                bool positionChanged = !mapCamera.origin.NearlyEquals(this._cameraFrame.origin, 1E-05f);
                bool rotationChanged = !mapCamera.rotation.NearlyEquals(this._cameraFrame.rotation, 1E-05f);
                
                //IDK If I can currnetly Handle this? 
                if (rotationChanged | positionChanged)
                  Game.Current.EventManager.TriggerEvent<MainMapCameraMoveEvent>(new MainMapCameraMoveEvent(rotationChanged, positionChanged));
                
                
                //Sets Camera Frame 
                this._cameraFrame = mapCamera;
                float height1 = 0.0f;
                
                //Gets Height at a Position?
                this.Mission.Scene.GetHeightAtPoint(this._cameraFrame.origin.AsVec2, BodyFlags.CommonCollisionExcludeFlagsForMissile, ref height1);
                float height2 = height1 + 0.5f;
                
                ///Updates Camera Left Drag 
                if ((double) this._cameraFrame.origin.z < (double) height2)
                {
                  
                  //Checks if ButtonDragginMode is On!!!!!! Breakpoint here 
                  if (this._leftButtonDraggingMode)
                  {
                    
                    //Does some Math Magic
                    Vec3 clickedPosition = this._clickedPosition;
                    Vec3 vec3_1 = clickedPosition - Vec3.DotProduct(clickedPosition - this._cameraFrame.origin, this._cameraFrame.rotation.s) * this._cameraFrame.rotation.s;
                    Vec3 va = (vec3_1 - this._cameraFrame.origin).NormalizedCopy();
                    Vec3 vec3_2 = vec3_1 - (this._cameraFrame.origin + new Vec3(0.0f, 0.0f, height2 - this._cameraFrame.origin.z, -1f));
                    Vec3 vb = vec3_2.NormalizedCopy();
                    Vec3 vec = Vec3.CrossProduct(va, vb);
                    float a = vec.Normalize();
                    this._cameraFrame.origin.z = height2;
                    this._cameraFrame.rotation.u = this._cameraFrame.rotation.u.RotateAboutAnArbitraryVector(vec, a);
                    ref Mat3 local = ref this._cameraFrame.rotation;
                    vec3_2 = Vec3.CrossProduct(this._cameraFrame.rotation.u, this._cameraFrame.rotation.s);
                    Vec3 vec3_3 = vec3_2.NormalizedCopy();
                    local.f = vec3_3;
                    this._cameraFrame.rotation.s = Vec3.CrossProduct(this._cameraFrame.rotation.f, this._cameraFrame.rotation.u);
                    Vec3 planeNormal = -Vec3.Up;
                    Vec3 rayDirection = -this._cameraFrame.rotation.u;
                    float t;
                    
                    //Do I need idealCameraTarget? 
                    if (MBMath.GetRayPlaneIntersectionPoint(ref planeNormal, ref this._idealCameraTarget, ref this._cameraFrame.origin, ref rayDirection, out t))
                    {
                      this._idealCameraTarget = this._cameraFrame.origin + rayDirection * t;
                      this._cameraTarget = this._idealCameraTarget;
                    }
                    
                    // Sets cameraElevation and CameraDistance 
                    Vec2 vec2 = this._cameraFrame.rotation.f.AsVec2;
                    vec2 = new Vec2(vec2.Length, this._cameraFrame.rotation.f.z);
                    this._cameraElevation = -vec2.RotationInRadians;
                    vec3_2 = this._cameraFrame.origin - this._idealCameraTarget;
                    this.CameraDistance = vec3_2.Length - 2f;
                    this._targetCameraDistance = this.CameraDistance;
                    this._additionalElevation = this._cameraElevation - CalculateCameraElevation(this.CameraDistance);
                    this._lastUsedIdealCameraTarget = this._idealCameraTarget.AsVec2;
                    ComputeMapCamera(ref this._cameraTarget, this._cameraBearing, this._cameraElevation, this.CameraDistance, ref this._lastUsedIdealCameraTarget);
                  }
                  else
                  {
                    float num1 = 0.4712389f;
                    int num2 = 0;
                    do
                    {
                      this._cameraElevation += (double) this._cameraFrame.origin.z < (double) height2 ? num1 : -num1;
                      this._additionalElevation = this._cameraElevation - CalculateCameraElevation(this.CameraDistance);
                      float val1_1 = 700f;
                      float num3 = (Campaign.Current.UseFreeCameraAtMapScreen ? 1f : 0.3f) / val1_1;
                      float val1_2 = 50f;
                      float minValue = (float) -((double) val1_1 - (double) Math.Min(val1_1, Math.Max(val1_2, this.CameraDistance))) * num3;
                      float maxValue = Math.Max(minValue + 1E-05f, 1.555088f - CalculateCameraElevation(this.CameraDistance));
                      this._additionalElevation = MBMath.ClampFloat(this._additionalElevation, minValue, maxValue);
                      this._cameraElevation = this._additionalElevation + CalculateCameraElevation(this.CameraDistance);
                      Vec2 zero = Vec2.Zero;
                      this._cameraFrame = ComputeMapCamera(ref this._cameraTarget, this._cameraBearing, this._cameraElevation, this.CameraDistance, ref zero);
                      this.Mission.Scene.GetHeightAtPoint(this._cameraFrame.origin.AsVec2, BodyFlags.CommonCollisionExcludeFlagsForMissile, ref height2);
                      height2 += 0.5f;
                      if ((double) num1 > 9.99999974737875E-05)
                        num1 *= 0.5f;
                      else
                        ++num2;
                    }
                    while ((double) num1 > 9.99999974737875E-05 || (double) this._cameraFrame.origin.z < (double) height2 && num2 < 5);
                    if ((double) this._cameraFrame.origin.z < (double) height2)
                    {
                      this._cameraFrame.origin.z = height2;
                      Vec3 planeNormal = -Vec3.Up;
                      Vec3 rayDirection = -this._cameraFrame.rotation.u;
                      float t;
                      if (MBMath.GetRayPlaneIntersectionPoint(ref planeNormal, ref this._idealCameraTarget, ref this._cameraFrame.origin, ref rayDirection, out t))
                      {
                        this._idealCameraTarget = this._cameraFrame.origin + rayDirection * t;
                        this._cameraTarget = this._idealCameraTarget;
                      }
                      this.CameraDistance = (this._cameraFrame.origin - this._idealCameraTarget).Length - 2f;
                      this._lastUsedIdealCameraTarget = this._idealCameraTarget.AsVec2;
                      ComputeMapCamera(ref this._cameraTarget, this._cameraBearing, this._cameraElevation, this.CameraDistance, ref this._lastUsedIdealCameraTarget);
                    }
                  }
                }
                
                
               
                this.MissionScreen.UpdateFreeCamera(this._cameraFrame);
                this._camera.SetFovVertical(0.6981317f, TaleWorlds.Engine.Screen.AspectRatio, 0.01f, TaleWorlds.Core.ManagedParameters.Instance.GetManagedParameter(TaleWorlds.Core.ManagedParametersEnum.CampaignMapMaxAllowedCameraHeight) * 4f);
                
                
                Vec3 origin = this._cameraFrame.origin;
                origin.z = 0.0f;
                this.Mission.Scene.SetDepthOfFieldFocus(0.0f);
                this.Mission.Scene.SetDepthOfFieldParameters(0.0f, 0.0f, false);
                this.MissionScreen.SceneLayer.SetFocusedShadowmap(false, ref origin, 0.0f);
                MatrixFrame identity = MatrixFrame.Identity;
                identity.rotation = this._cameraFrame.rotation;
                identity.origin = this._cameraTarget;
                this.Mission.Scene.GetHeightAtPoint(identity.origin.AsVec2, BodyFlags.CommonCollisionExcludeFlagsForMissile, ref identity.origin.z);
                identity.origin = MBMath.Lerp(identity.origin, this._cameraFrame.origin, 0.075f, 1E-05f);
                PathFaceRecord faceIndex = Campaign.Current.MapSceneWrapper.GetFaceIndex(identity.origin.AsVec2);
                if (faceIndex.IsValid())
                  MBMapScene.TickAmbientSounds(this.Mission.Scene, (int) Campaign.Current.MapSceneWrapper.GetFaceTerrainType(faceIndex));
                MBSoundManager.SetListenerFrame(identity);
              }

            
          private static MatrixFrame ComputeMapCamera(
              ref Vec3 cameraTarget,
              float cameraBearing,
              float cameraElevation,
              float cameraDistance,
              ref Vec2 lastUsedIdealCameraTarget)
              {
              Vec2 asVec2 = cameraTarget.AsVec2;
              MatrixFrame identity1 = MatrixFrame.Identity;
              MatrixFrame identity2 = MatrixFrame.Identity;
              identity2.origin = cameraTarget;
              identity2.rotation.RotateAboutSide(1.570796f);
              identity2.rotation.RotateAboutForward(-cameraBearing);
              identity2.rotation.RotateAboutSide(-cameraElevation);
              identity2.origin += identity2.rotation.u * (cameraDistance + 2f);
              float val2 = (float) Math.Atan((double) MathF.Tan(0.3490658f) * (double) TaleWorlds.Engine.Screen.AspectRatio);
              TaleWorlds.Core.ManagedParameters instance = TaleWorlds.Core.ManagedParameters.Instance;
              float num1 = instance.GetManagedParameter(TaleWorlds.Core.ManagedParametersEnum.CampaignMapMaxAllowedDistanceFromCenterPointY) - Math.Max(0.0f, identity2.origin.z) * MathF.Tan(Math.Min(0.3490658f, val2));
              float num2 = instance.GetManagedParameter(TaleWorlds.Core.ManagedParametersEnum.CampaignMapMaxAllowedDistanceFromCenterPointX) - Math.Max(0.0f, identity2.origin.z) * MathF.Tan(Math.Min(0.3490658f, val2));
              float managedParameter1 = instance.GetManagedParameter(TaleWorlds.Core.ManagedParametersEnum.CampaignMapCenterPointX);
              float managedParameter2 = instance.GetManagedParameter(TaleWorlds.Core.ManagedParametersEnum.CampaignMapCenterPointY);
              asVec2.x = MBMath.ClampFloat(asVec2.x, managedParameter1 - num2, managedParameter1 + num2);
              asVec2.y = MBMath.ClampFloat(asVec2.y, managedParameter2 - num1, managedParameter2 + num1);
              lastUsedIdealCameraTarget.x = MBMath.ClampFloat(lastUsedIdealCameraTarget.x, managedParameter1 - num2, managedParameter1 + num2);
              lastUsedIdealCameraTarget.y = MBMath.ClampFloat(lastUsedIdealCameraTarget.y, managedParameter2 - num1, managedParameter2 + num1);
              identity2.origin.x += asVec2.x - cameraTarget.x;
              identity2.origin.y += asVec2.y - cameraTarget.y;
              return identity2;
              }
      
      
            
            
            
            
            
            
                    }
          }