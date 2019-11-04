using Gympass.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Gympass.Domain.InfraEstrutura
{
    public class ReadLog : IReadLog
    {
        private string[] _lines;
        private static string _path;

        /// <summary>
        /// Recebe o caminho do arquivo de log
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string[] ReadResult(string path)
        {
            _path = path;
            var list = ReadFile();
            _lines = list.ToArray();

            return _lines;
        }

        /// <summary>
        /// Realiza a leitura do arquivo
        /// </summary>
        /// <returns></returns>
        private static List<string> ReadFile()
        {
            var list = new List<string>();

            try
            {
                var fileStream = new FileStream(_path, FileMode.Open, FileAccess.Read);

                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        list.Add(line);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            return list;
        }
    }
}
