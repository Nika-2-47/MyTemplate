using System;
using System.Drawing;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        NotifyIcon trayIcon = new NotifyIcon()
        {
            Icon = SystemIcons.Information,
            Text = "常駐ツール",
            Visible = true
        };

        // Tooltipやメニュー
        trayIcon.BalloonTipTitle = "通知タイトル";
        trayIcon.BalloonTipText = "これはテスト通知です。";
        trayIcon.ShowBalloonTip(3000); // 3秒表示

        // コンテキストメニュー
        trayIcon.ContextMenuStrip = new ContextMenuStrip();
        trayIcon.ContextMenuStrip.Items.Add("終了", null, (s, e) => Application.Exit());

        ShowCenterMessage("これは中央の半透明メッセージです", 3000);

        Application.Run(); // メッセージループ開始
    }

    static void ShowCenterMessage(string message, int durationMs)
    {
        Form msgForm = new Form()
        {
            FormBorderStyle = FormBorderStyle.None,
            StartPosition = FormStartPosition.Manual,
            ShowInTaskbar = false,
            TopMost = true,
            BackColor = Color.Black,
            Opacity = 0.7,
            Size = new Size(400, 100)
        };

        // 画面中央に配置
        Rectangle wa = Screen.PrimaryScreen?.WorkingArea ?? Screen.AllScreens[0].WorkingArea;
        msgForm.Location = new Point(
            wa.Left + (wa.Width - msgForm.Width) / 2,
            wa.Top + (wa.Height - msgForm.Height) / 2
        );

        Label lbl = new Label()
        {
            Text = message,
            ForeColor = Color.White,
            BackColor = Color.Transparent,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font("Meiryo UI", 16, FontStyle.Bold)
        };
        msgForm.Controls.Add(lbl);

        Timer timer = new Timer();
        timer.Interval = durationMs;
        timer.Tick += (s, e) =>
        {
            timer.Stop();
            timer.Dispose();
            msgForm.Close();
            msgForm.Dispose();
        };

        msgForm.Shown += (s, e) => timer.Start();
        msgForm.Show();
    }
}