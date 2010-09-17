﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Reflection;

using NUnit.Framework;

namespace LuaInterface.Tests
{
    /*
	 * Automated test cases for LuaInterface
	 *
	 * Author: Fabio Mascarenhas
	 * Version: 1.0
	 */
	[TestFixture]
    public class TestLuaInterface
    {
        private Lua lua;
        /*
         * Executed before each test case
         */
        [TestFixtureSetUp]
        public void Init()
        {
            lua = new Lua();

            GC.Collect();  // runs GC to expose unprotected delegates
        }
        /*
         * Executed after each test case
         */
        [TestFixtureTearDown]
        public void Destroy()
        {
            lua = null;
        }

		/*
		 * Tests if DoString is correctly returning values
		 */
		[Test]
		public void DoString() 
		{
			object[] res=lua.DoString("a=2\nreturn a,3");
			//Console.WriteLine("a="+res[0]+", b="+res[1]);
			Assert.AreEqual(res[0],2);
			Assert.AreEqual(res[1],3);
		}
		/*
		 * Tests getting of global numeric variables
		 */
		[Test]
		public void GetGlobalNumber() 
		{
			lua.DoString("a=2");
			double num=lua.GetNumber("a");
			//Console.WriteLine("a="+num);
			Assert.AreEqual(num,2);
		}
		/*
		 * Tests setting of global numeric variables
		 */
		[Test]
		public void SetGlobalNumber() 
		{
			lua.DoString("a=2");
			lua["a"]=3;
			double num=lua.GetNumber("a");
			//Console.WriteLine("a="+num);
			Assert.AreEqual(num,3);
		}
		/*
		 * Tests getting of numeric variables from tables
		 * by specifying variable path
		 */
		[Test]
		public void GetNumberInTable() 
		{
			lua.DoString("a={b={c=2}}");
			double num=lua.GetNumber("a.b.c");
			//Console.WriteLine("a.b.c="+num);
			Assert.AreEqual(num,2);
		}
		/*
		 * Tests setting of numeric variables from tables
		 * by specifying variable path
		 */
		[Test]
		public void SetNumberInTable() 
		{
			lua.DoString("a={b={c=2}}");
			lua["a.b.c"]=3;
			double num=lua.GetNumber("a.b.c");
			//Console.WriteLine("a.b.c="+num);
			Assert.AreEqual(num,3);
		}
		/*
		 * Tests getting of global string variables
		 */
		[Test]
		public void GetGlobalString() 
		{
			lua.DoString("a=\"test\"");
			string str=lua.GetString("a");
			//Console.WriteLine("a="+str);
			Assert.AreEqual(str,"test");
		}
		/*
		 * Tests setting of global string variables
		 */
		[Test]
		public void SetGlobalString() 
		{
			lua.DoString("a=\"test\"");
			lua["a"]="new test";
			string str=lua.GetString("a");
			//Console.WriteLine("a="+str);
			Assert.AreEqual(str,"new test");
		}
		/*
		 * Tests getting of string variables from tables
		 * by specifying variable path
		 */
		[Test]
		public void GetStringInTable() 
		{
			lua.DoString("a={b={c=\"test\"}}");
			string str=lua.GetString("a.b.c");
			//Console.WriteLine("a.b.c="+str);
			Assert.AreEqual(str,"test");
		}
		/*
		 * Tests setting of string variables from tables
		 * by specifying variable path
		 */
		[Test]
		public void SetStringInTable() 
		{
			lua.DoString("a={b={c=\"test\"}}");
			lua["a.b.c"]="new test";
			string str=lua.GetString("a.b.c");
			//Console.WriteLine("a.b.c="+str);
			Assert.AreEqual(str,"new test");
		}
		/*
		 * Tests getting and setting of global table variables
		 */
		[Test]
		public void GetAndSetTable() 
		{
			lua.DoString("a={b={c=2}}\nb={c=3}");
			LuaTable tab=lua.GetTable("b");
			lua["a.b"]=tab;
			double num=lua.GetNumber("a.b.c");
			//Console.WriteLine("a.b.c="+num);
			Assert.AreEqual(num,3);
		}
		/*
		 * Tests getting of numeric field of a table
		 */
		[Test]
		public void GetTableNumericField1() 
		{
			lua.DoString("a={b={c=2}}");
			LuaTable tab=lua.GetTable("a.b");
			double num=(double)tab["c"];
			//Console.WriteLine("a.b.c="+num);
			Assert.AreEqual(num,2);
		}
		/*
		 * Tests getting of numeric field of a table
		 * (the field is inside a subtable)
		 */
		[Test]
		public void GetTableNumericField2() 
		{
			lua.DoString("a={b={c=2}}");
			LuaTable tab=lua.GetTable("a");
			double num=(double)tab["b.c"];
			//Console.WriteLine("a.b.c="+num);
			Assert.AreEqual(num,2);
		}
		/*
		 * Tests setting of numeric field of a table
		 */
		[Test]
		public void SetTableNumericField1() 
		{
			lua.DoString("a={b={c=2}}");
			LuaTable tab=lua.GetTable("a.b");
			tab["c"]=3;
			double num=lua.GetNumber("a.b.c");
			//Console.WriteLine("a.b.c="+num);
			Assert.AreEqual(num,3);
		}
		/*
		 * Tests setting of numeric field of a table
		 * (the field is inside a subtable)
		 */
		[Test]
		public void SetTableNumericField2() 
		{
			lua.DoString("a={b={c=2}}");
			LuaTable tab=lua.GetTable("a");
			tab["b.c"]=3;
			double num=lua.GetNumber("a.b.c");
			//Console.WriteLine("a.b.c="+num);
			Assert.AreEqual(num,3);
		}
		/*
		 * Tests getting of string field of a table
		 */
		[Test]
		public void GetTableStringField1() 
		{
			lua.DoString("a={b={c=\"test\"}}");
			LuaTable tab=lua.GetTable("a.b");
			string str=(string)tab["c"];
			//Console.WriteLine("a.b.c="+str);
			Assert.AreEqual(str,"test");
		}
		/*
		 * Tests getting of string field of a table
		 * (the field is inside a subtable)
		 */
		[Test]
		public void GetTableStringField2() 
		{
			lua.DoString("a={b={c=\"test\"}}");
			LuaTable tab=lua.GetTable("a");
			string str=(string)tab["b.c"];
			//Console.WriteLine("a.b.c="+str);
			Assert.AreEqual(str,"test");
		}
		/*
		 * Tests setting of string field of a table
		 */
		[Test]
		public void SetTableStringField1() 
		{
			lua.DoString("a={b={c=\"test\"}}");
			LuaTable tab=lua.GetTable("a.b");
			tab["c"]="new test";
			string str=lua.GetString("a.b.c");
			//Console.WriteLine("a.b.c="+str);
			Assert.AreEqual(str,"new test");
		}
		/*
		 * Tests setting of string field of a table
		 * (the field is inside a subtable)
		 */
		[Test]
		public void SetTableStringField2() 
		{
			lua.DoString("a={b={c=\"test\"}}");
			LuaTable tab=lua.GetTable("a");
			tab["b.c"]="new test";
			string str=lua.GetString("a.b.c");
			//Console.WriteLine("a.b.c="+str);
			Assert.AreEqual(str,"new test");
		}
		/*
		 * Tests calling of a global function with zero arguments
		 */
		[Test]
		public void CallGlobalFunctionNoArgs() 
		{
			lua.DoString("a=2\nfunction f()\na=3\nend");
			lua.GetFunction("f").Call();
			double num=lua.GetNumber("a");
			//Console.WriteLine("a="+num);
			Assert.AreEqual(num,3);
		}
		/*
		 * Tests calling of a global function with one argument
		 */
		[Test]
		public void CallGlobalFunctionOneArg() 
		{
			lua.DoString("a=2\nfunction f(x)\na=a+x\nend");
			lua.GetFunction("f").Call(1);
			double num=lua.GetNumber("a");
			//Console.WriteLine("a="+num);
			Assert.AreEqual(num,3);
		}
		/*
		 * Tests calling of a global function with two arguments
		 */
		[Test]
		public void CallGlobalFunctionTwoArgs() 
		{
			lua.DoString("a=2\nfunction f(x,y)\na=x+y\nend");
			lua.GetFunction("f").Call(1,3);
			double num=lua.GetNumber("a");
			//Console.WriteLine("a="+num);
			Assert.AreEqual(num,4);
		}
		/*
		 * Tests calling of a global function that returns one value
		 */
		[Test]
		public void CallGlobalFunctionOneReturn() 
		{
			lua.DoString("function f(x)\nreturn x+2\nend");
			object[] ret=lua.GetFunction("f").Call(3);
			//Console.WriteLine("ret="+ret[0]);
			Assert.AreEqual(1,ret.Length);
			Assert.AreEqual(5,ret[0]);
		}
		/*
		 * Tests calling of a global function that returns two values
		 */
		[Test]
		public void CallGlobalFunctionTwoReturns() 
		{
			lua.DoString("function f(x,y)\nreturn x,x+y\nend");
			object[] ret=lua.GetFunction("f").Call(3,2);
			//Console.WriteLine("ret="+ret[0]+","+ret[1]);
			Assert.AreEqual(2,ret.Length);
			Assert.AreEqual(3,ret[0]);
			Assert.AreEqual(5,ret[1]);
		}
		/*
		 * Tests calling of a function inside a table
		 */
		[Test]
		public void CallTableFunctionTwoReturns() 
		{
			lua.DoString("a={}\nfunction a.f(x,y)\nreturn x,x+y\nend");
			object[] ret=lua.GetFunction("a.f").Call(3,2);
			//Console.WriteLine("ret="+ret[0]+","+ret[1]);
			Assert.AreEqual(2,ret.Length);
			Assert.AreEqual(3,ret[0]);
			Assert.AreEqual(5,ret[1]);
		}
		/*
		 * Tests setting of a global variable to a CLR object value
		 */
		[Test]
		public void SetGlobalObject() 
		{
			TestClass t1=new TestClass();
			t1.testval=4;
			lua["netobj"]=t1;
			object o=lua["netobj"];
			TestClass t2=(TestClass)lua["netobj"];
			Assert.AreEqual(t2.testval,4);
			Assert.AreEqual(t1, t2);
		}
		/*
		 * Tests if CLR object is being correctly collected by Lua
		 */
		[Ignore]
		[Test]
		public void GarbageCollection() 
		{
			TestClass t1=new TestClass();
			t1.testval=4;
			lua["netobj"]=t1;
			TestClass t2=(TestClass)lua["netobj"];
			Assert.AreNotEqual(lua.translator.objects[0], null);
			lua.DoString("netobj=nil;collectgarbage();");
			Assert.AreNotEqual(lua.translator.objects[0], null);
		}
		/*
		 * Tests setting of a table field to a CLR object value
		 */
		[Test]
		public void SetTableObjectField1() 
		{
			lua.DoString("a={b={c=\"test\"}}");
			LuaTable tab=lua.GetTable("a.b");
			TestClass t1=new TestClass();
			t1.testval=4;
			tab["c"]=t1;
			TestClass t2=(TestClass)lua["a.b.c"];
			//Console.WriteLine("a.b.c="+t2.testval);
			Assert.AreEqual(t2.testval,4);
			Assert.IsTrue(t1==t2);
		}
		/*
		 * Tests reading and writing of an object's field
		 */
		[Test]
		public void AccessObjectField() 
		{
			TestClass t1=new TestClass();
			t1.val=4;
			lua["netobj"]=t1;
			lua.DoString("var=netobj.val");
			double var=(double)lua["var"];
			//Console.WriteLine("value from Lua="+var);
			Assert.AreEqual(4,var);
			lua.DoString("netobj.val=3");
			Assert.AreEqual(3,t1.val);
			//Console.WriteLine("new val (from Lua)="+t1.val);
		}
		/*
		 * Tests reading and writing of an object's non-indexed
		 * property
		 */
		[Test]
		public void AccessObjectProperty() 
		{
			TestClass t1=new TestClass();
			t1.testval=4;
			lua["netobj"]=t1;
			lua.DoString("var=netobj.testval");
			double var=(double)lua["var"];
			//Console.WriteLine("value from Lua="+var);
			Assert.AreEqual(4,var);
			lua.DoString("netobj.testval=3");
			Assert.AreEqual(3,t1.testval);
			//Console.WriteLine("new val (from Lua)="+t1.testval);
		}
		/*
		 * Tests calling of an object's method with no overloads
		 */
		[Test]
		public void CallObjectMethod() 
		{
			TestClass t1=new TestClass();
			t1.testval=4;
			lua["netobj"]=t1;
			lua.DoString("netobj:setVal(3)");
			Assert.AreEqual(3,t1.testval);
			//Console.WriteLine("new val(from C#)="+t1.testval);
			lua.DoString("val=netobj:getVal()");
			int val=(int)lua.GetNumber("val");
			Assert.AreEqual(3,val);
			//Console.WriteLine("new val(from Lua)="+val);
		}
		/*
		 * Tests calling of an object's method with overloading
		 */
		[Test]
		public void CallObjectMethodByType() 
		{
			TestClass t1=new TestClass();
			lua["netobj"]=t1;
			lua.DoString("netobj:setVal('str')");
			Assert.AreEqual("str",t1.getStrVal());
			//Console.WriteLine("new val(from C#)="+t1.getStrVal());
		}
		/*
		 * Tests calling of an object's method with no overloading
		 * and out parameters
		 */
		[Ignore]
		[Test]
		public void CallObjectMethodOutParam() 
		{
			TestClass t1=new TestClass();
			lua["netobj"]=t1;
			lua.DoString("a,b=netobj:outVal()");
			int a=(int)lua.GetNumber("a");
			int b=(int)lua.GetNumber("b");
			Assert.AreEqual(3,a);
			Assert.AreEqual(5,b);
			//Console.WriteLine("function returned (from lua)="+a+","+b);
		}
		/*
		 * Tests calling of an object's method with overloading and
		 * out params
		 */
		[Ignore]
		[Test]
		public void CallObjectMethodOverloadedOutParam() 
		{
			TestClass t1=new TestClass();
			lua["netobj"]=t1;
			lua.DoString("a,b=netobj:outVal(2)");
			int a=(int)lua.GetNumber("a");
			int b=(int)lua.GetNumber("b");
			Assert.AreEqual(2,a);
			Assert.AreEqual(5,b);
			//Console.WriteLine("function returned (from lua)="+a+","+b);
		}
		/*
		 * Tests calling of an object's method with ref params
		 */
		[Ignore]
		[Test]
		public void CallObjectMethodByRefParam() 
		{
			TestClass t1=new TestClass();
			lua["netobj"]=t1;
			lua.DoString("a,b=netobj:outVal(2,3)");
			int a=(int)lua.GetNumber("a");
			int b=(int)lua.GetNumber("b");
			Assert.AreEqual(2,a);
			Assert.AreEqual(5,b);
			//Console.WriteLine("function returned (from lua)="+a+","+b);
		}
		/*
		 * Tests calling of two versions of an object's method that have
		 * the same name and signature but implement different interfaces
		 */
		[Ignore]
		[Test]
		public void CallObjectMethodDistinctInterfaces() 
		{
			TestClass t1=new TestClass();
			lua["netobj"]=t1;
			lua.DoString("a=netobj:foo()");
			lua.DoString("b=netobj['LuaInterface.Tests.IFoo1.foo'](netobj)");
			int a=(int)lua.GetNumber("a");
			int b=(int)lua.GetNumber("b");
			Assert.AreEqual(5,a);
			Assert.AreEqual(3,b);
			//Console.WriteLine("function returned (from lua)="+a+","+b);
		}
		/*
		 * Tests instantiating an object with no-argument constructor
		 */
		[Ignore]
		[Test]
		public void CreateNetObjectNoArgsCons()
		{
			lua.DoString("luanet.load_assembly('TestLua')");
			lua.DoString("TestClass=luanet.import_type(\"LuaInterface.Tests.TestClass\")");
			lua.DoString("test=TestClass()");
			lua.DoString("test:setVal(3)");
			object[] res=lua.DoString("return test");
			TestClass test=(TestClass)res[0];
			//Console.WriteLine("returned: "+test.testval);
			Assert.AreEqual(3,test.testval);
		}
		/*
		 * Tests instantiating an object with one-argument constructor
		 */
		[Test]
		public void CreateNetObjectOneArgCons()
		{
			lua.DoString("luanet.load_assembly('TestLua')");
			lua.DoString("TestClass=luanet.import_type(\"LuaInterface.Tests.TestClass\")");
			lua.DoString("test=TestClass(3)");
			object[] res=lua.DoString("return test");
			TestClass test=(TestClass)res[0];
			//Console.WriteLine("returned: "+test.testval);
			Assert.AreEqual(3,test.testval);
		}
		/*
		 * Tests instantiating an object with overloaded constructor
		 */
		[Test]
		public void CreateNetObjectOverloadedCons()
		{
			lua.DoString("luanet.load_assembly('TestLua')");
			lua.DoString("TestClass=luanet.import_type(\"LuaInterface.Tests.TestClass\")");
			lua.DoString("test=TestClass('str')");
			object[] res=lua.DoString("return test");
			TestClass test=(TestClass)res[0];
			//Console.WriteLine("returned: "+test.getStrVal());
			Assert.AreEqual("str",test.getStrVal());
		}
		/*
		 * Tests getting item of a CLR array
		 */
		[Test]
		public void ReadArrayField() 
		{
			string[] arr=new string[] { "str1", "str2", "str3" };
			lua["netobj"]=arr;
			lua.DoString("val=netobj[1]");
			string val=lua.GetString("val");
			Assert.AreEqual("str2",val);
			//Console.WriteLine("new val(from array to Lua)="+val);
		}
		/*
		 * Tests setting item of a CLR array
		 */
		[Test]
		public void WriteArrayField() 
		{
			string[] arr=new string[] { "str1", "str2", "str3" };
			lua["netobj"]=arr;
			lua.DoString("netobj[1]='test'");
			Assert.AreEqual("test",arr[1]);
			//Console.WriteLine("new val(from Lua to array)="+arr[1]);
		}
		/*
		 * Tests creating a new CLR array
		 */
		[Test]
		public void CreateArray() 
		{
			lua.DoString("luanet.load_assembly('TestLua')");
			lua.DoString("TestClass=luanet.import_type(\"LuaInterface.Tests.TestClass\")");
			lua.DoString("arr=TestClass[3]");
			lua.DoString("for i=0,2 do arr[i]=TestClass(i+1) end");
			TestClass[] arr=(TestClass[])lua["arr"];
			Assert.AreEqual(arr[1].testval,2);
		}
		/*
		 * Tests passing a Lua function to a delegate
		 * with value-type arguments
		 */
		[Test]
		public void LuaDelegateValueTypes() 
		{
			lua.DoString("luanet.load_assembly('TestLua')");
			lua.DoString("TestClass=luanet.import_type('LuaInterface.Tests.TestClass')");
			lua.DoString("test=TestClass()");
			lua.DoString("function func(x,y) return x+y; end");
			lua.DoString("test=TestClass()");
			lua.DoString("a=test:callDelegate1(func)");
			int a=(int)lua.GetNumber("a");
			Assert.AreEqual(5,a);
			//Console.WriteLine("delegate returned: "+a);
		}
		/*
		 * Tests passing a Lua function to a delegate
		 * with value-type arguments and out params
		 */
		[Test]
		public void LuaDelegateValueTypesOutParam() 
		{
			lua.DoString("luanet.load_assembly('TestLua')");
			lua.DoString("TestClass=luanet.import_type('LuaInterface.Tests.TestClass')");
			lua.DoString("test=TestClass()");
			lua.DoString("function func(x) return x,x*2; end");
			lua.DoString("test=TestClass()");
			lua.DoString("a=test:callDelegate2(func)");
			int a=(int)lua.GetNumber("a");
			Assert.AreEqual(6,a);
			//Console.WriteLine("delegate returned: "+a);
		}
		/*
		 * Tests passing a Lua function to a delegate
		 * with value-type arguments and ref params
		 */
		[Test]
		public void LuaDelegateValueTypesByRefParam() 
		{
			lua.DoString("luanet.load_assembly('TestLua')");
			lua.DoString("TestClass=luanet.import_type('LuaInterface.Tests.TestClass')");
			lua.DoString("test=TestClass()");
			lua.DoString("function func(x,y) return x+y; end");
			lua.DoString("test=TestClass()");
			lua.DoString("a=test:callDelegate3(func)");
			int a=(int)lua.GetNumber("a");
			Assert.AreEqual(5,a);
			//Console.WriteLine("delegate returned: "+a);
		}
		/*
		 * Tests passing a Lua function to a delegate
		 * with value-type arguments that returns a reference type
		 */
		[Test]
		public void LuaDelegateValueTypesReturnReferenceType() 
		{
			lua.DoString("luanet.load_assembly('TestLua')");
			lua.DoString("TestClass=luanet.import_type('LuaInterface.Tests.TestClass')");
			lua.DoString("test=TestClass()");
			lua.DoString("function func(x,y) return TestClass(x+y); end");
			lua.DoString("test=TestClass()");
			lua.DoString("a=test:callDelegate4(func)");
			int a=(int)lua.GetNumber("a");
			Assert.AreEqual(5,a);
			//Console.WriteLine("delegate returned: "+a);
		}
		/*
		 * Tests passing a Lua function to a delegate
		 * with reference type arguments
		 */
		[Test]
		public void LuaDelegateReferenceTypes() 
		{
			lua.DoString("luanet.load_assembly('TestLua')");
			lua.DoString("TestClass=luanet.import_type('LuaInterface.Tests.TestClass')");
			lua.DoString("test=TestClass()");
			lua.DoString("function func(x,y) return x.testval+y.testval; end");
			lua.DoString("test=TestClass()");
			lua.DoString("a=test:callDelegate5(func)");
			int a=(int)lua.GetNumber("a");
			Assert.AreEqual(5,a);
			//Console.WriteLine("delegate returned: "+a);
		}
		/*
		 * Tests passing a Lua function to a delegate
		 * with reference type arguments and an out param
		 */
		[Test]
		public void LuaDelegateReferenceTypesOutParam() 
		{
			lua.DoString("luanet.load_assembly('TestLua')");
			lua.DoString("TestClass=luanet.import_type('LuaInterface.Tests.TestClass')");
			lua.DoString("test=TestClass()");
			lua.DoString("function func(x) return x,TestClass(x*2); end");
			lua.DoString("test=TestClass()");
			lua.DoString("a=test:callDelegate6(func)");
			int a=(int)lua.GetNumber("a");
			Assert.AreEqual(6,a);
			//Console.WriteLine("delegate returned: "+a);
		}
		/*
		 * Tests passing a Lua function to a delegate
		 * with reference type arguments and a ref param
		 */
		[Test]
		public void LuaDelegateReferenceTypesByRefParam() 
		{
			lua.DoString("luanet.load_assembly('TestLua')");
			lua.DoString("TestClass=luanet.import_type('LuaInterface.Tests.TestClass')");
			lua.DoString("test=TestClass()");
			lua.DoString("function func(x,y) return TestClass(x+y.testval); end");
			lua.DoString("a=test:callDelegate7(func)");
			int a=(int)lua.GetNumber("a");
			Assert.AreEqual(5,a);
			//Console.WriteLine("delegate returned: "+a);
		}
		/*
		 * Tests passing a Lua table as an interface and
		 * calling one of its methods with value-type params
		 */
		[Test]
		public void LuaInterfaceValueTypes() 
		{
			lua.DoString("luanet.load_assembly('TestLua')");
			lua.DoString("TestClass=luanet.import_type('LuaInterface.Tests.TestClass')");
			lua.DoString("test=TestClass()");
			lua.DoString("itest={}");
			lua.DoString("function itest:test1(x,y) return x+y; end");
			lua.DoString("test=TestClass()");
			lua.DoString("a=test:callInterface1(itest)");
			int a=(int)lua.GetNumber("a");
			Assert.AreEqual(5,a);
			//Console.WriteLine("interface returned: "+a);
		}
		/*
		 * Tests passing a Lua table as an interface and
		 * calling one of its methods with value-type params
		 * and an out param
		 */
		[Test]
		public void LuaInterfaceValueTypesOutParam() 
		{
			lua.DoString("luanet.load_assembly('TestLua')");
			lua.DoString("TestClass=luanet.import_type('LuaInterface.Tests.TestClass')");
			lua.DoString("test=TestClass()");
			lua.DoString("itest={}");
			lua.DoString("function itest:test2(x) return x,x*2; end");
			lua.DoString("test=TestClass()");
			lua.DoString("a=test:callInterface2(itest)");
			int a=(int)lua.GetNumber("a");
			Assert.AreEqual(6,a);
			//Console.WriteLine("interface returned: "+a);
		}
		/*
		 * Tests passing a Lua table as an interface and
		 * calling one of its methods with value-type params
		 * and a ref param
		 */
		[Test]
		public void LuaInterfaceValueTypesByRefParam() 
		{
			lua.DoString("luanet.load_assembly('TestLua')");
			lua.DoString("TestClass=luanet.import_type('LuaInterface.Tests.TestClass')");
			lua.DoString("test=TestClass()");
			lua.DoString("itest={}");
			lua.DoString("function itest:test3(x,y) return x+y; end");
			lua.DoString("test=TestClass()");
			lua.DoString("a=test:callInterface3(itest)");
			int a=(int)lua.GetNumber("a");
			Assert.AreEqual(5,a);
			//Console.WriteLine("interface returned: "+a);
		}
		/*
		 * Tests passing a Lua table as an interface and
		 * calling one of its methods with value-type params
		 * returning a reference type param
		 */
		[Test]
		public void LuaInterfaceValueTypesReturnReferenceType() 
		{
			lua.DoString("luanet.load_assembly('TestLua')");
			lua.DoString("TestClass=luanet.import_type('LuaInterface.Tests.TestClass')");
			lua.DoString("test=TestClass()");
			lua.DoString("itest={}");
			lua.DoString("function itest:test4(x,y) return TestClass(x+y); end");
			lua.DoString("test=TestClass()");
			lua.DoString("a=test:callInterface4(itest)");
			int a=(int)lua.GetNumber("a");
			Assert.AreEqual(5,a);
			//Console.WriteLine("interface returned: "+a);
		}
		/*
		 * Tests passing a Lua table as an interface and
		 * calling one of its methods with reference type params
		 */
		[Test]
		public void LuaInterfaceReferenceTypes() 
		{
			lua.DoString("luanet.load_assembly('TestLua')");
			lua.DoString("TestClass=luanet.import_type('LuaInterface.Tests.TestClass')");
			lua.DoString("test=TestClass()");
			lua.DoString("itest={}");
			lua.DoString("function itest:test5(x,y) return x.testval+y.testval; end");
			lua.DoString("test=TestClass()");
			lua.DoString("a=test:callInterface5(itest)");
			int a=(int)lua.GetNumber("a");
			Assert.AreEqual(5,a);
			//Console.WriteLine("interface returned: "+a);
		}
		/*
		 * Tests passing a Lua table as an interface and
		 * calling one of its methods with reference type params
		 * and an out param
		 */
		[Test]
		public void LuaInterfaceReferenceTypesOutParam() 
		{
			lua.DoString("luanet.load_assembly('TestLua')");
			lua.DoString("TestClass=luanet.import_type('LuaInterface.Tests.TestClass')");
			lua.DoString("test=TestClass()");
			lua.DoString("itest={}");
			lua.DoString("function itest:test6(x) return x,TestClass(x*2); end");
			lua.DoString("test=TestClass()");
			lua.DoString("a=test:callInterface6(itest)");
			int a=(int)lua.GetNumber("a");
			Assert.AreEqual(6,a);
			//Console.WriteLine("interface returned: "+a);
		}
		/*
		 * Tests passing a Lua table as an interface and
		 * calling one of its methods with reference type params
		 * and a ref param
		 */
		[Test]
		public void LuaInterfaceReferenceTypesByRefParam() 
		{
			lua.DoString("luanet.load_assembly('TestLua')");
			lua.DoString("TestClass=luanet.import_type('LuaInterface.Tests.TestClass')");
			lua.DoString("test=TestClass()");
			lua.DoString("itest={}");
			lua.DoString("function itest:test7(x,y) return TestClass(x+y.testval); end");
			lua.DoString("a=test:callInterface7(itest)");
			int a=(int)lua.GetNumber("a");
			Assert.AreEqual(5,a);
			//Console.WriteLine("interface returned: "+a);
		}
		/*
		 * Tests passing a Lua table as an interface and
		 * accessing one of its value-type properties
		 */
		[Test]
		public void LuaInterfaceValueProperty() 
		{
			lua.DoString("luanet.load_assembly('TestLua')");
			lua.DoString("TestClass=luanet.import_type('LuaInterface.Tests.TestClass')");
			lua.DoString("test=TestClass()");
			lua.DoString("itest={}");
			lua.DoString("function itest:get_intProp() return itest.int_prop; end");
			lua.DoString("function itest:set_intProp(val) itest.int_prop=val; end");
			lua.DoString("a=test:callInterface8(itest)");
			int a=(int)lua.GetNumber("a");
			Assert.AreEqual(3,a);
			//Console.WriteLine("interface returned: "+a);
		}
		/*
		 * Tests passing a Lua table as an interface and
		 * accessing one of its reference type properties
		 */
		[Test]
		public void LuaInterfaceReferenceProperty() 
		{
			lua.DoString("luanet.load_assembly('TestLua')");
			lua.DoString("TestClass=luanet.import_type('LuaInterface.Tests.TestClass')");
			lua.DoString("test=TestClass()");
			lua.DoString("itest={}");
			lua.DoString("function itest:get_refProp() return TestClass(itest.int_prop); end");
			lua.DoString("function itest:set_refProp(val) itest.int_prop=val.testval; end");
			lua.DoString("a=test:callInterface9(itest)");
			int a=(int)lua.GetNumber("a");
			Assert.AreEqual(3,a);
			//Console.WriteLine("interface returned: "+a);
		}


