using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reading_files_async
{
    internal class ReadingFiles
    {
        public static async Task ReadingFilesAsync(params string[] namesOfFiles)
        {
            var pathOfSourceFile = "Result.txt";
            if (namesOfFiles == null)
                throw new ArgumentNullException(nameof(namesOfFiles));

            for (int i = 0; i < namesOfFiles.Length; i++)
            {
                if (!File.Exists(namesOfFiles[i]))
                    throw new FileNotFoundException("Файл с данными не найден", namesOfFiles[i]);
                await File.AppendAllLinesAsync(pathOfSourceFile, File.ReadLines(namesOfFiles[i]));
            }           
        }
    }
}
