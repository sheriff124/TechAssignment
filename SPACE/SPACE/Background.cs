using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;



namespace FlappyBird
{
	public class Background
	{	
		
		struct Layer
		{
			public SpriteUV[] sprite;
			public TextureInfo textureInfo;
			public float scrollSpeed;
			public bool foreground;
		};
		
		
		//Private variables.
		private Layer[] layer;
		private int bgSize;
		private float speedMod;
		
		//Public functions.
		public Background (int bgSize)
		{
			this.bgSize = bgSize;
			layer = new Layer[bgSize];
			speedMod = 1;
		}
		
		
		public void SetLayer (int layerNum, string texPath, float scrollSpeed, bool foreground)
		{
			layer[layerNum] = new Layer();
			layer[layerNum].sprite = new SpriteUV[2];
			layer[layerNum].textureInfo = new TextureInfo(texPath);
			layer[layerNum].scrollSpeed = scrollSpeed;
			layer[layerNum].foreground = foreground;
			
			for(int a = 0; a < 2; a++)
			{
				layer[layerNum].sprite[a] = new SpriteUV(layer[layerNum].textureInfo);
				layer[layerNum].sprite[a].Quad.S = 	layer[layerNum].textureInfo.TextureSizef;
			}
			
			Bounds2 b = layer[layerNum].sprite[0].Quad.Bounds2();
			float width = b.Point10.X;
			
			//Position Sprites
			layer[layerNum].sprite[0].Position = new Vector2(0.0f, 0.0f);
			layer[layerNum].sprite[1].Position = new Vector2(layer[layerNum].sprite[0].Position.X+width-4.0f, 0.0f);
		}
		
		
		public void ShiftLayer(int layerNum, float x, float y, bool additive)
		{
			if(additive)
			{
				for(int a = 0; a < 2; a++)
				{
					layer[layerNum].sprite[a].Position = new Vector2(layer[layerNum].sprite[a].Position.X + x, 
					                                                 layer[layerNum].sprite[a].Position.Y + y);
				}
			}
			else
			{
				for(int a = 0; a < 2; a++)
				{
					layer[layerNum].sprite[a].Position = new Vector2(x, y);
				}
			}
		}
		
		
		public void DrawBackground(Scene scene)
		{
			for(int a = 0; a < bgSize; a++)
			{
				if(layer[a].foreground == false)
				{
					foreach(SpriteUV sprite in layer[a].sprite)
					{
						scene.AddChild(sprite);
					}
				}
			}
		}
		
		public void DrawForeground(Scene scene)
		{
			for(int a = 0; a < bgSize; a++)
			{
				if(layer[a].foreground)
				{
					foreach(SpriteUV sprite in layer[a].sprite)
					{
						scene.AddChild(sprite);
					}
				}
			}
		}
		
		
		public void Dispose()
		{
			for(int a = 0; a < bgSize; a++)
			{
				layer[a].textureInfo.Dispose();
			}
		}
		
		public void Update(double deltaTime)
		{	
			for(int a = 0; a < bgSize; a++)
			{
				MoveLayer (deltaTime, layer[a].sprite, layer[a].scrollSpeed);
			}
		}
		
		public void SetSpeedModifier(float mod)
		{
			speedMod = mod;
		}
		
		private void MoveLayer(double deltaTime, SpriteUV[] sprite, float scrollSpeed)
		{	
			Bounds2 b = sprite[0].Quad.Bounds2();
			float width    = b.Point10.X;
			
			//Move sprite and round the position
			for(int a = 0; a <2 ; a++)
			{
				sprite[a].Position = new Vector2(FMath.Floor (sprite[a].Position.X - (scrollSpeed * speedMod)), FMath.Floor (sprite[a].Position.Y));
			}
			
					
			//Sprite 1
			if(sprite[0].Position.X < -width)
			{
				sprite[0].Position = new Vector2(sprite[1].Position.X+width-4, sprite[0].Position.Y);
			}
			else
			{
				sprite[0].Position = new Vector2(sprite[0].Position.X, sprite[0].Position.Y);
			}
			
			//Sprite 2
			if(sprite[1].Position.X < -width)
			{
				sprite[1].Position = new Vector2(sprite[0].Position.X+width-4, sprite[1].Position.Y);
			}
			else
			{
				sprite[1].Position = new Vector2(sprite[1].Position.X, sprite[1].Position.Y);
			}
		}
	}
}

