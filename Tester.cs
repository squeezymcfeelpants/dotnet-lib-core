using System;
using Core;

namespace dotnet_lib_core;

//
public class Tester
{
	//
	private static JSONConfig<AppConfig>? config = null;
	private static JSONSettings? settings = null;

	//
	public static void TestStaticConfigLoader()
	{
		Console.WriteLine("**** Running TestStaticConfigLoader()...");

		config = new JSONConfig<AppConfig>("../../../config.json");
		if (config != null)
		{
			Console.WriteLine("Successfully read config.json");
			Console.WriteLine($"OSCTargets: {config.data?.oscTargets.Count}");

			var oscTargets = config.data?.oscTargets;
			if (oscTargets != null)
			{
				oscTargets.ForEach((target) =>
				{
					Console.WriteLine($"Process OSC target '{target.name}' with IP address '{target.ipAddress}'");
				});
			}
		}
		else
		{
			Console.WriteLine("Unable to read config.json");
		}
	}

	//
	public static void TestStaticConfigLoaderFail()
	{
		Console.WriteLine("**** Running TestStaticConfigLoaderFail()...");
		
		var noconfig = new JSONConfig<AppConfig>("config.json");
	}

	//
	public static void TestSettingsLoader()
	{
		Console.WriteLine("**** Running TestSettingsLoader()...");

		settings = new JSONSettings("../../../settings.json");
		settings.Save();
	}

	//
	public static void TestSettingsLoaderFail()
	{
		Console.WriteLine("**** Running TestSettingsLoaderFail()...");

		settings = new JSONSettings("settings.json");
	}
}
