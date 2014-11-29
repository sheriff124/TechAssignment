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
		private enum Scene {Game, Menu};
		
		private static GameScene	gameScene;
		private static MenuScene	menuScene;
		//private static Scene		currentScene;
		
		private static Timer		deltaTimer;
		private static float		deltaTime;
		private static bool			quit;
				
		public static void Main (string[] args)
		{
			Initialize();
			
			quit = false;
			deltaTimer = new Timer();
			
			while (!quit)
			{
				deltaTime = (float)deltaTimer.Milliseconds();
				
				//Update (deltaTime);
				
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
			//Set up director
			Director.Initialize ();
			UISystem.Initialize(Director.Instance.GL.Context);
			
			Sce.PlayStation.HighLevel.UI.Scene blankUI = new Sce.PlayStation.HighLevel.UI.Scene();
			UISystem.SetScene(blankUI);
			
			//Create Game Scenes
			menuScene = new MenuScene();
			gameScene = new GameScene();
			menuScene.GetScene().Camera.SetViewFromViewport();
			gameScene.Camera.SetViewFromViewport();
			
			//Run the scene.
			//currentScene = Scene.Game;
			Director.Instance.RunWithScene(gameScene, true);
		}
		
		public static void Update(float deltaTime)
		{
			//Really basic scene manager
			//Calls update each frame on either the game or menu scene
//			switch(currentScene)
//			{
//				case Scene.Game:
//				
//					gameScene.Update (deltaTime);
//				
//					if(gameScene.swapScene)
//					{
//						currentScene = Scene.Menu;
//						//Director.Instance.ReplaceScene(menuScene.GetScene());
//					}
//				
//				break;
//				
//				case Scene.Menu:
//				
//					menuScene.Update (deltaTime);
//				
//					if(menuScene.swapScene)
//					{
//						currentScene = Scene.Game;
//						//Director.Instance.ReplaceScene(gameScene.GetScene());
//					}
//				
//				break;
//			}
		}
		
	}
}
