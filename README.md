# ИС для составления и контроля плана продаж магазина одежды C# WPF ASP.NET
PostgreSQL -> EFCore -> ASP.NET API -> MVVM -> WPF

# Установка
Система работает на базе PostgreSQL. Необходимо добавить роль для подключения API к серверу базы данных.
![](https://github.com/LuisanArgoose/ShopProject/blob/master/Screenshots/PGRole.png)

В контексте данных ShopProjectDbContext библиотеки ShopProject.EFDB прописать строку для инициализации базы данных. Строка подключения для ShopProject.API находится в файле appsettings.json.
![](https://github.com/LuisanArgoose/ShopProject/blob/master/Screenshots/SetInitConnecrionString.png)

В консоли диспетчера пакетов проинициализировать базу данных. Выбранный проект по умолчанию должен быть ShopProject.EFDB, в качестве запускаемого проекта должен быть ShopProject.API.
```
Update-Database -context ShopProjectDbContext
```
![](https://github.com/LuisanArgoose/ShopProject/blob/master/Screenshots/UpdateCommand.png)

Запустить проект ShopProject.UI и во вкладке настроек прописать IP API и проверить подключение.
```
https://localhost:7178/api/
```
Потом поочереди прожать кнопки очистки базы данных, создания стартовых и тестовых данных до завершения каждой команды
![](https://github.com/LuisanArgoose/ShopProject/blob/master/Screenshots/InitClient.png)

# Обзор
## Система имеет разграничения по уровням доступа - менеджер магазина и менеджер по продажам.
![](https://github.com/LuisanArgoose/ShopProject/blob/master/Screenshots/SingIn.png)

## Менеджер магазина имеет доступ к вкладке "План магазина".
![](https://github.com/LuisanArgoose/ShopProject/blob/master/Screenshots/ManagerProfile.png)

## Главным графиком является - основной план магазина, который отображает соответствие метрик к выставленному плану.
![](https://github.com/LuisanArgoose/ShopProject/blob/master/Screenshots/ManagerMainPlan1.png)

## Отдельно выведены результаты за период и соответствие каждой метрики за весь период.
![](https://github.com/LuisanArgoose/ShopProject/blob/master/Screenshots/ManagerMainPlan2.png)

## Также можно посмотреть каждую метрику и план по отдельности.
![](https://github.com/LuisanArgoose/ShopProject/blob/master/Screenshots/ManagerMainPlan2.png)

## Менеджер по продажам имеет доступ к вкладке "Магазины" и "Общий план".
![](https://github.com/LuisanArgoose/ShopProject/blob/master/Screenshots/SalesManagerProfile.png)

## Во вкладке "Общий план" отображены графики долей по результатам за период.
![](https://github.com/LuisanArgoose/ShopProject/blob/master/Screenshots/SalesManagerShopsPlan1.png)

## Также отображен график общей успеваемости всех магазинов
![](https://github.com/LuisanArgoose/ShopProject/blob/master/Screenshots/SalesManagerShopsPlan2.png)

## Во вкладке "Магазины" отображены планы каждого магазина
![](https://github.com/LuisanArgoose/ShopProject/blob/master/Screenshots/SalesManagerShopMainPlan1.png)

## Менеджеру по продажам доступна панель планов. Он может увидеть их список, удалить, добавить или изменить лобой из планов
![](https://github.com/LuisanArgoose/ShopProject/blob/master/Screenshots/SalesManagerShopMainPlan2.png)

## Для настройки приложения выведена специальная страница настроек с локальным сохранением
![](https://github.com/LuisanArgoose/ShopProject/blob/master/Screenshots/Settings.png)