		/*
		 * Tests making an object from a Lua table and calling the base
		 * class version of one of the methods the table overrides.
		 */
		[Ignore]
		[Test]
		public void LuaTableBaseMethod() 
		{
			lua.DoString("luanet.load_assembly('TestLua')");
			lua.DoString("TestClass=luanet.import_type('LuaInterface.Tests.TestClass')");
			lua.DoString("test={}");
			lua.DoString("function test:overridableMethod(x,y) return 2*self.base:overridableMethod(x,y); end");
			lua.DoString("luanet.make_object(test,'LuaInterface.Tests.TestClass')");
			lua.DoString("a=TestClass:callOverridable(test,2,3)");
			int a=(int)lua.GetNumber("a");
			lua.DoString("free_object(test)");
			Assert.AreEqual(10,a);
			//Console.WriteLine("interface returned: "+a);
		}
		/*
		 * Tests getting an object's method by its signature
		 * (from object)
		 */
		[Test]
		public void GetMethodBySignatureFromObj() 
		{
			lua.DoString("luanet.load_assembly('mscorlib')");
			lua.DoString("luanet.load_assembly('TestLua')");
			lua.DoString("TestClass=luanet.import_type('LuaInterface.Tests.TestClass')");
			lua.DoString("test=TestClass()");
			lua.DoString("setMethod=luanet.get_method_bysig(test,'setVal','System.String')");
			lua.DoString("setMethod('test')");
			TestClass test=(TestClass)lua["test"];
			Assert.AreEqual("test",test.getStrVal());
			//Console.WriteLine("interface returned: "+test.getStrVal());
		}
		/*
		 * Tests getting an object's method by its signature
		 * (from type)
		 */
		[Test]
		public void GetMethodBySignatureFromType() 
		{
			lua.DoString("luanet.load_assembly('mscorlib')");
			lua.DoString("luanet.load_assembly('TestLua')");
			lua.DoString("TestClass=luanet.import_type('LuaInterface.Tests.TestClass')");
			lua.DoString("test=TestClass()");
			lua.DoString("setMethod=luanet.get_method_bysig(TestClass,'setVal','System.String')");
			lua.DoString("setMethod(test,'test')");
			TestClass test=(TestClass)lua["test"];
			Assert.AreEqual("test",test.getStrVal());
			//Console.WriteLine("interface returned: "+test.getStrVal());
		}
		/*
		 * Tests getting a type's method by its signature
		 */
		[Test]
		public void GetStaticMethodBySignature() 
		{
			lua.DoString("luanet.load_assembly('mscorlib')");
			lua.DoString("luanet.load_assembly('TestLua')");
			lua.DoString("TestClass=luanet.import_type('LuaInterface.Tests.TestClass')");
			lua.DoString("make_method=luanet.get_method_bysig(TestClass,'makeFromString','System.String')");
			lua.DoString("test=make_method('test')");
			TestClass test=(TestClass)lua["test"];
			Assert.AreEqual("test",test.getStrVal());
			//Console.WriteLine("interface returned: "+test.getStrVal());
		}
		/*
		 * Tests getting an object's constructor by its signature
		 */
		[Test]
		public void GetConstructorBySignature() 
		{
			lua.DoString("luanet.load_assembly('mscorlib')");
			lua.DoString("luanet.load_assembly('TestLua')");
			lua.DoString("TestClass=luanet.import_type('LuaInterface.Tests.TestClass')");
			lua.DoString("test_cons=luanet.get_constructor_bysig(TestClass,'System.String')");
			lua.DoString("test=test_cons('test')");
			TestClass test=(TestClass)lua["test"];
			Assert.AreEqual("test",test.getStrVal());
			//Console.WriteLine("interface returned: "+test.getStrVal());
		}
        void TestOk(bool flag)
        {
            if (flag)
                Console.WriteLine("Test Passed.");
            else
                Console.WriteLine("Test Failed!!!!");
        }


