using System;
using SystemEx;

namespace Raylib_cs
{
	public static partial class RaylibEx
	{
		public static DisposableLock<Image> LockTextureData(this Texture2D texture)
			=> DisposableLock.Lock(() => Raylib.GetTextureData(texture), p => Raylib.UnloadImage(p));

		public static DisposableLock<Image> LockImage(this Image image)
			=> DisposableLock.Lock(image, p => Raylib.UnloadImage(p));

		public static DisposableLock<IntPtr> LockImageColors(this Image image)
			=> DisposableLock.Lock(() => Raylib.LoadImageColors(image), p => Raylib.UnloadImageColors(p));
	}
}
