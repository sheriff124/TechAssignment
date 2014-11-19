using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace FlappyBird
{
	public class Bird
	{
		//Private variables.
		private static SpriteUV 	sprite;
		private static TextureInfo	textureInfo;
		private static float		yPosBeforeRise;
		
		private static bool			rise;
		private static bool			alive; 
		
		private static float 		ySpeed;
		private static float		fallSpeed;
		private static float		riseSpeed;
		
		private static float		riseHeight;
		
		private static float		lowerYBoundary;
		private static float		upperYBoundary;
		
		private static float		deathDelay;
		
		
		
		public void SetAlive(bool b){alive = b;}
		public bool GetAlive(){return alive;}

		
		//Accessors.
		//public SpriteUV Sprite { get{return sprite;} }
		
		//Public functions.
		public Bird (Scene scene)
		{
			textureInfo  = new TextureInfo("/Application/textures/bird.png");
			
			//sprite	 		= new SpriteUV();
			sprite 			= new SpriteUV(textureInfo);	
			sprite.Quad.S 	= textureInfo.TextureSizef;
			
			//Values that need to be reset go in Initialize
			Initialize ();
			
			
			fallSpeed = -0.45f; //default -0.4
			riseSpeed = 1.0f; //default 0.8
			riseHeight = 44.0f; //default 42
			
			lowerYBoundary = 34.0f;
			upperYBoundary = 600.0f;
			
			deathDelay = 4.0f;
			
			scene.AddChild(sprite);
		}
		
		public void Initialize()
		{
			sprite.Position = new Vector2(200.0f,Director.Instance.GL.Context.GetViewport().Height*0.5f);
			sprite.Scale	= new Vector2(1.0f, 1.0f);
			sprite.Pivot 	= new Vector2(0.5f,0.5f);
			
			rise  = false;
			alive = true;
			ySpeed = 0.0f;
			sprite.Angle = 0.0f;
		}
		
		public void Dispose()
		{
			textureInfo.Dispose();
		}
		
		public void Update(double deltaTime)
		{	

			if(alive)
			{
				//Move the sprite upwards by a fixed amount
				if(rise)
				{
					if(ySpeed < 0)
					{
						ySpeed = 0;
					}
					
					if(sprite.Position.Y < yPosBeforeRise + riseHeight)
					{
						ySpeed += riseSpeed;
					}
					else
					{
						rise = false;
					}
				}
				else 
				{
	
					yPosBeforeRise = sprite.Position.Y;
					
					ySpeed += fallSpeed;
	
				}
				
				sprite.Angle = ySpeed/30;
			
			}
			else if(deathDelay > 0)
			{
				//If !Alive start the deathDelay timer
				deathDelay--;
				ySpeed = 0;
			}
			else
			{
				//Resume falling after delay
				ySpeed += fallSpeed;
			}
			
			
			//Set lower boundary of movement
			if(sprite.Position.Y < lowerYBoundary && ySpeed < 0)
			{
				ySpeed = 0;
				sprite.Position = new Vector2(sprite.Position.X, lowerYBoundary);
			}
			
			//Set upper boundary of movement
			if(sprite.Position.Y > upperYBoundary)
			{
				ySpeed = -1.0f;
				rise = false;
			}
			
			//Move the sprite
			sprite.Position = new Vector2(sprite.Position.X, sprite.Position.Y + ySpeed);
			
			
		}	
		
		public void DrawObject(Scene scene)
		{
			scene.AddChild(sprite);
		}
		
		public void Tapped()
		{
			if(!rise && alive)
			{
				rise = true;
				yPosBeforeRise = sprite.Position.Y;
			}
		}
		
		public SpriteUV GetSprite()
		{
			return sprite;
		}
	}
}

