

using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Engine.GauntletUI;
using System.Reflection;
using TaleWorlds.TwoDimension;
using Texture = TaleWorlds.Engine.Texture;

namespace LoadScene.SceneEditor.UIElements
{
    public class ModelTableauTextureProvider : TextureProvider 
    {

        private ModelTableau _modelTableau;
        private Texture _texture;
        private TaleWorlds.TwoDimension.Texture _providedTexture;



        public ModelTableauTextureProvider()
        {
            this._modelTableau = new ModelTableau();
        }


        public void SetTableau(GameEntity entity)
        {
            this._modelTableau.SetModel(entity);
        }

        private void CheckTexture()
        {
            if (!((NativeObject) this._texture != (NativeObject) this._modelTableau.Texture))
                return;
            this._texture = this._modelTableau.Texture;
            if ((NativeObject) this._texture != (NativeObject) null)
                this._providedTexture = new TaleWorlds.TwoDimension.Texture((TaleWorlds.TwoDimension.ITexture) new EngineTexture(this._texture));
            else
                this._providedTexture = (TaleWorlds.TwoDimension.Texture) null;
        }

        public override TaleWorlds.TwoDimension.Texture GetTexture(
            TwoDimensionContext twoDimensionContext,
            string name)
        {
            this.CheckTexture();
            return this._providedTexture;
        }
        
        
        public override void SetTargetSize(int width, int height)
        {
            base.SetTargetSize(width, height);
            this._modelTableau.SetTargetSize(width, height);
        }
        
        public override void Tick(float dt)
        {
            base.Tick(dt);
            this.CheckTexture();
            //this._modelTableau.OnTick(dt);
        }

    }


}