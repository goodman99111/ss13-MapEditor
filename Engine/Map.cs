using Game1.Engine;
using Game1.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game1
{
    class Map
    {
        private Tile[,] tiles;
        GraphicsDevice graphicsDevice;
        SpriteBatch spriteBatch;
        MapEvents mapEvents;

        public Map(int _size, GraphicsDevice _graphicsDevice, SpriteBatch _spriteBatch)
        {
            tiles = new Tile[_size, _size];
            graphicsDevice = _graphicsDevice;
            spriteBatch = _spriteBatch;
            mapEvents = new MapEvents(this);
            CreateTiles();
            

        }

        //Создаём и добавляем тайлы в массив
        private void CreateTiles()
        {
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int k = 0; k < tiles.GetLength(1); k++)
                {
                    tiles[i, k] = new Tile(new Vector2(32 * i, 32 * k), graphicsDevice, spriteBatch);
                }
            }
        }
        //
        public void Update()
        {
            mapEvents.Update();
        }
        //Отрисовка каждого тайла на карте
        public void Draw()
        {
            foreach (Tile tile in tiles)
                tile.Draw();
        }
        //Возврат тайла по его ID
        public Tile GetTileById(int id)
        {
            foreach(Tile tile in tiles)
            {
                if (tile.Id == id)
                    return tile;
            }
            return null;
        }
        public Tile[,] GetAllTiles()
        {
            return tiles;
        }
        

    }
    /*
    internal class Map2 
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
                    if (mouseState.RightButton == ButtonState.Pressed && !GetTileById(id).isClickDown)
                    {
                        //ContextMenu.Show(Map.GetTileById(id));
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
    */
}
