using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProjectFoldersCleanUp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Removes BIN and OBJ folders, .suo and .user files from Visual Studio projects");

            //var dir = Directory.GetCurrentDirectory();
            Console.Write("Enter solution path: ");
            var dir = Console.ReadLine();

            List<string> files = new List<string>();

            if (!string.IsNullOrWhiteSpace(dir))
            {
                var dirNames = Directory.GetDirectories(dir, "*.*", SearchOption.AllDirectories);

                foreach (var dirName in dirNames)
                {
                    files.AddRange(Directory.GetFiles(dirName, "*.suo"));
                    files.AddRange(Directory.GetFiles(dirName, "*.user"));
                }

                foreach (var file in files)
                {
                    try
                    {
                        Console.WriteLine("Deleting: {0}", file);
                        File.Delete(file);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("File delete error: {0}", ex.Message);
                        throw;
                    }
                }

                foreach (var dirName in dirNames)
                {
                    var dn = Path.GetFileName(dirName).ToLower();

                    if (dn == "bin" || dn == "obj")
                    {
                        try
                        {
                            Console.WriteLine("Deleting: {0}", dirName);
                            Directory.Delete(dirName, true);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Failed to delete: {0}", ex.Message);
                            throw;
                        }
                    }
                }
            }

            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