        /*
         * Tests capturing an exception
         */
        public void ThrowException()
        {
            Init();

            lua.DoString("luanet.load_assembly('mscorlib')");
            lua.DoString("luanet.load_assembly('TestLua')");
            lua.DoString("TestClass=luanet.import_type('LuaInterface.Tests.TestClass')");
            lua.DoString("test=TestClass()");
            lua.DoString("err,errMsg=pcall(test.exceptionMethod,test)");
            bool err = (bool)lua["err"];
            Exception errMsg = (Exception)lua["errMsg"];
            TestOk(!err);
            TestOk(errMsg.InnerException != null);
            if (errMsg.InnerException != null)
            {
                TestOk("exception test" == errMsg.InnerException.Message);
            }
            //Console.WriteLine("interface returned: "+errMsg.ToString());

            Destroy();
        }

        /*
         * Tests capturing an exception
         */
        public void ThrowUncaughtException()
        {
            Init();

            lua.DoString("luanet.load_assembly('mscorlib')");
            lua.DoString("luanet.load_assembly('TestLua')");
            lua.DoString("TestClass=luanet.import_type('LuaInterface.Tests.TestClass')");
            lua.DoString("test=TestClass()");

            try
            {
                lua.DoString("test:exceptionMethod()");

                Console.WriteLine("Test failed!!! Should have thrown an exception all the way out of Lua");
            }
            catch (Exception e)
            {
                Console.WriteLine("Uncaught exception success");
            }

            Destroy();
        }


