using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;

namespace AdvUnitTesting.Core
{
	[ImplementPropertyChanged]
	public class UserDetailViewModel
	{

		public UserDetailViewModel()
		{

		}
		public async Task LoadUser(int id)
		{
			await Task.Run(() => { });
		}

		public async Task Save()
		{

		}


		//Properties
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public bool IsLoggedIn { get; set; }
		public bool IsDirty { get; set; }

		public bool IsFavorite { get; private set; }
		//MakeFavorite
		//Login

		//Logout
	}
}
