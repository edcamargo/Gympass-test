namespace Gympass.Domain.Interfaces
{
    public interface IReadLog
    {
        /// <summary>
        /// Recebe o caminho do arquivo de log
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        string[] ReadResult(string path);
    }
}
