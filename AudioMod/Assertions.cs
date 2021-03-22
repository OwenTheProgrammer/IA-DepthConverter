using System;

namespace AudioMod
{
    public class Assertions
    {
        //All Console Logs are here
        public void AudioManager_LRByteError(byte[] Left, byte[] Right)
        {
            Console.WriteLine("AudioManager: Left and Right Channel byte[] length doesnt match \n -> LeftChannel: {0} | RightChannel: {1}\n", Left.Length, Right.Length);
        }
        public void AudioManager_ByteSampleError(int samples, byte[] Left, byte[] Right)
        {
            Console.WriteLine("AudioManager: Channel Byte array doesnt match sample count \n -> Samples: {0} | LeftChannel: {1} | RightChannel: {2}\n", samples, Left.Length, Right.Length);
        }
        public void AudioManager_FileCreationSuccess(String path)
        {
            Console.WriteLine("AudioManager: Successfully created file: {0}", path);
        }
        public void AudioManager_LoadSplitError(int Samples, byte[] AudioByteStream, int sampleRate, int bitsPerSample, int Channels, int HeaderLength)
        {
            Console.WriteLine("AudioManager: File header is too long, clip isnt long enough for current settings or SampleRate isnt correct \n ->" +
                "Samples: {0} | RawStream: {1} | Headerless: {2} | SampleRate: {3} at {4} b/s | Audio Channels: {5} | Header Length: {6} | Aprox Header: {7}\n",
                Samples, AudioByteStream.Length, (AudioByteStream.Length - HeaderLength), sampleRate, bitsPerSample, Channels, HeaderLength, (AudioByteStream.Length - Samples));
        }
        public void AudioManager_AudioSampleNull()
        {
            Console.WriteLine("AudioManager: The given Audio Sample is null.\n");
        }
        public void AudioManager_FileNotFound(String f)
        {
            Console.WriteLine("AudioManager: File ref ({0}) Not found.\n", f);
        }
        public void Main_CoreExecutionHalt()
        {
            Console.WriteLine("Main: Core Stack Halt returned.\n");
        }
        public void General_FileCount(String[] Files, String Path)
        {
            Console.WriteLine("{0} Files found in {1}\n", Files.Length, Path);
        }
        public void AudioManager_ConvertedSignature(bool Direction)
        {
            Console.WriteLine("AudioManager: Converting {0}\n", Direction ? "Signed -> Unsigned" : "Unsigned -> Signed");
        }
    }
}
