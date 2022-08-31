using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Store_Console
{
    class Program
    {
        public class Store
        {
            public string Name;
            public List<Product> Products;
            public List<Product> Basket;
            public List<Order> Orders;

            public Store()
            {
                Products = new List<Product>
                {
                    new Product("Хлеб", 25),
                    new Product("Молоко", 100),
                    new Product("Печенье", 50),
                    new Product("Масло", 250),
                    new Product("Йогурт", 300),
                    new Product("Сок", 80)
                };
                Basket = new List<Product>();
                Orders = new List<Order>();
            }
            public Store(string n)
            {
                Name = n;
            }

            public void ShowStore(List<Product> p)
            {
                int ProdNum = 1;
                foreach (var i in p)
                {
                    Console.Write($"{ProdNum}. ");
                    i.ShowProduct();
                    ProdNum++;
                }
            }
            public void ShowClient(List<Order> o)
            {
                foreach (var i in o)
                {
                    i.ShowClient();                    
                }
            }
            public void AddToBasket(int n)
            {

                Basket.Add(Products[n - 1]);
                Console.WriteLine($"Продукт {Products[n - 1].Name} добавлен в корзину");
                Console.WriteLine($"В корзине {Basket.Count} продуктов");
            }
            public void AddToCart(string n, decimal p)
            {
                Product NewProd = new Product(n, p);
                Products.Add(NewProd);
                Console.WriteLine($"Продукт {n}, {p} добавлен в магазин.");
            }

            public void CreateOrder()
            {
                Console.WriteLine("Введите имя клиента:");
                User user = new User(Console.ReadLine());
                Order order = new Order(Basket, user.Name);
                Orders.Add(order);
                Basket.Clear();
            }

        }
        public class Product
        {
            public string Name;
            public decimal Price;

            public Product(string n, decimal p)
            {
                Name = n;
                Price = p;
            }

            public void ShowProduct()
            {
                Console.WriteLine($"{Name}, {Price}");
            }
        }

        public class Order
        {
            public decimal Result;
            public List<Product> Products;
            public string User;

            public Order(List<Product> products, string u)
            {
                Products = products;
                User = u;
                foreach (var i in products)
                {
                    Result += i.Price;
                }
            }
            public void ShowClient()
            {
                Console.WriteLine($"Клиент {User}");
                foreach (var i in Products)
                {
                    Console.WriteLine($"{i.Name}, {i.Price}");
                }
                Console.WriteLine($"Общая сумма: {Result}");
            }
        }

        public class User
        {
            public string Name;
            public User(string n)
            {
                Name = n;
            }
        }

        static void Main(string[] args)
        {
            Store onlineStore = new Store();
            string AdminPass = "159";
            int numberAction = -1;
            Console.WriteLine("Онлайн-магазин.");
            do
            {
                Console.WriteLine("=========================================================");
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Показать каталог продуктов.");
                Console.WriteLine("2. Добавить товар в корзину.");
                Console.WriteLine("3. Посмотреть корзину.");
                Console.WriteLine("4. Оформить заказ.");
                Console.WriteLine("5. Посмотреть заказы клиентов.");
                Console.WriteLine("6. Добавить товар в магазин");
                Console.WriteLine("0. Выйти.");
                Console.WriteLine();
                Console.WriteLine("Выберите номер действия, которое хотите совершить.");
                Console.WriteLine("=========================================================");

                try
                {
                    numberAction = Convert.ToInt32(Console.ReadLine());
                    switch (numberAction)
                    {
                        case 1: onlineStore.ShowStore(onlineStore.Products); break;
                        case 2: onlineStore.ShowStore(onlineStore.Products); Console.WriteLine("Выберите товар из списка"); onlineStore.AddToBasket(Convert.ToInt32(Console.ReadLine())); break;
                        case 3: if (onlineStore.Basket.Count == 0) Console.WriteLine("Корзина пуста!"); else onlineStore.ShowStore(onlineStore.Basket); break;
                        case 4: onlineStore.CreateOrder(); break;
                        case 5:onlineStore.ShowClient(onlineStore.Orders); break;
                        case 6:
                            Console.WriteLine("Введите пароль:"); if (Console.ReadLine() == AdminPass)
                            {
                                Console.WriteLine("Введите название продукта:"); string n = Console.ReadLine();
                                Console.WriteLine("Введите стоимость продукта:"); decimal p = Convert.ToDecimal(Console.ReadLine());
                                onlineStore.AddToCart(n, p);
                            }
                            else { Console.WriteLine("Пароль неверный!"); }
                            break;
                        default:
                            Console.WriteLine("Выберите корректный номер действия!"); break;
                    }
                }
                catch
                {
                    Console.WriteLine("Выберите номер!");
                }
            } while (numberAction != 0);

        }
    }
}
