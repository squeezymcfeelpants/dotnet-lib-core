namespace dotnet_lib_core;

//
public class AppConfig
{
	public class ConfigOSCTarget
	{
		public string name { get; set; }
		public string ipAddress { get; set; }

		//
		public ConfigOSCTarget()
		{
			name = string.Empty;
			ipAddress = string.Empty;
		}
	}

	public string filePath { get; set; }
	public string audioPath { get; set; }
	public string messageWindow { get; set; }
	public int midiDevice { get; set; }
	public int midiChannel { get; set; }
	public int midiLightingDevice { get; set; }
	public int midiLightingChannel { get; set; }
	public List<ConfigOSCTarget> oscTargets { get; set; }
	
	//
	public AppConfig()
	{
		filePath = string.Empty;
		audioPath = string.Empty;
		messageWindow = string.Empty;

		midiDevice = 0;
		midiChannel = 0;
		midiLightingDevice = 0;
		midiLightingChannel = 0;

		oscTargets = new List<ConfigOSCTarget>();
	}
}
