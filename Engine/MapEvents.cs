using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Engine
{
    class MapEvents
    {
        private int id = 0;
        private Map map;

        public MapEvents(Map _map)
        {
            map = _map;
        }
        public void Update()
        {
            EnteredTile();
        }

        public void EnteredTile()
        {
            //Получаем координаты мыши с учётом смещения камеры
            Vector2 mousePos = UpdateMouse.WorldPosition;
            MouseState mouseState = Mouse.GetState();
            //Создаём область с координатами мыши, с которой будет сталкиваться тайл
            Rectangle rect = new Rectangle((int)mousePos.X, (int)mousePos.Y, 1, 1);
            if (id == 0)
            {
                foreach (var sprite in map.GetAllTiles())
                {
                    if (rect.Intersects(sprite.GetRect()))
                    {

                        id = sprite.Id;
                        map.GetTileById(id).EnterMouse();
                    }
                }
            }
            else
            {
                if (rect.Intersects(map.GetTileById(id).GetRect()))
                {
                    //Проверка на нажатие лкм
                    if (mouseState.LeftButton == ButtonState.Pressed && !map.GetTileById(id).isClickDown)
                    {
                        map.GetTileById(id).OnClick();
                        map.GetTileById(id).isClickDown = true;
                        map.GetTileById(id).isClickUp = false;
                    }
                    if (mouseState.RightButton == ButtonState.Pressed && !map.GetTileById(id).isClickDown)
                    {
                        //ContextMenu.Show(Map.GetTileById(id));
                    }
                    if (mouseState.LeftButton == ButtonState.Released && !map.GetTileById(id).isClickUp)
                    {
                        map.GetTileById(id).isClickDown = false;
                        map.GetTileById(id).isClickUp = true;
                    }
                    return;
                }
                else
                {
                    map.GetTileById(id).LeaveMouse();
                    id = 0;
                }
            }
        }
    }
}
