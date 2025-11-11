using System;

class Program
{
    static void Main()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        DisplayBanner();
        Console.ResetColor();

        // Difficulty selection
        int maxNumber = SelectDifficulty();
        
        // Create random number generator
        Random random = new Random();
        int secretNumber = random.Next(1, maxNumber + 1);
        int playerGuess = 0;
        int attempts = 0;
        int maxAttempts = CalculateMaxAttempts(maxNumber);

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\n🎯 I'm thinking of a number between 1 and {maxNumber}.");
        Console.WriteLine($"⏱️  You have {maxAttempts} attempts to guess it!");
        Console.WriteLine("========================================\n");
        Console.ResetColor();

        // Main game loop
        while (playerGuess != secretNumber && attempts < maxAttempts)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"📝 Attempt {attempts + 1}/{maxAttempts} - Enter your guess: ");
            Console.ResetColor();
            
            string? input = Console.ReadLine();
            attempts++;

            // Validate input
            if (!int.TryParse(input, out playerGuess))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                DisplayErrorArt();
                Console.WriteLine("❌ Invalid input! Please enter a number.");
                Console.ResetColor();
                attempts--;
                continue;
            }

            // Check if guess is out of range
            if (playerGuess < 1 || playerGuess > maxNumber)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"❌ Please guess a number between 1 and {maxNumber}!");
                Console.ResetColor();
                attempts--;
                continue;
            }

            // Calculate how close the guess is
            int difference = Math.Abs(playerGuess - secretNumber);
            
            // Provide hints with visual feedback
            if (playerGuess < secretNumber)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("📉 Too low! ");
                DisplayHeatMeter(difference, maxNumber);
            }
            else if (playerGuess > secretNumber)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("📈 Too high! ");
                DisplayHeatMeter(difference, maxNumber);
            }
            else
            {
                // Victory!
                Console.Clear();
                DisplayVictoryArt();
                DisplayPerformanceRating(attempts, maxAttempts);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"✨ The number was: {secretNumber}");
                Console.WriteLine($"🎯 You took {attempts} attempt(s).");
                Console.WriteLine("========================================");
                Console.ResetColor();
                
                // Play again?
                if (AskPlayAgain())
                {
                    Console.Clear();
                    Main();
                    return;
                }
            }
            
            Console.ResetColor();
        }

        // Out of attempts
        if (attempts >= maxAttempts && playerGuess != secretNumber)
        {
            Console.Clear();
            DisplayGameOverArt();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"💔 The secret number was: {secretNumber}");
            Console.WriteLine("Better luck next time!");
            Console.ResetColor();
            
            if (AskPlayAgain())
            {
                Console.Clear();
                Main();
            }
        }
    }

    static void DisplayBanner()
    {
        Console.WriteLine(@"
╔═══════════════════════════════════════════════════════════╗
║                                                           ║
║    ███╗   ██╗██╗   ██╗███╗   ███╗██████╗ ███████╗██████╗ ║
║    ████╗  ██║██║   ██║████╗ ████║██╔══██╗██╔════╝██╔══██╗║
║    ██╔██╗ ██║██║   ██║██╔████╔██║██████╔╝█████╗  ██████╔╝║
║    ██║╚██╗██║██║   ██║██║╚██╔╝██║██╔══██╗██╔══╝  ██╔══██╗║
║    ██║ ╚████║╚██████╔╝██║ ╚═╝ ██║██████╔╝███████╗██║  ██║║
║    ╚═╝  ╚═══╝ ╚═════╝ ╚═╝     ╚═╝╚═════╝ ╚══════╝╚═╝  ╚═╝║
║                                                           ║
║              🎮 G U E S S I N G   G A M E 🎮             ║
║                                                           ║
╚═══════════════════════════════════════════════════════════╝
");
    }

    static int SelectDifficulty()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n🎚️  Select Difficulty Level:");
        Console.WriteLine("================================");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("1️⃣  Easy   (1-50)   - Perfect for beginners");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("2️⃣  Medium (1-100)  - Classic challenge");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("3️⃣  Hard   (1-500)  - For the brave");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("4️⃣  Insane (1-1000) - Godlike difficulty");
        Console.ResetColor();
        Console.Write("\n👉 Your choice (1-4): ");

        string? choice = Console.ReadLine();
        return choice switch
        {
            "1" => 50,
            "2" => 100,
            "3" => 500,
            "4" => 1000,
            _ => 100
        };
    }

    static int CalculateMaxAttempts(int maxNumber)
    {
        return maxNumber switch
        {
            50 => 8,
            100 => 10,
            500 => 15,
            1000 => 20,
            _ => 10
        };
    }

    static void DisplayHeatMeter(int difference, int maxNumber)
    {
        double percentage = (double)difference / maxNumber * 100;
        
        if (percentage < 5)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("🔥🔥🔥 ON FIRE! You're extremely close!");
        }
        else if (percentage < 10)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("🌡️  Getting hot! Very close!");
        }
        else if (percentage < 20)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("❄️  Warm... Getting there!");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("🧊 Cold... Keep trying!");
        }
    }

    static void DisplayVictoryArt()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(@"
        ★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★
        
            🎉🎊  C O N G R A T U L A T I O N S  🎊🎉
            
                    YOU GUESSED IT!
                    
                      \ 😄 /
                       \|/
                        |
                       / \
                       
        ★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★
");
        Console.ResetColor();
    }

    static void DisplayGameOverArt()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(@"
        ╔═════════════════════════════════════╗
        ║                                     ║
        ║         💀 GAME OVER 💀             ║
        ║                                     ║
        ║       You ran out of attempts!      ║
        ║                                     ║
        ║            ( ︶︿︶)                  ║
        ║                                     ║
        ╚═════════════════════════════════════╝
");
        Console.ResetColor();
    }

    static void DisplayErrorArt()
    {
        Console.WriteLine(@"
        ⚠️  Oops! That's not right!
");
    }

    static void DisplayPerformanceRating(int attempts, int maxAttempts)
    {
        double ratio = (double)attempts / maxAttempts;
        
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n📊 Performance Rating:");
        
        if (ratio <= 0.3)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(@"
        ⭐⭐⭐⭐⭐ LEGENDARY!
        You're a mind reader! 🧠✨
");
        }
        else if (ratio <= 0.5)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(@"
        ⭐⭐⭐⭐ EXCELLENT!
        Outstanding performance! 🏆
");
        }
        else if (ratio <= 0.7)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(@"
        ⭐⭐⭐ GREAT!
        Well done! 👍
");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(@"
        ⭐⭐ GOOD!
        You made it! 😊
");
        }
        Console.ResetColor();
    }

    static bool AskPlayAgain()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("\n🔄 Play again? (Y/N): ");
        Console.ResetColor();
        
        string? response = Console.ReadLine();
        return response?.ToUpper() == "Y";
    }
}