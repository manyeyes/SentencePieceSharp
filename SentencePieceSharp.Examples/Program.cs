// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using System.Text;
using SentencePieceSharp;

internal static partial class Program
{
    public static string applicationBase = AppDomain.CurrentDomain.BaseDirectory;
    public const string SentencePieceModelName = "bbpe.model";
    private const string DownloadUrl = "https://modelscope.cn/models/manyeyes/sherpa-onnx-streaming-zipformer-small-ctc-zh-int8-2025-04-01/resolve/master/bbpe.model";
    //public const string SentencePieceModelName = "sentencepiece.bpe.model";
    //private const string DownloadUrl = "https://huggingface.co/intfloat/multilingual-e5-large/resolve/main/sentencepiece.bpe.model";


    private static SentencePieceProcessor _processor;

    [STAThread]
    private static async Task Main()
    {
        string SentencePieceModelPath = Path.Join(applicationBase, "models", SentencePieceModelName);
        await Initialize(SentencePieceModelPath);
        LoadModel(SentencePieceModelPath);
        // zipformer.bbpe.model
        string[] testArr = new string[] { "▁ƌŕş", "▁ƍĩĴ", "▁ƌĢĽ", "▁ƋŠħ", "▁ƋšĬ", "▁Ǝ", "š", "Į", "▁Ɛģň", "▁Ƌşĩ", "▁ƍĩĴ", "▁ƍĤř", "▁ƏŕŚ", "▁ƎĽĥ", "▁ƍĻŕ", "▁ƌĴŇ", "▁ƌŊō", "▁ƌŔŜ", "▁ƌŌģ", "▁ƍŃŁ", "▁ƌŕş", "▁ƍĩĴ", "▁ƎĽĥ", "▁ƎŅķ", "▁ƎŏŜ", "▁ƍĥń", "▁ƌĦŚ", "▁Ə", "Ŝ", "ň", "▁ƌĴŇ" };
        int[] result1 = TestEncodeWithBosEos(string.Join("", testArr));
        TestDecodeWithBosEos(result1, string.Join("", testArr));
        int[] result2 = TestEncodeWithBosEos("▁ƍ▁ƌľţ");
        TestDecodeWithBosEos(result2, "▁ƍ▁ƌľţ");

        //// sentencepiece.bpe.model
        //int[] result3 = TestEncodeWithBosEos("hello, you are a big dog");
        //TestDecodeWithBosEos(result3, "hello, you are a big dog");
        //TestDecodeWithBosEos(new[] { 1, 35377, 3, 8998, 37, 2 }, "Hello, world!");
    }


    public static async Task Initialize(string sentencePieceModelPath)
    {
        if (!File.Exists(sentencePieceModelPath))
        {
            using var client = new HttpClient();
            try
            {
                await DownloadFileAsync(DownloadUrl, sentencePieceModelPath);
                Console.WriteLine("文件下载成功！");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"下载文件时出错: {ex.Message}");
            }
        }
    }
    static async Task DownloadFileAsync(string downloadUrl, string savePath)
    {
        using (HttpClient client = new HttpClient())
        {
            client.Timeout = TimeSpan.FromSeconds(30);

            // 添加常见的请求头来模拟浏览器访问
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");
            client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            client.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.9");
            // 如果需要可以添加 Referer，这里只是示例，需要根据实际情况修改
            client.DefaultRequestHeaders.Add("Referer", "https://example.com");

            using (HttpResponseMessage response = await client.GetAsync(downloadUrl, HttpCompletionOption.ResponseHeadersRead))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"请求失败，状态码: {response.StatusCode}");
                }

                long? totalBytes = response.Content.Headers.ContentLength;
                long bytesRead = 0;

                using (Stream contentStream = await response.Content.ReadAsStreamAsync())
                using (FileStream fileStream = new FileStream(savePath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true))
                {
                    byte[] buffer = new byte[8192];
                    int bytes;
                    while ((bytes = await contentStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        await fileStream.WriteAsync(buffer, 0, bytes);
                        bytesRead += bytes;

                        if (totalBytes.HasValue)
                        {
                            double progress = (double)bytesRead / totalBytes.Value * 100;
                            Console.Write($"\r下载进度: {progress:F2}%");
                        }
                    }
                }

                if (totalBytes.HasValue && bytesRead != totalBytes.Value)
                {
                    throw new IOException("下载的文件大小与预期不符");
                }
            }
        }
    }


    public static void LoadModel(string SentencePieceModelPath)
    {
        _processor = new SentencePieceProcessor();
        var status = _processor.Load(SentencePieceModelPath);
        if (status)
        {
            _processor.SetEncodeExtraOptions(ExtraOptions.EncodeBosEos);
        }
    }

    public static int[] TestEncodeWithBosEos(string value, int[] tokenIds = null)
    {
        int[] result = _processor.Encode(value);
        Console.WriteLine(string.Join(",", result));
        Debug.Assert(result == tokenIds);
        return result;
    }

    public static void TestDecodeWithBosEos(int[] tokenIds, string value)
    {
        string result = _processor.Decode(tokenIds);
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine(result);
        Debug.Assert(result == value);
    }

    public static void TestDecodePiecesWithBosEos(string[] pieces, string value)
    {
        string result = _processor.DecodePieces(pieces);
        Console.OutputEncoding = Encoding.Unicode;
        Console.WriteLine(result);
        Debug.Assert(result == value);
    }

    public static void Dispose()
    {
        _processor.Dispose();
    }
}
