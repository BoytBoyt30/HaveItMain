using System;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using LibVLCSharp.Shared;

namespace HaveItMain.Views;

public partial class Video : Window
{
    private MediaPlayer mediaPlayer;
    private LibVLC libVLC;

    private Button playButton;
    private Button pauseButton;
    private Button stopButton;
    private Button openButton;

    public Video()
    {
        InitializeComponent();
        Setup();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void Setup()
    {
        Core.Initialize();
        libVLC = new LibVLC();
        mediaPlayer = new MediaPlayer(libVLC);
        
        playButton = this.FindControl<Button>("PlayButton");
        pauseButton = this.FindControl<Button>("PauseButton");
        stopButton = this.FindControl<Button>("StopButton");
        openButton = this.FindControl<Button>("OpenButton");
        
        playButton.Click += (s, e) => mediaPlayer?.Play();
        pauseButton.Click += (s, e) => mediaPlayer?.Pause();
        stopButton.Click += (s, e) => mediaPlayer?.Stop();

        openButton.Click += async (s, e) => await OpenFileDialog();
        var videoView = this.FindControl<Canvas>("VideoCanvas");
        mediaPlayer.EndReached += (sender, args) =>
        {
            videoView.Background = Avalonia.Media.Brush.Parse("#FFFFFF");
        };
    }

    private async Task<Task> OpenFileDialog()
    {
        var topLevel = TopLevel.GetTopLevel(this);
        if (topLevel?.StorageProvider is { } storageProvider)
        {
            var files = await storageProvider.OpenFilePickerAsync(new Avalonia.Platform.Storage.FilePickerOpenOptions()
            {
                Title = "Open Video File",
                AllowMultiple = false,
                FileTypeFilter = new[]
                {
                    new Avalonia.Platform.Storage.FilePickerFileType("Video Files")
                    {
                        Patterns = new[] {"*.mp4", "*.avi"}
                    }
                }
            });

            if (files.Count > 0)
            {
                string filepath = files[0].Path.LocalPath;
                PlayVideo(filepath);
            }
        }
        return Task.CompletedTask;
    }

    private void PlayVideo(string filePath)
    {
        if (!string.IsNullOrEmpty(filePath))
        {
            mediaPlayer.Stop();
            var media = new Media(libVLC, new System.Uri(filePath));
            mediaPlayer.Media = media;
            mediaPlayer.Play();
        }
    }
}