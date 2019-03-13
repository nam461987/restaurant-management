




















// This file was automatically generated by the PetaPoco T4 Template
// Do not make changes directly to this file - edit the template instead
// 
// The following connection settings were used to generate this file
// 
//     Connection String Name: `RestaurantCon`
//     Provider:               `System.Data.SqlClient`
//     Connection String:      `Persist Security Info = True;Data Source=mi3-wsq4.a2hosting.com;Network Library=DBMSSOCN;Initial Catalog=Restaurant;User ID=namto;Password=P@ssword`
//     Schema:                 ``
//     Include Views:          `True`



using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;

namespace RestaurantNS
{

	public partial class RestaurantRepo : Database
	{
		public RestaurantRepo() 
			: base("RestaurantCon")
		{
			CommonConstruct();
		}

		public RestaurantRepo(string connectionStringName) 
			: base(connectionStringName)
		{
			CommonConstruct();
		}
		
		partial void CommonConstruct();
		
		public interface IFactory
		{
			RestaurantRepo GetInstance();
		}
		
		public static IFactory Factory { get; set; }
        public static RestaurantRepo GetInstance()
        {
			if (_instance!=null)
				return _instance;
				
			if (Factory!=null)
				return Factory.GetInstance();
			else
				return new RestaurantRepo();
        }

		[ThreadStatic] static RestaurantRepo _instance;
		
		public override void OnBeginTransaction()
		{
			if (_instance==null)
				_instance=this;
		}
		
		public override void OnEndTransaction()
		{
			if (_instance==this)
				_instance=null;
		}
        

		public class Record<T> where T:new()
		{
			public static RestaurantRepo repo { get { return RestaurantRepo.GetInstance(); } }
			public bool IsNew() { return repo.IsNew(this); }
			public object Insert() { return repo.Insert(this); }

			public void Save() { repo.Save(this); }
			public int Update() { return repo.Update(this); }

			public int Update(IEnumerable<string> columns) { return repo.Update(this, columns); }
			public static int Update(string sql, params object[] args) { return repo.Update<T>(sql, args); }
			public static int Update(Sql sql) { return repo.Update<T>(sql); }
			public int Delete() { return repo.Delete(this); }
			public static int Delete(string sql, params object[] args) { return repo.Delete<T>(sql, args); }
			public static int Delete(Sql sql) { return repo.Delete<T>(sql); }
			public static int Delete(object primaryKey) { return repo.Delete<T>(primaryKey); }
			public static bool Exists(object primaryKey) { return repo.Exists<T>(primaryKey); }
			public static bool Exists(string sql, params object[] args) { return repo.Exists<T>(sql, args); }
			public static T SingleOrDefault(object primaryKey) { return repo.SingleOrDefault<T>(primaryKey); }
			public static T SingleOrDefault(string sql, params object[] args) { return repo.SingleOrDefault<T>(sql, args); }
			public static T SingleOrDefault(Sql sql) { return repo.SingleOrDefault<T>(sql); }
			public static T FirstOrDefault(string sql, params object[] args) { return repo.FirstOrDefault<T>(sql, args); }
			public static T FirstOrDefault(Sql sql) { return repo.FirstOrDefault<T>(sql); }
			public static T Single(object primaryKey) { return repo.Single<T>(primaryKey); }
			public static T Single(string sql, params object[] args) { return repo.Single<T>(sql, args); }
			public static T Single(Sql sql) { return repo.Single<T>(sql); }
			public static T First(string sql, params object[] args) { return repo.First<T>(sql, args); }
			public static T First(Sql sql) { return repo.First<T>(sql); }
			public static List<T> Fetch(string sql, params object[] args) { return repo.Fetch<T>(sql, args); }
			public static List<T> Fetch(Sql sql) { return repo.Fetch<T>(sql); }
			public static List<T> Fetch(long page, long itemsPerPage, string sql, params object[] args) { return repo.Fetch<T>(page, itemsPerPage, sql, args); }
			public static List<T> Fetch(long page, long itemsPerPage, Sql sql) { return repo.Fetch<T>(page, itemsPerPage, sql); }
			public static List<T> SkipTake(long skip, long take, string sql, params object[] args) { return repo.SkipTake<T>(skip, take, sql, args); }
			public static List<T> SkipTake(long skip, long take, Sql sql) { return repo.SkipTake<T>(skip, take, sql); }
			public static Page<T> Page(long page, long itemsPerPage, string sql, params object[] args) { return repo.Page<T>(page, itemsPerPage, sql, args); }
			public static Page<T> Page(long page, long itemsPerPage, Sql sql) { return repo.Page<T>(page, itemsPerPage, sql); }
			public static IEnumerable<T> Query(string sql, params object[] args) { return repo.Query<T>(sql, args); }
			public static IEnumerable<T> Query(Sql sql) { return repo.Query<T>(sql); }

		}

	}
	



    

