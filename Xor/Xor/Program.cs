using System;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xor
{
    class Program
    {
        static char[] key = new char[] {'`','^','_','*','?','%','~','<','Z','&' };
        static int i = 0;
        //static int keyAscii = Convert.ToInt32(key);
        [STAThread]
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("1. Chose a text file to crypt.\n2. Chose a text file to decrypt.\n3. Encrypt an executable.\n4. Decrypt an Executable.\n5. Exit\nEnter Choice: ");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch(choice)
            {
                case 1:
                    encryptTextFile();
                    break;
                case 2:
                    decryptTextFile();
                    break;
                case 3:
                    encryptExecutable();
                    break;
                case 4:
                    decryptExecutable();
                    break;
                case 5:
                    Application.Exit();
                    break;
            }
        }
        
        static void encryptExecutable()
        {
            i = 0;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("[*]Chose a file to crypt...");
            Thread.Sleep(10);
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            string location = openFileDialog.InitialDirectory + openFileDialog.FileName;
            Console.WriteLine("File Chosen: " + openFileDialog.FileName);
            Thread.Sleep(500);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("[*]Encrypting your file...");
            Console.ForegroundColor = ConsoleColor.Blue;
            string content = File.ReadAllText(location);          
            string encryptedData = encryptSub(content, 2, key[i]);          
            string outputFolder = @"C:\EncryptedFiles\";
            //Thread.Sleep(3000);
            Console.WriteLine("[*]Creating Directory for Output File...");
            Thread.Sleep(500);
            Directory.CreateDirectory(outputFolder);
            Console.WriteLine("[*]Directory created, output folder: " + outputFolder);
            Thread.Sleep(500);
            File.AppendAllText(outputFolder + "Encrypted.txt", encryptedData);
            string extension = Path.ChangeExtension(outputFolder + "Encrypted.txt", ".exe");
            File.Move(outputFolder + "Encrypted.txt", extension);
            Console.WriteLine("[*]Encryption Completed. Refer to output folder to retreive encrypted file.");
            ask();
        }
        static void decryptExecutable()
        {
            i = 0;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("[*]Chose a file to crypt...");
            Thread.Sleep(10);
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            string location = openFileDialog.InitialDirectory + openFileDialog.FileName;
            Console.WriteLine("File Chosen: " + openFileDialog.FileName);
            Thread.Sleep(500);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("[*]Decrypting your file...");
            Console.ForegroundColor = ConsoleColor.Blue;
            string content = File.ReadAllText(location);
            string decryptedData = decryptSub(content, 2, key[i]);
            string outputFolder = @"C:\DecryptedFiles\";
            Console.WriteLine("[*]Creating Directory for Output File...");
            Thread.Sleep(500);
            Directory.CreateDirectory(outputFolder);
            Console.WriteLine("[*]Directory created, output folder: " + outputFolder);
            Thread.Sleep(500);
            File.AppendAllText(outputFolder + "Decrypted.txt", decryptedData);
            string extension = Path.ChangeExtension(outputFolder + "Decrypted.txt", ".exe");
            File.Move(outputFolder + "Decrypted.txt", extension);
            Console.WriteLine("[*]Decryption Completed. Refer to output folder to retreive encrypted file.");
            ask();
        }
        static void encryptTextFile()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("[*]Chose a file to crypt...");
            Thread.Sleep(10);
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            string location = openFileDialog.InitialDirectory + openFileDialog.FileName;
            Console.WriteLine("File Chosen: " + openFileDialog.FileName);
            string content = File.ReadAllText(location);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[*]Enter the Number of layers of Encryption(Max 10): ");
            Console.ForegroundColor = ConsoleColor.Blue;
            int layers = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("[*]Encrypting your file...");
            string encryptedData = encryptSub(content, layers, key[i]);                       
            string outputFolder = @"C:\EncryptedFiles\" ;
            Thread.Sleep(3000);
            Console.WriteLine("[*]Creating Directory for Output File...");
            Thread.Sleep(500);
            Directory.CreateDirectory(outputFolder);
            Console.WriteLine("[*]Directory created, output folder: " + outputFolder);
            Thread.Sleep(500);
            File.AppendAllText(outputFolder + "Encrypted.txt", encryptedData);
            Console.WriteLine("[*]Encryption Completed. Refer to output folder to retreive encrypted file.");
            ask();
        }
        static string encryptSub(string content, int layers, char KEY)
        {
            string completedOutput = null;
            
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Key used for encryption: " + Convert.ToString(KEY));
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            foreach (char character in content)
            {
                int encryptedAscii = character ^ KEY;
                char convertedChar = Convert.ToChar(encryptedAscii);
                string convertedCharString = Convert.ToString(convertedChar);
                completedOutput += convertedCharString;
            }
            if (layers == 1)
            {
                return completedOutput;
            }
            
            return encryptSub(completedOutput, --layers, key[++i]);
            
        }

        static void decryptTextFile()
        {
            i = 0;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("[*]Chose a file to crypt...");            
            Thread.Sleep(10);
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            string location = openFileDialog.InitialDirectory + openFileDialog.FileName;
            Console.WriteLine("File Chosen: " + openFileDialog.FileName);
            string content = File.ReadAllText(location);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[*]Enter the Number of layers of Decryption(Max 10): ");
            Console.ForegroundColor = ConsoleColor.Blue;
            int layers = Convert.ToInt32(Console.ReadLine());
            string decryptedData = decryptSub(content, layers, key[i+(layers-1)]);
            Console.WriteLine("[*]Decrypting your file...");
            Thread.Sleep(3000);
            string outputFolder = @"C:\DecryptedFiles\";
            Console.WriteLine("[*]Creating Directory for Output File...");
            Thread.Sleep(500);
            Directory.CreateDirectory(outputFolder);
            Console.WriteLine("[*]Directory created, output folder: " + outputFolder);
            Thread.Sleep(500);
            File.AppendAllText(outputFolder + "Decrypted.txt", decryptedData);
            Console.WriteLine("[*]Decryption Completed. Refer to output folder to retreive decrypted file.");
            ask();
            
        }

        static string decryptSub(string content, int layers, char KEY)
        {
            string completedOutput = null;
            
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Key used for decryption: " + Convert.ToString(KEY));
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            foreach (char character in content)
            {
                int decryptedAscii = character ^ KEY;
                char convertedChar = Convert.ToChar(decryptedAscii);
                string convertedCharString = Convert.ToString(convertedChar);
                completedOutput += convertedCharString;
            }
            if (layers == 1)
            {
                return completedOutput;
            }
            int decreament = i + (layers - 1);
            return decryptSub(completedOutput, --layers, key[--decreament]);
        }
        static void ask()
        {
            Console.Write("\nWould you like to continue?(y/n): ");
            char choice = Convert.ToChar(Console.ReadLine());
            if (choice == 'y' || choice == 'Y')
            {
                Main(null);
            }
            else if (choice == 'n' || choice == 'N')
            {
                
                Console.WriteLine("[*]Exiting...");
                Thread.Sleep(1000);
                Application.Exit();
            }
            else
            {
                Console.WriteLine("Invalid request!");
                ask();
            }
        }
    }
}
