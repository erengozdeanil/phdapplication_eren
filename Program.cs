using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.IO.Pipes;

class Test
{
    static int CsTest()
    {
        int rc = 0;
  
        using (NamedPipeClientStream pipeClient =
           new NamedPipeClientStream(".", "my_pipe", PipeDirection.InOut))
        {
            // Connect to the pipe or wait until the pipe is available
            Console.Write("Connecting to pipe...");
            pipeClient.Connect();

            Console.WriteLine("Connected to pipe.");
            Console.WriteLine("There are currently {0} pipe server open.",
                pipeClient.NumberOfServerInstances);

            StreamWriter writer = new StreamWriter(pipeClient);
            StreamReader reader = new StreamReader(pipeClient);
            while (true)
            {
                try
                {
                    writer.AutoFlush = true;
                    // Write message back
                    int[,] my_array = new int[4,2] { { 1, 2 }, { 3, 4 }, { 5, 6 }, { 7, 8 } };
                    string my_message = new string("I cannot transfer 2d arrays yet");
                    writer.WriteLine(my_message);


                    // Read Message
                    string message = reader.ReadLine();
                    // int message = (BitConverter.ToInt32(Byte[message], Int32); //doesnt work
                    Console.WriteLine("Received from python : {0}", message);
                    // Console.ReadLine();
                }
                catch (EndOfStreamException)
                {
                    break;                    // When client disconnects
                }
            }

        }

        return rc;
    }

    static void Main(string[] args)
    {
        int result = CsTest();
        // Console.WriteLine($"\nC# test result: {result}");
    }
}