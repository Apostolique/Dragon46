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

        public static float MusicVolume = 0.05f;
        public static float SFXVolume = 0.2f;

        public static void UpdateVolume()
        {
            SoundManager.SetVolume((int)SoundType.Music, MusicVolume);
            SoundManager.SetVolume((int)SoundType.SFX, SFXVolume);
            var uiVolume = (SFXVolume == 0 ? 0 : SFXVolume + 0.2f);
            if (uiVolume > 1)
                uiVolume = 1;
            SoundManager.SetVolume((int)SoundType.UI, uiVolume);
        }

        public static void Setup(ContentManager content) {
            Content = content;

            SoundManager = new SoundManager();
            UpdateVolume();

            Infinite = content.Load<Effect>("infinite");
            Sky = content.Load<Texture2D>("sky");
            Mountains = content.Load<Texture2D>("mountains_2");
            Clouds = content.Load<Texture2D>("clouds");
            Ground = content.Load<Texture2D>("grass_and_path");
            Shrub1 = content.Load<Texture2D>("shrub_1");
            Shrub2 = content.Load<Texture2D>("shrub_2");
            Shrub3 = content.Load<Texture2D>("shrub_3");
            Shrub4 = content.Load<Texture2D>("shrub_4");
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
        public static Texture2D Clouds;
        public static Texture2D Ground;
        public static Texture2D Shrub1;
        public static Texture2D Shrub2;
        public static Texture2D Shrub3;
        public static Texture2D Shrub4;
        public static Texture2D Tree;

        public static DynamicSpriteFont UIFont;

        public static Dictionary<CharacterType, CharacterSprite> CharacterSprites;
    }
}