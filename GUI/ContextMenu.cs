using Microsoft.Xna.Framework.Graphics;
using Myra.Graphics2D.TextureAtlases;
using Myra.Graphics2D.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.GUI
{
    class ContextMenu
    {
        private static  Grid localGrid = new Grid();
        private static List<Image> list = null;
        public static void Show(Tile tile)
        {
            list = new List<Image>();
            int count = 0;

            localGrid.Height = 34;
            //ширина контексного меню равна ширине всех текстур в тайле
            localGrid.Width = tile.texture2Ds.Count * 32 + 3;

            localGrid.Left = (int)(tile.Position.X) + 35;
            localGrid.Top = (int)(tile.Position.Y) + 35;

            localGrid.Background = new TextureRegion(Textures.GetTexture("border"));
            foreach (Texture2D texture in tile.texture2Ds)
            {
                //текстуру рамки не добавляем в содержащиеся текстуры
                if (texture.Equals(Textures.GetTexture("border")))
                    continue;

                var image = new Image();
                image.Left = 32 * count;
                image.Top = 1;
                image.Renderable = new TextureRegion(texture);

                list.Add(image);
                count++;
            }
            foreach(Image image in list)
            {
                localGrid.Widgets.Add(image);
            }

            GUI.grid.Widgets.Add(localGrid);
        }
    }
}
