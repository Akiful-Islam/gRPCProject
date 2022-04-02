using Grpc.Net.Client;
using GrpcServer;
using System;
using System.Threading.Tasks;

namespace GrpcClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Would you like to Sign Up or Log In?");
            Console.WriteLine("1. Sign-Up                2. Sign-In");
            var input = Console.ReadLine();

            if (String.Equals(input, "1"))
            {
                await SignUp();
            }
            else if (String.Equals(input, "2"))
            {
                await SignIn();
            }
        }
        static async Task SignUp()
        {
            Console.Write("Enter An Email Address: ");
            var email = Console.ReadLine();
            Console.Write("Enter A Username: ");
            var username = Console.ReadLine();
            Console.Write("Enter A Password: ");
            var password = Console.ReadLine();


            var requestInput = new SignUpRequest { Email = email, UserName = username, Password = password };

            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new UserActivity.UserActivityClient(channel);

            var reply = await client.SignUpAsync(requestInput);

            Console.WriteLine(reply.Message);
        }
        static async Task SignIn()
        {
            Console.Write("Enter Your Username: ");
            var username = Console.ReadLine();
            Console.Write("Enter Password: ");
            var password = Console.ReadLine();


            var requestInput = new SignInRequest { UserName = username, Password = password };

            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new UserActivity.UserActivityClient(channel);

            var reply = await client.SignInAsync(requestInput);

            Console.WriteLine(reply.Message);
        }
    }
}
