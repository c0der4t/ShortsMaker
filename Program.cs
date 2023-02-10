using System.Diagnostics;

Console.WriteLine("Path to Video");
string filePath = Console.ReadLine();

Console.WriteLine("How many random clips to generate?");
int amountOfClips = Convert.ToInt32(Console.ReadLine());

Console.WriteLine("If you'd like to append a video to the end, give me it's path:"); 
string outroPath;
try
{
    outroPath = Console.ReadLine();
}
finally
{
    
}

string outputDirectory = Path.GetDirectoryName(filePath);
string outputPrefix = Path.GetFileNameWithoutExtension(filePath);
string outputExtension = Path.GetExtension(filePath);
int interval = 50;

var rand = new Random();
var startTimes = Enumerable.Range(0, (int)Math.Ceiling(GetVideoDuration(filePath) / interval))
                           .OrderBy(x => rand.Next())
                           .Select(x => x * interval)
                           .ToList();
startTimes.Add(int.MaxValue);

int startIndex = 0;

for (int i = 0; i < startTimes.Count - 1; i++)
{
    if (i < amountOfClips)
    {
        string outputFile = Path.Combine(outputDirectory, $"{startTimes[i] / 60}_{i}{outputExtension}");
        SplitVideo(filePath, outputFile, startTimes[i], interval, outroPath);
    }
}


static double GetVideoDuration(string filePath)
{
    var process = new Process
    {
        StartInfo = new ProcessStartInfo
        {
            FileName = "ffprobe",
            Arguments = $"-v error -show_entries format=duration -of default=noprint_wrappers=1:nokey=1 \"{filePath}\"",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true,
        }
    };
    process.Start();
    string output = process.StandardOutput.ReadToEnd();
    process.WaitForExit();
    return double.Parse(output);
}

static void SplitVideo(string filePath, string outputFile, int startTime, int duration, string outroPath = "")
{
    double timeOffset = 0.5;

    // Create a process to run ffmpeg
    var process = new Process
    {
        StartInfo = new ProcessStartInfo
        {
            FileName = "ffmpeg",
            Arguments = $"-i \"{filePath}\" -ss {startTime} -t {duration} -async 1 -c copy \"{outputFile}\"",
            UseShellExecute = false,
            CreateNoWindow = true,
        }
    };

    // Start the ffmpeg process and wait for it to finish
    process.Start();
    process.WaitForExit();

    Console.WriteLine($"Created clip - {outputFile}");

    // If the outroPath is not empty, concatenate the outro to the split video
    if (!string.IsNullOrEmpty(outroPath))
    {
        // Generate the concat file list
        string concatList = $"file '{outputFile}'\nfile '{outroPath}'";
        string concatListPath = Path.Combine(Path.GetDirectoryName(outputFile), "concat-list.txt");

        // Write the concat file list to a file
        File.WriteAllText(concatListPath, concatList);

        // Generate the concatenated output file path
        string concatenatedOutputPath = Path.Combine(Path.GetDirectoryName(outputFile), Path.GetFileNameWithoutExtension(outputFile) + "-with-outro.mp4");

        // Create a process to run ffmpeg to concatenate the split video and outro
        var concatProcess = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "ffmpeg",
                Arguments = $"-f concat -safe 0 -i \"{concatListPath}\" -async 1 -c copy \"{concatenatedOutputPath}\"",
                UseShellExecute = false,
                CreateNoWindow = true,
            }
        };

        // Start the ffmpeg concat process and wait for it to finish
        concatProcess.Start();
        concatProcess.WaitForExit();
    }
}
