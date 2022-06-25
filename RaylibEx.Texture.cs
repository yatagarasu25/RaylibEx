namespace Raylib_cs;

public static partial class RaylibEx
{
	public static Texture2D GenTextureColors(Vector2i size, Color[] colors)
		=> GenTextureColors(size.x, size.y, colors);

	public static Texture2D GenTextureColors(int width, int height, Color[] colors)
	{
		using var image = Lock(GenImageColors(width, height, colors));
		return Raylib.LoadTextureFromImage(image._);
	}
}
