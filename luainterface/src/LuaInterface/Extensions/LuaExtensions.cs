using System;
using System.Reflection;

namespace LuaInterface.Extension
{
	/// <summary>
	/// Description of LuaExtensions.
	/// </summary>
	public static class LuaExtensions
	{
		public static void RegisterMethods(this Lua lua, object that, Type inf)
		{
			foreach(MethodInfo info in inf.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Static))
			{
				LuaMethod[] attrs = (LuaMethod[])info.GetCustomAttributes(typeof(LuaMethod), false);
				foreach(LuaMethod attr in attrs)
				{
					lua.RegisterFunction(attr.Name, that, info);
				}
			}

		}
		
		public static LuaFunction Action(this Lua lua, Action fun) { return lua.LambdaFunction(fun.Target, fun.Method); }
		public static LuaFunction Action<T1, T2>(this Lua lua, Action<T1, T2> fun) { return lua.LambdaFunction(fun.Target, fun.Method); }
		public static LuaFunction Action<T1, T2, T3>(this Lua lua, Action<T1, T2, T3> fun) { return lua.LambdaFunction(fun.Target, fun.Method); }
		public static LuaFunction Action<T1, T2, T3, T4>(this Lua lua, Action<T1, T2, T3, T4> fun) { return lua.LambdaFunction(fun.Target, fun.Method); }
		
		public static LuaFunction Func<TResult>(this Lua lua, Func<TResult> fun) { return lua.LambdaFunction(fun.Target, fun.Method); }
		public static LuaFunction Func<T, TResult>(this Lua lua, Func<T, TResult> fun) { return lua.LambdaFunction(fun.Target, fun.Method); }
		public static LuaFunction Func<T1, T2, TResult>(this Lua lua, Func<T1, T2, TResult> fun) { return lua.LambdaFunction(fun.Target, fun.Method); }
		public static LuaFunction Func<T1, T2, T3, TResult>(this Lua lua, Func<T1, T2, T3, TResult> fun) { return lua.LambdaFunction(fun.Target, fun.Method); }
		public static LuaFunction Func<T1, T2, T3, T4, TResult>(this Lua lua, Func<T1, T2, T3, T4, TResult> fun) { return lua.LambdaFunction(fun.Target, fun.Method); }
	}
}