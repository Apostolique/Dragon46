using System;
using Dcrew.MonoGame._2D_Spatial_Partition;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject {
    public class BackgroundObjects : IAABB {
        public BackgroundObjects(int x, int y, float z, float scale, Texture2D texture, Core.AnimationStyle style, int depth = 0) {
            Z = z;
            Texture = texture;
            _aabb = new Rectangle(x, y + (int)(Texture.Height * scale), (int)(Texture.Width * scale), (int)(Texture.Height * scale));
            Depth = depth;

            _animationStyle = style;

            if (style == Core.AnimationStyle.Cloud) {
                _tween = new Tween(1000000, x, x - 20000, EasingFunctions.Linear);
            } else if (style == Core.AnimationStyle.Tree) {
                _tween = new Tween(Core.R.Next(6000, 10000), -MathF.PI / 32, MathF.PI / 32, EasingFunctions.CubicInOut, true);
            }
        }

        public Texture2D Texture {
            get;
            set;
        }
        public Rectangle AABB => _aabb;
        public float Angle {
            get;
            set;
        }
        public Vector2 Origin => Vector2.Zero;

        public float Z {
            get;
            set;
        } = 0;
        public int Depth {
            get;
            set;
        }

        public void Update(GameTime gameTime) {
            if (_tween != null) {
                _tween.Update(gameTime);
                if (_animationStyle == Core.AnimationStyle.Cloud) {
                    _aabb.X = (int)_tween.Value;
                } else if (_animationStyle == Core.AnimationStyle.Tree) {
                    Angle = _tween.Value;
                }
            }
        }

        public void Draw(SpriteBatch s) {
            s.Draw(Texture, AABB, Texture.Bounds, Color.White, Angle, new Vector2(Texture.Width / 2, Texture.Height), SpriteEffects.None, 0);
        }

        Rectangle _aabb;
        Tween _tween;
        Core.AnimationStyle _animationStyle;
    }
}