	[TableName("namto.Admin_Account")]



	[PrimaryKey("Id")]




	[ExplicitColumns]

    public partial class Admin_Account : RestaurantRepo.Record<Admin_Account>  
    {



		[Column] public int Id { get; set; }





		[Column] public int RestaurantId { get; set; }





		[Column] public int BranchId { get; set; }





		[Column] public int? TypeId { get; set; }





		[Column] public string UserName { get; set; }





		[Column] public string Email { get; set; }





		[Column] public string Mobile { get; set; }





		[Column] public string Home { get; set; }





		[Column] public string FullName { get; set; }





		[Column] public DateTime? BirthDate { get; set; }





		[Column] public string Address { get; set; }





		[Column] public string Avatar { get; set; }





		[Column] public string Description { get; set; }





		[Column] public string Gender { get; set; }





		[Column] public string IdentityNumber { get; set; }





		[Column] public string PasswordHash { get; set; }





		[Column] public int? Active { get; set; }





		[Column] public int? Status { get; set; }





		[Column] public DateTime? CreatedDate { get; set; }





		[Column] public int? CreatedStaffId { get; set; }





		[Column] public DateTime? UpdatedDate { get; set; }





		[Column] public int? UpdatedStaffId { get; set; }



	}

    

	[TableName("namto.Admin_Group")]



	[PrimaryKey("Id")]




	[ExplicitColumns]

    public partial class Admin_Group : RestaurantRepo.Record<Admin_Group>  
    {



		[Column] public int Id { get; set; }





		[Column] public int RestaurantId { get; set; }





		[Column] public int BranchId { get; set; }





		[Column] public string Code { get; set; }





		[Column] public string Name { get; set; }





		[Column] public string Description { get; set; }





		[Column] public int? Status { get; set; }





		[Column] public DateTime? CreatedDate { get; set; }





		[Column] public int? CreatedStaffId { get; set; }





		[Column] public DateTime? UpdatedDate { get; set; }





		[Column] public int? UpdatedStaffId { get; set; }



	}

    

	[TableName("namto.Admin_Group_Permission")]



	[PrimaryKey("Id")]




	[ExplicitColumns]

    public partial class Admin_Group_Permission : RestaurantRepo.Record<Admin_Group_Permission>  
    {



		[Column] public int Id { get; set; }





		[Column] public int RestaurantId { get; set; }





		[Column] public int BranchId { get; set; }





		[Column] public int GroupId { get; set; }





		[Column] public int PermissionId { get; set; }





		[Column] public int? Status { get; set; }





		[Column] public DateTime? CreatedDate { get; set; }





		[Column] public int? CreatedStaffId { get; set; }





		[Column] public DateTime? UpdatedDate { get; set; }





		[Column] public int? UpdatedStaffId { get; set; }



	}

    

	[TableName("namto.Admin_Permission")]



	[PrimaryKey("Id")]




	[ExplicitColumns]

    public partial class Admin_Permission : RestaurantRepo.Record<Admin_Permission>  
    {



		[Column] public int Id { get; set; }





		[Column] public string Code { get; set; }





