using System;
using System.IO;
using NAudio.Wave;

public class AudioRecorder : IDisposable
{
    private WasapiLoopbackCapture systemCapture;
    private WaveFileWriter writer;
    private string outputFilePath;
    private bool isRecording;
    public string Path { get => outputFilePath; }
    public AudioRecorder(string outputFile)
    {
        outputFilePath = outputFile;
    }

    public void StartRecording()
    {
        if (isRecording) return;

        // Create the capture for default playback device
        systemCapture = new WasapiLoopbackCapture();

        // Create the writer with the same format as the capture
        writer = new WaveFileWriter(outputFilePath, systemCapture.WaveFormat);

        // Event fires whenever audio data is available
        systemCapture.DataAvailable += (s, e) =>
        {
            writer.Write(e.Buffer, 0, e.BytesRecorded);
        };

        systemCapture.RecordingStopped += (s, e) =>
        {
            writer?.Dispose();
            writer = null;
            systemCapture.Dispose();
            systemCapture = null;
            isRecording = false;
        };

        systemCapture.StartRecording();
        isRecording = true;
    }

    public void StopRecording()
    {
        if (!isRecording) return;
        systemCapture.StopRecording();
    }

    public void Dispose()
    {
        if (isRecording)
        {
            StopRecording();
        }
        writer?.Dispose();
        systemCapture?.Dispose();
    }

    // 🔹 Future expansion point:
    // You could add a microphone capture here and mix it with system audio
    // using NAudio's MixingSampleProvider before writing to file.
}