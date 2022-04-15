using KnowledgePicker.WordCloud;
using KnowledgePicker.WordCloud.Coloring;
using KnowledgePicker.WordCloud.Drawing;
using KnowledgePicker.WordCloud.Layouts;
using KnowledgePicker.WordCloud.Primitives;
using KnowledgePicker.WordCloud.Sizers;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WordCloud.Proxy
{
    public class WordCloud
    {
        public static void Create()
        {
            if(StaticCache.Keywords == null)
            {
                return;
            }

            IEnumerable<WordCloudEntry> wordEntries = StaticCache.Keywords
                .OrderByDescending(o => o.Value).Take(300).Select(p => new WordCloudEntry(p.Key, p.Value));

            var wordCloud = new WordCloudInput(wordEntries)
            {
                Width = 1920,
                Height = 1080,
                MinFontSize = 32,
                MaxFontSize = 128,
            };


            var sizer = new LogSizer(wordCloud);
            var name = "Arial";
            SKTypeface skTypeface = null;
            try
            { 
                name = "Microsoft YaHei";
                skTypeface = SKTypeface.FromFamilyName(name);
            }
            catch
            {

            }

            try
            {
                name = "华文黑体";
                skTypeface = SKTypeface.FromFamilyName(name, 16, 16, SKFontStyleSlant.Italic);//字体
                //skTypeface = SKTypeface.FromFamilyName(name);
            }
            catch
            {

            }

            //Unable to load shared library 'libSkiaSharp'
            /*
             	InnerException	{System.DllNotFoundException: Unable to load shared library 'libSkiaSharp' or one of its dependencies. In order to help diagnose loading problems, consider setting the DYLD_PRINT_LIBRARIES environment variable: dlopen(liblibSkiaSharp, 0x0001): tried: 'liblibSkiaSharp' (no such file), '/usr/local/lib/liblibSkiaSharp' (no such file), '/usr/lib/liblibSkiaSharp' (no such file), '/Users/odinluo/Documents/Today.WordCloud/bin/Debug/net6.0/liblibSkiaSharp' (no such file)    at SkiaSharp.SkiaApi.sk_colortype_get_default_8888()    at SkiaSharp.SKImageInfo..cctor()}	System.DllNotFoundException
             */
            using var engine = skTypeface == null ?
               new SkGraphicEngine(sizer, wordCloud):
               new SkGraphicEngine(sizer, wordCloud, skTypeface);
            SpiralLayout layout = new SpiralLayout(wordCloud);
            var colorizer = new RandomColorizer(); // optional
            var wcg = new WordCloudGenerator<SKBitmap>(wordCloud, engine, layout, colorizer);

            IEnumerable<(LayoutItem Item, double FontSize)> items = wcg.Arrange();

            using var bitmap = new SKBitmap(wordCloud.Width, wordCloud.Height);
            using var canvas = new SKCanvas(bitmap);

            // Draw on white background.
            canvas.Clear(SKColors.White);
            canvas.DrawBitmap(wcg.Draw(), 0, 0); 
             
            // Save to PNG.
            using var data = bitmap.Encode(SKEncodedImageFormat.Png, 100);
            var fileName = $"{DateTime.Now.ToFileTime()}.png";
            using var writer = File.Create(fileName);
            data.SaveTo(writer);
            Console.WriteLine($"云图{fileName}生成！");
            
            Environment.Exit(0);
        }
    }
}
