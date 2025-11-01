using System;
using System.Text;
using YoutubeExplode;
using System.Threading.Tasks;
using YoutubeExplode.Converter;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;
using System.IO;

namespace _18_4_2_project
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Давай ссылку на видео");
            string link = Console.ReadLine();
            
            var youtube = new YoutubeClient();
            
            var video = await youtube.Videos.GetAsync(link);
            
            var title = video.Title;
            var author = video.Author.ChannelTitle; 
            var duration = video.Duration;

            Console.WriteLine("Название: " + video.Title + "\n" + "Автор: " + video.Author.ChannelTitle + "\n" + "Продолжительность: " + video.Duration + "\n");

            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(link);
            var streamInfo = streamManifest.GetMuxedStreams().GetWithHighestVideoQuality();
            string outputFilePath = Path.Combine(
            @"C:\Users\ASUS\source\repos\18-4-2-project\18-4-2-project\bin\Debug\net5.0",
            $"{title}.mp4"
            );
            await youtube.Videos.Streams.DownloadAsync(streamInfo, outputFilePath);
             Console.WriteLine($"Видео сохранено как: {outputFilePath}");
        }
    }
}
