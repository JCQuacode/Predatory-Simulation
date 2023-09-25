// Program Description: This program simulates the predatory relationship of 
//    cats and snakes with birds. The cats and snakes chase the birds around
//    until they are in range to be eaten. This also includes a hear and a smell.

using GenericLinkedList;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using static Cat;

public class Demo
{
    public static void Main()
    {
        Console.SetWindowSize(200, 31);

        // Text file names to store in arrays
        string filepath1 = @"..\..\..\cat_names.txt";
        string filepath2 = @"..\..\..\snake_names.txt";
        string filepath3 = @"..\..\..\bird_names.txt";

        // Arrays to store names
        string[] catNames = File.ReadAllLines(filepath1);
        string[] snakeNames = File.ReadAllLines(filepath2);
        string[] birdNames = File.ReadAllLines(filepath3);

        // New arrays to store shuffled names
        string[] newCatNames;
        string[] newSnakeNames;
        string[] newBirdNames;

        // Method to remove the numbering/bulleting/trailing whitespaces in the name list
        ExtractName(catNames);

        // Shufffled names stored in new arrays
        newCatNames = ShuffleArray(catNames);
        newSnakeNames = ShuffleArray(snakeNames);
        newBirdNames = ShuffleArray(birdNames);

        // Creation of cat objects
        Cat cat1 = new Cat(0, newCatNames[0], 5, RandomPosition(0, 30, true, true), RandomBreed());
        Cat cat2 = new Cat(1, newCatNames[1], 6.3, RandomPosition(0, 30, true, true), RandomBreed());

        // Creation of snake objects
        Snake snake1 = new Snake(0, newSnakeNames[0], 5, RandomPosition(0, 30, true, true), 5, true);
        Snake snake2 = new Snake(1, newSnakeNames[1], 5.5, RandomPosition(0, 30, true, true), 10, true);

        // Array lists for cats and snakes
        DoublyLinkedList<Animal> animals1 = new DoublyLinkedList<Animal>();
        DoublyLinkedList<Animal> animals2 = new DoublyLinkedList<Animal>();

        // Add cats to animals1
        animals1.AddFirst(cat1);
        animals1.AddFirst(cat2);

        // Add snakes to animals2
        animals2.AddLast(snake1);
        animals2.AddLast(snake2);

        // Merge animals1 and animals2 into animals list
        DoublyLinkedList<Animal> animals = animals1.Merge(animals2);

        // Array list for birds
        DoublyLinkedList<Animal> birds = new DoublyLinkedList<Animal>();

        // Creation of bird objects
        // Set 0 < x < 100
        // Set 0 < y < 70
        // Set 0 < z < 10
        Random r = new Random();
        for (int i = 0; i < 25; i++)
        {
            birds.AddLast(new Bird(i, newBirdNames[i], r.Next(1, 10), RandomPosition(0, 100, true)));
            Bird b = (Bird)birds.Find(i);
            b.RandomizePositionY(0, 30); // Changed upper limit from 70 to 30 since the window does not resize
            b.RandomizePositionZ(0, 10);
            b.Speed = r.Next(1, 14); // Starting speed when first spawned in
        }

        // Display the animals on the window
        DisplayAnimals(animals);

        // Display the birds on the window
        DisplayAnimals(birds);

        int j = 0;
        int animalCount = animals.GetCount();
        int birdCount = birds.GetCount();

        Animal animal;
        Bird bird;
        Bird nearestBird;
        Cat cat;
        Snake snake;

        int rounds = 0;
        Console.SetCursorPosition(0, 28);
        Console.Write("Rounds: " + rounds);

        // Simulation of the animals
        //Thread.Sleep(3000);
        while (birdCount > 0)
        {
            // Move the birds after each round/every animal has been checked
            if (j == animalCount)
            {
                j = 0;
                int bCount = birds.GetCount();
                for (int i = 0; i < bCount; i++)
                {
                    Bird b = (Bird)birds.Find(i);
                    RemoveAnimal(b);

                    Position bPrevPos = new Position(b.Pos.X, b.Pos.Y, b.Pos.Z);

                    b.MoveRandomX(-10, 10);
                    b.MoveRandomY(-10, 10);
                    b.MoveRandomZ(-2, 2);

                    b.Speed = Animal.DistanceBetween(b.Pos, bPrevPos);

                    DisplayAnimal(b, b.Id);
                }
                rounds++;
                Console.SetCursorPosition(0, 28);
                Console.Write("Rounds: " + rounds);
                //Thread.Sleep(3000);
            }

            animal = animals.Find(j);

            if (animal is Cat)
            {
                cat = (Cat)animal;
                bool hasAte = false;
                DoublyLinkedList<Animal> smelledAnimals = GetAnimalsSmelled(cat, animals, birds);

                for (int i = 0; i < birdCount; i++)
                {
                    bird = (Bird)birds.Find(i);
                    DoublyLinkedList<Bird> heardBirds1 = cat.HearMovement(bird, birds);
                    cat.FindDistance(bird);

                    // If the birds are in the range of the cat then it gets eaten
                    if (cat.InRange(bird) && !hasAte)
                    {
                        RemoveAnimal(bird);
                        DisplayAnimal(cat, cat.Id);

                        //Thread.Sleep(3000);

                        birds.Swap(bird, birds.Find(0));
                        birds.DeleteFirst();
                        cat.Eat(ref bird);
                        birdCount--;

                        heardBirds1 = cat.HearMovement(bird, birds);

                        hasAte = true;
                    }

                }
                // Find nearest bird for the cat if there aren't any in range
                if (!hasAte)
                {
                    nearestBird = cat.FindNearestBird(birds);

                    RemoveAnimal(cat);
                    cat.MoveTo(nearestBird);
                    DisplayAnimal(cat, cat.Id);
                }

                smelledAnimals = GetAnimalsSmelled(cat, animals, birds);
            }
            else if (animal is Snake)
            {
                snake = (Snake)animal;
                bool hasAte = false;
                DoublyLinkedList<Animal> smelledAnimals = GetAnimalsSmelled(snake, animals, birds);

                // If the birds are in the range of the snake then it gets eaten
                for (int i = 0; i < birdCount; i++)
                {
                    bird = (Bird)birds.Find(i);
                    DoublyLinkedList<Bird> heardBirds2 = snake.HearMovement(bird, birds);
                    //snake.FindDistance(bird);

                    if (snake.InRange(bird) && !hasAte)
                    {
                        RemoveAnimal(bird);
                        DisplayAnimal(snake, snake.Id);

                        //Thread.Sleep(3000);

                        birds.Swap(bird, birds.Find(0));
                        birds.DeleteFirst();
                        snake.Eat(ref bird);
                        birdCount--;

                        heardBirds2 = snake.HearMovement(bird, birds);

                        hasAte = true;
                    }

                }
                // Find nearest bird for the snake if there aren't any in range
                if (!hasAte)
                {
                    nearestBird = snake.FindNearestBird(birds);

                    RemoveAnimal(snake);
                    snake.MoveTo(nearestBird);
                    DisplayAnimal(snake, snake.Id);
                }
                smelledAnimals = GetAnimalsSmelled(snake, animals, birds);
            }
            j++;
        }

        Console.ReadLine();
    }

