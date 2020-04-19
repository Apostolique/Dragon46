using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpriteFontPlus;

namespace GameProject {
    public enum SoundType
    {
        Music, SFX, UI
    }

    public static class Assets {
        public static ContentManager Content;
        public static SoundManager SoundManager;

        public static void Setup(ContentManager content) {
            Content = content;

            SoundManager = new SoundManager();
            SoundManager.SetVolume((int)SoundType.Music, 0.5f);
            SoundManager.SetVolume((int)SoundType.SFX, 0.5f);
            SoundManager.SetVolume((int)SoundType.UI, 0.5f);

            Infinite = content.Load<Effect>("infinite");
            Sky = content.Load<Texture2D>("sky");
            Mountains = content.Load<Texture2D>("mountains_2");
            Ground = content.Load<Texture2D>("grass_and_path");
            Bush = content.Load<Texture2D>("bush");
            Tree = content.Load<Texture2D>("tree");

            UIFont = DynamicSpriteFont.FromTtf(TitleContainer.OpenStream("Content/Lato-Bold.ttf"), 16);

            CharacterSprites = new Dictionary<CharacterType, CharacterSprite>()
            {
                {
                    CharacterType.Warrior,
                    new CharacterSprite()
                    {
                        Texture = content.Load<Texture2D>("warrior"),
                        HitTexture = content.Load<Texture2D>("warrior_hit")
                    }
                },
                {
                    CharacterType.Archer,
                    new CharacterSprite()
                    {
                        Texture = content.Load<Texture2D>("archer"),
                        HitTexture = content.Load<Texture2D>("archer_hit")
                    }
                },
                {
                    CharacterType.Wizard,
                    new CharacterSprite()
                    {
                        Texture = content.Load<Texture2D>("wizard"),
                        HitTexture = content.Load<Texture2D>("wizard_hit")
                    }
                },
                {
                    CharacterType.Cleric,
                    new CharacterSprite()
                    {
                        Texture = content.Load<Texture2D>("cleric"),
                        HitTexture = content.Load<Texture2D>("cleric_hit")
                    }
                },

                {
                    CharacterType.GoblinMinion,
                    new CharacterSprite()
                    {
                        Texture = content.Load<Texture2D>("goblin_minion"),
                        HitTexture = content.Load<Texture2D>("goblin_minion_hit")
                    }
                },
                {
                    CharacterType.GoblinBrute,
                    new CharacterSprite()
                    {
                        Texture = content.Load<Texture2D>("goblin_brute"),
                        HitTexture = content.Load<Texture2D>("goblin_brute_hit")
                    }
                },
                {
                    CharacterType.GoblinShaman,
                    new CharacterSprite()
                    {
                        Texture = content.Load<Texture2D>("goblin_shaman"),
                        HitTexture = content.Load<Texture2D>("goblin_shaman_hit")
                    }
                },
                {
                    CharacterType.Dragon,
                    new CharacterSprite()
                    {
                        Texture = content.Load<Texture2D>("dragon"),
                        HitTexture = content.Load<Texture2D>("dragon_hit")
                    }
                },
            };
        }

        public static Effect Infinite;
        public static Texture2D Sky;
        public static Texture2D Mountains;
        public static Texture2D Ground;
        public static Texture2D Bush;
        public static Texture2D Tree;

        public static DynamicSpriteFont UIFont;

        public static Dictionary<CharacterType, CharacterSprite> CharacterSprites;
    }
}