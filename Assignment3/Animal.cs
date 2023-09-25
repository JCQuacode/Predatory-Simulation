// Program Description: This program contains a super class: Animal, and its subclasses:
//    Cat and Snake.

// Animal:
// Superclass for other specific animal subclasses to inherit from.
// Contains shared animal attributes.
// Move method to move the birds indirectly by passing the dx, dy and dz
// to the Move() in Position.
// Other move methods to randomize the positions of the birds
// Methods to find the nearest bird and to check whether the otehr animal is in range
// ToString method to display their properties.

using GenericLinkedList;
using System.ComponentModel.DataAnnotations.Schema;
using static Cat;
public class Animal: IComparable<Animal>
{

    // Attributes
    private int id;
    private string name;
    private double age;
    private Position pos;
    private double speed;
    private DoublyLinkedList<Animal> animalsSmelled = new DoublyLinkedList<Animal>();

    // Properties
    public int Id
    {
        get
        { return id; }
        set
        { id = value; }
    }
    public string Name
    {
        get
        { return name; }
        set
        { name = value; }
    }
    public double Age
    {
        get
        { return age; }
        set
        { age = value; }
    }
    public Position Pos
    {
        get
        { return pos; }
        set
        { pos = value; }
    }
    public double Speed
    {
        get
        { return speed; }
        set
        { speed = value; }
    }

    // Method to pass the moved positions to the Move method in Position
    public void Move(double dx, double dy, double dz)
    {
        Pos.Move(dx, dy, dz);
    }

    // Method to pass the object's movement to the MoveRandomX in Position
    public void MoveRandomX(int startRange, int endRange)
    {
        Pos.MoveRandomX(startRange, endRange);
    }

    // Method to pass the object's movement to the MoveRandomY in Position
    public void MoveRandomY(int startRange, int endRange)
    {
        Pos.MoveRandomY(startRange, endRange);
    }

    // Method to pass the object's movement to the MoveRandomZ in Position
    public void MoveRandomZ(int startRange, int endRange)
    {
        Pos.MoveRandomZ(startRange, endRange);
    }

    // Method to compare the objects by name
    public int CompareTo(Animal animal)
    {
        //Animal animal = (Animal)obj;
        int result;

        if (Name.CompareTo(animal.Name) == 0)
            result = 0;
        else if (Name.CompareTo(animal.Name) == -1)
            result = -1;
        else
            result = 1;

        return result;
    }

    // Method to randomize the position of x
    public void RandomizePositionX(int rangeStart, int rangeEnd)
    {
        Pos.RandomizePositionX(rangeStart, rangeEnd);
    }

    // Method to randomize the position of y
    public void RandomizePositionY(int rangeStart, int rangeEnd)
    {
        Pos.RandomizePositionY(rangeStart, rangeEnd);
    }

    // Method to randomize the position of z
    public void RandomizePositionZ(int rangeStart, int rangeEnd)
    {
        Pos.RandomizePositionZ(rangeStart, rangeEnd);
    }

    // Find the distance to another animal
    public double FindDistance(Animal other)
    {
        return Position.DistanceBetween(Pos, other.Pos);
    }

    // Method to find the distance between two points
    public static double DistanceBetween(Position pos1, Position pos2)
    {
        return Position.DistanceBetween(pos1, pos2);
    }

    // Method to find the nearest animal in the list
    public Bird FindNearestBird(DoublyLinkedList<Animal> birds)
    {
        Position minPos = birds.Find(0).Pos;
        Position newPos;

        Bird nearestBird = new Bird();

        double distance;
        double minDistance = -1;

        int count = birds.GetCount();

        for (int i = 0; i < count; i++)
        {
            newPos = birds.Find(i).Pos;
            distance = Position.DistanceBetween(Pos, newPos);

            if (distance <= minDistance || minDistance == -1)
            {
                minDistance = distance;
                nearestBird = (Bird)birds.Find(i);
            }
        }
        return nearestBird;
    }

