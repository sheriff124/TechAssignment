using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;
using Sce.PlayStation.Core.Audio;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;



namespace SPACE
{
	public class Player : Entity
	{
		public Player ()
		{
			TextureInfo texInfo = new TextureInfo("/Application/textures/Spacebro.png");
			sprite = new SpriteUV(texInfo);
			position = new Vector2(100.0f, 100.0f);
		}
		
		override public void Update(float deltaTime)
		{
			sprite.Position = position;
			sprite.Angle = angle;
			sprite.Scale = scale;
		}
		
	}
}

