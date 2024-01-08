using Client.MessageHandlers.MessageSenders;
using System.Net.Sockets;

namespace Client.TextInputHandlers
{
    public class TextInputHandler
    {
        NetworkStream Stream;
        IMessageSender MessageSender;
        private Task? InputListeningTask;
        private CancellationTokenSource Cts;

        public TextInputHandler(NetworkStream stream, IMessageSender messageSender)
        {
            Stream = stream;
            MessageSender = messageSender;
            Cts = new CancellationTokenSource();
        }

        public void StartListening()
        {
            InputListeningTask = Task.Run(ListenForUserInput, Cts.Token);
        }

        public void StopListening()
        {
            if (InputListeningTask != null && !InputListeningTask.IsCompleted)
            {
                Cts.Cancel();
                InputListeningTask.Wait(); // Optionally wait for the task to complete
            }
        }

        private void ListenForUserInput()
        {
            try
            {
                string? textInput = string.Empty;

                while (!Cts.Token.IsCancellationRequested)
                {
                    textInput = Console.ReadLine();

                    if (textInput == string.Empty)
                    {
                        break;
                    }
                    // Check for a cancellation request after reading the line
                    if (Cts.Token.IsCancellationRequested)
                    {
                        break;
                    }

                    MessageSender.SendMsg(textInput, Stream); // Call to your SendMessage method
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception here: 0 " + ex);
            }
        }
    }
}
