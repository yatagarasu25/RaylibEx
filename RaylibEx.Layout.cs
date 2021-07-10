using Layout.Engine;
using MathEx;
using System;
using SystemEx;

namespace Raylib_cs
{
	public class FontBoxLayout
		: BoxLayout
	{
		private Func<Font> font;
		private Func<string> text;
		private Func<colorb> color;

		public FontBoxLayout(Func<Font> font, Func<string> text)
			: base(vec2.zero)
		{
			this.font = font;
			this.text = text;
			this.color = () => colorb.WHITE;
		}

		public override void Draw()
		{
			RaylibEx.DrawText(font(), text(), rect, color());
		}
	}

	public class Texture2DBoxLayout
		: BoxLayout
	{
		protected Func<Texture2D> texture;
		protected Func<colorb> color;
		protected Func<BlendMode> blendMode;

		public Texture2DBoxLayout(Func<Texture2D> texture, Func<colorb> color = null, Func<BlendMode> blendMode = null)
			: base(vec2.zero)
		{
			this.texture = texture;
			this.color = color ?? (() => colorb.WHITE);
			this.blendMode = blendMode ?? (() => BlendMode.BLEND_ALPHA);
		}

		public override void Draw()
		{
			RaylibEx.DrawTexture(texture(), rect, color());
		}
	}

	public class Texture2DNPathBoxLayout
		: Texture2DBoxLayout
	{
		protected Func<NPatchInfo> npathInfo;

		public Texture2DNPathBoxLayout(Func<Texture2D> texture, Func<NPatchInfo> npathInfo, Func<colorb> color = null, Func<BlendMode> blendMode = null)
			: base(texture, color)
		{
			this.npathInfo = npathInfo;
		}

		public override void Draw()
		{
			RaylibEx.DrawTextureNPatch(texture(), npathInfo(), rect, color());
		}
	}

	public static partial class RaylibEx
	{
		public static FontBoxLayout MakeBoxLayout(this Font font, Func<string> text)
			=> new FontBoxLayout(() => font, text)
				.Also(box => {
					box.Content = RaylibEx.MeasureText(font, text());
				});

		public static BoxLayout MakeBoxLayout(this Texture2D texture, vec2? scale = null, colorb? color = null, BlendMode blendMode = BlendMode.BLEND_ALPHA)
			=> new BoxLayout((vec2)texture.size * (scale ?? vec2.one))
				.Also(box => {
					box.OnDraw = rect => {
						using (BeginBlendMode(blendMode))
						{
							rect.a += box.Padding.lt();
							rect.b -= box.Padding.rb();
							texture.DrawTexture(rect, color ?? colorb.WHITE);
						}
					};
				});

		public static Texture2DBoxLayout MakeBoxLayout(this Texture2D texture, NPatchInfo npathInfo, Func<colorb> color = null, Func<BlendMode> blendMode = null)
			=> new Texture2DNPathBoxLayout(() => texture, () => npathInfo, color, blendMode)
				.Also(box => {
					box.Content = vec2.xy(npathInfo.left + npathInfo.right, npathInfo.top + npathInfo.bottom);
				});
	}
}
