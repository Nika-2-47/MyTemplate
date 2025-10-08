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
            Text = "�풓�c�[��",
            Visible = true
        };

        // Tooltip�⃁�j���[
        trayIcon.BalloonTipTitle = "�ʒm�^�C�g��";
        trayIcon.BalloonTipText = "����̓e�X�g�ʒm�ł��B";
        trayIcon.ShowBalloonTip(3000); // 3�b�\��

        // �R���e�L�X�g���j���[
        trayIcon.ContextMenuStrip = new ContextMenuStrip();
        trayIcon.ContextMenuStrip.Items.Add("�I��", null, (s, e) => Application.Exit());

        ShowCenterMessage("����͒����̔��������b�Z�[�W�ł�", 3000);

        Application.Run(); // ���b�Z�[�W���[�v�J�n
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

        // ��ʒ����ɔz�u
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