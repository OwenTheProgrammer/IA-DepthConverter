using System;
using System.IO;
using System.Collections.Generic;

namespace AudioMod
{
    class Program
    {
        static Func<float, float, float, int> Terrace = (x, max, step) => (int)Math.Round((Math.Floor((x / max) * (step + 1)) / step) * max);
        public static AudioManager.AudioSample TerraceAudio(AudioManager mng, AudioManager.AudioSample sample, int ResolutionStep, bool IsDepth, bool Inverted) {
            ResolutionStep = IsDepth ? (int)Math.Pow(2, ResolutionStep) : ResolutionStep + 1;
            byte[] Left = sample.ChannelL;
            byte[] Right = sample.ChannelR;
            for(int i = 0; i < (mng.Samples / mng.Channels); i++) {
                byte inv = (byte)(Convert.ToInt32(Inverted) * 255);
                Left[i] = (byte)(inv - Terrace(Left[i], 255, ResolutionStep / 2));
                Right[i] = (byte)(inv - Terrace(Right[i], 255, ResolutionStep / 2));
            } return new AudioManager.AudioSample(Left, Right);
        }

        static void Main(string[] args) {
            //So I dont have to see huge Console.WriteLine functions
            Assertions Cnsl = new Assertions();

            //(samples, samples/second, channels, bits/sample, header length, Console)
            AudioManager audio = new AudioManager(226380, 44100, 2, 16, 44, Cnsl);

            //Loads the audio (path, SignedData)
            AudioManager.AudioSample InputAudio = audio.LoadAudio("Data/AudioParts/SnailsHouse-Morph", true);

            //Audio manager data, InputAudio, steps of resolution from top to bottom, is total steps: false, is memory alloc: true
            audio.GenerateAudioFile("Data/Output/SnailsHouse-Morph_Output", TerraceAudio(audio, InputAudio, 8, false, false), false);
        }
    }
}
