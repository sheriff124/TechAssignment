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
	public class Button
	{
		float width;
		float height;
		
		Rectangle rect;
		
		SpriteUV 		sprite;
		TextureInfo[] 	texInfo;
		
		//Sprite 0 - normal button
		//Sprite 1 - highlighted button
		//Sprite 2 - pressed button
		
		public Button (TextureInfo[] texInfo)
		{
			this.texInfo = texInfo;
			
			sprite = new SpriteUV(texInfo[0]);
			sprite.Quad.S = texInfo[0].TextureSizef;
			
			//Use the sprites first texture to determine the size values
			Bounds2 b = sprite.Quad.Bounds2();
			width = b.Point10.X;
			height = b.Point01.Y;

		}
		
		public void SetState(int state)
		{
			if(texInfo[state] != null)
			{
				sprite.TextureInfo = texInfo[state];
			}
		}
		
		public Rectangle GetRectangle()
		{
			rect = new Rectangle(sprite.Position.X, sprite.Position.Y, width, height);
			
			return rect;
		}

		public void DrawButton(Sce.PlayStation.HighLevel.GameEngine2D.Scene scene)
		{
			//scene.RemoveChild(currentSprite, true);
			scene.AddChild(sprite);
		}
		
		public void SetPosition(float newX, float newY)
		{
			sprite.Position = new Vector2(newX, newY);
		}
	}
}

