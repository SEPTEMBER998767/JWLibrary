﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JWLibrary.FFmpeg.Test
{
    public partial class Form1 : Form
    {
        JWLibrary.FFmpeg.FFMpegCaptureAV _ffmpegCav
            = new FFmpeg.FFMpegCaptureAV();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_ffmpegCav.Initialize())
            {
                _ffmpegCav.Register();

                JWLibrary.FFmpeg.FFmpegCommandModel model = new FFmpeg.FFmpegCommandModel
                {
                    AudioQuality = JWLibrary.FFmpeg.FFmpegCommandParameterSupport.GetSupportAudioQuality()[0],
                    Format = "mp4",
                    FrameRate = JWLibrary.FFmpeg.FFmpegCommandParameterSupport.GetSupportFrameRate()[0],
                    Height = "1440",
                    Width = "2560",
                    OffsetX = "0",
                    OffsetY = "0",
                    Preset = JWLibrary.FFmpeg.FFmpegCommandParameterSupport.GetSupportPreset()[0],
                    FullFileName = @"E:\test.mp4"
                };
                var command = JWLibrary.FFmpeg.BuildCommand.BuildRecordingCommand(FFmpeg.RecordingTypes.Local, model);
                _ffmpegCav.FFmpegCommandExcute(command);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _ffmpegCav.FFmpegCommandStop();
            _ffmpegCav.UnRegister();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (_ffmpegCav != null)
            {
                _ffmpegCav.Dispose();
            }

            base.OnClosing(e);
        }
    }
}