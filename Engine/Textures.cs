using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    public class Textures
    {
        private static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        public static Texture2D activeTexture;

        public Texture2D this[string nameTexture]
        {
            get
            {
                //Texture2D texture = textures[nameTexture];
                return textures[nameTexture];
            }
        }

        public static void LoadTextures(GraphicsDevice gr)
        {
            DirectoryInfo dir = new DirectoryInfo(@"textures\");
            foreach (var item in dir.GetDirectories())
            {
                foreach (var files in item.GetFiles())
                {
                    var texture = Texture2D.FromStream(gr, File.OpenRead($"textures\\{item.Name}\\{files.Name}"));
                    textures.Add(files.Name.Remove(files.Name.Length - 4, 4), texture);
                }
            }
        }

        public static Texture2D GetTexture(string name)
        {
            return textures[name];
        }
    }
}
