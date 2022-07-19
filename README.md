# WebDevelopment

Отдельный отчет по работе с SQL:
(П)Ознакомился с Dapper, Ado.Net и EF Core. У каждого свои особенности и приемущества. 
В тот момент когда EF Core выглядит более похожим на код C#, с его операторами LINQ и методами расшерения, dapper и ADO.Net позволяют использовать чистые SQL запросы. 
На сколько я понял на выгрузках больших данных Dapper будет работать быстрее. 
На проекте сделал Scaffold базы данных, потом добавил две модели и через миграцию обновил базу данных. 
В БД помимо установки constraints default, сделал триггер в таблице Users, на insert берется таск из таблицы Tasks и заполняется таблица UserTasks.
Вся БД експотнута в SQL script, файл скрипта можно найти в папке deploy.

Первый отчет о проделанном за неделю:
1. Создан проект WebDevelopment.Api
2. В локальной базе данных созданы таблицы по оговоренной схеме
3. Созданы контроллеры для каждой таблицы из БД
4. Подключен ErrorHandlerMiddleware
5. Создан сервис для работы с UserController и интеграционные тесты для него.
6. Создана модели для *UserRequest и валидация через FluentValidation. Автовалидация добавлена в Program.cs
7. Добавлена аутентификация через ApiKey (папка Security), ключ передается через хеддер и сверяется с тем, что указан в appsettings. Делалось по примеру из предосталенного для ознакомления проекта.
8. Настроен swagger для отправки ключа через хеддер.
9. Поправлены тесты, добавлена тестовая аутентификация.
10. Добавлен новый проект WebDevelopment.Infrastructure, куда с использование Reverse engineering из БД вытянута модель и контекст.
11. Контекст подключен к API через DI и передана строка подключения из appsettings. 
12. В процессе работы была прочитана информация по настройке сваггера, про claim и авторизацию с аутентификацией, про SQL базы данных и таблицы, предосталены были примеры интересных SQL запросов.  

Отчет о прочтенном материале перед созданием аппа.
Прочитал:
1. database first and code first worflow
2. что такое ORM, типы и Dapper (+ EF, миграции, dbcontex, подключение к существующией DB через Scaffold-DbContext)
3. SQLClient, если точнее, то про SqlConnection Class и прошелся по ссылкам в статье https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlconnection?view=dotnet-plat-ext-6.0
4. WebApi tutorial 
5. Repository pattern
6. Factory pattern
7. Background task с hosted service
8. бегло синтаксис и теорию SQL 
9. про domain driven design.
10. Stored Procedure
11. Attribute filters
12. Прочитал про FluentValidation, добавил в проект для метода post
13. Прочитал про ErrorHandlingMiddleware, добавил в проект