        /*
         * Tests nullable fields
         */
        public void TestNullable()
        {
            Init();

            lua.DoString("luanet.load_assembly('mscorlib')");
            lua.DoString("luanet.load_assembly('TestLua')");
            lua.DoString("TestClass=luanet.import_type('LuaInterface.Tests.TestClass')");
            lua.DoString("test=TestClass()");

            lua.DoString("val=test.NullableBool");
            TestOk(((object)lua["val"]) == null);
            lua.DoString("test.NullableBool = true");
            lua.DoString("val=test.NullableBool");
            TestOk(((bool)lua["val"]) == true);

            Destroy();
        }


        /*
         * Tests structure assignment
         */
        public void TestStructs()
        {
            Init();

            lua.DoString("luanet.load_assembly('TestLua')");
            lua.DoString("TestClass=luanet.import_type('LuaInterface.Tests.TestClass')");
            lua.DoString("test=TestClass()");
            lua.DoString("TestStruct=luanet.import_type('LuaInterface.Tests.TestStruct')");

            lua.DoString("struct=TestStruct(2)");
            lua.DoString("test.Struct = struct");
            lua.DoString("val=test.Struct.val");
            TestOk(((double)lua["val"]) == 2.0);

            Destroy();
        }

        public void TestMethodOverloads()
        {
            Init();

            lua.DoString("luanet.load_assembly('mscorlib')");
            lua.DoString("luanet.load_assembly('TestLua')");
            lua.DoString("TestClass=luanet.import_type('LuaInterface.Tests.TestClass')");
            lua.DoString("test=TestClass()");
            lua.DoString("test:MethodOverload()");
            lua.DoString("test:MethodOverload(test)");
            lua.DoString("test:MethodOverload(1,1,1)");
            lua.DoString("test:MethodOverload(2,2,i)\r\nprint(i)");
        }

