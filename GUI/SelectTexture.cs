using Microsoft.Xna.Framework;
using Myra.Graphics2D.TextureAtlases;
using Myra.Graphics2D.UI;
using System.IO;

namespace Game1.GUI
{
    internal class SelectTexture
    {
        private Panel panel;
        private Tree tree;
        private TreeNode text;
        private DirectoryInfo dir;
        private Image image;

        public void BuildUI(Grid grid)
        {

            panel = new Panel();
            panel.Width = 160;
            panel.Height = 500;
            panel.Top = MainMenu._height;
            panel.Background = new TextureRegion(Textures.GetTexture("MENUbackground"), new Rectangle((int)panel.Left, 0, (int)panel.Width, (int)panel.Height));
            panel.Left = (int)(GUI.WinWidth - panel.Width);

            tree = new Tree();
            tree.HasRoot = false;

            image = new Image();
            image.Width = 32;
            image.Height = 32;
            image.Top = 200;
            image.Left = (int)(panel.Width / 2 - image.Width / 2);
            image.Renderable = new TextureRegion(Textures.GetTexture("dark"));

            Label label = new Label();
            label.Text = "Preview";
            label.Left = (int)(panel.Width / 2 - image.Width / 2) -10;
            label.Top = 180;



            text = new TreeNode(tree);

            dir = new DirectoryInfo(@"textures\");
            foreach (var item in dir.GetDirectories())
            {
                if (item.Name == "GUI")//папки которые нам не нужны
                {
                    continue;
                }

                var DirNode = tree.AddSubNode(item.Name);
                foreach (var file in item.GetFiles())
                {
                    var FileNode = DirNode.AddSubNode(file.Name.Remove(file.Name.Length - 4, 4));

                    FileNode.TouchDown += (s, a) =>
                    {
                        Textures.activeTexture = Textures.GetTexture(file.Name.Remove(file.Name.Length - 4, 4));
                        image.Renderable = new TextureRegion(Textures.activeTexture);

                    };
                }
            }



            panel.Widgets.Add(tree);
            panel.Widgets.Add(image);
            panel.Widgets.Add(label);
            grid.Widgets.Add(panel);
            

        }
    }
}
