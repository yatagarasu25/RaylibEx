using Raylib_cs;
using System;
using SystemEx;

namespace Raylib_cs
{
	public static partial class RaylibEx
	{
		static Action<Texture2D> textureModifier = null;

		public static IDisposable TextureModifier(Action<Texture2D> modifier)
		{
			var prev = textureModifier;
			textureModifier = modifier;
			return DisposableLock.Lock(() => { textureModifier = prev; });
		}

		public static Texture2D LoadTexture(string path)
			=> Raylib.LoadTexture(path)
				.Also(t => {
					textureModifier?.Invoke(t);
				});

		public static Font LoadFont(string path, int fontSize, int[] fontChars = null, int charsCount = 0)
			=> Raylib.LoadFontEx(path, fontSize, fontChars, charsCount)
				.Also(f => {
					textureModifier?.Invoke(f.texture);
				});
	}
}
