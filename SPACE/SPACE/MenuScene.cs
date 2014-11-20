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
	public class MenuScene
	{
		private static Sce.PlayStation.HighLevel.GameEngine2D.Scene menuScene;
		
		private static int 				viewWidth, viewHeight;
		private static TouchStatus		currentTouchStatus;
		public  static bool				changeScene {get;}
		
		public static Background[]		bgList;
		
		private static SpriteUV 		logoSpr;
		private static TextureInfo 		logoTex;
		
		private static Button			playBtn;
		private static TextureInfo[]	playTex;
		private static Rectangle		playRec;
		
		
		
		public MenuScene ()
		{
			menuScene = new Sce.PlayStation.HighLevel.GameEngine2D.Scene();
			
			viewWidth = Director.Instance.GL.Context.GetViewport().Width;
			viewHeight = Director.Instance.GL.Context.GetViewport().Height;
			
			CreateBackground();
			bgList[0].DrawBackground(menuScene);
			
			//Create Logo
			logoTex = new TextureInfo("Application/textures/menus/FlappyLogoGlow.png");
			logoSpr = new SpriteUV(logoTex);
			logoSpr.Quad.S 	= logoTex.TextureSizef;
			
			//Create Play Button
			playTex = new TextureInfo[2];
			playTex[0] = new TextureInfo("Application/textures/menus/PlayButton.png");
			playTex[1] = new TextureInfo("Application/textures/menus/PlayButtonHL.png");
			playBtn = new Button(playTex);

			
			playRec = playBtn.GetRectangle();
			playBtn.SetPosition(viewWidth/2-playRec.Width/2,100.0f);
			
			
			menuScene.AddChild (logoSpr);
			playBtn.DrawButton (menuScene);
		}
		
		public void Update(double deltaTime)
		{
			// Query gamepad for current state
			var gamePadData = GamePad.GetData (0);
			List<TouchData> touches = Touch.GetData(0);
			foreach(TouchData data in touches)
			{
				currentTouchStatus = data.Status;
				float xPos = (data.X + 0.5f) * viewWidth;
				float yPos = (data.Y + 0.5f) * viewHeight;
				
				
				if(data.Status == TouchStatus.Move)
				{
					if(yPos < viewHeight/2)
					{
						bgList[0].SetSpeedModifier(xPos/50);
					}
					
					if(InsideRect(xPos, yPos, playBtn.GetRectangle()))
					{
						playBtn.SetState(1);
					}
					else
					{
						playBtn.SetState (0);
					}
				}
				
				if(data.Status == TouchStatus.Down)
				{
					if(InsideRect(xPos, yPos, playBtn.GetRectangle()))
					{
						changeScene = true;
					}
				}

			}
			
			
			bgList[0].Update(deltaTime);

		}
		
		public void CreateBackground()
		{
			bgList = new Background[1];
			
			//Create the background.
			bgList[0] = new Background(5);
			
			//Initialise background layers
			bgList[0].SetLayer(0, "/Application/textures/backgrounds/FlappyMenuL1.png", 0.0f, false);
			bgList[0].SetLayer(1, "/Application/textures/backgrounds/FlappyMenuL2.png", 0.5f, false);
			bgList[0].SetLayer(2, "/Application/textures/backgrounds/FlappyMenuL3.png", 1.0f, false);
			bgList[0].SetLayer(3, "/Application/textures/backgrounds/FlappyMenuL4.png", 2.0f, false);
			bgList[0].SetLayer(4, "/Application/textures/backgrounds/FlappyMenuForeground.png", 2.0f, false);
			bgList[0].ShiftLayer(4, 0.0f, -8.0f, true);
		}
		
		/// Inside rectangle test
	    private static bool InsideRect(float pixelX, float pixelY, Rectangle hitTestArea)
	    {
			pixelY = viewHeight-pixelY;
			
			playRec = playBtn.GetRectangle();
			hitTestArea = playRec;
	        if (hitTestArea.X <= pixelX && hitTestArea.X + hitTestArea.Width >= pixelX &&
	            hitTestArea.Y <= pixelY && hitTestArea.Y + hitTestArea.Height >= pixelY) {
	            return true;
	        }
	
	        return false;
	    }
		
		public Sce.PlayStation.HighLevel.GameEngine2D.Scene GetScene()
		{
			return menuScene;
		}
	}
}

