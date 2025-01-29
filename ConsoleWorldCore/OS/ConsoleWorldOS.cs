using ColdWind.AdvancedConsoleManager;
using System;

namespace ColdWind.ConsoleWorldCore.OS;

public class ConsoleWorldOS
{
    public void Start()
    {
        var mainConsole = new AdvancedConsole();
        Console.WriteLine("ConsoleWorldOS started");

        TestAdvancedConsole2(mainConsole);
        Thread.Sleep(400);
        MoveRight(mainConsole);
        Thread.Sleep(200);
        var mainConsole2 = new AdvancedConsole();
        TestAdvancedConsole2(mainConsole2);
        Thread.Sleep(400);
        MoveLeft(mainConsole2);
        Thread.Sleep(200);
        var mainConsole3 = new AdvancedConsole();
        TestAdvancedConsole2(mainConsole3);
        Thread.Sleep(300);
        //ChangeSize(mainConsole3);
        //Vector2Int position = mainConsole3.GetPosition();
        //mainConsole.MoveTo(mainConsole.GetPosition().X, position.Y);
        //Thread.Sleep(100);
        //mainConsole2.MoveTo(mainConsole2.GetPosition().X, position.Y);
        //Thread.Sleep(100);
        //MoveToCenterBoth(mainConsole, mainConsole2, mainConsole3);

        MoveUp(mainConsole);
        mainConsole.Destroy();

        MoveRightWithScale(mainConsole2);
        mainConsole2.Destroy();

        Thread.Sleep(200);
        mainConsole3.Title = ":)))))";
        Thread.Sleep(400);

        Compress(mainConsole3);
        mainConsole3.Destroy();

        Console.WriteLine("Thanks for watching");
        Thread.Sleep(2000);
    }

    private void Compress(AdvancedConsole console)
    {
        for (int i = 75; i > 50; i -= 1)
        {
            console.SetSize(i, 25);
            console.MoveToCenter();
            Thread.Sleep(20);
        }
        console.MoveToCenter();

        for (int i = 50, j = 25; j > 2; i -= 2, j -= 1)
        {
            console.SetSize(Math.Max(i, 15), j);
            console.MoveToCenter();
            Thread.Sleep(20);
        }
    }

    private void MoveToCenterBoth(AdvancedConsole console1, AdvancedConsole console2, AdvancedConsole centerConsole)
    {
        int window1StartX = console1.GetPosition().X;
        int window1StartPositionY = console1.GetPosition().Y;

        int window2StartX = console2.GetPosition().X;
        int startWidth = 75;
        int minWidth = 50;
        int valueWidth = startWidth;

        Vector2Int centerPosition = centerConsole.GetPosition();

        for (int i = window1StartX, j = window2StartX; i > centerPosition.X - 100 || j < centerPosition.X; i -= 1, j += 1)
        {
            if (i < window1StartX - 500 && valueWidth != minWidth)
            {
                valueWidth -= 1;

                if (valueWidth < minWidth)
                    valueWidth = minWidth;

                console2.SetSize(valueWidth, 25);
            }
            
            //console1.MoveTo(new Vector2Int(j, window1StartPositionY));
            
            console2.MoveTo(new Vector2Int(j, window1StartPositionY));

            Thread.Sleep(2);
        }
    }

    private void ChangeSize(AdvancedConsole console)
    {
        for (int i = 75; i > 50 ; i -= 1)
        {
            console.SetSize(i, 25);
            console.MoveToCenter();
            Thread.Sleep(10);
        }
        console.MoveToCenter();
    }

    private void MoveRight(AdvancedConsole console)
    {
        int windowStartPositionX = console.GetPosition().X;
        int windowStartPositionY = console.GetPosition().Y;

        for (int i = windowStartPositionX; i < windowStartPositionX + 610; i += 4)
        {
            console.MoveTo(new Vector2Int(i, console.GetPosition().Y));
            Thread.Sleep(1);
        }
    }

    private void MoveUp(AdvancedConsole console)
    {
        int windowStartPositionX = console.GetPosition().X;
        int windowStartPositionY = console.GetPosition().Y;

        for (int i = windowStartPositionY; i > windowStartPositionY - 800; i -= 25)
        {
            console.MoveTo(new Vector2Int(console.GetPosition().X, i));
            Thread.Sleep(300);
        }
    }

    private void MoveRightWithScale(AdvancedConsole console)
    {
        int windowStartPositionX = console.GetPosition().X;
        int windowStartPositionY = console.GetPosition().Y;

        int startWidth = 75;
        int minWidth = 50;
        int valueWidth = startWidth;

        for (int i = windowStartPositionX; i < windowStartPositionX + 500; i += 3)
        {
            console.MoveTo(new Vector2Int(i, console.GetPosition().Y));
            Thread.Sleep(10);
        }

        console.SetSize(minWidth, 25);

        for (int i = windowStartPositionX + 500; i < windowStartPositionX + 700; i += 3)
        {
            console.MoveTo(new Vector2Int(i, console.GetPosition().Y));
            Thread.Sleep(10);
        }
    }

    private void MoveLeft(AdvancedConsole console)
    {
        int windowStartPositionX = console.GetPosition().X;
        int windowStartPositionY = console.GetPosition().Y;

        for (int i = windowStartPositionX; i > windowStartPositionX - 610; i -= 4)
        {
            console.MoveTo(new Vector2Int(i, console.GetPosition().Y));
            Thread.Sleep(1);
        }
    }

    private void TestAdvancedConsole2(AdvancedConsole console)
    {
        int j = 15;
        int i = 2;
        console.SetSize(j, i);
        console.MoveToCenter();

        int maxWidth = 75;
        int maxHeight = 25;


        for (; i < maxHeight || j < maxWidth;)
        {
            if (i != maxHeight)
                i += 3;

            if (i > 23 && j != maxWidth)
                j += 5;

            if (j > maxWidth)
                j = maxWidth;

            if (i > maxHeight)
                i = maxHeight;

            console.SetSize(j, i);
            console.MoveToCenter();
            Thread.Sleep(1);
        }
        
    }

    private void TestAdvancedConsole(AdvancedConsole console)
    {
        Console.WriteLine("Advanced Console test 1 started");

        console.MoveToCenter();

        Thread.Sleep(2000);
        console.MoveTo(200, 200);

        Thread.Sleep(1000);
        console.Title = "Console :))))";
        console.SetSize(75, 20);
        console.MoveTo(new Vector2Int(1200, 200));

        Console.WriteLine("---Position:" + console.GetPosition());

        int windowStartPositionX = console.GetPosition().X;
        int windowStartPositionY = console.GetPosition().Y;

        for (int i = windowStartPositionX; i > windowStartPositionX - 1000; i -= 3)
        {
            console.MoveTo(new Vector2Int(i, console.GetPosition().Y));
            Thread.Sleep(1);
        }

        Console.WriteLine("---Position:" + console.GetPosition());

        Thread.Sleep(100);
        Console.WriteLine("Advanced Console test 1 finished");
    }
}
