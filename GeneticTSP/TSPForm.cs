using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneticTSP
{
    public partial class TSPForm : Form
    {
        public static int NUM_CITIES = 100;
        public static int POP_SIZE = 150;

        private TSPDrawer m_drawer;
        private TSPMap m_map;
        private TSPGenAlg m_gen_alg;
        private bool m_running;

        delegate void SetTextCallback(string text);

        public TSPForm()
        {
            InitializeComponent();
            Controls.Add(m_DrawSurface);
            Enter += SolveTSP;
            KeyDown += KeyPressed;
            FormClosing += OnFormClosing;
            m_gen_box.KeyDown += KeyPressed;
            m_map = new TSPMap(m_DrawSurface.Size.Height, m_DrawSurface.Size.Width, NUM_CITIES);
            m_gen_alg = new TSPGenAlg(NUM_CITIES, POP_SIZE, m_map, new BoltzmannFitnessScaler(300));
            m_running = true;
            m_DrawSurface.Focus();
        }
 
        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            m_running = false;
        }

        private void DrawPath()
        {
            var draw_surface_size = m_DrawSurface.Size;
            Bitmap flag = new Bitmap(draw_surface_size.Width, draw_surface_size.Height);
            Graphics flag_graphics = Graphics.FromImage(flag);
            flag_graphics.Clear(Color.White);
            m_drawer = new TSPDrawer(flag_graphics, draw_surface_size, 10);
            m_drawer.DrawCities(m_map.Cities.Values.ToList(), m_gen_alg.FittestGenome.Data);
            m_DrawSurface.Image = flag;
        }

        private void setGenerationText(string text)
        {
            if(m_gen_box.InvokeRequired)
            {
                SetTextCallback cb = new SetTextCallback(setGenerationText);
                if (Disposing || IsDisposed) return;
                try
                {
                    BeginInvoke(cb, new object[] { text });
                }
                catch
                {

                }
            }
            else
            {
                m_gen_box.Text = text;
            }
        }

        private async void SolveTSP(object sender, EventArgs e)
        {
            Console.WriteLine("Trying to solve again.");
            m_gen_alg.Done = true;
            m_gen_alg = new TSPGenAlg(NUM_CITIES, POP_SIZE, m_map, null);
            await Task.Run(() =>
            {
                Console.WriteLine($"GenAlg done {m_gen_alg.Done}");
                while (!m_gen_alg.Done && m_running)
                {
                    //                    Console.WriteLine($"Generation {m_gen_alg.GenerationNumber}");
                    setGenerationText(m_gen_alg.GenerationNumber.ToString());
                    var start = DateTime.Now.Millisecond;
                    m_gen_alg.Epoch();
                    var end = DateTime.Now.Millisecond;
//                    Console.WriteLine($"Took {end - start} ms");
//                    Thread.Sleep(100);
                    DrawPath();
                }
            });
        }

        private void KeyPressed(object sender, KeyEventArgs kea)
        {
            switch(kea.KeyCode)
            {
                case Keys.Enter:
                    OnEnter(kea);
                    break;
                case Keys.Space:
                    Console.WriteLine("You pressed optional key space");
                    break;
            }
        }

        private void CheckSelectedItem(ToolStripMenuItem selected_item)
        {
            foreach(ToolStripMenuItem item in selected_item.GetCurrentParent().Items)
            {
                item.Checked = false;
            }
            selected_item.Checked = true;
        }

        private void permutationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckSelectedItem(sender as ToolStripMenuItem);
            m_gen_alg.SetCrossoverType(CrossoverType.PartiallyMatched);
        }

        private void orderBasedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckSelectedItem(sender as ToolStripMenuItem);
            m_gen_alg.SetCrossoverType(CrossoverType.OrderBased);
        }

        private void positionBasedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckSelectedItem(sender as ToolStripMenuItem);
            m_gen_alg.SetCrossoverType(CrossoverType.PositionBased);
        }

        private void exchangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckSelectedItem(sender as ToolStripMenuItem);
            m_gen_alg.SetMutationType(MutationType.Exchange);
        }

        private void scrambleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckSelectedItem(sender as ToolStripMenuItem);
            m_gen_alg.SetMutationType(MutationType.Scramble);
        }

        private void insertionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckSelectedItem(sender as ToolStripMenuItem);
            m_gen_alg.SetMutationType(MutationType.Insertion);
        }

        private void displacementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckSelectedItem(sender as ToolStripMenuItem);
            m_gen_alg.SetMutationType(MutationType.Displacement);
        }

        private void inversionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckSelectedItem(sender as ToolStripMenuItem);
            m_gen_alg.SetMutationType(MutationType.Inversion);
        }

        private void displacedInversionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckSelectedItem(sender as ToolStripMenuItem);
            m_gen_alg.SetMutationType(MutationType.DisplacedInversion);
        }

        private void noneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckSelectedItem(sender as ToolStripMenuItem);
            m_gen_alg.SetScalingType(null);
        }

        private void rankToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckSelectedItem(sender as ToolStripMenuItem);
            m_gen_alg.SetScalingType(new RankFitnessScaler());
        }

        private void sigmaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckSelectedItem(sender as ToolStripMenuItem);
            m_gen_alg.SetScalingType(new SigmaFitnessScaler());
        }

        private void boltzmannToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckSelectedItem(sender as ToolStripMenuItem);
            m_gen_alg.SetScalingType(new BoltzmannFitnessScaler(300));
        }

        private void roulletteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckSelectedItem(sender as ToolStripMenuItem);
            m_gen_alg.SetSelectionStrategy(new RouletteWheelSelection());
        }

        private void tournamentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckSelectedItem(sender as ToolStripMenuItem);
            m_gen_alg.SetSelectionStrategy(new TournamentSelection());
        }

        private void SUSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckSelectedItem(sender as ToolStripMenuItem);
            m_gen_alg.SetSelectionStrategy(new SUSSelection());
        }

        private void elitismOnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckSelectedItem(sender as ToolStripMenuItem);
            m_gen_alg.Elitism = true;
        }

        private void elitismOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckSelectedItem(sender as ToolStripMenuItem);
            m_gen_alg.Elitism = false;
        }
    }

    public class TSPDrawer
    {
        private readonly Graphics m_graphics;
        private readonly int m_x_center;
        private readonly int m_y_center;
        private readonly int m_radius;
        private readonly int m_city_radius;

        public TSPDrawer(Graphics graphics, Size graphics_size, int city_diameter)
        {
            m_graphics = graphics;
            m_x_center = graphics_size.Width / 2;
            m_y_center = graphics_size.Height / 2;
            m_radius = m_y_center - city_diameter;
            m_city_radius = city_diameter / 2;
        }

        private void DrawCity(City city)
        {
            m_graphics.DrawEllipse(Pens.Black, city.X - m_city_radius, city.Y - m_city_radius, m_city_radius * 2, m_city_radius * 2);

            /*
            using (Font draw_font = new Font("Arial", 8))
            using (SolidBrush draw_brush = new SolidBrush(Color.Blue))
            {
                m_graphics.DrawString(city.ID.ToString(), draw_font, draw_brush, city.X, city.Y);
            }
            */
        }

        public void DrawCities(IList<City> cities, IList<int> indices)
        {
            City from = cities[indices.First()];
            DrawCity(from);
            foreach(int idx in indices.Skip(1))
            {
                City city = cities[idx];
                DrawCity(city);
                m_graphics.DrawLine(Pens.Red, from.X, from.Y, city.X, city.Y);
                from = city;
            }
        }
    }
}