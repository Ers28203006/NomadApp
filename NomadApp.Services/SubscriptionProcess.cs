using NomadApp.DataAccess;
using NomadApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NomadApp.Services
{
    public class SubscriptionProcess
    {
        public static void Input()
        {
            Console.WriteLine("Добро пожаловать на страницы нашего сайта Nomad, для совершения подписки Вам необходимо войти в систему.\n" +
                "Для входа в систему введите email:");
            string email = Console.ReadLine();
            Console.WriteLine("Введите пароль:");
            string password = Console.ReadLine();
            Autorization(email, password);
        }
        static void Autorization(string email, string password)
        {
            Client client = new Client();
            using (var context = new NomadsContext())
            {
                bool isEntry = false;
                foreach (var u in context.Clients.ToList())
                {
                    if (u.Email == email)
                    {
                        isEntry = true;
                        break;
                    }
                }

                if (isEntry == false)
                {
                    Console.WriteLine("Этой учетной записи в системе нет. \n" +
                        "Хотите зарегистрироваться?\n1.- да\n2.- нет");
                    int choice = 0;
                    while (choice == 0)
                    {
                        int.TryParse(Console.ReadLine(), out choice);
                        if (choice <= 0 || choice > 2) choice = 0;
                        else break;
                    }

                    switch (choice)
                    {
                        case 1:
                            Registration(ref client, email, password);
                            Console.WriteLine("Добро пожаловать в систему!");
                            Purchase(client);

                            break;
                        case 2:
                            Console.WriteLine("До свидания!");
                            break;
                    }
                }

                else
                {
                    client = context.Clients.FirstOrDefault(u => u.Email == email);
                    if (client.Password == password)
                    {
                        Console.WriteLine("Добро пожаловать в систему!");
                    }
                    else
                    {
                        while (true)
                        {
                            Console.Clear();
                            Console.WriteLine("Не верно введен пароль попробуйте снова: ");

                            password = Console.ReadLine();
                            if (client.Password == password)
                            {
                                Console.WriteLine("Добро пожаловать в систему!");
                                break;
                            }
                        }

                    }

                   
                    Purchase(client);

                    Console.WriteLine("Желаете отменить подписку?\n" +
                       "1. да\n" +
                       "2. нет");
                    int choice = 0;
                    while (choice == 0)
                    {
                        int.TryParse(Console.ReadLine(), out choice);
                        if (choice <= 0 || choice > 2) choice = 0;
                        else break;
                    }
                    switch (choice)
                    {
                        case 1:
                            CancelSubscription();
                            break;
                        case 2:
                            Console.WriteLine("До свидания!");
                            break;
                    }
                   
                }

            }

        }
        static void Registration(ref Client client, string email, string password)
        {
            using (var context = new NomadsContext())
            {
                client = new Client { Email = email, Password = password };
                context.Clients.Add(client);
                context.SaveChanges();

                client = context.Clients.FirstOrDefault(u => u.Email == email);
            }
        }

        static void ShowAssortimentsList()
        {
            using (var context = new NomadsContext())
            {
                Console.WriteLine("Ассортимент:");
                foreach (var p in context.Subscriptions.ToList())
                {
                    Console.WriteLine($"{p.Id}. Журнал {p.Name}: цена подписки - {p.Price}, период подписки - {p.Period}");
                }
            }
        }

        static void Purchase(Client client)
        {
            Console.Clear();
            ShowAssortimentsList();
            Console.WriteLine("*********************************\n" +
                "Добавьте желаемую подписку по её номеру: ");
            int choice = 0;

            Subscription market = new Subscription();

            using (var context = new NomadsContext())
            {
                while (choice == 0)
                {
                    int.TryParse(Console.ReadLine(), out choice);
                    if (choice < 0 || choice > context.Subscriptions.Count())
                    {
                        choice = 0;
                    }
                    else
                    {
                        market = context.Subscriptions.FirstOrDefault(m => m.Id == choice);
                        break;
                    }
                }


                SubscriptionRegistration basket = new SubscriptionRegistration
                {
                    ClientId = client.Id,
                    Client = client,
                    SubscriptionId = market.Id,
                    Subscription = market
                };

                context.SubscriptionRegistrations.Add(basket);
                context.SaveChanges();

                

                List<SubscriptionRegistration> baskets = new List<SubscriptionRegistration>();

                Console.WriteLine("Просмотр подписки: ");
                foreach (var b in context.SubscriptionRegistrations.ToList())
                {
                    Console.WriteLine($"Клиент: {b.Client.Email}," +
                        $" Журнал: {b.Subscription.Name}." +
                        $" Цена: {b.Subscription.Price}." +
                        $" Подписка на {b.Subscription.Period} месяцев.");
                    baskets.Add(b);
                }
            }
        }

        public static void CancelSubscription()
        {
            using (var context=new NomadsContext())
            {
                List<SubscriptionRegistration> baskets = new List<SubscriptionRegistration>();
                foreach (var b in context.SubscriptionRegistrations.ToList())
                    baskets.Add(b);
               string  email="";
                int  period =0;

                foreach (var b in baskets)
                {
                    if (b != null)
                    {
                        email = b.Client.Email;
                        period = b.Subscription.Period;
                        context.SubscriptionRegistrations.Remove(b);
                        context.SaveChanges();
                    }
                }

                List<Subscription> subscription = new List<Subscription>();
                foreach (var s in context.Subscriptions.ToList())
                    subscription.Add(s);
                foreach (var s in subscription)
                {
                    if (s != null && s.Period== period)
                    {
                        context.Subscriptions.Remove(s);
                        context.SaveChanges();
                    }
                }

                List<Client> clients = new List<Client>();
                foreach (var s in context.Clients.ToList())
                    clients.Add(s);

                foreach (var c in clients)
                {
                    if (c != null && c.Email == email)
                    {
                        context.Clients.Remove(c);
                        context.SaveChanges();
                    }
                }

                Console.WriteLine("Очень жаль, подписка отменена... Надеюсь Вы понимаете, что деньги за подписку Вам не вернут.");
            }
        }
    }
}
