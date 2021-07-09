using MathEx;
using Raylib_cs;

namespace Raylib_cs
{
	public static partial class RaylibEx
	{
		public static rect2 GetRectangle(this Texture2D texture)
			=> rect2.wh(texture.size);

		public static rect2 WH(this rect2 r, Texture2D t)
			=> rect2.xywh(r.a, (vec2)t.size);

		public static rect2 XY(this Texture2D t, float x, float y)
			=> rect2.xywh((x, y), (vec2)t.size);
	}
}
