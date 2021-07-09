using MathEx;
using System;
using SystemEx;

namespace Raylib_cs
{
	public static partial class RaylibEx
	{
		static TransformStack transformStack = new TransformStack();

		public static IDisposable BeginTransform(vec2 translate)
			=> BeginTransform(translate.xyz(0));

		public static IDisposable BeginTransform(vec3 translate)
		{
			Rlgl.rlPushMatrix();

			Rlgl.rlTranslatef(translate.x, translate.y, translate.z);
			transformStack = transformStack.Push(matrix4x4.Translate(translate));

			return DisposableLock.Lock(() => {
				transformStack = transformStack.Pop();
				Rlgl.rlPopMatrix();
			});
		}

		public static void Translate(vec2 translate)
			=> Translate(translate.xyz(0));

		public static void Translate(vec3 translate)
		{
			Rlgl.rlTranslatef(translate.x, translate.y, translate.z);
		}
	}
}
