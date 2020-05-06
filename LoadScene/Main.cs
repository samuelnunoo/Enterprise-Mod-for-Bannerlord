using System;
using System.Collections.Generic;
using System.Diagnostics;
using LoadScene.ModelObjects;
using LoadScene.NavigationElements;
using LoadScene.SceneEditor;
using LoadScene.Tests;
using SandBox;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using UIExtenderLib;

namespace LoadScene
{
    public class SubModule : MBSubModuleBase
    {

        //Class Variables
        private UIExtender _extender;
        private int _Key;
        private bool bounce;
        private GameEntity _item;
        private bool _continue;
        private EditorSceneHandler _editorhandler;
        private GameEntity _selectedEntity;
        
     
        
        //Initialize UIExtender and CustomMission Instance 
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            
            _extender = new UIExtender("ExampleUIMod");
            _extender.Register();
            
            this._editorhandler = new CustomMissionManagerHandler();
            


        }

        
        //Initialize CampaignBehaviors || Replace Here?
        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            
            //Check GameType
            Campaign campaign = game.GameType as Campaign;
            bool flag = (campaign == null);
            if (!flag)
            {
                //Set Variable 
                CampaignGameStarter gameInitializer = (CampaignGameStarter) gameStarterObject;
                
                
                //Add and Replace Here 
                    this.AddBehaviors(gameInitializer);
                  
                    
                    


            }



        }

        
        //Tick Events 
        protected override void OnApplicationTick(float dt)
        {
            try
            {

                // Check if Game is Running
                if (Game.Current == null || Game.Current.CurrentState != Game.State.Running)
                    return;

                
                // Set CustomMissionManager
                if (Game.Current.GameType is Campaign gameType){
                    if (!(gameType.CampaignMissionManager is CustomMissionManagerHandler))
                    {
                        gameType.CampaignMissionManager = (CustomMissionManagerHandler) new CustomMissionManagerHandler();
                    }
                    
                    }
                
                
                // Load Scene 
                if (Input.IsKeyPressed(InputKey.F10))
                {
                     this._editorhandler.LoadSceneEditor();
             

                }

                
                // Check if in Scene 
                if (GameStateManager.Current.ActiveState is MissionState)
                {
                 
                    
                    
                    //Place Item
                    if (Input.IsKeyPressed((InputKey.F9)))
                    {
                        this.PlaceItem();
                        
                    }

                  

                    //Testing This 
                    if (Input.IsKeyDown(InputKey.Q) && this._item !=null)
                    {
                        if (bounce == false)
                        {
                            bounce = true;
                            this._continue = true;
                            this.MoveItem(_item);
                        }
                       
                    }

                    if (Input.IsKeyReleased(InputKey.Q))
                    {
                        _continue = false;
                        bounce = false;
                    }

                }
            }
            catch
            {

            }
        }

      

        //Verify UIExtenderLib
        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();
            _extender.Verify();
        }
        

        //Helper for CampaignBehaviors Initialze
        private void AddBehaviors(CampaignGameStarter gameInitializer)
        {
            gameInitializer.AddBehavior(EnterpriseCampaignBehavior.Instance);
        }

      
        //Test Items 
        public void PlaceItem()
        
        {
            var x = 0;
            var y = 0;
            var z = 0;

            
            
           var frame =  Mission.Current.GetCameraFrame();
           var cx = x + frame.origin.x;
           var cy = y + frame.origin.y;
           var cz = z + frame.origin.z;
           Vec3 position = new Vec3(cx, cy, cz);
           Mat3 rotation = frame.rotation;
           MatrixFrame matrix = new MatrixFrame(rotation,position);
           GameEntity.Instantiate(Mission.Current.Scene, "ship_a", matrix);




        }

    

        
        // 
        public void MoveItem(GameEntity item)
        {

            var x = 10;
            var y = 0;
            var z = 0;

            
            
            var frame =  Mission.Current.GetCameraFrame();
            var cx = x + frame.origin.x;
            var cy = y + frame.origin.y;
            var cz = z + frame.origin.z;
            Vec3 position = new Vec3(cx, cy, cz);
            
            item.SetLocalPosition(position);
              
        }
        
        
        protected void ReplaceModel<TBaseType, TChildType>(IGameStarter gameStarterObject) where TBaseType : GameModel where TChildType : TBaseType
        {
            IList<GameModel> models = gameStarterObject.Models as IList<GameModel>;
            bool flag = models == null;
            if (flag)
            {
                Trace.WriteLine("Models was not a list");
            }
            else
            {
                bool found = false;
                for (int index = 0; index < models.Count; index++)
                {
                    bool flag2 = models[index] is TBaseType;
                    if (flag2)
                    {
                        found = true;
                        bool flag3 = models[index] is TChildType;
                        if (flag3)
                        {
                            Trace.WriteLine("Child model " + typeof(TChildType).Name + " found, skipping.");
                        }
                        else
                        {
                            Trace.WriteLine("Base model " + typeof(TBaseType).Name + " found. Replacing with child model " + typeof(TChildType).Name);
                            models[index] = Activator.CreateInstance<TChildType>();
                        }
                    }
                }
                bool flag4 = !found;
                if (flag4)
                {
                    Trace.WriteLine("Base model " + typeof(TBaseType).Name + " was not found. Adding child model " + typeof(TChildType).Name);
                    gameStarterObject.AddModel(Activator.CreateInstance<TChildType>());
                }
            }
        }
    }
    
}



