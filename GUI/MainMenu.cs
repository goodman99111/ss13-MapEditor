using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Engine;
using Microsoft.Xna.Framework;
using Myra.Graphics2D.TextureAtlases;
using Myra.Graphics2D.UI;

namespace Game1.GUI
{
    class MainMenu: VerticalStackPanel
    {
        //public Grid _grid;
        public MenuItem _menuOpenFile;
        public MenuItem _menuSaveFile;
        public MenuItem _menuFile;
        public MenuItem _menuCreateFile;
        public HorizontalMenu _mainMenu;
        
        public static int _height = 23;

        public void BuildUI(Grid grid)
        {
            var _grid = new Grid();

            grid.Widgets.Add(_grid);
            _grid.Height = _height;
            _grid.Width = grid.Width;
            

            _menuFile = new MenuItem();
            _menuFile.Text = "File";


            _mainMenu = new HorizontalMenu();
            _mainMenu.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Stretch;


            _menuCreateFile = new MenuItem();
            _menuCreateFile.Text = "Создать";

            //GUI c настройками создания карты
            _menuCreateFile.Selected += (e, a) =>
            {
                var panel = new Panel();
                panel.Width = 200;
                panel.Height = 100;
                panel.Background = new TextureRegion(Textures.GetTexture("MENUbackground"));
                panel.Left = (int)(GUI.WinWidth / 2 - panel.Width / 2);
                panel.Top = (int)(GUI.WinHeight / 2 - panel.Height / 2);

                var label = new Label();
                label.Text = "Размеры карты";
                label.Top = 10;
                label.Left = (int)(panel.Width / 2 - label.Text.Length / 2*9);

                var textbox = new TextBox();
                textbox.Top = label.Top + 30;
                textbox.Width = 50;
                textbox.TextChanged += (ev, ar) =>
                {
                    int i;
                    if (textbox.Text.Length > 0)
                    {
                        char ch = textbox.Text[textbox.Text.Length - 1];
                        if (!Int32.TryParse(textbox.Text[textbox.Text.Length - 1].ToString(), out i))
                        {
                            textbox.Text = textbox.Text.Remove(textbox.Text.Length - 1, 1);
                        }
                    }
                };

                var button = new TextButton();
                button.Top = textbox.Top + 30;
                button.Left = 70;
                button.Text = "Создать";

                button.Click += (ev, ar) =>
                {
                    if (textbox.Text.Length > 0)
                    {
                        Map map = new Map(Convert.ToInt32(textbox.Text), GameConfig.graphics, GameConfig.spriteBatch);
                        grid.Widgets.Remove(panel);
                    }
                };
                
                panel.Widgets.Add(label);
                panel.Widgets.Add(textbox);
                panel.Widgets.Add(button);
                grid.Widgets.Add(panel);
                
            };

            _menuFile.Items.Add(_menuCreateFile);
            
            _mainMenu.Items.Add(_menuFile);
            _grid.Widgets.Add(_mainMenu);
        }


        
    }
}