    // Method to remove the numbering/bulleting/trailing whitespaces
    public static void ExtractName(string[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = array[i].Substring(array[i].IndexOf(".") + 1);
            array[i] = array[i].Trim();
        }
    }

    // Method to shuffle the array
    public static string[] ShuffleArray(string[] array)
    {
        List<string> list = new List<string>(array.Length);
        List<string> newList = new List<string>(array.Length);

        string[] newArray = new string[array.Length];

        list = array.ToList();

        Random r = new Random();

        for (int i = list.Count; i > 0; i--)
        {
            int index = r.Next(i);

            newList.Add(list[index]);
            list.Remove(list[index]);

        }

        newArray = newList.ToArray();
        return newArray;
    }

    // Method to call the RandomPosition method in Position
    public static Position RandomPosition
        (int rangeStart, int rangeEnd, bool forX = false, bool forY = false, bool forZ = false)
    {
        return Position.RandomPosition
            (rangeStart, rangeEnd, forX, forY, forZ);
    }

    // Method to call the RandomBreed method in Cat
    public static Breed RandomBreed()
    {
        return Cat.RandomBreed();

    }

    // Method to display the animals in the array list on the window
    public static void DisplayAnimals(DoublyLinkedList<Animal> animals)
    {
        int catNo = 1;
        int snakeNo = 1;
        int birdNo = 1;

        int count = animals.GetCount();

        for (int i = 0; i < count; i++)
        {
            Animal a = animals.Find(i);
            Console.SetCursorPosition((int)a.Pos.X, (int)a.Pos.Y);

            if (a is Cat)
            {
                Console.Write($"C{catNo}");
                catNo++;
            }

            else if (a is Snake)
            {
                Console.Write($"S{snakeNo}");
                snakeNo++;
            }

            else if (a is Bird)
            {
                Console.Write($"B{birdNo}");
                birdNo++;
            }
        }
    }

