using System;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Core;

// JSONConfig
// rigid/structured data
public class JSONConfig<T>
{
	public T? data { get; private set; }

	public JSONConfig(string filename)
	{
		if (File.Exists(filename))
		{
			string jsonText = File.ReadAllText(filename);

			// Console.WriteLine("JSONConfig() got JSON data:");
			// Console.WriteLine(jsonText);

			data = JsonSerializer.Deserialize<T>(jsonText);

			// Console.WriteLine((data != null) ? "JSON read successfully!" : "JSON failed to load...");
		}
		else
		{
			Console.WriteLine($"Unable to find file '{filename}' in JSONConfig()");
		}
	}
}

// JSONSettings
// fluid/dynamic data
public class JSONSettings
{
	public JsonNode? data { get; private set; }

	private string filename;

	public JSONSettings(string filename)
	{
		this.filename = filename;
		data = null;

		if (File.Exists(filename))
		{
			string jsonText = File.ReadAllText(filename);

			// Console.WriteLine("JSONSettings() got JSON data:");
			// Console.WriteLine(jsonText);

			data = JsonNode.Parse(jsonText);

			// Console.WriteLine($"JSON blob: {data!.ToJsonString()}");
		}
		else
		{
			Console.WriteLine($"Unable to find file '{filename}' in JSONSettings()");
		}
	}

	// write the data
	public bool Save(string? saveTo = null)
	{
		if (data == null)
		{
			return false;
		}

		string? outfile = saveTo ?? filename ?? null;
		if (outfile == null)
		{
			return false;
		}

		// set options for human-readable/editable file
		var options = new JsonSerializerOptions{
			WriteIndented = true,
			IndentSize = 1,
			IndentCharacter = '\t'
		};
		string jsonText = data.ToJsonString(options);

		// TODO: try/catch this?
		File.WriteAllText(outfile, jsonText);

		return true;
	}

	//
	public JsonNode? ValueForKey(string key)
	{
		return (data != null) ? _FindKey(data, new List<string>(key.Split('.'))) : null;
	}

	//
	public T ValueForKey<T>(string key, T fallback)
	{
		JsonValue? node = ValueForKey(key) as JsonValue;

		if ((node != null) && (node.TryGetValue<T>(out T? res)))
		{
			return res;
		}

		return fallback;
	}

	//
	private JsonNode? _FindKey(JsonNode node, List<string> keys)
	{
		JsonObject? asObject = node as JsonObject;
		JsonArray? asArray = node as JsonArray;

		if ((asObject != null) || (asArray != null))
		{
			if (keys.Count > 0)
			{
				JsonNode? child = null;

				if (asObject != null)
				{
					// node as object
					child = asObject[keys[0]];
				}
				else if (asArray != null)
				{
					// node as array
					var index = int.Parse(keys[0]);
					if ((index >= 0) && (index < asArray.Count))
					{
						child = asArray[index];
					}
				}

				if (child != null)
				{
					if (keys.Count == 1)
					{
						return child;
					}

					keys.RemoveAt(0);

					return _FindKey(child, keys);
				}

			}
		}

		return null;
	}
}
