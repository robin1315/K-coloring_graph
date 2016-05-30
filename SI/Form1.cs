using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SI.rsat;
using System.Drawing.Drawing2D;


namespace SI
{


    public partial class Form1 : Form
    {
        
        bool shulddraw = false;
        int x = 0, y = 0;
        int ID = 2;
        bool firstclick = true;
        Vertex clickedVertex;
        int IDtoremove = 0;
        Generate_cnf cnf;
        Dictionary<int, int> Result;


        // Create font, brush, pen
        Font drawFont = new Font("Arial", 16);
        SolidBrush drawBrush = new SolidBrush(Color.Black);
        Pen p1 = new Pen(Color.Navy, 5);
        Graphics g;
        
        private List<Vertex> myVertexList = new List<Vertex>();
        private ArrayList myPtsLine = new ArrayList();

        public Form1()
        {
            InitializeComponent();
        }


        public void MouseDownDraw(object sender, MouseEventArgs e) {
            shulddraw = true;
            if (radioButton2.Checked)
            {
            //    x = e.X;
              //  y = e.Y;
            }
        }
        public void MouseMoveDraw(object sender, MouseEventArgs e){
            
            if (radioButton2.Checked && shulddraw)
            {
                /*
                g = pictureBox1.CreateGraphics();
                g.Clear(Color.WhiteSmoke);
                pictureBox1.Invalidate();
                //foreach (Vertex p in myVertexList)
                  //  g.DrawEllipse(new Pen(Color.Navy), p.X - 25, p.Y - 25, 50, 50);        
                g.DrawLine(p1, x, y, e.X, e.Y);
                myPtsLine.Add(new Point(x, y));
                */
              
            }
            label1.Text = e.X.ToString();
            label2.Text = e.Y.ToString();
        }
        public void MouseUpDraw(object sender, MouseEventArgs e) {
            
            if (radioButton2.Checked) {
                //myPtsLine.Add(new Point(e.X, e.Y));
            }
            shulddraw = false;
            //pictureBox1.Invalidate();
        }
        public void MouseClickDraw(object sender, MouseEventArgs e) {


            if (shulddraw && e.Button == MouseButtons.Left)
            {
                g = Graphics.FromHwnd(this.Handle);
                if (radioButton1.Checked)
                {
                    x = e.X;
                    y = e.Y;
                    
                    myVertexList.Add(new Vertex(ID, new Point(e.X, e.Y)));
                    ID++;
                    x=0;
                    y=0;
                }
                if (radioButton2.Checked)
                {
                    x = e.X;
                    y = e.Y;

                    if (!firstclick)
                    {
                        foreach (Vertex p in myVertexList)
                            if ((x - 20 <= p.centerXY.X && p.centerXY.X <= x + 20) && (y - 20 <= p.centerXY.Y && p.centerXY.Y <= y + 20))
                            {
                                //add line to vertex
                                if (clickedVertex.ID != p.ID)
                                {
                                    p.edge.Add(clickedVertex.ID);
                                    clickedVertex.edge.Add(p.ID);
                                }
                                break;
                            }
                        firstclick = true;
                        clickedVertex = null;
                        x = 0;
                        y = 0;   
                    }
                    else {

                        foreach (Vertex p in myVertexList)
                            if ((x - 20 <= p.centerXY.X && p.centerXY.X <= x + 20) && (y - 20 <= p.centerXY.Y && p.centerXY.Y <= y + 20))
                            {
                                clickedVertex = p;
                                firstclick = false;
                                break;
                            }
                        x = 0;
                        y = 0;
                    }
                }
                pictureBox1.Invalidate();
            }
            if (e.Button == MouseButtons.Right)
            {
                if (radioButton1.Checked)
                {
                    x = e.X;
                    y = e.Y;

                    foreach (Vertex p in myVertexList)
                        if ((x - 20 <= p.centerXY.X && p.centerXY.X <= x + 20) && (y - 20 <= p.centerXY.Y && p.centerXY.Y <= y + 20))
                        {
                            IDtoremove = p.ID;
                            myVertexList.Remove(p);
                            break;
                        }
                    foreach (Vertex p in myVertexList)
                        foreach (int ed in p.edge)
                            if (ed == IDtoremove)
                            {
                                p.edge.Remove(ed);
                                break;
                            }
                    IDtoremove = 0;
                    x = 0;
                    y = 0;
                    pictureBox1.Invalidate();
                }
                //int det = 0;
                Dictionary<int, int> dictdeleteedge = new Dictionary<int, int>();

                if (radioButton2.Checked) { //todo fajnie byloby to poprawic :d 
                    x = e.X;
                    y = e.Y;

                    foreach (Vertex p in myVertexList)
                    {
                        foreach (int n in p.edge)
                        {
                            foreach (Vertex py in myVertexList) {
                                if (py.ID == n)
                                {
                                    if(IsOnLine(p.centerXY, py.centerXY, new Point(x,y), 5))
                                            dictdeleteedge.Add(p.ID, py.ID);
                                        
                                        
                                }
                            }
                        }
                        
                    }

                    foreach (Vertex v in myVertexList) {
                        foreach (int ed in v.edge)
                        {
                            KeyValuePair<int, int> pair = new KeyValuePair<int, int>(v.ID, ed);
                            if (dictdeleteedge.Contains(pair))
                            {
                                v.edge.Remove(ed);
                                break;
                            }
                        }
                    }
                    x = 0;
                    y = 0;
                    pictureBox1.Invalidate();
                }
            }
        }

