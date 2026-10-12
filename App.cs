using Core;

namespace dotnet_lib_core;

class Program
{
    static void Main(string[] args)
    {
		Tester.TestStaticConfigLoader();
		Tester.TestStaticConfigLoaderFail();

		Tester.TestSettingsLoaderFail();
		Tester.TestSettingsLoader();
    }
}
