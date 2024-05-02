namespace QingYi.Tools.Download.Multithread
{
    internal class SingleFileMultithread(string url, string outputPath, int numThreads = 16)
    {
        private int numThreads = numThreads;
        private long fileSize;

        // 设置并发线程数
        public void SetNumThreads(int numThreads)
        {
            if (numThreads <= 0)
            {
                throw new ArgumentException("Number of threads must be greater than zero.");
            }
            this.numThreads = numThreads;
        }

        public void Download()
        {
            // 打印下载开始信息
            Console.WriteLine("Downloading...");

            // 使用HttpClient类发起GET请求，获取响应头信息
            using (HttpClient client = new())
            using (HttpResponseMessage response = client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead).Result)
            {
                // 获取文件大小
                fileSize = response.Content.Headers.ContentLength ?? 0;
            }

            // 计算每个线程下载的文件块大小
            long chunkSize = fileSize / numThreads;

            // 遍历每个线程，计算每个线程下载的文件块的起始和结束位置
            for (int i = 0; i < numThreads; i++)
            {
                int threadId = i;
                long start = i * chunkSize;
                long end = (i == numThreads - 1) ? fileSize : (i + 1) * chunkSize - 1;

                // 将每个线程的下载任务放入线程池中执行
                ThreadPool.QueueUserWorkItem((state) =>
                {
                    DownloadChunk(threadId, start, end);
                });
            }
        }

        private void DownloadChunk(int id, long start, long end)
        {
            // 创建HttpClient实例
            using HttpClient client = new();
            // 发送Get请求，并设置HttpCompletionOption为ResponseHeadersRead
            using HttpResponseMessage response = client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead).Result;
            // 获取响应内容
            using Stream input = response.Content.ReadAsStreamAsync().Result;
            // 创建文件流，用于写入文件
            using FileStream output = new(outputPath + id.ToString(), FileMode.Create, FileAccess.Write);
            // 将输入流定位到start位置
            input.Seek(start, SeekOrigin.Begin);
            // 创建缓冲区
            byte[] buffer = new byte[4096];
            int bytesRead;
            long totalBytesRead = 0;
            // 当读取的字节数大于0且总字节数小于end-start+1时，循环读取
            while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0 && totalBytesRead < (end - start + 1))
            {
                // 将缓冲区的内容写入文件流
                output.Write(buffer, 0, bytesRead);
                // 更新总字节数
                totalBytesRead += bytesRead;
            }
        }
    }

    internal class SingleFileMultiThreadAsync(int threadCount = SingleFileMultiThreadAsync.DefaultThreadCount)
    {
        private readonly HttpClient _httpClient = new();
        private const int DefaultThreadCount = 16;

        public async Task DownloadFileAsync(string fileUrl, string outputPath, CancellationToken cancellationToken = default)
        {
            // Step 1: 获取文件大小
            // 发送一个HEAD请求到fileUrl，并设置HttpCompletionOption为ResponseHeadersRead，以及传入cancellationToken
            var response = await _httpClient.SendAsync(
                new HttpRequestMessage(HttpMethod.Head, fileUrl),
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            );
            // 确保请求成功
            response.EnsureSuccessStatusCode();

            // 获取文件大小
            long fileSize = response.Content.Headers.ContentLength ?? 0;

            // 如果文件大小为0，抛出异常
            if (fileSize == 0)
            {
                throw new InvalidOperationException("Could not get file size");
            }

            // Step 2: 创建目标文件并为每个线程分配一个范围
            using var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None);
            fileStream.SetLength(fileSize); // 预留文件大小

            var tasks = new List<Task>();
            long chunkSize = fileSize / threadCount;
            for (int i = 0; i < threadCount; i++)
            {
                // 计算每个线程的开始和结束字节
                long startByte = i * chunkSize;
                long endByte = (i == threadCount - 1) ? fileSize - 1 : (startByte + chunkSize - 1);

                // 为每个线程分配一个下载任务
                tasks.Add(DownloadChunkAsync(fileUrl, startByte, endByte, fileStream, cancellationToken));
            }

            // 等待所有任务完成
            await Task.WhenAll(tasks);
        }

        private async Task DownloadChunkAsync(string fileUrl, long start, long end, Stream outputStream, CancellationToken cancellationToken)
        {
            // 创建一个HttpRequestMessage对象，用于发送HTTP GET请求
            var request = new HttpRequestMessage(HttpMethod.Get, fileUrl);
            // 设置请求头中的Range属性，表示要下载的文件块的起始和结束位置
            request.Headers.Range = new System.Net.Http.Headers.RangeHeaderValue(start, end);

            // 发送请求，并设置响应头的读取选项为ResponseHeadersRead，以便在读取响应头时立即开始读取响应体
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            // 确保响应状态码为成功状态码
            response.EnsureSuccessStatusCode();

            // 获取响应体中的内容流
            var contentStream = await response.Content.ReadAsStreamAsync(cancellationToken);

            // 将输出流的位置设置为起始位置
            outputStream.Seek(start, SeekOrigin.Begin);

            // 将内容流复制到输出流中，每次复制的字节数为8192
            await contentStream.CopyToAsync(outputStream, 8192, cancellationToken);
        }
    }
}
