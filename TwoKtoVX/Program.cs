using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace TwoKtoVX {
    static class Program {
        static void Main() {

            // Get our paths and locations for easy access later.
            var curpath = Environment.CurrentDirectory;
            var inpath = Path.Combine(curpath, "input");
            var outpath = Path.Combine(curpath, "output");

            // Make sure everything exists.
            if (!Directory.Exists(inpath)) Directory.CreateDirectory(inpath);
            if (!Directory.Exists(outpath)) Directory.CreateDirectory(outpath);

            // Tell our user what to do next.
            Console.WriteLine("Please place your RPG Maker 2000 Formatted sprites in the input folder where this application resides.");
            Console.WriteLine("Once you've done this, hit any key to continue.");
            Console.WriteLine(String.Empty);
            Console.ReadKey();

            // Process our images.
            foreach (var file in new DirectoryInfo(inpath).EnumerateFiles("*.png")) {
                using (var tmp = Image.FromFile(file.FullName)) {
                    var width = tmp.Width / 3;
                    var height = tmp.Height / 4;

                    using (var newimage = new Bitmap(tmp.Width, tmp.Height , PixelFormat.Format32bppArgb)) {
                        using (var g = Graphics.FromImage(newimage)) {
                            g.Clear(Color.Transparent);
                            g.DrawImage(tmp, new Rectangle(0, 0, tmp.Width, height), new Rectangle(0, height * 2, tmp.Width, height), GraphicsUnit.Pixel);
                            g.DrawImage(tmp, new Rectangle(0, height, tmp.Width, height), new Rectangle(0, height * 3, tmp.Width, height), GraphicsUnit.Pixel);
                            g.DrawImage(tmp, new Rectangle(0, height * 2, tmp.Width, height), new Rectangle(0, height, tmp.Width, height), GraphicsUnit.Pixel);
                            g.DrawImage(tmp, new Rectangle(0, height * 3, tmp.Width, height ), new Rectangle(0, 0, tmp.Width, height), GraphicsUnit.Pixel);

                            g.Flush();
                        }
                        newimage.Save(Path.Combine(outpath, file.Name), ImageFormat.Png);
                        Console.WriteLine(String.Format("Processed {0}.", file.Name));
                    }
                }
            }

            // notify the user we're done.
            Console.WriteLine(String.Empty);
            Console.WriteLine("All files have been processed, hit any key to close the application.");
            Console.ReadKey();
        }
    }
}
