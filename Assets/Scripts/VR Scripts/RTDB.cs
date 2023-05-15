using System.Collections.Generic;


public class RTDB
{
    private Dictionary<string, string> _data;

    public RTDB()
    {
        _data = new Dictionary<string, string>();
    }

    public void Set(string key, string value)
    {
        if (_data.ContainsKey(key))
        {
            _data[key] = value;
        }
        else
        {
            _data.Add(key, value);
        }
    }

    public string GetString(string key)
    {
        if (_data.TryGetValue(key, out string value))
        {
            return value;
        }
        return null;
    }
}