        bool IsOnLine(Point p1, Point p2, Point p, int width = 1)
        {
            var isOnLine = false;
            using (var path = new GraphicsPath())
            {
                using (var pen = new Pen(Brushes.Black, width))
                {
                    path.AddLine(p1, p2);
                    isOnLine = path.IsOutlineVisible(p, pen);
                }
            }
            return isOnLine;
        }
        private void DrawerPaint(object sender, PaintEventArgs e)
        {

            g = e.Graphics;
            p1.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
            foreach (Vertex p in myVertexList)
            {

                if(p.edge.Count > 0)
                    foreach (int ed in p.edge) { 
                        foreach(Vertex pt in myVertexList)
                            if (pt.ID == ed) {
                                g.DrawLine(p1, p.centerXY, pt.centerXY);
                            }
                    }
            }

            foreach (Vertex p in myVertexList)
            {
                g.DrawEllipse(new Pen(Color.Navy, 2), p.centerXY.X - 25, p.centerXY.Y - 25, 50, 50);
                g.FillEllipse(new SolidBrush(p.color), p.centerXY.X - 25, p.centerXY.Y - 25, 49, 49);
                g.DrawString((p.ID-1).ToString(), drawFont, drawBrush, p.centerXY);
            }
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            label1.Text = e.X.ToString();
            label2.Text = e.Y.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            myPtsLine = new ArrayList();
            myVertexList = new List<Vertex>();
            ID = 2;
            pictureBox1.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
        //TODO Procedura przekształcająca do problemu SAT   
            cnf = new Generate_cnf();


            cnf.Run(myVertexList.Count*4, createListOfNeighbours(myVertexList));

            richTextBox1.Text = "Wynik transformacji do problemu SAT:\n\n";
            richTextBox1.Text += String.Join(" ", cnf.CnfList.ToArray());
        }

        private List<string> createListOfNeighbours(List<Vertex> myVertexList)
        {
            List<string> neig = new List<string>();
            
            foreach (Vertex v in myVertexList) {
                foreach (int ed in v.edge) {
                    if (!neig.Contains(ed.ToString() + ":" + v.ID.ToString())) //&& !neig.Contains(v.ID.ToString() + ":" + ed.ToString())) // tutaj ewentulanie poprawic 1:2 2:1
                    {
                        neig.Add(ed.ToString() + ":" + v.ID.ToString());
                    }
                }
            }
            return neig;
        }

