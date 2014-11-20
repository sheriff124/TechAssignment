using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;
using Sce.PlayStation.Core.Audio;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.HighLevel.UI;
	
namespace SPACE
{
	public class AppMain
	{

		private enum Scene { Game, Menu };
		
		private static Sce.PlayStation.HighLevel.UI.Scene 			gameUI;
		private static Sce.PlayStation.HighLevel.UI.Scene 			blankUI;
		private static Sce.PlayStation.HighLevel.UI.Label			scoreLabel;
		
		private static GameScene	gameScene;
		private static MenuScene	menuScene;
		private static Scene		currentScene;
		
		private static Timer		deltaTimer;
		private static double		deltaTime;
		
		
				
		public static void Main (string[] args)
		{
			Initialize();
			
			bool quitGame = false;
			deltaTimer = new Timer();
			
			//Game loop
			while (!quitGame) 
			{
				deltaTime = deltaTimer.Milliseconds();
				
				Update (deltaTime);
				Director.Instance.Update();
				Director.Instance.Render();
				UISystem.Render();
				
				Director.Instance.GL.Context.SwapBuffers();
				Director.Instance.PostSwap();
			}
			
			Director.Terminate ();
		}
		
		
		public static void Initialize ()
		{
			Director.Initialize ();
			UISystem.Initialize(Director.Instance.GL.Context);
			
			//Create Scenes
			gameScene = new GameScene();
			gameScene.GetScene().Camera.SetViewFromViewport();
			menuScene = new MenuScene();
			menuScene.GetScene().Camera.SetViewFromViewport();
			
			//Create the UI Stuff 
			blankUI = new Sce.PlayStation.HighLevel.UI.Scene();
			gameUI = new Sce.PlayStation.HighLevel.UI.Scene();
			
			Panel panel  = new Panel();
			panel.Width  = Director.Instance.GL.Context.GetViewport().Width;
			panel.Height = Director.Instance.GL.Context.GetViewport().Height;
			scoreLabel = new Sce.PlayStation.HighLevel.UI.Label();
			scoreLabel.HorizontalAlignment = HorizontalAlignment.Center;
			scoreLabel.VerticalAlignment = VerticalAlignment.Top;
			scoreLabel.SetPosition(	Director.Instance.GL.Context.GetViewport().Width/2 - scoreLabel.Width/2,
									Director.Instance.GL.Context.GetViewport().Height*0.1f - scoreLabel.Height/2);
			panel.AddChildLast(scoreLabel);
			gameUI.RootWidget.AddChildLast(panel);
			
			//Make the Menu the first scene
			currentScene = Scene.Menu;
			
			//Run the current scene.
			Director.Instance.RunWithScene(menuScene.GetScene(), true);
		}
		
		
		
		public static void StartGameScene()
		{
			currentScene = Scene.Game;
			UISystem.SetScene(gameUI);
			Director.Instance.ReplaceScene(new TransitionSolidFade(gameScene.GetScene())
			{ Duration = 1.0f, Tween = (x) => Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.PowEaseOut( x, 3.0f )});
		}
		

		
		public static void StartMenuScene()
		{
			currentScene= Scene.Menu;
			UISystem.SetScene(blankUI);
			Director.Instance.ReplaceScene(menuScene.GetScene());
		}
		
		
		
		public static void Update(double deltaTime)
		{
			switch(currentScene)
			{
				case Scene.Game:
					gameScene.Update (deltaTime);
					scoreLabel.Text = "" + gameScene.GetScore();
				break;
				
				case Scene.Menu:
					menuScene.Update (deltaTime);
					if(true)
					{
						StartGameScene ();
					}
				break;
				
			}
		}
		
	}
}