        private void TestDispose()
        {
            System.GC.Collect();
            long startingMem = System.Diagnostics.Process.GetCurrentProcess().WorkingSet64;

            for (int i = 0; i < 10000; i++)
            {
                using (Lua lua = new Lua())
                {
                    _Calc(lua, i);
                }
            }

            Console.WriteLine("Was using " + startingMem / 1024 / 1024 + "MB, now using: " + System.Diagnostics.Process.GetCurrentProcess().WorkingSet64 / 1024 / 1024 + "MB");
        }

        private void _Calc(Lua lua, int i)
        {
            lua.DoString(
                     "sqrt = math.sqrt;" +
                     "sqr = function(x) return math.pow(x,2); end;" +
                     "log = math.log;" +
                     "log10 = math.log10;" +
                     "exp = math.exp;" +
                     "sin = math.sin;" +
                     "cos = math.cos;" +
                     "tan = math.tan;" +
                     "abs = math.abs;"
                     );

            lua.DoString("function calcVP(a,b) return a+b end");

            LuaFunction lf = lua.GetFunction("calcVP");
            Object[] ret = lf.Call(i, 20);
        }

        private void TestThreading()
        {
            Init();

            DoWorkClass doWork = new DoWorkClass();
            lua.RegisterFunction("dowork", doWork, typeof(DoWorkClass).GetMethod("DoWork"));

            bool failureDetected = false;
            int completed = 0;
            int iterations = 500;

            for (int i = 0; i < iterations; i++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(delegate(object o)
                {
                    try
                    {
                        lua.DoString("dowork()");
                    }
                    catch
                    {
                        failureDetected = true;
                    }
                    completed++;
                }));
            }

