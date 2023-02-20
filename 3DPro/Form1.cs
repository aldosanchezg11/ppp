namespace _3DPro
{
    public partial class Form1 : Form
    {
        Graphics g;
        Bitmap bmp;
        Figure f;

        public Form1()
        {
            InitializeComponent();
            bmp = new Bitmap(666, 359);
            pictureBox1.Image = bmp;
            g = Graphics.FromImage(bmp);
            f = new Figure();
        }
        double[,] matrix = Vertex.vertices;

    }
}