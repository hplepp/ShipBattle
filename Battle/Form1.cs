namespace Battle
{
    public partial class FormMain : Form
    {
        int dx = -5;

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            InitGame();
        }

        void InitGame()
        {
            pictureBoxCanon.Location = new Point((ClientSize.Width - pictureBoxCanon.Width) / 2,
                                                 ClientSize.Height - pictureBoxCanon.Height);
            pictureBoxCanonBall.Visible = false;
            pictureBoxBoom.Visible = false;
            pictureBoxShip.Location = new Point(732, 2);
            pictureBoxShip.Visible = true;
            timerCanonBall.Enabled = false;
            timerShip.Enabled = true;
        }

        private void timerShip_Tick(object sender, EventArgs e)
        {
            pictureBoxShip.Location = new Point(pictureBoxShip.Location.X + dx, pictureBoxShip.Location.Y);
            if (pictureBoxShip.Location.X < 0 ||
                    pictureBoxShip.Location.X > Width - pictureBoxShip.Width - 10) dx = -dx;
        }

        private void FormMain_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Close();

            if (e.KeyValue == ' ' && !timerCanonBall.Enabled)
            {
                pictureBoxCanonBall.Location = new Point(pictureBoxCanon.Left + 37, pictureBoxCanon.Top - 20);
                pictureBoxCanonBall.Visible = true;
                timerCanonBall.Enabled = true;
            }
        }

        private void timerCanonBall_Tick(object sender, EventArgs e)
        {
            pictureBoxCanonBall.Location = new Point(pictureBoxCanonBall.Location.X, 
                                                        pictureBoxCanonBall.Location.Y - 5);

            Point center = new Point(pictureBoxCanonBall.Left + pictureBoxCanonBall.Width / 2,
                                     pictureBoxCanonBall.Top + pictureBoxCanonBall.Height / 2);

            if (pictureBoxShip.Bounds.Contains(center))
            {
                pictureBoxBoom.Location = pictureBoxShip.Location;
                pictureBoxShip.Visible = false;
                pictureBoxCanonBall.Visible = false;
                pictureBoxBoom.Visible = true; 
                timerCanonBall.Enabled = false;
                timerShip.Enabled = false;

                if (MessageBox.Show("Хотите еще сыграть?", "Цель поражена", MessageBoxButtons.YesNo) ==
                      DialogResult.Yes) InitGame();
                else Close();
            }
            if (pictureBoxCanonBall.Location.Y < - pictureBoxCanonBall.Height) 
            {
                pictureBoxCanonBall.Visible = false;
                timerCanonBall.Enabled = false;
            }
        }
    }
}