		[Column] public string Name { get; set; }





		[Column] public string Description { get; set; }





		[Column] public int? Status { get; set; }





		[Column] public DateTime? CreatedDate { get; set; }





		[Column] public int? CreatedStaffId { get; set; }





		[Column] public DateTime? UpdatedDate { get; set; }





		[Column] public int? UpdatedStaffId { get; set; }



	}

    

	[TableName("namto.Customer")]



	[PrimaryKey("Id")]




	[ExplicitColumns]

    public partial class Customer : RestaurantRepo.Record<Customer>  
    {



		[Column] public int Id { get; set; }





		[Column] public int RestaurantId { get; set; }





		[Column] public int? BranchId { get; set; }





		[Column] public int? TypeId { get; set; }





		[Column] public string Name { get; set; }





		[Column] public string Phone { get; set; }





		[Column] public string Email { get; set; }





		[Column] public string Description { get; set; }





		[Column] public int? Status { get; set; }



	}

    

	[TableName("namto.Customer_Type")]



	[PrimaryKey("Id")]




	[ExplicitColumns]

    public partial class Customer_Type : RestaurantRepo.Record<Customer_Type>  
    {



		[Column] public int Id { get; set; }





		[Column] public int RestaurantId { get; set; }





		[Column] public int? BranchId { get; set; }





		[Column] public string Name { get; set; }





		[Column] public string Description { get; set; }





		[Column] public int? Status { get; set; }



	}

    

	[TableName("namto.Ingredient")]



	[PrimaryKey("Id")]




	[ExplicitColumns]

    public partial class Ingredient : RestaurantRepo.Record<Ingredient>  
    {



		[Column] public int Id { get; set; }





		[Column] public int RestaurantId { get; set; }





		[Column] public int? BranchId { get; set; }





		[Column] public string Name { get; set; }





		[Column] public string Description { get; set; }





		[Column] public int? Status { get; set; }



	}

    

	[TableName("namto.Menu")]



	[PrimaryKey("Id")]




	[ExplicitColumns]

    public partial class Menu : RestaurantRepo.Record<Menu>  
    {



		[Column] public int Id { get; set; }





		[Column] public int RestaurantId { get; set; }





		[Column] public int BranchId { get; set; }





		[Column] public int CategoryId { get; set; }





		[Column] public string Name { get; set; }





		[Column] public string Description { get; set; }





		[Column] public string Image { get; set; }





		[Column] public double? Price { get; set; }





		[Column] public int? UnitId { get; set; }





		[Column] public int? Status { get; set; }



	}

    

	[TableName("namto.Menu_Category")]



	[PrimaryKey("Id")]




	[ExplicitColumns]

    public partial class Menu_Category : RestaurantRepo.Record<Menu_Category>  
    {



		[Column] public int Id { get; set; }





		[Column] public int RestaurantId { get; set; }





		[Column] public int BranchId { get; set; }





		[Column] public string Name { get; set; }





		[Column] public string Avatar { get; set; }





		[Column] public string Description { get; set; }





		[Column] public int? Status { get; set; }



	}

    

	[TableName("namto.Menu_Definition")]



	[PrimaryKey("Id")]




	[ExplicitColumns]

    public partial class Menu_Definition : RestaurantRepo.Record<Menu_Definition>  
    {



		[Column] public int Id { get; set; }





		[Column] public int RestaurantId { get; set; }





		[Column] public int? BranchId { get; set; }





		[Column] public int MenuId { get; set; }





		[Column] public int IngredientId { get; set; }





		[Column] public double? Quantity { get; set; }





		[Column] public int? UnitId { get; set; }





		[Column] public string Description { get; set; }





		[Column] public int? Status { get; set; }



	}

    

	[TableName("namto.Menu_Unit")]



	[PrimaryKey("Id")]




	[ExplicitColumns]

    public partial class Menu_Unit : RestaurantRepo.Record<Menu_Unit>  
    {



		[Column] public int Id { get; set; }





