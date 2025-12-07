using System;
using System.IO;

namespace TextEditor
{
    public interface IConsole
    {
        void Clear();
        void WriteLine(string value);
        void Write(string value);
        string ReadLine();
        ConsoleKeyInfo ReadKey();
    }

    public class SystemConsole : IConsole
    {
        public void Clear() => Console.Clear();
        public void WriteLine(string value) => Console.WriteLine(value);
        public void Write(string value) => Console.Write(value);
        public string ReadLine() => Console.ReadLine();
        public ConsoleKeyInfo ReadKey() => Console.ReadKey();
    }

    public class TextEditor
    {
        private readonly IConsole _console;
        public TextEditor(IConsole console) => _console = console;

        public void Menu()
        {
            _console.Clear();
            _console.WriteLine("O que você deseja fazer?");
            _console.WriteLine("1 - Abrir arquivo");
            _console.WriteLine("2 - Criar novo arquivo");
            _console.WriteLine("0 - Sair");

            short option = short.Parse(_console.ReadLine());
            switch (option)
            {
                case 1:
                    OpenFile(); break;
                case 2:
                    CreateFile(); break;
                case 0:
                    Environment.Exit(0); break;
                default:
                    Menu(); break;
            }
        }

        public void OpenFile()
        {
            _console.Clear();
            _console.WriteLine("Qual o caminho do arquivo?");
            string path = _console.ReadLine();

            using (var file = new StreamReader(path))
            {
                string text = file.ReadToEnd();
                _console.WriteLine(text);
            }
            _console.WriteLine("");
            _console.ReadLine();
            Menu();
        }

        public void CreateFile()
        {
            _console.Clear();
            _console.WriteLine("Digite seu texto abaixo:");
            _console.WriteLine("-----------------------");

            string text = "";

            do
            {
                text += _console.ReadLine() + Environment.NewLine;
            }
            while (_console.ReadKey().Key != ConsoleKey.Escape);

            SaveFile(text);
        }

        public void SaveFile(string text)
        {
            _console.Clear();
            _console.WriteLine("Qual caminho para salvar o arquivo?");
            var path = _console.ReadLine();

            using (var file = new StreamWriter(path))
            {
                file.Write(text);
            }
            _console.WriteLine($"Arquivo {path} salvo com sucesso");
            _console.ReadLine();
            Menu();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var editor = new TextEditor(new SystemConsole());
            editor.Menu();
        }
    }
}