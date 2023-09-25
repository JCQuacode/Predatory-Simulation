// Program Description: This program contains a position class to store and manipulate the
//    positions of the animals. It has a method implementation to move and to print the positions.

// Position:
// Has attributes to show the position of objects.
// Move method to move the positions of the objects.
// Other move methods to randomize the positions of the objects
// ToString method to display their position.

public class Position
{
    // Attributes
    private double x;
    private double y;
    private double z;

    // Constructors
    public Position() { }
    public Position(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    // Properties for x, y and z
    public double X
    {
        get
        {
            return x;
        }
        set
        { x = Math.Clamp(value, 0, 100); }
    }
    public double Y
    {
        get
        {
            return y;
        }
        set
        { y = Math.Clamp(value, 0, 27); } // Clamped to 27 since the console window doesn't work well
                                          // this includes the room to display the text that shows the bird being eaten
    }
    public double Z
    {
        get
        {
            return z;
        }
        set
        { z = Math.Clamp(value, 0, 10); }
    }

    // Method to move the positions of the objects
    public void Move(double dx, double dy, double dz)
    {
        // Move x
        X = x + dx;

        // Move y
        Y = Y + dy;

        // Move z
        Z = Z + dz;

        //Console.WriteLine($"{dx}, {dy}, {dz}");
    }

    // Method to move to the animal
    public void MoveTo(Animal animal, double dist, double range)
    {
        double dx = 0, x = animal.Pos.X;
        double dy = 0, y = animal.Pos.Y;
        double dz = 0, z = animal.Pos.Z;

        if(Math.Abs(x - X) > range)
            dx = x - X > 0 ? dist : -dist;
        if (Math.Abs(y - Y) > range)
            dy = y - Y > 0 ? dist : -dist;
        if (Math.Abs(z - Z) > range && this is Bird)
            dz = z - Z > 0 ? dist : -dist;

        Move(dx, dy, dz);
    }

    // Method to randomly move the x of an object
    public void MoveRandomX(int startRange, int endRange)
    {
        double dx, dy = 0, dz = 0;
        Random r = new Random();

        dx = r.Next(startRange, endRange);

        Move(dx, dy, dz);
    }

    // Method to randomly move the y of an object
    public void MoveRandomY(int startRange, int endRange)
    {
        double dx = 0, dy, dz = 0;
        Random r = new Random();

        dy = r.Next(startRange, endRange);

        Move(dx, dy, dz);
    }

    // Method to randomly move the z of an object
    public void MoveRandomZ(int startRange, int endRange)
    {
        double dx = 0, dy = 0, dz;
        Random r = new Random();

        dz = r.Next(startRange, endRange);

        Move(dx, dy, dz);
    }

    // Method to randomize the position
    public static Position RandomPosition
        (int rangeStart, int rangeEnd, bool forX = false, bool forY = false, bool forZ = false)
    {
        double x = 0, y = 0, z = 0;
        Random r = new Random();

        if (forX)
            x = r.Next(rangeStart, rangeEnd) + r.NextDouble();
        if (forY)
            y = r.Next(rangeStart, rangeEnd) + r.NextDouble();
        if (forZ)
            z = r.Next(rangeStart, rangeEnd) + r.NextDouble();

        return new Position(x, y, z);

    }

    // Method to randomize the position of x
    public void RandomizePositionX(int rangeStart, int rangeEnd)
    {
        Random r = new Random();
        X = r.Next(rangeStart, rangeEnd) + r.NextDouble();
    }

    // Method to randomize the position of y
    public void RandomizePositionY(int rangeStart, int rangeEnd)
    {
       Random r = new Random();
        Y = r.Next(rangeStart, rangeEnd) + r.NextDouble();
    }

    // Method to randomize the position of z
    public void RandomizePositionZ(int rangeStart, int rangeEnd)
    {
        Random r = new Random();
        Z = r.Next(rangeStart, rangeEnd) + r.NextDouble();
    }

    // Method to find the distance between two points
    public static double DistanceBetween(Position pos1, Position pos2)
    {
        double result;
        double x, y, z;

        x = pos2.X - pos1.X;
        y = pos2.Y - pos1.Y;
        z = pos2.Z - pos1.Z;

        result = Math.Sqrt( Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Pow(z, 2) );

        return result;
    }

    // ToString to print out the position of the animal
    public override string ToString()
    {
        string result = String.Format("({0:F1},{1:F1},{2:F1})", X, Y, Z);
        return result;
    }
}