    // Method to display the animal on the window
    public static void DisplayAnimal(Animal a, int num)
    {
        Console.SetCursorPosition((int)a.Pos.X, (int)a.Pos.Y);

        if (a is Cat)
        {
            Console.Write($"C{num}");
        }

        else if (a is Snake)
        {
            Console.Write($"S{num}");
        }

        else if (a is Bird)
        {
            Console.Write($"B{num}");
        }
    }

    // Method to stop displaying the animal on the window
    // Method to display the animal on the window
    public static void RemoveAnimal(Animal a)
    {
        Console.SetCursorPosition((int)a.Pos.X, (int)a.Pos.Y);

        if (a is Bird)
        {
            Console.Write("   ");
        }

        else if (a is Cat)
        {
            Console.Write("   ");
        }

        else if (a is Snake)
        {
            Console.Write("   ");
        }
    }

    // Method to display the birds in the array list on the window
    public static void DisplayBirds(DoublyLinkedList<Bird> birds)
    {
        int count = birds.GetCount();
        for (int i = 0; i < count; i++)
        {
            Bird a = birds.Find(i);
            Console.SetCursorPosition((int)a.Pos.X, (int)a.Pos.Y);

            Console.Write($"B{i + 1}");
        }
    }

    // Method to move the animals randomly

    // Method to find the position of the cat on the window
    public static void FindCatPosition(Cat cat)
    {
        Console.SetCursorPosition((int)cat.Pos.X, (int)cat.Pos.Y);
        Console.Write("C");
    }

    // Method to find the position of the snake on the window
    public static void FindSnakePosition(Snake snake)
    {
        Console.SetCursorPosition((int)snake.Pos.X, (int)snake.Pos.Y);
        Console.Write("S");
    }

    // Method to return a list of the animals smelled
    public static DoublyLinkedList<Animal> GetAnimalsSmelled(Animal animal, DoublyLinkedList<Animal> animals, DoublyLinkedList<Animal> birds)
    {
        Snake snake;
        Cat cat;
        DoublyLinkedList<Animal> smelledAnimals = null;

        if (animal is Snake)
        {
            snake = (Snake)animal;
            smelledAnimals = snake.Smell(animals);
            snake.Smell(birds);
        }
        else if (animal is Cat)
        {
            cat = (Cat)animal;
            smelledAnimals = cat.Smell(animals);
            cat.Smell(birds);
        }

        return smelledAnimals;
    }
}


// Testing Code:

// Question 2:

/*int numElements = 100000;

// Singly linked lists of sortable objects
SinglyLinkedList<SortableObject> slist1 = new SinglyLinkedList<SortableObject>();
for (int i = 0; i < numElements; i++)
{
    slist1.AddLast(new SortableObject(Util.GetRandom()));
}
SinglyLinkedList<SortableObject> slist2 = new SinglyLinkedList<SortableObject>();
for (int i = 0; i < numElements; i++)
{
    slist2.AddLast(new SortableObject(Util.GetRandom()));
}

// Doubly linked lists of sortable objects
DoublyLinkedList<SortableObject> dlist1 = new DoublyLinkedList<SortableObject>();
for (int i = 0; i < numElements; i++)
{
    dlist1.AddLast(new SortableObject(Util.GetRandom()));
}
DoublyLinkedList<SortableObject> dlist2 = new DoublyLinkedList<SortableObject>();
for (int i = 0; i < numElements; i++)
{
    dlist2.AddLast(new SortableObject(Util.GetRandom()));
}

// Array lists of sortable objects
ArrayList<SortableObject> alist1 = new ArrayList<SortableObject>();
for (int i = 0; i < numElements; i++)
{
    alist1.AddLast(new SortableObject(Util.GetRandom()));
}
ArrayList<SortableObject> alist2 = new ArrayList<SortableObject>();
for (int i = 0; i < numElements; i++)
{
    alist2.AddLast(new SortableObject(Util.GetRandom()));
}

// Check the timings
Stopwatch stopWatch = new Stopwatch();
stopWatch.Start();

Console.WriteLine();
slist1.Merge(slist2);
dlist1.Merge(dlist2);
ArrayList<SortableObject>.Merge(alist1, alist2);
Console.WriteLine();

stopWatch.Stop();

// Get the elapsed time as a TimeSpan value.
TimeSpan ts = stopWatch.Elapsed;
long tsticks = stopWatch.ElapsedTicks;
Console.WriteLine();

// Format and display the TimeSpan value.
string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
Console.WriteLine("RunTime " + elapsedTime);
Console.WriteLine("Ticks " + tsticks);*/

// Question 3: