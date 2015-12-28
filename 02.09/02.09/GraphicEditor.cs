﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _02._09
{
    public partial class GraphicEditor : Form
    {
        private MyGraphic myGraphic = new MyGraphic();
        private bool isPressed = false;
        private bool isDrawing = false;
        private bool isMoving = false;
        private bool isDeleting = false;
        private bool isEndCatched = false;
        private float x = 0;
        private float y = 0;
        private float x1 = 0;
        private float y1 = 0;

        public GraphicEditor()
        {
            InitializeComponent();
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            if (sender.Equals(drawButton))
            {
                isDrawing = true;
                isMoving = false;
                isDeleting = false;
                drawButton.BackColor = Color.Gray;
                moveButton.BackColor = SystemColors.Control;
                deleteButton.BackColor = SystemColors.Control;
            }
            else if (sender.Equals(moveButton))
            {
                isDrawing = false;
                isMoving = true;
                isDeleting = false;
                drawButton.BackColor = SystemColors.Control;
                moveButton.BackColor = Color.Gray;
                deleteButton.BackColor = SystemColors.Control;
            }
            else if (sender.Equals(deleteButton))
            {
                isDrawing = false;
                isMoving = false;
                isDeleting = true;
                drawButton.BackColor = SystemColors.Control;
                moveButton.BackColor = SystemColors.Control;
                deleteButton.BackColor = Color.Gray;
            }
            else if (sender.Equals(undoButton))
            {
                isDrawing = false;
                isMoving = false;
                isDeleting = false;
                drawButton.BackColor = SystemColors.Control;
                moveButton.BackColor = SystemColors.Control;
                deleteButton.BackColor = SystemColors.Control;
                myGraphic.Undo();
                pictureBox.Invalidate();
            }
            else if (sender.Equals(redoButton))
            {
                isDrawing = false;
                isMoving = false;
                isDeleting = false;
                drawButton.BackColor = SystemColors.Control;
                moveButton.BackColor = SystemColors.Control;
                deleteButton.BackColor = SystemColors.Control;
                myGraphic.Redo();
                pictureBox.Invalidate();
            }
        }

        private void PictureBoxMouseDown(object sender, MouseEventArgs e)
        {
            isPressed = true;
            x = e.X;
            y = e.Y;

            if (isMoving)
            {
                if (myGraphic.IsEndCatched(ref x, ref y))
                {
                    isEndCatched = true;
                }
            }
        }

        private void PictureBoxMouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                if (isPressed)
                {
                    x1 = e.X;
                    y1 = e.Y;
                    pictureBox.Invalidate();
                }
            }
            else if (isMoving)
            {
                if (isPressed && isEndCatched)
                {
                    x1 = e.X;
                    y1 = e.Y;
                    pictureBox.Invalidate();
                }
            }
        }

        private void PictureBoxMouseUp(object sender, MouseEventArgs e)
        {
            isPressed = false;

            if (isDrawing || (isMoving && isEndCatched))
            {
                isEndCatched = false;
                PointF newStartPoint = new PointF(x, y);
                PointF newEndPoint = new PointF(x1, y1);
                myGraphic.AddNewLine(newStartPoint, newEndPoint);
            }
            else if (isDeleting)
            {
                if (myGraphic.IsLineDeleted(x, y))
                {
                    pictureBox.Invalidate();
                }
            }
        }

        private void PictureBoxPaint(object sender, PaintEventArgs e)
        {
            if (isDrawing || (isMoving && isEndCatched))
            {
                myGraphic.DrawNewLine(ref e, new PointF(x, y), new PointF(x1, y1));
            }

            myGraphic.RedrawAllLines(ref e);
        }
    }
}