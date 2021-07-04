using System;

namespace AudioMod
{
    public class Assertions {
        //All Console Logs are here
        public void AudioManager_LRByteError(byte[] Left, byte[] Right) {
            Console.WriteLine($"AudioManager: Left and Right Channel byte[] length doesnt match \n -> LeftChannel: {Left.Length} | RightChannel: {Right.Length}\n");
        }
        public void AudioManager_ByteSampleError(int samples, byte[] Left, byte[] Right) {
            Console.WriteLine($"AudioManager: Channel Byte array doesnt match sample count \n -> Samples: {samples} | LeftChannel: {Left.Length} | RightChannel: {Right.Length}\n");
        }
        public void AudioManager_FileCreationSuccess(String path) {
            Console.WriteLine($"AudioManager: Successfully created file: {path}");
        }
        public void AudioManager_LoadSplitError(int Samples, byte[] AudioByteStream, int sampleRate, int bitsPerSample, int Channels, int HeaderLength) {
            Console.WriteLine($"AudioManager: File header is too long, clip isnt long enough for current settings or SampleRate isnt correct \n -> Samples: {Samples} | RawStream: {AudioByteStream.Length} | Headerless: {(AudioByteStream.Length - HeaderLength)} | SampleRate: {sampleRate} at {bitsPerSample} b/s | Audio Channels: {Channels} | Header Length: {HeaderLength} | Aprox Header: {(AudioByteStream.Length - Samples)}\n");
        }
        public void AudioManager_AudioSampleNull() {
            Console.WriteLine("AudioManager: The given Audio Sample is null.\n");
        }
        public void AudioManager_FileNotFound(String f) {
            Console.WriteLine($"AudioManager: File ref ({f}) Not found.\n");
        }
        public void Main_CoreExecutionHalt() {
            Console.WriteLine("Main: Core Stack Halt returned.\n");
        }
        public void General_FileCount(String[] Files, String Path) {
            Console.WriteLine($"{Files.Length} Files found in {Path}\n");
        }
        public void AudioManager_ConvertedSignature(bool Direction) {
            Console.WriteLine($"AudioManager: Converting {(Direction ? "Signed -> Unsigned" : "Unsigned -> Signed")}\n");
        }
    }
}
