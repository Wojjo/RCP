namespace WorkTimeRegistrationApp.Drawable;

public class CircularProgress : BindableObject, IDrawable
{
    public double Progress { get; set; } 

    public Color ProgressColor { get; set; } = Colors.Green;
    
    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        float strokeWidth = 10;
        float radius = Math.Min(dirtyRect.Width, dirtyRect.Height) / 2 - strokeWidth;

        canvas.StrokeSize = strokeWidth;
        canvas.StrokeColor = Colors.LightGray;
        canvas.DrawCircle(dirtyRect.Center.X, dirtyRect.Center.Y, radius);

        if (Progress > 0)
        {
            canvas.StrokeColor = ProgressColor;
            canvas.StrokeLineCap = LineCap.Round;

            float startAngle = 0; 
            float sweepAngle = (float)(-360 * Progress); 

            canvas.DrawArc(
                dirtyRect.Center.X - radius, dirtyRect.Center.Y - radius,
                radius * 2, radius * 2,
                startAngle, sweepAngle, true, false);
        }
    }
}