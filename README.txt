GameTalkForum

Namn: Johannes Hellman
Email: johanneshellman@hotmail.com
Telefon: 0762355375

GameTalkForum är ett forum för gamers. Den har inloggning och möjlighet att skicka meddelanden mellan varandra och anmäla 
inlägg och kommentarer som sedan admin kan gå igenom och på ett användarvänligt sätt hantera ärenden. 
Planneringsstart började 17 maj 2021 och slutdatum för projektet var  18 juni 2021. 
Planneringstiden av uppbyggnad och databasen pågick de 2 första veckorna för att få en bra plannering inför start av kodningen, 
tidsåtgång uppskattar jag till 170 timmar. 
Tekniska krav för att drifta programmet är, Windows, IIS, ramverk: .NET CORE 5.

De olika Nuget-Paketen som används är:
Microsoft.AspNetCore.Identity.EntityFrameworkC
Microsoft.AspNetCore.Identity.UI
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
Microsoft.VisualStudio.Web.CodeGeneratio
Som databaslösning använder jag MS SQL i Azure och även Azures egna servermiljö.
Programmen som är skapade för detta projekt är ForumWeb och ForumAPI.

I startup.cs filen används DI:
services.AddHttpClient<Gateways.CategoryGateway>();
services.AddHttpClient<Gateways.SubCategoryGateway>();
services.AddHttpClient<Gateways.PostGateway>();
services.AddHttpClient<Gateways.CommentGateway>();
services.AddHttpClient<Gateways.ReportGateway>();
services.AddHttpClient<Gateways.MessageGateway>();
I IdentityHostingStartup.cs filen används DI:
services.AddDbContext<ForumWebContext>
services.AddDefaultIdentity<ForumWebUser>

Mina controllers som ligger i ForumAPI är 
CategoriesController.cs
CommentsController.cs
MessagesController.cs
PostsController.cs
ReportsController.cs
SubCategoriesController.cs

Micro Services används genom API programmet och dess DB. Programmeringsmönstret som jag har använd för projektet är RazorPages.
För installation, kontrollera att dina connection-strings är placerade i koden, 
placera dessa i filen appsettings.json under "ConnectionStrings": i båda programmen. 
Se till att glra en migration och update database och sedan ta med migration in i Azure när du går in för att plublicera det via 
Visual-Studio. Första kontot som registreras skall ha mail admin@test.com för att skapa en admin.

Endpoints som används till APIet är: 
Categories:
https://snackisforumapi.net/api/categories [GET] [POST]
https://snackisforumapi.net/api/categories/{id} [GET one] [PUT] [DELETE]
Comments:
https://snackisforumapi.net/api/comments [GET] [POST]
https://snackisforumapi.net/api/comments/{id} [GET one] [PUT] [DELETE]
Messages:
https://snackisforumapi.net/api/messages[GET] [POST]
https://snackisforumapi.net/api/messages/{id} [GET one] [PUT] [DELETE]
Posts:
https://snackisforumapi.net/api/posts [GET] [POST]
https://snackisforumapi.net/api/posts/{id} [GET one] [PUT] [DELETE]
Reports:
https://snackisforumapi.net/api/reports [GET] [POST]
https://snackisforumapi.net/api/reports/{id} [GET one] [PUT] [DELETE]
SubCategories:
https://snackisforumapi.net/api/subcategories [GET] [POST]
https://snackisforumapi.net/api/subcategories/{id} [GET one] [PUT] [DELETE]

Funktioner:
För inloggning används microsoft identity, vid inloggning registrerar man  mail, nickname och lösenord, senare kan man lägga till telefonnummer och en bild. 
När man är inloggad kan man kolla igenom folks poster och kommentarer, även skriva egna poster och kommentarer. 
Admin skapar kategorier och underkategorier. Man kan även skicka meddelanden till varandra, eller raportera varandras poster om de inehåller olämpligt innehåll mm.
Brister: 
Ligger det mesta i Azure då det är väldigt sörliga servrar för gratis konton och väldigt lite data användning som får användas dagligen. 
Connection-strings synns med lösenord uppe på Git och detta borde ändras så det ligger uppe på servern så som azure istället.
Förbättring:
Skulle säga att förbättra och utöka inkorgen och möjligheten till att skriva till varandra. 
Även kanske lägga till en gransknings funktion o lägen då någon använder fula ord så kan den inte postas direkt, 
utan åker till granskning först där admin måste godkänna den först eller deleta den.
Summering:
Det jag lärt mig mest är större förståelse av säkerhet och även vikten av en bra grundplannering inför självaste skapandet och kodningen.

Av: Johannes Hellman