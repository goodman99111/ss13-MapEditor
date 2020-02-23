using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Engine
{
    public class Camera2d
    {
        public Matrix _transform;
        public Vector2 _pos;

        public Camera2d()
        {
            _pos = new Vector2(-170,-170);
        }

        public void Move(Vector2 amount)
        {
            _pos += amount;
        }

        public Vector2 Pos
        {
            get { return _pos; }
            set { _pos = value; }
        }

        public void Update()
        {
            if(Keyboard.GetState().IsKeyDown(Keys.A))
            {
                _pos.X--;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                _pos.X++;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                _pos.Y--;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                _pos.Y++;
            }
        }

        public Matrix get_transformation(GraphicsDevice graphicsDevice)
        {
            _transform = Matrix.CreateTranslation(new Vector3(-_pos.X, -_pos.Y, 0));
            UpdateMouse.WorldPosition = Vector2.Transform(Mouse.GetState().Position.ToVector2(), Matrix.Invert(_transform));
            return _transform;


        }

    }
}