            while (completed < iterations && !failureDetected)
                Thread.Sleep(50);

            if (failureDetected)
                Console.WriteLine("==Problem with threading!==");
        }

        private void TestPrivateMethod()
        {
            Init();

            lua.DoString("luanet.load_assembly('mscorlib')");
            lua.DoString("luanet.load_assembly('TestLua')");
            lua.DoString("TestClass=luanet.import_type('LuaInterface.Tests.TestClass')");
            lua.DoString("test=TestClass()");
            try
            {
                lua.DoString("test:_PrivateMethod()");
            }
            catch
            {
                Console.WriteLine("Test Passed");
                return;
            }
            Console.WriteLine("Test Failed");
        }

        /*
         * Tests functions
         */
        public void TestFunctions()
        {
            Init();

            lua.DoString("luanet.load_assembly('mscorlib')");
            lua.DoString("luanet.load_assembly('TestLua')");
            lua.RegisterFunction("p", null, typeof(System.Console).GetMethod("WriteLine", new Type[] { typeof(String) }));

            /// Lua command that works (prints to console)
            lua.DoString("p('Foo')");

            /// Yet this works...
            lua.DoString("string.gsub('some string', '(%w+)', function(s) p(s) end)");

            /// This fails if you don't fix Lua5.1 lstrlib.c/add_value to treat LUA_TUSERDATA the same as LUA_FUNCTION
            lua.DoString("string.gsub('some string', '(%w+)', p)");

            Destroy();
        }




