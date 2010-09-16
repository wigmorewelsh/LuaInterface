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
			foreach(MethodInfo info in inf.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.NonPublic))
			{
				LuaMethod[] attrs = (LuaMethod[])info.GetCustomAttributes(typeof(LuaMethod), false);
				foreach(LuaMethod attr in attrs)
				{
					lua.RegisterFunction(attr.Name, that, info);
				}
			}

		}
	}
}