using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DotvvmMinutes.DefaultButtons.Model;
using DotVVM.BusinessPack.Controls;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.Hosting;

namespace DotvvmMinutes.DefaultButtons.ViewModels
{
    public class GridViewModel : MasterPageViewModel
    {
		public BusinessPackDataSet<CountryDTO> Countries { get; set; }

        public string OriginalName { get; set; }

        public override Task PreRender()
        {
            if (!Context.IsPostBack)
            {
                Countries = new BusinessPackDataSet<CountryDTO>()
                {
                    Items = new List<CountryDTO>()
                    {
                        new CountryDTO() { Id = 1, Name = "Czech Republic" },
                        new CountryDTO() { Id = 2, Name = "Slovakia" },
                        new CountryDTO() { Id = 3, Name = "Austria" },
                        new CountryDTO() { Id = 4, Name = "Poland" },
                        new CountryDTO() { Id = 5, Name = "Germany" }
                    },
                    RowEditOptions =
                    {
                        PrimaryKeyPropertyName = nameof(CountryDTO.Id)
                    }
                };
                
            }
            return base.PreRender();
        }

        public void Edit(CountryDTO item)
        {
            Countries.RowEditOptions.EditRowId = item.Id;
            OriginalName = item.Name;
        }

        public void Save(CountryDTO item)
        {
            Countries.RowEditOptions.EditRowId = null;
        }

        public void CancelEdit(CountryDTO item)
        {
            Countries.RowEditOptions.EditRowId = null;
            item.Name = OriginalName;
            OriginalName = null;
        }
    }

}
