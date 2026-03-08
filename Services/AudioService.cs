using NetCoreAudio;
using System;
using System.IO;
using Avalonia;
using Avalonia.Platform;
using HaveItMain;

public static class AudioService
{
    private static readonly Player _player = new Player();

    public static void PlaySfx(string fileName)
    {
        var state = App.ServiceState;
    
        if (state == null || !state.EnableSfx) return;

        try 
        {
            _player.SetVolume((byte)state.SfxVolume).Wait();
            var uri = new Uri($"avares://HaveItMain/Assets/{fileName}");
            using (var assetStream = Avalonia.Platform.AssetLoader.Open(uri))
            {
                string tempPath = Path.Combine(Path.GetTempPath(), fileName);
                using (var fileStream = File.Create(tempPath))
                {
                    assetStream.CopyTo(fileStream);
                }
                _player.Play(tempPath).Wait();
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"SFX Error: {ex.Message}");
        }
    }
}