		[Column] public int RestaurantId { get; set; }





		[Column] public int? BranchId { get; set; }





		[Column] public string Name { get; set; }





		[Column] public int? Status { get; set; }



	}

    

	[TableName("namto.Restaurant")]



	[PrimaryKey("Id")]




	[ExplicitColumns]

    public partial class Restaurant : RestaurantRepo.Record<Restaurant>  
    {



		[Column] public int Id { get; set; }





		[Column] public string Name { get; set; }





		[Column] public string Email { get; set; }





		[Column] public string Address { get; set; }





		[Column] public string Phone { get; set; }





		[Column] public string Description { get; set; }





		[Column] public int? Status { get; set; }



	}

    

	[TableName("namto.Restaurant_Branch")]



	[PrimaryKey("Id")]




	[ExplicitColumns]

    public partial class Restaurant_Branch : RestaurantRepo.Record<Restaurant_Branch>  
    {



		[Column] public int Id { get; set; }





		[Column] public int RestaurantId { get; set; }





		[Column] public string Name { get; set; }





		[Column] public string Address { get; set; }





		[Column] public string Phone { get; set; }





		[Column] public string OpenTime { get; set; }





		[Column] public string CloseTime { get; set; }





		[Column] public int? AllDay { get; set; }





		[Column] public int? Status { get; set; }



	}

    

	[TableName("namto.Restaurant_Table")]



	[PrimaryKey("Id")]




	[ExplicitColumns]

    public partial class Restaurant_Table : RestaurantRepo.Record<Restaurant_Table>  
    {



		[Column] public int Id { get; set; }





		[Column] public int RestaurantId { get; set; }





		[Column] public int BranchId { get; set; }





		[Column] public string Name { get; set; }





		[Column] public string Location { get; set; }





		[Column] public string Description { get; set; }





		[Column] public int? Status { get; set; }



	}

    

	[TableName("namto.Supplier")]



	[PrimaryKey("Id")]




	[ExplicitColumns]

    public partial class Supplier : RestaurantRepo.Record<Supplier>  
    {



		[Column] public int Id { get; set; }





		[Column] public int RestaurantId { get; set; }





		[Column] public int? BranchId { get; set; }





		[Column] public string Name { get; set; }





		[Column] public string Address { get; set; }





		[Column] public string Contact { get; set; }





		[Column] public string Description { get; set; }





		[Column] public int? Status { get; set; }



	}

    

	[TableName("namto.Admin_Group_Permission_View00")]




	[ExplicitColumns]

    public partial class Admin_Group_Permission_View00 : RestaurantRepo.Record<Admin_Group_Permission_View00>  
    {



		[Column] public int Id { get; set; }





		[Column] public int RestaurantId { get; set; }





		[Column] public int BranchId { get; set; }





		[Column] public int GroupId { get; set; }





		[Column] public int PermissionId { get; set; }





		[Column] public int? Status { get; set; }





		[Column] public DateTime? CreatedDate { get; set; }





		[Column] public int? CreatedStaffId { get; set; }





		[Column] public DateTime? UpdatedDate { get; set; }





		[Column] public int? UpdatedStaffId { get; set; }





		[Column] public string PermissionIdCode { get; set; }





		[Column] public string PermissionIdName { get; set; }





		[Column] public string PermissionDescription { get; set; }



	}

    

	[TableName("namto.Restaurant_Branch_View00")]




	[ExplicitColumns]

    public partial class Restaurant_Branch_View00 : RestaurantRepo.Record<Restaurant_Branch_View00>  
    {



		[Column] public int Id { get; set; }





		[Column] public int RestaurantId { get; set; }





		[Column] public string Name { get; set; }





		[Column] public string Address { get; set; }





		[Column] public string Phone { get; set; }





		[Column] public string OpenTime { get; set; }





		[Column] public string CloseTime { get; set; }





		[Column] public int? AllDay { get; set; }





		[Column] public int? Status { get; set; }





		[Column] public string RestaurantIdName { get; set; }



	}


}
