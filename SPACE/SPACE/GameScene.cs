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
	public class GameScene 
	{
		private static Sce.PlayStation.HighLevel.GameEngine2D.Scene gameScene;
		
		private static int				currentScore;
		private static int				currentBG;
		private static bool				touchedLastFrame;
		
		private static Obstacle[]		obstacles;
		private static Bird				bird;
		private static Background[] 	bgList;
		
		private static Sound			deathSound;
		private static Sound			scoreSound;
		private static SoundPlayer 		deathSoundPlr;
		private static SoundPlayer  	scoreSoundPlr;
		
		private static bool				scenePaused;
		
		
		
		public GameScene()
		{
			gameScene = new Sce.PlayStation.HighLevel.GameEngine2D.Scene();
			
			currentScore = 0;
			currentBG = 0;
			touchedLastFrame = true;
			scenePaused = true;
			
			//Initialise game objects in the order they need to be drawn
			CreateBackground ();
			bgList[0].DrawBackground(gameScene);
			CreateObstacles ();
			bgList[0].DrawForeground(gameScene);
			bird = new Bird(gameScene);
			
			//Create Sounds and SoundPlayers
			deathSound = new Sound("/Application/sfx/deathSound.wav");
			scoreSound = new Sound("/Application/sfx/scoreSound2.wav");
			
			deathSoundPlr = deathSound.CreatePlayer();
			deathSoundPlr.Volume = 0.5f;
			scoreSoundPlr = scoreSound.CreatePlayer();
			scoreSoundPlr.Volume = 0.5f;
		}
		
		public void Update(double deltaTime)
		{
			var touches = Touch.GetData(0);
			
			//A "Tap" occurs if touches.Count returned 0 on the previous frame
			if(touches.Count > 0 && touchedLastFrame == false)
			{
				if(scenePaused)
				{
					scenePaused = false;
				}
					
				if(bird.GetAlive())
				{
					bird.Tapped();
				}
				else
				{
					ResetScene ();
				}
				
				touchedLastFrame = true;
			}
			else if(touches.Count == 0)
			{
				touchedLastFrame = false;
			}
			
			if(!scenePaused)
			{
				bird.Update(deltaTime);
				
				if(bird.GetAlive())
				{
					//Move background.
					bgList[currentBG].Update(deltaTime);
								
					//Move obstacles.
					foreach(Obstacle obstacle in obstacles)
					{
						obstacle.Update(deltaTime);
						
						if(obstacle.HasCollidedWith(bird.GetSprite(), 8.0f))
						{
							deathSoundPlr.Play ();
							bird.SetAlive(false);
						}
						
						//Has the bird passed through the pipes (+1 point)
						if(obstacle.HasPassedThrough(bird.GetSprite()))
						{
							scoreSoundPlr.Play ();
							currentScore++;
						}
					}
					
				}//If Bird Is Alive
			}//If !scenePaused
		}
		
		public void ResetScene()
		{
			currentScore = 0;
			touchedLastFrame = false;
			
			obstacles[0].InitPosition();
			obstacles[1].InitPosition();
			
			bird.Initialize();
		}
		
		public static void CreateObstacles()
		{
			int viewportWidth = Director.Instance.GL.Context.GetViewport().Width;
			
			string[] pipeTex = new string[2];
			pipeTex[0] = "/Application/textures/obstacles/toppipe.png";
			pipeTex[1] = "/Application/textures/obstacles/bottompipe.png";
			
			obstacles = new Obstacle[2];
			obstacles[0] = new Obstacle(pipeTex, viewportWidth*0.5f, 3.0f);	
			obstacles[1] = new Obstacle(pipeTex, viewportWidth, 3.0f);
			
			obstacles[0].DrawObject(gameScene);
			obstacles[1].DrawObject(gameScene);
		}
		
		public static void CreateBackground()
		{
			bgList = new Background[1];
			currentBG = 0;
			
			//Create the background.
			bgList[0] = new Background(4);
			
			//Initialise background layers
			bgList[0].SetLayer(0, "/Application/textures/backgrounds/SeasideLargeL1.png", 0.0f, false);
			bgList[0].SetLayer(1, "/Application/textures/backgrounds/SeasideLargeL3.png", 1.0f, false);
			bgList[0].SetLayer(2, "/Application/textures/backgrounds/SeasideLargeL2.png", 2.0f, false);
			bgList[0].SetLayer(3, "/Application/textures/backgrounds/foreground.png", 1.0f, true);
			bgList[0].ShiftLayer(3, 0.0f, -8.0f, true);
			
			
//			//Create the background.
//			bgList[1] = new Background(5);
//			
//			//Initialise background layers
//			bgList[1].SetLayer(0, "/Application/textures/FlappyMenuL1.png", 0.0f, false);
//			bgList[1].SetLayer(1, "/Application/textures/FlappyMenuL2.png", 0.5f, false);
//			bgList[1].SetLayer(2, "/Application/textures/FlappyMenuL3.png", 1.0f, false);
//			bgList[1].SetLayer(3, "/Application/textures/FlappyMenuL4.png", 2.0f, false);
//			bgList[1].SetLayer(4, "/Application/textures/FlappyMenuForeground.png", 2.0f, true);
//			bgList[1].ShiftLayer(4, 0.0f, -8.0f, true);
		}
		
		public int GetScore()
		{
			return currentScore;
		}
		
		public Sce.PlayStation.HighLevel.GameEngine2D.Scene GetScene()
		{
			return gameScene;
		}
	}
}

