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
	public class Entity
	{
		protected Vector2 		position;
		protected Vector2 		scale;
		protected float   		angle;

		protected SpriteUV 		sprite;
		protected TextureInfo 	texInfo;

		protected Boolean 		gravityIsDown;
		
		public Entity ()
		{
		}
		
		public virtual void Draw (Scene _scene)
		{
			_scene.AddChild(sprite);
		}
		
		public virtual void Update(float deltaTime)
		{
			
		}
		
	}
}

