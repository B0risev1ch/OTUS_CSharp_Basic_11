// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

OtusDictionary otusDictionary = new OtusDictionary();

try
{
    otusDictionary.Add(0, "first");
    otusDictionary.Add(2, "second");
    //otusDictionary.Add(2, "second again"); //Exception!
    otusDictionary.Add(38, "thirty eight");  //div 16 == 6
    otusDictionary.Add(4, "may the fourth be with you");
    otusDictionary.Add(22, "twenty two");    //div 16 == 6..again


    otusDictionary.Get(22);
    otusDictionary.Get(38);
    // otusDictionary.Get(10); //Exception!
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

class OtusDictionary
{
    private class Element
    {
        public int Key;
        public string Value;

    }

    private Element[] _entries;
    private int _count;
    public OtusDictionary(int capacity = 16)
    {
        _entries = new Element[capacity];
    }

    public void Add(int key, string value)
    {
        if (value == null)
            throw new ArgumentNullException(nameof(value));

        int index = key % _entries.Length;

        while (_entries[index] != null && _entries[index].Key != key)
        {
            index = (index + 1) % _entries.Length;
        }

        if (_entries[index] != null)
        {
            throw new ArgumentException($"А уже есть такой ключ: '{index} : {_entries[index].Value}'");
        }

        Console.WriteLine($"Добавил '{key} : {value}' на место {index}");
        _entries[index] = new Element { Key = key, Value = value };
        _count++;

        if (_count >= _entries.Length * .7)
        {
            Rehash();
        }

    }

    public string Get(int key)
    {
        int index = key % _entries.Length;

        while (_entries[index] != null && _entries[index].Key != key)
        {
            index = (index + 1) % _entries.Length;
        }

        if (_entries[index] != null)
        {
            Console.WriteLine($"Прочитал '{key} : {_entries[index].Value}' на месте {index}");
            return _entries[index].Value;
        }



        throw new KeyNotFoundException($"Ключа '{key}' нет в таблице");
    }

    private void Rehash()
    {
        var oldEntries = _entries;
        _entries = new Element[_entries.Length * 2];
        _count = 0;

        foreach (var entry in oldEntries)
        {
            if (entry != null)
            {
                Add(entry.Key, entry.Value);
            }
        }
    }
}