        private void button3_Click(object sender, EventArgs e)
        {
        //TODO wywołąnie Sat solvera 
        // zwrócenie wyniku i jego wyświetlenie
        // czyli nadanie wszystkim vertexom z listy myVertexList idpowiednia wartośc pola color i ponowne rysowanie planszy pictureBox1.Invalidate(); 


            System.IO.File.WriteAllLines(@"problem.cnf", cnf.CnfList.ToArray());

            var rsat = new Rsat();
            rsat.Run();

            if (rsat.IsSatisfiable)
            { 
                //todo stworzyc dicta z wyniku zwroconego przez rsat . rsat.ResultVariables return List<string>
                if (rsat.ResultVariables == "")
                {
                    Result = new Dictionary<int, int>(myVertexList.Count);

                    for (int i = 0; i < myVertexList.Count; i++)
                    {
                        //KeyValuePair<int,int> values = new KeyValuePair<int, int>(myVertexList[i].ID, i + 1);
                        Result.Add(myVertexList[i].ID, i + 1);
                    }

                    kColoringGraph coloring = new kColoringGraph(Result, myVertexList);
                    coloring.Coloring();
                    pictureBox1.Invalidate();

                }
                else
                {
                    richTextBox1.Text = "\n" + rsat.ResultVariables;

                    Result = new Dictionary<int, int>();
                    var variableList = GetVariableList();
                    ConvertRsatVariablesToColors(rsat.ResultVariables, variableList);

                    kColoringGraph coloring = new kColoringGraph(Result, myVertexList);
                    coloring.Coloring();
                    pictureBox1.Invalidate();
                }
            }
            else
            {
                if(myVertexList.Count == createListOfNeighbours(myVertexList).Count)
                    ;
                MessageBox.Show("Not Satisfiable!", "Error");
                richTextBox1.Text += "Nie rozwiązywalne";
            }


            // mały test działa
            /*
            Dictionary<int, int> tmp = new Dictionary<int,int>();

            foreach (Vertex v in myVertexList)
            {
                if (v.ID == 1 || v.ID == 3)
                    tmp.Add(v.ID, 10);
                else 
                    tmp.Add(v.ID, v.ID);
            }
            ColoringGraph coloring = new ColoringGraph(tmp, myVertexList);
            myVertexList = coloring.Coloring();
            pictureBox1.Invalidate();
        
             */
         }
        //todo przeanalizowac ponizszy kod 
        private void ConvertRsatVariablesToColors(string rsatResultVariables, int[] variableList)
        {
            if (!string.IsNullOrEmpty(rsatResultVariables))
            {
                rsatResultVariables = rsatResultVariables.Replace('v', ' ');
                rsatResultVariables = rsatResultVariables.TrimStart();

                string[] split = rsatResultVariables.Split(' ');
                int[] colorsTable = new int[split.Length];

                for (int i = 0; i != split.Length; i++)
                {
                    colorsTable[i] = int.Parse(split[i]);
                }

                for (int i = 0; i != split.Length; i++)
                {
                    for (int j = 0; j < cnf.CnfList.Count * 4; j++)
                    {
                        if (variableList[j] == colorsTable[i])
                        {
                            var country = ((variableList[j] - 1) / 4) + 1;
                            var color = ((variableList[j] - 1) % 4) + 1;

                            if (Result.ContainsKey(country))
                            {
                                Result[country] = color;
                                ;
                            }
                            else
                            {
                               /* if(country == 1 && color > 2)
                                    Result.Add(country, color-2);
                                else if (country ==1 && color == 2)
                                    Result.Add(country, color-1);
                                else*/
                                    Result.Add(country, color);
                            }
                        }
                    }
                }
            }
        }

        private int[] GetVariableList()
        {
            var variableList = new int[cnf.CnfList.Count * 4];
            int counter = 0;

            foreach (string variable in cnf.firsC)
            {
                for (int i = 0; i < 4; i++)
                {
                    variableList[counter] = int.Parse(variable.Split(' ')[i]);
                    counter++;
                }
            }

            return variableList;
        }
    }
    public class Vertex
    {
        public int ID;
        public Point centerXY;
        public List<int> edge;
        public Color color;

        public Vertex(int id, Point XY)
        {
            this.centerXY = XY;
            this.ID = id;
            this.edge = new List<int>();
            this.color = Color.Aquamarine;
        }

        public void addEdge(int IdEdge)
        {
            this.edge.Add(IdEdge);

        }
        public void setColor(Color col)
        {
            this.color = col;
        }

    }
}
