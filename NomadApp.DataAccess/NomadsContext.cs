namespace NomadApp.DataAccess
{
    using NomadApp.Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class NomadsContext : DbContext
    {
        // Контекст настроен для использования строки подключения "NomadsContext" из файла конфигурации  
        // приложения (App.config или Web.config). По умолчанию эта строка подключения указывает на базу данных 
        // "NomadApp.DataAccess.NomadsContext" в экземпляре LocalDb. 
        // 
        // Если требуется выбрать другую базу данных или поставщик базы данных, измените строку подключения "NomadsContext" 
        // в файле конфигурации приложения.
        public NomadsContext()
            : base("name=NomadsContext")
        {
            Database.SetInitializer(new DataInitializer());
        }

       public DbSet<Client> Clients { get; set; }
       public DbSet<SubscriptionRegistration> SubscriptionRegistrations { get; set; }
       public DbSet<Subscription> Subscriptions { get; set; }

        // Добавьте DbSet для каждого типа сущности, который требуется включить в модель. Дополнительные сведения 
        // о настройке и использовании модели Code First см. в статье http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}