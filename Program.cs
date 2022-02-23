// See https://aka.ms/new-console-template for more information
using System.IO.Compression;

var pwd = Path.Combine(Directory.GetCurrentDirectory(), "test");
var stream = File.Create(@"out.zip");
var directory = new DirectoryInfo(pwd);

using (var zip = new ZipArchive(stream, ZipArchiveMode.Create, true))
{
    foreach (FileInfo file in directory.EnumerateFiles("*", SearchOption.AllDirectories))
    {
        string relativePath =
            file.FullName.Substring(pwd.Length + 1); // +1 prevents it from including the leading backslash
        string zipEntryName = relativePath.Replace('\\', '/'); // Normalize slashes

        zip.CreateEntryFromFile(file.FullName, zipEntryName);
    }
}
