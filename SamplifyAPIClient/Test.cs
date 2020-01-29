using System;
using Dynata.SamplifyAPIClient;

class Test
{
    static public void Main(string[] args)
    {
        Console.WriteLine("hi");

        SamplifyClient client = new SamplifyClient("api", "test", "test", SamplifyEnv.DEV);
    }
}