    // Store "Smell" the other animals in a list
    public DoublyLinkedList<Animal> Smell(DoublyLinkedList<Animal> animals)
    {
        int i = 0;
        Animal animal = animals.Find(i);

        Animal animalSmelled = null;
        if (animalsSmelled.Find(animal) != null)
            animalSmelled = animalsSmelled.Find(animal).data;

        Animal firstanimalSmelled = animalsSmelled.Find(0);

        while (animal != null)
        {
            if (animal != this)
            {
                if (animalSmelled == animal && animalSmelled != null)
                {
                    double X1 = animalSmelled.Pos.X;
                    double Y1 = animalSmelled.Pos.Y;
                    double Z1 = animalSmelled.Pos.Z;

                    double X2 = Pos.X;
                    double Y2 = Pos.Y;
                    double Z2 = Pos.Z;

                    if (!(Math.Abs(X1 - X2) <= 10 && Math.Abs(Y1 - Y2) <= 10 && Math.Abs(Z1 - Z2) <= 10))
                    {
                        animalsSmelled.Swap(animal, firstanimalSmelled);
                        animalsSmelled.DeleteFirst();
                        firstanimalSmelled = animalsSmelled.Find(0);
                    }
                }
                else
                {
                    double X1 = animal.Pos.X;
                    double Y1 = animal.Pos.Y;
                    double Z1 = animal.Pos.Z;

                    double X2 = Pos.X;
                    double Y2 = Pos.Y;
                    double Z2 = Pos.Z;

                    if (Math.Abs(X1 - X2) <= 10 && Math.Abs(Y1 - Y2) <= 10 && Math.Abs(Z1 - Z2) <= 10)
                        animalsSmelled.AddLast(animal);
                }
            }
            i++;
            animal = animals.Find(i);
            if (animalsSmelled.Find(animal) != null)
                animalSmelled = animalsSmelled.Find(animal).data;
        }

        i = 0;
        animalSmelled = animalsSmelled.Find(i);
        while (i < animalsSmelled.GetCount())
        {
            if (animals.Find(animalSmelled) == null)
            {
                animalsSmelled.Swap(animalSmelled, firstanimalSmelled);
                animalsSmelled.DeleteFirst();
                firstanimalSmelled = animalsSmelled.Find(0);
            }
            i++;
            animalSmelled = animalsSmelled.Find(i);
        }

        return this.animalsSmelled;
    }

    // ToString to print out the properties of the birds
    public override string ToString()
    {
        string a = String.Format("ID: {0}\nName: {1}\nAge: {2}\nPosition: {3}\n",
            Id, Name, Age, Pos.ToString());
        return a;
    }
}

// Cat:
// Subclass of Animal.
// Has another breed attribute.
// ToString method to display their properties.
public class Cat : Animal
{
    // Attributes
    private Breed breed;
    private DoublyLinkedList<Bird> birdsHeard = new DoublyLinkedList<Bird>();

    // Defining enum Breed
    public enum Breed
    {
        Abyssinian, American_Wirehair, Bengal, Himalayan, Ocicat, Serval
    }

    // Constructors
    public Cat() { }
    
    public Cat(int id, string name, double age, Position pos, Breed breed)
    {
        Id = id;
        Name = name;
        Age = age;
        Pos = pos;
        this.breed = breed;
    }

    // Method to randomize the cat breed
    public static Breed RandomBreed()
    {
        Breed[] breeds = (Breed[])Enum.GetValues(typeof(Breed));
        Random r = new Random();

        return breeds[r.Next(breeds.Length)];

    }

    // Check if the other animal is in range
    public bool InRange(Animal animal)
    {
        double X1 = animal.Pos.X;
        double Y1 = animal.Pos.Y;
        double Z1 = animal.Pos.Z;

        double X2 = Pos.X;
        double Y2 = Pos.Y;
        double Z2 = Pos.Z;

        if (Math.Abs(X1 - X2) <= 8 && Math.Abs(Y1 - Y2) <= 8 && Math.Abs(Z1 - Z2) <= 8)
            return true;
        else
            return false;
    }

    // Method to pass the animal to be moved to to the MoveTo in Position
    public void MoveTo(Animal animal)
    {
        Pos.MoveTo(animal, 16, 8);
    }

    // Method to eat the animal
    public void Eat(ref Bird bird)
    {
        Console.SetCursorPosition(0, 29); // Trying to keep everything under y = 30 since console window is broken for me
        Console.WriteLine($"B{bird.Id + 1} was eaten by C{Id + 1}  ");
        bird = null;
    }

    // Method to "hear" the birds and store them in a list
    public DoublyLinkedList<Bird> HearMovement(Bird bird, DoublyLinkedList<Animal> birds)
    {
        if (bird != null)
        {
            Node<Bird> birdFound = birdsHeard.Find(bird);
            Bird firstBirdHeard = birdsHeard.Find(0);

            if (bird.Speed > 5 && birdFound == null && FindDistance(bird) < 15)
            {
                birdsHeard.AddLast(bird);
            }
            else if (bird.Speed <= 5 && birdFound != null)
            {
                birdsHeard.Swap(firstBirdHeard, birdFound.data);
                birdsHeard.DeleteFirst();
            }
        }

        int i = 0;
        Bird birdHeard = birdsHeard.Find(i);
        Bird firstbirdHeard = (Bird)birdsHeard.Find(0);

        while (i < birdsHeard.GetCount())
        {
            if (birds.Find(birdHeard) == null)
            {
                birdsHeard.Swap(birdHeard, firstbirdHeard);
                birdsHeard.DeleteFirst();
                firstbirdHeard = birdsHeard.Find(0);
            }
            i++;
            birdHeard = birdsHeard.Find(i);
            firstbirdHeard = birdsHeard.Find(0);
        }

        return birdsHeard;
    }

