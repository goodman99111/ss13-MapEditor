using Microsoft.Xna.Framework.Graphics;
using Myra.Graphics2D.TextureAtlases;
using Myra.Graphics2D.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Game1.GUI
{
    class GUI
    {
        Texture2D preview; //отображение текстуры которую мы выбрали в меню
        MainMenu _menu;
        SelectTexture _selectTexture;
        public static int WinWidth;
        public static int WinHeight;

        public static Grid grid = new Grid();

        public GUI(GraphicsDeviceManager gr)
        {
            WinWidth = gr.PreferredBackBufferWidth;
            WinHeight = gr.PreferredBackBufferHeight;
            _menu = new MainMenu();
            _selectTexture = new SelectTexture();
            LoadGUI();
           
        }

        public void LoadGUI()
        {
            

            _menu.BuildUI();
            _selectTexture.BuildUI();

            Desktop.Widgets.Add(grid);
        }
    }
}
