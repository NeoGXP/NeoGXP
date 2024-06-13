namespace GXPEngine;

public class ShapeTest : Game
{
	private readonly EasyDraw _easyDraw;
	private int _frameCount = 0;

	public ShapeTest() : base(1900, 1000, false, pPixelArt: false)
	{
		_easyDraw = new EasyDraw(width, height, false);
		_easyDraw.Clear(Color.CadetBlue);
		this.AddChild(_easyDraw);
	}

	void Update()
	{
		_easyDraw.Clear(Color.CadetBlue);

		// Rectangles
		_easyDraw.ShapeAlign(CenterMode.Min, CenterMode.Min);
		_easyDraw.Fill(255);
		_easyDraw.NoStroke();
		_easyDraw.Rect(GetCoord(0), GetCoord(0), SIZE, SIZE);
		_easyDraw.Stroke(255, 0, 0);
		_easyDraw.StrokeWeight(5);
		_easyDraw.Rect(GetCoord(1), GetCoord(0), SIZE, SIZE);
		_easyDraw.NoFill();
		_easyDraw.Rect(GetCoord(2), GetCoord(0), SIZE, SIZE);

		// Circles
		_easyDraw.Fill(255);
		_easyDraw.NoStroke();
		_easyDraw.Ellipse(GetCoord(3), GetCoord(0), SIZE, SIZE);
		_easyDraw.Stroke(0, 255, 0);
		_easyDraw.StrokeWeight(5);
		_easyDraw.Ellipse(GetCoord(4), GetCoord(0), SIZE, SIZE);
		_easyDraw.NoFill();
		_easyDraw.Ellipse(GetCoord(5), GetCoord(0), SIZE, SIZE);

		// Ellipses
		_easyDraw.Fill(255);
		_easyDraw.NoStroke();
		_easyDraw.Ellipse(GetCoord(6), GetCoord(0), SIZE * 2, SIZE);
		_easyDraw.Stroke(0, 0, 255);
		_easyDraw.StrokeWeight(5);
		_easyDraw.Ellipse(GetCoord(8), GetCoord(0), SIZE * 2, SIZE);
		_easyDraw.NoFill();
		_easyDraw.Ellipse(GetCoord(10), GetCoord(0), SIZE * 2, SIZE);

		// Arcs
		_easyDraw.Fill(255);
		_easyDraw.NoStroke();
		_easyDraw.Arc(GetCoord(13), GetCoord(0), SIZE, SIZE, 0, 190);
		_easyDraw.Stroke(255, 255, 0);
		_easyDraw.StrokeWeight(5);
		_easyDraw.Arc(GetCoord(12), GetCoord(0), SIZE, SIZE, 0, 340);
		_easyDraw.NoFill();
		_easyDraw.Arc(GetCoord(14), GetCoord(0), SIZE, SIZE, 45, 135);
		_easyDraw.Arc(GetCoord(15), GetCoord(0), SIZE, SIZE, 0, _frameCount % 360);
		_easyDraw.Stroke(0, 255, 0);
		_easyDraw.StrokeWeight(5);
		_easyDraw.Fill(Color.DeepPink);
		_easyDraw.Arc(GetCoord(16), GetCoord(0), SIZE, SIZE, _frameCount / 2f % 360 - 180, _frameCount % 360);

		// Lines
		_easyDraw.Stroke(255);
		_easyDraw.StrokeWeight(1);
		_easyDraw.Line(GetCoord(0), GetCoord(1), GetCoord(0) + SIZE, GetCoord(1) + SIZE);
		_easyDraw.Stroke(0, 255, 255);
		_easyDraw.StrokeWeight(5);
		_easyDraw.Line(GetCoord(1), GetCoord(1), GetCoord(1) + SIZE, GetCoord(1) + SIZE);

		// Triangle
		_easyDraw.Fill(255);
		_easyDraw.NoStroke();
		_easyDraw.Triangle(GetCoord(2), GetCoord(1), GetCoord(2) + SIZE, GetCoord(1), GetCoord(2) + SIZE, GetCoord(1) + SIZE);
		_easyDraw.Stroke(0, 0, 255);
		_easyDraw.StrokeWeight(5);
		_easyDraw.Triangle(GetCoord(3), GetCoord(1) + SIZE, GetCoord(3) + SIZE, GetCoord(1), GetCoord(3) + SIZE, GetCoord(1) + SIZE);
		_easyDraw.NoFill();
		_easyDraw.Triangle(GetCoord(4), GetCoord(1), GetCoord(4) + SIZE, GetCoord(1) + SIZE / 2, GetCoord(4), GetCoord(1) + SIZE);

		// Quad (Normal Squares)
		_easyDraw.Fill(255);
		_easyDraw.NoStroke();
		_easyDraw.Quad(GetCoord(5), GetCoord(1), GetCoord(5) + SIZE, GetCoord(1), GetCoord(5) + SIZE, GetCoord(1) + SIZE, GetCoord(5), GetCoord(1) + SIZE);
		_easyDraw.Stroke(0, 0, 255);
		_easyDraw.StrokeWeight(5);
		_easyDraw.Quad(GetCoord(6), GetCoord(1), GetCoord(6) + SIZE, GetCoord(1), GetCoord(6) + SIZE, GetCoord(1) + SIZE, GetCoord(6), GetCoord(1) + SIZE);
		_easyDraw.NoFill();
		_easyDraw.Quad(GetCoord(7), GetCoord(1), GetCoord(7) + SIZE, GetCoord(1), GetCoord(7) + SIZE, GetCoord(1) + SIZE, GetCoord(7), GetCoord(1) + SIZE);

		// Quad (Rectangles)
		_easyDraw.Fill(255);
		_easyDraw.NoStroke();
		_easyDraw.Quad(GetCoord(8), GetCoord(1), GetCoord(8) + SIZE, GetCoord(1), GetCoord(8) + SIZE, GetCoord(1) + SIZE / 2, GetCoord(8), GetCoord(1) + SIZE / 2);
		_easyDraw.Stroke(255, 0, 255);
		_easyDraw.StrokeWeight(5);
		_easyDraw.Quad(GetCoord(9), GetCoord(1), GetCoord(9) + SIZE, GetCoord(1), GetCoord(9) + SIZE, GetCoord(1) + SIZE / 2, GetCoord(9), GetCoord(1) + SIZE / 2);
		_easyDraw.NoFill();
		_easyDraw.Quad(GetCoord(10), GetCoord(1), GetCoord(10) + SIZE, GetCoord(1), GetCoord(10) + SIZE, GetCoord(1) + SIZE / 2, GetCoord(10), GetCoord(1) + SIZE / 2);

		// Quad (Diamonds)
		_easyDraw.Fill(255);
		_easyDraw.NoStroke();
		_easyDraw.Quad(GetCoord(11), GetCoord(1) + SIZE / 2, GetCoord(11) + SIZE / 2, GetCoord(1), GetCoord(11) + SIZE, GetCoord(1) + SIZE / 2, GetCoord(11) + SIZE / 2, GetCoord(1) + SIZE);
		_easyDraw.Stroke(0, 255, 255);
		_easyDraw.StrokeWeight(5);
		_easyDraw.Quad(GetCoord(12), GetCoord(1) + SIZE / 2, GetCoord(12) + SIZE / 2, GetCoord(1), GetCoord(12) + SIZE, GetCoord(1) + SIZE / 2, GetCoord(12) + SIZE / 2, GetCoord(1) + SIZE);
		_easyDraw.NoFill();
		_easyDraw.Quad(GetCoord(13), GetCoord(1) + SIZE / 2, GetCoord(13) + SIZE / 2, GetCoord(1), GetCoord(13) + SIZE, GetCoord(1) + SIZE / 2, GetCoord(13) + SIZE / 2, GetCoord(1) + SIZE);

		// Quad (Parallelograms)
		_easyDraw.Fill(255);
		_easyDraw.NoStroke();
		_easyDraw.Quad(GetCoord(14), GetCoord(1), GetCoord(14) + SIZE / 2, GetCoord(1), GetCoord(14) + SIZE, GetCoord(1) + SIZE, GetCoord(14) + SIZE / 2, GetCoord(1) + SIZE);
		_easyDraw.Stroke(0, 0, 0);
		_easyDraw.StrokeWeight(5);
		_easyDraw.Quad(GetCoord(15), GetCoord(1), GetCoord(15) + SIZE / 2, GetCoord(1), GetCoord(15) + SIZE, GetCoord(1) + SIZE, GetCoord(15) + SIZE / 2, GetCoord(1) + SIZE);
		_easyDraw.NoFill();
		_easyDraw.Quad(GetCoord(16), GetCoord(1), GetCoord(16) + SIZE / 2, GetCoord(1), GetCoord(16) + SIZE, GetCoord(1) + SIZE, GetCoord(16) + SIZE / 2, GetCoord(1) + SIZE);

		// Quad (A)
		_easyDraw.Fill(255);
		_easyDraw.NoStroke();
		_easyDraw.Quad(GetCoord(0), GetCoord(2) + SIZE, GetCoord(0) + SIZE / 2, GetCoord(2), GetCoord(0) + SIZE, GetCoord(2) + SIZE, GetCoord(0) + SIZE / 2, GetCoord(2) + SIZE / 2);
		_easyDraw.Stroke(255, 0, 0);
		_easyDraw.StrokeWeight(5);
		_easyDraw.Quad(GetCoord(1), GetCoord(2) + SIZE, GetCoord(1) + SIZE / 2, GetCoord(2), GetCoord(1) + SIZE, GetCoord(2) + SIZE, GetCoord(1) + SIZE / 2, GetCoord(2) + SIZE / 2);
		_easyDraw.NoFill();
		_easyDraw.Quad(GetCoord(2), GetCoord(2) + SIZE, GetCoord(2) + SIZE / 2, GetCoord(2), GetCoord(2) + SIZE, GetCoord(2) + SIZE, GetCoord(2) + SIZE / 2, GetCoord(2) + SIZE / 2);

		// Polygon (Pentagon)
		float[] PentagonPoints(int col)
		{
			return
			[
				GetCoord(col) + 0.97552824f * SIZE, GetCoord(2) + 0.3854915f * SIZE,
				GetCoord(col) + 0.7938926f * SIZE, GetCoord(2) + 0.9445085f * SIZE,
				GetCoord(col) + 0.20610741f * SIZE, GetCoord(2) + 0.9445085f * SIZE,
				GetCoord(col) + 0.02447182f * SIZE, GetCoord(2) + 0.38549128f * SIZE,
				GetCoord(col) + 0.5f * SIZE, GetCoord(2) + 0.04f * SIZE,
			]; //Coordinates generated with Processing :)
		}

		_easyDraw.Fill(255);
		_easyDraw.NoStroke();
		_easyDraw.Polygon(PentagonPoints(3));
		_easyDraw.Stroke(Color.DarkOrange);
		_easyDraw.StrokeWeight(5);
		_easyDraw.Polygon(PentagonPoints(4));
		_easyDraw.NoFill();
		_easyDraw.Polygon(PentagonPoints(5));

		// Polygon (Hexagon)
		float[] HexagonPoints(int col)
		{
			return
			[
				GetCoord(col) + 0.9330127f * SIZE, GetCoord(2) + 0.25f * SIZE,
				GetCoord(col) + 0.9330127f * SIZE, GetCoord(2) + 0.75f * SIZE,
				GetCoord(col) + 0.49999997f * SIZE, GetCoord(2) + 1.0f * SIZE,
				GetCoord(col) + 0.066987306f * SIZE, GetCoord(2) + 0.75f * SIZE,
				GetCoord(col) + 0.066987365f * SIZE, GetCoord(2) + 0.24999991f * SIZE,
				GetCoord(col) + 0.5f * SIZE, GetCoord(2) + 0.0f * SIZE,
			]; //Coordinates generated with Processing :)
		}

		_easyDraw.Fill(255);
		_easyDraw.NoStroke();
		_easyDraw.Polygon(HexagonPoints(6));
		_easyDraw.Stroke(Color.Yellow);
		_easyDraw.StrokeWeight(5);
		_easyDraw.Polygon(HexagonPoints(7));
		_easyDraw.NoFill();
		_easyDraw.Polygon(HexagonPoints(8));


		if (_frameCount == 100)
		{
			SaveFrame("test.png");
		}
		_frameCount++;
	}

	private const float SIZE = 100f;
	private static float GetCoord(float x)
	{
		return SIZE / 10 + (SIZE + SIZE / 10) * x;
	}
}