        /*
         * Tests making an object from a Lua table and calling one of
         * methods the table overrides.
         */
        public void LuaTableOverridedMethod()
        {
            Init();

            lua.DoString("luanet.load_assembly('TestLua')");
            lua.DoString("TestClass=luanet.import_type('LuaInterface.Tests.TestClass')");
            lua.DoString("test={}");
            lua.DoString("function test:overridableMethod(x,y) return x*y; end");
            lua.DoString("luanet.luanet.make_object(test,'LuaInterface.Tests.TestClass')");
            lua.DoString("a=TestClass.callOverridable(test,2,3)");
            int a = (int)lua.GetNumber("a");
            lua.DoString("luanet.free_object(test)");
            TestOk(6 == a);
            //Console.WriteLine("interface returned: "+a);
        }


        /*
         * Tests making an object from a Lua table and calling a method
         * the table does not override.
         */
        public void LuaTableInheritedMethod()
        {
            Init();

            lua.DoString("luanet.load_assembly('TestLua')");
            lua.DoString("TestClass=luanet.import_type('LuaInterface.Tests.TestClass')");
            lua.DoString("test={}");
            lua.DoString("function test:overridableMethod(x,y) return x*y; end");
            lua.DoString("luanet.luanet.make_object(test,'LuaInterface.Tests.TestClass')");
            lua.DoString("test:setVal(3)");
            lua.DoString("a=test.testval");
            int a = (int)lua.GetNumber("a");
            lua.DoString("luanet.free_object(test)");
            TestOk(3 == a);
            //Console.WriteLine("interface returned: "+a);
        }


        /// <summary>
        /// Basic multiply method which expects 2 floats
        /// </summary>
        /// <param name="val"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        private float _TestException(float val, float val2)
        {
            return val * val2;
        }


        public void TestEventException()
        {
            Init();

            //Register a C# function
            MethodInfo testException = this.GetType().GetMethod("_TestException", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance, null, new Type[] { typeof(float), typeof(float) }, null);
            lua.RegisterFunction("Multiply", this, testException);

            //create the lua event handler code for the entity
            //includes the bad code!
            lua.DoString("function OnClick(sender, eventArgs)\r\n" +
                          "--Multiply expects 2 floats, but instead receives 2 strings\r\n" +
                          "Multiply(asd, we)\r\n" +
                        "end");

            //create the lua event handler code for the entity
            //good code
            //lua.DoString("function OnClick(sender, eventArgs)\r\n" +
            //              "--Multiply expects 2 floats\r\n" +
            //              "Multiply(2, 50)\r\n" +
            //            "end");

            //Create the event handler script
            lua.DoString("function SubscribeEntity(e)\r\ne.Clicked:Add(OnClick)\r\nend");

            //Create the entity object
            Entity entity = new Entity();

            //Register the entity object with the event handler inside lua
            LuaFunction lf = lua.GetFunction("SubscribeEntity");
            lf.Call(new object[1] { entity });

            try
            {
                //Cause the event to be fired
                entity.Click();

                Console.WriteLine("Test failed!!! Should have thrown an exception all the way out of Lua");
            }
            catch (LuaException e)
            {
                Console.WriteLine("Event exception success");
            }

        }

        public void TestExceptionWithChunkOverload()
        {
            Init();

            try
            {
                lua.DoString("thiswillthrowanerror", "MyChunk");
            }
            catch (Exception e)
            {
                if (e.Message.StartsWith("[string \"MyChunk\"]"))
                    Console.WriteLine("Chunk overload passed");
                else
                    Console.WriteLine("Chunk overload failed");
            }
        }

        public void TestGenerics()
        {
            Init();

            //Im not sure support for generic classes is possible to implement, see: http://msdn.microsoft.com/en-us/library/system.reflection.methodinfo.containsgenericparameters.aspx
            //specifically the line that says: "If the ContainsGenericParameters property returns true, the method cannot be invoked"

            //TestClassGeneric<string> genericClass = new TestClassGeneric<string>();

            //lua.RegisterFunction("genericMethod", genericClass, typeof(TestClassGeneric<>).GetMethod("GenericMethod"));
            //lua.RegisterFunction("regularMethod", genericClass, typeof(TestClassGeneric<>).GetMethod("RegularMethod"));

            //try
            //{
            //    lua.DoString("genericMethod('thestring')");
            //}
            //catch { }

            //try
            //{
            //    lua.DoString("regularMethod()");
            //}            
            //catch { }

            //if (genericClass.GenericMethodSuccess && genericClass.RegularMethodSuccess && genericClass.Validate("thestring"))
            //    Console.WriteLine("Generic class passed");
            //else
            //    Console.WriteLine("Generic class failed");

            bool passed = true;
            TestClassWithGenericMethod classWithGenericMethod = new TestClassWithGenericMethod();

            lua.RegisterFunction("genericMethod2", classWithGenericMethod, typeof(TestClassWithGenericMethod).GetMethod("GenericMethod"));

            try
            {
                lua.DoString("genericMethod2(100)");
            }
            catch { }

            if (!classWithGenericMethod.GenericMethodSuccess || !classWithGenericMethod.Validate<double>(100)) //note the gotcha: numbers are all being passed to generic methods as doubles
                passed = false;

            try
            {
                lua.DoString("luanet.load_assembly('TestLua')");
                lua.DoString("TestClass=luanet.import_type('LuaInterface.Tests.TestClass')");
                lua.DoString("test=TestClass(56)");
                lua.DoString("genericMethod2(test)");
            }
            catch { }

            if (!classWithGenericMethod.GenericMethodSuccess || (classWithGenericMethod.PassedValue as TestClass).val != 56)
                passed = false;

            if (passed)
                Console.WriteLine("Class with generic method passed");
            else
                Console.WriteLine("Class with generic method failed");
        }

