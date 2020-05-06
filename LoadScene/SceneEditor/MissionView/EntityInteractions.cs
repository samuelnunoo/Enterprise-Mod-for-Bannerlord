using System;
using System.Collections.Generic;
using System.Threading;
using LoadScene.SceneEditor.UIElements;
using SandBox.View.Map;
using SandBox.View.Menu;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.Engine.InputSystem;
using TaleWorlds.Engine.Screens;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View.Missions;
using TaleWorlds.MountAndBlade.View.Screen;



namespace LoadScene.SceneEditor.MissionViews
{ 
  
    
    partial class SceneEditorMissionView {

        private List<GameEntity> _entities;
        private EditorSceneHandler _handler;
        private testGlobalLayer _testGlobalLayer;
        
        public List<GameEntity> GetEntities()
        {
            //Get Entities 
            this._entities = new List<GameEntity>();
            this.Mission.Scene.GetEntities(ref this._entities);
            return this._entities;

        }

        private GameEntity _item;

        public override void OnMissionScreenPreLoad()
        {
            base.OnMissionScreenPreLoad();

            //Implement Handler and LoadScreen
            base.OnMissionScreenInitialize();
 

        


        }


        public void MoveItem(GameEntity item)
        {
            Vec2 position = Input.GetMousePositionRanged();
            Vec3 rayBegin;
            Vec3 rayEnd;
            this.MissionScreen.ScreenPointToWorldRay(position,out  rayBegin, out rayEnd );
            
            item.SetLocalPosition(rayEnd);
        }

        public void PlaceItem()
        {
            
            Vec2 position = Input.GetMousePositionRanged();
            Vec3 rayBegin;
            Vec3 rayEnd;
            this.MissionScreen.ScreenPointToWorldRay(position,out  rayBegin, out rayEnd );
            
            Mat3 rotation = Mat3.Identity;
            
            
            MatrixFrame frame = new MatrixFrame(rotation,rayEnd);
            GameEntity.Instantiate(Mission.Current.Scene, "ship_a", frame);
        }
        public void RotateItem(GameEntity item)
        {
            Mat3 rotation = item.GetFrame().rotation;
            rotation.RotateAboutSide(30f);
            Vec3 position = item.GetFrame().origin;
            MatrixFrame frame = new MatrixFrame(rotation,position);
            
            
            item.SetFrame(ref frame);


        }

        public void RotateUp(GameEntity item)
        {
            Mat3 rotation = item.GetFrame().rotation;
            rotation.RotateAboutUp(30f);
            Vec3 position = item.GetFrame().origin;
            MatrixFrame frame = new MatrixFrame(rotation,position);
            
            
            item.SetFrame(ref frame);
            
        }
        public void EntityInteractionsTick()
        {
            if (Input.IsKeyDown(InputKey.LeftMouseButton))
            {

                if (_item == null)
                {
                    
                    _item = GetCollidedEntity();

                }
                if (_item != null && !Input.IsKeyReleased(InputKey.LeftMouseButton))
                {
                    MoveItem(_item);
                    if (Input.IsKeyPressed(InputKey.Q) && _item!=null)
                    {
                        RotateUp(_item);
                    }
                    if (Input.IsKeyPressed(InputKey.E) && _item!=null)
                    {
                        RotateItem(_item);
                    }
                    
                    
                }
                
            }


            if (Input.IsKeyReleased(InputKey.LeftMouseButton))
            {
                _item = null; 
            }

            if (Input.IsKeyPressed(InputKey.Q) && !Input.IsKeyDown(InputKey.LeftMouseButton))
            {
                PlaceItem();
                
            }
          
        }
        public void CreateLayout()
        {
            //Template 
            this._testGlobalLayer = new testGlobalLayer();
            this._testGlobalLayer.Initialize();
            ScreenManager.AddGlobalLayer(this._testGlobalLayer, true);
            

        }
        
 
        
        private GameEntity GetCollidedEntity()
        {
  
            this.MissionScreen.ScreenPointToWorldRay(this.Input.GetMousePositionRanged(), out rayBegin, out rayEnd);
            GameEntity collidedEntity;
            this.Mission.Scene.RayCastForClosestEntityOrTerrain(rayBegin, rayEnd, out float _, out collidedEntity, 0.3f, BodyFlags.CommonFocusRayCastExcludeFlags);
            while ((NativeObject) collidedEntity != (NativeObject) null && (NativeObject) collidedEntity.Parent != (NativeObject) null)
                collidedEntity = collidedEntity.Parent;
            if (collidedEntity != null)
            {
                InformationManager.DisplayMessage(new InformationMessage(collidedEntity.ToString()));
            }
         
            return collidedEntity;
        }

 
        private void GetCursorIntersectionPoint(
            ref Vec3 clippedMouseNear,
            ref Vec3 clippedMouseFar,
            out float closestDistanceSquared,
            out Vec3 intersectionPoint,
            ref PathFaceRecord currentFace,
            BodyFlags excludedBodyFlags = BodyFlags.CommonFocusRayCastExcludeFlags)
        {
            double num = (double) (clippedMouseFar - clippedMouseNear).Normalize();
            this.MissionScreen.SceneLayer.SceneView.GetScene().GetBoundingBox(out Vec3 _, out Vec3 _);
            Vec3 direction = clippedMouseFar - clippedMouseNear;
            float maxDistance = direction.Normalize();
            this._mouseRay.Reset(clippedMouseNear, direction, maxDistance);
            intersectionPoint = Vec3.Zero;
            closestDistanceSquared = 1E+12f;
            float collisionDistance;
            if (this.MissionScreen.SceneLayer.SceneView.GetScene().RayCastForClosestEntityOrTerrain(clippedMouseNear, clippedMouseFar, out collisionDistance, 0.01f, excludedBodyFlags))
            {
                closestDistanceSquared = collisionDistance * collisionDistance;
                intersectionPoint = clippedMouseNear + direction * collisionDistance;
            }
            currentFace = Campaign.Current.MapSceneWrapper.GetFaceIndex(intersectionPoint.AsVec2);
        }
        
    }
    
    
}
