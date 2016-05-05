using System;
using CoreGraphics;
using CoreText;
using Foundation;
using UIKit;

namespace DrawGridSampleiOS
{
	public class GridView : UIView
	{
		CGContext context;

		public GridView()
		{
			BackgroundColor = UIColor.White;
		}

		public override void Draw(CoreGraphics.CGRect rect)
		{
			base.Draw(rect);

			context = UIGraphics.GetCurrentContext();

			var bounds = UIScreen.MainScreen.Bounds;
			var width = bounds.Width;
			var height = bounds.Height;

			var rows = 10;
			var columns = 10;

			var rowSize = Math.Round((float)(height / rows));
			var colSize = Math.Round((float)(width / columns));

			for (int r = 1; r < rows; r++)
			{
				var y = (float)(r * rowSize);
				DrawLine(0, y, width, y);
			}

			for (int c = 1; c < columns; c++)
			{
				var x = (float)(c * colSize);
				DrawLine(x, 0, x, height);
			}

			int counter = 1;

			float cellY = (float)(rowSize / 2);

			for (int i = 0; i < rows; i++)
			{
				float cellX = (float)(colSize / 2);

				for (int k = 0; k < columns; k++)
				{
					DrawText(counter.ToString(), cellX - (rows / 2), cellY - (columns / 2));

					cellX += (float)colSize;

					counter++;
				}

				cellY += (float)rowSize;
			}
		}

		public void DrawLine(nfloat x1, nfloat y1, nfloat x2, nfloat y2)
		{
			var points = new CGPoint[]{
				new CGPoint(x1, y1),
				new CGPoint(x2, y2)
			};

			context.SetLineWidth(1);

			UIColor.Clear.SetFill();
			UIColor.Black.SetStroke();

			var currentPath = new CGPath();
			currentPath.AddLines(points);

			context.AddPath(currentPath);
			context.DrawPath(CGPathDrawingMode.Stroke);
			context.SaveState();
		}

		private void DrawText(string t, float x, float y)
		{
			NSAttributedString attributedString = new NSAttributedString(
			t,
			new CTStringAttributes
			{
				ForegroundColorFromContext = true,
				Font = new CTFont("Arial", 10)
			});
			attributedString.DrawString(new CGPoint(x, y));
		}
	}
}

