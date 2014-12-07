using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace LD31
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Window : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Menu MainMenu;
        SpriteLibrary SpriteLibrary;
        MonsterFactory MonsterFactory;
        GameAudio GameAudio;
        System.Drawing.Point Location;
    
        public Window()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            SpriteLibrary = new SpriteLibrary();
            MonsterFactory = new MonsterFactory();
            GameAudio = new GameAudio();
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
            MainMenu = new Menu(Content);
            SpriteLibrary.Initialize(Content);
            MonsterFactory.Initialize();
            GameAudio.Initialize(Content);
            LD31.GameAudio = GameAudio;
            LD31.SpriteLibrary = SpriteLibrary;
            LD31.MonsterFactory = MonsterFactory;
            this.MainMenu.Initialize();
            this.Window.Title = "LD31 - Through The Windows - PeeWeeK";
            Globals.SCREEN_WIDTH = (ushort)GraphicsDevice.Adapter.CurrentDisplayMode.Width;
            Globals.SCREEN_HEIGHT = (ushort)GraphicsDevice.Adapter.CurrentDisplayMode.Height;
            this.SetBounds(new Rectangle(Globals.SCREEN_WIDTH / 2 - 400, Globals.SCREEN_HEIGHT / 2 - 240, 800, 480));
        }


        public void SetBounds(Rectangle r)
        { 
            graphics.PreferredBackBufferWidth = r.Width;
            graphics.PreferredBackBufferHeight = r.Height;
            graphics.ApplyChanges();
            this.Location = new System.Drawing.Point(r.X, r.Y);
            UpdateLocation();

        }

        public void UpdateLocation()
        {
            System.Windows.Forms.Form MainForm = (System.Windows.Forms.Form)System.Windows.Forms.Control.FromHandle(this.Window.Handle);
            MainForm.Location = this.Location;
        }

        public void SetMenuResolution()
        {
            graphics.PreferredBackBufferWidth = MainMenu.ViewWidth;
            graphics.PreferredBackBufferHeight = MainMenu.ViewHeight;
            graphics.ApplyChanges();
        }


        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {



            LD31.Input.Update();

            this.UpdateLocation();
            

            switch (LD31.Game.State)
            {
                case GameState.Menu: MainMenu.Update(); break;
                case GameState.Game: LD31.Dungeon.Update(gameTime.ElapsedGameTime.Milliseconds * 0.001f);  break;
            }
                

            // TODO: Add your update logic here
            base.Update(gameTime);
            
        }

        

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGray);

            switch (LD31.Game.State)
            {
                case GameState.Menu: MainMenu.Draw(spriteBatch); break;
                case GameState.Game: LD31.Dungeon.Draw(spriteBatch); break;
            }
            

            base.Draw(gameTime);
        }

    }
}
