using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Calculator
{
    public class MainForm : Form
    {
    private TextBox display = null!;
    private TableLayoutPanel panel = null!;
        private double accumulator = 0;
        private string pendingOp = null!;
    private bool entering = false;

    public MainForm()
        {
            InitializeComponent();
        }

    private void InitializeComponent()
        {
            this.Text = "Simple Calculator";
            this.ClientSize = new Size(320, 480);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

        display = new TextBox();
            display.ReadOnly = true;
            display.TextAlign = HorizontalAlignment.Right;
            display.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point);
            display.Dock = DockStyle.Top;
            display.Height = 70;
            display.Text = "0";

        panel = new TableLayoutPanel();
            panel.RowCount = 5;
            panel.ColumnCount = 4;
            panel.Dock = DockStyle.Fill;
            panel.Padding = new Padding(6);
            panel.BackColor = SystemColors.Control;

        for (int i = 0; i < 4; i++) panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            for (int i = 0; i < 5; i++) panel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));

        string[,] layout = new string[5,4]
            {
                {"C","±","%","/"},
                {"7","8","9","*"},
                {"4","5","6","-"},
                {"1","2","3","+"},
                {"0",".","=",""}
            };

        for (int r = 0; r < 5; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    string text = layout[r,c];
                    if (r == 4 && c == 2) continue; // we'll place = at c=2 later to keep layout simple
                    if (text == "") continue;

            Button btn = new Button();
                    btn.Text = text;
                    btn.Dock = DockStyle.Fill;
                    btn.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
                    btn.Click += Button_Click;

            if (r == 4 && c == 0)
                    {
                        // 0 spans two columns
                        panel.Controls.Add(btn, 0, 4);
                        panel.SetColumnSpan(btn, 2);
                        continue;
                    }

                    panel.Controls.Add(btn, c, r);
                }
            }

            // place '=' button at last column (row 4, col 3)
            Button eq = new Button();
            eq.Text = "=";
            eq.Dock = DockStyle.Fill;
            eq.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            eq.Click += Button_Click;
            panel.Controls.Add(eq, 3, 4);

            this.Controls.Add(panel);
            this.Controls.Add(display);
        }

        private void Button_Click(object? sender, EventArgs e)
        {
            if (sender is not Button btn) return;
            string t = btn.Text;

            if (double.TryParse(t, NumberStyles.Number, CultureInfo.InvariantCulture, out double d) || t == ".")
            {
                OnNumber(t);
                return;
            }

            switch (t)
            {
                case "C": Clear(); break;
                case "±": Negate(); break;
                case "%": Percent(); break;
                case "/": Oper("/"); break;
                case "*": Oper("*"); break;
                case "-": Oper("-"); break;
                case "+": Oper("+"); break;
                case "=": EqualsOp(); break;
            }
        }

        private void OnNumber(string t)
        {
            if (t == ".")
            {
                if (entering && display.Text.Contains(".")) return;
                if (!entering) { display.Text = "0."; entering = true; return; }
                display.Text += ".";
                return;
            }

            if (!entering || display.Text == "0")
            {
                display.Text = t;
                entering = true;
            }
            else
            {
                display.Text += t;
            }
        }

        private void Clear()
        {
            display.Text = "0";
            entering = false;
            accumulator = 0;
            pendingOp = null!;
        }

        private void Negate()
        {
            if (double.TryParse(display.Text, out double v))
            {
                v = -v;
                display.Text = v.ToString(CultureInfo.InvariantCulture);
            }
        }

        private void Percent()
        {
            if (double.TryParse(display.Text, out double v))
            {
                v = v / 100.0;
                display.Text = v.ToString(CultureInfo.InvariantCulture);
            }
        }

        private void Oper(string op)
        {
            if (double.TryParse(display.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out double v))
            {
                if (pendingOp == null)
                {
                    accumulator = v;
                }
                else
                {
                    accumulator = Compute(accumulator, v, pendingOp);
                    display.Text = accumulator.ToString(CultureInfo.InvariantCulture);
                }
            }
            pendingOp = op;
            entering = false;
        }

        private void EqualsOp()
        {
            if (pendingOp == null) return;
            if (!double.TryParse(display.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out double v)) return;
            double result = Compute(accumulator, v, pendingOp);
            display.Text = double.IsNaN(result) || double.IsInfinity(result) ? "Error" : result.ToString(CultureInfo.InvariantCulture);
            pendingOp = null!;
            entering = false;
            accumulator = 0;
        }

        private double Compute(double a, double b, string op)
        {
            try
            {
                return op switch
                {
                    "+" => a + b,
                    "-" => a - b,
                    "*" => a * b,
                    "/" => b == 0 ? double.NaN : a / b,
                    _ => b,
                };
            }
            catch
            {
                return double.NaN;
            }
        }
    }
}