    // ToString to print out the properties of the cats
    public override string ToString()
    {
        string breedString = breed.ToString();
        string newBreedString = breed.ToString();

        for(int i = 0; i < breedString.Length; i++)
        {
            if (breedString[i] == '_')
            {
                newBreedString = breedString.Substring(0, i) + " " + breedString.Substring(i+1);
            }
        }

        string b = String.Format("ID: {0}\nName: {1}\nAge: {2}\nPosition: {3}\nBreed: {4}\n",
            Id, Name, Age, Pos.ToString(), newBreedString);
        return b;
    }
}


// Snake:
// Subclass of Animal.
// Has another length and venomous attributes.
// ToString method to display their properties.
public class Snake : Animal
{
    // Attributes
    private double length;
    private bool venomous;
    private DoublyLinkedList<Bird> birdsHeard = new DoublyLinkedList<Bird>();

    // Properties
    public double Length
    {
        get { return length; }
        set { length = value; }
    }
    public bool Venomous
    {
        get { return venomous; }
        set { venomous = value; }
    }

    // Constructors
    public Snake() { }
    public Snake(int id, string name, double age, Position pos, double length, bool venomous)
    {
        Id = id;
        Name = name;
        Age = age;
        Pos = pos;
        Length = length;
        Venomous = venomous;
    }

    // Check if the other animal is in range
    public bool InRange(Animal animal)
    {
        double X1 = animal.Pos.X;
        double Y1 = animal.Pos.Y;
        double Z1 = animal.Pos.Z;

        double X2 = Pos.X;
        double Y2 = Pos.Y;
        double Z2 = Pos.Z;

        if (Math.Abs(X1 - X2) <= 3 && Math.Abs(Y1 - Y2) <= 3 && Math.Abs(Z1 - Z2) <= 3)
            return true;
        else
            return false;
    }

    // Method to pass the animal to be moved to to the MoveTo in Position
    public void MoveTo(Animal animal)
    {
        Pos.MoveTo(animal, 14, 3);
    }

    // Method to eat the animal
    public void Eat(ref Bird bird)
    {
        Console.SetCursorPosition(0, 29); // Trying to keep everything under y = 30 since console window is broken for me
        Console.WriteLine($"B{bird.Id + 1} was eaten by S{Id + 1}  ");
        bird = null;
    }

    // Method to "hear" the birds and store them in a list
    public DoublyLinkedList<Bird> HearMovement(Bird bird, DoublyLinkedList<Animal> birds)
    {
        if (bird != null)
        {
            Node<Bird> birdFound = birdsHeard.Find(bird);

            if (bird.Speed > 10 && birdFound == null && FindDistance(bird) < 10)
            {
                birdsHeard.AddLast(bird);
            }
            else if (bird.Speed <= 10 && birdFound != null)
            {
                birdsHeard.Swap(birdsHeard.Find(0), birdFound.data);
                birdsHeard.DeleteFirst();
            }
        }

        int i = 0;
        Bird birdHeard = birdsHeard.Find(i);
        Bird firstbirdHeard = (Bird)birdsHeard.Find(0);

        while (i < birdsHeard.GetCount())
        {
            if (birds.Find(birdHeard) == null)
            {
                birdsHeard.Swap(birdHeard, firstbirdHeard);
                birdsHeard.DeleteFirst();
                firstbirdHeard = birdsHeard.Find(0);
            }
            i++;
            birdHeard = birdsHeard.Find(i);
            firstbirdHeard = birdsHeard.Find(0);
        }

        return birdsHeard;
    }

    // ToString to print out the properties of the snakes
    public override string ToString()
    {
        string b = String.Format("ID: {0}\nName: {1}\nAge: {2}\nPosition: {3}" +
                                 "\nLength: {4}\nVenomous: {5}\n",
            Id, Name, Age, Pos.ToString(), Length, Venomous);
        return b;
    }
}

public class Bird: Animal
{
    // Constructors
    public Bird() { }
    public Bird(int id, string name, double age, Position pos)
    {
        Id = id;
        Name = name;
        Age = age;
        Pos = pos;
    }
}