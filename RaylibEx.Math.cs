namespace Raylib_cs;

public static partial class RaylibEx
{
	public static Rectangle GetRectangle(this Texture2D texture)
		=> Rectangle.wh(texture.size);

	public static Rectangle WH(this Rectangle r, Texture2D t)
		=> Rectangle.xywh(r.a, (vec2)t.size);

	public static Rectangle XY(this Texture2D t, float x, float y)
		=> Rectangle.xywh((x, y), (vec2)t.size);
}
