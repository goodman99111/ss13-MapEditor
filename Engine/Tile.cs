using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using Myra.Graphics2D.UI;

namespace Game1
{
    class Tile// Не принимает текстуры предметов. Пример: пол, стена и т.д.
    {
        public List<Texture2D> texture2Ds = new List<Texture2D>(); //список текстур лежащих на тайле
        private Vector2 position; //Позиция тайла
        GraphicsDevice graphicsDevice;
        SpriteBatch spriteBatch;

        Rectangle rect; //ХитБокс тайла
        int id;
        static int count = 1;

        public delegate void MouseEvent();

        public event MouseEvent MouseEntered;
        public event MouseEvent MouseLeaved; 
        public bool isClickDown;
        public bool isClickUp;

        public int Id
        {
            get { return id; }
        }
        public Vector2 Position
        {
            get { return position; }
        }

        public Tile(Vector2 _pos, GraphicsDevice gr, SpriteBatch sb)
        {
            id = count;
            position = _pos;
            graphicsDevice = gr;
            spriteBatch = sb;
            this.AddTexture(Textures.GetTexture("dark")); //дефолтная текстура
            rect = new Rectangle((int)_pos.X, (int)_pos.Y, texture2Ds[0].Width, texture2Ds[0].Height);

            count++;

            MouseEntered += () =>
            {
                this.AddTexture(Textures.GetTexture("border"));
            };
            
            MouseLeaved +=  () =>
            {
                this.DeleteTexture(texture2Ds.Count-1);
            };

        }

        public void AddTexture(string texturePath) //метод добавления структуры для клетки
        {
           texture2Ds.Add(Texture2D.FromStream(graphicsDevice, File.OpenRead(texturePath)));
        }
        public void AddTexture(Texture2D texture)
        {
            texture2Ds.Add(texture);
        }

        public void DeleteTexture(int zIndex)
        {
            texture2Ds.RemoveRange(zIndex, 1);
        }

        //отрисовка текстур
        public void Draw()
        {
            for (int i = 0; i < texture2Ds.Count; i++)
                spriteBatch.Draw(texture2Ds[i], rect, Color.White);
        }

        public void OnClick()
        {
            MouseLeaved();
            if(Textures.activeTexture != null)
                if(!Textures.activeTexture.Equals(texture2Ds[texture2Ds.Count-1]))
                    this.AddTexture(Textures.activeTexture);
            
            MouseEntered();
        }

        public Rectangle GetRect()
        {
            return rect;
        }

        public void LeaveMouse()
        {
            MouseLeaved();
        }

        public void EnterMouse()
        {
            MouseEntered();
        }

        public void MoveEnter()
        {

        }
    }
}
