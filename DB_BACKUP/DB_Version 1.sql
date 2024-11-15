USE [master]
GO
/****** Object:  Database [Kuznetsov IS-21-3 Zoo]    Script Date: 22.03.2024 17:45:35 ******/
CREATE DATABASE [Kuznetsov IS-21-3 Zoo]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Kuznetsov IS-21-3 Zoo', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Kuznetsov IS-21-3 Zoo.mdf' , SIZE = 139264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Kuznetsov IS-21-3 Zoo_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Kuznetsov IS-21-3 Zoo_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Kuznetsov IS-21-3 Zoo] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Kuznetsov IS-21-3 Zoo].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Kuznetsov IS-21-3 Zoo] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Kuznetsov IS-21-3 Zoo] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Kuznetsov IS-21-3 Zoo] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Kuznetsov IS-21-3 Zoo] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Kuznetsov IS-21-3 Zoo] SET ARITHABORT OFF 
GO
ALTER DATABASE [Kuznetsov IS-21-3 Zoo] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Kuznetsov IS-21-3 Zoo] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Kuznetsov IS-21-3 Zoo] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Kuznetsov IS-21-3 Zoo] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Kuznetsov IS-21-3 Zoo] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Kuznetsov IS-21-3 Zoo] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Kuznetsov IS-21-3 Zoo] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Kuznetsov IS-21-3 Zoo] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Kuznetsov IS-21-3 Zoo] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Kuznetsov IS-21-3 Zoo] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Kuznetsov IS-21-3 Zoo] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Kuznetsov IS-21-3 Zoo] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Kuznetsov IS-21-3 Zoo] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Kuznetsov IS-21-3 Zoo] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Kuznetsov IS-21-3 Zoo] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Kuznetsov IS-21-3 Zoo] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Kuznetsov IS-21-3 Zoo] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Kuznetsov IS-21-3 Zoo] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Kuznetsov IS-21-3 Zoo] SET  MULTI_USER 
GO
ALTER DATABASE [Kuznetsov IS-21-3 Zoo] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Kuznetsov IS-21-3 Zoo] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Kuznetsov IS-21-3 Zoo] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Kuznetsov IS-21-3 Zoo] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Kuznetsov IS-21-3 Zoo] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Kuznetsov IS-21-3 Zoo] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Kuznetsov IS-21-3 Zoo] SET QUERY_STORE = OFF
GO
USE [Kuznetsov IS-21-3 Zoo]
GO
/****** Object:  Table [dbo].[Animals]    Script Date: 22.03.2024 17:45:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Animals](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[About] [nvarchar](max) NULL,
	[Facts] [nvarchar](max) NULL,
	[ImageSRC] [nvarchar](255) NULL,
	[SoundSRC] [nvarchar](255) NULL,
 CONSTRAINT [PK_Animals] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 22.03.2024 17:45:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 22.03.2024 17:45:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[Email] [nvarchar](255) NULL,
	[Login] [nvarchar](255) NULL,
	[Password] [nvarchar](255) NULL,
	[Status] [float] NULL,
	[Role] [int] NULL,
	[Image] [nvarchar](max) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Animals] ON 

INSERT [dbo].[Animals] ([ID], [Name], [About], [Facts], [ImageSRC], [SoundSRC]) VALUES (1, N'Африканский слон', N'Встречаются африканские слоны на территории всего континента, как в тропических лесах Западной и Центральной Африки, так и на юге Сахары. Животные приспосабливаются к любой среде обитания, лишь бы в ней было легко найти пищу и воду. Они относятся к семейству слоновых и являются самыми крупными на земле млекопитающими. Слоны разделяются на два вида: лесной с прямо направленными вниз бивнями, и саванный с вывернутыми наружу. Первый, более темный, весит приблизительно 5 тонн, второй – 7-12. Питаются африканские слоны исключительно растительной пищей. Имеющийся у животного хобот позволяет поднимать вес более 200 кг.', N'Длина ушей африканского слона около 2-х м. С их помощью животные спасаются от зноя – при взмахах эффективно снижается температура собственного тела.', N'The African Elephant.jpg', N'The African Elephant.mp3')
INSERT [dbo].[Animals] ([ID], [Name], [About], [Facts], [ImageSRC], [SoundSRC]) VALUES (2, N'Жираф', N'Парнокопытное млекопитающее относится к семейству жирафовых. Животное является самым высоким на планете. Самцы жирафа имеют вес 900-1200 кг. Их высота составляет 5,5 – 6,1 м, причем одну треть занимает стройная шея. Самки обычно немного ниже и легче. Ареал обитания жирафов ближе к югу и юго-востоку от Сахары. Необычные животные больше всего обитают в редколесьях и саваннах Южной и Восточной Африки. Жирафы употребляют в пищу растительность, отдавая предпочтение листве акации.', N'У жирафов самое большое сердце среди всех наземных млекопитающих, его вес достигает 11 кг.', N'Giraffe.jpg', N'Giraffe.mp3')
INSERT [dbo].[Animals] ([ID], [Name], [About], [Facts], [ImageSRC], [SoundSRC]) VALUES (3, N'Зебра', N'Непарнокопытные млекопитающие относятся к семейству лошадиных. Длина тела зебры приблизительно 2 м, вес – максимум 360 кг. Полосатые животные делятся на виды: саванная, пустынная, горная. Первые два типа обитают в степях на востоке материка и южных районах континента. Малочисленные горные встречаются в высокогорьях. Животные – травоядные. Чтобы удовлетворить организм достаточным количеством калорий, зебры употребляют за сутки 5-7 кг пищи.', N'Самый главный вопрос про зебр - в какую полоску, в черную с белыми или наоборот? Оказываться зебры черные с белыми полосками, потому что в ранней эмбриональной стадии зебры черные, а уже затем развивают белые полосы.', N'Zebra.jpg', N'Zebra.mp3')
INSERT [dbo].[Animals] ([ID], [Name], [About], [Facts], [ImageSRC], [SoundSRC]) VALUES (4, N'Утконос', N'Уникальное животное является млекопитающим. Вес – 2 кг, длина с хвостом – 55 см. Зверек неуклюжий, но надежно защищен от врагов токсичной слюной, которая очень опасна для небольших животных. Утконосы проживают на прибрежных территориях небольших пресных рек, озер, в заболоченной местности. В ежедневный рацион входят моллюски, черви, ракообразные, личинки, водная растительность.', N'Несмотря на то, что утконосы откладывают яйца, они также производят молоко для своих малышей. Но делают они это не так, как другие животные. Они концентрируют молоко в своем брюхе, а затем выпотевают, чтобы их детеныши могли всасывать его из складок кожи или меха.', N'Duckbill.jpg', N'Duckbill.mp3')
INSERT [dbo].[Animals] ([ID], [Name], [About], [Facts], [ImageSRC], [SoundSRC]) VALUES (5, N'Капибара', N'Капибара – самый крупный грызун мировой фауны, достигающий 1,5 м длины и массы 60 кг. Внешне капибара похожа на гигантскую большеголовую морскую свинку. Пальцы ног имеют небольшие плавательные перепонки. Тело покрыто рыжевато-бурыми волосами, жесткими, как щетина.', N'Капибару поистине можно назвать самым миролюбивым животным на планете. Не было зарегистрировано случаев покушения на человека и других сородичей.', N'Capybara.jpg', N'Capybara.mp3')
INSERT [dbo].[Animals] ([ID], [Name], [About], [Facts], [ImageSRC], [SoundSRC]) VALUES (6, N'Волк', N'Крупное хищное млекопитающее. Длина тела может достигать 160 см, длина хвоста — до 52 см, масса тела может доходить до 80 кг. Размеры и общая масса волков меняются в зависимости от окружающего климата. Мех достаточно густой различного окраса – от почти белого до серо-бурого или рыжего оттенка.', N'Эти опасные хищники испытывают сильнейшую эмоциональную привязанность. Они моногамны — выбирают свою пару один раз и на всю жизнь. Надо сказать, что волк – это идеальный семьянин.', N'Wolf.jpg', N'Wolf.mp3')
INSERT [dbo].[Animals] ([ID], [Name], [About], [Facts], [ImageSRC], [SoundSRC]) VALUES (7, N'Лиса обыкновенная', N'Хищное млекопитающее семейства псовых, самый крупный вид рода лисиц. Длина тела достигает почти одного метра, хвоста — 40–60 см, а масса — 6–10 кг. Самки немного легче и меньше самцов. У лисы большие глаза с вертикальным зрачком, зверь обладает отличным зрением.', N'Лисы могут идентифицировать голоса друг друга, как и люди. Рыжая лиса имеет 28 различных звуков, которые они используют для общения.', N'Fox.jpg', N'Fox.mp3')
INSERT [dbo].[Animals] ([ID], [Name], [About], [Facts], [ImageSRC], [SoundSRC]) VALUES (8, N'Сурикат', N'Хищники, и основная их еда — насекомые и другие беспозвоночные. С удовольствием едят они и ящериц, и птичьи яйца, и мелких грызунов, и их детёнышей. Широко известна уникальная способность сурикат поедать животных, чей яд способен убить даже человека.', N'Сурикаты обладают некоторой устойчивостью к яду некоторых животных. Например, они могут поймать и съесть скорпионов, даже тех, которые ядовиты для других животных.', N'meerkat.jpg', N'meerkat.mp3')
INSERT [dbo].[Animals] ([ID], [Name], [About], [Facts], [ImageSRC], [SoundSRC]) VALUES (9, N'Сине-желтый Ара', N'Длина его тела составляет до метра, более мелкие разновидности могут быть 30-40 см в длину. Ара общителен и умен, но его сложно назвать покладистым питомцем. Он своенравен и требует серьезного воспитания. В ответ на грубое или жестокое обращение ара без колебаний пускает в ход свой страшный клюв.', N'Для сине-желтых ара характерно расселение в парах, но и в стаи они могут объединяться. У них сильно развит родительский инстинкт, эти птицы хорошо заботятся о птенцах. Оба родителя могут стать агрессивными к тем, кто покушается на жизнь птенцов.', N'Ara ararauna.jpg', N'Ara ararauna.mp3')
INSERT [dbo].[Animals] ([ID], [Name], [About], [Facts], [ImageSRC], [SoundSRC]) VALUES (10, N'Енот полоскун', N'Длина тела взрослого зверька колеблется от 45 до 60 сантиметров, а с хвостом увеличивается до 70-85-ти. Вес достигает девяти килограммов, но осенью увеличивается из-за жировых отложений, которые жизненно необходимы для зимней спячки. Окрас у енотов коричневый с серым отливом. Мех плотный, густой.', N'Когда первооткрыватели высадились у берегов Американского континента еноты почти сразу завоевали внимание исследователей, потому что не были похожи ни на одно известное животное. Долгое время ученые пытались их приписать к собаками, кошкам, барсукам и даже медведям! Однако, после долгих дискуссий, для енотов было создано отдельное семейство «енотовые», которое они по сей день делят с носухами и кинкажу.', N'Procyon.jpg', N'Procyon.mp3')
SET IDENTITY_INSERT [dbo].[Animals] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([ID], [Name]) VALUES (1, N'Moderator')
INSERT [dbo].[Roles] ([ID], [Name]) VALUES (2, N'User')
INSERT [dbo].[Roles] ([ID], [Name]) VALUES (3, N'Guest')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([ID], [Name], [Email], [Login], [Password], [Status], [Role], [Image]) VALUES (1, N'Кузнецов Владислав Игоревич', N'Armoryx2005@mail.ru', N'Armoryx', N'Moderator', 1, 1, NULL)
INSERT [dbo].[Users] ([ID], [Name], [Email], [Login], [Password], [Status], [Role], [Image]) VALUES (2, N'Кузнецов Владислав', N'User@mail.ru', N'User', N'123', 1, 2, N'Capybara.jpg')
INSERT [dbo].[Users] ([ID], [Name], [Email], [Login], [Password], [Status], [Role], [Image]) VALUES (3, N'Guest', NULL, N'', NULL, 1, 3, NULL)
INSERT [dbo].[Users] ([ID], [Name], [Email], [Login], [Password], [Status], [Role], [Image]) VALUES (5, N'Батон', N'Baton@mail.ru', N'Baton', N'12345', 1, 2, NULL)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles] FOREIGN KEY([Role])
REFERENCES [dbo].[Roles] ([ID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles]
GO
USE [master]
GO
ALTER DATABASE [Kuznetsov IS-21-3 Zoo] SET  READ_WRITE 
GO
