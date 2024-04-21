using Audio.Convert;

string flacFilePath = "input.flac";
string wavFilePath = "output.wav";

Flac.Flac2Wav.FlacConverter.ConvertFlacToWav(wavFilePath, flacFilePath);