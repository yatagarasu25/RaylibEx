namespace Raylib_cs;

public static partial class RaylibEx
{
	public static IDisposable<Image> EmptyImage()
		=> new Image().Lock();
	public static IDisposable<Image> GenImageColor(int width, int height, Color color)
		=> Raylib.GenImageColor(width, height, color).Lock();

	public static IDisposable<Image> GenImageColor(vec2i size, Color color)
		=> GenImageColor(size.x, size.y, color);

	public static IDisposable<Image> GenImageColors(Vector2i size, Color[] colors)
		=> GenImageColors(size.x, size.y, colors);

	public static IDisposable<Image> GenImageColors(int width, int height, Color[] colors)
	{
		var image = Raylib.GenImageColor(width, height, Color.BLANK);

		image.SaveImageColors(colors);

		return image.Lock();
	}

	public static unsafe void UsingImageColors(vec2i size, Color[] colors, Action<Image> fn)
	{
		fixed (colorb* data = &colors[0])
		{
			var image = new Image() {
				data = data,
				format = PixelFormat.UNCOMPRESSED_R8G8B8A8,
				mipmaps = 0,
				size = size
			};

			fn(image);
		}
	}



	public static Color[] LoadImageColors(this Image image)
	{
		using (var imageColorsPtr = image.LockImageColors())
		{
			var colorCount = image.size.product;
			var result = new Color[colorCount];
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

	public static void SaveImageColors(this Image image, Color[] colors)
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

	public static Image Resize(this Image image, int newWidth, int newHeight)
	{
		Raylib.ImageResize(ref image, newWidth, newHeight);
		return image;
	}

	public static Image ResizeCanvas(this Image image, int newWidth, int newHeight, int offsetX, int offsetY, Color color)
	{
		Raylib.ImageResizeCanvas(ref image, newWidth, newHeight, offsetX, offsetY, color);
		return image;
	}

	public static Image ScaleCrop(this Image image, int newWidth, int newHeight, float newPixelAspect = 1)
	{
		var ia = image.size.aspect;
		var ca = (float)newWidth / newHeight;

		float scale = (ia < ca)
			? (float)newWidth / image.size.x
			: (float)newHeight / image.size.y;

		var (w, h) = ((image.size.x * scale * newPixelAspect).Round(), (image.size.y * scale).Round());
		Raylib.ImageResize(ref image, w, h);
		Raylib.ImageResizeCanvas(ref image, newWidth, newHeight, (newWidth - w) / 2, (newHeight - h) / 2, Color.TRANSPARENT);

		return image;
	}

	public static void ImageDraw(this Image src, ref Image dst, rect2 xywh, colorb tint)
		=> Raylib.ImageDraw(ref dst, src, rect2.wh(src.size), xywh, tint);
	public static void ImageDraw(this Image src, ref Image dst, vec2i xy, colorb tint)
		=> src.ImageDraw(ref dst, rect2.xywh(xy, src.size), tint);
	public static void ImageDraw(this Image src, IDisposable<Image> dst, rect2 xywh, colorb tint)
	{
		var dsti = dst._;
		src.ImageDraw(ref dsti, xywh, tint);
		dst.Reset = dsti;
	}
	public static void ImageDraw(this Image src, IDisposable<Image> dst, vec2i xy, colorb tint)
		=> src.ImageDraw(dst, rect2.xywh(xy, src.size), tint);
	public static void ImageDraw(this IDisposable<Image> src, IDisposable<Image> dst, rect2 xywh)
		=> src._.ImageDraw(dst, xywh, colorb.WHITE);
	public static void ImageDraw(this IDisposable<Image> src, IDisposable<Image> dst, vec2i xy)
		=> src._.ImageDraw(dst, xy, colorb.WHITE);
}
