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
	
namespace FlappyBird
{
	public class AppMain
	{

		
		private enum scene : int
		{
			game, menu
		};
		
		
		private static Sce.PlayStation.HighLevel.UI.Scene 			gameUI;
		private static Sce.PlayStation.HighLevel.UI.Scene 			blankUI;
		private static Sce.PlayStation.HighLevel.UI.Label			scoreLabel;
		
		private static GameScene	gameScene;
		private static MenuScene	menuScene;
		private static scene		currentScene;
		
		private static Timer		deltaTimer;
		private static double		deltaTime;
		
				
		public static void Main (string[] args)
		{
			//Start the clock :3
			deltaTimer = new Timer();
			
			//Blast out some variables
			Initialize();
			
			//Game loop
			bool quitGame = false;
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
			//Initialise simple variables
			//touchedLastFrame = false;
			
			
			//Set up director and UISystem.
			Director.Initialize ();
			UISystem.Initialize(Director.Instance.GL.Context);
			
			//Set menu scene
			menuScene = new MenuScene();
			menuScene.GetScene().Camera.SetViewFromViewport();
			
			//Set game scene
			gameScene = new GameScene();
			gameScene.GetScene().Camera.SetViewFromViewport();
			
			
			
			
			//Set the ui scene.
			blankUI = new Sce.PlayStation.HighLevel.UI.Scene();
			
			gameUI = new Sce.PlayStation.HighLevel.UI.Scene();
			Panel panel  = new Panel();
			panel.Width  = Director.Instance.GL.Context.GetViewport().Width;
			panel.Height = Director.Instance.GL.Context.GetViewport().Height;
			scoreLabel = new Sce.PlayStation.HighLevel.UI.Label();
			//scoreLabel.SetSize(100.0f,100.0f);
			scoreLabel.HorizontalAlignment = HorizontalAlignment.Center;
			scoreLabel.VerticalAlignment = VerticalAlignment.Top;
			scoreLabel.SetPosition(
				Director.Instance.GL.Context.GetViewport().Width/2 - scoreLabel.Width/2,
				Director.Instance.GL.Context.GetViewport().Height*0.1f - scoreLabel.Height/2);
			//scoreLabel.Text = "" + currentScore;
			panel.AddChildLast(scoreLabel);
			gameUI.RootWidget.AddChildLast(panel);
			
			
			//Run the scene.
			currentScene = scene.menu;
			Director.Instance.RunWithScene(menuScene.GetScene(), true);
		}
		
		
		
		public static void StartGameScene()
		{
			currentScene = scene.game;
			UISystem.SetScene(gameUI);
			Director.Instance.ReplaceScene(new TransitionSolidFade(gameScene.GetScene())
			                              { Duration = 1.0f, Tween = (x) => Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.PowEaseOut( x, 3.0f )} 	
			                              );
		}
		

		
		public static void StartMenuScene()
		{
			currentScene= scene.menu;
			UISystem.SetScene(blankUI);
			Director.Instance.ReplaceScene(menuScene.GetScene());
		}
		
		
		
		public static void Update(double deltaTime)
		{
			switch(currentScene)
			{
				case scene.game:
					gameScene.Update (deltaTime);
					scoreLabel.Text = "" + gameScene.GetScore();
				break;
				
				case scene.menu:
					menuScene.Update (deltaTime);
					if(menuScene.NextScene())
					{
						StartGameScene ();
					}
				break;
				
			}
		}
		
	}
}
