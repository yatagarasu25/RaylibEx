using MathEx;
using System;
using System.Linq;
using SystemEx;
using Vector2i = MathEx.vec2i;

namespace Raylib_cs
{
	public struct AnimatedTexture2D
	{
		public uint[] id;          // OpenGL texture frame id
		public Vector2i size;      // Texture base size
		public int mipmaps;        // Mipmap levels, 1 by default
		public PixelFormat format; // Data format (PixelFormat type)
	}

	public static partial class RaylibEx
	{
		public static AnimatedTexture2D AsAnimated(this Texture2D[] frames)
			=> new AnimatedTexture2D {
				id = frames.Select(f => f.id).ToArray(),
				size = frames.All(f => f.size == frames[0].size) ? frames[0].size : vec2i.empty,
				mipmaps = frames.All(f => f.mipmaps == frames[0].mipmaps) ? frames[0].mipmaps : -1,
				format = frames.All(f => f.format == frames[0].format) ? frames[0].format : PixelFormat.UNCOMPRESSED_GRAYSCALE
			};
	}

	public class Texture2DAnimator
	{
		AnimatedTexture2D frames;
		int currentFrameIndex = 0;

		public Texture2DAnimator(Texture2D[] frames)
		{
			this.frames = frames.AsAnimated();
		}

		public Texture2D CurrentFrame
			=> new Texture2D {
				id = frames.id[currentFrameIndex],
				size = frames.size,
				mipmaps = frames.mipmaps,
				format = frames.format
			};
	}
}