        public static int func(int x, int y)
        {
            return x + y;
        }
        public int funcInstance(int x, int y)
        {
            return x + y;
        }


        public void RegisterFunctionStressTest()
        {
            LuaFunction fc = null;
            const int Count = 200;  // it seems to work with 41

            Init();

            MyClass t = new MyClass();

            for (int i = 1; i < Count - 1; ++i)
            {
                fc = lua.RegisterFunction("func" + i, t, typeof(MyClass).GetMethod("Func1"));
            }
            fc = lua.RegisterFunction("func" + (Count - 1), t, typeof(MyClass).GetMethod("Func1"));

            lua.DoString("print(func1())");
        }


        /*
         * Sample test script that shows some of the capabilities of
         * LuaInterface
         */
        public static void Main()
        {
            Console.WriteLine("Starting interpreter...");
            Lua l = new Lua();

            // Pause so we can connect with the debugger
            // Thread.Sleep(30000);


            Console.WriteLine("Reading test.lua file...");
            l.DoFile("test.lua");
            double width = l.GetNumber("width");
            double height = l.GetNumber("height");
            string message = l.GetString("message");
            double color_r = l.GetNumber("color.r");
            double color_g = l.GetNumber("color.g");
            double color_b = l.GetNumber("color.b");
            Console.WriteLine("Printing values of global variables width, height and message...");
            Console.WriteLine("width: " + width);
            Console.WriteLine("height: " + height);
            Console.WriteLine("message: " + message);
            Console.WriteLine("Printing values of the 'color' table's fields...");
            Console.WriteLine("color.r: " + color_r);
            Console.WriteLine("color.g: " + color_g);
            Console.WriteLine("color.b: " + color_b);
            width = 150;
            Console.WriteLine("Changing width's value and calling Lua function print to show it...");
            l["width"] = width;
            l.GetFunction("print").Call(width);
            message = "LuaNet Interface Test";
            Console.WriteLine("Changing message's value and calling Lua function print to show it...");
            l["message"] = message;
            l.GetFunction("print").Call(message);
            color_r = 30;
            color_g = 10;
            color_b = 200;
            Console.WriteLine("Changing color's fields' values and calling Lua function print to show it...");
            l["color.r"] = color_r;
            l["color.g"] = color_g;
            l["color.b"] = color_b;
            l.DoString("print(color.r,color.g,color.b)");
            Console.WriteLine("Printing values of the tree table's fields...");
            double leaf1 = l.GetNumber("tree.branch1.leaf1");
            string leaf2 = l.GetString("tree.branch1.leaf2");
            string leaf3 = l.GetString("tree.leaf3");
            Console.WriteLine("leaf1: " + leaf1);
            Console.WriteLine("leaf2: " + leaf2);
            Console.WriteLine("leaf3: " + leaf3);
            leaf1 = 30; leaf2 = "new leaf2";
            Console.WriteLine("Changing tree's fields' values and calling Lua function print to show it...");
            l["tree.branch1.leaf1"] = leaf1; l["tree.branch1.leaf2"] = leaf2;
            l.DoString("print(tree.branch1.leaf1,tree.branch1.leaf2)");
            Console.WriteLine("Returning values from Lua with 'return'...");
            object[] vals = l.DoString("return 2,3");
            Console.WriteLine("Returned: " + vals[0] + " and " + vals[1]);
            Console.WriteLine("Calling a Lua function that returns multiple values...");
            object[] vals1 = l.GetFunction("func").Call(2, 3);
            Console.WriteLine("Returned: " + vals1[0] + " and " + vals1[1]);
            Console.WriteLine("Creating a table and filling it from C#...");
            l.NewTable("tab");
            l.NewTable("tab.tab");
            l["tab.a"] = "a!";
            l["tab.b"] = 5.5;
            l["tab.tab.c"] = 6.5;
            l.DoString("print(tab.a,tab.b,tab.tab.c)");
            Console.WriteLine("Setting a table as another table's field...");
            l["tab.a"] = l["tab.tab"];
            l.DoString("print(tab.a.c)");
            Console.WriteLine("Registering a C# static method and calling it from Lua...");

            // Pause so we can connect with the debugger
            // Thread.Sleep(30000);

            l.RegisterFunction("func1", null, typeof(TestLuaInterface).GetMethod("func"));
            vals1 = l.GetFunction("func1").Call(2, 3);
            Console.WriteLine("Returned: " + vals1[0]);
            TestLuaInterface obj = new TestLuaInterface();
            Console.WriteLine("Registering a C# instance method and calling it from Lua...");
            l.RegisterFunction("func2", obj, typeof(TestLuaInterface).GetMethod("funcInstance"));
            vals1 = l.GetFunction("func2").Call(2, 3);
            Console.WriteLine("Returned: " + vals1[0]);

            Console.WriteLine("Testing throwing an exception...");
            obj.ThrowUncaughtException();

            Console.WriteLine("Testing catching an exception...");
            obj.ThrowException();

            Console.WriteLine("Testing inheriting a method from Lua...");
            obj.LuaTableInheritedMethod();

            Console.WriteLine("Testing overriding a C# method with Lua...");
            obj.LuaTableOverridedMethod();

            Console.WriteLine("Stress test RegisterFunction (based on a reported bug)..");
            obj.RegisterFunctionStressTest();

            Console.WriteLine("Test structures...");
            obj.TestStructs();

            Console.WriteLine("Test Nullable types...");
            obj.TestNullable();

            Console.WriteLine("Test functions...");
            obj.TestFunctions();

            Console.WriteLine("Test method overloads...");
            obj.TestMethodOverloads();

            Console.WriteLine("Test accessing private method...");
            obj.TestPrivateMethod();

            Console.WriteLine("Test event exceptions...");
            obj.TestEventException();

            Console.WriteLine("Test chunk overload exception...");
            obj.TestExceptionWithChunkOverload();

            Console.WriteLine("Test generics...");
            obj.TestGenerics();

            Console.WriteLine("Test threading...");
            obj.TestThreading();

            Console.WriteLine("Test memory leakage...");
            obj.TestDispose();

            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();
        }
    }
}
