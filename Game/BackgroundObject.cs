using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject {
    public class BackgroundObjects {
        public BackgroundObjects(int x, int y, float z, float scale, Texture2D texture, Core.AnimationStyle style, int depth = 0) {
            Z = z;
            Texture = texture;
            _aabb = new Rectangle(x, y + (int)(Texture.Height * scale), (int)(Texture.Width * scale), (int)(Texture.Height * scale));
            Depth = depth;
            _order = _nextOrder++;

            _animationStyle = style;

            if (style == Core.AnimationStyle.Cloud) {
                _tween = new Tween(1000000, x, x - 20000, EasingFunctions.Linear);
            } else if (style == Core.AnimationStyle.Tree) {
                _tween = new Tween(Core.R.Next(6000, 10000), -MathF.PI / 32, MathF.PI / 32, EasingFunctions.CubicInOut, true);
                _sway = _aabb.Height * MathF.Sin(MathF.PI / 32);
            }
        }

        public Texture2D Texture {
            get;
            set;
        }
        public float Angle {
            get;
            set;
        }

        public float Z {
            get;
            set;
        } = 0;
        public int Depth {
            get;
            set;
        }

        /// <summary>Top left corner of the area the object can draw in, sway included.</summary>
        public Vector2 XY => new Vector2(_aabb.X - _aabb.Width / 2f - _sway, _aabb.Y - _aabb.Height);
        /// <summary>Size of the area the object can draw in, sway included.</summary>
        public Vector2 Size => new Vector2(_aabb.Width + _sway * 2f, _aabb.Height);
        public bool Moves => _animationStyle == Core.AnimationStyle.Cloud;

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
            s.Draw(Texture, _aabb, Texture.Bounds, Color.White, Angle, new Vector2(Texture.Width / 2, Texture.Height), SpriteEffects.None, 0);
        }

        /// <summary>Sorts by depth, then by creation order so that equal depths don't flicker.</summary>
        public static int CompareDepth(BackgroundObjects a, BackgroundObjects b) {
            int depth = a.Depth.CompareTo(b.Depth);
            return depth != 0 ? depth : a._order.CompareTo(b._order);
        }

        Rectangle _aabb;
        Tween _tween;
        Core.AnimationStyle _animationStyle;
        float _sway;
        int _order;

        static int _nextOrder;
    }
}
