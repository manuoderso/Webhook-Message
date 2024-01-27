using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        while (true)
        {
            Console.Write("Enter the Discord webhook: ");
            string webhookUrl = Console.ReadLine();

            Console.Write("Enter the message: ");
            string message = Console.ReadLine();

            Console.Write("you want to send multiple messages? (1 for Yes, 2 for No): ");
            int choice = int.Parse(Console.ReadLine());

            if (choice == 1)
            {
                Console.Write("How many Times do you want to send the message?: ");
                int numberOfTimes = int.Parse(Console.ReadLine());

                for (int i = 0; i < numberOfTimes; i++)
                {
                    await SendMessage(webhookUrl, message);
                }
            }
            else if (choice == 2)
            {
                await SendMessage(webhookUrl, message);
            }
            else
            {
                Console.WriteLine("Invalid choice :( Exiting...");
            }

            Console.Write("Do you want to send another message? (1: Yes, 2: No): ");
            int repeatChoice = int.Parse(Console.ReadLine());

            if (repeatChoice != 1)
            {
                break;
            }
        }
    }

    static async Task SendMessage(string webhookUrl, string message)
    {
        string jsonPayload = $"{{ \"content\": \"{message}\" }}";

        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.PostAsync(webhookUrl, new StringContent(jsonPayload, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Message sent successfully!");
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }
    }
}
