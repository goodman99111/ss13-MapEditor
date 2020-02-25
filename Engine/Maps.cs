using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Engine
{
    //Класс содержащий все карты и отвечающий за логику их отрисовка
     static class Maps
     {
        private static List<Map> maps = new List<Map>();
        //Карта которая должны отрисовываться
        private static Map Active;

        private static bool ThereIsMap()
        {
            if (Active != null)
                return true;
            else
                return false;
        }
        //Получение активной карты 
        public static Map GetActiveMap()
        {
            return Active;
        }

        //Добавление карты в список
        public static void Add(Map map)
        {
            maps.Add(map);
            Active = map;
        }
         
        //Отрисовка активной карты
        public static void Draw()
        {
            if(ThereIsMap())
                Active.Draw();
        }
        public static void Update()
        {
            if(ThereIsMap())
                Active.Update();
        }


     }
}
