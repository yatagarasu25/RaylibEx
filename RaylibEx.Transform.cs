namespace Raylib_cs;

public static partial class RaylibEx
{
	static TransformStack transformStack = new TransformStack();

	public static IDisposable BeginTransform(Vector2 translate)
		=> BeginTransform(translate.xyz(0));

	public static IDisposable BeginTransform(Vector3 translate)
	{
		Rlgl.rlPushMatrix();

		Rlgl.rlTranslatef(translate.x, translate.y, translate.z);
		transformStack = transformStack.Push(Matrix4x4.Translate(translate));

		return DisposableLock.Lock(() => {
			transformStack = transformStack.Pop();
			Rlgl.rlPopMatrix();
		});
	}

	public static void Translate(Vector2 translate)
		=> Translate(translate.xyz(0));

	public static void Translate(Vector3 translate)
	{
		Rlgl.rlTranslatef(translate.x, translate.y, translate.z);
	}
}
