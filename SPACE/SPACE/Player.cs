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
			TextureInfo tInfo = new TextureInfo ("/Application/textures/spacebro.png");
			sprite = new SpriteUV (tInfo);
			
			sprite.Quad.S = tInfo.TextureSizef;
			sprite.Position = new Vector2 (0, 0);
			position = new Vector2(10.0f, 10.0f);
		}
		
		override public void Update(float deltaTime)
		{
			sprite.Position = position;
			sprite.Angle = angle;
			sprite.Scale = scale;
		}
		
		override public void AddToScene (Scene _scene)
		{
			//_scene.AddChild(sprite);
		}
		
	}
}

