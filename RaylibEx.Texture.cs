using System.Diagnostics;

namespace Raylib_cs;

public static partial class RaylibEx
{
	public static Texture2D GenTextureColors(Vector2i size, Color[] colors)
		=> GenTextureColors(size.x, size.y, colors);

	public static Texture2D GenTextureColors(int width, int height, Color[] colors)
	{
		using var image = GenImageColors(width, height, colors);
		return Raylib.LoadTextureFromImage(image._);
	}

	public static void UpdateTexture(IDisposable<Texture2D> texture, Image image)
	{
		Debug.Assert(texture._.size == image.size);

		unsafe
		{
			Raylib.UpdateTexture(texture._, image.data);
		}
	}
	public static void UpdateTexture(IDisposable<Texture2D> texture, IDisposable<Image> image)
		=> UpdateTexture(texture, image._);
}
