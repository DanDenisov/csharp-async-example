using System;
using System.Threading.Tasks;

namespace AsyncTest
{
    // a class, emulating 3D-model loading
    class ModelLoader
    {
        // model unique ID
        static int modelID;

        // entry point
        static void Main()
        {
            Console.WriteLine("Entry point...");

            bool loadStuff = true;

            // the main cycle (usually rendering)
            while (true)
            {
                // start loading model if the spacebar has been pressed
                if (loadStuff)
                {
                    if (Console.ReadKey().Key == ConsoleKey.Spacebar)
                    {
                        RunTask();
                        loadStuff = false;
                    }
                }

                // if the model's ID has changed, the model is loaded and we may exit the program
                if (modelID != 0)
                {
                    Console.WriteLine($"The model has been loaded! Its ID is {modelID}. Bye!");
                    break;
                }
            }

            Console.ReadLine();
        }

        // the main async procedure
        // NOTE: this is the only type of async methods ("async void") that can be called from a SYNCHRONOUS code
        // any other async procedure, that RETURNS something, needs to be awaited; synchronous code is not able to provide that!
        static async void RunTask()
        {
            // await for the result of the model loading procedure;
            // until that moment, context in this method is blocked!
            modelID = await RunTaskAsync();
        }

        // async procedure of loading a model
        static async Task<int> RunTaskAsync()
        {
            Console.WriteLine("Starting async task...");

            // return the result of completing a loading task AFTER it has completed;
            // until that moment, context in this method is blocked!
            return await Task.Run(() =>
            {
                for (int i = 0; i < 1000; i++)
                {
                    Console.WriteLine($"Executing operation #{i}");
                }

                return new Random().Next();
            });
        }
    }
}
