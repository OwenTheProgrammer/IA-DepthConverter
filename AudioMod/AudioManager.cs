using System;
using System.IO;
using System.Text;

namespace AudioMod
{
    public class AudioManager {
        static Func<string, byte[]> getBytes = x => Encoding.ASCII.GetBytes(x);
        public struct AudioSample {
            public byte[] ChannelL;
            public byte[] ChannelR;
            public AudioSample(byte[] L, byte[] R) {
                ChannelL = L; 
                ChannelR = R; 
            }
        }

        private Assertions Cnsl;
        private byte SectorSize = 32;
        private int subChunkSize = 16;
        private short audioFormat = 1;
        private short blockAlign;
        private int byteRate;
        private int quadChunkSize;
        private int chunkSize;

        public int headerLength;
        public short bitsPerSample;
        public short Channels;
        public int sampleRate;
        public int Samples;
        
        //ISO standard .WAV format header generation
        public AudioManager(int _samples, int _SampleRate, short _Channels, short SampleAlloc, int headerLen, Assertions Console) {
            Cnsl = Console;
            bitsPerSample = SampleAlloc;
            Channels = _Channels;
            sampleRate = _SampleRate;
            Samples = _samples * (bitsPerSample / 8) * Channels;
            blockAlign = (short)(Channels * (bitsPerSample / 8));
            byteRate = sampleRate * blockAlign;
            quadChunkSize = Samples * Channels * (bitsPerSample / 8);
            chunkSize = 4 + (8 + subChunkSize) + (8 + quadChunkSize);
            headerLength = headerLen;
        }
        public void GenerateAudioFile(String FileName, AudioSample Sample, bool IsSigned) {
            //Error Checking
            #region Error checking
            if(Sample.ChannelL == null || Sample.ChannelR == null)
                throw new ArgumentException("The given Audio Sample is null", nameof(Sample));
            if(Sample.ChannelL.Length != Sample.ChannelR.Length)
                throw new ArgumentException($"{Sample.ChannelL} has different length than {Sample.ChannelR}");
            if(Sample.ChannelL.Length != Samples / Channels || Sample.ChannelR.Length != Samples / Channels) {
                throw new ArgumentException($"Channel array differed sample count\n{Samples}, {Sample.ChannelL.Length}, {Sample.ChannelR.Length}");
            #endregion

            // Write/Overwrite file
            File.Delete(FileName);
            FileStream f = new FileStream(FileName, FileMode.Create);
            BinaryWriter wr = new BinaryWriter(f);

            //Write all Header Hex for .WAV
            wr.Write(getBytes("RIFF"));
            wr.Write(chunkSize);
            wr.Write(getBytes("WAVE"));
            wr.Write(getBytes("fmt"));
            wr.Write(SectorSize);
            wr.Write(subChunkSize);
            wr.Write(audioFormat);
            wr.Write(Channels);
            wr.Write(sampleRate);
            wr.Write(byteRate);
            wr.Write(blockAlign);
            wr.Write(bitsPerSample);
            wr.Write(getBytes("data"));
            wr.Write(quadChunkSize);

            //write bytes/sample point
            byte[] rbs = new byte[4];
            wr.Write(rbs);
            // Convert to complement signature if signed data is enabled
            if(IsSigned) {
                Cnsl.AudioManager_ConvertedSignature(false);
                Sample.ChannelL = UnsignedToSigned(Sample.ChannelL, bitsPerSample);
                Sample.ChannelR = UnsignedToSigned(Sample.ChannelR, bitsPerSample);
            }

            //Write the channel data
            for(int i = 0; i < Samples / Channels; i++) {
                wr.Write(Sample.ChannelL[i]);
                wr.Write(Sample.ChannelR[i]);
            }
            wr.Close();
            wr.Dispose();
            Cnsl.AudioManager_FileCreationSuccess(f.Name);
        }

        public static byte[] SignedToUnsigned(byte[] x, int bps) {
            byte[] output = new byte[x.Length];
            byte cb = (byte)((1 << bps) - 1);
            for(int i = 0; i < output.Length; i++)
                output[i] = (byte)(~x[i] & ~cb);
            return output;
        }

        public static byte[] UnsignedToSigned(byte[] x, int bps) {
            byte[] output = new byte[x.Length];
            byte cb = (byte)((1 << bps) - 1);
            for(int i = 0; i < output.Length; i++)
                output[i] = (byte)(x[i] ^ cb);
            return output;
        }

        public AudioSample LoadAudio(String FileName, bool IsSigned) {
            if(!File.Exists(FileName))
                throw new ArgumentException($"'{FileName}' Not found.");
            byte[] AudioByteStream = File.ReadAllBytes(FileName);
            if((AudioByteStream.Length - headerLength) != Samples) {
                Cnsl.AudioManager_LoadSplitError(Samples,AudioByteStream,sampleRate,bitsPerSample,Channels,headerLength);
                throw new ArgumentException($"Sample couldnt be loaded");
            }
            //Store each channels data
            byte[] LeftChannel = new byte[Samples / Channels];
            byte[] RightChannel = new byte[Samples / Channels];
            for(int i = headerLength, l = 0, r = 0; i < AudioByteStream.Length; i++) {
                // i % 2 == 0
                if((~i & 1) == 1) { LeftChannel[l] = AudioByteStream[i]; l++; }
                else { RightChannel[r] = AudioByteStream[i]; r++; }
            }
            //Add signature to output if enabled
            if(IsSigned) {
                Cnsl.AudioManager_ConvertedSignature(true);
                LeftChannel = SignedToUnsigned(LeftChannel, bitsPerSample);
                RightChannel = SignedToUnsigned(RightChannel, bitsPerSample);
            }
            return new AudioSample(LeftChannel, RightChannel);
        }
    }
}
