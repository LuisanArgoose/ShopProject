using Microsoft.EntityFrameworkCore;
using ShopProject.EFDB.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ShopProject.API.Data
{
    public static class DbFiller
    {
        public static void ClearDb(ServerAPIDbContext context)
        {
            if (context == null) { return; }
            try
            {
                context.Cashiers.RemoveRange(context.Cashiers);
                context.Shops.RemoveRange(context.Shops);
                context.Products.RemoveRange(context.Products);
                context.Roles.RemoveRange(context.Roles);
                context.Users.RemoveRange(context.Users);
                context.TokenLogins.RemoveRange(context.TokenLogins);
                context.Metrics.RemoveRange(context.Metrics);
            }
            catch
            {

            }
            context.SaveChanges();
            
        }
        public static void InitDb(ServerAPIDbContext context)
        {
            if (context == null) { return; }

            context.Cashiers.RemoveRange(context.Cashiers);
            context.Shops.RemoveRange(context.Shops);
            context.Products.RemoveRange(context.Products);
            context.Roles.RemoveRange(context.Roles);
            context.Users.RemoveRange(context.Users);
            context.TokenLogins.RemoveRange(context.TokenLogins);
            context.Metrics.RemoveRange(context.Metrics);

            var cashiers = new List<Cashier>()
            {
                new Cashier()
                {
                    FullName = "Иванова Екатерина Петровна"
                },

                new Cashier()
                {
                    FullName = "Смирнов Александр Иванович"
                },

                new Cashier()
                {
                    FullName = "Козлова Мария Дмитриевна"
                },

                new Cashier()
                {
                    FullName = "Попов Денис Сергеевич"
                },

                new Cashier()
                {
                    FullName = "Морозова Елена Александровна"
                },

                new Cashier()
                {
                    FullName = "Никитин Артем Владимирович"
                },

                new Cashier()
                {
                    FullName = "Петрова Ольга Николаевна"
                },

                new Cashier()
                {
                    FullName = "Сергеев Дмитрий Анатольевич"
                },

                new Cashier()
                {
                    FullName = "Федорова Анастасия Игоревна"
                },

                new Cashier()
                {
                    FullName = "Алексеев Владимир Викторович"
                },

                new Cashier()
                {
                    FullName = "Лебедева Маргарита Геннадьевна"
                },

                new Cashier()
                {
                    FullName = "Кузнецов Антон Сергеевич"
                },

                new Cashier()
                {
                    FullName = "Соколова Кристина Алексеевна"
                },

                new Cashier()
                {
                    FullName = "Костин Павел Дмитриевич"
                },

                new Cashier()
                {
                    FullName = "Игнатьева Елизавета Игоревна"
                },

                new Cashier()
                {
                    FullName = "Крылов Артур Петрович"
                },

                new Cashier()
                {
                    FullName = "Максимова Евгения Леонидовна"
                },

                new Cashier()
                {
                    FullName = "Григорьев Илья Васильевич"
                },

                new Cashier()
                {
                    FullName = "Орлова Людмила Викторовна"
                },

                new Cashier()
                {
                    FullName = "Карпов Даниил Юрьевич"
                },

                new Cashier()
                {
                    FullName = "Андреева Наталья Степановна"
                },

                new Cashier()
                {
                    FullName = "Герасимов Артем Михайлович"
                },

                new Cashier()
                {
                    FullName = "Бойко Анастасия Олеговна"
                },

                new Cashier()
                {
                    FullName = "Семенов Владислав Валентинович"
                },

                new Cashier()
                {
                    FullName = "Тарасова Софья Дмитриевна"
                },
            };

            var shops = new List<Shop>()
            {
                new Shop()
                {
                    ShopName = "Магазин 1",
                    Address = "Россия, г. Раменское, Зеленый пер., д. 22",
                    Cashiers = new List<Cashier>(cashiers.GetRange(0,5))

                },
                new Shop()
                {
                    ShopName = "Магазин 2",
                    Address = "Россия, г. Магнитогорск, Цветочная ул., д. 15",
                    Cashiers = new List<Cashier>(cashiers.GetRange(5,5))
                },
                new Shop()
                {
                    ShopName = "Магазин 3",
                    Address = "Россия, г. Березники, Южная ул., д. 17",
                    Cashiers = new List<Cashier>(cashiers.GetRange(10,5))
                },
                new Shop()
                {
                    ShopName = "Магазин 4",
                    Address = "Россия, г. Реутов, Цветочная ул., д. 8",
                    Cashiers = new List<Cashier>(cashiers.GetRange(15,5))
                },
                new Shop()
                {
                    ShopName = "Магазин 5",
                    Address = "Россия, г. Старый Оскол, Школьный пер., д. 20",
                    Cashiers = new List<Cashier>(cashiers.GetRange(20,5))
                }

            };

            var products = new List<Product>()
            {
                new Product()
                {
                    ProductName = "Женское белье комплект трусиков",
                    CostPrice = 100,
                    SellPrice = 250
                },

                new Product()
                {
                    ProductName = "Мужское белье трусы",
                    CostPrice = 80,
                    SellPrice = 200
                },

                new Product()
                {
                    ProductName = "Комплект нижнего белья для женщин",
                    CostPrice = 150,
                    SellPrice = 350
                },

                new Product()
                {
                    ProductName = "Трусы-бразилиана",
                    CostPrice = 90,
                    SellPrice = 180
                },

                new Product()
                {
                    ProductName = "Мужские боксеры",
                    CostPrice = 120,
                    SellPrice = 280
                },

                new Product()
                {
                    ProductName = "Женские трусы-слипы",
                    CostPrice = 70,
                    SellPrice = 150
                },

                new Product()
                {
                    ProductName = "Пижама для женщин",
                    CostPrice = 200,
                    SellPrice = 400
                },

                new Product()
                {
                    ProductName = "Мужская футболка без рукавов",
                    CostPrice = 60,
                    SellPrice = 130
                },

                new Product()
                {
                    ProductName = "Бюстгальтер пуш-ап",
                    CostPrice = 110,
                    SellPrice = 260
                },

                new Product()
                {
                    ProductName = "Мужская майка",
                    CostPrice = 50,
                    SellPrice = 120
                },

                new Product()
                {
                    ProductName = "Ночная сорочка для женщин",
                    CostPrice = 90,
                    SellPrice = 200
                },

                new Product()
                {
                    ProductName = "Женские трусы-бикини",
                    CostPrice = 60,
                    SellPrice = 130
                },

                new Product()
                {
                    ProductName = "Мужские трусы-боксеры с рисунком",
                    CostPrice = 100,
                    SellPrice = 240
                },

                new Product()
                {
                    ProductName = "Эротическое белье красного цвета",
                    CostPrice = 150,
                    SellPrice = 350
                },

                new Product()
                {
                    ProductName = "Пижама для мужчин",
                    CostPrice = 180,
                    SellPrice = 400
                },

                new Product()
                {
                    ProductName = "Женский комплект белья из кружева",
                    CostPrice = 130,
                    SellPrice = 280
                },

                new Product()
                {
                    ProductName = "Спортивный костюм из эластичного материала",
                    CostPrice = 250,
                    SellPrice = 450
                },

                new Product()
                {
                    ProductName = "Мужской хлопковый трусики",
                    CostPrice = 70,
                    SellPrice = 150
                },

                new Product()
                {
                    ProductName = "Боди с кружевным верхом для женщин",
                    CostPrice = 120,
                    SellPrice = 270
                },

                new Product()
                {
                    ProductName = "Женский лонгслив из мягкой ткани",
                    CostPrice = 80,
                    SellPrice = 180
                }
            };
            
            var roles = new List<Role>()
            {
                new Role()
                {
                    RoleName = "Менеджер",
                    IsShopManager = true,
                    IsSalesManager = false,
                    IsAdmin = false
                },
                new Role()
                {
                    RoleName = "Менеджер по продажам",
                    IsShopManager = false,
                    IsSalesManager = true,
                    IsAdmin = false
                },
                new Role()
                {
                    RoleName = "Админ",
                    IsShopManager = true,
                    IsSalesManager = true,
                    IsAdmin = true
                }

            };

            var users = new List<User>()
            {
                new User()
                {
                    Fullname = "Менеджер 1",
                    Login = "Manager1",
                    Password = "Pass1",
                    Role = roles.First(x => x.RoleName == "Менеджер"),
                    Shop = shops.First(x => x.ShopName == "Магазин 1")
                },
                new User()
                {
                    Fullname = "Менеджер 2",
                    Login = "Manager2",
                    Password = "Pass2",
                    Role = roles.First(x => x.RoleName == "Менеджер"),
                    Shop = shops.First(x => x.ShopName == "Магазин 2")
                },
                new User()
                {
                    Fullname = "Менеджер 3",
                    Login = "Manager3",
                    Password = "Pass3",
                    Role = roles.First(x => x.RoleName == "Менеджер"),
                    Shop = shops.First(x => x.ShopName == "Магазин 3")
                },
                new User()
                {
                    Fullname = "Менеджер 4",
                    Login = "Manager4",
                    Password = "Pass4",
                    Role = roles.First(x => x.RoleName == "Менеджер"),
                    Shop = shops.First(x => x.ShopName == "Магазин 4")
                },
                new User()
                {
                    Fullname = "Менеджер 5",
                    Login = "Manager5",
                    Password = "Pass5",
                    Role = roles.First(x => x.RoleName == "Менеджер"),
                    Shop = shops.First(x => x.ShopName == "Магазин 1")
                },
                new User()
                {
                    Fullname = "Менеджер по продажам 1",
                    Login = "SalesManager1",
                    Password = "SalesPass1",
                    Role = roles.First(x => x.RoleName == "Менеджер по продажам"),
                },
                new User()
                {
                    Fullname = "Админ1",
                    Login = "Admin1",
                    Password = "AdminPass1",
                    Shop = shops.First(x => x.ShopName == "Магазин 1"),
                    Role = roles.First(x => x.RoleName == "Админ"),
                }
            };


            var planAtributes = new List<Metric>()
            {
                new Metric()
                {
                    MetricName = "SalesCountInDay",
                    MetricViewName = "Продажи в день"
                },
                new Metric()
                {
                    MetricName = "AverageBill",
                    MetricViewName = "Средний чек"
                },
                new Metric()
                {
                    MetricName = "RevenueInDay",
                    MetricViewName = "Выручка в день"
                },
                new Metric()
                {
                    MetricName = "ProfitInDay",
                    MetricViewName = "Прибыль в день"
                },



            };
            

            context.AddRange(cashiers);
            context.AddRange(shops);
            context.AddRange(products);
            context.AddRange(roles);
            context.AddRange(users);
            context.AddRange(planAtributes);

            context.SaveChanges();
        }

        private static Random _rnd = new();
        public static void FillDb(ServerAPIDbContext context, DateTime startDate, DateTime endDate)
        {
            // Расстановка даты по порядку
            void Swap<T>(ref T a, ref T b)
            {
                T temp = a;
                a = b;
                b = temp;
            }
            if (startDate > endDate)
            {
                Swap(ref startDate, ref endDate);
            }

            //Очистка всех покупок
            context.Purchases.RemoveRange(context.Purchases);
            context.ShopPlans.RemoveRange(context.ShopPlans);

            /*
            //Перебор каждого дня в промежутке дат
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                var cashiers = context.Cashiers.ToList();

                foreach (var cashier in cashiers)
                {
                    //Случайное количество покупок кассира
                    //var randomPurchasesCount = 5 //Для контроля
                    var randomPurchasesCount = _rnd.Next(15, 25);
                    for (int i = 0; i < randomPurchasesCount; i++)
                    {
                        //Создание самой покупки
                        var purchase = new Purchase()
                        {
                            Cashier = cashier,
                            OperationTime = date.AddHours(8).AddMinutes(_rnd.Next(0, 720)),
                        };

                        var purchaseProductList = new List<PurchaseProduct>();

                        //Выбор случайных товаров
                        var products = new List<Product>();
                        //Количество товаров в покупке
                        //int count = 3; //Для контроля
                        int count = _rnd.Next(1, 5);
                        while (purchaseProductList.Select(x => x.Product).Count() < count)
                        {
                            //Выборка случайного продукта
                            //int randomIndex = _rnd.Next(count); //Для контроля
                            int randomIndex = _rnd.Next(context.Products.Count());
                            //var randomProduct = context.Products.ToList()[randomIndex];
                            var randomProduct = context.Products.ToList()[purchaseProductList.Select(x => x.Product).Count()];


                            //Проверка на сущетвование товара в покупке
                            if (!purchaseProductList.Select(x => x.Product).Contains(randomProduct))
                            {
                                purchaseProductList.Add(new PurchaseProduct()
                                {
                                    Product = randomProduct,
                                    Purchase = purchase,
                                    //Count = 1 //Для контроля
                                    Count = _rnd.Next(1, 5)
                                });
                            }
                        }
                        context.AddRange(purchaseProductList);
                    }
                }
            }*/
            
            var cashiers = context.Cashiers.ToList();
            var products = context.Products.ToList();

            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                foreach (var cashier in cashiers)
                {
                    var randomPurchasesCount = _rnd.Next(15, 25);
                    for (int i = 0; i < randomPurchasesCount; i++)
                    {
                        var purchase = new Purchase()
                        {
                            Cashier = cashier,
                            OperationTime = date.AddHours(8).AddMinutes(_rnd.Next(0, 720)),
                        };

                        var purchaseProductList = new List<PurchaseProduct>();

                        int count = _rnd.Next(1, 5);
                        while (purchaseProductList.Count < count)
                        {
                            var randomProduct = products[_rnd.Next(products.Count)];

                            if (!purchaseProductList.Any(pp => pp.Product == randomProduct))
                            {
                                purchaseProductList.Add(new PurchaseProduct()
                                {
                                    Product = randomProduct,
                                    Purchase = purchase,
                                    Count = _rnd.Next(1, 5)
                                });
                            }
                        }
                        context.AddRange(purchaseProductList);
                    }
                }
            }
            
            /*
            var shops = context.Shops.ToList();
            List<ShopPlan> shopPlans = [];
            foreach(var shop in shops)
            {
                var plansCount = _rnd.Next(15, 20);
                for (int i = 0; i < plansCount;)
                {
                    shopPlans.Add(new ShopPlan()
                    {
                        PlanAtributeId = context.PlanAtributes.First(x => x.AtributeName == "AverageBill").PlanAtributeId,
                        ShopId = shop.ShopId,
                        AtributeValue = _rnd.Next(590, 620),
                        SetTime = startDate.AddDays(_rnd.Next(0,(endDate - startDate).Days))
                    });
                }
                plansCount = _rnd.Next(15, 20);
                for (int i = 0; i < plansCount;)
                {
                    shopPlans.Add(new ShopPlan()
                    {
                        PlanAtributeId = context.PlanAtributes.First(x => x.AtributeName == "AllProfit").PlanAtributeId,
                        ShopId = shop.ShopId,
                        AtributeValue = _rnd.Next(130000, 165000),
                        SetTime = startDate.AddDays(_rnd.Next(0, (endDate - startDate).Days))
                    });
                }
                plansCount = _rnd.Next(15, 20);
                for (int i = 0; i < plansCount;)
                {
                    shopPlans.Add(new ShopPlan()
                    {
                        PlanAtributeId = context.PlanAtributes.First(x => x.AtributeName == "ClearProfit").PlanAtributeId,
                        ShopId = shop.ShopId,
                        AtributeValue = _rnd.Next(80000, 90000),
                        SetTime = startDate.AddDays(_rnd.Next(0, (endDate - startDate).Days))
                    });
                }
                plansCount = _rnd.Next(15, 20);
                for (int i = 0; i < plansCount;)
                {
                    shopPlans.Add(new ShopPlan()
                    {
                        PlanAtributeId = context.PlanAtributes.First(x => x.AtributeName == "PurchasesCount").PlanAtributeId,
                        ShopId = shop.ShopId,
                        AtributeValue = _rnd.Next(220, 260),
                        SetTime = startDate.AddDays(_rnd.Next(0, (endDate - startDate).Days))
                    });
                }
                
            }
            */
            var shops = context.Shops.ToList();
            var metrics = context.Metrics.ToList();

            List<ShopPlan> shopPlans = new List<ShopPlan>();

            foreach (var shop in shops)
            {
                foreach (var metric in metrics)
                {
                    var plansCount = _rnd.Next(15, 21); // Генерируем случайное число от 15 до 20
                    var dates = new List<DateTime>();
                    for (int i = 0; i < plansCount; i++)
                    {
                        var date = startDate.AddDays(_rnd.Next(0, (endDate.AddDays(30) - startDate).Days));
                        while (dates.Contains(date))
                        {
                            date = startDate.AddDays(_rnd.Next(0, (endDate.AddDays(30) - startDate).Days));
                        }
                        dates.Add(date);
                        var plan = new ShopPlan()
                        {
                            Metric = metric,
                            Shop = shop,
                            SetTime = date
                        };
                        plan.MetricValue = metric.MetricName switch
                        {
                            "SalesCountInDay" => _rnd.Next(90, 110),
                            "AverageBill" => _rnd.Next(1450, 1600),
                            "RevenueInDay" => _rnd.Next(140000, 160000),
                            "ProfitInDay" => _rnd.Next(60000, 80000),
                            

                        };

                        shopPlans.Add(plan);
                    }
                }
            }
            context.AddRange(shopPlans);
            context.SaveChanges();
        }
    }
}
