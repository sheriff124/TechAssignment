using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;


namespace FlappyBird
{

	
	public class Obstacle
	{
		const float kGap = 200.0f;
		
		//Private variables.
		private SpriteUV[] 	sprites;
		private TextureInfo[] texInfo;
		
		private float		width;
		private float		height;
		private bool		hasPassed;
		
		private float		speed;
		private float		startX;
		
		
		
		
		//Accessors.
		//public SpriteUV SpriteTop 	 { get{return sprites[0];} }
		//public SpriteUV SpriteBottom { get{return sprites[1];} }
		
		//Public functions.
		public Obstacle (string[] texPath, float startX, float speed)
		{
			this.speed = speed;
			this.startX = startX;
			
			this.texInfo = new TextureInfo[2];
			sprites	= new SpriteUV[2];
			
			for(int a = 0; a < 2; a++)
			{
				this.texInfo[a] = new TextureInfo(texPath[a]);
				
				sprites[a]			= new SpriteUV(texInfo[a]);	
				sprites[a].Quad.S 	= texInfo[a].TextureSizef;
				
				//Add to the current scene.
				//scene.AddChild(sprites[a]);
			}
			
			//Get sprite bounds.
			Bounds2 b = sprites[0].Quad.Bounds2();
			width  = b.Point10.X;
			height = b.Point01.Y;
			
			//Position pipes.
			InitPosition();
			

		}
		
		public void InitPosition()
		{
			sprites[0].Position = new Vector2(startX, Director.Instance.GL.Context.GetViewport().Height*RandomPosition());
			sprites[1].Position = new Vector2(startX, sprites[0].Position.Y-height-kGap);
			
			//Other stuff
			hasPassed = false;
		}
		
		public void DrawObject(Scene scene)
		{
			scene.AddChild(sprites[0]);
			scene.AddChild(sprites[1]);
		}
		
		public void Dispose()
		{
			texInfo[0].Dispose();
			texInfo[1].Dispose();
		}
		
		public void Update(double deltaTime)
		{			
			sprites[0].Position = new Vector2(sprites[0].Position.X - speed, sprites[0].Position.Y);
			sprites[1].Position = new Vector2(sprites[1].Position.X - speed, sprites[1].Position.Y);
			
			//If off the left of the viewport, loop them around.
			if(sprites[0].Position.X < -width)
			{
				sprites[0].Position = new Vector2(Director.Instance.GL.Context.GetViewport().Width,
			                              Director.Instance.GL.Context.GetViewport().Height*RandomPosition());
			
				sprites[1].Position = new Vector2(Director.Instance.GL.Context.GetViewport().Width,
			                              sprites[0].Position.Y-height-kGap);
				
				//Also reset hasPassed
				hasPassed = false;
			}		
		}
		
		private float RandomPosition()
		{
			Random rand = new Random();
			float randomPosition = (float)rand.NextDouble();
			randomPosition += 0.45f;
			
			if(randomPosition > 1.0f)
				randomPosition = 0.9f;
		
			return randomPosition;
		}
		
		public bool HasCollidedWith(SpriteUV sprite, float offset)
		{	
			//Get the width and height of the sprite
			Bounds2 b = sprite.Quad.Bounds2();
			float sWidth = b.Point10.X;
			float sHeight = b.Point01.Y;
			
			float x1 = sprite.Position.X 			+ offset;
			float x2 = sprite.Position.X + sWidth 	- offset;
			float y1 = sprite.Position.Y 			+ offset;
			float y2 = sprite.Position.Y + sHeight 	- offset;
			
			for(int a=0;a<2;a++)
			{
				if(	!(	x1 > sprites[a].Position.X + width  || x2 < sprites[a].Position.X	||
				   		y1 > sprites[a].Position.Y + height || y2 < sprites[a].Position.Y 	))
				{
					return true;
				}
			}
			
			return false;
		}
		
		public bool HasPassedThrough(SpriteUV sprite)
		{
			float offset = 0;
			
			if(!hasPassed)
			{
				for(int a=0;a<2;a++)
				{
					if(sprite.Position.X > sprites[a].Position.X + offset)
					{
						hasPassed = true;
						return true;
					}
				}
			}
			
			return false;
		}
		
		public SpriteUV[] GetSprite()
		{
			return sprites;
		}
	}
}

