using Game1.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Game1
{
    internal class Map 
    {
        public static Tile[,] tiles = new Tile[0, 0]; //Хранилище тайлов
        

        public static bool notInTile = true; //Находится ли мышка в области тайла
        public static int id = 0; //

        public Map(int _size, GraphicsDevice gr, SpriteBatch sb)
        {
            tiles = new Tile[_size, _size];

            //Добавляем тайлы на карту
            for (int i = 0; i < _size; i++)
            {
                for (int k = 0; k < _size; k++)
                {
                    tiles[i, k] = new Tile(new Vector2(32 * i, 32 * k), gr, sb);
                }
            }
        }

        public static Vector2 UpdateCamera { get; private set; }

        public static void Draw()
        {
            if(tiles.Length > 1)
                for (int i = 0; i < tiles.GetLength(0); i++)
                {
                    for (int k = 0; k < tiles.GetLength(1); k++)
                    {
                        tiles[i, k].Draw();
                    }
                }
        }



        public static void EnteredTile()
        {
            //Получаем координаты мыши с учётом смещения камеры
            Vector2 mousePos = UpdateMouse.WorldPosition;
            MouseState mouseState = Mouse.GetState();
            //Создаём область с координатами мыши, с которой будет сталкиваться тайл
            Rectangle rect = new Rectangle((int)mousePos.X, (int)mousePos.Y, 1, 1);
            if (id == 0)
            {
                foreach (var sprite in Map.tiles)
                {
                    if (rect.Intersects(sprite.GetRect()))
                    {

                        id = sprite.Id;
                        GetTileById(id).EnterMouse();
                    }
                }
            }
            else
            {
                if (rect.Intersects(GetTileById(id).GetRect()))
                {
                    //Проверка на нажатие лкм
                    if (mouseState.LeftButton == ButtonState.Pressed && !GetTileById(id).isClickDown)
                    {
                        GetTileById(id).OnClick();
                        GetTileById(id).isClickDown = true;
                        GetTileById(id).isClickUp = false;
                    }
                    if (mouseState.LeftButton == ButtonState.Released && !GetTileById(id).isClickDown)
                    {
                        
                    }
                    if (mouseState.LeftButton == ButtonState.Released &&  !GetTileById(id).isClickUp)
                    {
                        GetTileById(id).isClickDown = false;
                        GetTileById(id).isClickUp = true;
                    }
                    return;
                }
                else
                {
                    GetTileById(id).LeaveMouse();
                    id = 0;
                }
            }
        }

        public static Tile GetTileById(int id)
        {
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int k = 0; k < tiles.GetLength(0); k++)
                {
                    if (tiles[i, k].Id == id)
                    {
                        return tiles[i, k];
                    }
                }
            }

            return tiles[0, 0];
        }


    }
}
