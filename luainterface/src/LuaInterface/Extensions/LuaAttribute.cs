using System;

namespace LuaInterface.Extension
{
	/// <summary>
	/// Description of LuaAttribute.
	/// </summary>
	[AttributeUsageAttribute(AttributeTargets.Method, AllowMultiple = true)]
	public class LuaMethod : System.Attribute
	{
		public LuaMethod(
			string name,
			string help
		)
		{
			this.Name = name;
			this.Help = help;
		}
		
		public string Name { get; private set; }
		public string Help { get; private set; }
	}
}