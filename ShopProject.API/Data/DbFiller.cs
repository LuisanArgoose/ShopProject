using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ShopProject.API.Data
{
    public static class DbFiller
    {
        public static void InitDb(ServerAPIDbContext context)
        {
            if (context == null) { return; }

            context.Cashiers.RemoveRange(context.Cashiers);
            context.Shops.RemoveRange(context.Shops);
            context.Products.RemoveRange(context.Products);
            context.Roles.RemoveRange(context.Roles);
            context.Users.RemoveRange(context.Users);
            context.TokenLogins.RemoveRange(context.TokenLogins);

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
                    Role = roles.First(x => x.RoleName == "Админ"),
                }
            };

            var tokenLogin = new TokenLogin()
            {
                Login = "TokenKey",
                Password = "TokenPass"
            };

            context.AddRange(cashiers);
            context.AddRange(shops);
            context.AddRange(products);
            context.AddRange(roles);
            context.AddRange(users);
            context.AddRange(tokenLogin);

            context.SaveChanges();
        }

        private static Random _rnd = new();
        public static void FillDb(ServerAPIDbContext context, DateTime startDate, DateTime endDate)
        {
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

            context.Purchases.RemoveRange(context.Purchases);
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                var cashiers = context.Cashiers.ToList();

                foreach (var cashier in cashiers)
                {
                    for (int i = 0; i < _rnd.Next(15, 25); i++)
                    {
                        var purchase = new Purchase()
                        {
                            Cashier = cashier,
                            OperationTime = date.AddHours(8).AddMinutes(_rnd.Next(0, 720)),
                        };

                        var purchaseProductList = new List<PurchaseProduct>();
                        var products = context.Products.Distinct().Take(_rnd.Next(1, 5)).ToList();
                        foreach (var product in products)
                        {
                            purchaseProductList.Add(new PurchaseProduct()
                            {
                                Product = product,
                                Purchase = purchase,
                                Count = _rnd.Next(1, 5)
                            });
                        }
                        context.AddRange(purchaseProductList);
                    }
                }
            }
            context.SaveChanges();
        }
    }
}
