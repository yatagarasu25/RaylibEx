using MathEx;
using System;
using SystemEx;

namespace Raylib_cs
{
	public static partial class RaylibEx
	{
		public static Image GenImageColors(vec2i size, colorb[] colors)
			=> GenImageColors(size.x, size.y, colors);

		public static Image GenImageColors(int width, int height, colorb[] colors)
		{
			var image = Raylib.GenImageColor(width, height, colorb.BLANK);

			image.SaveImageColors(colors);

			return image;
		}

		public static colorb[] LoadImageColors(this Image image)
		{
			using (var imageColorsPtr = image.LockImageColors())
			{
				var colorCount = image.size.product;
				var result = new colorb[colorCount];
				unsafe
				{
					fixed (void* resultPtr = result)
					{
						Buffer.MemoryCopy((byte*)imageColorsPtr._, resultPtr, colorCount * 4, colorCount * 4);
					}
				}
				return result;
			}
		}

		public static void SaveImageColors(this Image image, colorb[] colors)
		{
			unsafe
			{
				var byteCount = image.size.product * 4;
				fixed (void* colorsPtr = colors)
				{
					Buffer.MemoryCopy(colorsPtr, (void*)image.data, byteCount, byteCount);
				}
			}
		}
	}
}
