using MathEx;

namespace Raylib_cs
{
	public static partial class RaylibEx
	{
		public static Texture2D GenTextureColors(vec2i size, colorb[] colors)
			=> GenTextureColors(size.x, size.y, colors);

		public static Texture2D GenTextureColors(int width, int height, colorb[] colors)
		{
			using var image = LockImage(GenImageColors(width, height, colors));
			return Raylib.LoadTextureFromImage(image._);
		}
	}
}
