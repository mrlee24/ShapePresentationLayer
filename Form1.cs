using ShapeBusinessLayer;
using ShapeDataLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShapePresentationLayer
{
    public partial class Form1 : Form
    {
        BUS_Shape busShape = new BUS_Shape();
        List<Shape2D> shapes = new List<Shape2D>();
        List<Circle> circles = new List<Circle>();
        List<ShapeBusinessLayer.Rectangle> rectangles = new List<ShapeBusinessLayer.Rectangle>();
        List<Ellipse> ellipses = new List<Ellipse>();
        List<IShape2D> shape2Ds = new List<IShape2D>();

        ShapeBusinessLayer.Rectangle rectangle;
        Ellipse ellipse;
        IShape2D shape2D;
        int index;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cbbBorder.DataSource = Enum.GetValues(typeof(EnumBorderColor));
            cbbBorder.Text = Convert.ToString(cbbBorder.Items[0]);

            cbbShape.DataSource = Enum.GetValues(typeof(EnumBrushingColor));
            cbbShape.Text = Convert.ToString(cbbShape.Items[0]);

            cbbShapeType.DataSource = Enum.GetValues(typeof(EnumShapeType));
        }

        private void btnOpenXML_Click(object sender, EventArgs e)
        {
            shapes = busShape.GetAllShape();

            lbShape.DataSource = shapes;
            lbShape.SelectedIndex = 0;
        }

        private void Ref()
        {
            cbbShapeType.Focus();
            cbbShapeType.DataSource = Enum.GetValues(typeof(EnumShapeType));
            cbbShapeType.Text = EnumShapeType.Undefine.ToString();
            tbX.Text = "";
            tbY.Text = "";
            tbHeight.Text = "";
            tbWidth.Text = "";
            tbXRadius.Text = "";
            tbYRadius.Text = "";
            tbRadius.Text = "";
            lbShape.Items.Clear();
            panelDrawing.Refresh();
        }

        private void btnSaveXML_Click(object sender, EventArgs e)
        {
            busShape.SaveAllShapes(shapes);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int shapeName = shape2Ds.Count + 1;

            int x = int.Parse(tbX.Text);
            int y = int.Parse(tbY.Text);

            if (cbbShapeType.Text == "Ellipse")
            {
                EnumShapeType shapeType = EnumShapeType.Ellipse;
                EnumBorderColor borderColor = (EnumBorderColor)Enum.Parse(typeof(EnumBorderColor), cbbBorder.Text);
                EnumBrushingColor brushingColor = (EnumBrushingColor)Enum.Parse(typeof(EnumBrushingColor), cbbShape.Text);

                float xRadius = float.Parse(tbXRadius.Text);
                float yRadius = float.Parse(tbYRadius.Text);

                Ellipse ellipse = new Ellipse(shapeName.ToString(), x, y, shapeType, brushingColor, borderColor, xRadius, yRadius);

                shapes.Add(ellipse);
                shape2Ds.Add(ellipse);
                ellipses.Add(ellipse);
            }
            else if (cbbShapeType.Text == "Rectangle")
            {
                EnumShapeType shapeType = EnumShapeType.Rectangle;
                EnumBorderColor borderColor = (EnumBorderColor)Enum.Parse(typeof(EnumBorderColor), cbbBorder.Text);
                EnumBrushingColor brushingColor = (EnumBrushingColor)Enum.Parse(typeof(EnumBrushingColor), cbbShape.Text);

                int width = int.Parse(tbWidth.Text);
                int height = int.Parse(tbHeight.Text);

                ShapeBusinessLayer.Rectangle rectangle = new ShapeBusinessLayer.Rectangle(shapeName.ToString(), x, y, shapeType, brushingColor, borderColor, width, height);

                shapes.Add(rectangle);
                rectangles.Add(rectangle);
                shape2Ds.Add(rectangle);
            }
            else if (cbbShapeType.Text == "Circle")
            {
                EnumShapeType shapeType = EnumShapeType.Circle;
                EnumBorderColor borderColor = (EnumBorderColor)Enum.Parse(typeof(EnumBorderColor), cbbBorder.Text);
                EnumBrushingColor brushingColor = (EnumBrushingColor)Enum.Parse(typeof(EnumBrushingColor), cbbShape.Text);

                float xRadius = float.Parse(tbRadius.Text);
                float yRadius = float.Parse(tbRadius.Text);

                Ellipse ellipse = new Ellipse(shapeName.ToString(), x, y, shapeType, brushingColor, borderColor, xRadius, yRadius);
                Circle circle = new Circle(shapeName.ToString(), x, y, shapeType, brushingColor, borderColor, xRadius, yRadius);

                shapes.Add(circle);
                shape2Ds.Add(ellipse);
                circles.Add(circle);
                ellipses.Add(circle);
            }
            Ref();
            UpdateListBox();
        }

        private void UpdateListBox()
        {
            lbShape.Items.Clear();
            foreach (IShape2D shape in shape2Ds)
                lbShape.Items.Add(shape);
        }

        private void cbbShapeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbShapeType.Text == "Circle")
            {
                tbRadius.ReadOnly = false;
                tbWidth.ReadOnly = true;
                tbHeight.ReadOnly = true;
                tbXRadius.ReadOnly = true;
                tbYRadius.ReadOnly = true;
            }
            else if (cbbShapeType.Text == "Rectangle")
            {
                tbRadius.ReadOnly = true;
                tbXRadius.ReadOnly = true;
                tbYRadius.ReadOnly = true;
                tbWidth.ReadOnly = false;
                tbHeight.ReadOnly = false;
            }
            else if (cbbShapeType.Text == "Ellipse")
            {
                tbRadius.ReadOnly = true;
                tbWidth.ReadOnly = true;
                tbHeight.ReadOnly = true;
                tbXRadius.ReadOnly = false;
                tbYRadius.ReadOnly = false;
            }
            else
            {
                tbRadius.ReadOnly = true;
                tbWidth.ReadOnly = true;
                tbHeight.ReadOnly = true;
                tbXRadius.ReadOnly = true;
                tbYRadius.ReadOnly = true;
            }
        }

        private void lbShape_SelectedIndexChanged(object sender, EventArgs e)
        {
            index = lbShape.SelectedIndex;
            if (index >= 0)
            {
                if (shapes[index].ShapeType == (EnumShapeType)Enum.Parse(typeof(EnumShapeType), "Circle"))
                {
                    cbbShapeType.Text = EnumShapeType.Circle.ToString();
                    tbX.Text = circles[index].XCoordinate.ToString();
                    tbY.Text = circles[index].YCoordinate.ToString();
                    tbRadius.Text = circles[index].XRadius.ToString();
                    tbRadius.Text = circles[index].YRadius.ToString();
                }
                else if (shapes[index].ShapeType == (EnumShapeType)Enum.Parse(typeof(EnumShapeType), "Rectangle"))
                {
                    cbbShapeType.Text = EnumShapeType.Rectangle.ToString();
                    tbX.Text = rectangles[index].XCoordinate.ToString();
                    tbY.Text = rectangles[index].YCoordinate.ToString();
                    tbHeight.Text = rectangles[index].Length.ToString();
                    tbWidth.Text = rectangles[index].Width.ToString();
                }
                else if (shapes[index].ShapeType == (EnumShapeType)Enum.Parse(typeof(EnumShapeType), "Ellipse"))
                {
                    cbbShapeType.Text = EnumShapeType.Ellipse.ToString();
                    tbX.Text = ellipses[index].XCoordinate.ToString();
                    tbY.Text = ellipses[index].YCoordinate.ToString();
                    tbXRadius.Text = ellipses[index].XRadius.ToString();
                    tbYRadius.Text = ellipses[index].YRadius.ToString();
                }
            }
        }

        protected void Draw()
        {
            if (cbbShapeType.Text == "Rectangle")
                rectangle = new ShapeBusinessLayer.Rectangle(shape2D.XCoordinate, shape2D.YCoordinate, (EnumBrushingColor)Enum.Parse(typeof(EnumBrushingColor), cbbShape.Text), (EnumBorderColor)Enum.Parse(typeof(EnumBorderColor), cbbBorder.Text), int.Parse(tbWidth.Text), int.Parse(tbHeight.Text));
            else if (cbbShapeType.Text == "Ellipse")
                ellipse = new Ellipse(shape2D.XCoordinate, shape2D.YCoordinate, (EnumBrushingColor)Enum.Parse(typeof(EnumBrushingColor), cbbShape.Text), (EnumBorderColor)Enum.Parse(typeof(EnumBorderColor), cbbBorder.Text), int.Parse(tbXRadius.Text), int.Parse(tbYRadius.Text));
            panelDrawing.Invalidate();
            panelDrawing.Update();
            Refresh();
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            Draw();
        }


        private void panelDrawing_Paint(object sender, PaintEventArgs e)
        {
            if (cbbShapeType.Text == "Rectangle" && rectangle != null)
                rectangle.Draw(e);
            else if (cbbShapeType.Text == "Ellipse" && ellipse != null)
                ellipse.Draw(e);
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            shape2D.MoveUp(int.Parse(tbY.Text));
            Draw();
            UpdateListBox();
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            shape2D.MoveDown(int.Parse(tbY.Text));
            Draw();
            UpdateListBox();
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            shape2D.MoveLeft(int.Parse(tbX.Text));
            Draw();
            UpdateListBox();
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            shape2D.MoveRight(int.Parse(tbX.Text));
            Draw();
            UpdateListBox();
        }

        private void btnUpLeft_Click(object sender, EventArgs e)
        {
            shape2D.MoveUpLeft(int.Parse(tbX.Text), int.Parse(tbY.Text));
            Draw();
            UpdateListBox();
        }

        private void btnDownLeft_Click(object sender, EventArgs e)
        {
            shape2D.MoveDownLeft(int.Parse(tbX.Text), int.Parse(tbY.Text));
            Draw();
            UpdateListBox();
        }

        private void btnUpRight_Click(object sender, EventArgs e)
        {
            shape2D.MoveUpRight(int.Parse(tbX.Text), int.Parse(tbY.Text));
            Draw();
            UpdateListBox();
        }

        private void btnDownRight_Click(object sender, EventArgs e)
        {
            shape2D.MoveDownRight(int.Parse(tbX.Text), int.Parse(tbY.Text));
            Draw();
            UpdateListBox();
        }

        private void tbX_TextChanged(object sender, EventArgs e)
        {
            if (!Validator.IsDecimal(tbX.Text))
            {
                tbX.Clear();
                MessageBox.Show("It must be a decimal number");
                tbX.Focus();
            }
        }

        private void tbY_TextChanged(object sender, EventArgs e)
        {
            if (!Validator.IsDecimal(tbY.Text))
            {
                tbY.Clear();
                MessageBox.Show("It must be a decimal number");
                tbY.Focus();
            }
        }

        private void tbHeight_TextChanged(object sender, EventArgs e)
        {
            if (!Validator.IsDecimal(tbHeight.Text))
            {
                tbHeight.Clear();
                MessageBox.Show("It must be a decimal number");
                tbHeight.Focus();
            }
        }

        private void tbWidth_TextChanged(object sender, EventArgs e)
        {
            if (!Validator.IsDecimal(tbWidth.Text))
            {
                tbWidth.Clear();
                MessageBox.Show("It must be a decimal number");
                tbWidth.Focus();
            }
        }

        private void tbXRadius_TextChanged(object sender, EventArgs e)
        {
            if (!Validator.IsDecimal(tbXRadius.Text))
            {
                tbXRadius.Clear();
                MessageBox.Show("It must be a decimal number");
                tbXRadius.Focus();
            }
        }

        private void tbYRadius_TextChanged(object sender, EventArgs e)
        {
            if (!Validator.IsDecimal(tbYRadius.Text))
            {
                tbYRadius.Clear();
                MessageBox.Show("It must be a decimal number");
                tbYRadius.Focus();
            }
        }

        private void tbRadius_TextChanged(object sender, EventArgs e)
        {
            if (!Validator.IsDecimal(tbRadius.Text))
            {
                tbRadius.Clear();
                MessageBox.Show("It must be a decimal number");
                tbRadius.Focus();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Ref();
        }
    }
}
