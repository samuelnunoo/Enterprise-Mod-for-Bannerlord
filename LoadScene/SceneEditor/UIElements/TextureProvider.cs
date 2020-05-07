using System.Reflection;
using TaleWorlds.TwoDimension;

namespace LoadScene.SceneEditor.UIElements
{
    public abstract class TextureProvider
    {
        public virtual void SetTargetSize(int width, int height)
        {
        }

        public abstract Texture GetTexture(TwoDimensionContext twoDimensionContext, string name);

        public virtual void Tick(float dt)
        {
        }

        public virtual void Clear()
        {
        }

        public void SetProperty(string name, object value)
        {
            PropertyInfo property = this.GetType().GetProperty(name);
            if (!(property != (PropertyInfo) null))
                return;
            property.GetSetMethod().Invoke((object) this, new object[1]
            {
                value
            });
        }
    }
}