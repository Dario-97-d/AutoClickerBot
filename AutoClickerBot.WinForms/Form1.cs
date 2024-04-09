using System.Runtime.InteropServices;

namespace AutoClickerBot.WinForms
{
    public partial class Form1 : Form
    {
        bool _running = false;
        int _clicksLeft = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_running)
            {
                timer1.Stop();
                button1.Text = "Start";
            }
            else
            {
                _clicksLeft = (int)numericUpDown2.Value;
                timer1.Interval = (int)numericUpDown1.Value;
                timer1.Start();
                button1.Text = "Stop";
            }

            _running = !_running;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _clicksLeft--;
            numericUpDown2.Value = _clicksLeft;
            if (_clicksLeft == 0)
            {
                timer1.Stop();
                button1.Text = "Start";
                return;
            }
            var p = new Point(MousePosition.X, MousePosition.Y);
            // Mouse Left Button Down: dwflag = 2
            mouse_event(2, p.X, p.Y, 0, 0);
            // Mouse Left Button Up: dwflag = 4
            mouse_event(4, p.X, p.Y, 0, 0);
        }

        [DllImport("user32.dll")]
        static extern void mouse_event(int dwflags, int dx, int dy, int dwdata, int dwextrainfo);
    }
}