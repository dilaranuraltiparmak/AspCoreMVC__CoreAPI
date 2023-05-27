using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository
{
    public class AppDbContext:DbContext
	{
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {
            
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

		public DbSet<Productfeature> Productfeatures { get; set; }


        //public DbSet<Productfeature> Productfeatures { get; set; } şimdi biz burada bu kısmı silip ekleyeceğimiz şey product'a bağlı olsun dersek bu tabloyu kullanmak yerine producttan da ulaşabiliriz örn:
        //    var p=new Product() { Productfeature = new Productfeature() };


        public override int SaveChanges()
        {
            //burada veritabanına kaydedilmeden once yapılan isleme gore createddate mı updatedate mi kısmı buradaki koda gore islenir
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is BaseEntity entityReference)
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                            {
                                entityReference.CreatedDate = DateTime.Now;
                                break;
                            }
                        case EntityState.Modified:
                            {
                                Entry(entityReference).Property(x => x.CreatedDate).IsModified=false;
                                entityReference.UpdatedDate= DateTime.Now;
                                break;
                            }
                    }
                }
            }
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

			//burada veritabanına kaydedilmeden once yapılan isleme gore createddate mı updatedate mi kısmı buradaki koda gore islenir
			foreach (var item in ChangeTracker.Entries())
			{
				if(item.Entity is BaseEntity entityReference)
				{
					switch (item.State)
					{
						case EntityState.Added:
							{
								entityReference.CreatedDate = DateTime.Now;
								break;
							}
							case EntityState.Modified: {

								Entry(entityReference).Property(x => x.CreatedDate).IsModified=false;//guncelleme yapacagı zaman yaratılmatarihini silmeyi engeller
							entityReference.UpdatedDate= DateTime.Now;
								break;
							}
					}
				}
			}
             







            return base.SaveChangesAsync(cancellationToken);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
			//git tüm assemblydeki solutiondaki configurationları oku nerden anlayacak IEntityTypeConfiguration2a bağlı olanalrdan

			modelBuilder.Entity<Productfeature>().HasData(new Productfeature()
			{
				Id=1,
				Color="Kırmızı",
				Height=100,
				Width=200,
				ProductId=1
			},
			new Productfeature()
			{
				Id=2,
				Color="Mor",
				Height=150,
				Width=300,
				ProductId=2
			});
		}
	}

}
