using Game1.Engine;
using Microsoft.Xna.Framework.Graphics;
using Myra.Graphics2D.TextureAtlases;
using Myra.Graphics2D.UI;
using System;

namespace Game1.GUI
{
    internal class MainMenu
    {
        private System.Windows.Forms.SaveFileDialog SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
        public MenuItem _menuOpenFile;
        public MenuItem _menuSaveFile;
        public MenuItem _menuFile;
        public MenuItem _menuCreateFile;
        public HorizontalMenu _mainMenu;

        public static int _height = 23;

        public void BuildUI()
        {
            var _grid = new Grid();

            GUI.grid.Widgets.Add(_grid);
            _grid.Height = _height;
            _grid.Width = GUI.grid.Width;


            _menuFile = new MenuItem();
            _menuFile.Text = "File";


            _mainMenu = new HorizontalMenu();
            _mainMenu.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Stretch;


            _menuCreateFile = new MenuItem();
            _menuCreateFile.Text = "Создать";

            _menuSaveFile = new MenuItem();
            _menuSaveFile.Text = "Сохранить";

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
                label.Left = (int)(panel.Width / 2 - label.Text.Length / 2 * 9);

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
                        Maps.Add(map);
                        GUI.grid.Widgets.Remove(panel);
                    }
                };

                panel.Widgets.Add(label);
                panel.Widgets.Add(textbox);
                panel.Widgets.Add(button);
                GUI.grid.Widgets.Add(panel);

            };
            _menuSaveFile.Selected += (e, a) =>
            {
                if (SaveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                    return;
                string fileName = SaveFileDialog.FileName;

                int x = 0; 
                int y = 0;
                string mapdata = "";

                
                
            };
            _menuFile.Items.Add(_menuCreateFile);
            _menuFile.Items.Add(_menuSaveFile);

            _mainMenu.Items.Add(_menuFile);
            _grid.Widgets.Add(_mainMenu);
        }



    }
}
