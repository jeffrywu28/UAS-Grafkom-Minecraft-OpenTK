using System;

using OpenTK;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using LearnOpenTK.Common;
using ConsoleApp2;

namespace Pertemuan_1
{ 
    
    class Program
    {
        

        static void Main(string[] args)
        {
            var nativeWindowSetting = new NativeWindowSettings()
            { 
                Size =new Vector2i(1270,720),
                Title = "tes"//0.0.03
            };

            // var window akan dihapus dr memori setelah
            // perintah di dalam selesai di eksekusi
            using (var window = new Window(GameWindowSettings.Default, nativeWindowSetting))
            {
                window.Run();
            }
        }
    }

}
